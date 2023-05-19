using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using vSharpStudio.common.DiffModel;
using vSharpStudio.common;
using vSharpStudio.vm.ViewModels;
using vSharpStudio.wpf.Controls;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;
using System.Windows;
using ViewModelBase;
using System.Windows.Controls;
using System.Xml.Linq;
using System.DirectoryServices;

namespace vSharpStudio.ViewModels
{
    // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/observableobject
    public class EditorRoleColumnVm : ObservableObject
    {
        private readonly ITreeConfigNode node;
        private readonly Role role;
        private readonly Action<Role> updateChildren;
        public string RoleGuid { get; private set; }
        internal EditorRoleColumnVm(ITreeConfigNode node, Role role, Action<Role> updateChildren)
        {
            this.node = node;
            this.role = role;
            this.RoleGuid = role.Guid;
            this.updateChildren = updateChildren;
            this.Update();
        }
        private string GetEnumValueDesc<TEnum>(TEnum val, int? v, bool isObjectUnderRole = false)
        {
            //if (v.HasValue && v.Value == 0 && !isObjectUnderRole)
            //    return string.Empty;
            var nam = val.ToString();
            Debug.Assert(nam != null);
            MemberInfo? memberInfo = typeof(TEnum).GetMember(nam).FirstOrDefault();
            Debug.Assert(memberInfo != null);
            var attributeObject = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
            if (attributeObject == null)
                return string.Empty;
            var attribute = (DescriptionAttribute)attributeObject;
            Debug.Assert(attribute != null);
            var s = attribute.Description;
            return s == null ? string.Empty : s;
        }
        public void Update()
        {
            if (node is IRoleAccess ra)
            {
                this.ListPrintAccess = VMHelpers.GetEnumComboBox<EnumPrintAccess>();
                switch (node)
                {
                    case Property p:
                        var pa = (RolePropertyAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(p.GetRolePropertyAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(p.GetRolePropertyPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumPropertyAccess>();
                        this._SelectedEditAccess = pa.EditAccess;
                        this._SelectedPrintAccess = pa.PrintAccess;
                        this.IsCustomEditAccess = pa.EditAccess != EnumPropertyAccess.P_BY_PARENT;
                        this.IsCustomPrintAccess = pa.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    case GroupListProperties gp:
                        pa = (RolePropertyAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(gp.GetRolePropertyAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(gp.GetRolePropertyPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumPropertyAccess>();
                        this._SelectedEditAccess = pa.EditAccess;
                        this._SelectedPrintAccess = pa.PrintAccess;
                        this.IsCustomEditAccess = pa.EditAccess != EnumPropertyAccess.P_BY_PARENT;
                        this.IsCustomPrintAccess = pa.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    case Constant cnst:
                        var cnsta = (RoleConstantAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(cnst.GetRoleConstantAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(cnst.GetRoleConstantPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumConstantAccess>();
                        this._SelectedEditAccess = cnsta.EditAccess;
                        this._SelectedPrintAccess = cnsta.PrintAccess;
                        this.IsCustomEditAccess = cnsta.EditAccess != EnumConstantAccess.CN_BY_PARENT;
                        this.IsCustomPrintAccess = cnsta.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    case GroupListConstants gcnst:
                        cnsta = (RoleConstantAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(gcnst.GetRoleConstantAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(gcnst.GetRoleConstantPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumConstantAccess>();
                        this._SelectedEditAccess = cnsta.EditAccess;
                        this._SelectedPrintAccess = cnsta.PrintAccess;
                        this.IsCustomEditAccess = cnsta.EditAccess != EnumConstantAccess.CN_BY_PARENT;
                        this.IsCustomPrintAccess = cnsta.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    case Catalog c:
                        var ca = (RoleCatalogAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(c.GetRoleCatalogAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(c.GetRoleCatalogPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
                        this._SelectedEditAccess = ca.EditAccess;
                        this._SelectedPrintAccess = ca.PrintAccess;
                        this.IsCustomEditAccess = ca.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT;
                        this.IsCustomPrintAccess = ca.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    case CatalogFolder cf:
                        ca = (RoleCatalogAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
                        this._SelectedEditAccess = ca.EditAccess;
                        this._SelectedPrintAccess = ca.PrintAccess;
                        this.IsCustomEditAccess = ca.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT;
                        this.IsCustomPrintAccess = ca.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    case Detail dt:
                        var det = (RoleDetailAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(dt.GetRoleDetailAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(dt.GetRoleDetailPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
                        this._SelectedEditAccess = det.EditAccess;
                        this._SelectedPrintAccess = det.PrintAccess;
                        this.IsCustomEditAccess = det.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT;
                        this.IsCustomPrintAccess = det.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    case GroupListDetails gdt:
                        det = (RoleDetailAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(gdt.GetRoleDetailAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(gdt.GetRoleDetailPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
                        this._SelectedEditAccess = det.EditAccess;
                        this._SelectedPrintAccess = det.PrintAccess;
                        this.IsCustomEditAccess = det.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT;
                        this.IsCustomPrintAccess = det.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    case Document d:
                        var da = (RoleDocumentAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(d.GetRoleDocumentAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(d.GetRoleDocumentPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumDocumentAccess>();
                        this._SelectedEditAccess = da.EditAccess;
                        this._SelectedPrintAccess = da.PrintAccess;
                        this.IsCustomEditAccess = da.EditAccess != EnumDocumentAccess.D_BY_PARENT;
                        this.IsCustomPrintAccess = da.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    case GroupListDocuments gd:
                        da = (RoleDocumentAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(gd.GetRoleDocumentAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(gd.GetRoleDocumentPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumDocumentAccess>();
                        this._SelectedEditAccess = da.EditAccess;
                        this._SelectedPrintAccess = da.PrintAccess;
                        this.IsCustomEditAccess = da.EditAccess != EnumDocumentAccess.D_BY_PARENT;
                        this.IsCustomPrintAccess = da.PrintAccess != EnumPrintAccess.PR_BY_PARENT;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            else if (node is IRoleGlobalSetting gra)
            {
                this.ListPrintAccess = VMHelpers.GetEnumComboBox<EnumPrintAccess>();
                switch (node)
                {
                    case GroupConstantGroups:
                        if (role.DefaultConstantEditAccessSettings == EnumConstantAccess.CN_BY_PARENT)
                            role.DefaultConstantEditAccessSettings = EnumConstantAccess.CN_EDIT;
                        this._SelectedEditAccess = role.DefaultConstantEditAccessSettings;
                        if (role.DefaultConstantPrintAccessSettings == EnumPrintAccess.PR_BY_PARENT)
                            role.DefaultConstantPrintAccessSettings = EnumPrintAccess.PR_PRINT;
                        this._SelectedPrintAccess = role.DefaultConstantPrintAccessSettings;
                        this.EditAccessStr = this.GetEnumValueDesc(role.DefaultConstantEditAccessSettings, (int)this._SelectedEditAccess);
                        this.PrintAccessStr = this.GetEnumValueDesc(role.DefaultConstantPrintAccessSettings, (int)this._SelectedPrintAccess);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumConstantAccess>();
                        break;
                    case GroupListCatalogs gc:
                        if (role.DefaultCatalogEditAccessSettings == EnumCatalogDetailAccess.C_BY_PARENT)
                            role.DefaultCatalogEditAccessSettings = EnumCatalogDetailAccess.C_MARK_DEL;
                        this._SelectedEditAccess = role.DefaultCatalogEditAccessSettings;
                        if (role.DefaultCatalogPrintAccessSettings == EnumPrintAccess.PR_BY_PARENT)
                            role.DefaultCatalogPrintAccessSettings = EnumPrintAccess.PR_PRINT;
                        this._SelectedPrintAccess = role.DefaultCatalogPrintAccessSettings;
                        this.EditAccessStr = this.GetEnumValueDesc(role.DefaultCatalogEditAccessSettings, (int)this._SelectedEditAccess);
                        this.PrintAccessStr = this.GetEnumValueDesc(role.DefaultCatalogPrintAccessSettings, (int)this._SelectedPrintAccess);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
                        break;
                    case GroupDocuments gd:
                        if (role.DefaultDocumentEditAccessSettings == EnumDocumentAccess.D_BY_PARENT)
                            role.DefaultDocumentEditAccessSettings = EnumDocumentAccess.D_UNPOST;
                        this._SelectedEditAccess = role.DefaultDocumentEditAccessSettings;
                        if (role.DefaultDocumentPrintAccessSettings == EnumPrintAccess.PR_BY_PARENT)
                            role.DefaultDocumentPrintAccessSettings = EnumPrintAccess.PR_PRINT;
                        this._SelectedPrintAccess = role.DefaultDocumentPrintAccessSettings;
                        this.EditAccessStr = this.GetEnumValueDesc(role.DefaultDocumentEditAccessSettings, (int)this._SelectedEditAccess);
                        this.PrintAccessStr = this.GetEnumValueDesc(role.DefaultDocumentPrintAccessSettings, (int)this._SelectedPrintAccess);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumDocumentAccess>();
                        break;
                    default:
                        throw new NotImplementedException();
                }
                this.IsCustomEditAccess = true;
                this.IsCustomPrintAccess = true;
            }
        }
        // https://en.wikipedia.org/wiki/List_of_Unicode_characters#Arrows

        public IEnumerable? ListEditAccess { get => _ListEditAccess; set => SetProperty(ref _ListEditAccess, value); }
        private IEnumerable? _ListEditAccess;
        public object? SelectedEditAccess
        {
            get => _SelectedEditAccess;
            set
            {
                if (_SelectedEditAccess != value)
                {
                    SetProperty(ref _SelectedEditAccess, value);
                    Debug.Assert(value != null);
                    switch (node)
                    {
                        case Property p:
                            p.SetRoleAccess(this.role, (EnumPropertyAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(p.GetRolePropertyAccess(role), null, true);
                            break;
                        case GroupListProperties gp:
                            gp.SetRoleAccess(this.role, (EnumPropertyAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(gp.GetRolePropertyAccess(role), null, true);
                            break;
                        case Constant cnst:
                            cnst.SetRoleAccess(this.role, (EnumConstantAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(cnst.GetRoleConstantAccess(role), null, true);
                            break;
                        case GroupListConstants gcnst:
                            gcnst.SetRoleAccess(this.role, (EnumConstantAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(gcnst.GetRoleConstantAccess(role), null, true);
                            break;
                        case GroupConstantGroups gcnstg:
                            this.role.DefaultConstantEditAccessSettings = (EnumConstantAccess)value;
                            this.EditAccessStr = this.GetEnumValueDesc((EnumConstantAccess)value, (int)value);
                            break;
                        case Catalog c:
                            c.SetRoleAccess(this.role, (EnumCatalogDetailAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(c.GetRoleCatalogAccess(role), null, true);
                            break;
                        case CatalogFolder cf:
                            cf.SetRoleAccess(this.role, (EnumCatalogDetailAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogAccess(role), null, true);
                            break;
                        case Detail dt:
                            dt.SetRoleAccess(this.role, (EnumCatalogDetailAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(dt.GetRoleDetailAccess(role), null, true);
                            break;
                        case GroupListDetails gdt:
                            gdt.SetRoleAccess(this.role, (EnumCatalogDetailAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(gdt.GetRoleDetailAccess(role), null, true);
                            break;
                        case GroupListCatalogs gc:
                            this.role.DefaultCatalogEditAccessSettings = (EnumCatalogDetailAccess)value;
                            this.EditAccessStr = this.GetEnumValueDesc((EnumCatalogDetailAccess)value, (int)value);
                            break;
                        case Document d:
                            d.SetRoleAccess(this.role, (EnumDocumentAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(d.GetRoleDocumentAccess(role), null, true);
                            break;
                        case GroupListDocuments gd:
                            gd.SetRoleAccess(this.role, (EnumDocumentAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc(gd.GetRoleDocumentAccess(role), null, true);
                            break;
                        case GroupDocuments:
                            this.role.DefaultDocumentEditAccessSettings = (EnumDocumentAccess)value;
                            this.EditAccessStr = this.GetEnumValueDesc((EnumDocumentAccess)value, (int)value);
                            break;
                        default:
                            break;
                    }
                    this.updateChildren(this.role);
                    if ((int)value > 0)
                        this.IsCustomEditAccess = true;
                    else
                        this.IsCustomEditAccess = false;
                }
            }
        }
        private object? _SelectedEditAccess;
        public bool IsCustomEditAccess { get => _IsCustomEditAccess; set => SetProperty(ref _IsCustomEditAccess, value); }
        private bool _IsCustomEditAccess = false;
        public string EditAccessStr { get => _EditAccessStr; set => SetProperty(ref _EditAccessStr, value); }
        private string _EditAccessStr = string.Empty;

        public IEnumerable? ListPrintAccess { get => _ListPrintAccess; set => SetProperty(ref _ListPrintAccess, value); }
        private IEnumerable? _ListPrintAccess;
        public EnumPrintAccess? SelectedPrintAccess
        {
            get => _SelectedPrintAccess;
            set
            {
                if (_SelectedPrintAccess != value)
                {
                    SetProperty(ref _SelectedPrintAccess, value);
                    Debug.Assert(value != null);
                    Debug.Assert(this._SelectedPrintAccess != null);
                    switch (node)
                    {
                        case Property p:
                            p.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(p.GetRolePropertyPrint(role), null, true);
                            break;
                        case GroupListProperties gp:
                            gp.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(gp.GetRolePropertyPrint(role), null, true);
                            break;
                        case Constant cnst:
                            cnst.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(cnst.GetRoleConstantPrint(role), null, true);
                            break;
                        case GroupListConstants gcnst:
                            gcnst.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(gcnst.GetRoleConstantPrint(role), null, true);
                            break;
                        case GroupConstantGroups gcnstg:
                            this.role.DefaultConstantPrintAccessSettings = value.Value;
                            this.PrintAccessStr = this.GetEnumValueDesc(role.DefaultConstantPrintAccessSettings, (int)this._SelectedPrintAccess);
                            break;
                        case Catalog c:
                            c.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(c.GetRoleCatalogPrint(role), null, true);
                            break;
                        case CatalogFolder cf:
                            cf.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogPrint(role), null, true);
                            break;
                        case Detail dt:
                            dt.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(dt.GetRoleDetailPrint(role), null, true);
                            break;
                        case GroupListDetails gdt:
                            gdt.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(gdt.GetRoleDetailPrint(role), null, true);
                            break;
                        case GroupListCatalogs gc:
                            this.role.DefaultCatalogPrintAccessSettings = value.Value;
                            this.PrintAccessStr = this.GetEnumValueDesc(role.DefaultCatalogPrintAccessSettings, (int)this._SelectedPrintAccess.Value);
                            break;
                        case Document d:
                            d.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(d.GetRoleDocumentPrint(role), null, true);
                            break;
                        case GroupListDocuments gd:
                            gd.SetRoleAccess(this.role, null, value);
                            this.PrintAccessStr = this.GetEnumValueDesc(gd.GetRoleDocumentPrint(role), null, true);
                            break;
                        case GroupDocuments:
                            this.role.DefaultDocumentPrintAccessSettings = value.Value;
                            this.PrintAccessStr = this.GetEnumValueDesc(role.DefaultDocumentPrintAccessSettings, (int)this._SelectedPrintAccess.Value);
                            break;
                        default:
                            break;
                    }
                    this.updateChildren(this.role);
                    if ((int)value > 0)
                        this.IsCustomPrintAccess = true;
                    else
                        this.IsCustomPrintAccess = false;
                }
            }
        }
        private EnumPrintAccess? _SelectedPrintAccess;
        public bool IsCustomPrintAccess { get => _IsCustomPrintAccess; set => SetProperty(ref _IsCustomPrintAccess, value); }
        private bool _IsCustomPrintAccess = false;
        public string PrintAccessStr { get => _PrintAccessStr; set => SetProperty(ref _PrintAccessStr, value); }
        private string _PrintAccessStr = string.Empty;
    }
}
