using System;
using System.Drawing;

namespace AGCSA
{

	using System;
	using System.ComponentModel;
	using System.Reflection;

    public class PredecessorDrawEventArgs : System.EventArgs
	{
		public bool CustomDraw;
		public Graphics Graphics;
		public int PredecessorIndex;
		public int TaskIndex;
		public E_CONSTRAINTTYPE PredecessorType;
    
		internal PredecessorDrawEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			CustomDraw = false;
			Graphics = null;
			PredecessorIndex = 0;
			TaskIndex = 0;
			PredecessorType = 0;
		}
	}

}

