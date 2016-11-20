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
	public partial class TableRollDemo : System.Web.UI.Page
	{
		Repository bits;
		string filePath = HttpContext.Current.Server.MapPath("./Tables/SampleTables.xml");

		protected void Page_Load(object sender, EventArgs e)
		{
			// Get or create the current GameBits Repository and load the xml file
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), filePath);
			Repository.Connect(provider);

			// Bind tables to UI components
			Monsters1.SetDataSource("Monsters Table 1");
			Monsters2.SetDataSource("Monsters Table 2");
			Villagers1.SetDataSource("Villagers Table 1");
			DataBind();
		}

		protected void SaveAll(object sender, EventArgs e)
		{
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), filePath);
			Repository.Connect(provider);
		}
	}
}
