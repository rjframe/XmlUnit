using System;
using System.IO;
using System.Text;
using System.Xml.XPath;

namespace XmlUnit
{

  public class XPath
  {
    private readonly string _xPathExpression;

    public XPath(String anXPathExpression)
    {
      _xPathExpression = anXPathExpression;
    }

    public Boolean XPathExists(String forSomeXml)
    {
      return XPathExists(new XmlInput(forSomeXml));
    }

    public Boolean XPathExists(XmlInput forInput)
    {
      XPathNodeIterator iterator = GetNodeIterator(forInput);
      return (iterator.Count > 0);
    }

    private XPathNodeIterator GetNodeIterator(XmlInput forXmlInput)
    {
      XPathNavigator xpathNavigator = GetNavigator(forXmlInput);
      return xpathNavigator.Select(_xPathExpression);
    }

    private XPathNavigator GetNavigator(XmlInput forXmlInput)
    {
      var xpathDocument = new XPathDocument(forXmlInput.CreateXmlReader());
      return xpathDocument.CreateNavigator();
    }

    public String EvaluateXPath(String forSomeXml)
    {
      return EvaluateXPath(new XmlInput(forSomeXml));
    }

    public String EvaluateXPath(XmlInput forXmlInput)
    {
      XPathNavigator xPathNavigator = GetNavigator(forXmlInput);
      XPathExpression xPathExpression = xPathNavigator.Compile(_xPathExpression);
      if (xPathExpression.ReturnType == XPathResultType.NodeSet)
      {
        return EvaluateXPath(xPathNavigator);
      }
      else
      {
        return xPathNavigator.Evaluate(xPathExpression).ToString();
      }
    }

    private String EvaluateXPath(XPathNavigator forXPathNavigator)
    {
      XPathNodeIterator iterator = forXPathNavigator.Select(_xPathExpression);

      var stringBuilder = new StringBuilder();
      XPathNavigator xpathNavigator;

      while (iterator.MoveNext())
      {
        xpathNavigator = iterator.Current;
        stringBuilder.Insert(stringBuilder.Length, xpathNavigator.Value);
      }
      return stringBuilder.ToString();
    }
  }
}
