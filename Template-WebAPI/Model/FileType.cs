using System.Collections.Generic;

namespace Template_WebAPI.Model
{
  public class FileType
  {
    public string _id { get; set; }
    public string DefaultDescription { get; set; }
    public string DefaultFileName { get; set; }
    public List<string> AlternateFileName { get; set; }
    public string DisplayName { get; set; }
    public string Editable { get; set; }
    public string ExtensionName { get; set; }
    public int FileTypeId { get; set; }
    public bool IsHeader { get; set; }
    public bool IsImage { get; set; }
    public bool IsRaw { get; set; }
    public bool IsXML { get; set; }
    public bool __Deleted { get; set; }
  }
}