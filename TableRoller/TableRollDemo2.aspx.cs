using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using GameBits;

namespace TableRoller
{
	public partial class TableRollDemo2 : System.Web.UI.Page
	{
		string filePath = HttpContext.Current.Server.MapPath("./Tables/ADnD1E_Treasure.xml");

		protected void Page_Load(object sender, EventArgs e)
		{
			// Bind tables to UI components
			MainTable.SetDataSource("III. Magic Items");
			DataBind();
		}

		protected void SaveAll(object sender, EventArgs e)
		{
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), filePath);
			Repository.Connect(provider);
		}
	}
}
