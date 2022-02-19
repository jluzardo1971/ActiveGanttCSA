using System;
using System.Web;

namespace AGCSA
{
	internal class clsAGCSARenderStream : IHttpModule
	{

		public const string ImageHandlerRequestFilename="image_stream1200.aspx";
		public const string ImageNamePrefix="i_m_g";

		public clsAGCSARenderStream()
		{
		}

		public virtual void Init(HttpApplication httpApp )
		{
			httpApp.BeginRequest += new EventHandler(httpApp_BeginRequest);
		}

		public virtual void Dispose()
		{
		}

		private void httpApp_BeginRequest(object sender, EventArgs e)
		{
			HttpApplication httpApp = (HttpApplication)sender;

			ActiveGanttCSACtl oCtrl = null;
			if( httpApp.Request.Path.ToLower().IndexOf(ImageHandlerRequestFilename) != -1 )
			{
				oCtrl = (ActiveGanttCSACtl)httpApp.Application[ImageNamePrefix + (string)httpApp.Request.QueryString["id"]];
				if( oCtrl == null )
				{
					return; // 404 will be returned
				}
				else
				{
					try
					{
						System.IO.MemoryStream memStream = oCtrl.OnPaint();
						memStream.WriteTo(httpApp.Context.Response.OutputStream);
						memStream.Close();

						httpApp.Context.ClearError();
						httpApp.Context.Response.ContentType = "image/png";
						httpApp.Response.StatusCode = 200;
						httpApp.Application.Remove(ImageNamePrefix + (string)httpApp.Request.QueryString["id"]);
                        //httpApp.Response.End();
                        httpApp.Context.ApplicationInstance.CompleteRequest();
					}
					catch(Exception ex)
					{
						ex = ex;
					}
				}
			}
		}




	}
}
