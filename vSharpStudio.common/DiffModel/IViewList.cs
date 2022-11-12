using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public class ViewFormData
    {
        public ViewFormData(ViewTreeData? treeViewData, ViewListData? viewListData)
        {
            this.TreeViewData = treeViewData;
            this.ViewListData = viewListData;
        }
        public ViewTreeData? TreeViewData { get; private set; }
        public ViewListData? ViewListData { get; private set; }
    }
    public class ViewListData
    {
        public ViewListData(IProperty? propertyId, IProperty? propertyRefParent = null, IProperty? propertyIsFolder = null)
        {
            this.ListViewProperties = new List<IProperty>();
            this.PropertyId = propertyId;
            this.PropertyRefParent = propertyRefParent;
            this.PropertyIsFolder = propertyIsFolder;
        }
        public List<IProperty> ListViewProperties { get; private set; }
        public IProperty? PropertyId { get; private set; }
        public IProperty? PropertyRefParent { get; private set; }
        public IProperty? PropertyIsFolder { get; private set; }
    }
    public class ViewTreeData
    {
        public ViewTreeData(IProperty? propertyId, IProperty? propertyRefParent = null, IProperty? propertyIsFolder = null)
        {
            this.ListViewProperties = new List<IProperty>();
            this.PropertyId = propertyId;
            this.PropertyRefParent = propertyRefParent;
            this.PropertyIsFolder = propertyIsFolder;
        }
        public List<IProperty> ListViewProperties { get; private set; }
        public IProperty? PropertyId { get; private set; }
        public IProperty? PropertyRefParent { get; private set; }
        public IProperty? PropertyIsFolder { get; private set; }
    }
    public interface IViewList
    {
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
    }
}
