namespace LinkShortener.DataLayer.Models
{
    public class Link
    {
        public virtual Guid Link_ID { get; set; }
        public virtual string Long_Link { get; set; }
        public virtual string Short_Link { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}