﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.std;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config
    {
        public class ValidationMessages
        {
            public const string NAME_START_WITH_DIGIT = "Remove digit from first position of 'Name'. 'Name' of a config object can't start with digit";
            public const string NAME_CANT_CONTAINS_SPACE = "Remove space from the 'Name'. Space is not alowed in a 'Name' of a config object";
            public const string NAME_HAS_TO_BE_UNIQUE = "Change 'Name'. 'Name' of the config object has to be unique";
            public const string NAME_CANT_BE_EMPTY = "Enter 'Name' for config object. 'Name' of the config object can't be empty";

            public const string TYPE_MIN_EMPTY = "Enter value for 'MinValue'";
            public const string TYPE_MAX_EMPTY = "Enter value for 'MaxValue'";
            public const string TYPE_LENGTH_POSITIVE = "Enter positive value for 'Length'";
            public const string TYPE_MAXLENGTH_GREATER_LENGTH = "'Max Length' has to be less or equal 'Length'";
            public const string TYPE_LENGTH_LESS_THAN_ACCURACY = "'Length' has to be greater than 'Accuracy'";
            public const string TYPE_LENGTH_GREATER_THAN_ZERO = "'Length' has to be greater than zero";
            public const string TYPE_ACCURACY_GREATER_THAN_LENGTH = "'Accuracy' has to be less than 'Length'";
            public const string TYPE_MINMAX_CANT_PARSE = "Can't parse to integer";
            public const string TYPE_MIN_HAS_TO_BE_LESS_THAN_MAX = "'MinValue' has to be less than 'MaxValue'";
            public const string TYPE_EMPTY_CONSTANT_NAME = "Please select Constant name";
            public const string TYPE_OBJECT_IS_NOT_FOUND = "Object is not found";
            public const string TYPE_EMPTY_ENUMERATION = "Please select Enumeration";
            public const string TYPE_EMPTY_CATALOG = "Please select Catalog";
            public const string TYPE_EMPTY_DOCUMENT = "Please select Document";
        }
    }
}
