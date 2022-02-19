using System;
using System.ComponentModel;
using Microsoft.Win32;

namespace AGCSA
{
	internal class RegistryLicenseProvider : LicenseProvider
	{
		public RegistryLicenseProvider()
		{
		}

		public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions) 
		{
			if (context.UsageMode == LicenseUsageMode.Runtime) 
			{
				return new RuntimeRegistryLicense(type);
			}
			else 
			{
				RegistryKey licenseKey = Registry.ClassesRoot.OpenSubKey("Licenses\\" + type.GUID.ToString());
				if ( licenseKey != null ) 
				{
					string strLic = (string)licenseKey.GetValue("");
					if ( strLic != null ) 
					{
						if (String.Compare("PKUPLSXOOIBOZGNEM",strLic,false) == 0)
						{
							return new DesigntimeRegistryLicense(type);
						}
					}
				}
				if ( allowExceptions == true ) 
				{
					throw new LicenseException(type, instance, "You need a design time license to use this control in the design environment.");
				}
				return null;
			}
		}
	}
}





















































