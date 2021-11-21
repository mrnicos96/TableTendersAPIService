using System.Linq;
using System.IO;

namespace TableTendersAPIService.Models
{
    public static class SampleData
    {
        public static void Initialize(TenderContext context)
        {
            if (context.Tenders.Any())
            {
                context.RemoveRange(context.Tenders);
                context.SaveChanges();
            }
            if (Directory.Exists(@"wwwroot\Upload\"))
            {
                Directory.Delete(@"wwwroot\Upload\", true);
            }
        }
    }
}
