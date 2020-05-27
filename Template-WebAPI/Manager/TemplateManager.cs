using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Template_WebAPI.Enums;
using Template_WebAPI.Events;
using Template_WebAPI.Model;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Manager
{
  public class TemplateManager : ITemplateManager
  {
    private readonly ITemplateRepository repository;
    enum CryptoAction
    {
      ActionEncrypt = 1,
      ActionDecrypt = 2
    }
    private readonly IEnumRepository enumRepository;
    private readonly ICloudFileManager cloudFileManager;    
    private readonly IEventSourceManager eventSourceManager;

    public TemplateManager(ITemplateRepository templateRepository, IEnumRepository enumRepository, ICloudFileManager cloudFileManager, IEventSourceManager eventSourceManager)
    {
      this.eventSourceManager = eventSourceManager;
      this.cloudFileManager = cloudFileManager;
      this.enumRepository = enumRepository;
      this.repository = templateRepository;
    }

    public async Task<Tuple<ErrorResponse, Template>> CreateAsync(Template template)
    {
      var returnedBoolValue = await repository.CheckIfNamesDuplicate(template);
      if (returnedBoolValue == true)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.1, $"The Template name \"{template.Name}\" is already in use."), null);
      }
      await cloudFileManager.UploadTemplateXMLFileAsync(template);
      await repository.AddAsync(template);
      await eventSourceManager.CreateTemplateEvent(template);
      return new Tuple<ErrorResponse, Template>(null, template);
    }

    public async Task<Tuple<ErrorResponse, Template>> CreateProjectAssociationAsync(string templateId, string projectId)
    {
      if (projectId == null || templateId == null)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.2, $"Neither the 'Template Id' nor the 'Project Id' can be null."), null);
      }
      else if (templateId.Length != 24)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.2, $"The Template Id string has to be in the BSON Id format."), null);
      }

      await repository.AddProjectByTemplateIdAsync(templateId, projectId);
      var updatedTemplate = await repository.GetByIdAsync(templateId);
      await eventSourceManager.UpdateTemplateEvent(updatedTemplate);
      return new Tuple<ErrorResponse, Template>(null, updatedTemplate);
    }

  public async Task<Tuple<ErrorResponse, Template>> DeleteByIdAsync(string templateId)
  {
    var template = await repository.GetByIdAsync(templateId);

    if (template == null)
    {
      return new Tuple<ErrorResponse, Template>(new ErrorResponse(404, $"Template not found"), null);
    }
    await eventSourceManager.RemoveTemplateEvent(template);
    await repository.RemoveAsync(template.Id);

    return new Tuple<ErrorResponse, Template>(null, template);
  }

  public async Task<Tuple<ErrorResponse, Template>> RemoveProjectAssociationAsync(string templateId, string projectId)
  {
    if (projectId == null || templateId == null)
    {
      return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.2, $"Neither the 'Template Id' nor the 'Project Id' can be null."), null);
    }
    else if (templateId.Length != 24)
    {
      return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.2, $"The Template Id string has to be in the BSON Id format."), null);
    }

    var template = await repository.GetByIdAsync(templateId);

    if (template == null)
    {
      return new Tuple<ErrorResponse, Template>(new ErrorResponse(404, $"Template not found"), null);
    }

    await repository.RemoveProjectFromTemplate(template.Id, projectId);
    template = await repository.GetByIdAsync(templateId);
    await eventSourceManager.UpdateTemplateEvent(template);

    return new Tuple<ErrorResponse, Template>(null, null);
  }

  public async Task<Tuple<ErrorResponse, IEnumerable<Template>>> GetAllAsync()
  {
    var templates = await repository.GetAllAsync();
    return new Tuple<ErrorResponse, IEnumerable<Template>>(null, templates);
  }

  public async Task<Tuple<ErrorResponse, Template>> GetUsingIdAsync(string templateId)
  {
    var template = await repository.GetByIdAsync(templateId);
    if (template == null)
    {
      return new Tuple<ErrorResponse, Template>(new ErrorResponse(404, $"Template not found"), null);
    }
    return new Tuple<ErrorResponse, Template>(null, template);
  }

  public async Task<Tuple<ErrorResponse, Template>> UpdateAsync(Template templateIn, string templateId)
  {
    var template = await repository.GetByIdAsync(templateId);

    if (template == null)
    {
      return new Tuple<ErrorResponse, Template>(new ErrorResponse(404, $"Template not found"), null);
    }

    var returnedBoolValue = await repository.CheckIfNamesDuplicate(templateIn);
    if (returnedBoolValue == true)
    {
      return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.1, $"The Template name \"{template.Name}\" is already in use."), null);
    }

    await repository.UpdateAsync(templateIn, templateId);

    return new Tuple<ErrorResponse, Template>(null, templateIn);
  }

  public Tuple<ErrorResponse, Template> ProcessTemplateFile(IFormFile file)
  {
    var folderName = Path.Combine("Resources", "File");
    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

    if (file.Length > 0)
    {
      var retVal = new Template();
      retVal.Id = repository.GenerateTemplateId();
      var fileName = $"{retVal.Id}";
      var fullPath = Path.Combine(pathToSave, fileName);
      var dbPath = Path.Combine(folderName, fileName);

      using (var stream = new FileStream(fullPath, FileMode.Create))
      {
        file.CopyTo(stream);
      }
      var defaultSensor = enumRepository.GetValues<Sensor>()[0];
      var legacyTemplate = ConvertXMLFileToXmlTemplate(fullPath);
      retVal.Version = legacyTemplate.GroupNames.TemplateGroups.TemplateGroupRow.Template.Version;
      retVal.TemplateInputMapping = new List<TemplateInputMapping>();
      retVal.Name = legacyTemplate.GroupNames.Title;
      foreach (var inputOutput in legacyTemplate.GroupNames.TemplateInputOutput.IO)
      {
        var fileTypeData = legacyTemplate.GroupNames.FileInfoTable.Row.Find(_ => _.FileTypeID == inputOutput.FileTypeID);
        if (fileTypeData == null)
        {
          return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.4, $"Unkown file type for input:{inputOutput.Title}."), null);
        }
        var fileType = new FileType
        {
          FileTypeId = int.Parse(fileTypeData.FileTypeID),
          DisplayName = fileTypeData.Title,
          DefaultFileName = fileTypeData.matchfilename,
          ExtensionName = fileTypeData.Extensions,
          IsHeader = bool.Parse(fileTypeData.IsHeader),
          IsImage = bool.Parse(fileTypeData.IsImage),
          IsRaw = bool.Parse(fileTypeData.IsRaw),
          IsXML = bool.Parse(fileTypeData.IsXML)
        };

        retVal.TemplateInputMapping.Add(new TemplateInputMapping
        {
          id = repository.GenerateTemplateId(),
          FieldName = inputOutput.FieldName,
          FileType = fileType,
          IsInput = inputOutput.Direction == "Input",
          moduleName = inputOutput.ModuleName,
          Sensor = defaultSensor
        });
      }
      return new Tuple<ErrorResponse, Template>(null, retVal);
    }
    else
    {
      return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.3, "The Template input upload should contain a file"), null);
    }
  }

  public async Task<Tuple<ErrorResponse, TemplateInputMapping>> CreateTemplateInputAsync(string templateId, TemplateInputMapping templateInputMapping)
  {
    await repository.CreateTemplateInputAsync(templateId, templateInputMapping);
    return new Tuple<ErrorResponse, TemplateInputMapping>(null, templateInputMapping);
  }
  #region Template Processing
  private Model.Legacy.TemplateObject ConvertXMLStringToXmlTemplate(string xmlString)
  {
    string dirtyChar = xmlString.Substring(100, 1);
    xmlString = xmlString.Replace(dirtyChar, "");

    //strip out module information. If Required: extract module (and master files directory), decrypt the BinHex using a different password and save to file(s). Crypto class does not support this at this time
    do
    {
      string endDoc = xmlString.Substring(xmlString.IndexOf("</MyData>") + "</MyData>".Length);
      xmlString = xmlString.Substring(0, xmlString.IndexOf("<MyData>")) + endDoc;

    } while (xmlString.IndexOf("</MyData>") > 0);

    //retrieve template Input/Output information. This is encrypted and converted to BinHex
    string templateInfo = xmlString.Substring(xmlString.IndexOf("<TIO>") + "<TIO>".Length);
    templateInfo = templateInfo.Substring(0, templateInfo.IndexOf("</TIO>"));

    string templateInfoDecrypt = DecryptString("fgh#$erthwr", templateInfo);

    //Change objectname "Table1" to "IO" for improved readability
    templateInfoDecrypt = templateInfoDecrypt.Replace("Table1", "IO");

    XmlDocument doc = new XmlDocument();
    doc.LoadXml(xmlString);

    string jsonText = JsonConvert.SerializeXmlNode(doc);

    doc = new XmlDocument();
    doc.LoadXml(templateInfoDecrypt);

    string jsonInputsOutputsText = JsonConvert.SerializeXmlNode(doc);

    Model.Legacy.TemplateObject templateJSON = JsonConvert.DeserializeObject<Model.Legacy.TemplateObject>(jsonText);
    Model.Legacy.TemplateIO templateIOJSON = JsonConvert.DeserializeObject<Model.Legacy.TemplateIO>(jsonInputsOutputsText);

    templateJSON.GroupNames.TemplateInputOutput = templateIOJSON.TemplateInputOutput;

    return templateJSON;
  }

  private Model.Legacy.TemplateObject ConvertXMLFileToXmlTemplate(string xmlPath)
  {
    string xmlString = null;
    using (StreamReader fs = new StreamReader(xmlPath))
    {
      xmlString = fs.ReadToEnd();
    }

    return ConvertXMLStringToXmlTemplate(xmlString);
  }

  private string DecryptString(string password, string EncryptedString)
  {
    byte[] bt = BinHextoByteArray(EncryptedString);
    return EncryptDecryptString(password, "", CryptoAction.ActionDecrypt, bt);
  }
  private string EncryptDecryptString(string Password, string InputString, CryptoAction direction, byte[] BT = null)
  {
    byte[] bytKey;
    byte[] bytIV;
    bytKey = CreateKey(Password);
    bytIV = CreateIV(Password);
    switch (direction)
    {
      case CryptoAction.ActionEncrypt:
        {
          return "";
        }

      case CryptoAction.ActionDecrypt:
        {
          return DecryptStringMain(BT, bytKey, bytIV, direction);
        }
    }
    return "";
  }
  private byte[] BinHextoByteArray(string BinHex)
  {
    int i;
    int iByte = BinHex.Length / 2;
    byte[] ByteVar = new byte[iByte];

    for (i = 0; i <= iByte - 1; i++)
      ByteVar[i] = Convert.ToByte(BinHex.Substring(i * 2, 2), 16);

    return ByteVar;
  }
  static int Asc(char c)
  {
    int converted = c;
    if (converted >= 0x80)
    {
      byte[] buffer = new byte[2];
      // if the resulting conversion is 1 byte in length, just use the value
      if (System.Text.Encoding.Default.GetBytes(new char[] { c }, 0, 1, buffer, 0) == 1)
      {
        converted = buffer[0];
      }
      else
      {
        // byte swap bytes 1 and 2;
        converted = buffer[0] << 16 | buffer[1];
      }
    }
    return converted;
  }
  private byte[] CreateKey(string strPassword)
  {
    // Convert strPassword to an array and store in chrData.
    char[] chrData = strPassword.ToCharArray();
    // Use intLength to get strPassword size.
    int intLength = chrData.GetUpperBound(0);
    // Declare bytDataToHash and make it the same size as chrData.
    byte[] bytDataToHash = new byte[intLength + 1];

    // Use For Next to convert and store chrData into bytDataToHash.
    for (int i = 0; i <= chrData.GetUpperBound(0); i++)
      bytDataToHash[i] = System.Convert.ToByte(Asc(chrData[i]));

    // Declare what hash to use.
    System.Security.Cryptography.SHA512Managed SHA512 = new System.Security.Cryptography.SHA512Managed();
    // Declare bytResult, Hash bytDataToHash and store it in bytResult.
    byte[] bytResult = SHA512.ComputeHash(bytDataToHash);
    // Declare bytKey(31).  It will hold 256 bits.
    byte[] bytKey = new byte[32];

    // Use For Next to put a specific size (256 bits) of 
    // bytResult into bytKey. The 0 To 31 will put the first 256 bits
    // of 512 bits into bytKey.
    for (int i = 0; i <= 31; i++)
    {
      bytKey[i] = bytResult[i];
    }

    return bytKey; // Return the key.
  }
  private byte[] CreateIV(string strPassword)
  {
    // Convert strPassword to an array and store in chrData.
    char[] chrData = strPassword.ToCharArray();
    // Use intLength to get strPassword size.
    int intLength = chrData.GetUpperBound(0);
    // Declare bytDataToHash and make it the same size as chrData.
    byte[] bytDataToHash = new byte[intLength + 1];

    // Use For Next to convert and store chrData into bytDataToHash.
    for (int i = 0; i <= chrData.GetUpperBound(0); i++)
    {
      bytDataToHash[i] = System.Convert.ToByte(Asc(chrData[i]));
    }

    // Declare what hash to use.
    System.Security.Cryptography.SHA512Managed SHA512 = new System.Security.Cryptography.SHA512Managed();
    // Declare bytResult, Hash bytDataToHash and store it in bytResult.
    byte[] bytResult = SHA512.ComputeHash(bytDataToHash);
    // Declare bytIV(15).  It will hold 128 bits.
    byte[] bytIV = new byte[16];

    // Use For Next to put a specific size (128 bits) of bytResult into bytIV.
    // The 0 To 30 for bytKey used the first 256 bits of the hashed password.
    // The 32 To 47 will put the next 128 bits into bytIV.
    for (int i = 32; i <= 47; i++)
    {
      bytIV[i - 32] = bytResult[i];
    }

    return bytIV; // Return the IV.
  }
  private string DecryptStringMain(byte[] InputString, byte[] bytKey, byte[] bytIV, CryptoAction Direction)
  {
    byte[] inputInBytes = InputString;
    byte[] result = null;
    using (MemoryStream encryptedStream = new MemoryStream())
    {
      CryptoStream csCryptoStream = null;
      System.Security.Cryptography.RijndaelManaged cspRijndael = new System.Security.Cryptography.RijndaelManaged();

      cspRijndael.Padding = PaddingMode.ISO10126;

      switch (Direction)
      {
        case CryptoAction.ActionEncrypt:
          {
            csCryptoStream = new CryptoStream(encryptedStream, cspRijndael.CreateEncryptor(bytKey, bytIV), CryptoStreamMode.Write);
            break;
          }

        case CryptoAction.ActionDecrypt:
          {
            csCryptoStream = new CryptoStream(encryptedStream, cspRijndael.CreateDecryptor(bytKey, bytIV), CryptoStreamMode.Write);
            break;
          }
      }

      csCryptoStream.Write(inputInBytes, 0, inputInBytes.Length);

      csCryptoStream.FlushFinalBlock();
      encryptedStream.Position = 0;

      result = new byte[encryptedStream.Length - 1 + 1];
      encryptedStream.Read(result, 0, (int)encryptedStream.Length);
    }
    return new UTF8Encoding().GetString(result);
  }

  #endregion


}
}