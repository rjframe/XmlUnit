using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace XmlUnit
{

  public class Validator
  {
    private bool hasValidated = false;
    private bool isValid = true;
    private string validationMessage;
    private readonly XmlReader validatingReader;

    private Validator(XmlReader xmlInputReader)
    {
      var settings = new XmlReaderSettings();

      settings.ValidationType = ValidationType.Schema;
      settings.ConformanceLevel = ConformanceLevel.Auto;
      settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
      settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
      settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
      settings.ValidationEventHandler += new ValidationEventHandler(ValidationFailed);
      //settings.Schemas.Add("http://www.publishing.org", new XmlTextReader(@"C:\Users\rjframe\Documents\Visual Studio 2012\Projects\XmlUnit\Tests\etc\Book.xsd"));
      
      validatingReader = XmlReader.Create(xmlInputReader, settings);
      
    }

    public Validator(XmlInput input) : this(input.CreateXmlReader())
    {
    }

    private void ValidationFailed(object sender, ValidationEventArgs e)
    {
      isValid = false;
      validationMessage += String.Format("{0} Line {1}.{2}", e.Message, e.Exception.LineNumber, Environment.NewLine);
    }

    private void Validate()
    {
      if (!hasValidated)
      {
        hasValidated = true;
        while (validatingReader.Read())
        {
          // only interested in ValidationFailed callbacks
        }
        validatingReader.Close();
      }
    }

    public Boolean IsValid
    {
      get
      {
        Validate();
        return isValid;
      }
    }

    public String ValidationMessage
    {
      get
      {
        Validate();
        return validationMessage;
      }
    }
  }
}
