using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
    public interface IItemWithDetails : IGuid, IName, ICompositeName, IParent
    {
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
        IForm GetForm(FormType ftype, string guidAppPrjGen);
        string GetDebuggerDisplay(bool isOptimistic);
    }
}
