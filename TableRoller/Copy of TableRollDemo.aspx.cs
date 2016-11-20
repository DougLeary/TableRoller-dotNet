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

namespace RealmSmith
{
	public partial class TableRollDemo : System.Web.UI.Page
	{
		GameBitsSession bits;
		string filePath = HttpContext.Current.Server.MapPath("./Tables/SampleTables.xml");

		protected void Page_Load(object sender, EventArgs e)
		{
			// Establish a GameBitsSession by calling the static Connect method;
			// In the future this will involve some kind of user authentication
			bits = GameBitsSession.Connect(filePath);	//"c:\\temp\\TestSave.xml");

			// Bind tables to UI components
			Monsters1.SetDataSource(bits, "Monsters Table 1");
			Monsters2.SetDataSource(bits, "Monsters Table 2");
			Villagers1.SetDataSource(bits, "Villagers Table 1");
			Treasures1.SetDataSource(bits, "Treasure Table 1");
			Gems0.SetDataSource(bits, "Gems and Jewels Table");
			Gems1.SetDataSource(bits, "Ornamental Stones");
			Gems2.SetDataSource(bits, "Semi-Precious Stones");
			Gems3.SetDataSource(bits, "Fancy Stones");
			Gems4.SetDataSource(bits, "Precious Stones");
			Gems5.SetDataSource(bits, "Gems");
			Gems6.SetDataSource(bits, "Jewels");
			DataBind();
		}

		protected void SaveAll(object sender, EventArgs e)
		{
			bits.Save(filePath);
		}
	}
}
