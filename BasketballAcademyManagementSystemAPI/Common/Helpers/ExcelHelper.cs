using ClosedXML.Excel;

namespace BasketballAcademyManagementSystemAPI.Common.Helpers
{
    public static class ExcelHelper
    {
        public static byte[] GenerateExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Tryout Basketball");

                // Merge & set title
                worksheet.Range("A1:U1").Merge().Value = "BẢNG CHẤM ĐIỂM TRYOUT YEN HOA BASKETBALL (IST - 2023)";
                worksheet.Range("A1:U1").Style.Font.Bold = true;
                worksheet.Range("A1:U1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Headers
                worksheet.Range("A2:A5").Merge().Value = "SBD";
                worksheet.Range("B2:B5").Merge().Value = "ID";
                worksheet.Range("C2:C5").Merge().Value = "Họ và tên";
                worksheet.Range("D2:D5").Merge().Value = "Giới tính";
                worksheet.Range("E2:E5").Merge().Value = "Ngày sinh";

                // Kỹ năng bóng rổ
                worksheet.Range("F2:N2").Merge().Value = "Kỹ năng bóng rổ";
                worksheet.Range("F3:F5").Merge().Value = "Dẫn bóng";
                worksheet.Range("G3:G5").Merge().Value = "Chuyền bóng";
                worksheet.Range("H3:H5").Merge().Value = "Ném rổ";
                worksheet.Range("I3:I5").Merge().Value = "Kết thúc rổ";

                // Đấu tập
                worksheet.Range("J3:N3").Merge().Value = "Đấu tập";
                worksheet.Range("J4:J5").Merge().Value = "Thái độ (TB)";
                worksheet.Range("K4:K5").Merge().Value = "Lãnh đạo (LS)";
                worksheet.Range("L4:L5").Merge().Value = "Kỹ năng (KN)";
                worksheet.Range("M4:M5").Merge().Value = "Thể lực (TL)";
                worksheet.Range("N4:N5").Merge().Value = "Tư duy (IQ)";

                // Thể lực
                worksheet.Range("O2:X2").Merge().Value = "Thể lực";
                worksheet.Range("O3:O5").Merge().Value = "Hexagon Test (second)";
                worksheet.Range("P3:P5").Merge().Value = "Bật xa tại chỗ (cm)";

                // Bật cao
                worksheet.Range("Q3:R3").Merge().Value = "Bật cao (cm)";
                worksheet.Range("Q4:Q5").Merge().Value = "Không đà";
                worksheet.Range("R4:R5").Merge().Value = "Có đà";

                // Agility & Sprint
                worksheet.Range("S3:S5").Merge().Value = "Agility Test 5-10-5 (second)";
                worksheet.Range("T3:T5").Merge().Value = "Sprint 20m (second)";
                worksheet.Range("U3:U5").Merge().Value = "Push up (reps)";

                // Plank Test
                worksheet.Range("V3:X3").Merge().Value = "Plank Test";
                worksheet.Range("V4:V5").Merge().Value = "Standard Plank (second)";
                worksheet.Range("W4:X4").Merge().Value = "Side Plank (second)";
                worksheet.Cell("W5").Value = "Right";
                worksheet.Cell("X5").Value = "Left";

                // Styling
                worksheet.Range("A2:X5").Style.Font.Bold = true;
                worksheet.Range("A2:X5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Columns().AdjustToContents();

                // Sample data
                worksheet.Cell("A6").Value = "1";
                worksheet.Cell("B6").Value = "3";
                worksheet.Cell("C6").Value = "Nguyễn Hải Phong";
                worksheet.Cell("D6").Value = "Nam";
                worksheet.Cell("F6").Value = "3";
                worksheet.Cell("G6").Value = "1";
                worksheet.Cell("H6").Value = "1";
                worksheet.Cell("I6").Value = "1";
                worksheet.Cell("J6").Value = "0";
                worksheet.Cell("K6").Value = "0";
                worksheet.Cell("L6").Value = "0";
                worksheet.Cell("M6").Value = "0";
                worksheet.Cell("N6").Value = "0";
                worksheet.Cell("O6").Value = "0.5";
                worksheet.Cell("Q6").Value = "0";
                worksheet.Cell("R6").Value = "2.5";
                worksheet.Cell("S6").Value = "1";
                worksheet.Cell("T6").Value = "0";
                worksheet.Cell("U6").Value = "0";
                worksheet.Cell("V6").Value = "0";
                worksheet.Cell("W6").Value = "0";
                worksheet.Cell("X6").Value = "0";

                // Tô viền tất cả các ô chứa dữ liệu
                var dataRange = worksheet.RangeUsed();
                dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                // Căn giữa tất cả nội dung theo cả chiều ngang và chiều dọc
                dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                // Save file
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
