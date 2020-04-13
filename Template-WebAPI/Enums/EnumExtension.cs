using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Template_WebAPI.Interfaces;

namespace Template_WebAPI.Enums
{
  public class EnumExtension : IEnumExtension
  {
    public List<EnumValue> GetValuesAsync<T>()
    {
      var values = new List<EnumValue>();

      foreach (var itemType in Enum.GetValues(typeof(T)))
      {
        values.Add(new EnumValue()
        {
          Name = Enum.GetName(typeof(T), itemType),
          Value = (int)itemType,
          Description = itemType.GetType()
                                  .GetMember(itemType.ToString())
                                  .First()
                                  .GetCustomAttribute<DescriptionAttribute>()?
                                  .Description ?? string.Empty
        });
      }
      return values;
    }
  }
}
