XmlUnit - C#
------------

This is a modification of XmlUnit for C# from [xmlunit.sourceforge.net](http://xmlunit.sourceforge.net). I have updated it for use with NUnit 2.x (tested with NUnit 2.6, but should work with other versions as well. The `master-net2.0` branch is still compatible with the .NET 2.0 Framework, but the `master` branch targets .NET 3.5.

### Documentation

The tests probably provide the best documentation for this (it's pretty simple). The [documentation for the original (Java) project](http://xmlunit.sourceforge.net/userguide/html/index.html) is probably useful, though the Java project is far ahead of the C# one.

### Changes

- **(Breaking)** The XmlAssertion class has been replaced with AssertXml, as NUnit's Assertion class has been deprecated and removed.
- Deprecated .NET features have been replaced (.NET 3.5 only).
- **(Breaking)** A validating XML reader now *must* reference a schema to validate against (.NET 3.5 only). Note that validating readers are the default, and must be disabled if you don't want it.

### Examples

    using NUnit.Framework;
    using XmlUnit;

    [TestFixture]
    namespace MyProject.Tests
    {
        private string pathToXml = @"C:\my\xml\file.xml";
        private string path2 = @"C:\my\xml\file2.xml";
        
        public class MyTests
        {
            [Test]
            public void AssertXmlIsValid()
            {
                AssertXml.IsValidXml(new StreamReader(pathToXml));
            }
            
            [Test]
            public void AssertXmlIsEqual()
            {
                // Uses validating parser - must reference a schema
                AssertXml.AreEqual(pathToXml, path2);
                
                // Now the same thing without a validating parser
                var diffConfig = new diffConfiguration(useValidatingParser: false);
                var diff = xmlDiff(pathToXml, path2, diffConfig);
                AssertXml.AreEqual(diff);
            }
        }
    }
