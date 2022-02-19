using System;

namespace AGCSA
{
	using System;
	using System.ComponentModel;
	using System.Reflection;


    public class ObjectStateChangedEventArgs : System.EventArgs
	{
		public E_EVENTTARGET EventTarget;
		public int Index;
		public bool Cancel;
		public int DestinationIndex;
		public int InitialRowIndex;
		public int FinalRowIndex;
		public int InitialColumnIndex;
		public int FinalColumnIndex;
        public AGCSA.DateTime StartDate;
        public AGCSA.DateTime EndDate;
    
		internal ObjectStateChangedEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
            EventTarget = 0;
            Index = 0;
            StartDate = new AGCSA.DateTime();
            EndDate = new AGCSA.DateTime();
            Cancel = false;
		}
	}
}

