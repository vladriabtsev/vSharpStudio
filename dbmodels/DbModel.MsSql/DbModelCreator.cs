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
        DiffConfig diff_config;
        protected override void Visit(IConstant m, DiffDataType diff_type)
        {
        }
        protected override void Visit(List<IConstant> diff_lst)
        {
            //x.HasOne("Node").WithMany().HasForeignKey("ParentAltId");
            //x.HasIndex("ParentAltId");
            if (diff_config.Constants.ListAll.Count == 0)
                return;
            string name = "Constants";
            var builder = modelBuilder.Entity(name).ToTable(name, diff_config.Config.Name);
            builder.Property(diff_config.Config.PrimaryKeyType.ToType(), "Id").UseSqlServerIdentityColumn();
            builder.HasKey("Id");
            foreach (var t in diff_lst)
            {
                if (t.IsDeleted())
                    continue;
                CreateField(builder, t.Name, t.DataTypeI, diff_config);
            }
        }
        protected override void Visit(IEnumerationPair m, DiffEnumerationPair diff_type)
        {
            // only code generation
        }
        protected override void Visit(IEnumeration m, DiffEnumerationType diff_type)
        {
            // only code generation
        }
        protected override void Visit(IProperty m, DiffProperty diff, DiffDataType diff_type)
        {
        }
        protected override void Visit(IPropertiesTab m, DiffPropertiesTab diff)
        {
        }
        protected override void Visit(ICatalog m, DiffCatalog diff)
        {
            if (m.IsDeleted())
                return;
            var builder = modelBuilder.Entity(m.Name).ToTable(m.Name, diff_config.Config.Name);
            builder.Property(diff_config.Config.PrimaryKeyType.ToType(), "Id").UseSqlServerIdentityColumn();
            builder.HasKey("Id");
            foreach (var t in m.GetDiffListProperties().ListAll)
            {
                if (t.IsDeleted())
                    continue;
                CreateField(builder, t.Name, t.DataTypeI, diff_config);
            }
            foreach (var t in m.GetDiffListPropertiesTabs().ListAll)
            {
                if (t.IsDeleted())
                    continue;
                CreateTab(builder, m.Name, t);
            }
        }
        protected override void Visit(IDocument m, DiffListProperties diff_shared_prop, DiffDocument diff)
        {
            if (m.IsDeleted())
                return;
            var builder = modelBuilder.Entity(m.Name).ToTable(m.Name, diff_config.Config.Name);
            builder.Property(diff_config.Config.PrimaryKeyType.ToType(), "Id").UseSqlServerIdentityColumn();
            builder.HasKey("Id");
            foreach (var t in diff_shared_prop.ListAll)
            {
                if (t.IsDeleted())
                    continue;
                CreateField(builder, t.Name, t.DataTypeI, diff_config);
            }

            foreach (var t in m.GetDiffListProperties().ListAll)
            {
                if (t.IsDeleted())
                    continue;
                CreateField(builder, t.Name, t.DataTypeI, diff_config);
            }
            foreach (var t in m.GetDiffListPropertiesTabs().ListAll)
            {
                if (t.IsDeleted())
                    continue;
                CreateTab(builder, m.Name, t);
            }
        }
        protected override void Visit(IConfig m, DiffConfig diff)
        {
            this.diff_config = diff;
        }
        private void CreateTab(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder parent_builder, string parent, IPropertiesTab tab)
        {
            string tabname = parent + tab.Name;
            parent_builder.HasMany(tabname);
            var builder = modelBuilder.Entity(tabname).ToTable(tabname, diff_config.Config.Name);
            builder.Property(diff_config.Config.PrimaryKeyType.ToType(), "Id").UseSqlServerIdentityColumn();
            builder.HasKey("Id");
            foreach (var t in tab.GroupPropertiesI.ListPropertiesI)
            {
                CreateField(builder, t.Name, t.DataTypeI, diff_config);
            }
            foreach (var t in tab.GroupPropertiesTabsI.ListPropertiesTabsI)
            {
                CreateTab(builder, tabname, t);
            }
        }
        private static void CreateField(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder builder, string name, IDataType datatype, DiffConfig diff)
        {
            switch (datatype.DataTypeEnum)
            {
                case EnumDataType.ANY:
                    throw new NotImplementedException();
                case EnumDataType.BOOL:
                    builder.Property(datatype.ClrType, name).HasColumnType("bit").IsRequired(!datatype.IsNullable);
                    break;
                case EnumDataType.TIME:
                    builder.Property(datatype.ClrType, name).HasColumnType("datetime2").IsRequired(!datatype.IsNullable);
                    //builder.Property(t.ClrType, name).HasColumnType("time").IsRequired(!t.IsNullable);
                    break;
                case EnumDataType.DATE:
                    builder.Property(datatype.ClrType, name).HasColumnType("datetime2").IsRequired(!datatype.IsNullable);
                    //builder.Property(t.ClrType, name).HasColumnType("date").IsRequired(!t.IsNullable);
                    break;
                case EnumDataType.DATETIME:
                    builder.Property(datatype.ClrType, name).HasColumnType("datetime2").IsRequired(!datatype.IsNullable);
                    break;
                case EnumDataType.CATALOG:
                    if (string.IsNullOrWhiteSpace(datatype.ObjectGuid))
                        throw new Exception("ObjectGuid is required");
                    ICatalog cat = null;
                    foreach (var t in diff.Catalogs.ListAll)
                    {
                        if (t.Guid == datatype.ObjectGuid)
                            cat = t;
                    }
                    if (cat == null)
                        throw new Exception("ObjectGuid is not found in the list of diff catalogs");
                    //builder.Property(diff.Config.PrimaryKeyType.ToType(), name + "Id");
                    builder.HasOne(cat.Name, name);
                    //if (isUseIndexForFk)
                    //    builder.HasIndex(new string[] { name + "Id" });
                    break;
                case EnumDataType.CATALOGS:
                    if (datatype.ListObjectGuidsI == null)
                        throw new Exception("ListObjectGuids is required");
                    if (datatype.ListObjectGuidsI.Count() == 0)
                        throw new Exception("ListObjectGuids is empty");
                    builder.Property(diff.Config.PrimaryKeyType.ToType(), "CatalogId");
                    foreach (var tg in datatype.ListObjectGuidsI)
                    {
                        ICatalog cats = null;
                        foreach (var t in diff.Catalogs.ListAll)
                        {
                            if (t.Guid == tg)
                                cats = t;
                        }
                        if (cats == null)
                            throw new Exception("ObjectGuid is not found in the list of diff catalogs");
                        builder.HasOne(cats.Name);
                    }
                    break;
                case EnumDataType.DOCUMENT:
                    if (string.IsNullOrWhiteSpace(datatype.ObjectGuid))
                        throw new Exception("ObjectGuid is required");
                    IDocument doc = null;
                    foreach (var t in diff.Documents.ListAll)
                    {
                        if (t.Guid == datatype.ObjectGuid)
                            doc = t;
                    }
                    if (doc == null)
                        throw new Exception("ObjectGuid is not found in the list of diff documents");
                    //builder.Property(diff.Config.PrimaryKeyType.ToType(), name);
                    builder.HasOne(doc.Name, name);
                    //if (isUseIndexForFk)
                    //    builder.HasIndex(new string[] { name + "Id" });
                    break;
                case EnumDataType.DOCUMENTS:
                    throw new NotImplementedException();
                case EnumDataType.ENUMERATION:
                    if (string.IsNullOrWhiteSpace(datatype.ObjectGuid))
                        throw new Exception("ObjectGuid is required");
                    IEnumeration enumer = null;
                    foreach (var t in diff.Enumerations.ListAll)
                    {
                        if (t.Guid == datatype.ObjectGuid)
                            enumer = t;
                    }
                    if (enumer == null)
                        throw new Exception("ObjectGuid is not found in the list of diff enumerations");
                    if (datatype.IsNullable)
                    {
                        switch (enumer.DataTypeEnum)
                        {
                            case EnumEnumerationType.STRING_VALUE:
                                builder.Property(typeof(string), name).HasColumnType("nvarchar(" + enumer.DataTypeLength + ")").IsRequired(!datatype.IsNullable);
                                break;
                            case EnumEnumerationType.INTEGER_VALUE:
                                builder.Property(typeof(int?), name).HasColumnType("int").IsRequired(!datatype.IsNullable);
                                break;
                            case EnumEnumerationType.SHORT_VALUE:
                                builder.Property(typeof(short?), name).HasColumnType("smallint").IsRequired(!datatype.IsNullable);
                                break;
                            case EnumEnumerationType.BYTE_VALUE:
                                builder.Property(typeof(byte?), name).HasColumnType("tinyint").IsRequired(!datatype.IsNullable);
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                    }
                    else
                    {
                        switch (enumer.DataTypeEnum)
                        {
                            case EnumEnumerationType.STRING_VALUE:
                                builder.Property(typeof(string), name).HasColumnType("nvarchar(" + enumer.DataTypeLength + ")").IsRequired(!datatype.IsNullable);
                                break;
                            case EnumEnumerationType.INTEGER_VALUE:
                                builder.Property(typeof(int), name).HasColumnType("int").IsRequired(!datatype.IsNullable);
                                break;
                            case EnumEnumerationType.SHORT_VALUE:
                                builder.Property(typeof(short), name).HasColumnType("smallint").IsRequired(!datatype.IsNullable);
                                break;
                            case EnumEnumerationType.BYTE_VALUE:
                                builder.Property(typeof(byte), name).HasColumnType("tinyint").IsRequired(!datatype.IsNullable);
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                    }
                    break;
                case EnumDataType.NUMERICAL:
                    var f = builder.Property(datatype.ClrType, name).IsRequired(!datatype.IsNullable);
                    // https://docs.microsoft.com/en-us/sql/t-sql/data-types/data-types-transact-sql?view=sql-server-2017
                    // bigint - 2 ^ 63(-9, 223, 372, 036, 854, 775, 808) to 2 ^ 63 - 1(9, 223, 372, 036, 854, 775, 807)    8 Bytes
                    // int -2 ^ 31(-2, 147, 483, 648) to 2 ^ 31 - 1(2, 147, 483, 647)    4 Bytes
                    // smallint - 2 ^ 15(-32, 768) to 2 ^ 15 - 1(32, 767)  2 Bytes
                    // tinyint 0 to 255    1 Byte

                    // decimal[ (p[ ,s] )] and numeric[ (p[ ,s] )]
                    // values are from - 10^38 +1 through 10^38 - 1
                    // Precision Storage bytes
                    // 1 - 9     5
                    // 10 - 19   9
                    // 20 - 28   13
                    // 29 - 38   17

                    // money - 922,337,203,685,477.5808 to 922,337,203,685,477.5807(-922, 337, 203, 685, 477.58
                    // to 922, 337, 203, 685, 477.58 for Informatica.Informatica only supports two decimals, not four.) 8 bytes
                    // smallmoney - 214,748.3648 to 214,748.3647  4 bytes

                    // float [ (n) ]
                    // n value	Precision	Storage size
                    // 1 - 24    7 digits    4 bytes
                    // 25 - 53   15 digits   8 bytes
                    if (datatype.Accuracy == 0)
                    {
                        if (datatype.Length <= 2)
                            f.HasColumnType("tinyint");
                        else if (datatype.Length <= 4)
                            f.HasColumnType("smallint");
                        else if (datatype.Length <= 9)
                            f.HasColumnType("int");
                        else if (datatype.Length <= 18)
                            f.HasColumnType("bigint");
                        else if (datatype.Length <= 38)
                            f.HasColumnType("decimal(" + datatype.Length + ")");
                        else
                            throw new ArgumentException("Length");
                    }
                    else
                    {
                        // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                        // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                        // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                        if (datatype.Length == 0)
                            f.HasColumnType("decimal(38," + datatype.Accuracy + ")");
                        else if (datatype.Length <= 38)
                            f.HasColumnType("decimal(" + datatype.Length + "," + datatype.Accuracy + ")");
                        else
                            throw new ArgumentException("Length");
                    }
                    break;
                case EnumDataType.STRING:
                    if (datatype.Length == 0)
                        builder.Property(typeof(string), name).HasColumnType("nvarchar(MAX)").IsRequired(!datatype.IsNullable);
                    else
                        builder.Property(typeof(string), name).HasColumnType("nvarchar(" + datatype.Length + ")").IsRequired(!datatype.IsNullable);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
