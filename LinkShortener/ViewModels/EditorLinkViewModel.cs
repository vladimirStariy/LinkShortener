using System.ComponentModel;

namespace LinkShortener.ViewModels
{
    public class EditorLinkViewModel
    {
        public int id { get; set; }
        [DisplayName("Ссылка: ")]
        public string Long_Link { get; set; }
    }
}
