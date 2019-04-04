using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    public partial class DataType : EntityObjectBase<DataType, DataType.DataTypeValidator>, ITreeNode
    {
        public void OnInitFromDto()
        {
        }
        public DataType(EnumDataType type, uint? length = null, uint? accuracy = null) : this()
        {
            this.DataTypeEnum = type;
            switch (this.DataTypeEnum)
            {
                case EnumDataType.Any:
                    break;
                case EnumDataType.Bool:
                    break;
                case EnumDataType.Catalogs:
                    break;
                case EnumDataType.Documents:
                    break;
                case EnumDataType.Numerical:
                    // TODO revisit default length and accuracy for Numerical
                    this.Length = length ?? 16;
                    this.Accuracy = accuracy ?? 2;
                    break;
                case EnumDataType.Enum:
                    break;
                case EnumDataType.String:
                    this.Length = length ?? 30;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public DataType(EnumDataType type, string guidOfType) : this()
        {
            this.DataTypeEnum = type;
            this.TypeGuid = guidOfType;
            switch (this.DataTypeEnum)
            {
                case EnumDataType.Catalog:
                    break;
                case EnumDataType.Constant:
                    break;
                case EnumDataType.Document:
                    break;
                case EnumDataType.Enum:
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public BigInteger MinValue
        {
            set
            {
                if (_MinValue != value)
                {
                    _MinValue = value;
                    NotifyPropertyChanged();
                    ValidateProperty();
                    this.MinValueString = _MinValue.ToString();
                }
            }
            get
            {
                if (_MinValue == null)
                {
                    BigInteger v;
                    if (BigInteger.TryParse(this.MinValueString, out v))
                        _MinValue = v;
                }
                return _MinValue;
            }
        }
        private BigInteger _MinValue;
        public BigInteger MaxValue
        {
            set
            {
                if (_MaxValue != value)
                {
                    _MaxValue = value;
                    NotifyPropertyChanged();
                    ValidateProperty();
                    this.MaxValueString = _MaxValue.ToString();
                }
            }
            get
            {
                if (_MaxValue == null)
                {
                    BigInteger v;
                    if (BigInteger.TryParse(this.MaxValueString, out v))
                        _MaxValue = v;
                }
                return _MaxValue;
            }
        }
        private BigInteger _MaxValue;

        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes
        {
            get { return this._SubNodes; }
            set
            {
                this._SubNodes = value;
                NotifyPropertyChanged();
            }
        }
        private IEnumerable<ITreeNode> _SubNodes;
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                this._IsSelected = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsSelected;
        public bool IsExpended
        {
            get { return this._IsExpended; }
            set
            {
                this._IsExpended = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsExpended;

        #endregion ITreeNode
    }
}
