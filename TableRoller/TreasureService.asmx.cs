using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using GameBits;

namespace TableRoller
{
	/// <summary>
	/// Summary description for TreasureService
	/// </summary>
	[WebService(Namespace = "http://realmsmith.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]
	public class TreasureService : System.Web.Services.WebService
	{
		string ADnDPath = HttpContext.Current.Server.MapPath("./Tables/ADnD1E_Treasure.xml");
		string GemsPath = HttpContext.Current.Server.MapPath("./Tables/GemsAndJewelry.xml");

		// not used yet; 
		// need to modify the Gems table and others, and extend the GameObject loading methods
		class TreasureObject : GameObject		
		{
			public int GpValue { get; set; }
			public int XpValue { get; set; }
		}

		RollableList GetADnDTreasureTypes()
		{
			// Generate the ADnD 1E TreasureTypes list
			// TODO: 
			// - read from XML or another data source; I don't think the syntax to specify some of these things exists yet
			// - on the other hand, this is a good demo for generating tables in code
			// - generate individual spells for scrolls
			// - incorporate gp values for gems and jewelery, and XP values for magic items; 
			//	-- this will involve creating descendant GameObject classes with these attributes;
			//	-- add the option to hide gem names and to consolidate the values into one, e.g. "gems (50,000 gp)"
			//	-- include a total GP and XP value for each trove, or the data for computing it on the client side
			RollableList List = new RollableList();
			// establish session
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), GemsPath);
			Repository.Connect(provider);
			provider.FilePath = ADnDPath;
			provider.Load();

			// Treasure types table from AD&D 1E Monster Manual
			ItemList list = new ItemList();

			// ******  TODO: die rolls shouldn't require a leading 1; d6 should default to 1d6; 
			// ******  also, a die roll string consisting only of a constant N should convert to Nd1 (or 0d0+N)
			list.Add(new ItemRoll(new GameObject("cp", "cp"), DieRoll.FromString("1d6"), 1000, 25));
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d6"), 1000, 30));
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("1d6"), 1000, 35));
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("1d10"), 1000, 40));
			list.Add(new ItemRoll(new GameObject("pp", "pp"), DieRoll.FromString("1d4"), 100, 25));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("4d10"), 1, 60));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("3d10"), 1, 50));
			TableRoll tempRoll = new TableRoll(Repository.GetTable("I. Map or Magic Determination"));
			tempRoll.Rolls = 3;
			list.Add(new ItemRoll(tempRoll, DieRoll.FromString("1d1"), 1, 30));
			List.Add("A", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("cp", "cp"), DieRoll.FromString("1d8"), 1000, 50));
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d6"), 1000, 25));
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("1d4"), 1000, 25));
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("1d3"), 1000, 25));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("1d8"), 1, 30));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("1d4"), 1, 20));

			// Sword, armor or misc. weapon: 10%
			//   This means 10% of the time we have to pick one of three tables (Swords, Armor, and Misc. Weapons) and roll on that table.
			//   - create a temporary RollableTable that contains TableRolls for those three tables;
			//   - create an ItemRoll that rolls on the temp table 10% of the time;
			//   - add the ItemRoll to the List.
			RollableTable tempTable = new RollableTable();
			tempTable.Dice = new DieRoll("1d3");
			tempTable.Add(new TableRoll(Repository.GetTable("III. Swords")), 1);
			tempTable.Add(new TableRoll(Repository.GetTable("III. Armor and Shields")), 2);
			tempTable.Add(new TableRoll(Repository.GetTable("III. Miscellaneous Weapons")), 3);
			list.Add(new ItemRoll(new TableRoll(tempTable), DieRoll.FromString("1d1"), 1, 10));
			List.Add("B", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("cp", "cp"), DieRoll.FromString("1d12"), 1000, 20));
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d6"), 1000, 30));
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("1d4"), 1000, 10));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("1d6"), 1, 25));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("1d3"), 1, 20));
			tempRoll = new TableRoll(Repository.GetTable("I. Map or Magic Determination"));
			tempRoll.Rolls = 2;
			list.Add(new ItemRoll(tempRoll, DieRoll.FromString("1d1"), 1, 10));
			List.Add("C", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("cp", "cp"), DieRoll.FromString("1d8"), 1000, 10));
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d12"), 1000, 15));
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("1d8"), 1000, 15));
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("1d6"), 1000, 50));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("1d10"), 1, 30));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("1d6"), 1, 25));
			// Any 2 plus 1 potion: 15%
			// This means 15% of the time we roll 2 Maps-or-Magic results, AND we roll a potion;
			// - create a TableRoll that rolls twice on the Maps and Magic table
			// - create a TableRoll that rolls once on the Potions table
			// - put both TableRolls in an ItemList
			// - create an ItemRoll that returns the ItemList 15% of the time. 
			tempRoll = new TableRoll(Repository.GetTable("I. Map or Magic Determination"));
			tempRoll.Rolls = 2;
			ItemList tempList = new ItemList();
			tempList.Add(tempRoll);
			// don't need tempRoll for the potions since Rolls = 1
			tempList.Add(new TableRoll(Repository.GetTable("III. Potions")));
			list.Add(new ItemRoll(tempList, DieRoll.FromString("1d1"), 1, 15));
			List.Add("D", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("cp", "cp"), DieRoll.FromString("1d10"), 1000, 5));
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d12"), 1000, 25));
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("1d6"), 1000, 25));
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("1d8"), 1000, 25));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("1d12"), 1, 15));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("1d8"), 1, 10));
			// Any 3 plus 1 scroll: 25% -- same pattern as D
			tempRoll = new TableRoll(Repository.GetTable("I. Map or Magic Determination"));
			tempRoll.Rolls = 3;
			tempList = new ItemList();
			tempList.Add(tempRoll);
			tempList.Add(new TableRoll(Repository.GetTable("III. Scrolls")));
			list.Add(new ItemRoll(tempList, DieRoll.FromString("1d1"), 1, 25));
			List.Add("E", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d20"), 1000, 10));
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("1d12"), 1000, 15));
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("1d10"), 1000, 40));
			list.Add(new ItemRoll(new GameObject("pp", "pp"), DieRoll.FromString("1d8"), 100, 35));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("3d10"), 1, 20));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("1d10"), 1, 10));

			// Any 3 except swords or misc. weapons, plus 1 potion and 1 scroll: 30%
			// To exclude swords or misc. weapons we can ignore results above 75 on the Magic Items table. 
			// This requires a modified version the Map or Magic Determination table. 
			tempTable = new RollableTable();
			tempTable.Dice = new DieRoll("1d100");
			tempTable.Add(new TableRoll(Repository.GetTable("II. Map Table")), 10);
			tempRoll = new TableRoll(Repository.GetTable("III. Magic Items"));
			tempRoll.IgnoreAbove = 75;
			tempTable.Add(tempRoll, 100);
			// create a TableRoll to roll 3 times on the temp table
			tempRoll = new TableRoll(tempTable);
			tempRoll.Rolls = 3;
			// create an ItemList and add the tempRoll and the potion and scroll rolls
			tempList = new ItemList();
			tempList.Add(tempRoll);
			tempList.Add(new TableRoll(Repository.GetTable("III. Potions")));
			tempList.Add(new TableRoll(Repository.GetTable("III. Scrolls")));
			list.Add(new ItemRoll(tempList, DieRoll.FromString("1d1"), 1, 30));

			List.Add("F", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("10d4"), 1000, 50));
			list.Add(new ItemRoll(new GameObject("pp", "pp"), DieRoll.FromString("1d20"), 100, 50));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("5d4"), 1, 30));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("1d10"), 1, 25));

			// Any 4 plus 1 scroll: 35% -- same pattern as E
			tempRoll = new TableRoll(Repository.GetTable("I. Map or Magic Determination"));
			tempRoll.Rolls = 4;
			tempList = new ItemList();
			tempList.Add(tempRoll);
			tempList.Add(new TableRoll(Repository.GetTable("III. Scrolls")));
			list.Add(new ItemRoll(tempList, DieRoll.FromString("1d1"), 1, 35));

			List.Add("G", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("cp", "cp"), DieRoll.FromString("5d6"), 1000, 25));
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d100"), 1000, 40));
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("10d4"), 1000, 40));
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("10d6"), 1000, 55));
			list.Add(new ItemRoll(new GameObject("pp", "pp"), DieRoll.FromString("5d10"), 100, 25));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("1d100"), 1, 50));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("10d4"), 1, 50));

			// Any 4 plus 1 scroll: 35% -- same pattern as E
			tempRoll = new TableRoll(Repository.GetTable("I. Map or Magic Determination"));
			tempRoll.Rolls = 4;
			tempList = new ItemList();
			tempList.Add(tempRoll);
			tempList.Add(new TableRoll(Repository.GetTable("III. Potions")));
			tempList.Add(new TableRoll(Repository.GetTable("III. Scrolls")));
			list.Add(new ItemRoll(tempList, DieRoll.FromString("1d1"), 1, 15));

			List.Add("H", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("pp", "pp"), DieRoll.FromString("3d6"), 100, 30));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("2d10"), 1, 55));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("1d12"), 1, 50));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("I. Map or Magic Determination")), DieRoll.FromString("1d1"), 1, 15));
			List.Add("I", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("cp", "cp"), DieRoll.FromString("3d8")));
			List.Add("J", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("3d6")));
			List.Add("K", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("2d6")));
			List.Add("L", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("2d4")));
			List.Add("M", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("pp", "pp"), DieRoll.FromString("1d6")));
			List.Add("N", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("cp", "cp"), DieRoll.FromString("1d4"), 1000, 25));
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d3"), 1000, 20));
			List.Add("O", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d6"), 1000, 30));
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("1d2"), 1000, 25));
			List.Add("P", list);

			list = new ItemList();
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("1d4"), 1, 50));
			List.Add("Q", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("2d4"), 1000, 40));
			list.Add(new ItemRoll(new GameObject("pp", "pp"), DieRoll.FromString("10d6"), 100, 50));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("4d8"), 1, 55));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("1d12"), 1, 45));
			List.Add("R", list);

			list = new ItemList();
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("III. Potions")), DieRoll.FromString("2d4"), 1, 40));
			List.Add("S", list);

			list = new ItemList();
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("III. Scrolls")), DieRoll.FromString("1d4"), 1, 50));
			List.Add("T", list);

			list = new ItemList();
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("10d8"), 1, 90));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("5d6"), 1, 80));
			// 1 roll on each magic item table excluding potions and scrolls: 70%
			tempList = new ItemList();
			tempList.Add(new TableRoll(Repository.GetTable("III. Rings")));
			tempList.Add(new TableRoll(Repository.GetTable("III. Rods, Staves and Wands")));
			tempList.Add(new TableRoll(Repository.GetTable("III. Miscellaneous Magic")));
			tempList.Add(new TableRoll(Repository.GetTable("III. Armor and Shields")));
			tempList.Add(new TableRoll(Repository.GetTable("III. Swords")));
			tempList.Add(new TableRoll(Repository.GetTable("III. Miscellaneous Weapons")));
			list.Add(new ItemRoll(tempList, new DieRoll("1d1"), 1, 70));
			List.Add("U", list);

			list = new ItemList();
			// 2 rolls on each magic item table excluding potions and scrolls: 85%
			// Just reuse the temp list from "U" but resolve it twice
			list.Add(new ItemRoll(tempList, new DieRoll("2d1"), 1, 85));
			List.Add("V", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("5d6"), 1000, 60));
			list.Add(new ItemRoll(new GameObject("pp", "pp"), DieRoll.FromString("1d8"), 100, 15));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("10d8"), 1, 60));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("5d8"), 1, 50));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("II. Map Table")), DieRoll.FromString("1d1"), 1, 55));
			List.Add("W", list);

			list = new ItemList();
			tempList = new ItemList();
			tempList.Add(new TableRoll(Repository.GetTable("III. Miscellaneous Magic")));
			tempList.Add(new TableRoll(Repository.GetTable("III. Potions")));
			list.Add(new ItemRoll(tempList, new DieRoll("1d1"), 1, 60));
			List.Add("X", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("2d6"), 1000, 70));
			List.Add("Y", list);

			list = new ItemList();
			list.Add(new ItemRoll(new GameObject("cp", "cp"), DieRoll.FromString("1d3"), 1000, 20));
			list.Add(new ItemRoll(new GameObject("sp", "sp"), DieRoll.FromString("1d4"), 1000, 25));
			list.Add(new ItemRoll(new GameObject("ep", "ep"), DieRoll.FromString("1d4"), 1000, 25));
			list.Add(new ItemRoll(new GameObject("gp", "gp"), DieRoll.FromString("1d4"), 1000, 30));
			list.Add(new ItemRoll(new GameObject("pp", "pp"), DieRoll.FromString("1d6"), 100, 30));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Gems and Jewels")), DieRoll.FromString("10d6"), 1, 55));
			list.Add(new ItemRoll(new TableRoll(Repository.GetTable("Jewelry")), DieRoll.FromString("5d6"), 1, 50));
			tempRoll = new TableRoll(Repository.GetTable("III. Magic Items"));
			tempRoll.Rolls = 3;
			list.Add(new ItemRoll(tempRoll, DieRoll.FromString("1d1"), 1, 50));
			List.Add("Z", list);

			return List;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public string GetADnD1ETreasureByType(string TreasureTypes)
		{
			// roll on the ADnDTreasureTypes rollable list
			RollableList List = GetADnDTreasureTypes();
			ItemList list = (ItemList)(List.ResolveItem(TreasureTypes));
			string strList = list.ToString(ItemList.Format.Uncompressed).Trim();
			if (strList.Length == 0)
			{
				return "Nothing";
			}
			else
			{
				return strList.Replace(ItemList.Separator, "<br />");
			}
		}
	}
}
