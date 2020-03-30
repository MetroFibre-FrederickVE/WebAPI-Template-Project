using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Enums;

namespace Template_WebAPI.Interfaces
{
    public interface IEnumExtensions
    {
        Task<List<EnumValue>> GetValuesAsync<T>();
    }
}
