using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using GameBits;

namespace TableRoller
{
	/// <summary>
	/// CharacterBuilder - really just a demo class for DieRoll
	/// </summary>
	public class CharacterBuilder
	{
		public enum RollingMethod
		{
			Simple,
			Method1,
			Method2,
			Method3,
			Method4
		}

		public enum Ability
		{
			Strength,
			Intelligence,
			Wisdom,
			Dexterity,
			Constitution,
			Charisma
		}

		public enum CharacterClass
		{
			Cleric,
			Druid,
			Fighter,
			Paladin,
			Ranger,
			MagicUser,
			Illusionist,
			Thief,
			Assassin,
			Monk
		}

		public static Hashtable Prerequisites;

		static CharacterBuilder()
		{
			Prerequisites = new Hashtable(10);
			Prerequisites.Add(CharacterClass.Cleric, new int[] { 0, 0, 9, 0, 0, 0 });
			Prerequisites.Add(CharacterClass.Druid, new int[] { 0, 0, 12, 0, 0, 15 });
			Prerequisites.Add(CharacterClass.Fighter, new int[] { 9, 0, 0, 0, 7, 0 });
			Prerequisites.Add(CharacterClass.Paladin, new int[] { 12, 9, 13, 0, 9, 17 });
			Prerequisites.Add(CharacterClass.Ranger, new int[] { 13, 13, 14, 0, 14, 0 });
			Prerequisites.Add(CharacterClass.MagicUser, new int[] { 0, 9, 0, 6, 0, 0 });
			Prerequisites.Add(CharacterClass.Illusionist, new int[] { 0, 15, 0, 16, 0, 0 });
			Prerequisites.Add(CharacterClass.Thief, new int[] { 0, 0, 0, 9, 0, 0 });
			Prerequisites.Add(CharacterClass.Assassin, new int[] { 12, 11, 0, 12, 0, 0 });
			Prerequisites.Add(CharacterClass.Monk, new int[] { 15, 0, 15, 15, 11, 0 });
		}

		public RollingMethod Method;
		public Hashtable AbilityScores;

		public CharacterBuilder()
		{
			Method = RollingMethod.Simple;
			AbilityScores = new Hashtable(6);
		}

		public void RollAbilities()
		{
			DieRoll dr;
			if (Method == RollingMethod.Simple)
			{
				// Simply roll 3d6 for each attribute and take what you get
				dr = new DieRoll(3, 6, 0);
				DieRollResults results = dr.MultiRoll(6);
				SetAbilities(results);
			}
			else if (Method == RollingMethod.Method1)
			{
				// Roll 4d6 for each ability and keep the top 3 dice
				dr = new DieRoll(4, 6, 0);
				dr.Keep = 3;
				AbilityScores.Add(Ability.Strength, dr.Roll());
				AbilityScores.Add(Ability.Intelligence, dr.Roll());
				AbilityScores.Add(Ability.Wisdom, dr.Roll());
				AbilityScores.Add(Ability.Dexterity, dr.Roll());
				AbilityScores.Add(Ability.Constitution, dr.Roll());
				AbilityScores.Add(Ability.Charisma, dr.Roll());
			}
			else if (Method == RollingMethod.Method2)
			{
				// Roll 3d6 12 times and keep the best 6 results
				dr = new DieRoll(3, 6, 0);
				DieRollResults results = dr.MultiRoll(12, 6);
				results.KeepBest(6);
				SetAbilities(results);
			}
			else if (Method == RollingMethod.Method3)
			{
				// For each ability roll 3d6 6 times and keep the best result
				dr = new DieRoll(3, 6, 0);
				DieRollResults results = new DieRollResults();
				for (int i = 0; i < 6; i++)
				{
					// when called with params Roll returns a RollResult;
					// add the first and only member of this RollResult, an int,  to the results list
					results.Add(dr.MultiRoll(6,1)[0]);
				}
				SetAbilities(results);
			}
		}

		private void SetAbilities(DieRollResults results)
		{
			AbilityScores.Add(Ability.Strength, results[0]);
			AbilityScores.Add(Ability.Intelligence, results[1]);
			AbilityScores.Add(Ability.Wisdom, results[2]);
			AbilityScores.Add(Ability.Dexterity, results[3]);
			AbilityScores.Add(Ability.Constitution, results[4]);
			AbilityScores.Add(Ability.Charisma, results[5]);
		}

		public bool IsEligibleFor(CharacterClass Class)
		{
			int[] requirements = (int[])Prerequisites[Class];
			for (int i = (int)Ability.Strength; i <= (int)Ability.Charisma; i++)
			{
				Object obj = AbilityScores[(Ability)i];
				int result = (int)obj;
				if (result < requirements[i])
				{
					return false;
				}
			}
			return true;
		}
	}
}
