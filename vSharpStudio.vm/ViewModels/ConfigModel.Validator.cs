using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigModel
    {
        public partial class ConfigModelValidator
        {
            public ConfigModelValidator()
            {
                //RuleFor(x => x.SolutionPath).NotEmpty();
                //RuleFor(x => x.SolutionPath).Must((o, path) =>
                //{
                //    if (string.IsNullOrWhiteSpace(path))
                //        return true;
                //    return Directory.Exists(path);
                //}).WithMessage(Config.ValidationMessages.FOLDER_IS_NOT_EXISTS);
                //RuleFor(x => x.SolutionPath).Must((o, path) =>
                //{
                //    if (string.IsNullOrWhiteSpace(path))
                //        return true;
                //    if (!Directory.Exists(path))
                //        return true;
                //    var lst = Directory.EnumerateFiles(path, "*.sln");
                //    var n = lst.GetEnumerator();
                //    if (n.MoveNext())
                //        return true;
                //    return false;
                //}).WithMessage(Config.ValidationMessages.SOLUTION_FOLDER_IS_NOT_EXISTS);
            }
        }
    }
}
