using System.Collections.Generic;

namespace vSharpStudio.common
{
    public interface INodeWithPositionProperties
    {
        uint GetNextFreePosition();
        //uint LastPosition { get; }
        Dictionary<int, IStandartPropertyGuidPosition> DicPositionsForStandartProperties { get; }
    }
}
