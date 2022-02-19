using System;
using System.Drawing;

namespace AGCSA
{

	using System;
	using System.ComponentModel;
	using System.Reflection;


    public class CustomTierDrawEventArgs : System.EventArgs
	{
		public string Text;
		public bool CustomDraw;
		public string StyleIndex;
		public E_TIERPOSITION TierPosition;
		public AGCSA.DateTime StartDate;
        public AGCSA.DateTime EndDate;
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
		public int LeftTrim;
		public int RightTrim;
		public Graphics Graphics;
        public E_INTERVAL Interval;
        public int Factor;
    
    
		internal CustomTierDrawEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			Text = "";
			CustomDraw = false;
			StyleIndex = "";
			TierPosition = E_TIERPOSITION.SP_UPPER;
            StartDate = new AGCSA.DateTime();
            EndDate = new AGCSA.DateTime();
			Left = 0;
			Top = 0;
			Right = 0;
			Bottom = 0;
			LeftTrim = 0;
			RightTrim = 0;
			Graphics = null;
            Interval = E_INTERVAL.IL_SECOND;
            Factor = 0;
		}
	}

}

