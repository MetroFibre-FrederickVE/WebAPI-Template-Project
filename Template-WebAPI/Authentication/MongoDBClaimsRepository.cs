using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Authentication
{
  public class MongoDBClaimsRepository : BaseRepository<SecurityClaims>
  {
    public MongoDBClaimsRepository(IMongoContext context) : base(context)
    {

    }
  }
}
