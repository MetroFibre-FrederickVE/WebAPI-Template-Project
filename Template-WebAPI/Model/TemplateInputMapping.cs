using Template_WebAPI.Enums;

namespace Template_WebAPI.Model
{
    public class TemplateInputMapping
    {
        public string _id { get; set; }
        public bool IsInput { get; set; }
        public string moduleName { get; set; }
        public string FieldName { get; set; }        
        public FileType FileType { get; set; }
        public Sensor Sensor { get; set; }
    }
}