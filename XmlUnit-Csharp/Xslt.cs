using System;
using System.IO;
using System.Security.Policy;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XmlUnit
{

  public class Xslt
  {
    private readonly XmlInput _xsltInput;
    private readonly XmlResolver _xsltResolver;
    private readonly Evidence _evidence;

    public Xslt(XmlInput xsltInput, XmlResolver xsltResolver, Evidence evidence)
    {
      _xsltInput = xsltInput;
      _xsltResolver = xsltResolver;
      _evidence = evidence;
    }

    public Xslt(XmlInput xsltInput)
      : this(xsltInput, null, null)
    {
    }

    public Xslt(String xslt, String baseURI)
      : this(new XmlInput(xslt, baseURI))
    {
    }

    public Xslt(String xslt)
      : this(new XmlInput(xslt))
    {
    }

    public XmlOutput Transform(String someXml)
    {
      return Transform(new XmlInput(someXml));
    }

    public XmlOutput Transform(XmlInput someXml)
    {
      return Transform(someXml, null);
    }

    public XmlOutput Transform(XmlInput someXml, XsltArgumentList xsltArgs)
    {
      return Transform(someXml.CreateXmlReader(), null, xsltArgs);
    }

    public XmlOutput Transform(XmlReader xmlTransformed, XmlResolver resolverForXmlTransformed, XsltArgumentList xsltArgs)
    {
      var transform = new XslCompiledTransform();
      var xsltReader = _xsltInput.CreateXmlReader();

      var settings = new XsltSettings();
      
      transform.Load(xsltReader, settings, _xsltResolver);
      
      XmlSpace space = XmlSpace.Default;
      XPathDocument document = new XPathDocument(xmlTransformed, space);
      XPathNavigator navigator = document.CreateNavigator();

      return new XmlOutput(transform, xsltArgs, navigator, resolverForXmlTransformed,
          new XmlReader[] { xmlTransformed, xsltReader });
    }
  }
}
