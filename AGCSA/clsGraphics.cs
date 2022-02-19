using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AGCSA
{



	using System.Drawing;

	internal class clsGraphics
	{
    
		private struct T_PRECT
		{
			public int lLeft;
			public int lTop;
			public int lRight;
			public int lBottom;
		}
    
		private ActiveGanttCSACtl mp_oControl;
		private T_PRECT mp_udtPreviousClipRegion;
		private System.Collections.ArrayList mp_audtActiveReversibleFrames;
		private System.Collections.ArrayList mp_audtActiveReversibleLinesStart;
		private System.Collections.ArrayList mp_audtActiveReversibleLinesEnd;
		private bool mp_bCustomPrinting;
		private Graphics mp_lCustomDC;
		private int mp_lPWidth;
		private int mp_lPHeight;
		private int mp_lFocusLeft;
		private int mp_lFocusTop;
		private int mp_lFocusRight;
		private int mp_lFocusBottom;
		private bool mp_bEnableClipRegions;
		internal bool bToolTipGraphics;
		private Graphics mp_oToolTipGraphics = null;
    
		//// ---------------------------------------------------------------------------------------------------------------------
		//// Construction/Destruction & Initialization
		//// ---------------------------------------------------------------------------------------------------------------------
    
		internal clsGraphics(ActiveGanttCSACtl Value) : base()
		{
			mp_oControl = Value;
			mp_audtActiveReversibleFrames = new System.Collections.ArrayList();
			mp_audtActiveReversibleLinesStart = new System.Collections.ArrayList();
			mp_audtActiveReversibleLinesEnd = new System.Collections.ArrayList();
			mp_bCustomPrinting = false;
			mp_bEnableClipRegions = true;
			bToolTipGraphics = false;
		}
 
    
		internal bool EnableClipRegions 
		{
			get { return mp_bEnableClipRegions; }
			set { mp_bEnableClipRegions = value; }
		}
    
		internal int f_FocusLeft 
		{
			get { return mp_lFocusLeft; }
			set { mp_lFocusLeft = value; }
		}
    
		internal int f_FocusTop 
		{
			get { return mp_lFocusTop; }
			set { mp_lFocusTop = value; }
		}
    
		internal int f_FocusRight 
		{
			get { return mp_lFocusRight; }
			set { mp_lFocusRight = value; }
		}
    
		internal int f_FocusBottom 
		{
			get { return mp_lFocusBottom; }
			set { mp_lFocusBottom = value; }
		}
    
		public Graphics oGraphics 
		{
			get 
			{
				if (mp_bCustomPrinting == false) 
				{
					if (bToolTipGraphics == false) 
					{
						return mp_oControl.f_HDC();
					}
					else 
					{
						return mp_oToolTipGraphics;
					}
				}
				else 
				{
					return mp_lCustomDC;
				}
			}
		}
    
		public bool CustomPrinting 
		{
			get { return mp_bCustomPrinting; }
			set { mp_bCustomPrinting = value; }
		}
    
		public Graphics CustomDC 
		{
			set { mp_lCustomDC = value; }
		}
    
		public int Width()
		{
			if (mp_bCustomPrinting == false) 
			{
				return mp_oControl.f_Width();
			}
			else 
			{
				return mp_lPWidth;
			}
		}
    
		public int Height()
		{
			if (mp_bCustomPrinting == false) 
			{
				return mp_oControl.f_Height();
			}
			else 
			{
				return mp_lPHeight;
			}
		}
    
		public int CustomWidth 
		{
			get { return mp_lPWidth; }
			set { mp_lPWidth = value; }
		}
    
		public int CustomHeight 
		{
			get { return mp_lPHeight; }
			set { mp_lPHeight = value; }
		}
    
		public void DrawPolygon(Color v_lColor, Point[] r_oPoints, int v_Len)
		{
			Pen hPen;
			hPen = new Pen(v_lColor);
			oGraphics.DrawPolygon(hPen, r_oPoints);
		}

        public void DrawEdge(int v_X1, int v_Y1, int v_X2, int v_Y2, Color clrBackColor, GRE_BUTTONSTYLE v_yButtonStyle, GRE_EDGETYPE v_lEdgeType, bool v_bFilled, clsStyle oStyle)
		{
			Color lExteriorLeftTopColor = Color.White;
			Color lInteriorLeftTopColor = Color.White;
			Color lExteriorRightBottomColor = Color.White;
			Color lInteriorRightBottomColor = Color.White;
			if (v_yButtonStyle == GRE_BUTTONSTYLE.BT_NORMALWINDOWS) 
			{
                switch (v_lEdgeType)
                {
                    case GRE_EDGETYPE.ET_RAISED:
                        if (oStyle == null)
                        {
                            lExteriorLeftTopColor = Color.FromArgb(255, 240, 240, 240);
                            lInteriorLeftTopColor = Color.FromArgb(255, 192, 192, 192);
                            lInteriorRightBottomColor = Color.Gray;
                            lExteriorRightBottomColor = Color.FromArgb(255, 64, 64, 64);
                        }
                        else
                        {
                            lExteriorLeftTopColor = oStyle.ButtonBorderStyle.RaisedExteriorLeftTopColor;
                            lInteriorLeftTopColor = oStyle.ButtonBorderStyle.RaisedInteriorLeftTopColor;
                            lInteriorRightBottomColor = oStyle.ButtonBorderStyle.RaisedInteriorRightBottomColor;
                            lExteriorRightBottomColor = oStyle.ButtonBorderStyle.RaisedExteriorRightBottomColor;
                        }
                        break;
                    case GRE_EDGETYPE.ET_SUNKEN:
                        if (oStyle == null)
                        {
                            lExteriorLeftTopColor = Color.Gray;
                            lInteriorLeftTopColor = Color.FromArgb(255, 64, 64, 64);
                            lInteriorRightBottomColor = Color.FromArgb(255, 192, 192, 192);
                            lExteriorRightBottomColor = Color.FromArgb(255, 240, 240, 240);
                        }
                        else
                        {
                            lExteriorLeftTopColor = oStyle.ButtonBorderStyle.SunkenExteriorLeftTopColor;
                            lInteriorLeftTopColor = oStyle.ButtonBorderStyle.SunkenInteriorLeftTopColor;
                            lInteriorRightBottomColor = oStyle.ButtonBorderStyle.SunkenInteriorRightBottomColor;
                            lExteriorRightBottomColor = oStyle.ButtonBorderStyle.SunkenExteriorRightBottomColor;
                        }
                        break;
                }
                // Exterior Left
                DrawLine(v_X1, v_Y1, v_X1, v_Y2, GRE_LINETYPE.LT_NORMAL, lExteriorLeftTopColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                // Exterior Top
                DrawLine(v_X1, v_Y1, v_X2, v_Y1, GRE_LINETYPE.LT_NORMAL, lExteriorLeftTopColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                // Exterior Right
                DrawLine(v_X2, v_Y2, v_X2, v_Y1, GRE_LINETYPE.LT_NORMAL, lExteriorRightBottomColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                // Exterior Bottom
                DrawLine(v_X1, v_Y2, v_X2, v_Y2, GRE_LINETYPE.LT_NORMAL, lExteriorRightBottomColor, GRE_LINEDRAWSTYLE.LDS_SOLID);

                // Interior Left
                DrawLine(v_X1 + 1, v_Y1 + 1, v_X1 + 1, v_Y2 - 1, GRE_LINETYPE.LT_NORMAL, lInteriorLeftTopColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                // Interior Top
                DrawLine(v_X1 + 1, v_Y1 + 1, v_X2 - 1, v_Y1 + 1, GRE_LINETYPE.LT_NORMAL, lInteriorLeftTopColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                // Interior Right
                DrawLine(v_X2 - 1, v_Y2 - 1, v_X2 - 1, v_Y1 + 1, GRE_LINETYPE.LT_NORMAL, lInteriorRightBottomColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                // Interior Bottom
                DrawLine(v_X1 + 1, v_Y2 - 1, v_X2 - 1, v_Y2 - 1, GRE_LINETYPE.LT_NORMAL, lInteriorRightBottomColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
				if (v_bFilled == true) 
				{
					DrawLine(v_X1 + 2, v_Y1 + 2, v_X2 - 2, v_Y2 - 2, GRE_LINETYPE.LT_FILLED, clrBackColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				}
			}
			else 
			{
                switch (v_lEdgeType)
                {
                    case GRE_EDGETYPE.ET_RAISED:
                        if (oStyle == null)
                        {
                            lExteriorLeftTopColor = Color.White;
                            lExteriorRightBottomColor = Color.FromArgb(255, 64, 64, 64);
                        }
                        else
                        {
                            lExteriorLeftTopColor = oStyle.ButtonBorderStyle.RaisedExteriorLeftTopColor;
                            lExteriorRightBottomColor = oStyle.ButtonBorderStyle.RaisedExteriorRightBottomColor;
                        }
                        break;
                    case GRE_EDGETYPE.ET_SUNKEN:
                        if (oStyle == null)
                        {
                            lExteriorLeftTopColor = Color.Gray;
                            lExteriorRightBottomColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            lExteriorLeftTopColor = oStyle.ButtonBorderStyle.SunkenExteriorLeftTopColor;
                            lExteriorRightBottomColor = oStyle.ButtonBorderStyle.SunkenExteriorRightBottomColor;
                        }
                        break;
                }
				DrawLine(v_X1, v_Y1, v_X2, v_Y1, GRE_LINETYPE.LT_NORMAL, lExteriorLeftTopColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				DrawLine(v_X1, v_Y1, v_X1, v_Y2, GRE_LINETYPE.LT_NORMAL, lExteriorLeftTopColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				DrawLine(v_X1, v_Y2, v_X2, v_Y2, GRE_LINETYPE.LT_NORMAL, lExteriorRightBottomColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				DrawLine(v_X2, v_Y2, v_X2, v_Y1 - 1, GRE_LINETYPE.LT_NORMAL, lExteriorRightBottomColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				if (v_bFilled == true) 
				{
					DrawLine(v_X1 + 1, v_Y1 + 1, v_X2 - 1, v_Y2 - 1, GRE_LINETYPE.LT_FILLED, clrBackColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				}
			}
		}

		public void DrawLine(int v_X1, int v_Y1, int v_X2, int v_Y2, GRE_LINETYPE v_yStyle, Color v_lColor, GRE_LINEDRAWSTYLE v_lDrawStyle)
		{
			DrawLine(v_X1, v_Y1, v_X2, v_Y2, v_yStyle, v_lColor, v_lDrawStyle, 1, true);
		}

        public void DrawLine(int v_X1, int v_Y1, int v_X2, int v_Y2, GRE_LINETYPE v_yStyle, Color v_lColor, GRE_LINEDRAWSTYLE v_lDrawStyle, int v_lWidth)
        {
            DrawLine(v_X1, v_Y1, v_X2, v_Y2, v_yStyle, v_lColor, v_lDrawStyle, v_lWidth, true);
        }
    
		public void DrawLine(int v_X1, int v_Y1, int v_X2, int v_Y2, GRE_LINETYPE v_yStyle, Color v_lColor, GRE_LINEDRAWSTYLE v_lDrawStyle, int v_lWidth, bool v_bCreatePens)
		{
			Pen mp_ucPen = new Pen(v_lColor, v_lWidth);
			SolidBrush mp_ucBrush = new SolidBrush(v_lColor);
			Point[] Points;
			switch (v_lDrawStyle) 
			{
				case GRE_LINEDRAWSTYLE.LDS_SOLID:
					mp_ucPen.DashStyle = DashStyle.Solid;
					break;
				case GRE_LINEDRAWSTYLE.LDS_DOT:
					mp_ucPen.DashStyle = DashStyle.Dot;
					break;
			}
			switch (v_yStyle) 
			{
				case GRE_LINETYPE.LT_NORMAL:
					Points = new Point[2];
					Points[0].X = v_X1;
					Points[0].Y = v_Y1;
					Points[1].X = v_X2;
					Points[1].Y = v_Y2;
					oGraphics.DrawPolygon(mp_ucPen, Points);
					break;
				case GRE_LINETYPE.LT_BORDER:
					Points = new Point[5];
					Points[0].X = v_X1;
					Points[0].Y = v_Y1;
					Points[1].X = v_X2;
					Points[1].Y = v_Y1;
					Points[2].X = v_X2;
					Points[2].Y = v_Y2;
					Points[3].X = v_X1;
					Points[3].Y = v_Y2;
					Points[4].X = v_X1;
					Points[4].Y = v_Y1;
					oGraphics.DrawPolygon(mp_ucPen, Points);
					break;
				case GRE_LINETYPE.LT_FILLED:
					Points = new Point[5];
					Points[0].X = v_X1;
					Points[0].Y = v_Y1;
					Points[1].X = v_X2 + 1;
					Points[1].Y = v_Y1;
					Points[2].X = v_X2 + 1;
					Points[2].Y = v_Y2 + 1;
					Points[3].X = v_X1;
					Points[3].Y = v_Y2 + 1;
					Points[4].X = v_X1;
					Points[4].Y = v_Y1;
					oGraphics.FillPolygon(mp_ucBrush, Points);
					break;
			}
		}
    
		public void DrawFigure(int v_X, int v_Y, int v_dx, int v_dy, GRE_FIGURETYPE v_yFigureType, Color v_lBorderColor, Color v_lFillColor, GRE_LINEDRAWSTYLE v_yBorderStyle)
		{
			Pen oPen = new Pen(v_lBorderColor);
			SolidBrush oBrush = new SolidBrush(v_lFillColor);
			PointF[] Points;
			if (v_dx % 2 != 0) 
			{
				v_dx = v_dx + 1;
				v_dy = v_dy + 1;
			}
			switch (v_yFigureType) 
			{
				case GRE_FIGURETYPE.FT_PROJECTUP:
					Points = new PointF[5];
					Points[0].X = v_X;
					Points[0].Y = v_Y;
					Points[1].X = v_X + v_dx / 2;
					Points[1].Y = v_Y + v_dy / 2;
					Points[2].X = v_X + v_dx / 2;
					Points[2].Y = v_Y + v_dy;
					Points[3].X = v_X - v_dx / 2;
					Points[3].Y = v_Y + v_dy;
					Points[4].X = v_X - v_dx / 2;
					Points[4].Y = v_Y + v_dy / 2;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_PROJECTDOWN:
					Points = new PointF[5];
					Points[0].X = v_X + v_dx / 2;
					Points[0].Y = v_Y;
					Points[1].X = v_X + v_dx / 2;
					Points[1].Y = v_Y + v_dy / 2;
					Points[2].X = v_X;
					Points[2].Y = v_Y + v_dy;
					Points[3].X = v_X - v_dx / 2;
					Points[3].Y = v_Y + v_dy / 2;
					Points[4].X = v_X - v_dx / 2;
					Points[4].Y = v_Y;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_DIAMOND:
					Points = new PointF[4];
					Points[0].X = v_X;
					Points[0].Y = v_Y;
					Points[1].X = v_X + v_dx / 2;
					Points[1].Y = v_Y + v_dy / 2;
					Points[2].X = v_X;
					Points[2].Y = v_Y + v_dy;
					Points[3].X = v_X - v_dx / 2;
					Points[3].Y = v_Y + v_dy / 2;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_CIRCLEDIAMOND:
					Points = new PointF[4];
					Points[0].X = v_X;
					Points[0].Y = v_Y + v_dy / 4;
					Points[1].X = v_X + v_dx / 4;
					Points[1].Y = v_Y + v_dy / 2;
					Points[2].X = v_X;
					Points[2].Y = v_Y + (3 * v_dy) / 4;
					Points[3].X = v_X - v_dx / 4;
					Points[3].Y = v_Y + v_dy / 2;
					oGraphics.DrawEllipse(oPen, mp_oControl.MathLib.RoundDouble(v_X - v_dx / 2), v_Y, v_dx, v_dy);
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_TRIANGLEUP:
					Points = new PointF[3];
					Points[0].X = v_X;
					Points[0].Y = v_Y;
					Points[1].X = v_X + v_dx / 2;
					Points[1].Y = v_Y + v_dy;
					Points[2].X = v_X - v_dx / 2;
					Points[2].Y = v_Y + v_dy;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_TRIANGLEDOWN:
					Points = new PointF[3];
					Points[0].X = v_X + v_dx / 2;
					Points[0].Y = v_Y;
					Points[1].X = v_X - v_dx / 2;
					Points[1].Y = v_Y;
					Points[2].X = v_X;
					Points[2].Y = v_Y + v_dy;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_TRIANGLERIGHT:
					Points = new PointF[3];
					Points[0].X = v_X;
					Points[0].Y = v_Y;
					Points[1].X = v_X;
					Points[1].Y = v_Y + v_dy;
					Points[2].X = v_X + v_dx;
					Points[2].Y = v_Y + v_dy / 2;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_TRIANGLELEFT:
					Points = new PointF[3];
					Points[0].X = v_X;
					Points[0].Y = v_Y;
					Points[1].X = v_X;
					Points[1].Y = v_Y + v_dy;
					Points[2].X = v_X - v_dx;
					Points[2].Y = v_Y + v_dy / 2;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_CIRCLETRIANGLEUP:
					Points = new PointF[3];
					Points[0].X = v_X;
					Points[0].Y = v_Y + v_dy / 4;
					Points[1].X = v_X + v_dx / 4;
					Points[1].Y = v_Y + (3 * v_dy) / 4;
					Points[2].X = v_X - v_dx / 4;
					Points[2].Y = v_Y + (3 * v_dy) / 4;
					oGraphics.DrawEllipse(oPen, mp_oControl.MathLib.RoundDouble(v_X - v_dx / 2), v_Y, v_dx, v_dy);
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_CIRCLETRIANGLEDOWN:
					Points = new PointF[3];
					Points[0].X = v_X - v_dx / 4;
					Points[0].Y = v_Y + v_dy / 4;
					Points[1].X = v_X + v_dx / 4;
					Points[1].Y = v_Y + v_dy / 4;
					Points[2].X = v_X;
					Points[2].Y = v_Y + (3 * v_dy) / 4;
					oGraphics.DrawEllipse(oPen, mp_oControl.MathLib.RoundDouble(v_X - v_dx / 2), v_Y, v_dx, v_dy);
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_ARROWUP:
					Points = new PointF[7];
					Points[0].X = v_X;
					Points[0].Y = v_Y;
					Points[1].X = v_X + v_dx / 2;
					Points[1].Y = v_Y + v_dy / 2;
					Points[2].X = v_X + v_dx / 4;
					Points[2].Y = v_Y + v_dy / 2;
					Points[3].X = v_X + v_dx / 4;
					Points[3].Y = v_Y + v_dy;
					Points[4].X = v_X - v_dx / 4;
					Points[4].Y = v_Y + v_dy;
					Points[5].X = v_X - v_dx / 4;
					Points[5].Y = v_Y + v_dy / 2;
					Points[6].X = v_X - v_dx / 2;
					Points[6].Y = v_Y + v_dy / 2;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_ARROWDOWN:
					Points = new PointF[7];
					Points[0].X = v_X - v_dx / 4;
					Points[0].Y = v_Y;
					Points[1].X = v_X + v_dx / 4;
					Points[1].Y = v_Y;
					Points[2].X = v_X + v_dx / 4;
					Points[2].Y = v_Y + v_dy / 2;
					Points[3].X = v_X + v_dx / 2;
					Points[3].Y = v_Y + v_dy / 2;
					Points[4].X = v_X;
					Points[4].Y = v_Y + v_dy;
					Points[5].X = v_X - v_dx / 2;
					Points[5].Y = v_Y + v_dy / 2;
					Points[6].X = v_X - v_dx / 4;
					Points[6].Y = v_Y + v_dy / 2;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_CIRCLEARROWUP:
					Points = new PointF[7];
					Points[0].X = v_X;
					Points[0].Y = v_Y + v_dy / 4;
					Points[1].X = v_X + v_dx / 4;
					Points[1].Y = v_Y + v_dy / 2;
					Points[2].X = v_X + v_dx / 8;
					Points[2].Y = v_Y + v_dy / 2;
					Points[3].X = v_X + v_dx / 8;
					Points[3].Y = v_Y + (3 * v_dy) / 4;
					Points[4].X = v_X - v_dx / 8;
					Points[4].Y = v_Y + (3 * v_dy) / 4;
					Points[5].X = v_X - v_dx / 8;
					Points[5].Y = v_Y + v_dy / 2;
					Points[6].X = v_X - v_dx / 4;
					Points[6].Y = v_Y + v_dy / 2;
					oGraphics.DrawEllipse(oPen, mp_oControl.MathLib.RoundDouble(v_X - v_dx / 2), v_Y, v_dx, v_dy);
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_CIRCLEARROWDOWN:
					Points = new PointF[7];
					Points[0].X = v_X - v_dx / 8;
					Points[0].Y = v_Y + v_dy / 4;
					Points[1].X = v_X + v_dx / 8;
					Points[1].Y = v_Y + v_dy / 4;
					Points[2].X = v_X + v_dx / 8;
					Points[2].Y = v_Y + v_dy / 2;
					Points[3].X = v_X + v_dx / 4;
					Points[3].Y = v_Y + v_dy / 2;
					Points[4].X = v_X;
					Points[4].Y = v_Y + (3 * v_dy) / 4;
					Points[5].X = v_X - v_dx / 4;
					Points[5].Y = v_Y + v_dy / 2;
					Points[6].X = v_X - v_dx / 8;
					Points[6].Y = v_Y + v_dy / 2;
					oGraphics.DrawEllipse(oPen, mp_oControl.MathLib.RoundDouble(v_X - v_dx / 2), v_Y, v_dx, v_dy);
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_SMALLPROJECTUP:
					Points = new PointF[5];
					Points[0].X = v_X;
					Points[0].Y = v_Y + v_dy / 2;
					Points[1].X = v_X + v_dx / 4;
					Points[1].Y = v_Y + (3 * v_dy) / 4;
					Points[2].X = v_X + v_dx / 4;
					Points[2].Y = v_Y + v_dy;
					Points[3].X = v_X - v_dx / 4;
					Points[3].Y = v_Y + v_dy;
					Points[4].X = v_X - v_dx / 4;
					Points[4].Y = v_Y + (3 * v_dy) / 4;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_SMALLPROJECTDOWN:
					Points = new PointF[5];
					Points[0].X = v_X + v_dx / 4;
					Points[0].Y = v_Y;
					Points[1].X = v_X + v_dx / 4;
					Points[1].Y = v_Y + v_dy / 4;
					Points[2].X = v_X;
					Points[2].Y = v_Y + v_dy / 2;
					Points[3].X = v_X - v_dx / 4;
					Points[3].Y = v_Y + v_dy / 4;
					Points[4].X = v_X - v_dx / 4;
					Points[4].Y = v_Y;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_RECTANGLE:
					Points = new PointF[4];
					Points[0].X = v_X - v_dx / 8;
					Points[0].Y = v_Y;
					Points[1].X = v_X + v_dx / 8;
					Points[1].Y = v_Y;
					Points[2].X = v_X + v_dx / 8;
					Points[2].Y = v_Y + v_dy;
					Points[3].X = v_X - v_dx / 8;
					Points[3].Y = v_Y + v_dy;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_SQUARE:
					Points = new PointF[4];
					Points[0].X = v_X - v_dx / 4;
					Points[0].Y = v_Y + v_dx / 4;
					Points[1].X = v_X + v_dx / 4;
					Points[1].Y = v_Y + v_dx / 4;
					Points[2].X = v_X + v_dx / 4;
					Points[2].Y = v_Y + (3 * v_dy) / 4;
					Points[3].X = v_X - v_dx / 4;
					Points[3].Y = v_Y + (3 * v_dy) / 4;
					mp_DrawFigureAux(oBrush, oPen, Points);
					break;
				case GRE_FIGURETYPE.FT_CIRCLE:
					oGraphics.FillEllipse(oBrush, (float)v_X - v_dx / 2, (float)v_Y, (float)v_dx, (float)v_dy);
					break;
				default:
					return;
            
			}
		}
    
		private void mp_DrawFigureAux(SolidBrush oBrush, Pen oPen, PointF[] oPoints)
		{
			oGraphics.FillPolygon(oBrush, oPoints);
			oGraphics.DrawPolygon(oPen, oPoints);
		}
    
		public void DrawPattern(int v_X1, int v_Y1, int v_X2, int v_Y2, Color v_lColor, GRE_PATTERN v_lDrawStyle, int v_iPatternFactor)
		{
			int tmp = 0;
			int c = 0;
			int c1 = 0;
			int c2 = 0;
			int i1 = 0;
			int j1 = 0;
			int i2 = 0;
			int j2 = 0;
			if (v_X1 > v_X2) 
			{
				tmp = v_X1;
				v_X1 = v_X2;
				v_X2 = tmp;
			}
			if (v_Y1 > v_Y2) 
			{
				tmp = v_Y1;
				v_Y1 = v_Y2;
				v_Y2 = tmp;
			}
			if (v_lDrawStyle == GRE_PATTERN.FP_HORIZONTALLINE | v_lDrawStyle == GRE_PATTERN.FP_CROSS) 
			{
				for (j1 = (v_Y1 + v_iPatternFactor); j1 <= v_Y2; j1 += v_iPatternFactor) 
				{
					DrawLine(v_X1, j1, v_X2, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				}
			}
			if (v_lDrawStyle == GRE_PATTERN.FP_VERTICALLINE | v_lDrawStyle == GRE_PATTERN.FP_CROSS) 
			{
				for (j1 = (v_X1 + v_iPatternFactor); j1 <= v_X2; j1 += v_iPatternFactor) 
				{
					DrawLine(j1, v_Y1, j1, v_Y2, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
				}
			}
			if (v_lDrawStyle == GRE_PATTERN.FP_UPWARDDIAGONAL | v_lDrawStyle == GRE_PATTERN.FP_DIAGONALCROSS) 
			{
				c1 = System.Convert.ToInt32((v_Y1 + v_X1) / v_iPatternFactor + 1);
				c2 = System.Convert.ToInt32((v_Y2 + v_X2) / v_iPatternFactor);
				for (c = c1; c <= c2; c++) 
				{
					i1 = v_X1;
					i2 = v_X2;
					j1 = c * v_iPatternFactor - i1;
					j2 = c * v_iPatternFactor - i2;
					if (j2 < v_Y1) 
					{
						i2 = c * v_iPatternFactor - v_Y1;
						j2 = c * v_iPatternFactor - i2;
					}
					if (j1 > v_Y2) 
					{
						i1 = c * v_iPatternFactor - v_Y2;
						j1 = c * v_iPatternFactor - i1;
					}
					DrawLine(i1, j1, i2, j2, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, false);
				}
			}
			if (v_lDrawStyle == GRE_PATTERN.FP_DOWNWARDDIAGONAL | v_lDrawStyle == GRE_PATTERN.FP_DIAGONALCROSS) 
			{
				c1 = System.Convert.ToInt32((v_Y1 - v_X2) / v_iPatternFactor + 1);
				c2 = System.Convert.ToInt32((v_Y2 - v_X1) / v_iPatternFactor);
				for (c = c1; c <= c2; c++) 
				{
					i1 = v_X1;
					i2 = v_X2;
					j1 = i1 + c * v_iPatternFactor;
					j2 = i2 + c * v_iPatternFactor;
					if (j1 < v_Y1) 
					{
						i1 = v_Y1 - c * v_iPatternFactor;
						j1 = i1 + c * v_iPatternFactor;
					}
					if (j2 > v_Y2) 
					{
						i2 = v_Y2 - c * v_iPatternFactor;
						j2 = i2 + c * v_iPatternFactor;
					}
					DrawLine(i1, j1, i2, j2, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, false);
				}
			}
			if (v_lDrawStyle == GRE_PATTERN.FP_LIGHT) 
			{
				for (j1 = (v_Y1 + 1); j1 <= (v_Y2 - 1); j1++) 
				{
					if (j1 % 2 == 0) 
					{
						for (j2 = (v_X1 + 1); j2 <= (v_X2 - 1); j2 += 4) 
						{
							DrawLine(j2, j1, j2 + 1, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
						}
					}
					else 
					{
						for (j2 = (v_X1 + 3); j2 <= (v_X2 - 1); j2 += 4) 
						{
							DrawLine(j2, j1, j2 + 1, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
						}
					}
				}
			}
			if (v_lDrawStyle == GRE_PATTERN.FP_MEDIUM) 
			{
				for (j1 = (v_Y1 + 1); j1 <= (v_Y2 - 1); j1++) 
				{
					if (j1 % 2 == 0) 
					{
						for (j2 = (v_X1 + 1); j2 <= (v_X2 - 1); j2 += 2) 
						{
							DrawLine(j2, j1, j2 + 1, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
						}
					}
					else 
					{
						for (j2 = (v_X1 + 2); j2 <= (v_X2 - 1); j2 += 2) 
						{
							DrawLine(j2, j1, j2 + 1, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
						}
					}
				}
			}
			if (v_lDrawStyle == GRE_PATTERN.FP_DARK) 
			{
				for (j1 = (v_Y1 + 1); j1 <= (v_Y2 - 1); j1++) 
				{
					if (j1 % 2 == 0) 
					{
						for (j2 = (v_X1 + 1); j2 <= (v_X2 - 1); j2 += 4) 
						{
							if (j2 + 3 < v_X2) 
							{
								DrawLine(j2, j1, j2 + 3, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
							}
							else 
							{
								DrawLine(j2, j1, v_X2, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
							}
						}
					}
					else 
					{
						DrawLine(v_X1, j1, v_X1 + 2, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
						for (j2 = (v_X1 + 3); j2 <= (v_X2 - 1); j2 += 4) 
						{
							if (j2 + 3 < v_X2) 
							{
								DrawLine(j2, j1, j2 + 3, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
							}
							else 
							{
								DrawLine(j2, j1, v_X2, j1, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID, 1, true);
							}
						}
					}
				}
			}
		}
    
		public void DrawPolyLine(Color v_lColor, int v_lWidth, GRE_LINEDRAWSTYLE v_lDrawStyle, Point[] r_oPoints, int v_Len)
		{
			Pen hPen;
			hPen = new Pen(v_lColor, v_lWidth);
			oGraphics.DrawLines(hPen, r_oPoints);
		}
    
		public void DrawTextEx(int v_X1, int v_Y1, int v_X2, int v_Y2, string v_sParam, StringFormat v_lFlags, Color v_lColor, Font v_oFont, bool v_bClip)
		{
			SolidBrush mp_ucBrush = new SolidBrush(v_lColor);
			RectangleF oRect = new RectangleF();
			oRect.X = v_X1;
			oRect.Y = v_Y1;
			oRect.Width = v_X2 - v_X1;
			oRect.Height = v_Y2 - v_Y1;
			oGraphics.DrawString(v_sParam, v_oFont, mp_ucBrush, oRect, v_lFlags);
		}
    
		public void DrawAlignedText(int v_lLeft, int v_lTop, int v_lRight, int v_lBottom, string v_sParam, GRE_HORIZONTALALIGNMENT v_yHPos, GRE_VERTICALALIGNMENT v_yVPos, Color v_lColor, Font v_oFont)
		{
			DrawAlignedText(v_lLeft, v_lTop, v_lRight, v_lBottom, v_sParam, v_yHPos, v_yVPos, v_lColor, v_oFont, true);
		}

		public void DrawAlignedText(int v_lLeft, int v_lTop, int v_lRight, int v_lBottom, string v_sParam, GRE_HORIZONTALALIGNMENT v_yHPos, GRE_VERTICALALIGNMENT v_yVPos, Color v_lColor, Font v_oFont,  bool v_bClip)
		{
			int lHeight = 0;
			int lWidth = 0;
			SolidBrush mp_ucBrush = new SolidBrush(v_lColor);
			lHeight = mp_oControl.mp_lStrHeight(v_sParam, v_oFont);
			lWidth = mp_oControl.mp_lStrWidth(v_sParam, v_oFont);
			RectangleF oRect = new RectangleF();
			StringFormat oFormat = new StringFormat();
			if (v_bClip == false & lWidth > (v_lRight - v_lLeft)) return; 
			if (v_bClip == false & lHeight > (v_lBottom - v_lTop)) return; 
			oRect.X = v_lLeft;
			oRect.Y = v_lTop;
			oRect.Width = v_lRight - v_lLeft;
			oRect.Height = v_lBottom - v_lTop;
			switch (v_yHPos) 
			{
				case GRE_HORIZONTALALIGNMENT.HAL_LEFT:
					oFormat.Alignment = StringAlignment.Near;
					break;
				case GRE_HORIZONTALALIGNMENT.HAL_CENTER:
					oFormat.Alignment = StringAlignment.Center;
					break;
				case GRE_HORIZONTALALIGNMENT.HAL_RIGHT:
					oFormat.Alignment = StringAlignment.Far;
					break;
			}
			switch (v_yVPos) 
			{
				case GRE_VERTICALALIGNMENT.VAL_TOP:
					oFormat.LineAlignment = StringAlignment.Near;
					break;
				case GRE_VERTICALALIGNMENT.VAL_CENTER:
					oFormat.LineAlignment = StringAlignment.Center;
					break;
				case GRE_VERTICALALIGNMENT.VAL_BOTTOM:
					oFormat.LineAlignment = StringAlignment.Far;
					break;
			}
			oGraphics.DrawString(v_sParam, v_oFont, mp_ucBrush, oRect, oFormat);
		}
    
		public void ClipRegion(int v_X1, int v_Y1, int v_X2, int v_Y2, bool v_bStore)
		{
			if ((mp_bEnableClipRegions == false)) 
			{
				return;
			}
			Rectangle oRect = new Rectangle(v_X1, v_Y1, (v_X2 - v_X1 + 1), (v_Y2 - v_Y1 + 1));
			Region oRegion = new Region(oRect);
			if (v_bStore == true) 
			{
				mp_udtPreviousClipRegion.lLeft = v_X1;
				mp_udtPreviousClipRegion.lRight = v_X2;
				mp_udtPreviousClipRegion.lTop = v_Y1;
				mp_udtPreviousClipRegion.lBottom = v_Y2;
			}
			oGraphics.Clip = oRegion;
		}
    
		public void RestorePreviousClipRegion()
		{
			if ((mp_bEnableClipRegions == false)) 
			{
				return;
			}
			ClipRegion(mp_udtPreviousClipRegion.lLeft, mp_udtPreviousClipRegion.lTop, mp_udtPreviousClipRegion.lRight, mp_udtPreviousClipRegion.lBottom, false);
		}
    
		public void ClearClipRegion()
		{
			oGraphics.ResetClip();
		}
    
		public void TileImageHorizontal(Image ImageHandle, int v_X1, int v_Y1, int v_X2, int v_Y2, bool v_bTransparent)
		{
			int X = 0;
			int lImageWidth = 0;
			int lImageHeight = 0;
			lImageHeight = ImageHandle.Height;
			lImageWidth = ImageHandle.Width;
			while (X < (v_X2 - v_X1)) 
			{
				if ((X + lImageWidth) > (v_X2 - v_X1)) 
				{
					PaintImage(ImageHandle, v_X2 - lImageWidth, v_Y1, v_X2, v_Y1 + lImageHeight, 0, 0, v_bTransparent);
				}
				else 
				{
					PaintImage(ImageHandle, v_X1 + X, v_Y1, v_X1 + X + lImageWidth, v_Y1 + lImageHeight, 0, 0, v_bTransparent);
				}
				X = X + lImageWidth;
			}
		}
    
		public void PaintImage(Image Image, int X1, int Y1, int X2, int Y2, int xOrigin, int yOrigin, bool bUseMask)
		{
			RectangleF DestRect = new RectangleF(0,0,0,0);
			RectangleF OriginRect = new RectangleF(0,0,0,0);
			OriginRect.X = xOrigin;
			OriginRect.Y = yOrigin;
			OriginRect.Width = Image.Width - xOrigin;
			OriginRect.Height = Image.Height - yOrigin;
			DestRect.X = X1;
			DestRect.Y = Y1;
			DestRect.Width = X2 - X1;
			DestRect.Height = Y2 - Y1;
			if (bUseMask == false) 
			{
				oGraphics.DrawImage(Image, DestRect, OriginRect, GraphicsUnit.Pixel);
			}
			else 
			{
				Bitmap oBitmap;
				oBitmap = (Bitmap) Image.Clone();
				oBitmap.MakeTransparent(Color.White);
				oGraphics.DrawImage(oBitmap, DestRect, OriginRect, GraphicsUnit.Pixel);
			}
		}
    
		public void DrawImage(Image v_oImage, GRE_HORIZONTALALIGNMENT v_yHorizontalAlignment, GRE_VERTICALALIGNMENT v_yVerticalAlignment, int v_lImageXMargin, int v_lImageYMargin, int v_lLeft, int v_lRight, int v_lTop, int v_lBottom, bool v_bTransparent
			)
		{
			bool bDrawImage = false;
			bool bHorizontalSmall = false;
			bool bVerticalSmall = false;
			int XOrigin = 0;
			int YOrigin = 0;
			int xDest = 0;
			int yDest = 0;
			int lxWidth = 0;
			int lyHeight = 0;
			int lImageHeight = 0;
			int lImageWidth = 0;
			if ((v_oImage == null)) 
			{
				return;
			}
			lImageHeight = v_oImage.Height;
			lImageWidth = v_oImage.Width;
			if (v_yHorizontalAlignment == GRE_HORIZONTALALIGNMENT.HAL_CENTER) 
			{
				v_lImageXMargin = 0;
			}
			if (v_yVerticalAlignment == GRE_VERTICALALIGNMENT.VAL_CENTER) 
			{
				v_lImageYMargin = 0;
			}
			bDrawImage = true;
			if ((v_lRight - v_lLeft) < (lImageWidth + v_lImageXMargin)) 
			{
				lxWidth = v_lRight - v_lLeft - v_lImageXMargin;
				if (lxWidth <= 0) bDrawImage = false; 
				bHorizontalSmall = true;
			}
			else 
			{
				lxWidth = lImageWidth;
				bHorizontalSmall = false;
			}
			if ((v_lBottom - v_lTop) < (lImageHeight + v_lImageYMargin)) 
			{
				lyHeight = v_lBottom - v_lTop - v_lImageYMargin;
				if (lyHeight <= 0) bDrawImage = false; 
				bVerticalSmall = true;
			}
			else 
			{
				lyHeight = lImageHeight;
				bVerticalSmall = false;
			}
			if (bHorizontalSmall == false) 
			{
				switch (v_yHorizontalAlignment) 
				{
					case GRE_HORIZONTALALIGNMENT.HAL_LEFT:
						xDest = v_lLeft + v_lImageXMargin;
						break;
					case GRE_HORIZONTALALIGNMENT.HAL_CENTER:
						xDest = ((v_lRight - v_lLeft) - lImageWidth) / 2 + v_lLeft;
						break;
					case GRE_HORIZONTALALIGNMENT.HAL_RIGHT:
						xDest = v_lRight - lImageWidth - v_lImageXMargin;
						break;
				}
				XOrigin = 0;
			}
			else 
			{
				switch (v_yHorizontalAlignment) 
				{
					case GRE_HORIZONTALALIGNMENT.HAL_LEFT:
						XOrigin = 0;
						xDest = v_lLeft + v_lImageXMargin;
						break;
					case GRE_HORIZONTALALIGNMENT.HAL_CENTER:
						XOrigin = (lImageWidth - lxWidth) / 2;
						xDest = v_lLeft;
						break;
					case GRE_HORIZONTALALIGNMENT.HAL_RIGHT:
						XOrigin = lImageWidth - lxWidth;
						xDest = v_lRight - lxWidth - v_lImageXMargin;
						break;
				}
			}
			if (bVerticalSmall == false) 
			{
				switch (v_yVerticalAlignment) 
				{
					case GRE_VERTICALALIGNMENT.VAL_TOP:
						yDest = v_lTop + v_lImageYMargin;
						break;
					case GRE_VERTICALALIGNMENT.VAL_CENTER:
						yDest = ((v_lBottom - v_lTop) - lImageHeight) / 2 + v_lTop;
						break;
					case GRE_VERTICALALIGNMENT.VAL_BOTTOM:
						yDest = v_lBottom - lImageHeight - v_lImageYMargin;
						break;
				}
				YOrigin = 0;
			}
			else 
			{
				switch (v_yVerticalAlignment) 
				{
					case GRE_VERTICALALIGNMENT.VAL_TOP:
						YOrigin = 0;
						yDest = v_lTop + v_lImageYMargin;
						break;
					case GRE_VERTICALALIGNMENT.VAL_CENTER:
						YOrigin = (lImageHeight - lyHeight) / 2;
						yDest = v_lTop;
						break;
					case GRE_VERTICALALIGNMENT.VAL_BOTTOM:
						YOrigin = lImageHeight - lyHeight;
						yDest = v_lBottom - lyHeight - v_lImageYMargin;
						break;
				}
			}
			if (bDrawImage == true) 
			{
				PaintImage(v_oImage, xDest, yDest, xDest + lxWidth, yDest + lyHeight, XOrigin, YOrigin, v_bTransparent);
			}
		}
    
		public void DrawFocusRectangle(int v_X1, int v_Y1, int v_X2, int v_Y2)
		{
			Rectangle myrect = new Rectangle(v_X1, v_Y1, v_X2 - v_X1, v_Y2 - v_Y1);
			System.Windows.Forms.ControlPaint.DrawFocusRectangle(oGraphics, myrect);
		}
    
		public void GradientFill(int v_X1, int v_Y1, int v_X2, int v_Y2, Color clrStartColor, Color clrEndColor, GRE_GRADIENTFILLMODE iGradientType)
		{
            if ((v_X2 - v_X1) <= 0)
            {
                return;
            }
            if ((v_Y2 - v_Y1) <= 0)
            {
                return;
            }
			RectangleF oRect = new RectangleF(v_X1, v_Y1, v_X2 - v_X1, v_Y2 - v_Y1);
			LinearGradientBrush mp_ucBrush = null;
			if ((iGradientType == GRE_GRADIENTFILLMODE.GDT_VERTICAL)) 
			{
				mp_ucBrush = new LinearGradientBrush(oRect, clrStartColor, clrEndColor, LinearGradientMode.Vertical);
			}
			else if ((iGradientType == GRE_GRADIENTFILLMODE.GDT_HORIZONTAL)) 
			{
				mp_ucBrush = new LinearGradientBrush(oRect, clrStartColor, clrEndColor, LinearGradientMode.Horizontal);
			}
			Point[] Points = new Point[5];
			Points[0].X = v_X1;
			Points[0].Y = v_Y1;
			Points[1].X = v_X2 + 1;
			Points[1].Y = v_Y1;
			Points[2].X = v_X2 + 1;
			Points[2].Y = v_Y2 + 1;
			Points[3].X = v_X1;
			Points[3].Y = v_Y2 + 1;
			Points[4].X = v_X1;
			Points[4].Y = v_Y1;
			oGraphics.FillPolygon(mp_ucBrush, Points);
		}
    
		public void HatchFill(int v_X1, int v_Y1, int v_X2, int v_Y2, Color clrForeColor, Color clrBackColor, GRE_HATCHSTYLE yHatchStyle)
		{
			HatchBrush mp_ucBrush;
			mp_ucBrush = new HatchBrush((HatchStyle)yHatchStyle, clrForeColor, clrBackColor);
			Point[] Points = new Point[5];
			Points[0].X = v_X1;
			Points[0].Y = v_Y1;
			Points[1].X = v_X2 + 1;
			Points[1].Y = v_Y1;
			Points[2].X = v_X2 + 1;
			Points[2].Y = v_Y2 + 1;
			Points[3].X = v_X1;
			Points[3].Y = v_Y2 + 1;
			Points[4].X = v_X1;
			Points[4].Y = v_Y1;
			oGraphics.FillPolygon(mp_ucBrush, Points);
		}
    
		public Color ConvertColor(int dwOleColor)
		{
			return ColorTranslator.FromOle(dwOleColor);
		}
    
		public bool LineIntersection(int X1, int Y1, int X2, int Y2)
		{
			return true;
		}
    
		internal void mp_DrawItemI(clsTask oTask, string sStyleIndex, bool Selected, clsStyle v_oStyle)
		{
			clsStyle oStyle;
			clsMilestoneStyle oMilestoneStyle;
			if ((v_oStyle == null)) 
			{
				if (mp_oControl.StrLib.StrIsNumeric(sStyleIndex)) 
				{
					if (mp_oControl.StrLib.StrCLng(sStyleIndex) < 0 | mp_oControl.StrLib.StrCLng(sStyleIndex) > mp_oControl.Styles.Count) 
					{
						mp_oControl.mp_ErrorReport(SYS_ERRORS.STYLE_INVALID_INDEX, "Style object element not found when preparing to draw, invalid index", "mp_DrawItemI");
						return;
					}
				}
				else 
				{
					if (mp_oControl.Styles.oCollection.m_bDoesKeyExist(sStyleIndex) == false) 
					{
                        mp_oControl.mp_ErrorReport(SYS_ERRORS.STYLE_INVALID_KEY, "Style object element not found when preparing to draw, invalid key (\"" + sStyleIndex + "\")", "mp_DrawItemI");
						return;
					}
				}
				oStyle = mp_oControl.Styles.FItem(sStyleIndex);
			}
			else 
			{
				oStyle = v_oStyle;
			}
			switch (oStyle.Appearance) 
			{
				case E_STYLEAPPEARANCE.SA_FLAT:
					oMilestoneStyle = oStyle.MilestoneStyle;
					DrawFigure(mp_oControl.MathLib.GetXCoordinateFromDate(oTask.StartDate), oTask.Top, oTask.Bottom - oTask.Top, oTask.Bottom - oTask.Top, oMilestoneStyle.ShapeIndex, oMilestoneStyle.BorderColor, oMilestoneStyle.FillColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
					break;
				case E_STYLEAPPEARANCE.SA_GRAPHICAL:
                    if (oStyle.MilestoneStyle.Image == null)
                    {
                    }
                    else
                    {
                        DrawImage(oStyle.MilestoneStyle.Image, oStyle.ImageAlignmentHorizontal, oStyle.ImageAlignmentVertical, oStyle.ImageXMargin, oStyle.ImageYMargin, oTask.Left, oTask.Right, oTask.Top, oTask.Bottom, oStyle.UseMask);
                    }
					break;
				default:
					oMilestoneStyle = oStyle.MilestoneStyle;
					DrawFigure(mp_oControl.MathLib.GetXCoordinateFromDate(oTask.StartDate), oTask.Top, oTask.Bottom - oTask.Top, oTask.Bottom - oTask.Top, oMilestoneStyle.ShapeIndex, oMilestoneStyle.BorderColor, oMilestoneStyle.FillColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
					break;
			}
			mp_DrawItemText(oTask.Left, oTask.Top, oTask.Right, oTask.Bottom, oTask.LeftTrim, oTask.RightTrim, oStyle, oTask.Text);
            if (oStyle.SelectionRectangleStyle.Visible == true & Selected)
            {
                if (oStyle.SelectionRectangleStyle.Mode == E_SELECTIONRECTANGLEMODE.SRM_DOTTED)
                {
                    DrawFocusRectangle(oTask.Left, oTask.Top, oTask.Right, oTask.Bottom);
                }
                else if (oStyle.SelectionRectangleStyle.Mode == E_SELECTIONRECTANGLEMODE.SRM_COLOR)
                {
                    DrawLine(oTask.Left, oTask.Top, oTask.Right, oTask.Bottom, GRE_LINETYPE.LT_BORDER, oStyle.SelectionRectangleStyle.Color, GRE_LINEDRAWSTYLE.LDS_SOLID, oStyle.SelectionRectangleStyle.BorderWidth);
                }
            }
		}

        internal void mp_DrawItemEx(int v_lLeft, int v_lTop, int v_lRight, int v_lBottom, string sText, bool v_bIsSelected, Image v_oImage, int v_lLeftTrim, int v_lRightTrim, clsStyle v_oStyle, Color clrBackColor, Color clrForeColor, Color clrStartGradientColor, Color clrEndGradientColor, Color clrHatchBackColor, Color clrHatchForeColor)
        {
            clsStyle oStyle;
            clsTaskStyle oTaskStyle;
            if ((v_oStyle == null))
            {
                mp_oControl.mp_ErrorReport(SYS_ERRORS.STYLE_NULL, "Style object is null when preparing to draw.", "mp_DrawItemEx");
                return;
            }
            else
            {
                oStyle = v_oStyle;
            }
            oTaskStyle = oStyle.TaskStyle;
            switch (oStyle.Appearance)
            {
                case E_STYLEAPPEARANCE.SA_RAISED:
                    DrawEdge(v_lLeft, v_lTop, v_lRight, v_lBottom, clrBackColor, oStyle.ButtonStyle, GRE_EDGETYPE.ET_RAISED, true, v_oStyle);
                    break;
                case E_STYLEAPPEARANCE.SA_SUNKEN:
                    DrawEdge(v_lLeft, v_lTop, v_lRight, v_lBottom, clrBackColor, oStyle.ButtonStyle, GRE_EDGETYPE.ET_SUNKEN, true, v_oStyle);
                    break;
                case E_STYLEAPPEARANCE.SA_FLAT:
                    int lTop = 0;
                    int lBottom = 0;
                    lTop = v_lTop;
                    lBottom = v_lBottom;
                    switch (oStyle.FillMode)
                    {
                        case GRE_FILLMODE.FM_COMPLETELYFILLED:
                            break;
                        case GRE_FILLMODE.FM_UPPERHALFFILLED:
                            lBottom = v_lTop + ((v_lBottom - v_lTop) / 2);
                            break;
                        case GRE_FILLMODE.FM_LOWERHALFFILLED:
                            lTop = v_lBottom - ((v_lBottom - v_lTop) / 2);
                            break;
                    }
                    if ((oStyle.BackgroundMode == GRE_BACKGROUNDMODE.FP_SOLID))
                    {
                        DrawLine(v_lLeft, lTop, v_lRight, lBottom, GRE_LINETYPE.LT_FILLED, clrBackColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                    }
                    else if ((oStyle.BackgroundMode == GRE_BACKGROUNDMODE.FP_GRADIENT))
                    {
                        GradientFill(v_lLeft, lTop, v_lRight, lBottom, clrStartGradientColor, clrEndGradientColor, oStyle.GradientFillMode);
                    }
                    else if ((oStyle.BackgroundMode == GRE_BACKGROUNDMODE.FP_PATTERN))
                    {
                        DrawPattern(v_lLeft, lTop, v_lRight, lBottom, clrBackColor, oStyle.Pattern, oStyle.PatternFactor);
                    }
                    else if ((oStyle.BackgroundMode == GRE_BACKGROUNDMODE.FP_HATCH))
                    {
                        HatchFill(v_lLeft, lTop, v_lRight, lBottom, clrHatchForeColor, clrHatchBackColor, oStyle.HatchStyle);
                    }
                    if (oStyle.BorderStyle == GRE_BORDERSTYLE.SBR_SINGLE)
                    {
                        DrawLine(v_lLeft, lTop, v_lRight, lBottom, GRE_LINETYPE.LT_BORDER, oStyle.BorderColor, GRE_LINEDRAWSTYLE.LDS_SOLID, oStyle.BorderWidth);
                    }
                    else if (oStyle.BorderStyle == GRE_BORDERSTYLE.SBR_CUSTOM)
                    {
                        if (oStyle.CustomBorderStyle.Left == true)
                        {
                            DrawLine(v_lLeft, lTop, v_lLeft, lBottom, GRE_LINETYPE.LT_NORMAL, oStyle.BorderColor, GRE_LINEDRAWSTYLE.LDS_SOLID, oStyle.BorderWidth);
                        }
                        if (oStyle.CustomBorderStyle.Top == true)
                        {
                            DrawLine(v_lLeft, lTop, v_lRight, lTop, GRE_LINETYPE.LT_NORMAL, oStyle.BorderColor, GRE_LINEDRAWSTYLE.LDS_SOLID, oStyle.BorderWidth);
                        }
                        if (oStyle.CustomBorderStyle.Right == true)
                        {
                            DrawLine(v_lRight, lTop, v_lRight, lBottom, GRE_LINETYPE.LT_NORMAL, oStyle.BorderColor, GRE_LINEDRAWSTYLE.LDS_SOLID, oStyle.BorderWidth);
                        }
                        if (oStyle.CustomBorderStyle.Bottom == true)
                        {
                            DrawLine(v_lLeft, lBottom, v_lRight, lBottom, GRE_LINETYPE.LT_NORMAL, oStyle.BorderColor, GRE_LINEDRAWSTYLE.LDS_SOLID, oStyle.BorderWidth);
                        }
                    }
                    DrawFigure(v_lRight, v_lTop, v_lBottom - v_lTop, v_lBottom - v_lTop, oTaskStyle.EndShapeIndex, oTaskStyle.EndBorderColor, oTaskStyle.EndFillColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                    DrawFigure(v_lLeft, v_lTop, v_lBottom - v_lTop, v_lBottom - v_lTop, oTaskStyle.StartShapeIndex, oTaskStyle.StartBorderColor, oTaskStyle.StartFillColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                    break;
                case E_STYLEAPPEARANCE.SA_CELL:
                    DrawLine(v_lLeft, v_lTop, v_lRight, v_lBottom, GRE_LINETYPE.LT_FILLED, clrBackColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                    DrawLine(v_lLeft, v_lBottom, v_lRight, v_lBottom, GRE_LINETYPE.LT_NORMAL, oStyle.BorderColor, GRE_LINEDRAWSTYLE.LDS_SOLID, oStyle.BorderWidth);
                    break;
                case E_STYLEAPPEARANCE.SA_GRAPHICAL:

                    if (oTaskStyle.MiddleImage == null | oTaskStyle.StartImage == null | oTaskStyle.EndImage == null)
                    {
                    }
                    else
                    {
                        int lImageHeight = 0;
                        int lImageWidth = 0;
                        lImageHeight = oTaskStyle.MiddleImage.Height;
                        lImageWidth = oTaskStyle.MiddleImage.Width;
                        TileImageHorizontal(oTaskStyle.MiddleImage, v_lLeft, v_lTop, v_lRight, v_lBottom, oStyle.UseMask);
                        //// Exit if the start and end sections don't fit
                        if ((v_lRight - v_lLeft) > (lImageWidth * 2))
                        {
                            //// Left Section
                            PaintImage(oTaskStyle.StartImage, v_lLeft, v_lTop, v_lLeft + lImageWidth, v_lTop + lImageHeight, 0, 0, oStyle.UseMask);
                            //// Right Section
                            PaintImage(oTaskStyle.EndImage, v_lRight - lImageWidth, v_lTop, v_lRight, v_lTop + lImageHeight, 0, 0, oStyle.UseMask);
                        }
                    }
                    break;
            }
            if ((v_oImage != null))
            {
                DrawImage(v_oImage, oStyle.ImageAlignmentHorizontal, oStyle.ImageAlignmentVertical, oStyle.ImageXMargin, oStyle.ImageYMargin, v_lLeft, v_lRight, v_lTop, v_lBottom, oStyle.UseMask);
            }
            mp_DrawItemText(v_lLeft, v_lTop, v_lRight, v_lBottom, v_lLeftTrim, v_lRightTrim, oStyle, sText);
            if (oStyle.SelectionRectangleStyle.Visible == true & v_bIsSelected)
            {
                mp_DrawSelectionRectangle(v_lLeft, v_lTop, v_lRight, v_lBottom, oStyle);
            }
        }

        internal void mp_DrawSelectionRectangle(int v_lLeft, int v_lTop, int v_lRight, int v_lBottom, clsStyle oStyle)
        {
            if (oStyle.SelectionRectangleStyle.Mode == E_SELECTIONRECTANGLEMODE.SRM_DOTTED)
            {
                DrawFocusRectangle(v_lLeft + oStyle.SelectionRectangleStyle.OffsetLeft, v_lTop + oStyle.SelectionRectangleStyle.OffsetTop, v_lRight - oStyle.SelectionRectangleStyle.OffsetRight, v_lBottom - oStyle.SelectionRectangleStyle.OffsetBottom);
            }
            else if (oStyle.SelectionRectangleStyle.Mode == E_SELECTIONRECTANGLEMODE.SRM_COLOR)
            {
                DrawLine(v_lLeft + oStyle.SelectionRectangleStyle.OffsetLeft, v_lTop + oStyle.SelectionRectangleStyle.OffsetTop, v_lRight - oStyle.SelectionRectangleStyle.OffsetRight, v_lBottom - oStyle.SelectionRectangleStyle.OffsetBottom, GRE_LINETYPE.LT_BORDER, oStyle.SelectionRectangleStyle.Color, GRE_LINEDRAWSTYLE.LDS_SOLID, oStyle.SelectionRectangleStyle.BorderWidth);
            }
        }

        internal void mp_DrawItem(int v_lLeft, int v_lTop, int v_lRight, int v_lBottom, string sStyleIndex, string sText, bool v_bIsSelected, Image v_oImage, int v_lLeftTrim, int v_lRightTrim, clsStyle v_oStyle)
        {
            clsStyle oStyle;
            if ((v_oStyle == null))
            {
                if (mp_oControl.StrLib.StrIsNumeric(sStyleIndex))
                {
                    if (mp_oControl.StrLib.StrCLng(sStyleIndex) < 0 | mp_oControl.StrLib.StrCLng(sStyleIndex) > mp_oControl.Styles.Count)
                    {
                        mp_oControl.mp_ErrorReport(SYS_ERRORS.STYLE_INVALID_INDEX, "Style object element not found when preparing to draw, invalid index", "mp_DrawItem");
                        return;
                    }
                }
                else
                {
                    if (mp_oControl.Styles.oCollection.m_bDoesKeyExist(sStyleIndex) == false)
                    {
                        mp_oControl.mp_ErrorReport(SYS_ERRORS.STYLE_INVALID_KEY, "Style object element not found when preparing to draw, invalid key (\"" + sStyleIndex + "\")", "mp_DrawItem");
                        return;
                    }
                }
                oStyle = mp_oControl.Styles.FItem(sStyleIndex);
            }
            else
            {
                oStyle = v_oStyle;
            }
            mp_DrawItemEx(v_lLeft, v_lTop, v_lRight, v_lBottom, sText, v_bIsSelected, v_oImage, v_lLeftTrim, v_lRightTrim, oStyle, oStyle.BackColor, oStyle.ForeColor, oStyle.StartGradientColor, oStyle.EndGradientColor, oStyle.HatchBackColor, oStyle.HatchForeColor);
        }
    
		private void mp_DrawItemText(int v_lLeft, int v_lTop, int v_lRight, int v_lBottom, int v_lLeftTrim, int v_lRightTrim, clsStyle oStyle, string sText)
		{
			int lTextLeft = 0;
			int lTextRight = 0;
			int lTextTop = 0;
			int lTextBottom = 0;
			if (oStyle.TextVisible == false) 
			{
				return;
			}
			if (sText == "") 
			{
				return;
			}
			switch (oStyle.TextPlacement) 
			{
				case E_TEXTPLACEMENT.SCP_OBJECTEXTENTSPLACEMENT:
					if ((oStyle.DrawTextInVisibleArea == false)) 
					{
						lTextLeft = v_lLeft;
						lTextRight = v_lRight;
					}
					else 
					{
						lTextLeft = v_lLeftTrim;
						lTextRight = v_lRightTrim;
					}

					lTextTop = v_lTop;
					lTextBottom = v_lBottom;
					if (oStyle.TextAlignmentHorizontal == GRE_HORIZONTALALIGNMENT.HAL_LEFT) 
					{
						lTextLeft = v_lLeft + oStyle.TextXMargin;
					}

					if (oStyle.TextAlignmentHorizontal == GRE_HORIZONTALALIGNMENT.HAL_RIGHT) 
					{
						lTextRight = v_lRight - oStyle.TextXMargin;
					}

					if (oStyle.TextAlignmentVertical == GRE_VERTICALALIGNMENT.VAL_TOP) 
					{
						lTextTop = v_lTop + oStyle.TextYMargin;
					}

					if (oStyle.TextAlignmentVertical == GRE_VERTICALALIGNMENT.VAL_BOTTOM) 
					{
						lTextBottom = v_lBottom - oStyle.TextYMargin;
					}

					DrawAlignedText(lTextLeft, lTextTop, lTextRight, lTextBottom, sText, oStyle.TextAlignmentHorizontal, oStyle.TextAlignmentVertical, oStyle.ForeColor, oStyle.Font, oStyle.ClipText);
					break;
				case E_TEXTPLACEMENT.SCP_OFFSETPLACEMENT:
					DrawTextEx(v_lLeft + oStyle.TextFlags.OffsetLeft, v_lTop + oStyle.TextFlags.OffsetTop, v_lRight - oStyle.TextFlags.OffsetRight, v_lBottom - oStyle.TextFlags.OffsetBottom, sText, oStyle.TextFlags.GetFlags(), oStyle.ForeColor, oStyle.Font, oStyle.ClipText);
					break;
				case E_TEXTPLACEMENT.SCP_EXTERIORPLACEMENT:
					if (oStyle.TextAlignmentHorizontal == GRE_HORIZONTALALIGNMENT.HAL_LEFT) 
					{
						lTextLeft = v_lLeft - mp_oControl.mp_lStrWidth(sText, oStyle.Font) - oStyle.TextXMargin;
						lTextRight = v_lLeft - oStyle.TextXMargin + 1;
					}

					if (oStyle.TextAlignmentHorizontal == GRE_HORIZONTALALIGNMENT.HAL_RIGHT) 
					{
						lTextLeft = v_lRight + oStyle.TextXMargin;
						lTextRight = v_lRight + mp_oControl.mp_lStrWidth(sText, oStyle.Font) + oStyle.TextXMargin + 1;
					}

					if (oStyle.TextAlignmentHorizontal == GRE_HORIZONTALALIGNMENT.HAL_CENTER) 
					{
						lTextLeft = v_lLeft;
						lTextRight = v_lRight + 1;
					}

					if (oStyle.TextAlignmentVertical == GRE_VERTICALALIGNMENT.VAL_TOP) 
					{
						lTextTop = v_lTop - mp_oControl.mp_lStrHeight(sText, oStyle.Font) - oStyle.TextYMargin;
						lTextBottom = v_lTop - oStyle.TextYMargin + 1;
					}

					if (oStyle.TextAlignmentVertical == GRE_VERTICALALIGNMENT.VAL_BOTTOM) 
					{
						lTextTop = v_lBottom + oStyle.TextYMargin;
						lTextBottom = v_lBottom + mp_oControl.mp_lStrHeight(sText, oStyle.Font) + oStyle.TextYMargin + 1;
					}

					if (oStyle.TextAlignmentVertical == GRE_VERTICALALIGNMENT.VAL_CENTER) 
					{
						lTextTop = v_lTop;
						lTextBottom = v_lBottom + 1;
					}
					DrawAlignedText(lTextLeft, lTextTop, lTextRight, lTextBottom, sText, GRE_HORIZONTALALIGNMENT.HAL_LEFT, GRE_VERTICALALIGNMENT.VAL_TOP, oStyle.ForeColor, oStyle.Font, oStyle.ClipText);
					break;
			}
		}
    
		internal bool mp_bPositionItem(ref int r_lTop, ref int r_lBottom, clsStyle v_oStyle)
		{
			if (v_oStyle.Placement == E_PLACEMENT.PLC_ROWEXTENTSPLACEMENT | v_oStyle.Appearance == E_STYLEAPPEARANCE.SA_CELL) 
			{
				return true;
			}
			if (v_oStyle.Placement == E_PLACEMENT.PLC_OFFSETPLACEMENT) 
			{
				int lTop = 0;
				int lBottom = 0;
				lTop = r_lTop;
				lBottom = r_lBottom;
				switch (v_oStyle.FillMode) 
				{
					case GRE_FILLMODE.FM_COMPLETELYFILLED:
						r_lTop = r_lTop + v_oStyle.OffsetTop;
						r_lBottom = r_lTop + v_oStyle.OffsetBottom;
						break;
					case GRE_FILLMODE.FM_UPPERHALFFILLED:
						r_lTop = r_lTop + v_oStyle.OffsetTop;
						r_lBottom = r_lTop + (v_oStyle.OffsetBottom / 2);
						break;
					case GRE_FILLMODE.FM_LOWERHALFFILLED:
						r_lTop = r_lTop + v_oStyle.OffsetTop + (v_oStyle.OffsetBottom / 2);
						r_lBottom = r_lTop + (v_oStyle.OffsetBottom / 2);
						break;
				}
				if (!(r_lTop > lTop & r_lTop < lBottom)) 
				{
					return false;
				}
				return true;
			}
			return false;
		}

        internal void DrawPoint(int X, int Y, Color clrColor)
        {
            SolidBrush mp_ucBrush = new SolidBrush(clrColor);
            oGraphics.FillRectangle(mp_ucBrush, X, Y, 1, 1);
        }

        internal void mp_DrawArrow(int v_X, int v_Y, GRE_ARROWDIRECTION v_ArrowDirection, int v_ArrowSize, Color v_lColor)
        {
            int i = 0;
            switch (v_ArrowDirection)
            {
                case GRE_ARROWDIRECTION.AWD_LEFT:
                    DrawPoint(v_X, v_Y, v_lColor);
                    for (i = 1; i <= v_ArrowSize; i++)
                    {
                        v_X = v_X + 1;
                        DrawLine(v_X, v_Y - i, v_X, v_Y + i, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                    }

                    break;
                case GRE_ARROWDIRECTION.AWD_RIGHT:
                    DrawPoint(v_X, v_Y, v_lColor);
                    for (i = 1; i <= v_ArrowSize; i++)
                    {
                        v_X = v_X - 1;
                        DrawLine(v_X, v_Y - i, v_X, v_Y + i, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                    }

                    break;
                case GRE_ARROWDIRECTION.AWD_UP:
                    DrawPoint(v_X, v_Y, v_lColor);
                    for (i = 1; i <= v_ArrowSize; i++)
                    {
                        v_Y = v_Y + 1;
                        DrawLine(v_X - i, v_Y, v_X + i, v_Y, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                    }

                    break;
                case GRE_ARROWDIRECTION.AWD_DOWN:
                    DrawPoint(v_X, v_Y, v_lColor);
                    for (i = 1; i <= v_ArrowSize; i++)
                    {
                        v_Y = v_Y - 1;
                        DrawLine(v_X - i, v_Y, v_X + i, v_Y, GRE_LINETYPE.LT_NORMAL, v_lColor, GRE_LINEDRAWSTYLE.LDS_SOLID);
                    }

                    break;
            }
        }







	}
}

