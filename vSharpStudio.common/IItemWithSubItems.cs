using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
    public interface IItemWithSubItems : IGuid, IName, ICompositeName, IParent
    {
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false);
        IReadOnlyList<IItemWithSubItems> GetIncludedSubItems(string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
        IForm GetForm(FormType ftype, string guidAppPrjGen);
        string GetDebuggerDisplay(bool isOptimistic);
    }
}
