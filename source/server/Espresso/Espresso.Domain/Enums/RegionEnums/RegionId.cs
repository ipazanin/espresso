// RegionId.cs
//
// © 2021 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Domain.Enums.RegionEnums
{
    public enum RegionId
    {
        Undefined = 0,
        Global = 1,
        [Display(Name = "Dalmacija")]
        Dalmacija = 2,
        [Display(Name = "Istra & Kvarner")]
        Istra = 3,
        [Display(Name = "Lika")]
        Lika = 4,
        [Display(Name = "Zagreb i okolica")]
        Zagreb = 5,
        [Display(Name = "Sjeverna Hrvatska")]
        SjevernaHrvatska = 6,
        [Display(Name = "Slavonija & Baranja")]
        Slavonija = 7,
    }
}
