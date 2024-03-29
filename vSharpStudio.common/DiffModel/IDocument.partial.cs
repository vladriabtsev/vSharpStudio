﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IDocument : ITreeConfigNodeSortable, IGetNodeSetting, IDbTable
    {
        IGroupListDocuments ParentGroupListDocumentsI { get; }
        IReadOnlyList<IProperty> GetAllProperties(bool isSupportVersion);
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isSupportVersion, bool isExcludeSpecial = false);
        IReadOnlyList<IProperty> GetIncludedPropertiesWithShared(string guidAppPrjGen, bool isSupportVersion, bool isExcludeSpecial = false);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen);
        void GetSpecialProperties(List<IProperty> res, bool isSupportVersion);
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumPropertyAccess GetRolePropertyAccess(IRole role);
        EnumPrintAccess GetRolePropertyPrint(IRole role);
        EnumDocumentAccess GetRoleDocumentAccess(IRole role);
        EnumPrintAccess GetRoleDocumentPrint(IRole role);
        IReadOnlyList<string> GetRolesByAccess(EnumDocumentAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        string FullName { get; } // name with config name
    }
}
