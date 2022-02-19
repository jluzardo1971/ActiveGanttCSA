using System;
using System.Drawing;

namespace AGCSA
{

	using System;
	using System.ComponentModel;
	using System.Reflection;

    public class DrawEventArgs : System.EventArgs
	{
		public E_EVENTTARGET EventTarget;
		public bool CustomDraw;
		public int ObjectIndex;
		public int ParentObjectIndex;
		public Graphics Graphics;
    
		internal DrawEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			EventTarget = 0;
			CustomDraw = false;
			ObjectIndex = 0;
			ParentObjectIndex = 0;
			Graphics = null;
		}
	}


}

