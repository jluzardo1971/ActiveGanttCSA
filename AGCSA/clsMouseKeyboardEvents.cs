using System;

namespace AGCSA
{
	internal class clsMouseKeyboardEvents
	{


		private ActiveGanttCSACtl mp_oControl;
		internal clsMouseKeyboardEvents(ActiveGanttCSACtl Value)
		{
			mp_oControl = Value;
		}

		internal void OnMouseClick(int X, int Y)
		{
			E_EVENTTARGET yEventTarget = E_EVENTTARGET.EVT_NONE;
			yEventTarget = CursorPosition(X, Y);
			mp_oControl.MouseEventArgs.X = X;
			mp_oControl.MouseEventArgs.Y = Y;
			mp_oControl.MouseEventArgs.EventTarget = yEventTarget;
			mp_oControl.MouseEventArgs.Cancel = false;
			mp_oControl.FireControlClick();
			if (mp_oControl.MouseEventArgs.Cancel == true) 
			{
				return;
			}
			switch (yEventTarget) 
			{
				case E_EVENTTARGET.EVT_VSCROLLBAR:
					mp_oControl.VerticalScrollBar.ScrollBar.ScrollBarClick(X, Y);
					break;
				case E_EVENTTARGET.EVT_HSCROLLBAR:
					mp_oControl.HorizontalScrollBar.ScrollBar.ScrollBarClick(X, Y);
					break;
				case E_EVENTTARGET.EVT_TIMELINESCROLLBAR:
					mp_oControl.CurrentViewObject.TimeLine.TimeLineScrollBar.ScrollBar.ScrollBarClick(X, Y);
					break;
				case E_EVENTTARGET.EVT_TREEVIEW:
					mp_oControl.Treeview.TreeviewClick(X, Y);
					break;
                case E_EVENTTARGET.EVT_TASK:
                    mp_oControl.SelectedTaskIndex = mp_oControl.MathLib.GetTaskIndexByPosition(X, Y);
                    break;
                case E_EVENTTARGET.EVT_PREDECESSOR:
                    mp_oControl.SelectedPredecessorIndex = mp_oControl.MathLib.GetPredecessorIndexByPosition(X, Y);
                    break;
                case E_EVENTTARGET.EVT_ROW:
                    mp_oControl.SelectedRowIndex = mp_oControl.MathLib.GetRowIndexByPosition(Y);
                    break;
                case E_EVENTTARGET.EVT_CELL:
                    mp_oControl.SelectedCellIndex = mp_oControl.MathLib.GetCellIndexByPosition(X);
                    mp_oControl.SelectedRowIndex = mp_oControl.MathLib.GetRowIndexByPosition(Y);
                    break;
                case E_EVENTTARGET.EVT_COLUMN:
                    mp_oControl.SelectedColumnIndex = mp_oControl.MathLib.GetColumnIndexByPosition(X, Y);
                    break;
                case E_EVENTTARGET.EVT_PERCENTAGE:
                    mp_oControl.SelectedPercentageIndex = mp_oControl.MathLib.GetPercentageIndexByPosition(X, Y);
                    break;
			}
		}

		private E_EVENTTARGET CursorPosition(int X, int Y)
		{
			if (mp_oControl.VerticalScrollBar.ScrollBar.OverControl(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_VSCROLLBAR;
			}
			else if (mp_oControl.HorizontalScrollBar.ScrollBar.OverControl(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_HSCROLLBAR;
			}
			else if (mp_oControl.CurrentViewObject.TimeLine.TimeLineScrollBar.ScrollBar.OverControl(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_TIMELINESCROLLBAR;
			}
			else if (mp_oControl.Treeview.OverControl(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_TREEVIEW;
			}
			else if (mp_bOverSplitter(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_SPLITTER;
			}
			else if (mp_bOverEmptySpace(Y) == true) 
			{
				return E_EVENTTARGET.EVT_NONE;
			}
			else if (mp_bOverTimeLine(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_TIMELINE;
			}
			else if (mp_bOverSelectedColumn(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_SELECTEDCOLUMN;
			}
			else if (mp_bOverColumn(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_COLUMN;
			}
			else if (mp_bOverSelectedRow(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_SELECTEDROW;
			}
            else if (mp_bOverCell(X, Y) == true)
            {
                return E_EVENTTARGET.EVT_CELL;
            }
			else if (mp_bOverRow(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_ROW;
			}
            else if (mp_bOverSelectedPercentage(X, Y) == true)
            {
                return E_EVENTTARGET.EVT_SELECTEDPERCENTAGE;
            }
            else if (mp_bOverPercentage(X, Y) == true)
            {
                return E_EVENTTARGET.EVT_PERCENTAGE;
            }
            else if (mp_bOverSelectedTask(X, Y) == true)
            {
                return E_EVENTTARGET.EVT_SELECTEDTASK;
            }
            else if (mp_bOverTask(X, Y) == true)
            {
                return E_EVENTTARGET.EVT_TASK;
            }
            else if (mp_bOverPredecessor(X, Y) == true)
            {
                return E_EVENTTARGET.EVT_PREDECESSOR;
            }
            else if (mp_bOverClientArea(X, Y) == true)
            {
                return E_EVENTTARGET.EVT_CLIENTAREA;
            }
            else
            {
                return E_EVENTTARGET.EVT_NONE;
            }
		}

		private bool mp_bOverSplitter(int X, int Y)
		{
            if (mp_oControl.Splitter.Width == 0)
            {
                return false;
            }
            if (X >= (mp_oControl.Splitter.Right - mp_oControl.Splitter.Width) & X <= mp_oControl.Splitter.Right & Y < mp_oControl.clsG.Height()) 
			{
				return true;
			}
			else 
			{
				return false;
			}
		}

		private bool mp_bOverEmptySpace(int Y)
		{
			if (Y > mp_oControl.Rows.TopOffset) 
			{
				return true;
			}
			else 
			{
				return false;
			}
		}

		private bool mp_bOverTimeLine(int X, int Y)
		{
			if (X >= mp_oControl.CurrentViewObject.TimeLine.f_lStart & X <= mp_oControl.CurrentViewObject.TimeLine.f_lEnd & Y <= mp_oControl.CurrentViewObject.TimeLine.Bottom & Y >= mp_oControl.mt_TopMargin) 
			{
				return true;
			}
			else 
			{
				return false;
			}
		}

		private bool mp_bOverSelectedColumn(int X, int Y)
		{
			clsColumn oColumn = null;
			if (mp_oControl.SelectedColumnIndex == 0 | mp_oControl.Columns.Count == 0) 
			{
				return false;
			}
			oColumn = (clsColumn)mp_oControl.Columns.oCollection.m_oReturnArrayElement(mp_oControl.SelectedColumnIndex);
			if (X >= oColumn.Left & X <= oColumn.Right & Y >= oColumn.Top & Y <= oColumn.Bottom) 
			{
				return true;
			}
			else 
			{
				return false;
			}
		}

		private bool mp_bOverColumn(int X, int Y)
		{
			clsColumn oColumn = null;
			int lIndex = 0;
			if (!(X <= mp_oControl.Splitter.Left & Y <= mp_oControl.CurrentViewObject.TimeLine.Bottom)) 
			{
				return false;
			}
			for (lIndex = 1; lIndex <= mp_oControl.Columns.Count; lIndex++) 
			{
				oColumn = (clsColumn)mp_oControl.Columns.oCollection.m_oReturnArrayElement(lIndex);
				if (oColumn.Visible == true) 
				{
					if (X >= oColumn.Left & X <= oColumn.Right & Y >= oColumn.Top & Y <= oColumn.Bottom) 
					{
						return true;
					}
				}
			}
			return false;
		}

		internal bool mp_bOverSelectedRow(int X, int Y)
		{
			clsRow oRow = null;
			if (mp_oControl.SelectedRowIndex == 0 | mp_oControl.Rows.Count == 0) 
			{
				return false;
			}
			oRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(mp_oControl.SelectedRowIndex);
			if (oRow.MergeCells == true) 
			{
				if (X >= oRow.Left & X <= oRow.Right & Y >= oRow.Top & Y <= oRow.Bottom) 
				{
					return true;
				}
				else 
				{
					return false;
				}
			}
			else 
			{
				if (X >= oRow.Left & X <= oRow.Right & Y >= oRow.Top & Y <= oRow.Bottom) 
				{
					if (mp_oControl.SelectedCellIndex == mp_oControl.MathLib.GetCellIndexByPosition(X)) 
					{
						return true;
					}
					else 
					{
						return false;
					}
				}
				else 
				{
					return false;
				}
			}
		}

		internal bool mp_bOverRow(int X, int Y)
		{
			clsRow oRow = null;
			int lIndex = 0;
			if (!(X <= mp_oControl.CurrentViewObject.TimeLine.f_lStart & Y > mp_oControl.CurrentViewObject.TimeLine.Bottom)) 
			{
				return false;
			}
			for (lIndex = 1; lIndex <= mp_oControl.Rows.Count; lIndex++) 
			{
				oRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(lIndex);
				if (oRow.Visible == true) 
				{
					if (X >= oRow.Left & X <= oRow.Right & Y >= oRow.Top & Y <= oRow.Bottom) 
					{
						return true;
					}
				}
			}
			return false;
		}

        internal bool mp_bOverCell(int X, int Y)
        {
            clsRow oRow = null;
            int lIndex = 0;
            if (!(X <= mp_oControl.CurrentViewObject.TimeLine.f_lStart & Y > mp_oControl.CurrentViewObject.TimeLine.Bottom))
            {
                return false;
            }
            for (lIndex = 1; lIndex <= mp_oControl.Rows.Count; lIndex++)
            {
                oRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(lIndex);
                if (oRow.Visible == true)
                {
                    if (X >= oRow.Left & X <= oRow.Right & Y >= oRow.Top & Y <= oRow.Bottom & oRow.MergeCells == false)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

		private bool mp_bOverSelectedTask(int X, int Y)
		{
			clsTask oSelectedTask = null;
			if (X < mp_oControl.CurrentViewObject.TimeLine.f_lStart) 
			{
				return false;
			}
			if (X > mp_oControl.CurrentViewObject.TimeLine.f_lEnd) 
			{
				return false;
			}
			if (mp_oControl.SelectedTaskIndex == 0) 
			{
				return false;
			}
			oSelectedTask = (clsTask)mp_oControl.Tasks.oCollection.m_oReturnArrayElement(mp_oControl.SelectedTaskIndex);
			if (X >= oSelectedTask.Left & X <= oSelectedTask.Right & Y >= oSelectedTask.Top & Y <= oSelectedTask.Bottom & mp_oControl.MathLib.InCurrentLayer(oSelectedTask.LayerIndex)) 
			{
				return true;
			}
			else 
			{
				return false;
			}
		}

        private bool mp_bOverSelectedPredecessor(int X, int Y)
        {
            clsPredecessor oSelectedPredecessor = null;
            if (X < mp_oControl.CurrentViewObject.TimeLine.f_lStart)
            {
                return false;
            }
            if (X > mp_oControl.CurrentViewObject.TimeLine.f_lEnd)
            {
                return false;
            }
            if (mp_oControl.SelectedPredecessorIndex == 0)
            {
                return false;
            }
            oSelectedPredecessor = (clsPredecessor)mp_oControl.Predecessors.oCollection.m_oReturnArrayElement(mp_oControl.SelectedPredecessorIndex);
            if (oSelectedPredecessor.HitTest(X, Y) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		private bool mp_bOverSelectedPercentage(int X, int Y)
		{
			clsPercentage oSelectedPercentage = null;
			if (X < mp_oControl.CurrentViewObject.TimeLine.f_lStart) 
			{
				return false;
			}
			if (X > mp_oControl.CurrentViewObject.TimeLine.f_lEnd) 
			{
				return false;
			}
			if (mp_oControl.SelectedPercentageIndex == 0) 
			{
				return false;
			}
			oSelectedPercentage = (clsPercentage)mp_oControl.Percentages.oCollection.m_oReturnArrayElement(mp_oControl.SelectedPercentageIndex);
			if (X >= oSelectedPercentage.Left & X <= oSelectedPercentage.RightSel & Y >= oSelectedPercentage.Top & Y <= oSelectedPercentage.Bottom) 
			{
				return true;
			}
			else 
			{
				return false;
			}
		}


		private E_AREA mp_yTaskArea(int X, int Y)
		{
			clsTask oSelectedTask = null;
			oSelectedTask = (clsTask)mp_oControl.Tasks.oCollection.m_oReturnArrayElement(mp_oControl.SelectedTaskIndex);
			if (X >= oSelectedTask.Left & X <= oSelectedTask.Right & Y >= oSelectedTask.Top & Y <= oSelectedTask.Bottom & mp_oControl.MathLib.InCurrentLayer(oSelectedTask.LayerIndex)) 
			{
				if (X >= oSelectedTask.Left & X <= oSelectedTask.Left + 2) 
				{
					if (oSelectedTask.f_bLeftVisible == true) 
					{
						return E_AREA.EA_LEFT;
					}
					else 
					{
						return E_AREA.EA_CENTER;
					}
				}
				if (X >= oSelectedTask.Right - 2 & X <= oSelectedTask.Right) 
				{
					if (oSelectedTask.f_bRightVisible == true) 
					{
						return E_AREA.EA_RIGHT;
					}
					else 
					{
						return E_AREA.EA_CENTER;
					}
				}
				return E_AREA.EA_CENTER;
			}
			return E_AREA.EA_NONE;
		}

		internal E_AREA mp_yRowArea(int X, int Y)
		{
			clsRow oSelectedRow = null;
			oSelectedRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(mp_oControl.SelectedRowIndex);
			if (Y >= oSelectedRow.Bottom & Y <= oSelectedRow.Bottom + 3) 
			{
				return E_AREA.EA_BOTTOM;
			}
			else 
			{
				return E_AREA.EA_CENTER;
			}
		}

		private E_AREA mp_yColumnArea(int X, int Y)
		{
			clsColumn oSelectedColumn = null;
			oSelectedColumn = (clsColumn)mp_oControl.Columns.oCollection.m_oReturnArrayElement(mp_oControl.SelectedColumnIndex);
			if (X >= (oSelectedColumn.Right - 3) & X <= oSelectedColumn.Right) 
			{
				return E_AREA.EA_RIGHT;
			}
			else 
			{
				return E_AREA.EA_CENTER;
			}
		}

		private bool mp_bOverTask(int X, int Y)
		{
			clsTask oTask = null;
			int lIndex = 0;
			for (lIndex = mp_oControl.Tasks.Count; lIndex >= 1; lIndex += -1) 
			{
				oTask = (clsTask)mp_oControl.Tasks.oCollection.m_oReturnArrayElement(lIndex);
				if (oTask.Visible == true & mp_oControl.MathLib.InCurrentLayer(oTask.LayerIndex)) 
				{
					if (X >= oTask.Left & X <= oTask.Right & Y >= oTask.Top & Y <= oTask.Bottom) 
					{
						return true;
					}
				}
			}
			return false;
		}

        private bool mp_bOverPredecessor(int X, int Y)
        {
            clsPredecessor oPredecessor = null;
            int lIndex = 0;
            for (lIndex = mp_oControl.Predecessors.Count; lIndex >= 1; lIndex += -1)
            {
                oPredecessor = (clsPredecessor)mp_oControl.Predecessors.oCollection.m_oReturnArrayElement(lIndex);
                if (oPredecessor.Visible == true)
                {
                    if (oPredecessor.HitTest(X, Y) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

		private bool mp_bOverPercentage(int X, int Y)
		{
			clsPercentage oPercentage = null;
			int lIndex = 0;
			for (lIndex = mp_oControl.Percentages.Count; lIndex >= 1; lIndex += -1) 
			{
				oPercentage = (clsPercentage)mp_oControl.Percentages.oCollection.m_oReturnArrayElement(lIndex);
				if (oPercentage.Visible == true) 
				{
					if (X >= oPercentage.Left & X <= oPercentage.RightSel & Y >= oPercentage.Top & Y <= oPercentage.Bottom) 
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool mp_bOverClientArea(int X, int Y)
		{
			if (X >= mp_oControl.CurrentViewObject.TimeLine.f_lStart & X <= mp_oControl.CurrentViewObject.TimeLine.f_lEnd & Y >= mp_oControl.CurrentViewObject.ClientArea.Top) 
			{
				return true;
			}
			else 
			{
				return false;
			}
		}


		private int mp_fSnapX(int X)
		{
			AGCSA.DateTime dtDate = new AGCSA.DateTime();
			if (mp_oControl.CurrentViewObject.ClientArea.Grid.SnapToGrid == false) 
			{
				return X;
			}
			dtDate = mp_oControl.MathLib.GetDateFromXCoordinate(X);
			dtDate = mp_oControl.MathLib.RoundDate(mp_oControl.CurrentViewObject.ClientArea.Grid.Interval, mp_oControl.CurrentViewObject.ClientArea.Grid.Factor, dtDate);
			return mp_oControl.MathLib.GetXCoordinateFromDate(dtDate);
		}



	}
}
