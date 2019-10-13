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
    public partial class SubModel : ISubModel, IIncludeDefaultPolicy
    {
        public static readonly string DefaultName = "SubModel";
        protected override void OnInitFromDto()
        {
            foreach (var t in this.ListObjectInclusionRecords)
            {
                if (t.Inclusion.HasValue)
                {
                    this.DicInclusionRecordObjectGuids[t.Guid] = t;
                }
            }
        }
        [BrowsableAttribute(false)]
        public Dictionary<string, ObjectInclusionRecord> DicInclusionRecordObjectGuids = new Dictionary<string, ObjectInclusionRecord>();

        #region for model object nodes
        internal bool? CheckIsSelected(ITreeConfigNode obj)
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

        internal void IsSelectedChanged(string guid, bool? prev, bool? isSelected)
        {
            if (this.DicInclusionRecordObjectGuids.ContainsKey(guid))
            {
                if (isSelected == null)
                {
                    this.DicInclusionRecordObjectGuids.Remove(guid);
                }
                else
                    this.DicInclusionRecordObjectGuids[guid].Inclusion = isSelected;
            }
            else
            {
                if (isSelected != null)
                    this.DicInclusionRecordObjectGuids[guid].Inclusion = isSelected;
            }
        }

        #endregion for model object nodes

        [BrowsableAttribute(false)]
        public List<SubModelRow> ListObjects
        {
            get
            {
                _ListObjects.Clear();
                ITreeConfigNode p = this;
                while (p.Parent != null)
                    p = p.Parent;
                Config m = (Config)p;
                List<SubModelRow> lst = new List<SubModelRow>();
                bool is_all = true;
                string grp = "Constants";
                foreach (var t in m.Model.GroupConstants.ListConstants)
                {
                    if (this.DicInclusionRecordObjectGuids.ContainsKey(t.Guid))
                    {
                        lst.Add(new SubModelRow(this, grp, t.Name, t.Guid, true));
                    }
                    else
                    {
                        lst.Add(new SubModelRow(this, grp, t.Name, t.Guid, false));
                        is_all = false;
                    }
                }
                _ListObjects.Add(new SubModelRow() { SubModel = this, GroupName = grp, ListSubModelRow = lst, IsIncluded = is_all && (lst.Count > 0) });
                _ListObjects.AddRange(lst);

                lst = new List<SubModelRow>();
                is_all = true;
                grp = "Enumerations";
                foreach (var t in m.Model.GroupEnumerations.ListEnumerations)
                {
                    if (this.DicInclusionRecordObjectGuids.ContainsKey(t.Guid))
                    {
                        lst.Add(new SubModelRow(this, grp, t.Name, t.Guid, true));
                    }
                    else
                    {
                        lst.Add(new SubModelRow(this, grp, t.Name, t.Guid, false));
                        is_all = false;
                    }
                }
                _ListObjects.Add(new SubModelRow(this, grp, lst, is_all && (lst.Count > 0)));
                _ListObjects.AddRange(lst);

                lst = new List<SubModelRow>();
                is_all = true;
                grp = "Catalogs";
                foreach (var t in m.Model.GroupCatalogs.ListCatalogs)
                {
                    if (this.DicInclusionRecordObjectGuids.ContainsKey(t.Guid))
                    {
                        lst.Add(new SubModelRow(this, grp, t.Name, t.Guid, true));
                    }
                    else
                    {
                        lst.Add(new SubModelRow(this, grp, t.Name, t.Guid, false));
                        is_all = false;
                    }
                }
                _ListObjects.Add(new SubModelRow(this, grp, lst, is_all && (lst.Count > 0)));
                _ListObjects.AddRange(lst);

                lst = new List<SubModelRow>();
                is_all = true;
                grp = "Documents";
                foreach (var t in m.Model.GroupDocuments.GroupListDocuments.ListDocuments)
                {
                    if (this.DicInclusionRecordObjectGuids.ContainsKey(t.Guid))
                    {
                        lst.Add(new SubModelRow(this, grp, t.Name, t.Guid, true));
                    }
                    else
                    {
                        lst.Add(new SubModelRow(this, grp, t.Name, t.Guid, false));
                        is_all = false;
                    }
                }
                _ListObjects.Add(new SubModelRow(this, grp, lst, is_all && (lst.Count > 0)));
                _ListObjects.AddRange(lst);

                return _ListObjects;
            }
        }
        private List<SubModelRow> _ListObjects = new List<SubModelRow>();
    }
}
