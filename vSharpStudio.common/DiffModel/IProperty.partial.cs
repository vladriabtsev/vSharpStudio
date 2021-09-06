using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IProperty : IParent, ITreeConfigNode, IGetNodeSetting
    {
        //string DefaultValue { get; }
        bool IsPKey { get; set; }
        bool IsComputed { get; set; }
        string ComplexObjectName { get; set; }
        IPropertyRangeValuesRequirements RangeValuesRequirementsI { get; }
        string ComplexObjectNameWithDot();
    }
    public interface IPropertyRangeValuesRequirements
    {
        bool IsHasRequirements { get; }
        IReadOnlyList<IValidationBoundary> ListBoundaries { get; }
        IReadOnlyList<string> ListValues { get; }
        bool IsHasErrors { get; }
        IReadOnlyList<string> ListErrors { get; }
    }
    public interface IValidationBoundary
    {
        public string BoundaryMin { get; }
        public string BoundaryMax { get; }
    }
}
