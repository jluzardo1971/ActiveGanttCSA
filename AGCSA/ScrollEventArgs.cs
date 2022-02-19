using System;

namespace AGCSA
{
	using System;
	using System.ComponentModel;
	using System.Reflection;

    public class ScrollEventArgs : System.EventArgs
	{
		public E_SCROLLBAR ScrollBarType;
		public int Offset;
    
		internal ScrollEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			ScrollBarType = 0;
			Offset = 0;
		}
	}
}

