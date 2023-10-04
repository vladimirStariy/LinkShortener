namespace LinkShortener.DataLayer.Models
{
    public class Link
    {
        public int Link_ID { get; set; }
        public string Long_Link { get; set; }
        public string? Short_Link { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RedirectCount { get; set; }
    }
}