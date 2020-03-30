using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Interfaces;

namespace Template_WebAPI.Enums
{
    public class EnumExtensions : IEnumExtensions
    {
        public async Task<List<EnumValue>> GetValuesAsync<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                values.Add(new EnumValue()
                {
                    Name = Enum.GetName(typeof(T), itemType),
                    Value = (int)itemType
                });
            }
            return values;
        }
    }
}
