using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public static class ModelExt
    {
        public static bool? CheckIsIncluded(this Model m, ITreeConfigNode node)
        {
            return m.CheckIsSelected(node);
        }
        public static bool? CheckIsIncluded(this ITreeConfigNode node, Model m)
        {
            return m.CheckIsSelected(node);
        }
    }
    public partial class Model : IModel, IIncludeDefaultPolicy
    {
        public static readonly string DefaultName = "Model";
        private Config cfg = null;
        protected override void OnInitFromDto()
        {
            foreach (var t in this.ListObjectInclusionRecords)
            {
                this.DicInclusionRecordObjectGuids[t.Guid] = t;
            }
        }
        [BrowsableAttribute(false)]
        public Dictionary<string, ObjectInclusionRecord> DicInclusionRecordObjectGuids = new Dictionary<string, ObjectInclusionRecord>();
        partial void OnEnumDefaultInclusionChanging(EnumIncludeDefaultPolicy from, EnumIncludeDefaultPolicy to)
        {
            if (Config.IsLoading)
                return;
            if (cfg == null)
                cfg = (Config)this.Parent.Parent;
            var m = cfg.Model;
            ChildrenInclusionChange(m);
        }

        private static void ChildrenInclusionChange(ConfigModel m)
        {
            throw new NotImplementedException();
            if (m.HasChildren(m))
                foreach (var t in m.GetChildren(m))
                {
                }
        }
        #region for model object nodes
        public bool CheckIsSelected(ITreeConfigNode obj)
        {
            if (this.DicInclusionRecordObjectGuids.ContainsKey(obj.Guid))
            {
                return this.DicInclusionRecordObjectGuids[obj.Guid].Inclusion;
            }
            else
            {
                ITreeConfigNode p = obj.Parent;
                while (p != null)
                {
                    if (this.DicInclusionRecordObjectGuids.ContainsKey(p.Guid))
                        return this.DicInclusionRecordObjectGuids[p.Guid].Inclusion;
                    p = p.Parent;
                }
            }
            switch (this.EnumDefaultInclusion)
            {
                case EnumIncludeDefaultPolicy.INCLUDE:
                    return true;
                case EnumIncludeDefaultPolicy.EXCLUDE:
                    return false;
            }
            throw new NotSupportedException();
        }

        public void IsSelectedChanged(string guid, bool prev, bool isSelected)
        {
            if (cfg == null)
                cfg = (Config)this.Parent.Parent;
#if DEBUG
            if (cfg.DicNodes.ContainsKey(guid))
                throw new ArgumentException();
#endif
            var n = cfg.DicNodes[guid];
            var isPrevSel = CheckIsSelected(n);
            if (isSelected)
            {
                //if (!isPrevSel)

            }
            else
            {

            }
            //if (this.DicInclusionRecordObjectGuids.ContainsKey(guid))
            //{
            //    if (isSelected == null)
            //    {
            //        var r = this.DicInclusionRecordObjectGuids[guid];
            //        this.ListObjectInclusionRecords.Remove(r);
            //        this.DicInclusionRecordObjectGuids.Remove(guid);
            //    }
            //    else
            //        this.DicInclusionRecordObjectGuids[guid].Inclusion = isSelected;
            //}
            //else
            //{
            //    if (isSelected != null)
            //    {
            //        var r = new ObjectInclusionRecord() { Inclusion = isSelected, Guid = guid };
            //        this.DicInclusionRecordObjectGuids[guid] = r;
            //        this.ListObjectInclusionRecords.Add(r);
            //    }
            //}
        }

        #endregion for model object nodes

        [BrowsableAttribute(false)]
        public List<ModelRow> ListObjects
        {
            get
            {
                _ListObjects.Clear();
                ITreeConfigNode p = this;
                while (p.Parent != null)
                    p = p.Parent;
                Config m = (Config)p;
                List<ModelRow> lst = new List<ModelRow>();
                bool is_all = true;
                string grp = "Constants";
                foreach (var t in m.Model.GroupConstants.ListConstants)
                {
                    if (this.DicInclusionRecordObjectGuids.ContainsKey(t.Guid))
                    {
                        lst.Add(new ModelRow(this, grp, t.Name, t.Guid, true));
                    }
                    else
                    {
                        lst.Add(new ModelRow(this, grp, t.Name, t.Guid, false));
                        is_all = false;
                    }
                }
                _ListObjects.Add(new ModelRow() { SubModel = this, GroupName = grp, ListSubModelRow = lst, IsIncluded = is_all && (lst.Count > 0) });
                _ListObjects.AddRange(lst);

                lst = new List<ModelRow>();
                is_all = true;
                grp = "Enumerations";
                foreach (var t in m.Model.GroupEnumerations.ListEnumerations)
                {
                    if (this.DicInclusionRecordObjectGuids.ContainsKey(t.Guid))
                    {
                        lst.Add(new ModelRow(this, grp, t.Name, t.Guid, true));
                    }
                    else
                    {
                        lst.Add(new ModelRow(this, grp, t.Name, t.Guid, false));
                        is_all = false;
                    }
                }
                _ListObjects.Add(new ModelRow(this, grp, lst, is_all && (lst.Count > 0)));
                _ListObjects.AddRange(lst);

                lst = new List<ModelRow>();
                is_all = true;
                grp = "Catalogs";
                foreach (var t in m.Model.GroupCatalogs.ListCatalogs)
                {
                    if (this.DicInclusionRecordObjectGuids.ContainsKey(t.Guid))
                    {
                        lst.Add(new ModelRow(this, grp, t.Name, t.Guid, true));
                    }
                    else
                    {
                        lst.Add(new ModelRow(this, grp, t.Name, t.Guid, false));
                        is_all = false;
                    }
                }
                _ListObjects.Add(new ModelRow(this, grp, lst, is_all && (lst.Count > 0)));
                _ListObjects.AddRange(lst);

                lst = new List<ModelRow>();
                is_all = true;
                grp = "Documents";
                foreach (var t in m.Model.GroupDocuments.GroupListDocuments.ListDocuments)
                {
                    if (this.DicInclusionRecordObjectGuids.ContainsKey(t.Guid))
                    {
                        lst.Add(new ModelRow(this, grp, t.Name, t.Guid, true));
                    }
                    else
                    {
                        lst.Add(new ModelRow(this, grp, t.Name, t.Guid, false));
                        is_all = false;
                    }
                }
                _ListObjects.Add(new ModelRow(this, grp, lst, is_all && (lst.Count > 0)));
                _ListObjects.AddRange(lst);

                return _ListObjects;
            }
        }
        private List<ModelRow> _ListObjects = new List<ModelRow>();
    }
}
