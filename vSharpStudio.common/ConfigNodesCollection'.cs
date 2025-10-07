using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class ConfigNodesCollection<T> : SortedObservableCollection<T>, IChildrenCollection
      where T : ITreeConfigNodeSortable
    {
        private readonly IConfig? cfg;
        private readonly ITreeConfigNode? parent;
        private readonly bool isUseDicNodes = false;
        public EnumSortingType SortingType { get; set; }

        public ConfigNodesCollection(ITreeConfigNode? parent)
        {
            this.isUseDicNodes = !(typeof(T).Name == typeof(IPluginGeneratorNodeSettings).Name);
            this.parent = parent;
            if (this.parent == null)
            {
            }
            else
                this.cfg = (IConfig)this.parent.Cfg;
        }
        public void Add(object item, int explicitPosition)
        {
#if DEBUG
            for (int i = 0; i < this.Count; ++i)
            {
                Debug.Assert(this[i].ExplicitSortingPosition != explicitPosition);
            }
#endif
            this.Add((T)item, explicitPosition);
        }
        public new void Add(T item, int explicitPosition)
        {
            Debug.Assert(this.cfg != null);
            //Debug.Assert(!this.cfg.DicNodes.ContainsKey(item.Guid));
#if DEBUG
            for (int i = 0; i < this.Count; ++i)
            {
                Debug.Assert(this[i].ExplicitSortingPosition != explicitPosition);
            }
#endif
            if (isUseDicNodes && this.cfg.IsInitialized)
                this.cfg.AddToDicNodes(item);
            item.Parent = this.parent;
            base.Add(item, explicitPosition);
            Debug.Assert(item.ExplicitSortingPosition > 0);
        }
        public void Add(object item, object? selected = null)
        {
            this.Add((T)item, (T?)selected);
        }
        public new void AddClone(T item)
        {
            base.AddClone(item);
        }
        /// <summary>
        /// Add T item after selected
        /// </summary>
        /// <param name="item"></param>
        /// <param name="selected"></param>
        public new void Add(T item, T? selected = default(T))
        {
            Debug.Assert(this.cfg != null);
            Debug.Assert(!this.cfg.DicNodes.ContainsKey(item.Guid));
            if (isUseDicNodes && this.cfg.IsInitialized)
                this.cfg.AddToDicNodes(item);
            item.Parent = this.parent;

            int explicitPosition = 0;
            if (selected == null) // add at end of list
            {
                if (this.Count > 0)
                {
                    for (int i = 0; i < this.Count; ++i)
                    {
                        explicitPosition = System.Math.Max(explicitPosition, this[i].ExplicitSortingPosition);
                    }
                }
            }
            else
            {
                if (SortingType == EnumSortingType.EXPLICIT) // add after selected
                {
                    var indx = this.IndexOf(selected);
                    explicitPosition = selected.ExplicitSortingPosition - 1;
                    for (int i = this.Count - 1; i >= indx; --i)
                    {
                        this[i].ExplicitSortingPosition = this[i].ExplicitSortingPosition + 1;
                    }
                }
                else // add at end of list, but will be sorted by name
                {
                    for (int i = 0; i < this.Count; ++i)
                    {
                        explicitPosition = System.Math.Max(explicitPosition, this[i].ExplicitSortingPosition);
                    }
                }
            }
            explicitPosition++;
#if DEBUG
            for (int i = 0; i < this.Count; ++i)
            {
                Debug.Assert(this[i].ExplicitSortingPosition != explicitPosition);
            }
#endif
            base.Add(item, explicitPosition);
            Debug.Assert(item.ExplicitSortingPosition > 0);
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            Debug.Assert(collection != null);
            Debug.Assert(this.cfg != null);
            foreach (T item in collection)
            {
                item.Parent = this.parent;
                if (isUseDicNodes && this.cfg.IsInitialized)
                    this.cfg.AddToDicNodes(item);
            }
            base.AddRange(collection);
        }
        public new bool Remove(T item)
        {
            Debug.Assert(this.cfg != null);
            if (isUseDicNodes)
            {
                this.cfg.RemoveFromDicNodes(item);
            }
            int indx = -1;
            foreach (var t in this)
            {
                indx++;
                if (t.Guid == item.Guid)
                {
                    base.RemoveAt(indx);
                    return true;
                }
            }
            return false;
        }
        #region IMoveUpDown
        public override bool CanUp(T current)
        {
            if (this.SortingType != EnumSortingType.EXPLICIT)
                return false;
            return base.CanUp(current);
        }
        public override bool CanDown(T current)
        {
            if (this.SortingType != EnumSortingType.EXPLICIT)
                return false;
            return base.CanDown(current);
        }
        #endregion IMoveUpDown
        public void Sort(EnumSortingType? sortType = null)
        {
            if (sortType != null)
            {
                this.SortingType = sortType.Value;
            }
            InternalSort();
        }
        protected override void InternalSort()
        {
            if (Items.Count > 1)
            {
                switch (this.SortingType)
                {
                    case EnumSortingType.EXPLICIT:
                        InternalSort(Items.OrderBy(t => t.ExplicitSortingPosition, Comparer<int>.Create((k1, k2) => k1.CompareTo(k2))));
                        break;
                    case EnumSortingType.ASCENDING:
                        InternalSort(Items.OrderBy(t => t.NameToCompare, Comparer<string>.Create((k1, k2) => k1.CompareTo(k2))));
                        break;
                    case EnumSortingType.DESCENDING:
                        InternalSort(Items.OrderByDescending(t => t.NameToCompare, Comparer<string>.Create((k1, k2) => k1.CompareTo(k2))));
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
