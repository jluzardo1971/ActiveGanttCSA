using System;

namespace AGCSA
{
	using System;
	using System.ComponentModel;
	using System.Reflection;

    public class ErrorEventArgs : System.EventArgs
	{
		public int Number;
		public string Description;
		public string Source;
    
		internal ErrorEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			Number = 0;
			Description = "";
			Source = "";
		}
	}
}

