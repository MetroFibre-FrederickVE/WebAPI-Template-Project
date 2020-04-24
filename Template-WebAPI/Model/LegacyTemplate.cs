using System.Collections.Generic;

namespace Template_WebAPI.Model.Legacy
{
  public class TemplateObject
  {
    public Xml xml { get; set; }
    public Groupnames GroupNames { get; set; }
  }

  public class Xml
  {
    public string version { get; set; }
    public string encoding { get; set; }
    public string standalone { get; set; }
  }

  public class Groupnames
  {
    public string GroupnameID { get; set; }
    public string Title { get; set; }
    public Templategroups TemplateGroups { get; set; }
    public Fileinfotable FileInfoTable { get; set; }
    public Masterfilecontents MasterfileContents { get; set; }
    public Templateinputoutput TemplateInputOutput { get; set; }
  }

  public class Templategroups
  {
    public Templategrouprow TemplateGroupRow { get; set; }
  }

  public class Templategrouprow
  {
    public string Count { get; set; }
    public string TemplateGroupID { get; set; }
    public string GroupNameID { get; set; }
    public string TemplateID { get; set; }
    public string HideModules { get; set; }
    public string GroupLocX { get; set; }
    public string GroupLocY { get; set; }
    public string TemplateRank { get; set; }
    public Template Template { get; set; }
    public Templatemodules TemplateModules { get; set; }
  }

  public class Template
  {
    public string TemplateID { get; set; }
    public string Title { get; set; }
    public string OwnerID { get; set; }
    public string DateAdded { get; set; }
    public string DateModified { get; set; }
    public string AllowUpload { get; set; }
    public string AllowModuleReuse { get; set; }
    public string Version { get; set; }
    public object Summary { get; set; }
    public string TemplateParentID { get; set; }
    public string Latest { get; set; }
    public Owner Owner { get; set; }
  }

  public class Owner
  {
    public string OwnerID { get; set; }
    public string Name { get; set; }
    public object Surname { get; set; }
    public object Contact { get; set; }
    public object Email { get; set; }
    public object Company { get; set; }
  }

  public class Templatemodules
  {
    public Templatemodulesrow[] TemplateModulesRow { get; set; }
  }

  public class Templatemodulesrow
  {
    public string Count { get; set; }
    public string TempModID { get; set; }
    public string TemplateGroupID { get; set; }
    public string ModuleID { get; set; }
    public string locx { get; set; }
    public string locy { get; set; }
    public string ModuleCount { get; set; }
    public string ModulePriority { get; set; }
    public string GroupnameID { get; set; }
    public Paths Paths { get; set; }
    public Modules Modules { get; set; }
  }

  public class Paths
  {
    public Pathrow[] PathRow { get; set; }
  }

  public class Pathrow
  {
    public string Count { get; set; }
    public string PathID { get; set; }
    public string ParentPathID { get; set; }
    public string TemplateGroupID { get; set; }
    public string ModuleInfoID { get; set; }
    public string PathRank { get; set; }
    public string PathValue { get; set; }
    public string TempModID { get; set; }
    public string FinalFile { get; set; }
    public string UserEdit { get; set; }
    public string GroupnameID { get; set; }
    public object FilesUsed { get; set; }
  }

  public class Modules
  {
    public string ModuleID { get; set; }
    public string ModuleParentID { get; set; }
    public string Title { get; set; }
    public string Version { get; set; }
    public string EXEPath { get; set; }
    public string ErrorPath { get; set; }
    public string ProgressPath { get; set; }
    public string IsProtected { get; set; }
    public string WasDownloaded { get; set; }
    public string DateLoaded { get; set; }
    public string Latest { get; set; }
    public Moduleinfo ModuleInfo { get; set; }
    public Datacontents DataContents { get; set; }
  }

  public class Moduleinfo
  {
    public Row[] Row { get; set; }
  }

  public class Row
  {
    public string Count { get; set; }
    public string ModuleInfoID { get; set; }
    public string ModuleID { get; set; }
    public string Title { get; set; }
    public string MoreInformation { get; set; }
    public string DefaultValue { get; set; }
    public string Extension { get; set; }
    public TemplateModuleParameterType TypeID { get; set; }
    public string Filetypes { get; set; }
  }

  public class Datacontents
  {
    public string ExtractToPath { get; set; }
  }

  public class Fileinfotable
  {
    // public Row1[] Row { get; set; }
    public List<Row1> Row { get; set; }
  }

  public class Row1
  {
    public string Count { get; set; }
    public string FileTypeID { get; set; }
    public string Title { get; set; }
    public string IsImage { get; set; }
    public string IsRaw { get; set; }
    public string IsXML { get; set; }
    public string IsHeader { get; set; }
    public string CanEdit { get; set; }
    public string matchfilename { get; set; }
    public string Extensions { get; set; }
  }
  public class Masterfilecontents
  {
    public string ExtractToPath { get; set; }
  }
  public class TemplateIO
  {
    public Xml xml { get; set; }
    public Templateinputoutput TemplateInputOutput { get; set; }
  }
  public class Templateinputoutput
  {
    public IO[] IO { get; set; }
  }

  public class IO
  {
    public string GroupnameID { get; set; }
    public string Title { get; set; }
    public string ModuleName { get; set; }
    public string FieldName { get; set; }
    public string FinalFile { get; set; }
    public string Direction { get; set; }
    public string FileTypeID { get; set; }
  }
}