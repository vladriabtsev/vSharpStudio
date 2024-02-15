using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class RegisterMappingRow : VmValidatable<RegisterMappingRow, RegisterMappingRowValidator>
    {
        public Document Doc { get; private set; }
        public Register Reg { get; private set; }
        public RegisterDimension? Dimension { get; private set; }
        public Property? AttachedProperty { get; private set; }
        public string RegPropertyGuid
        {
            get { return _RegPropertyGuid; }
            set
            {
                if (SetProperty(ref this._RegPropertyGuid, value))
                {
                    this.ValidateProperty();
                }
            }
        }
        private string _RegPropertyGuid;
        public RegisterMappingRow(Document doc, Register reg, RegisterDimension dim)
        : base(RegisterMappingRowValidator.Validator)
        {
            this.Doc = doc;
            this.Reg = reg;
            this.Dimension = dim;
            this.RegPropertyGuid = dim.Guid;
            this.Name = dim.Name;
        }
        public RegisterMappingRow(Document doc, Register reg, string regPropertyGuid, string accumulatorName)
            : base(RegisterMappingRowValidator.Validator)
        {
            this.Doc = doc;
            this.Reg = reg;
            this.RegPropertyGuid = regPropertyGuid;
            this.Name = accumulatorName;
        }
        public string Name { get; private set; }
        public Property? Selected
        {
            get => _Selected;
            set
            {
                if (SetProperty(ref _Selected, value))
                {
                    if (_Selected != null)
                        this.Reg.MappingRegPropertyAdd(this.Doc.Guid, this.RegPropertyGuid, _Selected.Guid);
                    else
                        this.Reg.MappingRegPropertyRemove(this.Doc.Guid, this.RegPropertyGuid);
                    this.ValidateProperty();
                }
            }
        }
        private Property? _Selected;
        public ObservableCollection<Property> ListToMap
        {
            get => _ListToMap;
            set => SetProperty(ref _ListToMap, value);
        }
        private ObservableCollection<Property> _ListToMap = new ObservableCollection<Property>();
    }
}
