using System.ComponentModel;

namespace LinkShortener.ViewModels
{
    public class LinkViewModel
    {
        public int Link_ID { get; set; }
        [DisplayName("Длинная ссылка")]
        public string Long_Link { get; set; }
        [DisplayName("Короткая ссылка")]
        public string Short_Link { get; set; }
        [DisplayName("Дата создания")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Количество переходов")]
        public int RedirectCount { get; set; }
    }
}
