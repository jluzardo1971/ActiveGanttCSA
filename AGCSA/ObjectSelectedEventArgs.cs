using System;

namespace AGCSA
{
	using System;
	using System.ComponentModel;
	using System.Reflection;

    public class ObjectSelectedEventArgs : System.EventArgs
	{
		public E_EVENTTARGET EventTarget;
		public int ObjectIndex;
		public int ParentObjectIndex;
    
		internal ObjectSelectedEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			EventTarget = 0;
			ObjectIndex = 0;
			ParentObjectIndex = 0;
		}
	}
}

