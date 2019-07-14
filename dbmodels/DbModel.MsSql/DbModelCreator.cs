using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vSharpStudio.common;

namespace vPlugin.DbModel.MsSql
{
    public class DbModelCreator : DiffModelVisitorBase
    {
        IConfig config;
        protected override void Visit(IEnumeration m, DiffEnumerationType diff_type)
        {
            //modelBuilder.Entity("test1");

            throw new NotImplementedException();
        }

        protected override void Visit(IEnumerationPair m, DiffEnumerationPair diff_type)
        {
            throw new NotImplementedException();
        }

        protected override void Visit(List<IConstant> lst)
        {
            string name = "Constants";
            var cnst = modelBuilder.Entity(name).ToTable(name, config.Name);
            cnst.Property(typeof(int), "Id").UseSqlServerIdentityColumn();
            cnst.HasKey("Id");
            foreach (var t in lst)
            {
                var p = cnst.Property(t.Name);
                switch (t.DataTypeI.DataTypeEnum)
                {
                    case EnumDataType.ANY:
                        throw new NotImplementedException();
                    case EnumDataType.BOOL:
                        throw new NotImplementedException();
                    case EnumDataType.CATALOG:
                        throw new NotImplementedException();
                    case EnumDataType.CATALOGS:
                        throw new NotImplementedException();
                    case EnumDataType.DOCUMENT:
                        throw new NotImplementedException();
                    case EnumDataType.DOCUMENTS:
                        throw new NotImplementedException();
                    case EnumDataType.ENUMERATION:
                        throw new NotImplementedException();
                    case EnumDataType.NUMERICAL:
                        p.Metadata.SetProviderClrType(Type.GetType(t.DataTypeI.ClrType));
                        p.Metadata.SetMaxLength((int)t.DataTypeI.Length);
                        //p.Metadata.SetMaxLength((int)t.DataTypeI.Length);
                        throw new NotImplementedException();
                    case EnumDataType.STRING:
                        p.Metadata.SetProviderClrType(typeof(string));
                        p.Metadata.SetMaxLength((int)t.DataTypeI.Length);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                //var diff_type = t.GetDiffDataType();
                //p.Metadata()
            }
        }
        protected override void Visit(IConstant m, DiffDataType diff_type)
        {
        }

        protected override void Visit(IConfig m, DiffConfig diff)
        {
            config = m;
        }

        protected override void Visit(ICatalog m, DiffCatalog diff)
        {
            throw new NotImplementedException();
        }

        protected override void Visit(IDocument m, DiffDocument diff)
        {
            throw new NotImplementedException();
        }

        protected override void Visit(IPropertiesTab m, DiffPropertiesTab diff)
        {
            throw new NotImplementedException();
        }

        protected override void Visit(IProperty m, DiffProperty diff, DiffDataType diff_type)
        {
            throw new NotImplementedException();
        }

    }
}
