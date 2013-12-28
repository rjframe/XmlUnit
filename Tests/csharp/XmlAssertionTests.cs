using System.IO;

using NUnit.Framework;
using XmlUnit;

namespace XmlUnit.Tests
{

  [TestFixture]
  public class XmlAssertionTests
  {

    [Test]
    public void AssertStringEqualAndIdenticalToSelf()
    {
      string control = "<assert>true</assert>";
      string test = "<assert>true</assert>";
      AssertXml.AreIdentical(control, test);
      AssertXml.AreEqual(control, test);
    }

    [Test]
    public void AssertDifferentStringsNotEqualNorIdentical()
    {
      string control = "<assert>true</assert>";
      string test = "<assert>false</assert>";
      XmlDiff xmlDiff = new XmlDiff(control, test);
      AssertXml.AreNotIdentical(xmlDiff);
      AssertXml.AreNotEqual(xmlDiff);
    }

    [Test]
    public void AssertXmlIdenticalUsesOptionalDescription()
    {
      string description = "An Optional Description";
      bool caughtException = true;
      try
      {
        XmlDiff diff = new XmlDiff(new XmlInput("<a/>"), new XmlInput("<b/>"),
                                   new DiffConfiguration(description));
        AssertXml.AreIdentical(diff);
        caughtException = false;
      }
      catch (NUnit.Framework.AssertionException e)
      {
        Assert.IsTrue(e.Message.IndexOf(description) > -1);
      }
      Assert.IsTrue(caughtException);
    }

    [Test]
    public void AssertXmlEqualsUsesOptionalDescription()
    {
      string description = "Another Optional Description";
      bool caughtException = true;
      try
      {
        XmlDiff diff = new XmlDiff(new XmlInput("<a/>"), new XmlInput("<b/>"),
                                   new DiffConfiguration(description));
        AssertXml.AreEqual(diff);
        caughtException = false;
      }
      catch (NUnit.Framework.AssertionException e)
      {
        Assert.IsTrue(e.Message.IndexOf(description) > -1);
      }
      Assert.IsTrue(caughtException);
    }

    [Test]
    public void AssertXmlValidTrueForValidFile()
    {
      StreamReader reader = GetStreamReader(ValidatorTests.VALID_FILE);
      try
      {
        AssertXml.IsValidXml(reader);
      }
      finally
      {
        reader.Close();
      }
    }

    [Test]
    public void AssertXmlValidFalseForInvalidFile()
    {
      StreamReader reader = GetStreamReader(ValidatorTests.INVALID_FILE);
      bool caughtException = true;
      try
      {
        AssertXml.IsValidXml(reader);
        caughtException = false;
      }
      catch (AssertionException e)
      {
        AvoidUnusedVariableCompilerWarning(e);
      }
      finally
      {
        reader.Close();
      }
      Assert.IsTrue(caughtException);
    }

    private StreamReader GetStreamReader(string file)
    {
      FileStream input = File.Open(file, FileMode.Open, FileAccess.Read);
      return new StreamReader(input);
    }

    private static readonly string MY_SOLAR_SYSTEM = "<solar-system><planet name='Earth' position='3' supportsLife='yes'/><planet name='Venus' position='4'/></solar-system>";

    [Test]
    public void AssertXPathExistsWorksForExistentXPath()
    {
      AssertXml.XPathExists("//planet[@name='Earth']", MY_SOLAR_SYSTEM);
    }

    [Test]
    public void AssertXPathExistsFailsForNonExistentXPath()
    {
      bool caughtException = true;
      try
      {
        AssertXml.XPathExists("//star[@name='alpha centauri']", MY_SOLAR_SYSTEM);
        caughtException = false;
      }
      catch (AssertionException e)
      {
        AvoidUnusedVariableCompilerWarning(e);
      }
      Assert.IsTrue(caughtException);
    }

    [Test]
    public void AssertXPathEvaluatesToWorksForMatchingExpression()
    {
      AssertXml.XPathEvaluatesTo("//planet[@position='3']/@supportsLife", MY_SOLAR_SYSTEM, "yes");
    }

    [Test]
    public void AssertXPathEvaluatesToWorksForNonMatchingExpression()
    {
      AssertXml.XPathEvaluatesTo("//planet[@position='4']/@supportsLife", MY_SOLAR_SYSTEM, "");
    }

    [Test]
    public void AssertXPathEvaluatesToWorksConstantExpression()
    {
      AssertXml.XPathEvaluatesTo("true()", MY_SOLAR_SYSTEM, "True");
      AssertXml.XPathEvaluatesTo("false()", MY_SOLAR_SYSTEM, "False");
    }

    [Test]
    public void AssertXslTransformResultsWorksWithStrings()
    {
      string xslt = XsltTests.IDENTITY_TRANSFORM;
      string someXml = "<a><b>c</b><b/></a>";
      AssertXml.XslTransformResults(xslt, someXml, someXml);
    }

    [Test]
    public void AssertXslTransformResultsWorksWithXmlInput()
    {
      StreamReader xsl = GetStreamReader(".\\..\\..\\etc\\animal.xsl");
      XmlInput xslt = new XmlInput(xsl);
      StreamReader xml = GetStreamReader(".\\..\\..\\etc\\testAnimal.xml");
      XmlInput xmlToTransform = new XmlInput(xml);
      XmlInput expectedXml = new XmlInput("<dog/>");
      AssertXml.XslTransformResults(xslt, xmlToTransform, expectedXml);
    }

    [Test]
    public void AssertXslTransformResultsCatchesFalsePositive()
    {
      StreamReader xsl = GetStreamReader(".\\..\\..\\etc\\animal.xsl");
      XmlInput xslt = new XmlInput(xsl);
      StreamReader xml = GetStreamReader(".\\..\\..\\etc\\testAnimal.xml");
      XmlInput xmlToTransform = new XmlInput(xml);
      XmlInput expectedXml = new XmlInput("<cat/>");
      bool caughtException = true;
      try
      {
        AssertXml.XslTransformResults(xslt, xmlToTransform, expectedXml);
        caughtException = false;
      }
      catch (AssertionException e)
      {
        AvoidUnusedVariableCompilerWarning(e);
      }
      Assert.IsTrue(caughtException);
    }

    private void AvoidUnusedVariableCompilerWarning(AssertionException e)
    {
      string msg = e.Message;
    }
  }
}
