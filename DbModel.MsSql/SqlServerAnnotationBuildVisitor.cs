using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.vm.ViewModels;

namespace DbModel.MsSql
{
    public class SqlServerAnnotationBuildVisitor : IVisitorConfig
    {
        public SqlServerModelAnnotations Annotations { get; set; }
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

        void IVisitorConfig.Visit(Properties m)
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

        void IVisitorConfig.Visit(Document m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Documents m)
        {
            throw new NotImplementedException();
        }
    }
}
