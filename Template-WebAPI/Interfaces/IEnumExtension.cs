using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Enums;

namespace Template_WebAPI.Interfaces
{
  public interface IEnumExtension
  {
    List<EnumValue> GetValuesAsync<T>();
  }
}
