using System.Collections.Generic;

namespace vSharpStudio.common
{
    public interface INodeWithStandartProperties
    {
        uint LastPosition { get; set; }
        Dictionary<int, IStandartPropertyGuidPosition> DicPositionsForStandartProperties { get; }
    }
}
