using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ModelVisitorNodeGenSettings : ModelVisitorBase
    {
        Action<INodeGenSettings> _act = null;
        public void NodeGenSettingsApplyAction(IConfig curr, Action<INodeGenSettings> act)
        {
            Contract.Requires(curr != null);
            _act = act;
            this.RunThroughConfig(curr.Model, null);
        }
        //protected override void Visit(IConfig c)
        //{
        //    if (c is INodeGenSettings)
        //        _act(c as INodeGenSettings);
        //}
        //protected override void Visit(IConfigModel m)
        //{
        //    if (m is INodeGenSettings)
        //        _act(m as INodeGenSettings);
        //}
        protected override void BeginVisit(IGroupListCommon m)
        {
            if (m is INodeGenSettings)
                _act(m as INodeGenSettings);
        }
        protected override void BeginVisit(IGroupListConstants cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void BeginVisit(IConstant cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void BeginVisit(IGroupListEnumerations cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void BeginVisit(IEnumeration en)
        {
            if (en is INodeGenSettings)
                _act(en as INodeGenSettings);
        }
        protected override void BeginVisit(IEnumerationPair p)
        {
            if (p is INodeGenSettings)
                _act(p as INodeGenSettings);
        }
        protected override void BeginVisit(IGroupListCatalogs cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void BeginVisit(ICatalog ct)
        {
            if (ct is INodeGenSettings)
                _act(ct as INodeGenSettings);
        }
        protected override void BeginVisit(IGroupDocuments cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void BeginVisit(IGroupListDocuments cn)
        {
            if (cn is INodeGenSettings)
                _act(cn as INodeGenSettings);
        }
        protected override void BeginVisit(IDocument d)
        {
            if (d is INodeGenSettings)
                _act(d as INodeGenSettings);
        }
        protected override void BeginVisit(IGroupListPropertiesTabs t)
        {
            if (t is INodeGenSettings)
                _act(t as INodeGenSettings);
        }
        protected override void BeginVisit(IPropertiesTab t)
        {
            if (t is INodeGenSettings)
                _act(t as INodeGenSettings);
        }
        protected override void BeginVisit(IGroupListProperties t)
        {
            if (t is INodeGenSettings)
                _act(t as INodeGenSettings);
        }
        protected override void BeginVisit(IProperty p)
        {
            if (p is INodeGenSettings)
                _act(p as INodeGenSettings);
        }
        protected override void BeginVisit(IReport p)
        {
            if (p is INodeGenSettings)
                _act(p as INodeGenSettings);
        }
        protected override void BeginVisit(IForm p)
        {
            if (p is INodeGenSettings)
                _act(p as INodeGenSettings);
        }
    }
}
