using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore.Metadata;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.DbModels
{
    public partial class MsSqlServerAnnotationBuildVisitor : IVisitorConfig
    {
        public SqlServerModelAnnotations Annotations { get; set; }

        CancellationToken IVisitorConfig.Token => throw new NotImplementedException();

        void IVisitorConfig.Visit(Config m)
        {
            //IModel
            //this.Annotations = new SqlServerModelAnnotations();
        }

        void IVisitorConfig.Visit(Property m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(DataType m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Constant m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Constants m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Enumeration m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Enumerations m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Catalog m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Catalogs m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(vm.ViewModels.Properties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Documents p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Journals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertiesTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Config m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Property m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(DataType m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Constant m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Constants m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Enumeration m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Enumerations m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Catalog m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Catalogs m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(vm.ViewModels.Properties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Documents p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Journals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertiesTree p)
        {
            throw new NotImplementedException();
        }
    }
}
