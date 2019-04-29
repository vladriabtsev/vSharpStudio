using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using vSharpStudio.vm.ViewModels;

namespace GetLatestAttr
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Diagnostics.Trace.WriteLine("##### GetLatestAttr: " + Directory.GetCurrentDirectory());
            try
            {
                string pathAttrDat = @"..\..\..\GenFromProto\attr.json";
                if (!File.Exists(pathAttrDat))
                {
                    File.CreateText(pathAttrDat);
                    System.Diagnostics.Trace.WriteLine("##### GetLatestAttr: " + Path.GetFullPath(pathAttrDat) + " was created.");
                }
                string txt = File.ReadAllText(pathAttrDat);
//                var map = Ext.GetDicAttributes();
//                var json = JsonFormatter.Default.Format(map);
                //if (txt != json)
                //{
                //    File.WriteAllText(pathAttrDat, json);
                //    System.Diagnostics.Trace.WriteLine("##### GetLatestAttr: " + Path.GetFullPath(pathAttrDat) + " was updated.");
                //}
                System.Diagnostics.Trace.WriteLine("##### GetLatestAttr: no need to update.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("##### GetLatestAttr: Exception:" + ex.Message);
                throw;
            }
            //            var cfg2 = new Config(json);
        }
    }
}
