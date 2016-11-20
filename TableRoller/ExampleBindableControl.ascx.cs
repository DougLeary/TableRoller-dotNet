using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TableRoller
{
	[System.ComponentModel.DefaultBindingProperty("TableName")]
	public partial class ExampleBindableControl : System.Web.UI.UserControl
	{
		public string TableName
		{
			get { return TextBox1.Text; }
			set { TextBox1.Text = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}