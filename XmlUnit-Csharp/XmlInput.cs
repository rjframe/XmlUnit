using System;
using System.IO;
using System.Xml;

namespace XmlUnit
{

  public class XmlInput
  {
    private delegate XmlReader XmlInputTranslator(Object originalInput, String baseURI);
    private readonly string _baseURI;
    private readonly object _originalInput;
    private readonly XmlInputTranslator _translateInput;
    private static readonly string CURRENT_FOLDER = ""; //".";

    private XmlInput(Object someXml, String baseURI, XmlInputTranslator translator)
    {
      _baseURI = baseURI;
      _originalInput = someXml;
      _translateInput = translator;
    }

    public XmlInput(String someXml, String baseURI)
      : this(someXml, baseURI, new XmlInputTranslator(TranslateString))
    {
    }

    public XmlInput(String someXml)
      : this(someXml, CURRENT_FOLDER)
    {
    }

    private static XmlReader TranslateString(Object originalInput, String baseURI)
    {
      return new XmlTextReader(baseURI, new StringReader((string)originalInput));
    }

    public XmlInput(Stream someXml, String baseURI)
      : this(someXml, baseURI, new XmlInputTranslator(TranslateStream))
    {
    }

    public XmlInput(Stream someXml)
      : this(someXml, CURRENT_FOLDER)
    {
    }

    private static XmlReader TranslateStream(Object originalInput, String baseURI)
    {
      return new XmlTextReader(baseURI, new StreamReader((Stream)originalInput));
    }

    public XmlInput(TextReader someXml, String baseURI)
      : this(someXml, baseURI, new XmlInputTranslator(TranslateReader))
    {
    }

    public XmlInput(TextReader someXml)
      : this(someXml, CURRENT_FOLDER)
    {
    }

    private static XmlReader TranslateReader(Object originalInput, String baseURI)
    {
      return new XmlTextReader(baseURI, (TextReader)originalInput);
    }

    public XmlInput(XmlReader someXml)
      : this(someXml, null, new XmlInputTranslator(NullTranslator))
    {
    }

    private static XmlReader NullTranslator(Object originalInput, String baseURI)
    {
      return (XmlReader)originalInput;
    }

    public XmlReader CreateXmlReader()
    {
      return _translateInput(_originalInput, _baseURI);
    }

    public override bool Equals(object other)
    {
      if (other != null && other is XmlInput)
      {
        return _originalInput.Equals(((XmlInput)other)._originalInput);
      }
      return false;
    }

    public override int GetHashCode()
    {
      return _originalInput.GetHashCode();
    }
  }
}
