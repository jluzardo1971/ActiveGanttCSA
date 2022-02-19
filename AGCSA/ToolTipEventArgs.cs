using System;
using System.Drawing;

namespace AGCSA
{

	using System;
	using System.ComponentModel;
	using System.Reflection;


    public class ToolTipEventArgs : System.EventArgs
	{
		public int InitialRowIndex;
		public int FinalRowIndex;
		public int TaskIndex;
		public int MilestoneIndex;
		public int PercentageIndex;
		public int RowIndex;
		public int CellIndex;
		public int ColumnIndex;
		public AGCSA.DateTime InitialStartDate;
        public AGCSA.DateTime InitialEndDate;
        public AGCSA.DateTime StartDate;
        public AGCSA.DateTime EndDate;
		public int XStart;
		public int XEnd;
		public E_OPERATION Operation;
		public E_EVENTTARGET EventTarget;
		public string TaskPosition;
		public string PredecessorPosition;
		public int X;
		public int Y;
		public int X1;
		public int Y1;
		public int X2;
		public int Y2;
		public bool CustomDraw;
		public Graphics Graphics;
		public E_TOOLTIPTYPE ToolTipType;
    
		internal ToolTipEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			InitialRowIndex = 0;
			FinalRowIndex = 0;
			RowIndex = 0;
			TaskIndex = 0;
			MilestoneIndex = 0;
			PercentageIndex = 0;
			CellIndex = 0;
			ColumnIndex = 0;
			StartDate = new AGCSA.DateTime();
			EndDate = new AGCSA.DateTime();
            InitialStartDate = new AGCSA.DateTime();
            InitialEndDate = new AGCSA.DateTime();
			XStart = 0;
			XEnd = 0;
			X = 0;
			Y = 0;
			X1 = 0;
			Y1 = 0;
			X2 = 0;
			Y2 = 0;
			Operation = 0;
			EventTarget = 0;
			ToolTipType = 0;
		}
	}
}

