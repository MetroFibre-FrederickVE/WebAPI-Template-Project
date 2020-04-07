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

        public async Task<Boolean> CheckIfNamesDuplicate(Template template)
        {
            _dbCollection = _mongoContext.GetCollection<Template>(typeof(Template).Name);

            var filterById = Builders<Template>.Filter.Eq("_id", new ObjectId(template.Id));

            var filterByIdAndName = Builders<Template>.Filter.Eq("_id", new ObjectId(template.Id))
                & Builders<Template>.Filter.Eq("Name", template.Name);
            try
            {
                var returnedTemplateSearchResult = await _dbCollection.FindAsync(filterByIdAndName).Result.FirstOrDefaultAsync();
                
                if (returnedTemplateSearchResult != null)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException();
            }

            var returnedTemplateSearch = await _dbCollection.FindAsync(filterById).Result.FirstOrDefaultAsync();

            if (returnedTemplateSearch.Id == template.Id)
            {
                var filterByName = Builders<Template>.Filter.Eq("Name", template.Name);
                var returnTemplateIfFound = await _dbCollection.FindAsync(filterByName).Result.FirstOrDefaultAsync();

                if (returnTemplateIfFound != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
