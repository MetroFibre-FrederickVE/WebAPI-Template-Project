using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Template_WebAPI.Authentication
{
  [Route("templatemanagement/v1/[controller]")]
  [ApiController]
  public class ClaimsController : ControllerBase
  {
    private readonly IClaimsRepository _claimsRepository;

    public ClaimsController(IClaimsRepository claimsRepository)
    {
      _claimsRepository = claimsRepository;
    }

    [HttpGet("{id:length(24)}", Name = "Get")]
    public async Task<List<GroupsRole>> GetAllClaims(string id)
    {
      var testable = await _claimsRepository.GetSecurityClaimsAsync(id);

      return testable;
    }
  }
}