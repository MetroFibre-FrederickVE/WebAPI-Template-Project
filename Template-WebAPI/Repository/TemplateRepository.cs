using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Template_WebAPI.Interfaces;
using Template_WebAPI.Model;

namespace Template_WebAPI.Repository
{
    public class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(IMongoContext context) : base(context)
        {

        }

        public async Task AddProjectByTemplateIdAsync(string templateId, string projectId)
        {
            var idFilter = Builders<Template>.Filter.Eq("_id", new ObjectId(templateId));
            var updateBuilder = Builders<Template>.Update.AddToSet("ProjectId", projectId);

            await _dbCollection.UpdateOneAsync(idFilter, updateBuilder);
        }

        public async Task RemoveProjectFromTemplate(string templateId, string projectId)
        {
            var idFilter = Builders<Template>.Filter.Eq("_id", new ObjectId(templateId));
            var updateBuilder = Builders<Template>.Update.Pull("ProjectId", projectId);
            
            await _dbCollection.UpdateOneAsync(idFilter, updateBuilder);
        }
    }
}
