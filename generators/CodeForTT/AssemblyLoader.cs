using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeForTT
{
	class AssemblyLoader : MarshalByRefObject
	{
		private string currAsmPath = null;
		public List<ServiceMethod> GetServiceMethodsDescriptions(string asmPath, string contract)
		{
			currAsmPath = asmPath;
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ReflectionData_ReflectionOnlyAssemblyResolve;
			List<ServiceMethod> res = new List<ServiceMethod>();
			Assembly asm = Assembly.ReflectionOnlyLoadFrom(asmPath);
			Type type = asm.GetType(contract);
			if (type == null)
				throw new Exception("Service contract '" + contract + "' is not found in assembly " + asmPath);
			List<MethodInfo> lst = Utils.GetMethods(type);
			foreach (var t in lst)
			{
				ServiceMethod sm = new ServiceMethod();
				res.Add(sm);
				sm.Name = t.Name;
				sm.ReturnTypeName = t.ReturnType.Name;
				sm.ReturnTypeNamespace = t.ReturnType.Namespace;
				var attrs = t.GetCustomAttributesData();
				foreach (var tt in attrs)
				{
					if (tt.AttributeType.Name == "ResponseTypeAttribute")
					{
						var val = (Type)tt.ConstructorArguments[0].Value;
						sm.ResponseTypeName = Utils.TypeName(val);
						sm.ModelType = Utils.TypeName(val, true);
						break;
					}
				}
				ParameterInfo[] lstp = t.GetParameters();
				for (int i = 0; i < lstp.Length; i++)
				{
					ParameterInfo tt = lstp[i];
					ServiceMethodParameter p = new ServiceMethodParameter();
					p.Default = "";
					sm.Parameters.Add(p);
					p.Name = tt.Name;
					p.TypeName = Utils.TypeName(tt.ParameterType);
					p.TypeNamespace = tt.ParameterType.Namespace;
					try
					{
						if (tt.DefaultValue != null)
							p.Default = " = " + tt.DefaultValue.ToString();
					}
					catch (Exception) { }
				}
				var attrds = t.GetCustomAttributesData();
				foreach (var tt in attrds)
				{
					if (tt.AttributeType.Name == "ResponseTypeAttribute")
					{
						var val = (Type)tt.ConstructorArguments[0].Value;
						sm.ResponseTypeName = Utils.TypeName(val);
						break;
					}
				}
			}
			return res;
		}
		private Assembly ReflectionData_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			Assembly res = null;
			if (!args.Name.StartsWith("ServiceClientBaseNET"))
			{
				try
				{
					res = System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
					return res;
				}
				catch (Exception) { }
			}
			string nam = args.Name.Substring(0, args.Name.IndexOf(','));
			string path = Path.GetDirectoryName(currAsmPath) + "\\" + nam + ".dll";
			return Assembly.ReflectionOnlyLoadFrom(path);
		}

		internal void LoadAssembly(String assemblyPath)
		{
			try
			{
				Assembly.ReflectionOnlyLoadFrom(assemblyPath);
			}
			catch (FileNotFoundException)
			{
				/* Continue loading assemblies even if an assembly
				 * can not be loaded in the new AppDomain. */
			}
		}
	}
}
