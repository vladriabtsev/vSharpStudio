using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.Migration
{
    public class ChangesConstants
    {
        public List<Constant> ListAdded { get; set; }

        public List<Constant> ListDeleted { get; set; }

        public List<Constant> ListRenamed { get; set; }

        public List<Constant> ListChanged { get; set; }
    }

    public class ChangesEnumerations
    {
        public List<Enumeration> ListAdded { get; set; }

        public List<Enumeration> ListDeleted { get; set; }

        public List<Enumeration> ListRenamed { get; set; }

        public List<Enumeration> ListChanged { get; set; }
    }

    public class ChangesEnumerationPairs
    {
        public List<EnumerationPair> ListAdded { get; set; }

        public List<EnumerationPair> ListDeleted { get; set; }

        public List<EnumerationPair> ListRenamed { get; set; }

        public List<EnumerationPair> ListChanged { get; set; }
    }

    public class ChangesProperties
    {
        public List<Property> ListAdded { get; set; }

        public List<Property> ListDeleted { get; set; }

        public List<Property> ListRenamed { get; set; }

        public List<Property> ListChanged { get; set; }
    }

    public class ChangesCatalogs
    {
        public List<Catalog> ListAdded { get; set; }

        public List<Catalog> ListDeleted { get; set; }

        public List<Catalog> ListRenamed { get; set; }

        public List<Catalog> ListChanged { get; set; }
    }
    // partial class ConfigCompareVisitor : IVisitorConfigNode
    // {
    //    CancellationToken IVisitorConfigNode.Token => throw new NotImplementedException();

    // DatabaseModel dbModel;

    // void IVisitorConfigNode.Visit(GroupConfigs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Config p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(ConfigTree p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListProperties p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Property p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListConstants p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Constant p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListEnumerations p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Enumeration p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(EnumerationPair p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Catalog p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListCatalogs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupDocuments p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Document p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListDocuments p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListJournals p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Journal p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListForms p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Form p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListReports p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Report p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupPropertiesTab p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListPropertiesTabs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupConfigs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Config p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(ConfigTree p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListProperties p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Property p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListConstants p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Constant p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListEnumerations p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Enumeration p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(EnumerationPair p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Catalog p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListCatalogs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupDocuments p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Document p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListDocuments p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListJournals p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Journal p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListForms p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Form p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListReports p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Report p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupPropertiesTab p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListPropertiesTabs p)
    //    {
    //        throw new NotImplementedException();
    //    }
    // }
}
