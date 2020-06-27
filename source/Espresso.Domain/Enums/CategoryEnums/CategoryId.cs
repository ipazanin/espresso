using System.ComponentModel.DataAnnotations;

namespace Espresso.Domain.Enums.CategoryEnums
{
    /// <summary>
    /// In Application Categories
    /// </summary>
    public enum CategoryId
    {
        [Display(Name = "Undefined")]
        Undefined = 0,
        [Display(Name = "Vijesti")]
        Vijesti = 1,
        [Display(Name = "Sport")]
        Sport = 2,
        [Display(Name = "Show")]
        Show = 3,
        [Display(Name = "Lifestyle")]
        Lifestyle = 4,
        [Display(Name = "Tech")]
        Tech = 5,
        [Display(Name = "Viral")]
        Viral = 6,
        [Display(Name = "Biznis")]
        Biznis = 7,
        [Display(Name = "Auto/Moto")]
        AutoMoto = 8,
        [Display(Name = "Kultura")]
        Kultura = 9,
        [Display(Name = "Koronavirus")]
        Koronavirus = 10
    }
}
