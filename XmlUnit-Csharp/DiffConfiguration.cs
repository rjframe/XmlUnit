using System;
using System.Xml;

namespace XmlUnit
{

  public class DiffConfiguration
  {
    public const WhitespaceHandling DEFAULT_WHITESPACE_HANDLING = WhitespaceHandling.All;
    public const String DEFAULT_DESCRIPTION = "XmlDiff";
    public const Boolean DEFAULT_USE_VALIDATING_PARSER = true;
    public const Boolean DEFAULT_IGNORE_ATTRIBUTE_ORDER = true;

    private readonly string _description;
    private readonly bool _useValidatingParser;
    private readonly WhitespaceHandling _whitespaceHandling;
    private readonly bool ignoreAttributeOrder;

    public DiffConfiguration(String description = DEFAULT_DESCRIPTION,
        Boolean useValidatingParser = DEFAULT_USE_VALIDATING_PARSER,
        WhitespaceHandling whitespaceHandling = DEFAULT_WHITESPACE_HANDLING,
        Boolean ignoreAttributeOrder = DEFAULT_IGNORE_ATTRIBUTE_ORDER)
    {
      _description = description;
      _useValidatingParser = useValidatingParser;
      _whitespaceHandling = whitespaceHandling;
      this.ignoreAttributeOrder = ignoreAttributeOrder;
    }

    public DiffConfiguration(String description, WhitespaceHandling whitespaceHandling)
      : this(description, DEFAULT_USE_VALIDATING_PARSER, whitespaceHandling)
    {
    }

    public DiffConfiguration(WhitespaceHandling whitespaceHandling)
      : this(DEFAULT_DESCRIPTION, DEFAULT_USE_VALIDATING_PARSER, whitespaceHandling)
    {
    }
    
    public DiffConfiguration(Boolean useValidatingParser)
      : this(DEFAULT_DESCRIPTION, useValidatingParser, DEFAULT_WHITESPACE_HANDLING)
    {
    }

    public string Description
    {
      get
      {
        return _description;
      }
    }

    public bool UseValidatingParser
    {
      get
      {
        return _useValidatingParser;
      }
    }

    public WhitespaceHandling WhitespaceHandling
    {
      get
      {
        return _whitespaceHandling;
      }
    }

    public bool IgnoreAttributeOrder
    {
      get
      {
        return ignoreAttributeOrder;
      }
    }
  }
}
