using OfficeOpenXml;
using System.IO;
using System.Linq;

namespace TableTendersAPIService.Models
{
    public static class SampleData
    {
        const string FILE = @"Data.xlsx";
        const int WORKSHEET_START_ROW = 1;
        const string WORKSHEET_NAME = "Лист1";


        public static void Initialize(TenderContext context)
        {
            if (!context.Tenders.Any())
            {
                FileInfo excelFile = new FileInfo(FILE);
                using (ExcelPackage excel = new ExcelPackage(excelFile))
                {
                    var sheet = excel.Workbook.Worksheets[WORKSHEET_NAME];
                    for (int i = WORKSHEET_START_ROW + 1; i <= sheet.Dimension.End.Row; ++i)
                    {
                        context.Tenders.AddRange(
                            new Tender
                            {
                                Name = sheet.Cells[i, 1].Value.ToString(),
                                StartDate = sheet.Cells[i, 2].Value.ToString(),
                                ExpirationDate = sheet.Cells[i, 3].Value.ToString(),
                                TenderSiteURL = sheet.Cells[i, 4].Value.ToString()
                            }
                            );
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
