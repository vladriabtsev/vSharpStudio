using System.Security.Cryptography;
using M = vPlugins.DapperModels.Sqlite.Model;

M.Settings.InitDefault();

M.Catalogs.Catalog1.Delete(null); // delete all catalog records

// DB record operations
var cat1 = M.Catalogs.Catalog1.Create(); // create catalog element in memory
cat1.Insert(); // insert in DB (atomic opertion)
cat1.Property1 = "test data";
cat1.Update(); // update in DB (atomic opertion)
var cnt = M.Catalogs.Catalog1.Count(); // count all catalog records
if (cnt != 1) throw new Exception();
var lst = M.Catalogs.Catalog1.Select(null); // select all 'Catalog1' header records
cat1.Delete(); // delete from DB (atomic opertion)
cnt = M.Catalogs.Catalog1.Count(); // count all catalog records
if (cnt > 0) throw new Exception();

// Complex objects operations
var cat2 = M.Catalogs.Catalog1.Create(); // create catalog element in memory
cat2.Save(); // save updated catalog header record with children records (if complex catalog)
cnt = M.Catalogs.Catalog1.Count(); // count all catalog records
if (cnt != 1) throw new Exception();
var cat = M.Catalogs.Catalog1.Load(cat2.Id); // loading catalog header record with children records (if complex catalog)
cat.Property1= "test data2";
cat.Save(); // save updated catalog header record with children records (if complex catalog)
cnt = M.Catalogs.Catalog1.Count(); // count all catalog records
cat2.Remove(); // remove catalog header record with children records (if complex catalog)
cnt = M.Catalogs.Catalog1.Count(); // count all catalog records
if (cnt > 0) throw new Exception();

// DB transaction operations
cat1 = M.Catalogs.Catalog1.Create(); // create catalog element in memory
cat2 = M.Catalogs.Catalog1.Create(); // create catalog element in memory
using (var uow = new M.UnitOfWork())
{
    uow.Save(cat1);
    uow.Save(cat2);
    uow.Commit();
}
cnt = M.Catalogs.Catalog1.Count(); // count all catalog records
if (cnt != 2) throw new Exception();

// Sample of where clause
M.Catalogs.Catalog1.Delete($"{M.Catalogs.Catalog1.F_ID}=@p1", new { p1 = cat1.Id });
cnt = M.Catalogs.Catalog1.Count(); // count all catalog records
if (cnt != 1) throw new Exception();

using (var uow = new M.UnitOfWork())
{
    uow.Delete(cat1);
    uow.Delete(cat2);
    uow.Commit();
}
cnt = M.Catalogs.Catalog1.Count(); // count all catalog records
if (cnt > 0) throw new Exception();

// DB transaction on commit operations
var dt = new M.TransactionOnCommit();
cat1 = M.Catalogs.Catalog1.Create(); // create catalog element in memory
dt.Insert(cat1);
cat2 = M.Catalogs.Catalog1.Create(); // create catalog element in memory
dt.Insert(cat2);
if (cnt > 0) throw new Exception();
dt.Commit();
cnt = M.Catalogs.Catalog1.Count(); // count all catalog records
if (cnt != 2) throw new Exception();

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
