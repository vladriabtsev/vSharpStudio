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
    public class EditorRoleBaseVm : ObservableObject
    {
        ITreeConfigNode node;
        Role role;
        internal EditorRoleBaseVm(ITreeConfigNode node, Role role)
        {
            this.node = node;
            this.role = role;
            this.Update(node, role);
        }
        private string GetEnumValueDesc<TEnum>(TEnum val, int? v, bool isObjectUnderRole = false)
        {
            //if (v.HasValue && v.Value == 0 && !isObjectUnderRole)
            //    return string.Empty;
            MemberInfo? memberInfo = typeof(TEnum).GetMember(val.ToString()).FirstOrDefault();
            Debug.Assert(memberInfo != null);
            DescriptionAttribute? attribute = (DescriptionAttribute)memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
            Debug.Assert(attribute != null);
            var s = attribute.Description;
            return s == null ? string.Empty : s;
        }
        public void Update(ITreeConfigNode node, Role role)
        {
            if (node is IRoleAccess ra)
            {
                this.ListPrintAccess = VMHelpers.GetEnumComboBox<EnumPrintAccess>();
                string? s = null;
                switch (node)
                {
                    case Property p:
                        var pa = (RolePropertyAccess)ra.GetRoleAccess(role);
                        var pa2 = p.GetRolePropertyAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(pa2, null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(p.GetRolePropertyPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumPropertyAccess>();
                        this._SelectedEditAccess = pa.EditAccess;
                        this._SelectedPrintAccess = pa.PrintAccess;
                        if (pa.EditAccess != EnumPropertyAccess.P_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (pa.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        //this.EditAccessIcon = Application.Current.FindResource("iconCustomActionEditor");
                        //"iconDownload" iconRedirectedRequest 
                        break;
                    case GroupListProperties gp:
                        pa = (RolePropertyAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(gp.GetRolePropertyAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(gp.GetRolePropertyPrint(role), null, true);
                        //this.EditAccessStr = this.GetEnumValueDesc(pa.EditAccess, (int)pa.EditAccess);
                        //this.PrintAccessStr = this.GetEnumValueDesc(pa.PrintAccess, (int)pa.PrintAccess);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumPropertyAccess>();
                        this._SelectedEditAccess = pa.EditAccess;
                        this._SelectedPrintAccess = pa.PrintAccess;
                        if (pa.EditAccess != EnumPropertyAccess.P_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (pa.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        break;
                    case Constant cnst:
                        var cnsta = (RoleConstantAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(cnst.GetRoleConstantAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(cnst.GetRoleConstantPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumConstantAccess>();
                        this._SelectedEditAccess = cnsta.EditAccess;
                        this._SelectedPrintAccess = cnsta.PrintAccess;
                        if (cnsta.EditAccess != EnumConstantAccess.CN_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (cnsta.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        break;
                    case GroupListConstants gcnst:
                        cnsta = (RoleConstantAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(gcnst.GetRoleConstantAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(gcnst.GetRoleConstantPrint(role), null, true);
                        //this.EditAccessStr = this.GetEnumValueDesc(cnsta.EditAccess, (int)cnsta.EditAccess);
                        //this.PrintAccessStr = this.GetEnumValueDesc(cnsta.PrintAccess, (int)cnsta.PrintAccess);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumConstantAccess>();
                        this._SelectedEditAccess = cnsta.EditAccess;
                        this._SelectedPrintAccess = cnsta.PrintAccess;
                        if (cnsta.EditAccess != EnumConstantAccess.CN_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (cnsta.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        break;
                    case Catalog c:
                        var ca = (RoleCatalogAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(c.GetRoleCatalogAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(c.GetRoleCatalogPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
                        this._SelectedEditAccess = ca.EditAccess;
                        this._SelectedPrintAccess = ca.PrintAccess;
                        if (ca.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (ca.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        break;
                    case CatalogFolder cf:
                        ca = (RoleCatalogAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogPrint(role), null, true);
                        //this.EditAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogAccess(role), (int)ca.EditAccess, true);
                        //this.PrintAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogPrint(role), (int)ca.PrintAccess, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
                        this._SelectedEditAccess = ca.EditAccess;
                        this._SelectedPrintAccess = ca.PrintAccess;
                        //if (ca.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT)
                        //    this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        //if (ca.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                        //    this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        if (ca.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (ca.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        break;
                    case Detail dt:
                        var det = (RoleDetailAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(dt.GetRoleDetailAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(dt.GetRoleDetailPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
                        this._SelectedEditAccess = det.EditAccess;
                        this._SelectedPrintAccess = det.PrintAccess;
                        if (det.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (det.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        break;
                    case GroupListDetails gdt:
                        det = (RoleDetailAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(gdt.GetRoleDetailAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(gdt.GetRoleDetailPrint(role), null, true);
                        //this.EditAccessStr = this.GetEnumValueDesc(det.EditAccess, (int)det.EditAccess);
                        //this.PrintAccessStr = this.GetEnumValueDesc(det.PrintAccess, (int)det.PrintAccess);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
                        this._SelectedEditAccess = det.EditAccess;
                        this._SelectedPrintAccess = det.PrintAccess;
                        if (det.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (det.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        break;
                    case Document d:
                        var da = (RoleDocumentAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(d.GetRoleDocumentAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(d.GetRoleDocumentPrint(role), null, true);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumDocumentAccess>();
                        this._SelectedEditAccess = da.EditAccess;
                        this._SelectedPrintAccess = da.PrintAccess;
                        if (da.EditAccess != EnumDocumentAccess.D_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (da.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        break;
                    case GroupListDocuments gd:
                        da = (RoleDocumentAccess)ra.GetRoleAccess(role);
                        this.EditAccessStr = this.GetEnumValueDesc(gd.GetRoleDocumentAccess(role), null, true);
                        this.PrintAccessStr = this.GetEnumValueDesc(gd.GetRoleDocumentPrint(role), null, true);
                        //this.EditAccessStr = this.GetEnumValueDesc(da.EditAccess, (int)da.EditAccess);
                        //this.PrintAccessStr = this.GetEnumValueDesc(da.PrintAccess, (int)da.PrintAccess);
                        this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumDocumentAccess>();
                        this._SelectedEditAccess = da.EditAccess;
                        this._SelectedPrintAccess = da.PrintAccess;
                        if (da.EditAccess != EnumDocumentAccess.D_BY_PARENT)
                            this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.EditAccessIcon = new ControlTemplate();
                        //this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        if (da.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                            this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                        else
                            this.PrintAccessIcon = new ControlTemplate();
                        //this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            else if (node is IRoleGlobalSetting gra)
            {
                this.ListPrintAccess = VMHelpers.GetEnumComboBox<EnumPrintAccess>();
                this.EditAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                this.PrintAccessIcon = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
                string? s = null;
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
            }
        }
        // https://en.wikipedia.org/wiki/List_of_Unicode_characters#Arrows

        public IEnumerable ListEditAccess { get => _ListEditAccess; set => SetProperty(ref _ListEditAccess, value); }
        private IEnumerable _ListEditAccess;
        public object SelectedEditAccess
        {
            get => _SelectedEditAccess;
            set
            {
                if (_SelectedEditAccess != value)
                {
                    switch (node)
                    {
                        case Property p:
                            p.SetRoleAccess(this.role, (EnumPropertyAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumPropertyAccess)value, null, true);
                            break;
                        case GroupListProperties gp:
                            gp.SetRoleAccess(this.role, (EnumPropertyAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumPropertyAccess)value, (int)value);
                            break;
                        case Constant cnst:
                            cnst.SetRoleAccess(this.role, (EnumConstantAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumConstantAccess)value, null, true);
                            break;
                        case GroupListConstants gcnst:
                            gcnst.SetRoleAccess(this.role, (EnumConstantAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumConstantAccess)value, (int)value);
                            break;
                        case GroupConstantGroups gcnstg:
                            this.role.DefaultConstantEditAccessSettings = (EnumConstantAccess)value;
                            this.EditAccessStr = this.GetEnumValueDesc((EnumConstantAccess)value, (int)value);
                            break;
                        case Catalog c:
                            c.SetRoleAccess(this.role, (EnumCatalogDetailAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumCatalogDetailAccess)value, null, true);
                            break;
                        case CatalogFolder cf:
                            cf.SetRoleAccess(this.role, (EnumCatalogDetailAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumCatalogDetailAccess)value, (int)value);
                            break;
                        case Detail dt:
                            dt.SetRoleAccess(this.role, (EnumCatalogDetailAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumCatalogDetailAccess)value, (int)value);
                            break;
                        case GroupListDetails gdt:
                            gdt.SetRoleAccess(this.role, (EnumCatalogDetailAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumCatalogDetailAccess)value, (int)value);
                            break;
                        case GroupListCatalogs gc:
                            this.role.DefaultCatalogEditAccessSettings = (EnumCatalogDetailAccess)value;
                            this.EditAccessStr = this.GetEnumValueDesc((EnumCatalogDetailAccess)value, (int)value);
                            break;
                        case Document d:
                            d.SetRoleAccess(this.role, (EnumDocumentAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumDocumentAccess)value, (int)value);
                            break;
                        case GroupListDocuments gd:
                            gd.SetRoleAccess(this.role, (EnumDocumentAccess)value, null);
                            this.EditAccessStr = this.GetEnumValueDesc((EnumDocumentAccess)value, (int)value);
                            break;
                        case GroupDocuments:
                            this.role.DefaultDocumentEditAccessSettings = (EnumDocumentAccess)value;
                            this.EditAccessStr = this.GetEnumValueDesc((EnumDocumentAccess)value, (int)value);
                            break;
                        default:
                            break;
                    }
                    SetProperty(ref _SelectedEditAccess, value);
                }
            }
        }
        private object _SelectedEditAccess;
        public string EditAccessStr { get => _EditAccessStr; set => SetProperty(ref _EditAccessStr, value); }
        private string _EditAccessStr = string.Empty;
        public ControlTemplate EditAccessIcon { get => _EditAccessIcon; set => SetProperty(ref _EditAccessIcon, value); }
        private ControlTemplate _EditAccessIcon = new ControlTemplate();

        public IEnumerable ListPrintAccess { get => _ListPrintAccess; set => SetProperty(ref _ListPrintAccess, value); }
        private IEnumerable _ListPrintAccess;
        public EnumPrintAccess SelectedPrintAccess
        {
            get => _SelectedPrintAccess;
            set
            {
                if (_SelectedPrintAccess != value)
                {
                    this.PrintAccessStr = this.GetEnumValueDesc((EnumPrintAccess)value, (int)value);
                    switch (node)
                    {
                        case Property p:
                            p.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case GroupListProperties gp:
                            gp.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case Constant cnst:
                            cnst.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case GroupListConstants gcnst:
                            gcnst.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case GroupConstantGroups gcnstg:
                            this.role.DefaultConstantEditAccessSettings = (EnumConstantAccess)value;
                            break;
                        case Catalog c:
                            c.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case CatalogFolder cf:
                            cf.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case Detail dt:
                            dt.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case GroupListDetails gdt:
                            gdt.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case GroupListCatalogs gc:
                            this.role.DefaultCatalogEditAccessSettings = (EnumCatalogDetailAccess)value;
                            break;
                        case Document d:
                            d.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case GroupListDocuments gd:
                            gd.SetRoleAccess(this.role, null, (EnumPrintAccess)value);
                            break;
                        case GroupDocuments:
                            this.role.DefaultDocumentEditAccessSettings = (EnumDocumentAccess)value;
                            break;
                        default:
                            break;
                    }
                }
                SetProperty(ref _SelectedPrintAccess, value);
            }
        }
        private EnumPrintAccess _SelectedPrintAccess;
        public string PrintAccessStr { get => _PrintAccessStr; set => SetProperty(ref _PrintAccessStr, value); }
        private string _PrintAccessStr = string.Empty;
        public ControlTemplate PrintAccessIcon { get => _PrintAccessIcon; set => SetProperty(ref _PrintAccessIcon, value); }
        private ControlTemplate _PrintAccessIcon = new ControlTemplate();
    }
    //public class ModelNodeRoles : ITreeModel
    //{
    //    Model? model;
    //    public ITreeConfigNode Node { get; private set; }
    //    private static ConfigNodesCollection<Role> roles;
    //    public List<object?> ListRoleAccess { get; private set; }
    //    public ModelNodeRoles(ConfigNodesCollection<Role> roles, Model model)
    //    {
    //        this.model = model;
    //        this.Node = this.model;
    //        ModelNodeRoles.roles = roles;
    //        this.ListRoleAccess = new List<object?>();
    //    }
    //    public ModelNodeRoles(ITreeConfigNode node)
    //    {
    //        this.Node = node;
    //        this.ListRoleAccess = new List<object?>();
    //        if (node is IRoleAccess ra)
    //        {
    //            foreach (var role in ModelNodeRoles.roles)
    //                this.ListRoleAccess.Add(ra.GetRoleAccess(role));
    //        }
    //        else
    //        {
    //            for (int i = 0; i < ModelNodeRoles.roles.Count; i++)
    //                this.ListRoleAccess.Add(null);
    //        }
    //    }
    //    public IEnumerable GetChildren(object parent)
    //    {
    //        ITreeConfigNode node = parent == null ? this.Node : ((ModelNodeRoles)parent).Node;
    //        var res = new List<object>();
    //        foreach (var t in node.GetListChildren())
    //        {
    //            var tt = (ITreeConfigNode)t;
    //            if (parent == null && (tt.Name == "Common" || tt.Name == "Enumerations"))
    //                continue;
    //            res.Add(new ModelNodeRoles(tt));
    //        }
    //        return res;
    //    }
    //    public bool HasChildren(object parent)
    //    {
    //        var p = (ModelNodeRoles)parent;
    //        return p.Node.GetListChildren().Count > 0;
    //    }
    //}
}
