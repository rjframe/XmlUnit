using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using NUnit.Framework;
using XmlUnit;

namespace XmlUnit.Tests
{

  [TestFixture]
  public class ValidatorTests
  {
    public static readonly string VALID_FILE = ".\\etc\\BookXsdGenerated.xml";
    public static readonly string INVALID_FILE = ".\\etc\\invalidBook.xml";
    public static readonly string XSD_FILE = ".\\etc\\Book.xsd";

    [Test]
    public void TestXmlFilesExist()
    {
      Assert.That(File.Exists(Path.Combine(Environment.CurrentDirectory, VALID_FILE)), "Cannot find VALID_FILE");
      Assert.That(File.Exists(Path.Combine(Environment.CurrentDirectory, INVALID_FILE)), "Cannot find INVALID_FILE");
      Assert.That(File.Exists(Path.Combine(Environment.CurrentDirectory, XSD_FILE)), "Cannot find XSD_FILE");
    }

    [Test]
    public void XsdValidFileIsValid()
    {
      PerformAssertion(VALID_FILE, true);
    }

    private Validator PerformAssertion(string file, bool expected)
    {
      Console.WriteLine(Environment.CurrentDirectory);
      using (var input = File.Open(file, FileMode.Open, FileAccess.Read))
      {
        var validator = new Validator(new XmlInput(new StreamReader(input)));

        Assert.AreEqual(expected, validator.IsValid, String.Format("Expected {0}, was {1}. {2}", expected, validator.IsValid, validator.ValidationMessage));
        return validator;
      }
    }

    [Test]
    public void XsdInvalidFileIsNotValid()
    {
      Validator validator = PerformAssertion(INVALID_FILE, false);
      Assert.IsFalse(validator.IsValid, validator.ValidationMessage);
    }

    [Test]
    public void XsdInvalidFileIsNotValidFindsCorrectProblem()
    {
      // This tests that the validation fails at the correct spot
      Validator validator = PerformAssertion(INVALID_FILE, false);
      Assert.IsTrue(validator.ValidationMessage.IndexOf("http://www.publisher.org") > -1, validator.ValidationMessage);
      Assert.IsTrue(validator.ValidationMessage.IndexOf("Book") > -1, validator.ValidationMessage);
    }
  }
}
