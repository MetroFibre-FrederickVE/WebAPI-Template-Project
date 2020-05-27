using System.ComponentModel;

namespace Template_WebAPI.Enums
{
  public enum EventType
  {
    [Description("Create")]
    Create = 1,
    [Description("Update")]
    Update = 2,
    [Description("Remove")]
    Remove = 3
  }
}