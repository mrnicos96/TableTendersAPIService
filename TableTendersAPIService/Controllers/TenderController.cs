using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TableTendersAPIService.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;


namespace TableTendersAPIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigin")]

    public class TenderController : ControllerBase
    {
        TenderContext db;
        public static IWebHostEnvironment _environment;
        const string WORKSHEET_NAME = "Лист1";
        const int WORKSHEET_START_ROW = 1;

        public TenderController(
            TenderContext context,
            IWebHostEnvironment environment)
        {
            db = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tender>>> Get()
        {
            return await db.Tenders.ToListAsync();
        }

        public class FileUploadAPI
        {
            public IFormFile files { get; set; }
        }

        [HttpPost]
        public async Task<string> Post([FromForm] FileUploadAPI objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();

                        FileInfo excelFile = new FileInfo(@$"wwwroot\Upload\{objFile.files.FileName}");

                        if (!excelFile.Exists)
                            return "Failed - File no found";

                        if (db.Tenders.Any())
                        {
                            db.RemoveRange(db.Tenders);
                            db.SaveChanges();
                        }

                        using (ExcelPackage excel = new ExcelPackage(excelFile))
                        {
                            var sheet = excel.Workbook.Worksheets[WORKSHEET_NAME];
                            for (int i = WORKSHEET_START_ROW + 1; i <= sheet.Dimension.End.Row; ++i)
                            {
                                db.Tenders.AddRange(
                                    new Tender
                                    {
                                        Name = sheet.Cells[i, 1].Value.ToString(),
                                        StartDate = sheet.Cells[i, 2].Value.ToString(),
                                        ExpirationDate = sheet.Cells[i, 3].Value.ToString(),
                                        TenderSiteURL = sheet.Cells[i, 4].Value.ToString()
                                    }
                                    );
                                db.SaveChanges();
                            }
                        }
                        return "\\Upload\\" + objFile.files.FileName;
                    }                    
                }
                else
                {
                    return "Failed";
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
