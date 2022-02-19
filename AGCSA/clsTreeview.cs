using System;
using System.Drawing;

namespace AGCSA
{
	public class clsTreeview
	{

		private struct S_CHECKBOXCLICK
		{
			public int lNodeIndex;
			public void Clear()
			{
				lNodeIndex = 0;
			}
		}

		private struct S_SIGNCLICK
		{
			public int lNodeIndex;
			public void Clear()
			{
				lNodeIndex = 0;
			}
		}

		private struct S_ROWMOVEMENT
		{
			public int lRowIndex;
			public int lDestinationRowIndex;
			public void Clear()
			{
				lRowIndex = 0;
				lDestinationRowIndex = 0;
			}
		}

		private struct S_ROWSIZING
		{
			public int lRowIndex;
			public void Clear()
			{
				lRowIndex = 0;
			}
		}

		private struct S_ROWSELECTION
		{
			public int lRowIndex;
			public int lCellIndex;
			public void Clear()
			{
				lRowIndex = 0;
				lCellIndex = 0;
			}
		}

		private ActiveGanttCSACtl mp_oControl;
		private int mp_lLastVisibleNode;
		private int mp_lIndentation;
		private Color mp_clrBackColor;
		private Color mp_clrCheckBoxBorderColor;
		private Color mp_clrCheckBoxColor;
		private Color mp_clrCheckBoxMarkColor;
		private Color mp_clrSelectedBackColor;
		private Color mp_clrSelectedForeColor;
		private Color mp_clrTreeLineColor;
		private Color mp_clrPlusMinusBorderColor;
		private Color mp_clrPlusMinusSignColor;
		private bool mp_bCheckBoxes;
		private bool mp_bTreeLines;
		private bool mp_bImages;
		private bool mp_bPlusMinusSigns;
		private bool mp_bFullColumnSelect;
		private bool mp_bExpansionOnSelection;
		private string mp_sPathSeparator;
		private S_CHECKBOXCLICK s_chkCLK;
		private S_SIGNCLICK s_sgnCLK;

		private S_ROWSELECTION s_rowSEL;
		public clsTreeview(ActiveGanttCSACtl Value)
		{
			mp_oControl = Value;
			mp_lLastVisibleNode = 0;
			mp_lIndentation = 20;
			mp_clrBackColor = Color.White;
			mp_clrCheckBoxBorderColor = Color.Gray;
			mp_clrCheckBoxColor = Color.White;
			mp_clrCheckBoxMarkColor = Color.Black;
			mp_clrSelectedBackColor = Color.Blue;
			mp_clrSelectedForeColor = Color.White;
			mp_clrTreeLineColor = Color.Gray;
			mp_clrPlusMinusBorderColor = Color.Gray;
			mp_clrPlusMinusSignColor = Color.Black;
			mp_bCheckBoxes = false;
			mp_bTreeLines = true;
			mp_bImages = true;
			mp_bPlusMinusSigns = true;
			mp_bFullColumnSelect = false;
			mp_bExpansionOnSelection = false;
			mp_sPathSeparator = "/";
		}

		internal void TreeviewClick(int X, int Y)
		{
			E_EVENTTARGET yEventTarget = E_EVENTTARGET.EVT_NONE;
			yEventTarget = CursorPosition(X, Y);
			switch (yEventTarget) 
			{
				case E_EVENTTARGET.EVT_TREEVIEWCHECKBOX:
					mp_EO_CHECKBOXCLICK(X, Y);
					break;
				case E_EVENTTARGET.EVT_TREEVIEWSIGN:
					mp_EO_SIGNCLICK(X, Y);
					break;
				case E_EVENTTARGET.EVT_ROW:
					mp_EO_ROWSELECTION(X, Y);
					break;
			}
		}

		internal bool OverControl(int X, int Y)
		{
			clsRow oRow = null;
			int lIndex = 0;
            if (mp_oControl.TreeviewColumnIndex == 0) 
			{
				return false;
			}
            if (!(X >= LeftTrim & X <= RightTrim))
			{
				return false;
			}
			for (lIndex = 1; lIndex <= mp_oControl.Rows.Count; lIndex++) 
			{
				oRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(lIndex);
				if (oRow.Visible == true) 
				{
					if (Y >= oRow.Top & Y <= oRow.Bottom) 
					{
						return true;
					}
				}
			}
			return false;
		}

		private E_EVENTTARGET CursorPosition(int X, int Y)
		{
			if (mp_bOverCheckBox(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_TREEVIEWCHECKBOX;
			}
			else if (mp_bOverPlusMinusSign(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_TREEVIEWSIGN;
			}
			else if (mp_oControl.MouseKeyboardEvents.mp_bOverSelectedRow(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_SELECTEDROW;
			}
			else if (mp_oControl.MouseKeyboardEvents.mp_bOverRow(X, Y) == true) 
			{
				return E_EVENTTARGET.EVT_ROW;
			}
			return E_EVENTTARGET.EVT_NONE;
		}

		private bool mp_bOverCheckBox(int X, int Y)
		{
			int lIndex = 0;
			clsNode oNode = null;
			clsRow oRow = null;
			bool bReturn = false;
			if (mp_bCheckBoxes == false) 
			{
				return false;
			}
			bReturn = false;
			for (lIndex = 1; lIndex <= mp_oControl.Rows.Count; lIndex++) 
			{
				oRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(lIndex);
				oNode = oRow.Node;
                if (oRow.ClientAreaVisibility == E_CLIENTAREAVISIBILITY.VS_INSIDEVISIBLEAREA & X >= (oNode.CheckBoxLeft) & X <= (oNode.CheckBoxLeft + 13) & Y <= (oNode.YCenter + 6) & Y >= (oNode.YCenter - 7)) 
				{
					bReturn = true;
				}
			}
			return bReturn;
		}

		private bool mp_bOverPlusMinusSign(int X, int Y)
		{
			int lIndex = 0;
			clsNode oNode = null;
			clsRow oRow = null;
			bool bReturn = false;
			if (mp_bPlusMinusSigns == false) 
			{
				return false;
			}
			bReturn = false;
			for (lIndex = 1; lIndex <= mp_oControl.Rows.Count; lIndex++) 
			{
				oRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(lIndex);
				oNode = oRow.Node;
                if (oRow.ClientAreaVisibility == E_CLIENTAREAVISIBILITY.VS_INSIDEVISIBLEAREA & X >= (oNode.Left - 5) & X <= (oNode.Left + 5) & Y <= (oNode.YCenter + 5) & Y >= (oNode.YCenter - 5)) 
				{
					bReturn = true;
				}
			}
			return bReturn;
		}

		private void mp_EO_ROWSELECTION(int X, int Y)
		{
			clsRow oRow = null;
			s_rowSEL.lRowIndex = mp_oControl.MathLib.GetRowIndexByPosition(Y);
			s_rowSEL.lCellIndex = mp_oControl.MathLib.GetCellIndexByPosition(X);
			mp_oControl.SelectedRowIndex = s_rowSEL.lRowIndex;
			mp_oControl.SelectedCellIndex = s_rowSEL.lCellIndex;
			oRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(mp_oControl.SelectedRowIndex);
			if (oRow.MergeCells == true) 
			{
				mp_oControl.ObjectSelectedEventArgs.Clear();
				mp_oControl.ObjectSelectedEventArgs.EventTarget = E_EVENTTARGET.EVT_ROW;
				mp_oControl.ObjectSelectedEventArgs.ObjectIndex = mp_oControl.SelectedRowIndex;
				mp_oControl.FireObjectSelected();
			}
			else 
			{
				mp_oControl.ObjectSelectedEventArgs.Clear();
				mp_oControl.ObjectSelectedEventArgs.EventTarget = E_EVENTTARGET.EVT_CELL;
				mp_oControl.ObjectSelectedEventArgs.ObjectIndex = mp_oControl.SelectedCellIndex;
				mp_oControl.ObjectSelectedEventArgs.ParentObjectIndex = mp_oControl.SelectedRowIndex;
				mp_oControl.FireObjectSelected();
			}
		}

		private void mp_EO_CHECKBOXCLICK(int X, int Y)
		{
			clsRow oRow = null;
			clsNode oNode = null;
			s_chkCLK.lNodeIndex = mp_oControl.MathLib.GetNodeIndexByCheckBoxPosition(X, Y);
			oRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(s_chkCLK.lNodeIndex);
			oNode = oRow.Node;
			oNode.Checked = !oNode.Checked;
			mp_oControl.NodeEventArgs.Clear();
			mp_oControl.NodeEventArgs.Index = s_chkCLK.lNodeIndex;
			mp_oControl.FireNodeChecked();
		}

		private void mp_EO_SIGNCLICK(int X, int Y)
		{
			clsRow oRow = null;
			clsNode oNode = null;
			s_sgnCLK.lNodeIndex = mp_oControl.MathLib.GetNodeIndexBySignPosition(X, Y);
			oRow = (clsRow)mp_oControl.Rows.oCollection.m_oReturnArrayElement(s_sgnCLK.lNodeIndex);
			oNode = oRow.Node;
			oNode.Expanded = !oNode.Expanded;
			mp_oControl.NodeEventArgs.Clear();
			mp_oControl.NodeEventArgs.Index = s_sgnCLK.lNodeIndex;
			mp_oControl.FireNodeExpanded();
		}

		internal void Draw()
		{
            if (mp_oControl.TreeviewColumnIndex == 0)
            {
                return;
            }
            if (mp_oControl.Columns.Item(mp_oControl.TreeviewColumnIndex.ToString()).Visible == false)
            {
                return;
            }
            mp_oControl.clsG.ClipRegion(LeftTrim, mp_oControl.CurrentViewObject.ClientArea.Top, RightTrim, mp_oControl.clsG.Height() - mt_BorderThickness - 1, false);
            mp_oControl.Rows.NodesDrawBackground();
            mp_oControl.clsG.ClipRegion(LeftTrim, mp_oControl.CurrentViewObject.ClientArea.Top, RightTrim - 2, mp_oControl.clsG.Height() - mt_BorderThickness - 1, false);
            mp_oControl.Rows.NodesDraw();
            mp_oControl.Rows.NodesDrawTreeLines();
            mp_oControl.Rows.NodesDrawElements();
            mp_oControl.clsG.ClearClipRegion();
		}

		internal int f_FirstVisibleNode 
		{
			get 
			{
				if (mp_oControl.Rows.Count == 0) 
				{
					return 0;
				}
				else 
				{
					return mp_oControl.VerticalScrollBar.Value;
				}
			}
		}

		public int FirstVisibleNode 
		{
			get 
			{
				if (mp_oControl.Rows.Count == 0) 
				{
					return 0;
				}
				else 
				{
					return mp_oControl.Rows.RealFirstVisibleRow;
				}
			}
			set 
			{
				if (value < 1) 
				{
					value = 1;
				}
				else if (((value > mp_oControl.Rows.Count) & (mp_oControl.Rows.Count != 0))) 
				{
					value = mp_oControl.Rows.Count;
				}
				mp_oControl.VerticalScrollBar.Value = value;
			}
		}

		public int LastVisibleNode 
		{
			get { return mp_lLastVisibleNode; }
		}

		internal int f_LastVisibleNode 
		{
			set { mp_lLastVisibleNode = value; }
		}

		internal int mt_BorderThickness 
		{
			get { return mp_oControl.mt_BorderThickness; }
		}

		public int Indentation 
		{
			get { return mp_lIndentation; }
			set { mp_lIndentation = value; }
		}

		public void ClearSelections()
		{
			mp_oControl.SelectedRowIndex = 0;
		}

		public Color CheckBoxBorderColor 
		{
			get { return mp_clrCheckBoxBorderColor; }
			set { mp_clrCheckBoxBorderColor = value; }
		}

		public Color CheckBoxColor 
		{
			get { return mp_clrCheckBoxColor; }
			set { mp_clrCheckBoxColor = value; }
		}

		public Color CheckBoxMarkColor 
		{
			get { return mp_clrCheckBoxMarkColor; }
			set { mp_clrCheckBoxMarkColor = value; }
		}

		public Color BackColor 
		{
			get { return mp_clrBackColor; }
			set { mp_clrBackColor = value; }
		}

		public string PathSeparator 
		{
			get { return mp_sPathSeparator; }
			set { mp_sPathSeparator = value; }
		}

		public bool TreeLines 
		{
			get { return mp_bTreeLines; }
			set { mp_bTreeLines = value; }
		}

		public bool PlusMinusSigns 
		{
			get { return mp_bPlusMinusSigns; }
			set { mp_bPlusMinusSigns = value; }
		}

		public bool Images 
		{
			get { return mp_bImages; }
			set { mp_bImages = value; }
		}

		public bool CheckBoxes 
		{
			get { return mp_bCheckBoxes; }
			set { mp_bCheckBoxes = value; }
		}

		public bool FullColumnSelect 
		{
			get { return mp_bFullColumnSelect; }
			set { mp_bFullColumnSelect = value; }
		}

		public bool ExpansionOnSelection 
		{
			get { return mp_bExpansionOnSelection; }
			set { mp_bExpansionOnSelection = value; }
		}

		public Color SelectedBackColor 
		{
			get { return mp_clrSelectedBackColor; }
			set { mp_clrSelectedBackColor = value; }
		}

		public Color SelectedForeColor 
		{
			get { return mp_clrSelectedForeColor; }
			set { mp_clrSelectedForeColor = value; }
		}

		public Color TreeLineColor 
		{
			get { return mp_clrTreeLineColor; }
			set { mp_clrTreeLineColor = value; }
		}

		public Color PlusMinusBorderColor 
		{
			get { return mp_clrPlusMinusBorderColor; }
			set { mp_clrPlusMinusBorderColor = value; }
		}

		public Color PlusMinusSignColor 
		{
			get { return mp_clrPlusMinusSignColor; }
			set { mp_clrPlusMinusSignColor = value; }
		}

        internal int Left
        {
            get
            {
                if (mp_oControl.TreeviewColumnIndex == 0)
                {
                    return 0;
                }
                return mp_oControl.Columns.Item(mp_oControl.TreeviewColumnIndex.ToString()).Left;
            }
        }

        internal int Right
        {
            get
            {
                if (mp_oControl.TreeviewColumnIndex == 0)
                {
                    return 0;
                }
                return mp_oControl.Columns.Item(mp_oControl.TreeviewColumnIndex.ToString()).Right;
            }
        }

        internal int LeftTrim
        {
            get
            {
                if (mp_oControl.TreeviewColumnIndex == 0)
                {
                    return 0;
                }
                return mp_oControl.Columns.Item(mp_oControl.TreeviewColumnIndex.ToString()).LeftTrim;
            }
        }

        internal int RightTrim
        {
            get
            {
                if (mp_oControl.TreeviewColumnIndex == 0)
                {
                    return 0;
                }
                return mp_oControl.Columns.Item(mp_oControl.TreeviewColumnIndex.ToString()).RightTrim;
            }
        }

		public void SetXML(string sXML)
		{
			clsXML oXML = new clsXML(mp_oControl, "Treeview");
			oXML.SetXML(sXML);
			oXML.InitializeReader();
			oXML.ReadProperty("BackColor", ref mp_clrBackColor);
			oXML.ReadProperty("CheckBoxBorderColor", ref mp_clrCheckBoxBorderColor);
			oXML.ReadProperty("CheckBoxColor", ref mp_clrCheckBoxColor);
			oXML.ReadProperty("CheckBoxes", ref mp_bCheckBoxes);
			oXML.ReadProperty("CheckBoxMarkColor", ref mp_clrCheckBoxMarkColor);
			oXML.ReadProperty("ExpansionOnSelection", ref mp_bExpansionOnSelection);
			oXML.ReadProperty("FullColumnSelect", ref mp_bFullColumnSelect);
			oXML.ReadProperty("Images", ref mp_bImages);
			oXML.ReadProperty("Indentation", ref mp_lIndentation);
			oXML.ReadProperty("PathSeparator", ref mp_sPathSeparator);
			oXML.ReadProperty("PlusMinusBorderColor", ref mp_clrPlusMinusBorderColor);
			oXML.ReadProperty("PlusMinusSignColor", ref mp_clrPlusMinusSignColor);
			oXML.ReadProperty("PlusMinusSigns", ref mp_bPlusMinusSigns);
			oXML.ReadProperty("SelectedBackColor", ref mp_clrSelectedBackColor);
			oXML.ReadProperty("SelectedForeColor", ref mp_clrSelectedForeColor);
			oXML.ReadProperty("TreeLineColor", ref mp_clrTreeLineColor);
			oXML.ReadProperty("TreeLines", ref mp_bTreeLines);
		}

		public string GetXML()
		{
			clsXML oXML = new clsXML(mp_oControl, "Treeview");
			oXML.InitializeWriter();
			oXML.WriteProperty("BackColor", mp_clrBackColor);
			oXML.WriteProperty("CheckBoxBorderColor", mp_clrCheckBoxBorderColor);
			oXML.WriteProperty("CheckBoxColor", mp_clrCheckBoxColor);
			oXML.WriteProperty("CheckBoxes", mp_bCheckBoxes);
			oXML.WriteProperty("CheckBoxMarkColor", mp_clrCheckBoxMarkColor);
			oXML.WriteProperty("ExpansionOnSelection", mp_bExpansionOnSelection);
			oXML.WriteProperty("FullColumnSelect", mp_bFullColumnSelect);
			oXML.WriteProperty("Images", mp_bImages);
			oXML.WriteProperty("Indentation", mp_lIndentation);
			oXML.WriteProperty("PathSeparator", mp_sPathSeparator);
			oXML.WriteProperty("PlusMinusBorderColor", mp_clrPlusMinusBorderColor);
			oXML.WriteProperty("PlusMinusSignColor", mp_clrPlusMinusSignColor);
			oXML.WriteProperty("PlusMinusSigns", mp_bPlusMinusSigns);
			oXML.WriteProperty("SelectedBackColor", mp_clrSelectedBackColor);
			oXML.WriteProperty("SelectedForeColor", mp_clrSelectedForeColor);
			oXML.WriteProperty("TreeLineColor", mp_clrTreeLineColor);
			oXML.WriteProperty("TreeLines", mp_bTreeLines);
			return oXML.GetXML();
		}

	}

}

