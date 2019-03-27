using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ViewModelBase;
using FluentValidation;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DataType
    {
        public partial class DataTypeValidator
        {
            public DataTypeValidator()
            {
                RuleFor(x => x.MinValueString).NotEmpty().WithMessage("Please provide minimum value").WithSeverity(Severity.Warning);
                RuleFor(x => x.MaxValueString).NotEmpty().WithMessage("Please provide maximum value").WithSeverity(Severity.Warning);
                RuleFor(x => x.MinValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                RuleFor(x => x.MaxValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                RuleFor(x => x.Length).GreaterThan(0u);
                RuleFor(x => x.Accuracy).LessThan(x => x.Length);
                RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Catalog).WithMessage("Please select catalog name");
                RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Document).WithMessage("Please select document name");
            }
            private bool ParsableToBigInteger(string val)
            {
                BigInteger v;
                return BigInteger.TryParse(val, out v);
            }
        }
    }
}
