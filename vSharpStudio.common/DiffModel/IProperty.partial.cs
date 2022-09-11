using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IProperty : IParent, ITreeConfigNode, IGetNodeSetting, ILayoutFieldParameters
    {
        //string DefaultValue { get; }
        bool IsPKey { get; set; }
        bool IsComputed { get; set; }
        bool IsDocShared { get; set; }
        string ComplexObjectName { get; set; }
        string PropValueValue { get; }
        IPropertyRangeValuesRequirements RangeValuesRequirementsI { get; }
        string ComplexObjectNameWithDot();
        object Tag { get; set; }
        static IConfig Config { get; set; }
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
