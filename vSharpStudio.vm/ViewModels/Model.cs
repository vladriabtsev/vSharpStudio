using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public static class ModelExt
    {
        public static bool? CheckIsIncluded(this Model m, ITreeConfigNode node)
        {
            return m.CheckIsIncluded(node);
        }
        public static bool? CheckIsIncluded(this ITreeConfigNode node, Model m)
        {
            return m.CheckIsIncluded(node);
        }
    }
    public partial class Model : IModel, IIncludeDefaultPolicy
    {
        public static readonly string DefaultName = "Model";
        private Config cfg = null;
        partial void OnInitBegin()
        {
            this.DicInclusionRecordObjectGuids = new DictionaryExt<string, ObjectInclusionRecord>((key, val) =>
            {
#if DEBUG
                bool isFound = false;
                foreach(var t in this.ListObjectInclusionRecords)
                {
                    if (t.Guid==key)
                    {
                        isFound = true;
                        break;
                    }
                }
                if (isFound)
                    throw new Exception();
#endif
                this.ListObjectInclusionRecords.Add(val);
            }, (key, val) =>
            {
                int i = 0;
                foreach (var t in this.ListObjectInclusionRecords)
                {
                    if (t.Guid == key)
                    {
                        this.ListObjectInclusionRecords.RemoveAt(i);
                        return;
                    }
                    i++;
                }
#if DEBUG
                throw new Exception();
#endif
            }, () =>
            {
                this.ListObjectInclusionRecords.Clear();
            });
        }
        partial void OnInit()
        {
            cfg = (Config)this.Parent.Parent;
        }
        protected override void OnInitFromDto()
        {
            foreach (var t in this.ListObjectInclusionRecords)
            {
                this.DicInclusionRecordObjectGuids[t.Guid] = t;
            }
            this.DicInclusionRecordObjectGuids.IsActivateActions = true;
        }
        [BrowsableAttribute(false)]
        public DictionaryExt<string, ObjectInclusionRecord> DicInclusionRecordObjectGuids;
        object lock_object = new object();
        EnumIncludeDefaultPolicy from, to;
        partial void OnEnumDefaultInclusionChanging(EnumIncludeDefaultPolicy from, EnumIncludeDefaultPolicy to)
        {
            if (Config.IsLoading)
                return;
            if (cfg == null)
                cfg = (Config)this.Parent.Parent;
            var m = cfg.Model;
            lock (lock_object)
            {
                this.from = from;
                this.to = to;
                ModelChildrenDefaultInclusionChange(m);
            }
        }

        private void ModelChildrenDefaultInclusionChange(ConfigModel m)
        {
            if (m.HasChildren(m))
            {
                InclusionChange(m, this.to == EnumIncludeDefaultPolicy.INCLUDE, null);
            }
        }
        private void InclusionChange(ITreeConfigNode node, bool isParentIncluded, bool? isInclude)
        {
            if (node.IsIncludableInModels) // only nodes which inclusion can be editable
            {
                ObjectInclusionRecord r = null;
                this.DicInclusionRecordObjectGuids.TryGetValue(node.Guid, out r);
                var isWasIncluded = this.CheckIsIncluded(node);
                if (isInclude.HasValue)
                {
                    if (isInclude.Value)
                    {
                        if (isParentIncluded)
                        {
                            if (r != null)
                            {
                                this.DicInclusionRecordObjectGuids.Remove(node.Guid);
                            }
                        }
                        else
                        {
                            if (r == null)
                            {
                                r = new ObjectInclusionRecord() { Guid = node.Guid };
                                this.DicInclusionRecordObjectGuids[node.Guid] = r; ;
                            }
                            r.Inclusion = true;
                        }
                    }
                    else
                    {
                        if (isParentIncluded)
                        {
                            if (r == null)
                            {
                                r = new ObjectInclusionRecord() { Guid = node.Guid };
                                this.DicInclusionRecordObjectGuids[node.Guid] = r; ;
                            }
                            r.Inclusion = false;
                        }
                        else
                        {
                            if (r != null)
                            {
                                this.DicInclusionRecordObjectGuids.Remove(node.Guid);
                            }
                        }
                    }

                }
                else
                {
                    if (isWasIncluded)
                    {
                        if (isParentIncluded) // will be included implicitly
                        {
                            if (r != null)
                            {
                                this.DicInclusionRecordObjectGuids.Remove(node.Guid);
                            }
                        }
                        else // need to be included explicitly
                        {
                            if (r == null)
                            {
                                r = new ObjectInclusionRecord() { Guid = node.Guid };
                                this.DicInclusionRecordObjectGuids[node.Guid] = r; ;
                            }
                            r.Inclusion = true;
                        }
                    }
                    else
                    {
                        if (isParentIncluded) // need to be excluded explicitly
                        {
                            if (r == null)
                            {
                                r = new ObjectInclusionRecord() { Guid = node.Guid };
                                this.DicInclusionRecordObjectGuids[node.Guid] = r; ;
                            }
                            r.Inclusion = false;
                        }
                        else  // will be excluded implicitly
                        {
                            if (r != null)
                            {
                                this.DicInclusionRecordObjectGuids.Remove(node.Guid);
                            }
                        }
                    }
                }
                if (node.HasChildren(node))
                {
                    foreach (var t in node.GetChildren(node))
                    {
                        InclusionChange((ITreeConfigNode)t, isWasIncluded, null);
                    }
                }
            }
#if DEBUG
            else
            {
                if (this.DicInclusionRecordObjectGuids.ContainsKey(node.Guid))
                {
                    throw new NotSupportedException("Not supported behaviour trap");
                }
            }
#endif
        }
        #region for model object nodes
        public bool CheckIsIncluded(ITreeConfigNode obj)
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

        public void OnIsIncludedChanging(ITreeConfigNode node, bool isIncluded)
        {
            if (cfg == null)
                cfg = (Config)this.Parent.Parent;

            ITreeConfigNode p = null;
            p = node;
            bool isParentIncluded = false;
            while (p.Parent != null)
            {
                p = p.Parent;
                if (p.IsIncludableInModels)
                {
                    isParentIncluded = CheckIsIncluded(p);
                    break;
                }
            }
            InclusionChange(node, isParentIncluded, isIncluded);
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
