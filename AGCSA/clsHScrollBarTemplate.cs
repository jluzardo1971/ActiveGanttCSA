using System;
using System.Timers;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AGCSA
{

	public class clsHScrollBarTemplate
	{



		private enum E_BUTTON
		{
			BTN_NONE = 0,
			BTN_LEFT = 1,
			BTN_RIGHT = 2,
			BTN_LEFTLCHANGE = 3,
			BTN_RIGHTLCHANGE = 4,
			BTN_BUTTON = 5,
		}

		private ActiveGanttCSACtl mp_oControl;
		internal int mp_lSmallChange;
		internal int mp_lLargeChange;
		internal int mp_lValue;
		internal int mp_lMin;
		internal int mp_lMax;
		private int ButtonX1;
		private int ButtonX2;
		private int ButtonY1;
		private int ButtonY2;
		private bool Visible;
		internal bool mp_bEnabled;
		internal int Height;
		internal int Width;
		internal int Top;
		internal int Left;
		private bool mp_bMouseDown;
		private int mp_iMouseXPosition;
		private E_BUTTON mp_iButton;
		private int mp_iTimerInterval;
		private E_SCROLLSTATE mp_yState;
		private int mp_lValueBuff;
		private System.Timers.Timer mp_oTimer = new System.Timers.Timer();
        private string mp_sStyleIndex;
        private clsStyle mp_oStyle;
        public clsButtonState ArrowButtons;
        public clsButtonState ThumbButton;

		internal delegate void ValueChangedEventHandler (Object sender, System.EventArgs e, int Offset);
		internal event ValueChangedEventHandler ValueChanged;


		public clsHScrollBarTemplate()
		{
		}

		internal void Initialize(ActiveGanttCSACtl oControl)
		{
			mp_oControl = oControl;
            ArrowButtons = new clsButtonState(oControl, "Arrow");
            ThumbButton = new clsButtonState(oControl, "Thumb");
			mp_lSmallChange = 1;
			Height = 17;
			mp_iButton = E_BUTTON.BTN_NONE;
			mp_iTimerInterval = 100;
			mp_lValueBuff = Value;
			mp_oTimer.Elapsed += new System.Timers.ElapsedEventHandler(mp_oTimer_Tick);
            mp_sStyleIndex = "DS_SCROLLBAR";
            mp_oStyle = mp_oControl.Styles.FItem("DS_SCROLLBAR");
		}

		internal bool Enabled 
		{
			get 
			{
				return mp_bEnabled;
			}
			set 
			{
				mp_bEnabled = value;
			}
		}

		internal int Value 
		{
			get 
			{
				return mp_lValue;
			}
			set 
			{
				mp_lValue = value;
				if (mp_lValue < mp_lMin)
				{
					throw new Exception("Value is less than mp_lMin.");
				}
			}
		}

		internal int Min 
		{
			get 
			{
				return mp_lMin;
			}
			set 
			{
				mp_lMin = value;
			}
		}

		internal int Max 
		{
			get 
			{
				return mp_lMax;
			}
			set 
			{
				mp_lMax = value;
				if (mp_lMax < mp_lMin)
				{
					throw new Exception("mp_lMax is less than mp_lMin.");
				}
			}
		}

		internal int SmallChange 
		{
			get 
			{
				return mp_lSmallChange;
			}
			set 
			{
				mp_lSmallChange = value;
			}
		}

		internal int LargeChange 
		{
			get 
			{
				return mp_lLargeChange;
			}
			set 
			{
				mp_lLargeChange = value;
			}
		}

		public int TimerInterval 
		{
			get 
			{
				return mp_iTimerInterval;
			}
			set 
			{
				mp_iTimerInterval = value;
			}
		}

		internal void Draw()
		{
            if (Visible == false)
                return;
            clsStyle oArrowButtonLeftStyle = ArrowButtons.NormalStyle;
            clsStyle oArrowButtonRightStyle = ArrowButtons.NormalStyle;
            clsStyle oThumbButtonStyle = ThumbButton.NormalStyle;
            if ((mp_lMax - mp_lMin) == 0)
            {
                return;
            }
            mp_oControl.clsG.mp_DrawItem(Left, Top, Left + Width - 1, Top + Height - 1, "", "", false, null, 0, 0,
            mp_oStyle);
            CalculateH();
            if (Enabled == false)
            {
                oThumbButtonStyle = ThumbButton.DisabledStyle;
                oArrowButtonLeftStyle = ArrowButtons.DisabledStyle;
                oArrowButtonRightStyle = ArrowButtons.DisabledStyle;
            }
            else if (mp_bMouseDown == true)
            {
                if (mp_iButton == E_BUTTON.BTN_LEFT)
                {
                    oThumbButtonStyle = ThumbButton.NormalStyle;
                    oArrowButtonLeftStyle = ArrowButtons.PressedStyle;
                    oArrowButtonRightStyle = ArrowButtons.NormalStyle;
                }
                else if (mp_iButton == E_BUTTON.BTN_RIGHT)
                {
                    oThumbButtonStyle = ThumbButton.NormalStyle;
                    oArrowButtonLeftStyle = ArrowButtons.NormalStyle;
                    oArrowButtonRightStyle = ArrowButtons.PressedStyle;
                }
                else if (mp_iButton == E_BUTTON.BTN_BUTTON)
                {
                    oThumbButtonStyle = ThumbButton.PressedStyle;
                    oArrowButtonLeftStyle = ArrowButtons.NormalStyle;
                    oArrowButtonRightStyle = ArrowButtons.NormalStyle;
                }
                else
                {
                    oThumbButtonStyle = ThumbButton.NormalStyle;
                    oArrowButtonLeftStyle = ArrowButtons.NormalStyle;
                    oArrowButtonRightStyle = ArrowButtons.NormalStyle;
                }
            }
            else
            {
                oArrowButtonLeftStyle = ArrowButtons.NormalStyle;
                oArrowButtonRightStyle = ArrowButtons.NormalStyle;
                oThumbButtonStyle = ThumbButton.NormalStyle;
            }
            mp_oControl.clsG.mp_DrawItem(Left, Top, Left + 16, Top + Height - 1, "", "", false, null, 0, 0,
            oArrowButtonLeftStyle);
            mp_oControl.clsG.mp_DrawItem(Left + Width - 17, Top, Left + Width - 1, Top + Height - 1, "", "", false, null, 0, 0,
            oArrowButtonRightStyle);
            mp_oControl.clsG.mp_DrawItem(Left + ButtonX1, Top + ButtonY1 - 1, Left + ButtonX2 - 2, Top + ButtonY2 - 1, "", "", false, null, 0, 0,
            oThumbButtonStyle);

            if (oArrowButtonLeftStyle.ScrollBarStyle.DropShadow == true)
            {
                mp_oControl.clsG.mp_DrawArrow(Left + oArrowButtonLeftStyle.ScrollBarStyle.DropShadowLeftX, Top + oArrowButtonLeftStyle.ScrollBarStyle.DropShadowLeftY, GRE_ARROWDIRECTION.AWD_LEFT, oArrowButtonLeftStyle.ScrollBarStyle.ArrowSize, oArrowButtonLeftStyle.ScrollBarStyle.DropShadowArrowColor);
            }
            mp_oControl.clsG.mp_DrawArrow(Left + oArrowButtonLeftStyle.ScrollBarStyle.LeftX, Top + oArrowButtonLeftStyle.ScrollBarStyle.LeftY, GRE_ARROWDIRECTION.AWD_LEFT, oArrowButtonLeftStyle.ScrollBarStyle.ArrowSize, oArrowButtonLeftStyle.ScrollBarStyle.ArrowColor);
            if (oArrowButtonRightStyle.ScrollBarStyle.DropShadow == true)
            {
                mp_oControl.clsG.mp_DrawArrow(Left + Width - 17 + oArrowButtonRightStyle.ScrollBarStyle.DropShadowRightX, Top + oArrowButtonRightStyle.ScrollBarStyle.DropShadowRightY, GRE_ARROWDIRECTION.AWD_RIGHT, oArrowButtonRightStyle.ScrollBarStyle.ArrowSize, oArrowButtonRightStyle.ScrollBarStyle.DropShadowArrowColor);
            }
            mp_oControl.clsG.mp_DrawArrow(Left + Width - 17 + oArrowButtonRightStyle.ScrollBarStyle.RightX, Top + oArrowButtonRightStyle.ScrollBarStyle.RightY, GRE_ARROWDIRECTION.AWD_RIGHT, oArrowButtonRightStyle.ScrollBarStyle.ArrowSize, oArrowButtonRightStyle.ScrollBarStyle.ArrowColor);
		}

		internal bool OverControl(int X, int Y)
		{
			if (X >= Left & X <= Left + Width & Y >= Top & Y <= Top + Height)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void ConvertToRelativeCoords(ref int X, ref int Y)
		{
			X = X - Left;
			Y = Y - Top;
		}

		internal bool ScrollBarClick(int X, int Y)
		{
			if (OverControl(X,Y) == false)
			{
				return false;
			}
			ConvertToRelativeCoords(ref X, ref Y);
			CalculateH();
			if (X < 17)
			{
				mp_iButton = E_BUTTON.BTN_LEFT;
				SmallChangeLeft();
				return true;
			}
			else if (X > 17 & X < ButtonX1)
			{
				mp_iButton = E_BUTTON.BTN_LEFTLCHANGE;
				LargeChangeLeft();
				return true;
			}
			else if (X >= ButtonX1 & X <= ButtonX2)
			{
				mp_iButton = E_BUTTON.BTN_BUTTON;
				mp_iMouseXPosition = X;
				return true;
			}
			else if (X > ButtonY2 & X < Width - 17)
			{
				mp_iButton = E_BUTTON.BTN_RIGHTLCHANGE;
				LargeChangeRight();
				return true;
			}
			else if (X > Width - 17 & X < Width)
			{
				mp_iButton = E_BUTTON.BTN_RIGHT;
				SmallChangeRight();
				return true;
			}
			else
			{
				return false;
			}
		}

		private void SmallChangeLeft()
		{
			if ((mp_lValue - mp_lSmallChange) >= mp_lMin)
			{
				mp_lValue = mp_lValue - mp_lSmallChange;
			}
			else
			{
				mp_lValue = mp_lMin;
			}
			mp_ValueChanged();
		}

		private void SmallChangeRight()
		{
			if ((mp_lValue + mp_lSmallChange) <= mp_lMax)
			{
				mp_lValue = mp_lValue + mp_lSmallChange;
			}
			else
			{
				mp_lValue = mp_lMax;
			}
			mp_ValueChanged();
		}

		private void LargeChangeLeft()
		{
			if ((mp_lValue - mp_lLargeChange) >= mp_lMin)
			{
				mp_lValue = mp_lValue - mp_lLargeChange;
			}
			else if ((mp_lValue - mp_lLargeChange) < mp_lMin)
			{
				mp_lValue = mp_lMin;
			}
			mp_ValueChanged();
		}

		private void LargeChangeRight()
		{
			if ((mp_lValue + mp_lLargeChange) <= mp_lMax)
			{
				mp_lValue = mp_lValue + mp_lLargeChange;
			}
			else if ((mp_lValue + mp_lLargeChange) > mp_lMax)
			{
				mp_lValue = mp_lMax;
			}
			mp_ValueChanged();
		}

		private void mp_ValueChanged()
		{
			System.EventArgs e = new System.EventArgs();
            if ((mp_lValue - mp_lValueBuff) != 0)
            {
                if (ValueChanged != null)
                {
                    ValueChanged(this, e, mp_lValue - mp_lValueBuff);
                }
            }
			mp_lValueBuff = mp_lValue;
		}

		internal void mp_oTimer_Tick(Object source, ElapsedEventArgs e)
		{
			switch (mp_iButton)
			{
				case E_BUTTON.BTN_LEFT:
					SmallChangeLeft();
					break;
				case E_BUTTON.BTN_LEFTLCHANGE:
					LargeChangeLeft();
					break;
				case E_BUTTON.BTN_RIGHTLCHANGE:
					LargeChangeRight();
					break;
				case E_BUTTON.BTN_RIGHT:
					SmallChangeRight();
					break;
			}
		}

		private void CalculateH()
		{
			int lWidth;
			int lItemLength = 0;
			lWidth = Width - (16 * 2) - 1;
			if (mp_lLargeChange > 0)
			{
				lItemLength = mp_oControl.MathLib.RoundDouble((double)lWidth / ((((double)mp_lMax - (double)mp_lMin) / (double)mp_lLargeChange) + 1));
			}
			if (lItemLength < 8)
			{
				lItemLength = 8;
			}
			lItemLength = lItemLength + 1;
			lWidth = lWidth - lItemLength;
			ButtonX1 = mp_oControl.MathLib.RoundDouble(((((double)mp_lValue - (double)mp_lMin) / ((double)mp_lMax - (double)mp_lMin)) * (double)lWidth) + 17);
			ButtonX2 = ButtonX1 + lItemLength;
			ButtonY1 = 1;
			ButtonY2 = ButtonY1 + Height - 1;
		}

		private int InvCalculateH(int X)
		{
			int lWidth;
			int lItemLength = 0;
			int iReturnValue;
			lWidth = Width - (16 * 2) - 1;
			if (mp_lLargeChange > 0)
			{
				lItemLength = mp_oControl.MathLib.RoundDouble((double)lWidth / ((((double)mp_lMax - (double)mp_lMin) / (double)mp_lLargeChange) + 1));
			}
			if (lItemLength < 8)
			{
				lItemLength = 8;
			}
			lItemLength = lItemLength + 1;
			lWidth = lWidth - lItemLength;
			iReturnValue = mp_oControl.MathLib.RoundDouble(((((double)X - 17) * ((double)mp_lMax - (double)mp_lMin)) / (double)lWidth) + (double)mp_lMin);
			return iReturnValue;
		}

		internal E_SCROLLSTATE State 
		{
			get 
			{
				return mp_yState;
			}
			set 
			{
				mp_yState = value;
				switch (mp_yState)
				{
					case E_SCROLLSTATE.SS_CANTDISPLAY:
						mp_yState = E_SCROLLSTATE.SS_HIDDEN;
						this.Visible = false;
						break;
					case E_SCROLLSTATE.SS_NOTNEEDED:
						if (mp_oControl.ScrollBarBehaviour == E_SCROLLBEHAVIOUR.SB_DISABLE)
						{
							mp_yState = E_SCROLLSTATE.SS_SHOWN;
							this.Enabled = false;
							this.Visible = true;
						}
						else
						{
							mp_yState = E_SCROLLSTATE.SS_HIDDEN;
							this.Visible = false;
						}
						break;
					case E_SCROLLSTATE.SS_NEEDED:
						mp_yState = E_SCROLLSTATE.SS_SHOWN;
						this.Enabled = true;
						this.Visible = true;
						break;
				}
			}
		}

		internal object SaveViewState()
		{
			object[] oState = new object[4];
			oState[0] = mp_lMin;
			oState[1] = mp_lMax;
			oState[2] = mp_lValue;
			return oState;
		}

		internal void LoadViewState(object savedState)
		{
			object[] oState = (object[])savedState;
			mp_lMin = (int) oState[0];
			mp_lMax = (int) oState[1];
			mp_lValue = (int) oState[2];
		}

        public string StyleIndex
        {
            get
            {
                if (mp_sStyleIndex == "DS_SCROLLBAR")
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
                    value = "DS_SCROLLBAR";
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
            clsXML oXML = new clsXML(mp_oControl, "ScrollBar");
            oXML.InitializeWriter();
            oXML.WriteProperty("StyleIndex", mp_sStyleIndex);
            oXML.WriteObject(ArrowButtons.GetXML());
            oXML.WriteObject(ThumbButton.GetXML());
            return oXML.GetXML();
        }

        public void SetXML(string sXML)
        {
            clsXML oXML = new clsXML(mp_oControl, "ScrollBar");
            oXML.SetXML(sXML);
            oXML.InitializeReader();
            oXML.ReadProperty("StyleIndex", ref mp_sStyleIndex);
            StyleIndex = mp_sStyleIndex;
            ArrowButtons.SetXML(oXML.ReadObject("ArrowButtonState"));
            ThumbButton.SetXML(oXML.ReadObject("ThumbButtonState"));
        }

	}
}

