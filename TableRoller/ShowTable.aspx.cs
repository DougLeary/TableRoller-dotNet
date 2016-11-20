using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GameBits;

namespace TableRoller
{
	public partial class ShowTable : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["TableName"] != null)
			{
				// connect to the existing GameBits Repository
				RollableTable1.SetDataSource(Request.QueryString["TableName"]);
				DataBind();
			}
		}
	}
}