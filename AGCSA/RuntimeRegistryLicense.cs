using System;
using System.ComponentModel;

namespace AGCSA
{
	internal class RuntimeRegistryLicense : License 
	{
		private Type type;

		public RuntimeRegistryLicense(Type type) 
		{
			if ( type == null ) throw new NullReferenceException("The licensed type reference may not be null.");
			this.type = type;
		}

		public override string LicenseKey 
		{
			get 
			{
				return type.GUID.ToString();
			}
		}
		public override void Dispose() 
		{
		}
	}
}

