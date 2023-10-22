using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class CatalogsRelationManyToManyTable : IParent
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = this.Name + mes;
        }
        partial void OnCreated()
        {
            this._Guid = System.Guid.NewGuid().ToString();
            this._RefCat1Guid = System.Guid.NewGuid().ToString();
            this._RefCat2Guid = System.Guid.NewGuid().ToString();
            Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        private void Init()
        {
        }
        partial void OnRefCat1GuidChanged()
        {
            if (this._RefCat2Guid != null)
            {
                this._Name = this.GetName();
            }
        }
        private string GetName()
        {
            Debug.Assert(this.Parent != null);
            Debug.Assert(this.Parent is GroupListCatalogs);
            var cfg = ((GroupListCatalogs)this.Parent).ParentModel.Cfg;
            Debug.Assert(cfg.DicNodes.ContainsKey(this._RefCat1Guid));
            string nameCat1 = ((Catalog)cfg.DicNodes[this._RefCat1Guid]).Name;
            Debug.Assert(cfg.DicNodes.ContainsKey(this._RefCat2Guid));
            string nameCat2 = ((Catalog)cfg.DicNodes[this._RefCat2Guid]).Name;
            Debug.Assert(nameCat1.CompareTo(nameCat2) != 0);
            if (nameCat1.CompareTo(nameCat2) < 0)
                return $"Many_to_many_{nameCat1}_{nameCat2}";
            else
                return $"Many_to_many_{nameCat2}_{nameCat1}";
        }
        partial void OnRefCat2GuidChanged()
        {
            if (this._RefCat1Guid != null)
            {
                this._Name = this.GetName();
            }
        }
    }
}
