using System;
using System.Xml;

namespace XmlUnit
{

  public class Difference
  {
    private readonly DifferenceType _id;
    private readonly bool _majorDifference;
    private XmlNodeType _controlNodeType;
    private XmlNodeType _testNodeType;

    public Difference(DifferenceType id)
    {
      _id = id;
      _majorDifference = Differences.isMajorDifference(id);
    }

    public Difference(DifferenceType id, XmlNodeType controlNodeType, XmlNodeType testNodeType)
      : this(id)
    {
      _controlNodeType = controlNodeType;
      _testNodeType = testNodeType;
    }

    public DifferenceType Id
    {
      get
      {
        return _id;
      }
    }

    public Boolean MajorDifference
    {
      get
      {
        return _majorDifference;
      }
    }

    public XmlNodeType ControlNodeType
    {
      get
      {
        return _controlNodeType;
      }
    }

    public XmlNodeType TestNodeType
    {
      get
      {
        return _testNodeType;
      }
    }

    public override String ToString()
    {
      return String.Format("{0} type: {1}, control Node: {2}, test Node: {3}",
          base.ToString(), (int)_id, _controlNodeType.ToString(), _testNodeType.ToString());
    }
  }
}
