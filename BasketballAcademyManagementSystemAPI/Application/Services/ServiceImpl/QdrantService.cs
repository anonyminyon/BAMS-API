using System.Net;
using System.Text.Json;
using System.Text;
using BasketballAcademyManagementSystemAPI.Common.Constants;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class QdrantService : IQdrantService
    {
        private readonly HttpClient _http;
        private readonly string _endpoint;
        private readonly string _apiKey;

        public QdrantService(HttpClient http)
        {
            _http = http;
            _endpoint = Environment.GetEnvironmentVariable("QDRANT_END_POINT") ?? throw new InvalidOperationException("QDRANT_END_POINT is not set in the environment variables.");
            _apiKey = Environment.GetEnvironmentVariable("QDRANT_API_KEY") ?? throw new InvalidOperationException("QDRANT_API_KEY is not set in the environment variables.");

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("api-key", _apiKey);
        }

        public async Task<bool> InsertChunksAsync(List<(string text, float[] vector)> chunks, string useFor)
        {
            // Get the collection name based on the use for purpose
            var collectionName = useFor == ChatbotConstant.UseForGuest
                ? ChatbotConstant.ChatbotDocumentForGuestCollectionName
                : null;

            if (collectionName == null)
            {
                return false;
            }

            await EnsureCollectionExistsAsync(collectionName);

            // Add each chunk vector to the points list
            var points = new List<object>();
            for (int i = 0; i < chunks.Count; i++)
            {
                var (text, vector) = chunks[i];

                points.Add(new
                {
                    id = Guid.NewGuid().ToString(),
                    vector = vector,
                    payload = new { text = text }
                });
            }

            var requestBody = new
            {
                points = points
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PutAsync($"{_endpoint}/collections/{collectionName}/points?wait=true", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return false;
            }
            return true;
        }

        public async Task EnsureCollectionExistsAsync(string collectionName)
        {
            var endpoint = $"{_endpoint}/collections/{collectionName}";
            var checkRes = await _http.GetAsync(endpoint);

            if (checkRes.StatusCode == HttpStatusCode.OK)
            {
                return;
            }
            else if (checkRes.StatusCode == HttpStatusCode.NotFound)
            {
                var createBody = new
                {
                    vectors = new { size = 1024, distance = "Cosine" }
                };

                var res = await _http.PutAsync(endpoint,
                    new StringContent(JsonSerializer.Serialize(createBody), Encoding.UTF8, "application/json"));

                res.EnsureSuccessStatusCode();
            }
        }

        public async Task<string?> SearchRelevantContextAsync(float[] vector, string useFor)
        {
            var collectionName = useFor == ChatbotConstant.UseForGuest
                ? ChatbotConstant.ChatbotDocumentForGuestCollectionName
                : null;

            if (collectionName == null)
            {
                return null;
            }

            await EnsureCollectionExistsAsync(collectionName);

            var query = new
            {
                vector,
                top = 4,
                with_payload = true,
                score_threshold = 0.2
            };

            var body = JsonSerializer.Serialize(query);
            var res = await _http.PostAsync(
                $"{_endpoint}/collections/{collectionName}/points/search",
                new StringContent(body, Encoding.UTF8, "application/json"));

            var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());

            var results = json.RootElement.GetProperty("result");

            if (results.GetArrayLength() == 0)
            {
                return null;
            }

            var texts = results.EnumerateArray()
                .Select(result => result.GetProperty("payload").GetProperty("text").GetString())
                .Where(text => !string.IsNullOrEmpty(text))
                .ToList();

            return texts.Count > 0 ? string.Join("\n", texts) : null;

        }

        public async Task DeleteAllAsync(string useFor)
        {
            if (useFor == ChatbotConstant.UseForGuest)
            {
                var url = $"{_endpoint}/collections/{ChatbotConstant.ChatbotDocumentForGuestCollectionName}/points/delete";
                var body = JsonSerializer.Serialize(new { filter = new { must = new object[] { } } });
                await _http.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            }
        }

        public async Task<List<VectorItem>> ListVectorsAsync(string useFor, int limit = 20)
        {
            var collectionName = useFor == ChatbotConstant.UseForGuest
                ? ChatbotConstant.ChatbotDocumentForGuestCollectionName
                : null;
            if (collectionName == null)
            {
                return new List<VectorItem>();
            }

            await EnsureCollectionExistsAsync(collectionName);

            var url = $"{_endpoint}/collections/{collectionName}/points/scroll";
            var body = JsonSerializer.Serialize(new
            {
                limit,
                with_vector = false,
                with_payload = true
            });

            var res = await _http.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            res.EnsureSuccessStatusCode();

            var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
            var points = doc.RootElement.GetProperty("result").GetProperty("points");

            return points.EnumerateArray().Select(pt => new VectorItem
            {
                Id = pt.GetProperty("id").ToString(),
                Text = pt.GetProperty("payload").GetProperty("text").GetString()
            }).ToList();
        }

        public async Task<bool> DeleteCollectionAsync(string useFor)
        {
            var collectionName = useFor == ChatbotConstant.UseForGuest
                ? ChatbotConstant.ChatbotDocumentForGuestCollectionName
                : null;
            if (collectionName == null)
            {
                return false;
            }

            await EnsureCollectionExistsAsync(collectionName);

            // Delete the collection
            var url = $"{_endpoint}/collections/{collectionName}";
            var res = await _http.DeleteAsync(url);

            if (res.IsSuccessStatusCode)
            {
                Console.WriteLine($"✅ Deleted collection '{collectionName}'");
                return true;
            }

            var body = await res.Content.ReadAsStringAsync();
            Console.WriteLine($"⚠️ Failed to delete collection '{collectionName}': {res.StatusCode}\n{body}");
            return false;
        }

        public async Task<List<QdrantService.VectorItem>> ListChatbotVectorsAsync(string useFor, int limit = 20)
        {
            var collectionName = useFor == ChatbotConstant.UseForGuest
               ? ChatbotConstant.ChatbotDocumentForGuestCollectionName
               : null;
            if (collectionName == null)
            {
                return await Task.FromResult(new List<QdrantService.VectorItem>());
            }

            await EnsureCollectionExistsAsync(collectionName);

            var url = $"{_endpoint}/collections/{collectionName}/points/scroll";
            var body = JsonSerializer.Serialize(new
            {
                limit,
                with_vector = false,
                with_payload = true
            });

            var res = await _http.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            res.EnsureSuccessStatusCode();

            var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
            var points = doc.RootElement.GetProperty("result").GetProperty("points");

            return points.EnumerateArray().Select(pt => new VectorItem
            {
                Id = pt.GetProperty("id").ToString(),
                Text = pt.GetProperty("payload").GetProperty("text").GetString()
            }).ToList();
        }

        public class VectorItem
        {
            public string Id { get; set; } = string.Empty;
            public string Text { get; set; } = string.Empty;
        }
    }
}
