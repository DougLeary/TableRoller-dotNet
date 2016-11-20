using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using GameBits;

namespace RealmSmith
{
	public class LoadablePage : Page
	{
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
	}

	/// <summary>
	/// Handler to load a UserControl referenced by a jQuery ajax call
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class JQueryUserControlHandler : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			// We add control in Page tree collection
			using (var dummyPage = new LoadablePage())
			{
				Repository bits = new Repository();
				RollableTableControl rc = GetControl(context);
				rc.SetDataSource("Monsters Table 2");
				System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();
				dummyPage.Controls.Add(form);
				dummyPage.Form.Controls.Add(rc);

				StringBuilder sb = new StringBuilder();
				StringWriter sw = new StringWriter(sb);
				HtmlTextWriter tw = new HtmlTextWriter(sw);
				try
				{
//					context.Server.Execute(dummyPage, context.Response.Output, true);
					rc.RenderControl(tw);
				}
				catch (Exception ex) { }
				context.Response.Write(sb.ToString());
			}
		}

		private RollableTableControl GetControl(HttpContext context)
		{
			// URL path given by load(fn) method on click of button
			string strPath = context.Request.Url.LocalPath.Replace(".jscx", ".ascx");
			RollableTableControl userctrl = null;
			using (var dummyPage = new Page())
			{
				userctrl = dummyPage.LoadControl(strPath) as RollableTableControl;
			}
			// Loaded user control is returned
			return userctrl;
		}

		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

	}
}
