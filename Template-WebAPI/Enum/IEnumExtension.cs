using System.Collections.Generic;

namespace Template_WebAPI.Enums
{
  public interface IEnumExtension
  {
    List<EnumValue> GetValues<T>();
  }
}
