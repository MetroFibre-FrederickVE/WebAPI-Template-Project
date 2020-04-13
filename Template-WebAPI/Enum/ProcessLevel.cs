using System;
using System.ComponentModel;

namespace Template_WebAPI.Enums
{
  public enum ProcessLevel
  {
    [Description("Raw")]
    Raw = 0,

    [Description("Data Correction")]
    DataCorrection = 1,

    [Description("Data Preparation")]
    DataPreparation = 2,

    [Description("Spectral Processing")]
    SpectralProcessing = 3,

    [Description("Spectral Interpretation")]
    SpectralInterpretation = 4,

    [Description("Product Generation")]
    ProductGeneration = 5,

    [Description("Warehousing")]
    Warehousing = 6,

    [Description("Data Query")]
    DataQuery = 7
  }
}
