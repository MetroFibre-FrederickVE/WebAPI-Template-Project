using Template_WebAPI.Interfaces;
using Template_WebAPI.Models;

namespace Template_WebAPI.Repository
{
    public class EnumRepository : BaseRepository<Templates>, IEnumRepository
    {
        public EnumRepository(IMongoContext context) : base(context)
        {

        }
    }
}
