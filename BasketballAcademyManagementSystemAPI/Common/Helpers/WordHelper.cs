using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BasketballAcademyManagementSystemAPI.Common.Helpers
{
    public static class WordHelper
    {
        public static List<string> ExtractChunksByMarker(Stream stream)
        {
            var chunks = new List<string>();
            using var wordDoc = WordprocessingDocument.Open(stream, false);
            var body = wordDoc.MainDocumentPart?.Document?.Body;

            if (body == null) return chunks;

            List<string> buffer = new();
            foreach (var para in body.Elements<Paragraph>())
            {
                var text = para.InnerText?.Trim();
                if (string.IsNullOrWhiteSpace(text)) continue;

                if (text.StartsWith("[CHUNK]"))
                {
                    if (buffer.Count > 0)
                    {
                        chunks.Add(string.Join(". ", buffer).Trim());
                        buffer.Clear();
                    }

                    // Bỏ [CHUNK] khỏi dòng tiêu đề
                    buffer.Add(text.Replace("[CHUNK]", "").Trim());
                }
                else
                {
                    buffer.Add(text);
                }
            }

            // Thêm chunk cuối cùng nếu còn
            if (buffer.Count > 0)
            {
                chunks.Add(string.Join("\n", buffer).Trim());
            }

            return chunks;
        }
    }
}
