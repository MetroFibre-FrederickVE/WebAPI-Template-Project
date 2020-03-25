using Template_WebAPI.Interfaces;
using Template_WebAPI.Models;

namespace Template_WebAPI.Repository
{
    public class TemplateRepository : BaseRepository<Templates>, ITemplateRepository
    {
        public TemplateRepository(IMongoContext context) : base(context)
        {

        }
    }
}
