using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task AddProjectByTemplateIdAsync(Template templateObj, string templateId)
        {
            if (templateObj == null || templateId == null)
            {
                throw new ArgumentNullException(typeof(Template).Name + " object is null");
            }
            else if (templateId.Length != 24)
            {
                throw new ArgumentOutOfRangeException(typeof(Template).Name + " string has to be 24 characters.");
            }

            await _dbCollection.ReplaceOneAsync(Builders<Template>.Filter.Eq("_id", new ObjectId(templateId)), templateObj);
        }

        public async Task RemoveProjectFromTemplate(string templateId, string projectIdInput)
        {

            var idFilter = Builders<Template>.Filter.Eq("_id", new ObjectId(templateId));
            var updateBuilder = Builders<Template>.Update.Pull("ProjectId", projectIdInput);
            
            await _dbCollection.UpdateOneAsync(idFilter, updateBuilder);

            // ######################################################################################################################################

            // Loop method to "Set" value, however the array remains unchanged outside of scope.
            //var templates = await _dbCollection.Find(idFilter).ToListAsync();

            //foreach (var template in templates)
            //{
            //    foreach (var projectIdArrayValue in template.ProjectId)
            //    {
            //        if (projectIdArrayValue == projectIdInput)
            //        {
            //            var indexOfItem = template.ProjectId.ToList().IndexOf(projectIdArrayValue);
            //            template.ProjectId.SetValue("", indexOfItem); // Replaces string with empty but doesnt remove array entry
            //            //template.ProjectId.RemoveAt(indexOfItem); // For if field type is a list
            //        }
            //    }

            //}
        }
    }
}
