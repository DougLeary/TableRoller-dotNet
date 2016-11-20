using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using GameBits;
namespace TableRoller
{
	public partial class Exp1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// create a dictionary to use as a rollable table
			Dictionary<int, string> d = new Dictionary<int, string>();
			d.Add(4, "Orc");
			d.Add(8, "Goblin");
			d.Add(9, "Hobgoblin");
			d.Add(12, "Kobold");
			d.Add(16, "Gnoll");
			d.Add(20, "Derro");

			// roll d20 10x; each time get the first item whose key is >= the roll result, and append it to a string
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 10; i++)
			{
				int roll = DieRoll.Roll("d20");
				foreach (int key in d.Keys)
				{
					if (key >= roll)
					{
						sb.Append(", ");
						sb.Append(d[key]);
						break;
					}
				}
			}
			Label1.Text = sb.ToString(2, sb.Length - 2);
		}
	}

}
