namespace Template_WebAPI.Models
{
  public interface ITemplateDatabaseSettings
  {
    public string CollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }
}
