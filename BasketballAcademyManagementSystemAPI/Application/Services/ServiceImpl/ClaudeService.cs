using Amazon.BedrockRuntime.Model;
using Amazon.BedrockRuntime;
using System.Text.Json;
using System.Text;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class ClaudeService : IClaudeService
    {
        private readonly AmazonBedrockRuntimeClient _client;

        public ClaudeService()
        {
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");

            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("AWS credentials are not set in the environment variables.");
            }

            var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);
            _client = new AmazonBedrockRuntimeClient(credentials, Amazon.RegionEndpoint.APSoutheast1);
        }

        public async Task<string> AskAsync(string prompt)
        {
            var body = new
            {
                anthropic_version = "bedrock-2023-05-31",
                messages = new[] {
                new { role = "user", content = prompt }
            },
                max_tokens = 1000,
                temperature = 0.2
            };

            var req = new InvokeModelRequest
            {
                ModelId = "anthropic.claude-instant-v1",
                ContentType = "application/json",
                Accept = "application/json",
                Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body)))
            };

            var res = await _client.InvokeModelAsync(req);
            using var reader = new StreamReader(res.Body);
            var result = await reader.ReadToEndAsync();
            var doc = JsonDocument.Parse(result);
            return doc.RootElement.GetProperty("content")[0].GetProperty("text").GetString();
        }
    }
}
