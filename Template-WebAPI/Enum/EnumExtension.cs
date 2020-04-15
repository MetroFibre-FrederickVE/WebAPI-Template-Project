using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Template_WebAPI.Enums
{
  public class EnumExtension : IEnumExtension
  {
    public List<EnumValue> GetValues<T>()
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
