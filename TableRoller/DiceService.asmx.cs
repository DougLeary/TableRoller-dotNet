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

namespace TableRoller.Services
{
	/// <summary>
	/// Dice Rolling Service
	/// </summary>
	[WebService(Namespace = "http://tableroller.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.Web.Script.Services.ScriptService]
	public class DiceService : WebService
	{
		public class RollResult
		{
			public string DieRollString;
			public int RolledValue;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public RollResult Roll(string dieRollString)
		{
			DieRoll dr = new DieRoll(dieRollString);
			RollResult result = new RollResult();
			result.DieRollString = dieRollString;
			result.RolledValue = dr.Roll();
			return result;
		}
	}
}
