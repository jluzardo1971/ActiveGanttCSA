using System;

namespace AGCSA
{
	using System;
	using System.ComponentModel;
	using System.Reflection;


    public class KeyEventArgs : System.EventArgs
	{
		public System.Windows.Forms.Keys KeyCode;
		public bool Cancel;
		public char CharacterCode;
    
		internal KeyEventArgs()
		{
			Clear();
		}
    
		internal void Clear()
		{
			KeyCode = 0;
			Cancel = false;
		}
	}
}

