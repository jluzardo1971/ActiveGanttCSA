using System;
using System.Drawing;

namespace AGCSA
{
	public class clsProgressLine
	{
		private ActiveGanttCSACtl mp_oControl;
		private Color mp_clrForeColor;
		private AGCSA.DateTime mp_dtPosition;
		private E_PROGRESSLINELENGTH mp_yLength;
		private E_PROGRESSLINETYPE mp_yLineType;
		private clsTimeLine mp_oTimeLine;

		public clsProgressLine(ActiveGanttCSACtl Value, clsTimeLine oTimeLine)
		{
			mp_oControl = Value;
			mp_clrForeColor = Color.Red;
			mp_dtPosition = new AGCSA.DateTime();
            mp_dtPosition.SetToCurrentDateTime();
			mp_yLength = E_PROGRESSLINELENGTH.TLMA_TICKMARKAREA;
			mp_yLineType = E_PROGRESSLINETYPE.TLMT_SYSTEMTIME;
			mp_oTimeLine = oTimeLine;

		}

		~clsProgressLine()
		{
			mp_oControl = null;
		}

        public AGCSA.DateTime Position 
		{
			get 
			{
				return mp_dtPosition;
			}
			set 
			{
				mp_dtPosition = value;
			}
		}

		public Color ForeColor 
		{
			get 
			{
				return mp_clrForeColor;
			}
			set 
			{
				mp_clrForeColor = value;
			}
		}

		public E_PROGRESSLINELENGTH Length 
		{
			get 
			{
				return mp_yLength;
			}
			set 
			{
				mp_yLength = value;
			}
		}

		public E_PROGRESSLINETYPE LineType 
		{
			get 
			{
				return mp_yLineType;
			}
			set 
			{
				mp_yLineType = value;
			}
		}

		internal void Draw()
		{
			int lXCoordinate;
			E_PROGRESSLINELENGTH yTimeLineMarkerLength;
            AGCSA.DateTime dtDate = new AGCSA.DateTime();
			if (mp_yLineType == E_PROGRESSLINETYPE.TLMT_SYSTEMTIME)
			{
				dtDate.SetToCurrentDateTime();
			}
			else
			{
				dtDate = mp_dtPosition;
			}
			if (dtDate >= mp_oTimeLine.StartDate & dtDate <= mp_oTimeLine.EndDate)
			{
				yTimeLineMarkerLength = mp_yLength;
				lXCoordinate = mp_oControl.MathLib.GetXCoordinateFromDate(mp_dtPosition);
				if (mp_oTimeLine.TickMarkArea.Visible == false & yTimeLineMarkerLength == E_PROGRESSLINELENGTH.TLMA_BOTH)
				{
					yTimeLineMarkerLength = E_PROGRESSLINELENGTH.TLMA_CLIENTAREA;
				}
				if (mp_oTimeLine.TickMarkArea.Visible == false & yTimeLineMarkerLength == E_PROGRESSLINELENGTH.TLMA_TICKMARKAREA)
				{
					yTimeLineMarkerLength = E_PROGRESSLINELENGTH.TLMA_NONE;
				}
				if (yTimeLineMarkerLength == E_PROGRESSLINELENGTH.TLMA_TICKMARKAREA)
				{
					mp_oControl.clsG.DrawLine(lXCoordinate, mp_oTimeLine.TiersTickMarksPosition("TickMarkArea"), lXCoordinate, mp_oTimeLine.Bottom, GRE_LINETYPE.LT_NORMAL, mp_clrForeColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				}
				else if (yTimeLineMarkerLength == E_PROGRESSLINELENGTH.TLMA_CLIENTAREA)
				{
					mp_oControl.clsG.DrawLine(lXCoordinate, mp_oControl.CurrentViewObject.ClientArea.Top, lXCoordinate, mp_oControl.CurrentViewObject.ClientArea.Bottom, GRE_LINETYPE.LT_NORMAL, mp_clrForeColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				}
				else if (yTimeLineMarkerLength == E_PROGRESSLINELENGTH.TLMA_BOTH)
				{
					mp_oControl.clsG.DrawLine(lXCoordinate, mp_oTimeLine.TiersTickMarksPosition("TickMarkArea"), lXCoordinate, mp_oControl.CurrentViewObject.ClientArea.Bottom, GRE_LINETYPE.LT_NORMAL, mp_clrForeColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				}
			}
		}

		public String GetXML()
		{
			clsXML oXML = new clsXML(mp_oControl, "ProgressLine");
			oXML.InitializeWriter();
			oXML.WriteProperty("ForeColor", mp_clrForeColor);
			oXML.WriteProperty("Length", mp_yLength);
			oXML.WriteProperty("LineType", mp_yLineType);
			oXML.WriteProperty("Position", mp_dtPosition);
			return oXML.GetXML();
		}

		public void SetXML(String sXML)
		{
			clsXML oXML = new clsXML(mp_oControl, "ProgressLine");
			oXML.SetXML(sXML);
			oXML.InitializeReader();
			oXML.ReadProperty("ForeColor", ref mp_clrForeColor);
			oXML.ReadProperty("Length", ref mp_yLength);
			oXML.ReadProperty("LineType", ref mp_yLineType);
			oXML.ReadProperty("Position", ref mp_dtPosition);
		}

	}
}

