using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public static class Ext
    {
        public static bool IsNew(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.New.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsDeprecated(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.Deprecated.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsDeleted(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.Deleted.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsRenamed(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.Renamed.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsCanLooseData(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.CanLooseData.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsDiffDataType(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffDataType.ToString()) == null)
                return false;
            return true;
        }
        public static DiffDataType GetDiffDataType(this IMutableAnnotatable obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffDataType.ToString());
            return (DiffDataType)annotation.Value;
        }
        public static bool IsDiffProperties(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffProperties.ToString()) == null)
                return false;
            return true;
        }
        public static DiffProperties GetDiffProperties(this IMutableAnnotatable obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffProperties.ToString());
            return (DiffProperties)annotation.Value;
        }
    }
}
