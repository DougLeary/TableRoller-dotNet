using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using GameBits;

namespace TableRoller
{
	public partial class EditTable : System.Web.UI.Page
	{
		string filePath = HttpContext.Current.Server.MapPath("App_Data/Tables.xml");

		protected void Page_Load(object sender, EventArgs e)
		{
			// Bind tables to UI components
			Monsters1.SetDataSource("Monsters Table 1");
			DataBind();
		}
	}
}
