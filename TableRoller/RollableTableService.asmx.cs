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
	/// Summary description for TableRoller
	/// </summary>
	[WebService(Namespace = "http://tableroller.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]
	public class RollableTableService : System.Web.Services.WebService
	{
		public class ResultItem
		{
			public string Name;
			public int Count;
		}

		private string _dataFilePath = HttpContext.Current.Server.MapPath("./Tables/SampleTables.xml");

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public string CreateTable(string TableName, string DieRollText)
		{
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), _dataFilePath);
			Repository.Connect(provider);
			Repository bits = Repository.GetCurrentRepository();

			string status = "success";
			try
			{
				RollableTable table = Repository.GetTable(TableName);
				table.Dice = new DieRoll(DieRollText);
			}
			catch (Exception ex)
			{
				status = "error:" + ex.Message;
			}

			return status;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public string AddGameObject(string TableName, int HighRoll, string ObjectName, string Plural, int Count)
		{
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), _dataFilePath);
			Repository.Connect(provider);
			Repository bits = Repository.GetCurrentRepository();

			string status = "success";
			try
			{
				RollableTable table = Repository.GetTable(TableName);
				table.Add(new GameObjectInstance(new GameObject(ObjectName, Plural), Count), HighRoll); 
			}
			catch (Exception ex)
			{
				status = "error:" + ex.Message;
			}

			return status;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public string RollList(string TableName, int NumberOfRolls)
		{
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), _dataFilePath);
			Repository.Connect(provider);
			Repository bits = Repository.GetCurrentRepository();

			// Perform a number of rolls on the table
			SortedList<string, int> list = Repository.GetTable(TableName).RollList(NumberOfRolls);

			// Dump the results into a serializable array
			ResultItem[] result = new ResultItem[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				ResultItem item = new ResultItem();
				item.Name = list.Keys[i];
				item.Count = list.Values[i];
				result[i] = item;
			}

			// Return the results as JSON data
			JavaScriptSerializer js = new JavaScriptSerializer();
			return js.Serialize(result);
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public void LoadXml(string filePath)
		{
			// Establish a GameBits Repository by calling the static Connect method;
			// In the future this will involve some kind of user authentication
			string mappedPath = HttpContext.Current.Server.MapPath(filePath);
			XmlProvider provider = new XmlProvider(Repository.GetCurrentRepository(), mappedPath);
			Repository.Connect(provider);
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public string GetTableList()
		{
			Repository bits = Repository.GetCurrentRepository();
			JavaScriptSerializer js = new JavaScriptSerializer();
			return js.Serialize(bits.Tables.Keys);
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public string GetTable(string tableName)
		{
			Repository bits = Repository.GetCurrentRepository();
			RollableTable table = Repository.GetTable(tableName);

			var tableRows = 
				from DataRow row in table.Rows
				select new { RollRange = row["RangeText"], Item = row["Item"].ToString() };
			var result = new { TableName = table.TableName, DiceText = table.Dice.ToString(), Rows = tableRows };
			JavaScriptSerializer js = new JavaScriptSerializer();
			return js.Serialize(result);
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public string GetRepository(string repositoryName)
		{
			// Currently we return only the CurrentRepository
			// TODO: search for one of many repositories by name 
			Repository bits = Repository.GetCurrentRepository();

			var tables =
				from RollableTable table in bits.Tables
				select table;
			var result = new { Name = bits.Name, Tables = tables };
			JavaScriptSerializer js = new JavaScriptSerializer();
			return js.Serialize(result);
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public string ClearRepository()
		{
			// TODO: need some security on this
			Repository.Connect(null, true);
			JavaScriptSerializer js = new JavaScriptSerializer();
			return js.Serialize("");
		}

	}
}


