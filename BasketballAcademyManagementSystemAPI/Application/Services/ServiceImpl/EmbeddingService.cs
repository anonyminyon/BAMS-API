using Amazon.BedrockRuntime.Model;
using Amazon.BedrockRuntime;
using System.Text.Json;
using System.Text;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class EmbeddingService : IEmbeddingService
    {
        private readonly AmazonBedrockRuntimeClient _client;

        public EmbeddingService()
        {
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");

            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("AWS credentials are not set in the environment variables.");
            }

            var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);
            _client = new AmazonBedrockRuntimeClient(credentials, Amazon.RegionEndpoint.APSoutheast2);
        }

        public async Task<float[]> GetEmbeddingAsync(string text)
        {
            const int maxRetries = 3;
            const int baseDelayMs = 500;
            var rng = new Random();

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    var requestBody = JsonSerializer.Serialize(new { inputText = text });

                    var request = new InvokeModelRequest
                    {
                        ModelId = "amazon.titan-embed-text-v2:0",
                        ContentType = "application/json",
                        Accept = "application/json",
                        Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody))
                    };

                    var response = await _client.InvokeModelAsync(request);
                    using var reader = new StreamReader(response.Body);
                    var result = await reader.ReadToEndAsync();

                    var json = JsonDocument.Parse(result);
                    return json.RootElement.GetProperty("embedding").Deserialize<float[]>()!;
                }
                catch (ServiceUnavailableException ex) when (attempt < maxRetries)
                {
                    int backoff = (int)(Math.Pow(2, attempt) * baseDelayMs);
                    int jitter = rng.Next(100, 300);
                    int delay = backoff + jitter;

                    Console.WriteLine($"[Retry] Attempt {attempt} failed: {ex.Message}. Retrying in {delay}ms...");
                    await Task.Delay(delay);
                }
            }

            throw new Exception("Retry failed for embedding after max attempts.");
        }
    }
}
