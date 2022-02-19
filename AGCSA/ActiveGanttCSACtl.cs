using System.ComponentModel;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGCSA
{
	[System.Runtime.InteropServices.GuidAttribute("3EC6CB88-D14B-41AB-82DD-03FCC5D3A212")]
	[LicenseProvider(typeof(RegistryLicenseProvider))]
	[Designer("AGCSA.clsDesigner, AGCSA"),
	ToolboxData("<{0}:ActiveGanttCSACtl runat=server></{0}:ActiveGanttCSACtl>")]
	public class ActiveGanttCSACtl : System.Web.UI.WebControls.WebControl, IPostBackDataHandler, IPostBackEventHandler
	{

#region IPostBackDataHandler

		public virtual bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection) 
		{
			return false;
		}

		public virtual void RaisePostDataChangedEvent() 
		{
		}

#endregion

#region IPostBackEventHandler

		public void RaisePostBackEvent(string eventArgument)
		{
			int X;
			int Y;
			X = System.Convert.ToInt32(Page.Request.Params["__CLICKCOORD_X"]);
			Y = System.Convert.ToInt32(Page.Request.Params["__CLICKCOORD_Y"]);
			OnClick(new System.Web.UI.ImageClickEventArgs(X, Y));
		}

#endregion

		protected override void OnPreRender(EventArgs e)
		{
			Page.RegisterRequiresPostBack(this);
			String formName = null;
			foreach (Control oControl in Page.Controls)
			{
				if (oControl is System.Web.UI.HtmlControls.HtmlForm)
				{
					formName = oControl.UniqueID;
					break;
				}
			}
			if (mp_sFormID == "")
			{
				if (formName == null)
				{
					throw new Exception("The page has no form.");
				}
			}
			else
			{
				formName = mp_sFormID;
			}

			Page.ClientScript.RegisterHiddenField("__EVENTTARGET", "");
			Page.ClientScript.RegisterHiddenField("__EVENTARGUMENT", "");
			Page.ClientScript.RegisterHiddenField("__CLICKCOORD_X", "-1");
			Page.ClientScript.RegisterHiddenField("__CLICKCOORD_Y", "-1");
            //VS2002 & VS2003 equivalent:
            //Page.RegisterHiddenField("__EVENTTARGET", "");
            //Page.RegisterHiddenField("__EVENTARGUMENT", "");
            //Page.RegisterHiddenField("__CLICKCOORD_X", "-1");
            //Page.RegisterHiddenField("__CLICKCOORD_Y", "-1");
            


			System.Text.StringBuilder script = new System.Text.StringBuilder();
			script.Append(Environment.NewLine);
			script.AppendFormat("<script language=\"javascript\">{0}", Environment.NewLine);
			script.AppendFormat("function __SCSIMG_CLICK(sCaller) {0}{1}", "{", Environment.NewLine);
			script.AppendFormat("var theform = document.{0};{1}", formName, Environment.NewLine);
			script.AppendFormat("theform.__EVENTTARGET.value = sCaller;{0}", Environment.NewLine);
			script.AppendFormat("theform.__EVENTARGUMENT.value = sCaller.split(\"$\").join(\":\");{0}", Environment.NewLine);
			script.AppendFormat("theform.__CLICKCOORD_X.value = event.offsetX;{0}", Environment.NewLine);
			script.AppendFormat("theform.__CLICKCOORD_Y.value = event.offsetY;{0}", Environment.NewLine);
			script.AppendFormat("theform.submit();{0}", Environment.NewLine);
			script.AppendFormat("{0}{1}", "}", Environment.NewLine);
			script.Append("</script>");
		    if (Page.ClientScript.IsClientScriptBlockRegistered("__SCSIMG_CLICK") == false)
		    {
			    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "__SCSIMG_CLICK", script.ToString());
		    }
            //VS2002 & VS2003 equivalent:
            //if (Page.IsClientScriptBlockRegistered("__SCSIMG_CLICK") == false)
            //{
            //    Page.RegisterClientScriptBlock("__SCSIMG_CLICK", script.ToString());
            //}

		}

		protected override void Render(HtmlTextWriter output)
		{
			String uniqueName = GenerateUniqueName();
			String toolTip = "";
			Page.Application[clsAGCSARenderStream.ImageNamePrefix + uniqueName] = this;
			String sOutput; 
			sOutput = "<img src='" + clsAGCSARenderStream.ImageHandlerRequestFilename + "?id=" + uniqueName + "' id='" + this.UniqueID + "' name='" + this.UniqueID + "' border='0' height='" + this.Height.Value + "' width='" + this.Width.Value + "' alt='" + toolTip + "' onclick='javascript:__SCSIMG_CLICK(\"" + this.UniqueID + "\")'>";
			output.Write(sOutput);
		}

		string GenerateUniqueName()
		{
			string sControlName = System.Guid.NewGuid().ToString();
			return sControlName;
		}


		// ---------------------------------------------------------------------------------------------------------------------
		// Private Enumerations ActiveGanttCtl
		// ---------------------------------------------------------------------------------------------------------------------

		enum E_DRAWOPTYPE 
		{
			DOT_ALL = 0,
			DOT_ROWSANDCLIENTAREA = 1,
			DOT_TABLEAREA = 2,
			DOT_TIMELINEANDCLIENTAREA = 3,
		}

		// ---------------------------------------------------------------------------------------------------------------------
		// Member Variables
		// ---------------------------------------------------------------------------------------------------------------------

		private License mp_oLicense = null;
		//// Public Classes
		[System.ComponentModel.Browsable(false)] public clsRows Rows;
		[System.ComponentModel.Browsable(false)] public clsTasks Tasks;
		[System.ComponentModel.Browsable(false)] public clsColumns Columns;
		[System.ComponentModel.Browsable(false)] public clsStyles Styles;
		[System.ComponentModel.Browsable(false)] public clsLayers Layers;
		[System.ComponentModel.Browsable(false)] public clsPercentages Percentages;
		[System.ComponentModel.Browsable(false)] public clsTimeBlocks TimeBlocks;
        [System.ComponentModel.Browsable(false)] public clsPredecessors Predecessors; 
		[System.ComponentModel.Browsable(false)] public clsViews Views;
		[System.ComponentModel.Browsable(false)] public clsSplitter Splitter;
		[System.ComponentModel.Browsable(false)] public clsTreeview Treeview;
		[System.ComponentModel.Browsable(false)] public clsDrawing Drawing;
		[System.ComponentModel.Browsable(false)] public clsMath MathLib;
		[System.ComponentModel.Browsable(false)] public clsString StrLib;
		[System.ComponentModel.Browsable(false)] public clsVerticalScrollBar VerticalScrollBar;
		[System.ComponentModel.Browsable(false)] public clsHorizontalScrollBar HorizontalScrollBar;
        [System.ComponentModel.Browsable(false)] public clsTierAppearance TierAppearance;
        [System.ComponentModel.Browsable(false)] public clsTierFormat TierFormat;
        [System.ComponentModel.Browsable(false)] public clsScrollBarSeparator ScrollBarSeparator;
		internal clsViewState oViewState = new clsViewState();
		private clsTimeBlocks tmpTimeBlocks;
		internal clsMouseKeyboardEvents MouseKeyboardEvents;
		private clsView mp_oCurrentView;
		internal clsGraphics clsG;
		private bool mp_bAllowAdd = true;
		private bool mp_bAllowEdit = true;
		private bool mp_bAllowSplitterMove = true;
		private bool mp_bAllowRowSize = true;
		private bool mp_bAllowRowMove = true;
		private bool mp_bAllowColumnSize = true;
		private bool mp_bAllowColumnMove = true;
		private bool mp_bAllowTimeLineScroll = true;
		private bool mp_bAllowPredecessorAdd = true;
		private bool mp_bDoubleBuffering = true;
        private bool mp_bEnforcePredecessors = false;
		private int mp_lMinColumnWidth = 5;
		private int mp_lMinRowHeight = 5;
        private int mp_lSelectedTaskIndex = 0;
        private int mp_lSelectedColumnIndex = 0;
        private int mp_lSelectedRowIndex = 0;
        private int mp_lSelectedCellIndex = 0;
        private int mp_lSelectedPercentageIndex = 0;
        private int mp_lSelectedPredecessorIndex = 0;
        private int mp_lTreeviewColumnIndex = 0;
		private string mp_sCurrentLayer = "0";
		private string mp_sCurrentView = "";
		private E_ADDMODE mp_yAddMode = E_ADDMODE.AT_TASKADD;
        private E_INTERVAL mp_yAddDurationInterval = E_INTERVAL.IL_SECOND;
		private E_SCROLLBEHAVIOUR mp_yScrollBarBehaviour = E_SCROLLBEHAVIOUR.SB_HIDE;
		private E_TIMEBLOCKBEHAVIOUR mp_yTimeBlockBehaviour = E_TIMEBLOCKBEHAVIOUR.TBB_ROWEXTENTS;
		private E_LAYEROBJECTENABLE mp_yLayerEnableObjects = E_LAYEROBJECTENABLE.EC_INCURRENTLAYERONLY;
		private E_REPORTERRORS mp_yErrorReports = E_REPORTERRORS.RE_MSGBOX;
        private E_TIERAPPEARANCESCOPE mp_yTierAppearanceScope = E_TIERAPPEARANCESCOPE.TAS_CONTROL;
        private E_TIERFORMATSCOPE mp_yTierFormatScope = E_TIERFORMATSCOPE.TFS_CONTROL;
        private E_PREDECESSORMODE mp_yPredecessorMode = E_PREDECESSORMODE.PM_CREATEWARNINGFLAG;
        private string mp_sControlTag = "";
		private Graphics mp_oGraphics;
		private System.Globalization.CultureInfo mp_oCulture;
        private string mp_sStyleIndex;
        private clsStyle mp_oStyle;
        private System.Drawing.Image mp_oImage;
        private string mp_sImageTag;
		public ToolTipEventArgs ToolTipEventArgs = new ToolTipEventArgs();
		public ObjectAddedEventArgs ObjectAddedEventArgs = new ObjectAddedEventArgs();
		public CustomTierDrawEventArgs CustomTierDrawEventArgs = new CustomTierDrawEventArgs();
		public MouseEventArgs MouseEventArgs = new MouseEventArgs();
		public KeyEventArgs KeyEventArgs = new KeyEventArgs();
		public ScrollEventArgs ScrollEventArgs = new ScrollEventArgs();
		public DrawEventArgs DrawEventArgs = new DrawEventArgs();
		public PredecessorDrawEventArgs PredecessorDrawEventArgs = new PredecessorDrawEventArgs();
		public ObjectSelectedEventArgs ObjectSelectedEventArgs = new ObjectSelectedEventArgs();
		public ObjectStateChangedEventArgs ObjectStateChangedEventArgs = new ObjectStateChangedEventArgs();
		public ErrorEventArgs ErrorEventArgs = new ErrorEventArgs();
		public NodeEventArgs NodeEventArgs = new NodeEventArgs();
        public PredecessorExceptionEventArgs PredecessorExceptionEventArgs = new PredecessorExceptionEventArgs();
        private DataSet mp_oMSPIDataSet;

		private string mp_sFormID = "";

        public delegate void ControlClickEventHandler(object sender, MouseEventArgs e);
		public event ControlClickEventHandler ControlClick;

        public delegate void DrawEventHandler(object sender, DrawEventArgs e);
		public event DrawEventHandler Draw;
        public delegate void PredecessorDrawEventHandler(object sender, PredecessorDrawEventArgs e);
		public event PredecessorDrawEventHandler PredecessorDraw;
        public delegate void CustomTierDrawEventHandler(object sender, CustomTierDrawEventArgs e);
		public event CustomTierDrawEventHandler CustomTierDraw;
        public delegate void TierTextDrawEventHandler(object sender, CustomTierDrawEventArgs e);
		public event TierTextDrawEventHandler TierTextDraw;

        public delegate void ObjectSelectedEventHandler(object sender, ObjectSelectedEventArgs e);
		public event ObjectSelectedEventHandler ObjectSelected;

        public delegate void ActiveGanttErrorEventHandler(object sender, ErrorEventArgs e);
		public event ActiveGanttErrorEventHandler ActiveGanttError;
        public delegate void PredecessorExceptionEventHandler(object sender, PredecessorExceptionEventArgs e);
        public event PredecessorExceptionEventHandler PredecessorException;

        public delegate void ControlScrollEventHandler(object sender, ScrollEventArgs e);
		public event ControlScrollEventHandler ControlScroll;

        public delegate void ControlRedrawnEventHandler(object sender, System.EventArgs e);
		public event ControlRedrawnEventHandler ControlRedrawn;
        public delegate void ControlDrawEventHandler(object sender, System.EventArgs e);
		public event ControlDrawEventHandler ControlDraw;
        public delegate void TimeLineChangedEventHandler(object sender, System.EventArgs e);
		public event TimeLineChangedEventHandler TimeLineChanged;

        public delegate void NodeExpandedEventHandler(object sender, NodeEventArgs e);
		public event NodeExpandedEventHandler NodeExpanded;
        public delegate void NodeCheckedEventHandler(object sender, NodeEventArgs e);
		public event NodeCheckedEventHandler NodeChecked;

        public delegate void ViewStateRefreshedEventHandler(object sender, System.EventArgs e);
		public event ViewStateRefreshedEventHandler ViewStateRefreshed;
		

        internal void FirePredecessorException()
        {
            if (PredecessorException != null)
            {
                PredecessorException(this, PredecessorExceptionEventArgs);
            }
        }

		internal void FireControlClick()
		{
			if (ControlClick != null) 
			{
				ControlClick(this, MouseEventArgs);
			}
		}

		internal void FireDraw()
		{
			if (Draw != null) 
			{
				Draw(this, DrawEventArgs);
			}
		}

		internal void FirePredecessorDraw()
		{
			if (PredecessorDraw != null) 
			{
				PredecessorDraw(this, PredecessorDrawEventArgs);
			}
		}

		internal void FireCustomTierDraw()
		{
			if (CustomTierDraw != null) 
			{
				CustomTierDraw(this, CustomTierDrawEventArgs);
			}
		}

		internal void FireTierTextDraw()
		{
			if (TierTextDraw != null) 
			{
				TierTextDraw(this, CustomTierDrawEventArgs);
			}
		}

		internal void FireObjectSelected()
		{
			if (ObjectSelected != null) 
			{
				ObjectSelected(this, ObjectSelectedEventArgs);
			}
		}

		internal void FireActiveGanttError()
		{
			if (ActiveGanttError != null) 
			{
				ActiveGanttError(this, ErrorEventArgs);
			}
		}

        internal void FireControlScroll()
		{
			if (ControlScroll != null) 
			{
				ControlScroll(this, ScrollEventArgs);
			}
		}

		internal void FireNodeChecked()
		{
			if (NodeChecked != null) 
			{
				NodeChecked(this, NodeEventArgs);
			}
		}

		internal void FireNodeExpanded()
		{
			if (NodeExpanded != null) 
			{
				NodeExpanded(this, NodeEventArgs);
			}
		}

		internal void FireControlDraw()
		{
			if (ControlDraw != null) 
			{
				ControlDraw(this, new System.EventArgs());
			}
		}

		internal void FireControlRedrawn()
		{
			if (ControlRedrawn != null) 
			{
				ControlRedrawn(this, new System.EventArgs());
			}
		}

		internal void FireTimeLineChanged()
		{
			if (TimeLineChanged != null) 
			{
				TimeLineChanged(this, new System.EventArgs());
			}
		}

		internal clsTimeBlocks TempTimeBlocks()
		{
			return tmpTimeBlocks;
		}

		public ActiveGanttCSACtl()
		{
			this.mp_oLicense = LicenseManager.Validate(typeof(ActiveGanttCSACtl), this);

			clsG = new clsGraphics(this);
			MathLib = new clsMath(this);
			StrLib = new clsString(this);
			Styles = new clsStyles(this);
            mp_sStyleIndex = "DS_CONTROL";
            mp_oStyle = Styles.FItem("DS_CONTROL");
            VerticalScrollBar = new clsVerticalScrollBar(this);
            HorizontalScrollBar = new clsHorizontalScrollBar(this);
			Rows = new clsRows(this);
			Tasks = new clsTasks(this);
			Columns = new clsColumns(this);
			Layers = new clsLayers(this);
			Percentages = new clsPercentages(this);
			TimeBlocks = new clsTimeBlocks(this);
            Predecessors = new clsPredecessors(this);
			tmpTimeBlocks = new clsTimeBlocks(this);
			Splitter = new clsSplitter(this);
			Views = new clsViews(this);
			Treeview = new clsTreeview(this);
			mp_oCurrentView = Views.FItem("0");
			MouseKeyboardEvents = new clsMouseKeyboardEvents(this);
			Drawing = new clsDrawing(this);
			mp_oCulture = (System.Globalization.CultureInfo) System.Globalization.CultureInfo.CurrentCulture.Clone();
            TierAppearance = new clsTierAppearance(this);
            TierFormat = new clsTierFormat(this);
            ScrollBarSeparator = new clsScrollBarSeparator(this);

            mp_oImage = null;
            mp_sImageTag = "";
		}

		public override void Dispose()
		{
			if ((mp_oLicense != null)) 
			{
				mp_oLicense.Dispose();
				mp_oLicense = null;
			}
		}

		//All Drawing Here
		internal System.IO.MemoryStream OnPaint()
		{
			System.IO.MemoryStream memStream = new System.IO.MemoryStream();
			Bitmap b = new Bitmap(clsG.Width(), clsG.Height(), PixelFormat.Format24bppRgb);
			mp_oGraphics = Graphics.FromImage(b);
			mp_Draw();
			mp_oGraphics.Save();
			ImageFormat imgformat = ImageFormat.Png;
			b.Save(memStream, imgformat);
			return memStream;
		}

		private void mp_Draw()
		{
			FireControlDraw();
            clsG.ClipRegion(0, 0, clsG.Width(), clsG.Height(), false);
            clsG.mp_DrawItem(0, 0, clsG.Width() - 1, clsG.Height() - 1, "", "", false, this.Image, 0, 0, this.Style);
			mp_oCurrentView.TimeLine.Calculate();
			mp_PositionScrollBars();
			Columns.Position();
			Rows.InitializePosition();
			Rows.PositionRows();
			Columns.Draw();
			Rows.Draw();
			Treeview.Draw();
			mp_oCurrentView.TimeLine.Draw();
			mp_oCurrentView.TimeLine.ProgressLine.Draw();
			TimeBlocks.CreateTemporaryTimeBlocks();
			TimeBlocks.Draw();
			mp_oCurrentView.ClientArea.Grid.Draw();
			mp_oCurrentView.ClientArea.Draw();
            Predecessors.Draw();
			Tasks.Draw();
			Percentages.Draw();
			mp_oCurrentView.TimeLine.ProgressLine.Draw();
			Splitter.Draw();
            clsG.ClipRegion(0, 0, clsG.Width(), clsG.Height(), false);
            if (VerticalScrollBar.State == E_SCROLLSTATE.SS_SHOWN)
            {
                clsG.mp_DrawItem(VerticalScrollBar.Left, VerticalScrollBar.Top + VerticalScrollBar.Height, VerticalScrollBar.Left + 16, VerticalScrollBar.Top + VerticalScrollBar.Height + 16, "", "", false, null, 0, 0, ScrollBarSeparator.Style);
                clsG.ClipRegion(0, 0, clsG.Width(), clsG.Height(), false);
            }
            else if (mp_oCurrentView.TimeLine.TimeLineScrollBar.State == E_SCROLLSTATE.SS_SHOWN)
            {
                clsG.mp_DrawItem(mp_oCurrentView.TimeLine.TimeLineScrollBar.Left + mp_oCurrentView.TimeLine.TimeLineScrollBar.Width, mp_oCurrentView.TimeLine.TimeLineScrollBar.Top, mp_oCurrentView.TimeLine.TimeLineScrollBar.Left + mp_oCurrentView.TimeLine.TimeLineScrollBar.Width + 16, mp_oCurrentView.TimeLine.TimeLineScrollBar.Top + 16, "", "", false, null, 0, 0, ScrollBarSeparator.Style);
                clsG.ClipRegion(0, 0, clsG.Width(), clsG.Height(), false);
            }
			mp_DrawDebugMetrics();
			if ((int)VerticalScrollBar.State == (int)E_SCROLLSTATE.SS_SHOWN) 
			{
				VerticalScrollBar.ScrollBar.Draw();
			}
			if ((int)HorizontalScrollBar.State == (int)E_SCROLLSTATE.SS_SHOWN) 
			{
				HorizontalScrollBar.ScrollBar.Draw();
			}
			if ((int)mp_oCurrentView.TimeLine.TimeLineScrollBar.State == (int)E_SCROLLSTATE.SS_SHOWN) 
			{
				mp_oCurrentView.TimeLine.TimeLineScrollBar.ScrollBar.Draw();
			}
		#if DemoVersion
			Font oFont = new Font("Arial", 12, FontStyle.Bold);
			System.Random rnd;
			rnd = new System.Random((int)System.DateTime.Now.Ticks);
			Color oColor = new Color();
			oColor = Color.FromArgb(255, rnd.Next(0,255),rnd.Next(0,255),rnd.Next(0,255));
			clsG.DrawAlignedText(20, 20, clsG.Width() - 20, clsG.Height() - 20, "ActiveGanttCSA Scheduler Component" + "\r\n" + "Trial Version: " + Version +  "\r\n" + "For evaluation purposes only" + "\r\n" + "Purchase the full version through: " + "\r\n" + "http://www.sourcecodestore.com", GRE_HORIZONTALALIGNMENT.HAL_RIGHT, GRE_VERTICALALIGNMENT.VAL_BOTTOM, oColor, oFont, true);
#endif
            FireControlRedrawn();
		}

		private void mp_DrawDebugMetrics()
		{
		}

		internal Graphics f_HDC()
		{
			return mp_oGraphics;
		}

		internal int f_Width()
		{
			return (int)this.Width.Value;
		}

		internal int f_Height()
		{
			return (int)this.Height.Value;
		}

		internal int mp_lStrWidth(string sString, Font r_oFont)
		{
			return MathLib.RoundDouble(mp_oGraphics.MeasureString(sString, r_oFont).Width);
		}

		internal int mp_lStrHeight(string sString, Font r_oFont)
		{
			return MathLib.RoundDouble(mp_oGraphics.MeasureString(sString, r_oFont).Height);
		}

		internal void f_Draw()
		{
			mp_Draw();
		}

		internal bool f_UserMode 
		{
			get { return true; }
		}

		internal int mt_BorderThickness 
		{
            get
            {
                switch (mp_oStyle.Appearance)
                {
                    case E_STYLEAPPEARANCE.SA_RAISED:
                        return 2;
                    case E_STYLEAPPEARANCE.SA_SUNKEN:
                        return 2;
                    case E_STYLEAPPEARANCE.SA_FLAT:
                        if (mp_oStyle.BorderStyle == GRE_BORDERSTYLE.SBR_NONE)
                        {
                            return 0;
                        }
                        else
                        {
                            return mp_oStyle.BorderWidth;
                        }
                    case E_STYLEAPPEARANCE.SA_CELL:
                        if (mp_oStyle.BorderStyle == GRE_BORDERSTYLE.SBR_NONE)
                        {
                            return 0;
                        }
                        else
                        {
                            return mp_oStyle.BorderWidth;
                        }
                    case E_STYLEAPPEARANCE.SA_GRAPHICAL:
                        return 0;
                }
                return 0;
            }
		}

		internal int mt_TableBottom 
		{
			get 
			{
				if (HorizontalScrollBar.State == E_SCROLLSTATE.SS_SHOWN) 
				{
                    return clsG.Height() - mt_BorderThickness - 1 - HorizontalScrollBar.Height;
				}
				else 
				{
                    return clsG.Height() - mt_BorderThickness - 1;
				}
			}
		}

		internal int mt_TopMargin 
		{
            get { return mt_BorderThickness; }
		}

		internal int mt_LeftMargin 
		{
            get { return mt_BorderThickness; }
		}

		internal int mt_RightMargin 
		{
			get 
			{
				if (VerticalScrollBar.State == E_SCROLLSTATE.SS_SHOWN) 
				{
                    return clsG.Width() - mt_BorderThickness - 1 - VerticalScrollBar.Width;
				}
				else 
				{
                    return clsG.Width() - mt_BorderThickness - 1;
				}
			}
		}

		internal int mt_BottomMargin 
		{
            get { return clsG.Height() - mt_BorderThickness - 1; }
		}

		internal clsGraphics GrphLib()
		{
			return clsG;
		}

		protected virtual void OnClick(System.Web.UI.ImageClickEventArgs e)
		{
			RefreshViewState();
			mp_oCurrentView.TimeLine.Calculate();
			mp_PositionScrollBars();
			Columns.Position();
			Rows.InitializePosition();
			Rows.PositionRows();
			MouseKeyboardEvents.OnMouseClick(e.X, e.Y);
		}

		internal void VerticalScrollBar_ValueChanged(int Offset)
		{
			ScrollEventArgs.Clear();
			ScrollEventArgs.ScrollBarType = E_SCROLLBAR.SCR_VERTICAL;
			ScrollEventArgs.Offset = Offset;
            FireControlScroll();
		}

		internal void HorizontalScrollBar_ValueChanged(int Offset)
		{
			ScrollEventArgs.Clear();
			ScrollEventArgs.ScrollBarType = E_SCROLLBAR.SCR_HORIZONTAL1;
			ScrollEventArgs.Offset = Offset;
            FireControlScroll();
		}

		internal void TimeLineScrollBar_ValueChanged(int Offset)
		{
			ScrollEventArgs.Clear();
			ScrollEventArgs.ScrollBarType = E_SCROLLBAR.SCR_HORIZONTAL2;
			ScrollEventArgs.Offset = Offset;
            FireControlScroll();
		}

		internal void mp_PositionScrollBars()
		{
			if (clsG.Height() <= mp_oCurrentView.ClientArea.Top) 
			{
				VerticalScrollBar.State = E_SCROLLSTATE.SS_CANTDISPLAY;
				HorizontalScrollBar.State = E_SCROLLSTATE.SS_CANTDISPLAY;
				mp_oCurrentView.TimeLine.TimeLineScrollBar.State = E_SCROLLSTATE.SS_CANTDISPLAY;
                return;
			}

            //// Determine need for HorizontalScrollBar
            int lWidth = 0;
            lWidth = Columns.Width;
            if (lWidth > Splitter.Right)
            {
                if (HorizontalScrollBar.mf_Visible == true)
                {
                    HorizontalScrollBar.State = E_SCROLLSTATE.SS_NEEDED;
                }
                else
                {
                    HorizontalScrollBar.State = E_SCROLLSTATE.SS_NOTNEEDED;
                }
            }
            else
            {
                HorizontalScrollBar.State = E_SCROLLSTATE.SS_NOTNEEDED;
            }
            if (Splitter.Right < 5)
            {
                HorizontalScrollBar.State = E_SCROLLSTATE.SS_CANTDISPLAY;
            }

            //// Determine need for mp_oCurrentView.TimeLine.TimeLineScrollBar
            if (Splitter.Right < clsG.Width() - (18 + mt_BorderThickness))
            {
                if (mp_oCurrentView.TimeLine.TimeLineScrollBar.Enabled == true)
                {
                    if (mp_oCurrentView.TimeLine.TimeLineScrollBar.mf_Visible == true)
                    {
                        mp_oCurrentView.TimeLine.TimeLineScrollBar.State = E_SCROLLSTATE.SS_NEEDED;
                    }
                    else
                    {
                        mp_oCurrentView.TimeLine.TimeLineScrollBar.State = E_SCROLLSTATE.SS_NOTNEEDED;
                    }
                }
                else
                {
                    mp_oCurrentView.TimeLine.TimeLineScrollBar.State = E_SCROLLSTATE.SS_NOTNEEDED;
                }
            }
            else
            {
                mp_oCurrentView.TimeLine.TimeLineScrollBar.State = E_SCROLLSTATE.SS_CANTDISPLAY;
            }

            //// Determine need for VerticalScrollBar
            if (((Rows.Height() + mp_oCurrentView.ClientArea.Top + HorizontalScrollBar.Height + mt_BorderThickness) > clsG.Height()) | (Rows.RealFirstVisibleRow > 1))
            {
                if (mp_oCurrentView.TimeLine.TimeLineScrollBar.State == E_SCROLLSTATE.SS_CANTDISPLAY)
                {
                    VerticalScrollBar.State = E_SCROLLSTATE.SS_CANTDISPLAY;
                }
                else
                {
                    VerticalScrollBar.State = E_SCROLLSTATE.SS_NEEDED;
                }
            }
            else
            {
                VerticalScrollBar.State = E_SCROLLSTATE.SS_NOTNEEDED;
            }

            if (VerticalScrollBar.mf_Visible == false)
            {
                VerticalScrollBar.State = E_SCROLLSTATE.SS_CANTDISPLAY;
            }
            if (HorizontalScrollBar.mf_Visible == false)
            {
                HorizontalScrollBar.State = E_SCROLLSTATE.SS_CANTDISPLAY;
            }
            if (mp_oCurrentView.TimeLine.TimeLineScrollBar.mf_Visible == false)
            {
                mp_oCurrentView.TimeLine.TimeLineScrollBar.State = E_SCROLLSTATE.SS_CANTDISPLAY;
            }
			if (VerticalScrollBar.State == E_SCROLLSTATE.SS_SHOWN) 
			{
				VerticalScrollBar.Position();
			}
			if (HorizontalScrollBar.State == E_SCROLLSTATE.SS_SHOWN) 
			{
				HorizontalScrollBar.Position();
			}
			if (mp_oCurrentView.TimeLine.TimeLineScrollBar.State == E_SCROLLSTATE.SS_SHOWN) 
			{
				mp_oCurrentView.TimeLine.TimeLineScrollBar.Position();
			}
		}

		public void WriteXML(string url)
		{
			clsXML oXML = new clsXML(this, "ActiveGanttCtl");
			mp_WriteXML(ref oXML);
			oXML.WriteXML(url);
		}

		public void ReadXML(string url)
		{
			clsXML oXML = new clsXML(this, "ActiveGanttCtl");
			oXML.ReadXML(url);
			mp_ReadXML(ref oXML);
		}

		public void SetXML(string sXML)
		{
			clsXML oXML = new clsXML(this, "ActiveGanttCtl");
			oXML.SetXML(sXML);
			mp_ReadXML(ref oXML);
		}

		public string GetXML()
		{
			clsXML oXML = new clsXML(this, "ActiveGanttCtl");
			mp_WriteXML(ref oXML);
			return oXML.GetXML();
		}

		private void mp_WriteXML(ref clsXML oXML)
		{
			oXML.InitializeWriter();
			oXML.WriteProperty("Version", "AGCSA");
            oXML.WriteProperty("ControlTag", mp_sControlTag);
			oXML.WriteProperty("AddMode", mp_yAddMode);
            oXML.WriteProperty("AddDurationInterval", mp_yAddDurationInterval);
			oXML.WriteProperty("AllowAdd", mp_bAllowAdd);
			oXML.WriteProperty("AllowColumnMove", mp_bAllowColumnMove);
			oXML.WriteProperty("AllowColumnSize", mp_bAllowColumnSize);
			oXML.WriteProperty("AllowEdit", mp_bAllowEdit);
			oXML.WriteProperty("AllowPredecessorAdd", mp_bAllowPredecessorAdd);
			oXML.WriteProperty("AllowRowMove", mp_bAllowRowMove);
			oXML.WriteProperty("AllowRowSize", mp_bAllowRowSize);
			oXML.WriteProperty("AllowSplitterMove", mp_bAllowSplitterMove);
			oXML.WriteProperty("AllowTimeLineScroll", mp_bAllowTimeLineScroll);
            oXML.WriteProperty("EnforcePredecessors", mp_bEnforcePredecessors);
			oXML.WriteProperty("CurrentLayer", mp_sCurrentLayer);
			oXML.WriteProperty("CurrentView", mp_sCurrentView);
			oXML.WriteProperty("DoubleBuffering", mp_bDoubleBuffering);
			oXML.WriteProperty("ErrorReports", mp_yErrorReports);
			oXML.WriteProperty("LayerEnableObjects", mp_yLayerEnableObjects);
			oXML.WriteProperty("MinColumnWidth", mp_lMinColumnWidth);
			oXML.WriteProperty("MinRowHeight", mp_lMinRowHeight);
			oXML.WriteProperty("ScrollBarBehaviour", mp_yScrollBarBehaviour);
			oXML.WriteProperty("SelectedCellIndex", mp_lSelectedCellIndex);
			oXML.WriteProperty("SelectedColumnIndex", mp_lSelectedColumnIndex);
			oXML.WriteProperty("SelectedPercentageIndex", mp_lSelectedPercentageIndex);
            oXML.WriteProperty("SelectedPredecessorIndex", mp_lSelectedPredecessorIndex);
			oXML.WriteProperty("SelectedRowIndex", mp_lSelectedRowIndex);
			oXML.WriteProperty("SelectedTaskIndex", mp_lSelectedTaskIndex);
            oXML.WriteProperty("TreeviewColumnIndex", mp_lTreeviewColumnIndex);
			oXML.WriteProperty("TimeBlockBehaviour", mp_yTimeBlockBehaviour);
            oXML.WriteProperty("TierAppearanceScope", mp_yTierAppearanceScope);
            oXML.WriteProperty("TierFormatScope", mp_yTierFormatScope);
            oXML.WriteProperty("PredecessorMode", mp_yPredecessorMode);
            oXML.WriteProperty("StyleIndex", mp_sStyleIndex);
            oXML.WriteProperty("Image", ref mp_oImage);
            oXML.WriteProperty("ImageTag", mp_sImageTag);
			oXML.WriteObject(Styles.GetXML());
			oXML.WriteObject(Rows.GetXML());
			oXML.WriteObject(Columns.GetXML());
			oXML.WriteObject(Layers.GetXML());
			oXML.WriteObject(Tasks.GetXML());
            oXML.WriteObject(Predecessors.GetXML());
			oXML.WriteObject(Views.GetXML());
			oXML.WriteObject(TimeBlocks.GetXML());
            oXML.WriteObject(TimeBlocks.CP_GetXML());
			oXML.WriteObject(Percentages.GetXML());
			oXML.WriteObject(Splitter.GetXML());
			oXML.WriteObject(Treeview.GetXML());
            oXML.WriteObject(TierAppearance.GetXML());
            oXML.WriteObject(TierFormat.GetXML());
            oXML.WriteObject(ScrollBarSeparator.GetXML());
            oXML.WriteObject(VerticalScrollBar.GetXML());
            oXML.WriteObject(HorizontalScrollBar.GetXML());
		}

		private void mp_ReadXML(ref clsXML oXML)
		{
			String sVersion = "";
			String sCurrentView = "";
			Clear();
			oXML.InitializeReader();
			oXML.ReadProperty("Version", ref sVersion);
            oXML.ReadProperty("ControlTag", ref mp_sControlTag);
			oXML.ReadProperty("AddMode", ref mp_yAddMode);
            oXML.ReadProperty("AddDurationInterval", ref mp_yAddDurationInterval);
			oXML.ReadProperty("AllowAdd", ref mp_bAllowAdd);
			oXML.ReadProperty("AllowColumnMove", ref mp_bAllowColumnMove);
			oXML.ReadProperty("AllowColumnSize", ref mp_bAllowColumnSize);
			oXML.ReadProperty("AllowEdit", ref mp_bAllowEdit);
			oXML.ReadProperty("AllowPredecessorAdd", ref mp_bAllowPredecessorAdd);
			oXML.ReadProperty("AllowRowMove", ref mp_bAllowRowMove);
			oXML.ReadProperty("AllowRowSize", ref mp_bAllowRowSize);
			oXML.ReadProperty("AllowSplitterMove", ref mp_bAllowSplitterMove);
			oXML.ReadProperty("AllowTimeLineScroll", ref mp_bAllowTimeLineScroll);
            oXML.ReadProperty("EnforcePredecessors", ref mp_bEnforcePredecessors);
			oXML.ReadProperty("CurrentLayer", ref mp_sCurrentLayer);
			oXML.ReadProperty("CurrentView", ref sCurrentView);
			oXML.ReadProperty("DoubleBuffering", ref mp_bDoubleBuffering);
			oXML.ReadProperty("ErrorReports", ref mp_yErrorReports);
			oXML.ReadProperty("LayerEnableObjects", ref mp_yLayerEnableObjects);
			oXML.ReadProperty("MinColumnWidth", ref mp_lMinColumnWidth);
			oXML.ReadProperty("MinRowHeight", ref mp_lMinRowHeight);
			oXML.ReadProperty("ScrollBarBehaviour", ref mp_yScrollBarBehaviour);
			oXML.ReadProperty("SelectedCellIndex", ref mp_lSelectedCellIndex);
			oXML.ReadProperty("SelectedColumnIndex", ref mp_lSelectedColumnIndex);
			oXML.ReadProperty("SelectedPercentageIndex", ref mp_lSelectedPercentageIndex);
            oXML.ReadProperty("SelectedPredecessorIndex", ref mp_lSelectedPredecessorIndex);
			oXML.ReadProperty("SelectedRowIndex", ref mp_lSelectedRowIndex);
			oXML.ReadProperty("SelectedTaskIndex", ref mp_lSelectedTaskIndex);
            oXML.ReadProperty("TreeviewColumnIndex", ref mp_lTreeviewColumnIndex);
			oXML.ReadProperty("TimeBlockBehaviour", ref mp_yTimeBlockBehaviour);
            oXML.ReadProperty("TierAppearanceScope", ref mp_yTierAppearanceScope);
            oXML.ReadProperty("TierFormatScope", ref mp_yTierFormatScope);
            oXML.ReadProperty("PredecessorMode", ref mp_yPredecessorMode);
            oXML.ReadProperty("StyleIndex", ref mp_sStyleIndex);
            oXML.ReadProperty("Image", ref mp_oImage);
            oXML.ReadProperty("ImageTag", ref mp_sImageTag);
			Styles.SetXML(oXML.ReadObject("Styles"));
			Rows.SetXML(oXML.ReadObject("Rows"));
			Columns.SetXML(oXML.ReadObject("Columns"));
			Layers.SetXML(oXML.ReadObject("Layers"));
			Tasks.SetXML(oXML.ReadObject("Tasks"));
            Predecessors.SetXML(oXML.ReadObject("Predecessors"));
			Views.SetXML(oXML.ReadObject("Views"));
			TimeBlocks.SetXML(oXML.ReadObject("TimeBlocks"));
            TimeBlocks.CP_SetXML(oXML.ReadObject("CP_TimeBlocks"));
			Percentages.SetXML(oXML.ReadObject("Percentages"));
			Splitter.SetXML(oXML.ReadObject("Splitter"));
			Treeview.SetXML(oXML.ReadObject("Treeview"));
            TierAppearance.SetXML(oXML.ReadObject("TierAppearance"));
            TierFormat.SetXML(oXML.ReadObject("TierFormat"));
            ScrollBarSeparator.SetXML(oXML.ReadObject("ScrollBarSeparator"));
            VerticalScrollBar.SetXML(oXML.ReadObject("VerticalScrollBar"));
            HorizontalScrollBar.SetXML(oXML.ReadObject("HorizontalScrollBar"));
            StyleIndex = mp_sStyleIndex;
            Rows.UpdateTree();
			CurrentView = sCurrentView;
			mp_oCurrentView.TimeLine.Position(mp_oCurrentView.TimeLine.StartDate);
		}

        internal void mp_ErrorReport(SYS_ERRORS ErrNumber, string ErrDescription, string ErrSource)
		{
            if (mp_yErrorReports == E_REPORTERRORS.RE_MSGBOX)
            {
                ShowMessageBox(System.Convert.ToString(ErrNumber) + ": " + ErrDescription + " (" + ErrSource + ")");
            }
            else if (mp_yErrorReports == E_REPORTERRORS.RE_HIDE)
            {
            }
            else if (mp_yErrorReports == E_REPORTERRORS.RE_RAISE)
            {
                AGError ex = new AGError(ErrNumber.ToString() + ": " + ErrDescription + " - " + ErrSource);
                ex.ErrNumber = (int)ErrNumber;
                ex.ErrDescription = ErrDescription;
                ex.ErrSource = ErrSource;
                throw ex;
            }
            else if (mp_yErrorReports == E_REPORTERRORS.RE_RAISEEVENT)
            {
                ErrorEventArgs.Clear();
                ErrorEventArgs.Number = (int)ErrNumber;
                ErrorEventArgs.Description = ErrDescription;
                ErrorEventArgs.Source = ErrSource;
                FireActiveGanttError();
            }
		}

		public E_REPORTERRORS ErrorReports 
		{
			get { return mp_yErrorReports; }
			set { mp_yErrorReports = value; }
		}

		[System.ComponentModel.Browsable(false)]
		public string CurrentLayer 
		{
			get { return mp_sCurrentLayer; }
			set { mp_sCurrentLayer = value; }
		}

		[System.ComponentModel.Browsable(false)]
		public string CurrentView 
		{
			get { return mp_sCurrentView; }
			set 
			{
				if (value.Length == 0) 
				{
					value = "0";
				}
				mp_oCurrentView = Views.FItem(value);
				mp_sCurrentView = value;
			}
		}

		[System.ComponentModel.Browsable(false)]
		public clsView CurrentViewObject 
		{
			get { return mp_oCurrentView; }
		}

		public E_LAYEROBJECTENABLE LayerEnableObjects 
		{
			get { return mp_yLayerEnableObjects; }
			set { mp_yLayerEnableObjects = value; }
		}

		public E_SCROLLBEHAVIOUR ScrollBarBehaviour 
		{
			get { return mp_yScrollBarBehaviour; }
			set { mp_yScrollBarBehaviour = value; }
		}

        public E_TIERAPPEARANCESCOPE TierAppearanceScope
        {
            get { return mp_yTierAppearanceScope; }
            set { mp_yTierAppearanceScope = value; }
        }

        public E_TIERFORMATSCOPE TierFormatScope
        {
            get { return mp_yTierFormatScope; }
            set { mp_yTierFormatScope = value; }
        }

		public E_TIMEBLOCKBEHAVIOUR TimeBlockBehaviour 
		{
			get { return mp_yTimeBlockBehaviour; }
			set { mp_yTimeBlockBehaviour = value; }
		}

		[System.ComponentModel.Browsable(false)]
		public int SelectedTaskIndex 
		{
			get { return mp_lSelectedTaskIndex; }
			set 
			{
				if (value <= 0) 
				{
					value = 0;
				}
				else if (value > Tasks.Count) 
				{
					value = Tasks.Count;
				}
				mp_lSelectedTaskIndex = value;
			}
		}

		[System.ComponentModel.Browsable(false)]
		public int SelectedColumnIndex 
		{
			get { return mp_lSelectedColumnIndex; }
			set 
			{
				if (value <= 0) 
				{
					value = 0;
				}
				else if (value > Columns.Count) 
				{
					value = Columns.Count;
				}
				mp_lSelectedColumnIndex = value;
			}
		}

		[System.ComponentModel.Browsable(false)]
		public int SelectedRowIndex 
		{
			get { return mp_lSelectedRowIndex; }
			set 
			{
				if (value <= 0) 
				{
					value = 0;
				}
				else if (value > Rows.Count) 
				{
					value = Rows.Count;
				}
				mp_lSelectedRowIndex = value;
			}
		}

		[System.ComponentModel.Browsable(false)]
		public int SelectedCellIndex 
		{
			get { return mp_lSelectedCellIndex; }
			set 
			{
				if (value <= 0) 
				{
					value = 0;
				}
				else if (value > Columns.Count) 
				{
					value = Columns.Count;
				}
				mp_lSelectedCellIndex = value;
			}
		}

		public int SelectedPercentageIndex 
		{
			get { return mp_lSelectedPercentageIndex; }
			set 
			{
				if (value <= 0) 
				{
					value = 0;
				}
				else if (value > Percentages.Count) 
				{
					value = Percentages.Count;
				}
				mp_lSelectedPercentageIndex = value;
			}
		}

        public int SelectedPredecessorIndex
        {
            get { return mp_lSelectedPredecessorIndex; }
            set
            {
                if (value <= 0)
                {
                    value = 0;
                }
                else if (value > Percentages.Count)
                {
                    value = Percentages.Count;
                }
                mp_lSelectedPredecessorIndex = value;
            }
        }

        public int TreeviewColumnIndex
        {
            get
            {
                if (Columns.Count == 0)
                {
                    return 0;
                }
                else if (mp_lTreeviewColumnIndex > Columns.Count)
                {
                    return 0;
                }
                else if (mp_lTreeviewColumnIndex < 0)
                {
                    return 0;
                }
                else
                {
                    return mp_lTreeviewColumnIndex;
                }
            }
            set
            {
                if (value <= 0)
                {
                    value = 0;
                }
                else if (value > Columns.Count)
                {
                    value = Columns.Count;
                }
                mp_lTreeviewColumnIndex = value;
            }
        }

        public string StyleIndex
        {
            get
            {
                if (mp_sStyleIndex == "DS_CONTROL")
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
                    value = "DS_CONTROL";
                mp_sStyleIndex = value;
                mp_oStyle = Styles.FItem(value);
            }
        }

        public new clsStyle Style
        {
            get { return mp_oStyle; }
        }

        public System.Drawing.Image Image
        {
            get { return mp_oImage; }
            set { mp_oImage = value; }
        }

        public string ImageTag
        {
            get { return mp_sImageTag; }
            set { mp_sImageTag = value; }
        }

		public System.Globalization.CultureInfo Culture 
		{
			get { return mp_oCulture; }
			set { mp_oCulture = value; }
		}

        public string ControlTag
        {
            get { return mp_sControlTag; }
            set
            {
                mp_sControlTag = value;
            }
        }

		public void ClearSelections()
		{
			mp_lSelectedTaskIndex = 0;
			mp_lSelectedColumnIndex = 0;
			mp_lSelectedRowIndex = 0;
			mp_lSelectedCellIndex = 0;
			mp_lSelectedPercentageIndex = 0;
            mp_lSelectedPredecessorIndex = 0;
		}

		public void Clear()
		{
			Tasks.Clear();
			Rows.Clear();
			Styles.Clear();
			Layers.Clear();
			Columns.Clear();
			TimeBlocks.Clear();
			Views.Clear();
		}

        public void CheckPredecessors()
        {
            int i = 0;
            clsTask oTask;
            for (i = 1; i <= Tasks.Count; i++)
            {
                oTask = (clsTask)Tasks.oCollection.m_oReturnArrayElement(i);
                oTask.mp_bWarning = false;
            }
            if (Predecessors.Count == 0)
            {
                return;
            }
            clsPredecessor oPredecessor;
            for (i = 1; i <= Predecessors.Count; i++)
            {
                oPredecessor = (clsPredecessor)Predecessors.oCollection.m_oReturnArrayElement(i);
                oPredecessor.Check(mp_yPredecessorMode);
            }
        }

        public bool EnforcePredecessors
        {
            get { return mp_bEnforcePredecessors; }
            set { mp_bEnforcePredecessors = value; }
        }

        public E_PREDECESSORMODE PredecessorMode
        {
            get { return mp_yPredecessorMode; }
            set { mp_yPredecessorMode = value; }
        }

		public string ModuleCompletePath 
		{
			get { return System.Reflection.Assembly.GetExecutingAssembly().Location; }
		}

		public string Version 
		{
			get 
			{
				System.Reflection.Assembly ai = System.Reflection.Assembly.GetExecutingAssembly();
				return ai.GetName().Version.ToString();
			}
		}

		protected override object SaveViewState()
		{
			object[] oState = new object[6];
			oState[0] = VerticalScrollBar.ScrollBar.SaveViewState();
			oState[1] = HorizontalScrollBar.ScrollBar.SaveViewState();
			oState[2] = CurrentViewObject.TimeLine.TimeLineScrollBar.ScrollBar.SaveViewState();
			//CheckBoxes
			int lIndex = 0;
			bool bChecked = false;
			bool bExpanded = false;
			string sKey = null;
			string sParam = "";
			clsRow oRow = null;
			for (lIndex = 1; lIndex <= Rows.Count - 1; lIndex++) 
			{
				oRow = Rows.Item(lIndex.ToString());
				bChecked = oRow.Node.Checked;
				sKey = oRow.Key;
				sParam = sParam + sKey + "," + System.Convert.ToInt16(bChecked).ToString() + ";";
			}
			oState[3] = sParam;
			sParam = "";
			for (lIndex = 1; lIndex <= Rows.Count - 1; lIndex++) 
			{
				oRow = Rows.Item(lIndex.ToString());
				bExpanded = oRow.Node.Expanded;
				sKey = oRow.Key;
				sParam = sParam + sKey + "," + System.Convert.ToInt16(bExpanded).ToString() + ";";
			}
			oState[4] = sParam;
			return oState;
		}

		protected override void LoadViewState(object savedState)
		{
			if ((savedState != null)) 
			{
				object[] oState = (object[])savedState;
				VerticalScrollBar.ScrollBar.LoadViewState(oState[0]);
				HorizontalScrollBar.ScrollBar.LoadViewState(oState[1]);
				CurrentViewObject.TimeLine.TimeLineScrollBar.ScrollBar.LoadViewState(oState[2]);
				oViewState.VerticalScrollBar_Value = VerticalScrollBar.ScrollBar.Value;
				oViewState.HorizontalScrollBar_Value = HorizontalScrollBar.ScrollBar.Value;
				oViewState.TimeLineScrollBar_Value = mp_oCurrentView.TimeLine.TimeLineScrollBar.ScrollBar.Value;
				oViewState.sCheckedNodes = (string)oState[3];
				oViewState.sExpandedNodes = (string)oState[4];
			}
		}

		private void RefreshViewState()
		{
			string sParam = null;
			string[] aParam = null;
			long lIndex = 0;
			string sRow = null;
			bool bChecked = false;
			bool bExpanded = false;
			if (ViewStateRefreshed != null) 
			{
				ViewStateRefreshed(this, new System.EventArgs());
			}
			VerticalScrollBar.ScrollBar.Value = oViewState.VerticalScrollBar_Value;
			HorizontalScrollBar.ScrollBar.Value = oViewState.HorizontalScrollBar_Value;
			mp_oCurrentView.TimeLine.TimeLineScrollBar.ScrollBar.Value = oViewState.TimeLineScrollBar_Value;

			sParam = oViewState.sCheckedNodes;
			aParam = sParam.Split(';');
			for (lIndex = 0; lIndex <= aParam.GetUpperBound(0); lIndex++) 
			{
				string[] aRow = null;
				sRow = aParam[lIndex];
				if (sRow.Length > 0) 
				{
					aRow = sRow.Split(',');
					if (Rows.oCollection.m_bDoesKeyExist(aRow[0]) == true) 
					{
						if (aRow[1] == "0") 
						{
							bChecked = false;
						}
						else 
						{
							bChecked = true;
						}
						Rows.Item(aRow[0]).Node.Checked = bChecked;
					}
				}
			}

			sParam = oViewState.sExpandedNodes;
			aParam = sParam.Split(';');
			for (lIndex = 0; lIndex <= aParam.GetUpperBound(0); lIndex++) 
			{
				string[] aRow = null;
				sRow = aParam[lIndex];
				if (sRow.Length > 0) 
				{
					aRow = sRow.Split(',');
					if (Rows.oCollection.m_bDoesKeyExist(aRow[0]) == true) 
					{
						if (aRow[1] == "0") 
						{
							bExpanded = false;
						}
						else 
						{
							bExpanded = true;
						}
						Rows.Item(aRow[0]).Node.Expanded = bExpanded;
					}
				}
			}

		}

		public string FormID 
		{
			get { return mp_sFormID; }
			set { mp_sFormID = value; }
		}

        private bool FindColumn(DataTable oDataTable, string sColumnName)
        {
            foreach (DataColumn oColumn in oDataTable.Columns)
            {
                if (oColumn.ColumnName.ToLower() == sColumnName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        internal void ShowMessageBox(string sMessage)
        {
            sMessage = sMessage.Replace("'", "\\'");
            string sScript = "<script type=\"text/javascript\">alert('" + sMessage + "');</script>";
            Page oPage = System.Web.HttpContext.Current.CurrentHandler as Page;
            if (oPage != null && !oPage.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                oPage.ClientScript.RegisterClientScriptBlock(oPage.GetType(), "alert", sScript);
            }
        }    
	}

    public class AGError : Exception
    {
        private string mp_sErrDescription;
        private int mp_lErrNumber;
        private string mp_sErrSource;

        public AGError() : base() { }
        public AGError(string s) : base(s) { }
        public AGError(string s, Exception ex) : base(s, ex) { }

        public string ErrDescription
        {
            get
            {
                return mp_sErrDescription;
            }
            set
            {
                mp_sErrDescription = value;
            }
        }

        public int ErrNumber
        {
            get
            {
                return mp_lErrNumber;
            }
            set
            {
                mp_lErrNumber = value;
            }
        }

        public string ErrSource
        {
            get
            {
                return mp_sErrSource;
            }
            set
            {
                mp_sErrSource = value;
            }
        }
    }
}
