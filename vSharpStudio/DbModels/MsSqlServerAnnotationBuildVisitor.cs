using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore.Metadata;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.DbModels
{
    public partial class MsSqlServerAnnotationBuildVisitor : IVisitorConfigNode
    {
        public SqlServerModelAnnotations Annotations { get; set; }

        CancellationToken IVisitorConfigNode.Token => throw new NotImplementedException();

        void IVisitorConfigNode.Visit(Config m)
        {
            //IModel
            //this.Annotations = new SqlServerModelAnnotations();
        }

        void IVisitorConfigNode.Visit(Property m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Constant m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Enumeration m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Catalog m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(ConfigTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupConfigs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Report p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListCatalogs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListProperties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListConstants p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListEnumerations p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListJournals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListForms p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListReports p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListPropertiesTabs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupPropertiesTab p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Config m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Property m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Constant m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Enumeration m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Catalog m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(ConfigTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupConfigs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Report p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListCatalogs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListProperties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListConstants p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListEnumerations p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListJournals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListForms p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListReports p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListPropertiesTabs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertiesTab p)
        {
            throw new NotImplementedException();
        }
    }
}
