using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForTT
{
	[Serializable]
	public class ServiceMethodParameter
	{
		public string Name;
		public string TypeName;
		public string TypeNamespace;
		public string Default;
	}
	[Serializable]
	public class ServiceMethod
	{
		public ServiceMethod()
		{
			Parameters = new List<ServiceMethodParameter>();
		}
		public string Name;
		public string ModelType;
		public string ResponseTypeName;
		public string ResponseType
		{
			get
			{
				if (!string.IsNullOrEmpty(ResponseTypeName))
					return ResponseTypeName;
				return ReturnTypeNamespace + "." + ReturnTypeName;
			}
		}
		public string ReturnTypeName;
		public string ReturnTypeNamespace;
		public string ReturnType { get { return ReturnTypeNamespace + "." + ReturnTypeName; } }
		public List<ServiceMethodParameter> Parameters;
	}

}
