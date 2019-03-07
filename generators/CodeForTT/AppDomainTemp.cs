using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CodeForTT
{
	public class AppDomainTemp
	{
		private AppDomain BuildChildDomain(AppDomain parentDomain)
		{
			Evidence evidence = new Evidence(parentDomain.Evidence);
			AppDomainSetup setup = parentDomain.SetupInformation;
			return AppDomain.CreateDomain("DiscoveryRegion", evidence, setup);
		}
		public List<ServiceMethod> GetServiceMethodsDescriptions(string asmPath, string contract)
		{
			AppDomain childDomain = BuildChildDomain(AppDomain.CurrentDomain);
			try
			{
				Type loaderType = typeof(AssemblyLoader);
				if (loaderType.Assembly != null)
				{
					var loader = (AssemblyLoader)childDomain.CreateInstanceFrom(
						loaderType.Assembly.Location, loaderType.FullName).Unwrap();
					return loader.GetServiceMethodsDescriptions(asmPath, contract);
				}
				return null;
			}
			finally
			{
				AppDomain.Unload(childDomain);
			}
		}
	}
}
