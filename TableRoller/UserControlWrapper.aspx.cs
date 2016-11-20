using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TableRoller
{
	public partial class UserControlWrapper : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["ControlName"] == null)
			{
				Page page = new Page();
				Control control = (Control)LoadControl("~/.../" + Request.QueryString["ControlName"]);
				StringWriter sw = new StringWriter();
				page.Controls.Add(control);
				Server.Execute(page, sw, false);
				Response.Write(sw.ToString());
				Response.Flush();
				Response.Close();
			}
		}
	}
}