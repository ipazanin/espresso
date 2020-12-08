using System.ComponentModel.DataAnnotations;

namespace Espresso.Common.Enums
{
    public enum AppEnvironment
    {
        Undefined = 0,
        Local = 1,
        [Display(Name = "Development")]
        Dev = 2,
        [Display(Name = "Production")]
        Prod = 3
    }
}
