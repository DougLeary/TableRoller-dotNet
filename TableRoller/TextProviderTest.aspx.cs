using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GameBits;

namespace TableRoller
{
	public partial class TextProviderTest : System.Web.UI.Page
	{
		protected RollableTable MyTable;
		string SamplePath = HttpContext.Current.Server.MapPath("./Tables/SampleTables.xml");
		string ADnDPath = HttpContext.Current.Server.MapPath("./Tables/ADnD1E_Treasure.xml");

		protected void Page_Load(object sender, EventArgs e)
		{
			// establish session
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), SamplePath);
			Repository.Connect(provider);
			provider.FilePath = ADnDPath;
			provider.Load();

			string[] weatherList = new string[] { "Rainy", "Overcast", "Foggy", "Partly Cloudy", "Sunny" };
			MyTable = RollableTable.FromList("Weather", weatherList);

		}

		protected void RollClick(object sender, EventArgs e)
		{
			IResolver results = (IResolver)(MyTable.Roll());
			string strList = results.ToString().Trim();
			if (strList.Length == 0)
			{
				Results.Text = "Nothing";
			}
			else
			{
				Results.Text = strList.Replace(ItemList.Separator, "<br />");
			}
		}

	}
}