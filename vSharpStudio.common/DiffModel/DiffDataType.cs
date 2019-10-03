using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffDataType
    {
        public DiffDataType(IDataType previous, IDataType current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public IDataType Current { get; private set; }
        public IDataType Previous { get; private set; }
        public bool IsSame()
        {
            if (this.Previous == null)
                return true;
            if (this.Current == null)
                return false;
            if (this.Current.DataTypeEnum != this.Previous.DataTypeEnum)
                return false;
            if (this.Current.IsNullable != this.Previous.IsNullable)
                return false;
            switch (this.Current.DataTypeEnum)
            {
                case EnumDataType.ANY:
                case EnumDataType.BOOL:
                case EnumDataType.CATALOG:
                case EnumDataType.CATALOGS:
                case EnumDataType.DATE:
                case EnumDataType.DATETIME:
                case EnumDataType.DOCUMENT:
                case EnumDataType.DOCUMENTS:
                case EnumDataType.ENUMERATION:
                case EnumDataType.TIME:
                    break;
                case EnumDataType.NUMERICAL:
                    if (this.Current.Length != this.Previous.Length)
                        return false;
                    if (this.Current.Accuracy != this.Previous.Accuracy)
                        return false;
                    if (this.Current.IsPositive != this.Previous.IsPositive)
                        return false;
                    break;
                case EnumDataType.STRING:
                    if (this.Current.Length != this.Previous.Length)
                        return false;
                    break;
                default:
                    throw new ArgumentException();
            }
            return true;
        }
    }
}
