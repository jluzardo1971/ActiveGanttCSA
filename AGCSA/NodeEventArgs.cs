using System;

namespace AGCSA
{

	using System;
	using System.ComponentModel;
	using System.Reflection;


    public class NodeEventArgs : System.EventArgs
	{
		public int Index;
    
		internal NodeEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			Index = 0;
		}
	}

}

