using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Drawing;

namespace AGCSA
{
	internal class clsDesigner : System.Web.UI.Design.ControlDesigner
	{
		public clsDesigner()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override String GetDesignTimeHtml()
		{
			ActiveGanttCSACtl ctl = (ActiveGanttCSACtl) this.Component;
			StringWriter sw = new StringWriter();
			HtmlTextWriter tw = new HtmlTextWriter(sw);
			HyperLink placeholderlink = new HyperLink();
			placeholderlink.Width = ctl.Width;
			placeholderlink.Height = ctl.Height;
			placeholderlink.BorderStyle = BorderStyle.Solid;
			placeholderlink.BorderColor = Color.Gray;
			placeholderlink.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(2);
			placeholderlink.Text = "<p align=\"left\"><b>ActiveGantt Scheduler Component for ASP.Net</b></p><p align=\"left\">C# Version " + ctl.Version + "<p>";
			//placeholderlink.NavigateUrl = "http:www.sourcecodestore.com"
			placeholderlink.RenderControl(tw);
			return sw.ToString();
		}


	}
}
