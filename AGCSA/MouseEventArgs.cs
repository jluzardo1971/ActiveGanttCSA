using System;

namespace AGCSA
{

	using System;
	using System.ComponentModel;
	using System.Reflection;


    public class MouseEventArgs : System.EventArgs
	{
		public int X;
		public int Y;
		public E_EVENTTARGET EventTarget;
		public E_OPERATION Operation;
		public E_MOUSEBUTTONS Button;
		public bool Cancel;
    
		internal MouseEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			X = 0;
			Y = 0;
			EventTarget = 0;
			Operation = 0;
			Button = 0;
			Cancel = false;
		}
	}
}

