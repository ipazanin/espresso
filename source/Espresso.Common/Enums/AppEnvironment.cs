using System.ComponentModel.DataAnnotations;

namespace Espresso.Common.Enums
{
    public enum AppEnvironment
    {
        [Display(Name = "Undefined")]
        Undefined = 0,
        [Display(Name = "Local")]
        Local = 1,
        [Display(Name = "Development")]
        Dev = 2,
        [Display(Name = "Production")]
        Prod = 3
    }
}
