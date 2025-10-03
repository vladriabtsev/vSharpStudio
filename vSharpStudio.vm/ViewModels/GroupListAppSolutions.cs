using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListAppSolutions : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Slns:{ListAppSolutions.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Config ParentConfig { get { Debug.Assert(this.Parent != null); return (Config)this.Parent; } }
        [Browsable(false)]
        public IConfig ParentConfigI { get { Debug.Assert(this.Parent != null); return (IConfig)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentConfig.Children;
        }
        #endregion ITree

        public new ConfigNodesCollection<AppSolution> Children { get { return this.ListAppSolutions; } }

        partial void OnCreated()
        {
            this.IsEditable = false;
            //this.DefaultDb.Parent = this;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        partial void OnSortTypeChanged() { this.ListAppSolutions.Sort((int)this.SortType); }
        private void Init()
        {
            OnSortTypeChanged();
            this.ListAppSolutions.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListAppSolutions.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListAppSolutions.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            this._Name = Defaults.ApplicationGroupName;
        }
        //[PropertyOrderAttribute(11)]
        //[ExpandableObjectAttribute()]
        //[ReadOnly(true)]
        //[DisplayName("Settings")]
        //[Description("Default group generators settings. Group generators are working together")]
        //public object DynamicMainSettings
        //{
        //    get
        //    {
        //        return this._DynamicMainSettings;
        //    }
        //    set
        //    {
        //        if (this._DynamicMainSettings != value)
        //        {
        //            this._DynamicMainSettings = value;
        //            this.NotifyPropertyChanged();
        //            this.ValidateProperty();
        //        }
        //    }
        //}
        //private object _DynamicMainSettings;

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public void AddAppSolution(AppSolution node)
        {
            this.NodeAddNewSubNode(node);
        }
        public AppSolution AddAppSolution(string name, string appSolutionPath)
        {
            appSolutionPath = Path.GetFullPath(appSolutionPath);
            AppSolution node = new AppSolution(this, name)
            {
                //RelativeAppSolutionPath = this.GetRelativeToConfigDiskPath(appSolutionPath)
                RelativeAppSolutionPath = appSolutionPath
            };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            AppSolution node = null!;
            if (node_impl == null)
            {
                node = new AppSolution(this);
            }
            else
            {
                node = (AppSolution)node_impl;
            }
            if (this.ListAppSolutions.Count > 0)
            {
                node.SortingValue = this.ListAppSolutions[this.ListAppSolutions.Count - 1].SortingValue + 1;
            }
            else
            {
                node.SortingValue = 1;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.AppSolutionName, node, this.ListAppSolutions);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            return [.. lst];
        }
        public string? TableNameValidation(string name)
        {
            foreach (var t in this.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (ttt.DynamicGeneratorSettings is IDbNamesValidator lim)
                        {
                            var res = lim.TableNameValidation(name);
                            if (res != null)
                            {
                                return $"{res} Sln:{t.Name}, Prj:{tt.Name}, Gen:{ttt.Name}";
                            }
                        }
                    }
                }
            }
            return null;
        }
        public string? FieldNameValidation(string name)
        {
            foreach (var t in this.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (ttt.DynamicGeneratorSettings is IDbNamesValidator lim)
                        {
                            var res = lim.FieldNameValidation(name);
                            if (res != null)
                            {
                                return $"{res} Sln:{t.Name}, Prj:{tt.Name}, Gen:{ttt.Name}";
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
