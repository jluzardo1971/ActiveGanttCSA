using System;
using System.Collections.Generic;
using System.Text;

namespace AGCSA
{
    public class clsScrollBarSeparator
    {

        private ActiveGanttCSACtl mp_oControl;
        private string mp_sStyleIndex;
        private clsStyle mp_oStyle;

        internal clsScrollBarSeparator(ActiveGanttCSACtl Value)
        {
            mp_oControl = Value;
            mp_sStyleIndex = "DS_SB_SEPARATOR";
            mp_oStyle = mp_oControl.Styles.FItem("DS_SB_SEPARATOR");
        }

        public string StyleIndex
        {
            get
            {
                if (mp_sStyleIndex == "DS_SB_SEPARATOR")
                {
                    return "";
                }
                else
                {
                    return mp_sStyleIndex;
                }
            }
            set
            {
                value = value.Trim();
                if (value.Length == 0)
                    value = "DS_SB_SEPARATOR";
                mp_sStyleIndex = value;
                mp_oStyle = mp_oControl.Styles.FItem(value);
            }
        }

        public clsStyle Style
        {
            get { return mp_oStyle; }
        }

        public string GetXML()
        {
            clsXML oXML = new clsXML(mp_oControl, "ScrollBarSeparator");
            oXML.InitializeWriter();
            oXML.WriteProperty("StyleIndex", mp_sStyleIndex);
            return oXML.GetXML();
        }

        public void SetXML(string sXML)
        {
            clsXML oXML = new clsXML(mp_oControl, "ScrollBarSeparator");
            oXML.SetXML(sXML);
            oXML.InitializeReader();
            oXML.ReadProperty("StyleIndex", ref mp_sStyleIndex);
            StyleIndex = mp_sStyleIndex;
        }

    }
}
