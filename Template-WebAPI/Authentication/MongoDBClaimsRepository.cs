using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Authentication
{
  public class MongoDBClaimsRepository : BaseRepository<SecurityClaims>, IClaimsRepository
  {
    public MongoDBClaimsRepository(IMongoContext context) : base(context)
    {

    }

    public async Task<IEnumerable<SecurityClaims>> CreateTemplateEvent(SecurityClaims securityClaims)
    {
      var all = _dbCollection.Find(Builders<SecurityClaims>.Filter.Empty);
      var allList = all.ToListAsync();
      return await allList;
    }
  }
}
