using System;
using System.Collections.Generic;
using System.Text;

namespace AGCSA
{
    public class PredecessorExceptionEventArgs : System.EventArgs
    {
        public int PredecessorIndex;
        public E_CONSTRAINTTYPE PredecessorType;

        internal PredecessorExceptionEventArgs()
        {
            Clear();
        }

        internal void Clear()
        {
            PredecessorIndex = 0;
            PredecessorType = 0;
        }
    }
}
