using System.Collections.Generic;
using System.Diagnostics;
using FluentValidation;
using FluentValidation.Results;
using ViewModelBase;

namespace vSharpStudio.common
{
    public class Common
    {
        public static ValidationFailure CreateValidationFailure(string propertyName, string errorMessage, Severity severity = Severity.Error)
        {
            Debug.Assert(!VmBindable.IsDebugStopOnCreateValidationFailure || false);
            return new ValidationFailure(propertyName, errorMessage) { Severity = severity };
        }
    }
    public static class Ext
    {
        public static Dictionary<string, object> ToDicSql(this string param, object obj)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic[param] = obj;
            return dic;
        }
        public static Dictionary<string, object> ToDicSql(this Dictionary<string, object> dic, string param, object obj)
        {
            dic[param] = obj;
            return dic;
        }
    }
}
