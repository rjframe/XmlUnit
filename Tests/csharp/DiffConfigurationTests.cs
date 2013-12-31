using XmlUnit;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using NUnit.Framework;


namespace XmlUnit.Tests
{

  [TestFixture]
  public class DiffConfigurationTests
  {
    private static string xmlWithWhitespace = "<elemA>as if<elemB> \r\n </elemB>\t</elemA>";
    private static string xmlWithoutWhitespaceElement = "<elemA>as if<elemB/>\r\n</elemA>";
    private static string xmlWithWhitespaceElement = "<elemA>as if<elemB> </elemB></elemA>";
    private static string xmlWithoutWhitespace = "<elemA>as if<elemB/></elemA>";

    [Test]
    public void DefaultConfiguredWithGenericDescription()
    {
      var diffConfiguration = new DiffConfiguration();
      Assert.AreEqual(DiffConfiguration.DEFAULT_DESCRIPTION, diffConfiguration.Description);

      Assert.AreEqual(DiffConfiguration.DEFAULT_DESCRIPTION,
                             new XmlDiff("", "").OptionalDescription);
    }

    [Test]
    public void DefaultConfiguredToUseValidatingParser()
    {
      DiffConfiguration diffConfiguration = new DiffConfiguration();
      Assert.AreEqual(DiffConfiguration.DEFAULT_USE_VALIDATING_PARSER,
                             diffConfiguration.UseValidatingParser);

      using (FileStream controlFileStream = File.Open(ValidatorTests.VALID_FILE,
                                                      FileMode.Open,
                                                      FileAccess.Read))
      using (FileStream testFileStream = File.Open(ValidatorTests.INVALID_FILE,
                                                   FileMode.Open,
                                                   FileAccess.Read))
      {
        XmlDiff diff = new XmlDiff(new StreamReader(controlFileStream), new StreamReader(testFileStream));
        Assert.Throws<XmlSchemaValidationException>(delegate
        {
          diff.Compare();
        });
      }
    }

    [Test]
    public void CanConfigureNotToUseValidatingParser()
    {
      DiffConfiguration diffConfiguration = new DiffConfiguration(useValidatingParser: false);
      Assert.AreEqual(false, diffConfiguration.UseValidatingParser);
      System.Console.WriteLine("Use validating parser: " + diffConfiguration.UseValidatingParser.ToString());

      FileStream controlFileStream = File.Open(ValidatorTests.VALID_FILE,
                                               FileMode.Open, FileAccess.Read);
      FileStream testFileStream = File.Open(ValidatorTests.INVALID_FILE,
                                            FileMode.Open, FileAccess.Read);
      try
      {
        XmlDiff diff = new XmlDiff(new XmlInput(controlFileStream),
                                   new XmlInput(testFileStream),
                                   diffConfiguration);
        diff.Compare();
      }
      catch (XmlSchemaException e)
      {
        Assert.Fail("Unexpected validation failure: " + e.Message);
      }
      finally
      {
        controlFileStream.Close();
        testFileStream.Close();
      }
    }

    [Test]
    public void DefaultConfiguredWithWhitespaceHandlingAll()
    {
      DiffConfiguration diffConfiguration = new DiffConfiguration(useValidatingParser: false);
      Assert.AreEqual(WhitespaceHandling.All, diffConfiguration.WhitespaceHandling);

      PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespaceElement, false, diffConfiguration);
      PerformAssertion(xmlWithoutWhitespace, xmlWithoutWhitespaceElement, false, diffConfiguration);
      PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespace, false, diffConfiguration);
      PerformAssertion(xmlWithoutWhitespaceElement, xmlWithWhitespaceElement, false, diffConfiguration);
    }

    private void PerformAssertion(string control, string test, bool assertion)
    {
      XmlDiff diff = new XmlDiff(control, test);
      PerformAssertion(diff, assertion);
    }
    private void PerformAssertion(string control, string test, bool assertion,
                                  DiffConfiguration xmlUnitConfiguration)
    {
      XmlDiff diff = new XmlDiff(new XmlInput(control), new XmlInput(test),
                                 xmlUnitConfiguration);
      PerformAssertion(diff, assertion);
    }
    private void PerformAssertion(XmlDiff diff, bool assertion)
    {
      Assert.AreEqual(assertion, diff.Compare().Equal);
      Assert.AreEqual(assertion, diff.Compare().Identical);
    }

    [Test]
    public void CanConfigureWhitespaceHandlingSignificant()
    {
      DiffConfiguration xmlUnitConfiguration =
        new DiffConfiguration(whitespaceHandling: WhitespaceHandling.Significant, useValidatingParser: false);
      PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespaceElement,
                       true, xmlUnitConfiguration);
      PerformAssertion(xmlWithoutWhitespace, xmlWithoutWhitespaceElement,
                       true, xmlUnitConfiguration);
      PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespace,
                       true, xmlUnitConfiguration);
      PerformAssertion(xmlWithoutWhitespaceElement, xmlWithWhitespaceElement,
                       true, xmlUnitConfiguration);
    }

    [Test]
    public void CanConfigureWhitespaceHandlingNone()
    {
      DiffConfiguration xmlUnitConfiguration =
        new DiffConfiguration(whitespaceHandling: WhitespaceHandling.None, useValidatingParser: false);
      PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespaceElement,
                       true, xmlUnitConfiguration);
      PerformAssertion(xmlWithoutWhitespace, xmlWithoutWhitespaceElement,
                       true, xmlUnitConfiguration);
      PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespace,
                       true, xmlUnitConfiguration);
      PerformAssertion(xmlWithoutWhitespaceElement, xmlWithWhitespaceElement,
                       true, xmlUnitConfiguration);
    }
  }
}
