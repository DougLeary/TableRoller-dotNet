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
	public partial class RollableTableControl : System.Web.UI.UserControl
	{
		private RollableTable _underlyingTable;
		public object DataSource
		{
			get
			{
				return _underlyingTable;
			}
			set
			{
				_underlyingTable = value as RollableTable;
				Grid.DataSource = _underlyingTable;
			}
		}

		public string TitleText
		{
			get
			{
				if (ViewState["TitleText"] == null)
				{
					if (_underlyingTable != null)
					{
						TitleText = _underlyingTable.TableName;
					}
					else
					{
						TitleText = String.Empty;
					}
				}
				return ViewState["TitleText"] as string;
			}
			set
			{
				ViewState["TitleText"] = value;
			}
		}

		public string TableName
		{
			get
			{
				if (ViewState["TableName"] == null)
				{
					if (_underlyingTable != null)
					{
						TableName = _underlyingTable.TableName;
					}
					else
					{
						TableName = String.Empty;
					}
				}
				return ViewState["TableName"] as string;
			}
			set
			{
				ViewState["TableName"] = value;
			}
		}

		public Unit Width { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (_underlyingTable == null)
				{
					if (TableName != null)
					{
						// need a way to determine the current GameBits Repository here
					}
				}

				if (_underlyingTable != null)
				{
					DiceDisplay.Text = _underlyingTable.Dice.ToString();
				}

				if (TitleText == null)
				{
					TitleText = _underlyingTable.TableName;
				}

				if (Width != Unit.Pixel(0))
				{
					Grid.Width = Width;
				}
			}
		}

		public RollableTableControl()
		{
		}

		public RollableTableControl(RollableTable table)
		{
			DataSource = table;
		}

		public void SetDataSource(string TableName)
		{
			this.DataSource = Repository.GetTable(TableName);
		}

	}
}