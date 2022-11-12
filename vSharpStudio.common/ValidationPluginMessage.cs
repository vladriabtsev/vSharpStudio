using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public class ValidationPluginMessage
    {
        public enum EnumValidationMessage { Info, Warning, Error }
        public EnumValidationMessage Level { get; set; }
        public string? Message { get; set; }
        public string? PropertyName { get; set; }
    }
}
