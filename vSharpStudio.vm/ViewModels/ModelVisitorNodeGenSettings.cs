using System;
using System.Collections.Generic;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ModelVisitorNodeGenSettings : ModelVisitorBase
    {
        Action<INodeGenSettings> _act = null;
        public void NodeGenSettingsApplyAction(IConfig curr, Action<INodeGenSettings> act)
        {
            _act = act;
            this.RunThroughConfig(curr.Model, null);
        }
        //protected override void Visit(IConfig c)
        //{
        //    if (c is INodeGenSettings)
        //        _act(c as INodeGenSettings);
        //}
        protected override void Visit(IConfigModel m)
        {
            if (m is INodeGenSettings)
                _act(m as INodeGenSettings);
        }
        protected override void Visit(IGroupListCommon m)
        {
            if (m is INodeGenSettings)
                _act(m as INodeGenSettings);
        }
        protected override void Visit(IGroupListConstants cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void Visit(IConstant cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void Visit(IGroupListEnumerations cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void Visit(IEnumeration en)
        {
            if (en is INodeGenSettings)
                _act(en as INodeGenSettings);
        }
        protected override void Visit(IEnumerationPair p)
        {
            if (p is INodeGenSettings)
                _act(p as INodeGenSettings);
        }
        protected override void Visit(IGroupListCatalogs cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void Visit(ICatalog ct)
        {
            if (ct is INodeGenSettings)
                _act(ct as INodeGenSettings);
        }
        protected override void Visit(IGroupDocuments cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void Visit(IGroupListDocuments cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void Visit(IDocument d)
        {
            if (d is INodeGenSettings)
                _act(d as INodeGenSettings);
        }
        protected override void Visit(IGroupListPropertiesTabs t)
        {
            if (t is INodeGenSettings)
                _act(t as INodeGenSettings);
        }
        protected override void Visit(IPropertiesTab t)
        {
            if (t is INodeGenSettings)
                _act(t as INodeGenSettings);
        }
        protected override void Visit(IGroupListProperties t)
        {
            if (t is INodeGenSettings)
                _act(t as INodeGenSettings);
        }
        protected override void Visit(IProperty p)
        {
            if (p is INodeGenSettings)
                _act(p as INodeGenSettings);
        }
        protected override void Visit(IReport p)
        {
            if (p is INodeGenSettings)
                _act(p as INodeGenSettings);
        }
        protected override void Visit(IForm p)
        {
            if (p is INodeGenSettings)
                _act(p as INodeGenSettings);
        }
    }
}
