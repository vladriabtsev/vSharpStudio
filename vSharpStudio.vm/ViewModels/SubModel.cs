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
    public partial class SubModel : ISubModel
    {
        public static readonly string DefaultName = "SubModel";
        protected override void OnInitFromDto()
        {
            foreach (var t in this.ListGuids)
            {
                this.DicGuids[t] = null;
            }
        }
        public Dictionary<string, string> DicGuids = new Dictionary<string, string>();
        //[BrowsableAttribute(false)]
        //public Config Config
        //{
        //    set
        //    {
        //        _Config = value;
        //        NotifyPropertyChanged();
        //        if (this.Parent != null)
        //            this.Parent.Name = _Config.Name;
        //        //ValidateProperty();
        //    }
        //    get { return _Config; }
        //}

        //IConfig IBaseConfig.Config => _Config;

        //private Config _Config;

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
                    if (this.DicGuids.ContainsKey(t.Guid))
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
                    if (this.DicGuids.ContainsKey(t.Guid))
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
                    if (this.DicGuids.ContainsKey(t.Guid))
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
                    if (this.DicGuids.ContainsKey(t.Guid))
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
