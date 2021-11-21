using System.Linq;

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
        }
    }
}
