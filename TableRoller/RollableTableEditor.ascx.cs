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
	public partial class RollableTableEditor : System.Web.UI.UserControl
	{
		protected RollableTable UnderlyingTable;

		private string _clientId = String.Empty;
		public string ClientId
		{
			get 
			{
				if (_clientId == String.Empty)
				{
					_clientId = "gb_" + UnderlyingTable.TableName.Trim().Replace(" ", "_");
				}
				return _clientId; 
			}
			set { _clientId = value; }
		}

		public object DataSource
		{
			get
			{
				return UnderlyingTable;
			}
			set
			{
				UnderlyingTable = value as RollableTable;
				Repeater1.DataSource = UnderlyingTable;
			}
		}

		public string TitleText
		{
			get
			{
				if (UnderlyingTable != null)
				{
					return UnderlyingTable.TableName;
				}
				else
				{
					return String.Empty;
				}
			}
			set
			{
				if (UnderlyingTable != null)
				{
					UnderlyingTable.TableName = value;
				}
			}
		}

		public string DieRollText
		{
			get
			{
				if (UnderlyingTable != null)
				{
					return UnderlyingTable.Dice.ToString();
				}
				else
				{
					return "d20";
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public RollableTableEditor()
		{
		}

		public RollableTableEditor(RollableTable table)
		{
			DataSource = table;
		}

		public void SetDataSource(string TableName)
		{
			this.DataSource = Repository.GetTable(TableName);
		}

		public string TypeName(IResolver item)
		{
			return item.GetType().Name;
		}

	}
}