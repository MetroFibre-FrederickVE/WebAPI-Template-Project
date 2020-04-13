using System;
using System.ComponentModel;

namespace Template_WebAPI.Enums
{
  public enum Sensor
  {
    [Description("Not Available")]
    NotAvailable = 0,

    [Description("RGB")]
    Rgb = 1,

    [Description("Swir")]
    Swir = 2,

    [Description("Vnir")]
    Vnir = 3,

    [Description("Vn_Swir")]
    Vn_Swir = 4,

    [Description("Mwir")]
    Mwir = 5,

    [Description("Lwir_Hs")]
    Lwir_Hs = 6,

    [Description("Lwir_Owl")]
    Lwir_Owl = 7,

    [Description("Combined")]
    Combined = 8,

    [Description("Ranged")]
    Ranged = 9
  }
}
