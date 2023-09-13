using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NorthwindMVCUygulama.ViewModels
{
    public class CustomerListView
    {

        public string CustomerId { get; set; } = null!;

        [Display(Name = "Şirket Adı")]
        public string CompanyName { get; set; } = null!;

        [Display(Name = "Şehir")]
        public string? City { get; set; }

        [Display(Name = "Telefon")]
        public string? Phone { get; set; }

    }
}
