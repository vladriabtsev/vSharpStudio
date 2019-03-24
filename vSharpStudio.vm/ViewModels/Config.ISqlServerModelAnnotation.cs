using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config : IRelationalModelAnnotations
    {
        IReadOnlyList<ISequence> IRelationalModelAnnotations.Sequences => throw new NotImplementedException();

        IReadOnlyList<IDbFunction> IRelationalModelAnnotations.DbFunctions => throw new NotImplementedException();

        string IRelationalModelAnnotations.DefaultSchema => throw new NotImplementedException();

        IDbFunction IRelationalModelAnnotations.FindDbFunction(MethodInfo method)
        {
            throw new NotImplementedException();
        }

        ISequence IRelationalModelAnnotations.FindSequence(string name, string schema)
        {
            throw new NotImplementedException();
        }
    }
}
