using System;
using System.Drawing;

namespace AGCSA
{
	public class clsDrawing
	{
    
		private ActiveGanttCSACtl mp_oControl;
    
		internal clsDrawing(ActiveGanttCSACtl Value)
		{
			mp_oControl = Value;
		}
    
		public Graphics GraphicsInfo()
		{
			return mp_oControl.clsG.oGraphics;
		}
    
		public void DrawLine(int X1, int Y1, int X2, int Y2, Color LineColor, GRE_LINEDRAWSTYLE LineStyle, int LineWidth)
		{
			mp_oControl.clsG.DrawLine(X1, Y1, X2, Y2, GRE_LINETYPE.LT_NORMAL, LineColor, LineStyle, LineWidth, true);
		}
    
		public void DrawBorder(int X1, int Y1, int X2, int Y2, Color LineColor, GRE_LINEDRAWSTYLE LineStyle, int LineWidth)
		{
			mp_oControl.clsG.DrawLine(X1, Y1, X2, Y2, GRE_LINETYPE.LT_BORDER, LineColor, LineStyle, LineWidth, true);
		}
    
		public void DrawRectangle(int X1, int Y1, int X2, int Y2, Color LineColor, GRE_LINEDRAWSTYLE LineStyle, int LineWidth)
		{
			mp_oControl.clsG.DrawLine(X1, Y1, X2, Y2, GRE_LINETYPE.LT_FILLED, LineColor, LineStyle, LineWidth, true);
		}
    
		public void DrawText(int X1, int Y1, int X2, int Y2, string Text, StringFormat TextFlags, Color TextColor, Font TextFont)
		{
			mp_oControl.clsG.DrawTextEx(X1, Y1, X2, Y2, Text, TextFlags, TextColor, TextFont, true);
		}
    
		public void DrawAlignedText(int X1, int Y1, int X2, int Y2, string Text, GRE_HORIZONTALALIGNMENT HorizontalAlignment, GRE_VERTICALALIGNMENT VerticalAlignment, Color TextColor, Font TextFont)
		{
			mp_oControl.clsG.DrawAlignedText(X1, Y1, X2, Y2, Text, HorizontalAlignment, VerticalAlignment, TextColor, TextFont, true);
		}
    
		public void PaintImage(Image Image, int X1, int Y1, int X2, int Y2)
		{
			mp_oControl.clsG.PaintImage(Image, X1, Y1, X2, Y2, 0, 0, true);
		}
    
		//Public Sub ClearClipRegion()
		// mp_oControl.clsG.ClearClipRegion()
		//End Sub
    
	}
}

