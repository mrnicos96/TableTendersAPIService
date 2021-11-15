namespace TableTendersAPIService.Models
{
    public class Tender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string ExpirationDate { get; set; }
        public string TenderSiteURL { get; set; }
    }
}