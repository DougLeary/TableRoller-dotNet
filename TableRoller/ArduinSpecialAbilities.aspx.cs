using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GameBits;

namespace RealmSmith
{
	public partial class ArduinSpecialAbilities : System.Web.UI.Page
	{
		string filePath = HttpContext.Current.Server.MapPath("./Tables/Arduin.xml");

		protected void Page_Load(object sender, EventArgs e)
		{
			// Establish a GameBits Repository by calling the static Connect method;
			// In the future this will involve some kind of user authentication
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), filePath);
			Repository.Connect(provider);

			// Bind tables to UI components
			RollableTableControl1.SetDataSource("Arduin Special Abilities");
			DataBind();
		}
	}
}
