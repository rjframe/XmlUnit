using System;
using System.IO;

using NUnit.Framework;

namespace XmlUnit
{

  /// <summary>Implements the classic test model of NUnit's Assert class.</summary>  
  /// <remarks>Replaces the XmlAssertion class from XmlUnit 0.4; Assertion is deprecated in NUnit now.
  /// We will implement NUnit's constraint-based model at a future time.</remarks>
  public class AssertXml : Assert
  {

    #region AreEqual/AreNotEqual

    /// <summary>Worker method to determine whether two pieces of xml markup are similar</summary>
    /// <param name="xmlDiff">The result of an XML comparison</param>
    /// <param name="assertion">true if asserting that the result is similar</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    private static void AreEqual(XmlDiff xmlDiff, Boolean assertion, String message, Object[] args)
    {
      var diffResult = xmlDiff.Compare();
      String msg = message;

      if (message.Equals(String.Empty))
      {
        msg = diffResult.StringValue;
      }

      if (assertion)
      {
        NUnit.Framework.Assert.IsTrue(diffResult.Equal, msg, args);
      }
      else
      {
        NUnit.Framework.Assert.IsFalse(diffResult.Equal, msg, args);
      }
    }

    /// <summary>Assert that two pieces of XML markup are similar</summary>
    /// <param name="xmlDiff">The result of an XML comparison</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    public static void AreEqual(XmlDiff xmlDiff, String message, Object[] args)
    {
      AreEqual(xmlDiff, true, message, args);
    }

    public static void AreEqual(XmlDiff xmlDiff, String message)
    {
      AreEqual(xmlDiff, message, null);
    }

    public static void AreEqual(XmlDiff xmlDiff)
    {
      AreEqual(xmlDiff, String.Empty, null);
    }

    public static void AreEqual(XmlInput controlXml, XmlInput testXml, String message, Object[] args)
    {
      AreEqual(new XmlDiff(controlXml, testXml), message, args);
    }

    public static void AreEqual(XmlInput controlXml, XmlInput testXml, String message)
    {
      AreEqual(new XmlDiff(controlXml, testXml), message, null);
    }

    public static void AreEqual(XmlInput controlXml, XmlInput testXml)
    {
      AreEqual(new XmlDiff(controlXml, testXml), String.Empty, null);
    }

    /// <summary>Assert that two pieces of XML markup are similar</summary>
    /// <param name="controlTextReader">The TextReader object to test against</param>
    /// <param name="testTextReader">The TextReader object to test</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    public static void AreEqual(TextReader controlTextReader, TextReader testTextReader, String message, Object[] args)
    {
      AreEqual(new XmlDiff(controlTextReader, testTextReader), message, args);
    }

    /// <summary>Assert that two pieces of XML are similar</summary>
    /// <param name="controlTextReader">The TextReader object to test against</param>
    /// <param name="testTextReader">The TextReader object to test</param>
    /// <param name="message">The message that will be displayed on failure</param>
    public static void AreEqual(TextReader controlTextReader, TextReader testTextReader, String message)
    {
      AreEqual(new XmlDiff(controlTextReader, testTextReader), message, null);
    }

    /// <summary>Assert that two pieces of XML are similar</summary>
    /// <param name="controlTextReader">The TextReader object to test against</param>
    /// <param name="testTextReader">The TextReader object to test</param>
    public static void AreEqual(TextReader controlTextReader, TextReader testTextReader)
    {
      AreEqual(new XmlDiff(controlTextReader, testTextReader), String.Empty, null);
    }

    public static void AreNotEqual(XmlDiff xmlDiff, String message, Object[] args)
    {
      AreEqual(xmlDiff, false, message, args);
    }

    public static void AreNotEqual(XmlDiff xmlDiff, String message)
    {
      AreNotEqual(xmlDiff, message, null);
    }

    public static void AreNotEqual(XmlDiff xmlDiff)
    {
      AreNotEqual(xmlDiff, String.Empty, null);
    }

    public static void AreNotEqual(TextReader controlTextReader, TextReader testTextReader, String message, Object[] args)
    {
      AreNotEqual(new XmlDiff(controlTextReader, testTextReader), message, args);
    }

    public static void AreNotEqual(TextReader controlTextReader, TextReader testTextReader, String message)
    {
      AreNotEqual(new XmlDiff(controlTextReader, testTextReader), message, null);
    }

    public static void AreNotEqual(TextReader controlTextReader, TextReader testTextReader)
    {
      AreNotEqual(new XmlDiff(controlTextReader, testTextReader), String.Empty, null);
    }

    #endregion AreEqual/AreNotEqual

    #region AreIdentical/AreNotIdentical

    /// <summary>Worker method to determine whether two pieces of xml markup are identical</summary>
    /// <param name="xmlDiff">The result of an XML comparison</param>
    /// <param name="assertion">true if asserting that the result is identical</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    private static void AreIdentical(XmlDiff xmlDiff, Boolean assertion, String message, Object[] args)
    {
      var diffResult = xmlDiff.Compare();
      String msg = message;

      if (message.Equals(String.Empty))
      {
        msg = xmlDiff.OptionalDescription;
      }

      if (assertion)
      {
        NUnit.Framework.Assert.IsTrue(diffResult.Identical, msg, args);
      }
      else
      {
        NUnit.Framework.Assert.IsFalse(diffResult.Identical, msg, args);
      }
    }

    /// <summary>Assert that two pieces of XML markup are identical</summary>
    /// <param name="xmlDiff">The result of an XML comparison</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    public static void AreIdentical(XmlDiff xmlDiff, String message, Object[] args)
    {
      AreIdentical(xmlDiff, true, message, args);
    }

    public static void AreIdentical(XmlDiff xmlDiff, String message)
    {
      AreIdentical(xmlDiff, message, null);
    }

    public static void AreIdentical(XmlDiff xmlDiff)
    {
      AreIdentical(xmlDiff, String.Empty, null);
    }

    /// <summary>Assert that two pieces of XML markup are identical</summary>
    /// <param name="controlTextReader">The TextReader object to test against</param>
    /// <param name="testTextReader">The TextReader object to test</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    public static void AreIdentical(TextReader controlTextReader, TextReader testTextReader, String message, Object[] args)
    {
      AreIdentical(new XmlDiff(controlTextReader, testTextReader), message, args);
    }

    /// <summary>Assert that two pieces of XML markup are identical</summary>
    /// <param name="controlTextReader">The TextReader object to test against</param>
    /// <param name="testTextReader">The TextReader object to test</param>
    /// <param name="message">The message that will be displayed on failure</param>
    public static void AreIdentical(TextReader controlTextReader, TextReader testTextReader, String message)
    {
      AreIdentical(new XmlDiff(controlTextReader, testTextReader), message, null);
    }

    /// <summary>Assert that two pieces of XML markup are identical</summary>
    /// <param name="controlTextReader">The TextReader object to test against</param>
    /// <param name="testTextReader">The TextReader object to test</param>
    public static void AreIdentical(TextReader controlTextReader, TextReader testTextReader)
    {
      AreIdentical(new XmlDiff(controlTextReader, testTextReader), String.Empty, null);
    }

    public static void AreIdentical(String controlXml, String testXml, String message, Object[] args)
    {
      AreIdentical(new XmlDiff(controlXml, testXml), message, args);
    }

    public static void AreIdentical(String controlXml, String testXml, String message)
    {
      AreIdentical(new XmlDiff(controlXml, testXml), message, null);
    }

    public static void AreIdentical(String controlXml, String testXml)
    {
      AreIdentical(new XmlDiff(controlXml, testXml), String.Empty, null);
    }

    /// <summary>Assert that two pieces of XML markup are not identical</summary>
    /// <param name="xmlDiff">The result of an XML comparison</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    public static void AreNotIdentical(XmlDiff xmlDiff, String message, Object[] args)
    {
      AreIdentical(xmlDiff, false, message, args);
    }

    public static void AreNotIdentical(XmlDiff xmlDiff, String message)
    {
      AreNotIdentical(xmlDiff, message, null);
    }

    public static void AreNotIdentical(XmlDiff xmlDiff)
    {
      AreNotIdentical(xmlDiff, String.Empty, null);
    }

    /// <summary>Assert that two pieces of XML markup are not identical</summary>
    /// <param name="controlTextReader">The TextReader object to test against</param>
    /// <param name="testTextReader">The TextReader object to test</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    public static void AreNotIdentical(TextReader controlTextReader, TextReader testTextReader, String message, Object[] args)
    {
      AreNotIdentical(new XmlDiff(controlTextReader, testTextReader), message, args);
    }

    /// <summary>Assert that two pieces of XML markup are not identical</summary>
    /// <param name="controlTextReader">The TextReader object to test against</param>
    /// <param name="testTextReader">The TextReader object to test</param>
    /// <param name="message">The message that will be displayed on failure</param>
    public static void AreNotIdentical(TextReader controlTextReader, TextReader testTextReader, String message)
    {
      AreNotIdentical(new XmlDiff(controlTextReader, testTextReader), message, null);
    }

    /// <summary>Assert that two pieces of XML markup are not identical</summary>
    /// <param name="controlTextReader">The TextReader object to test against</param>
    /// <param name="testTextReader">The TextReader object to test</param>
    public static void AreNotIdentical(TextReader controlTextReader, TextReader testTextReader)
    {
      AreNotIdentical(new XmlDiff(controlTextReader, testTextReader), String.Empty, null);
    }

    public static void AreNotIdentical(String controlXml, String testXml, String message, Object[] args)
    {
      AreNotIdentical(new XmlDiff(controlXml, testXml), message, args);
    }

    public static void AreNotIdentical(String controlXml, String testXml, String message)
    {
      AreNotIdentical(new XmlDiff(controlXml, testXml), message, null);
    }

    public static void AreNotIdentical(String controlXml, String testXml)
    {
      AreNotIdentical(new XmlDiff(controlXml, testXml), String.Empty, null);
    }

    #endregion

    #region IsValidXml/IsNotValidXml


    /// <summary>Worker method to determine if a given piece of XML is valid</summary>
    /// <param name="validator">The object that validates the XML</param>
    /// <param name="assertion">true if asserting that the XML is valid</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    /// <seealso cref="XmlUnit.Validator"/>
    private static void IsValidXml(Validator validator, Boolean assertion, String message, Object[] args)
    {
      String msg = message;

      if (message.Equals(String.Empty))
      {
        msg = validator.ValidationMessage;
      }

      if (assertion)
      {
        NUnit.Framework.Assert.IsTrue(validator.IsValid, msg, args);
      }
      else
      {
        NUnit.Framework.Assert.IsFalse(validator.IsValid, msg, args);
      }
    }

    /// <summary>Assert that a Validator instance returns true</summary>
    /// <param name="validator">The object that validates the XML</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    /// <seealso cref="XmlUnit.Validator"/>
    public static void IsValidXml(Validator validator, String message, Object[] args)
    {
      IsValidXml(validator, true, message, args); 
    }

    /// <summary>Assert that a Validator instance returns true</summary>
    /// <param name="validator">The object that validates the XML</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <seealso cref="XmlUnit.Validator"/>
    public static void IsValidXml(Validator validator, String message)
    {
      IsValidXml(validator, message, null);
    }

    /// <summary>Assert that a Validator instance returns true</summary>
    /// <param name="validator">The object that validates the XML</param>
    /// <seealso cref="XmlUnit.Validator"/>
    public static void IsValidXml(Validator validator)
    {
      IsValidXml(validator, String.Empty, null);
    }
    
    /// <summary>Assert that a given piece of XML is valid. It must have a DOCTYPE.</summary>
    /// <param name="xmlText">The XML to validate</param>
    /// <param name="baseUri">The XML's base URI</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    public static void IsValidXml(String xmlText, String baseUri, String message, Object[] args)
    {
      IsValidXml(new XmlInput(xmlText, baseUri), message, args);
    }

    /// <summary>Assert that a given piece of XML is valid. It must have a DOCTYPE.</summary>
    /// <param name="xmlText">The XML to validate</param>
    /// <param name="baseUri">The XML's base URI</param>
    /// <param name="message">The message that will be displayed on failure</param>
    public static void IsValidXml(String xmlText, String baseUri, String message)
    {
      IsValidXml(new XmlInput(xmlText, baseUri), message, null);
    }

    /// <summary>Assert that a given piece of XML is valid. It must have a DOCTYPE.</summary>
    /// <param name="xmlText">The XML to validate</param>
    /// <param name="message">The message that will be displayed on failure</param>
    /// <param name="args">Arguments to be used in formatting the message</param>
    public static void IsValidXml(String xmlText, String message, Object[] args)
    {
      IsValidXml(new XmlInput(xmlText), message, args);
    }

    /// <summary>Assert that a given piece of XML is valid. It must have a DOCTYPE.</summary>
    /// <param name="xmlText">The XML to validate</param>
    /// <param name="message">The message that will be displayed on failure</param>
    public static void IsValidXml(String xmlText, String message)
    {
      IsValidXml(new XmlInput(xmlText), message, null);
    }

    /// <summary>Assert that a given piece of XML is valid. It must have a DOCTYPE.</summary>
    /// <param name="xmlText">The XML to validate</param>
    public static void IsValidXml(String xmlText)
    {
      IsValidXml(new XmlInput(xmlText), String.Empty, null);
    }

    public static void IsValidXml(TextReader reader, String baseUri, String message, Object[] args)
    {
      IsValidXml(new XmlInput(reader, baseUri), message, args);
    }
    
    public static void IsValidXml(TextReader reader, String baseUri, String message)
    {
      IsValidXml(new XmlInput(reader, baseUri), message, null);
    }
    
    public static void IsValidXml(TextReader reader, String message, Object[] args)
    {
      IsValidXml(new XmlInput(reader), message, args);
    }

    public static void IsValidXml(TextReader reader, String message)
    {
      IsValidXml(new XmlInput(reader), message, null);
    }

    public static void IsValidXml(TextReader reader)
    {
      IsValidXml(new XmlInput(reader), String.Empty, null);
    }

    public static void IsValidXml(XmlInput xmlInput, String message, Object[] args)
    {
      IsValidXml(new Validator(xmlInput), message, args);
    }
    
    public static void IsValidXml(XmlInput xmlInput, String message)
    {
      IsValidXml(new Validator(xmlInput), message, null);
    }

    public static void IsValidXml(XmlInput xmlInput)
    {
      IsValidXml(new Validator(xmlInput), String.Empty, null);
    }

    #endregion IsValidXml/IsNotValidXml

    #region XPath

    public static void XPathExists(String expression, XmlInput xmlInput, String message, Object[] args)
    {
      XPath xpath = new XPath(expression);
      NUnit.Framework.Assert.AreEqual(true, xpath.XPathExists(xmlInput), message, args);
    }

    public static void XPathExists(String expression, XmlInput xmlInput, String message)
    {
      XPathExists(expression, xmlInput, message, null);
    }

    public static void XPathExists(String expression, XmlInput xmlInput)
    {
      XPathExists(expression, xmlInput, String.Empty, null);
    }

    public static void XPathExists(String expression, String xml, String message, Object[] args)
    {
      XPathExists(expression, new XmlInput(xml), message, args);
    }

    public static void XPathExists(String expression, String xml, String message)
    {
      XPathExists(expression, new XmlInput(xml), message, null);
    }

    public static void XPathExists(String expression, String xml)
    {
      XPathExists(expression, new XmlInput(xml), String.Empty, null);
    }

    public static void XPathExists(String expression, TextReader xmlReader, String message, Object[] args)
    {
      XPathExists(expression, new XmlInput(xmlReader), message, args);
    }

    public static void XPathExists(String expression, TextReader xmlReader, String message)
    {
      XPathExists(expression, new XmlInput(xmlReader), message, null);
    }

    public static void XPathExists(String expression, TextReader xmlReader)
    {
      XPathExists(expression, new XmlInput(xmlReader), String.Empty, null);
    }

    public static void XPathEvaluatesTo(String expression, XmlInput xmlInput, String expectedValue,
        String message, Object[] args)
    {
      var xpath = new XPath(expression);
      NUnit.Framework.Assert.AreEqual(expectedValue, xpath.EvaluateXPath(xmlInput), message, args);
    }

    public static void XPathEvaluatesTo(String expression, XmlInput xmlInput, String expectedValue, String message)
    {
      XPathEvaluatesTo(expression, xmlInput, expectedValue, message, null);
    }

    public static void XPathEvaluatesTo(String expression, XmlInput xmlInput, String expectedValue)
    {
      XPathEvaluatesTo(expression, xmlInput, expectedValue, String.Empty, null);
    }

    public static void XPathEvaluatesTo(String expression, String xml, String expectedValue, String message,
        Object[] args)
    {
      XPathEvaluatesTo(expression, new XmlInput(xml), expectedValue, message, args);
    }

    public static void XPathEvaluatesTo(String expression, String xml, String expectedValue, String message)
    {
      XPathEvaluatesTo(expression, new XmlInput(xml), expectedValue, message, null);
    }

    public static void XPathEvaluatesTo(String expression, String xml, String expectedValue)
    {
      XPathEvaluatesTo(expression, new XmlInput(xml), expectedValue, String.Empty, null);
    }

    public static void XPathEvaluatesTo(String expression, TextReader xmlReader, String expectedValue,
        String message, Object[] args)
    {
      XPathEvaluatesTo(expression, new XmlInput(xmlReader), expectedValue, message, args);
    }

    public static void XPathEvaluatesTo(String expression, TextReader xmlReader, String expectedValue, String message)
    {
      XPathEvaluatesTo(expression, new XmlInput(xmlReader), expectedValue, message, null);
    }

    public static void XPathEvaluatesTo(String expression, TextReader xmlReader, String expectedValue)
    {
      XPathEvaluatesTo(expression, new XmlInput(xmlReader), expectedValue, String.Empty, null);
    }

    #endregion XPath

    #region XSL

    public static void XslTransformResults(XmlInput xslTransform, XmlInput xmlToTransform, XmlInput expectedResult,
        String message, Object[] args)
    {
      Xslt xslt = new Xslt(xslTransform);
      XmlOutput output = xslt.Transform(xmlToTransform);
      Console.WriteLine(output.AsString());
      AreEqual(expectedResult, output.AsXml());
    }

    public static void XslTransformResults(XmlInput xslTransform, XmlInput xmlToTransform, XmlInput expectedResult,
        String message)
    {
      XslTransformResults(xslTransform, xmlToTransform, expectedResult, message, null);
    }

    public static void XslTransformResults(XmlInput xslTransform, XmlInput xmlToTransform, XmlInput expectedResult)
    {
      XslTransformResults(xslTransform, xmlToTransform, expectedResult, String.Empty, null);
    }

    public static void XslTransformResults(String xslTransform, String xmlToTransform, String expectedResult,
        String message, Object[] args)
    {
      XslTransformResults(new XmlInput(xslTransform), new XmlInput(xmlToTransform), new XmlInput(expectedResult),
          message, args);
    }

    public static void XslTransformResults(String xslTransform, String xmlToTransform, String expectedResult,
        String message)
    {
      XslTransformResults(new XmlInput(xslTransform), new XmlInput(xmlToTransform), new XmlInput(expectedResult),
          message, null);
    }

    public static void XslTransformResults(String xslTransform, String xmlToTransform, String expectedResult)
    {
      XslTransformResults(new XmlInput(xslTransform), new XmlInput(xmlToTransform), new XmlInput(expectedResult),
          String.Empty, null);
    }

    #endregion XSL

  }
}
