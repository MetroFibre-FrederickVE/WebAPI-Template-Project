using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Template_WebAPI.Authentication
{
  public class ClaimsRequirment : IAuthorizationRequirement
  {
    private readonly JsonSerializerSettings serializerSettings;

    public ClaimsRequirment(string role)
    {
      MatchingRole = role;

      serializerSettings = new JsonSerializerSettings
      {
        ContractResolver = new DefaultContractResolver
        {
          NamingStrategy = new CamelCaseNamingStrategy()
        },
        Formatting = Formatting.Indented
      };
    }

    public string MatchingRole { get; private set; }
  }
}
