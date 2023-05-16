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

namespace vSharpStudio.ViewModels
{
    // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/observableobject
    public class EditorRolesVm : ObservableObject, ITreeModel
    {
        Model? model;
        public ITreeConfigNode Node { get; private set; }
        private static ConfigNodesCollection<Role> roles;
        public List<EditorRoleBaseVm> ListRoleAccess { get; private set; }
        public EditorRolesVm(ConfigNodesCollection<Role> roles, Model model)
        {
            this.model = model;
            this.Node = this.model;
            EditorRolesVm.roles = roles;
            this.ListRoleAccess = new List<EditorRoleBaseVm>();
        }
        public EditorRolesVm(ITreeConfigNode node)
        {
            this.Node = node;
            this.ListRoleAccess = new List<EditorRoleBaseVm>();
            foreach (var role in EditorRolesVm.roles)
                this.ListRoleAccess.Add(new EditorRoleBaseVm(node, role));
        }
        public IEnumerable GetChildren(object parent)
        {
            ITreeConfigNode node = parent == null ? this.Node : ((EditorRolesVm)parent).Node;
            var res = new List<object>();
            foreach (var t in node.GetListChildren())
            {
                var tt = (ITreeConfigNode)t;
                if (parent == null && (tt.Name == "Common" || tt.Name == "Enumerations"))
                    continue;
                res.Add(new EditorRolesVm(tt));
            }
            return res;
        }
        public bool HasChildren(object parent)
        {
            var p = (EditorRolesVm)parent;
            return p.Node.GetListChildren().Count > 0;
        }
    }
    //public class EditorRoleVm : ObservableObject
    //{
    //    ITreeConfigNode node;
    //    public EditorRoleVm(ITreeConfigNode node, Role role)
    //    {
    //        this.node = node;
    //        this.Update(node, role);
    //    }
    //    private string GetEnumValueDesc<TEnum>(TEnum val, int? v, bool isObjectUnderRole = false)
    //    {
    //        if (v.HasValue && v.Value == 0 && !isObjectUnderRole)
    //            return string.Empty;
    //        MemberInfo? memberInfo = typeof(TEnum).GetMember(val.ToString()).FirstOrDefault();
    //        Debug.Assert(memberInfo != null);
    //        DescriptionAttribute? attribute = (DescriptionAttribute)memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
    //        Debug.Assert(attribute != null);
    //        var s = attribute.Description;
    //        return s == null ? string.Empty : s;
    //    }
    //    private void SetVisibility(int edit, EnumPrintAccess pr, bool isFromParent = false)
    //    {
    //        if (edit == 0)
    //        {
    //            if (isFromParent)
    //            {
    //                this.BtnEditAccess.IconControlTemplate = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
    //                this.BtnEditAccess.ToolTipText = "Role EDIT access from parent nodes";
    //            }
    //            else
    //            {
    //                this.BtnEditAccess.IconControlTemplate = (ControlTemplate)Application.Current.FindResource("iconDownload");
    //                this.BtnEditAccess.ToolTipText = "Role EDIT access is set to get from parent nodes";
    //            }
    //        }
    //        else
    //        {
    //            this.BtnEditAccess.IconControlTemplate = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
    //            this.BtnEditAccess.ToolTipText = "Role EDIT access is set explicitly for this node";
    //        }
    //        if (pr == EnumPrintAccess.PR_BY_PARENT)
    //        {
    //            if (isFromParent)
    //            {
    //                this.BtnPrintAccess.IconControlTemplate = (ControlTemplate)Application.Current.FindResource("iconRedirectedRequest");
    //                this.BtnPrintAccess.ToolTipText = "Role PRINT access from parent nodes";
    //            }
    //            else
    //            {
    //                this.BtnPrintAccess.IconControlTemplate = (ControlTemplate)Application.Current.FindResource("iconDownload");
    //                this.BtnPrintAccess.ToolTipText = "Role PRINT access is set to get from parent nodes";
    //            }
    //        }
    //        else
    //        {
    //            this.BtnPrintAccess.IconControlTemplate = (ControlTemplate)Application.Current.FindResource("iconCustomActionEditor");
    //            this.BtnPrintAccess.ToolTipText = "Role PRINT access is set explicitly for this node";
    //        }
    //    }
    //    public void Update(ITreeConfigNode node, Role role)
    //    {
    //        if (node is IRoleAccess ra)
    //        {
    //            this.ListPrintAccess = VMHelpers.GetEnumComboBox<EnumPrintAccess>();
    //            string? s = null;
    //            switch (node)
    //            {
    //                case Property p:
    //                    var pa = (RolePropertyAccess)ra.GetRoleAccess(role);
    //                    var pa2 = p.GetRolePropertyAccess(role.Guid);
    //                    this.EditAccessStr = this.GetEnumValueDesc(pa2, null, true);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(p.GetRolePropertyPrint(role.Guid), null, true);
    //                    this.SetVisibility((int)pa.EditAccess, pa.PrintAccess, true);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumPropertyAccess>();
    //                    this.SelectedEditAccess = pa.EditAccess;
    //                    this.SelectedPrintAccess = pa.PrintAccess;
    //                    break;
    //                case GroupListProperties:
    //                    pa = (RolePropertyAccess)ra.GetRoleAccess(role);
    //                    this.EditAccessStr = this.GetEnumValueDesc(pa.EditAccess, (int)pa.EditAccess);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(pa.PrintAccess, (int)pa.PrintAccess);
    //                    this.SetVisibility((int)pa.EditAccess, pa.PrintAccess);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumPropertyAccess>();
    //                    this.SelectedEditAccess = pa.EditAccess;
    //                    this.SelectedPrintAccess = pa.PrintAccess;
    //                    break;
    //                case Constant cnst:
    //                    var cnsta = (RoleConstantAccess)ra.GetRoleAccess(role);
    //                    this.EditAccessStr = this.GetEnumValueDesc(cnst.GetRoleConstantAccess(role.Guid), null, true);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(cnst.GetRoleConstantPrint(role.Guid), null, true);
    //                    this.SetVisibility((int)cnsta.EditAccess, cnsta.PrintAccess, true);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumConstantAccess>();
    //                    this.SelectedEditAccess = cnsta.EditAccess;
    //                    this.SelectedPrintAccess = cnsta.PrintAccess;
    //                    break;
    //                case GroupListConstants:
    //                    cnsta = (RoleConstantAccess)ra.GetRoleAccess(role);
    //                    this.EditAccessStr = this.GetEnumValueDesc(cnsta.EditAccess, (int)cnsta.EditAccess);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(cnsta.PrintAccess, (int)cnsta.PrintAccess);
    //                    this.SetVisibility((int)cnsta.EditAccess, cnsta.PrintAccess);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumConstantAccess>();
    //                    this.SelectedEditAccess = cnsta.EditAccess;
    //                    this.SelectedPrintAccess = cnsta.PrintAccess;
    //                    break;
    //                case GroupConstantGroups gcnsta:
    //                    cnsta = (RoleConstantAccess)ra.GetRoleAccess(role);
    //                    var cnste = gcnsta.GetRoleConstantAccess(role.Guid);
    //                    this.EditAccessStr = this.GetEnumValueDesc(cnste, (int)cnste);
    //                    var cnstp = gcnsta.GetRoleConstantPrint(role.Guid);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(cnstp, (int)cnstp);
    //                    this.SetVisibility((int)cnste, cnstp);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumConstantAccess>();
    //                    this.SelectedEditAccess = cnsta.EditAccess;
    //                    this.SelectedPrintAccess = cnsta.PrintAccess;
    //                    break;
    //                case Catalog c:
    //                    var ca = (RoleCatalogAccess)ra.GetRoleAccess(role);
    //                    this.EditAccessStr = this.GetEnumValueDesc(c.GetRoleCatalogAccess(role.Guid), null, true);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(c.GetRoleCatalogPrint(role.Guid), null, true);
    //                    this.SetVisibility((int)ca.EditAccess, ca.PrintAccess, true);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
    //                    this.SelectedEditAccess = ca.EditAccess;
    //                    this.SelectedPrintAccess = ca.PrintAccess;
    //                    break;
    //                case CatalogFolder cf:
    //                    ca = (RoleCatalogAccess)ra.GetRoleAccess(role);
    //                    this.EditAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogAccess(role.Guid), (int)ca.EditAccess, true);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(cf.GetRoleCatalogPrint(role.Guid), (int)ca.PrintAccess, true);
    //                    this.SetVisibility((int)ca.EditAccess, ca.PrintAccess, true);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
    //                    this.SelectedEditAccess = ca.EditAccess;
    //                    this.SelectedPrintAccess = ca.PrintAccess;
    //                    break;
    //                case GroupListCatalogs gc:
    //                    ca = (RoleCatalogAccess)ra.GetRoleAccess(role);
    //                    var ce = gc.GetRoleCatalogAccess(role.Guid);
    //                    this.EditAccessStr = this.GetEnumValueDesc(ce, (int)ce);
    //                    var cp = gc.GetRoleCatalogPrint(role.Guid);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(cp, (int)cp);
    //                    this.SetVisibility((int)ce, cp);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
    //                    this.SelectedEditAccess = ca.EditAccess;
    //                    this.SelectedPrintAccess = ca.PrintAccess;
    //                    break;
    //                case Detail dt:
    //                    var det = (RoleDetailAccess)ra.GetRoleAccess(role);
    //                    this.EditAccessStr = this.GetEnumValueDesc(dt.GetRoleDetailAccess(role.Guid), null, true);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(dt.GetRoleDetailPrint(role.Guid), null, true);
    //                    this.SetVisibility((int)det.EditAccess, det.PrintAccess, true);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
    //                    this.SelectedEditAccess = det.EditAccess;
    //                    this.SelectedPrintAccess = det.PrintAccess;
    //                    break;
    //                case GroupListDetails:
    //                    det = (RoleDetailAccess)ra.GetRoleAccess(role);
    //                    this.EditAccessStr = this.GetEnumValueDesc(det.EditAccess, (int)det.EditAccess);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(det.PrintAccess, (int)det.PrintAccess);
    //                    this.SetVisibility((int)det.EditAccess, det.PrintAccess);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumCatalogDetailAccess>();
    //                    this.SelectedEditAccess = det.EditAccess;
    //                    this.SelectedPrintAccess = det.PrintAccess;
    //                    break;
    //                case Document d:
    //                    var da = (RoleDocumentAccess)ra.GetRoleAccess(role);
    //                    this.EditAccessStr = this.GetEnumValueDesc(d.GetRoleDocumentAccess(role.Guid), null, true);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(d.GetRoleDocumentPrint(role.Guid), null, true);
    //                    this.SetVisibility((int)da.EditAccess, da.PrintAccess, true);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumDocumentAccess>();
    //                    this.SelectedEditAccess = da.EditAccess;
    //                    this.SelectedPrintAccess = da.PrintAccess;
    //                    break;
    //                case GroupListDocuments:
    //                    da = (RoleDocumentAccess)ra.GetRoleAccess(role);
    //                    this.EditAccessStr = this.GetEnumValueDesc(da.EditAccess, (int)da.EditAccess);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(da.PrintAccess, (int)da.PrintAccess);
    //                    this.SetVisibility((int)da.EditAccess, da.PrintAccess);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumDocumentAccess>();
    //                    this.SelectedEditAccess = da.EditAccess;
    //                    this.SelectedPrintAccess = da.PrintAccess;
    //                    break;
    //                case GroupDocuments gd:
    //                    da = (RoleDocumentAccess)ra.GetRoleAccess(role);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumDocumentAccess>();
    //                    var de = gd.GetRoleDocumentAccess(role.Guid);
    //                    this.EditAccessStr = this.GetEnumValueDesc(de, (int)de);
    //                    var dp = gd.GetRoleDocumentPrint(role.Guid);
    //                    this.PrintAccessStr = this.GetEnumValueDesc(dp, (int)dp);
    //                    this.SetVisibility((int)de, dp);
    //                    this.ListEditAccess = VMHelpers.GetEnumComboBox<EnumDocumentAccess>();
    //                    this.SelectedEditAccess = da.EditAccess;
    //                    this.SelectedPrintAccess = da.PrintAccess;
    //                    break;
    //                default:
    //                    throw new NotImplementedException();
    //            }
    //        }
    //    }
    //    // https://en.wikipedia.org/wiki/List_of_Unicode_characters#Arrows

    //    public IEnumerable ListEditAccess { get => _ListEditAccess; set => SetProperty(ref _ListEditAccess, value); }
    //    private IEnumerable _ListEditAccess;
    //    public object SelectedEditAccess
    //    {
    //        get => _SelectedEditAccess;
    //        set
    //        {
    //            switch (node)
    //            {
    //                case GroupConstantGroups gcnsta:
    //                    break;
    //                case GroupListCatalogs gc:
    //                    break;
    //                case GroupDocuments gd:
    //                    break;
    //                default:
    //                    break;
    //            }
    //            SetProperty(ref _SelectedEditAccess, value);
    //        }
    //    }
    //    private object _SelectedEditAccess;
    //    public string EditAccessStr { get => _EditAccessStr; set => SetProperty(ref _EditAccessStr, value); }
    //    private string _EditAccessStr = string.Empty;

    //    public IEnumerable ListPrintAccess { get => _ListPrintAccess; set => SetProperty(ref _ListPrintAccess, value); }
    //    private IEnumerable _ListPrintAccess;
    //    public EnumPrintAccess SelectedPrintAccess { get => _SelectedPrintAccess; set => SetProperty(ref _SelectedPrintAccess, value); }
    //    private EnumPrintAccess _SelectedPrintAccess;
    //    public string PrintAccessStr { get => _PrintAccessStr; set => SetProperty(ref _PrintAccessStr, value); }
    //    private string _PrintAccessStr = string.Empty;
    //}
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
