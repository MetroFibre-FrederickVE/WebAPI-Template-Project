namespace Template_WebAPI.Models
{
    public class TemplateDbSettings : ITemplateDbSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
