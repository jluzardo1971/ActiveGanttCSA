using System;

namespace AGCSA
{
	public class clsLayer : clsItemBase
	{
		private ActiveGanttCSACtl mp_oControl;
		private bool mp_bVisible;
		private String mp_sTag;

		public clsLayer(ActiveGanttCSACtl Value)
		{
			mp_oControl = Value;
			mp_bVisible = true;
			mp_sTag = "";
		}

		~clsLayer()
		{				   
		}

		public String Key 
		{
			get 
			{
				return mp_sKey;
			}
			set 
			{
				mp_oControl.Layers.oCollection.mp_SetKey(ref mp_sKey, value, SYS_ERRORS.LAYERS_SET_KEY);
			}
		}

		public bool Visible 
		{
			get 
			{
				return mp_bVisible;
			}
			set 
			{
				mp_bVisible = value;
			}
		}

		public String Tag
		{
			get
			{
				return mp_sTag;
			}
			set
			{
				mp_sTag = value;
			}
		}

		public String GetXML()
		{
			clsXML oXML = new clsXML(mp_oControl, "Layer");
			oXML.InitializeWriter();
			oXML.WriteProperty("Key", mp_sKey);
			oXML.WriteProperty("Visible", mp_bVisible);
			oXML.WriteProperty("Tag", mp_sTag);
			return oXML.GetXML();
		}

		public void SetXML(String sXML)
		{
			clsXML oXML = new clsXML(mp_oControl, "Layer");
			oXML.SetXML(sXML);
			oXML.InitializeReader();
			oXML.ReadProperty("Key", ref mp_sKey);
			oXML.ReadProperty("Visible", ref mp_bVisible);
			oXML.ReadProperty("Tag", ref mp_sTag);
		}


	}
}

