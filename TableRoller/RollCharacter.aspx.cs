using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using GameBits;

namespace TableRoller
{
	public partial class RollCharacter : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			AllowedClassesHeading.Visible = IsPostBack;
		}

		protected void ButtonRoll_Click(object sender, EventArgs e)
		{
			
			CharacterBuilder cb = new CharacterBuilder();
			if (RollingMethod.SelectedItem.Value == "Simple")
			{
				cb.Method = CharacterBuilder.RollingMethod.Simple;
			}
			else if (RollingMethod.SelectedItem.Value == "Method1")
			{
				cb.Method = CharacterBuilder.RollingMethod.Method1;
			}
			else if (RollingMethod.SelectedItem.Value == "Method2")
			{
				cb.Method = CharacterBuilder.RollingMethod.Method2;
			}
			else if (RollingMethod.SelectedItem.Value == "Method3")
			{
				cb.Method = CharacterBuilder.RollingMethod.Method3;
			}

			cb.RollAbilities();
			
			LabelStr.Text = cb.AbilityScores[CharacterBuilder.Ability.Strength].ToString();
			LabelInt.Text = cb.AbilityScores[CharacterBuilder.Ability.Intelligence].ToString();
			LabelWis.Text = cb.AbilityScores[CharacterBuilder.Ability.Wisdom].ToString();
			LabelDex.Text = cb.AbilityScores[CharacterBuilder.Ability.Dexterity].ToString();
			LabelCon.Text = cb.AbilityScores[CharacterBuilder.Ability.Constitution].ToString();
			LabelCha.Text = cb.AbilityScores[CharacterBuilder.Ability.Charisma].ToString();

			StringBuilder sb = new StringBuilder();
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.Cleric)) { sb.Append(", Cleric"); }
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.Druid)) { sb.Append(", Druid"); }
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.Fighter)) { sb.Append(", Fighter"); }
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.Paladin)) { sb.Append(", <b>Paladin</b>"); }
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.Ranger)) { sb.Append(", <b>Ranger</b>"); }
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.MagicUser)) { sb.Append(", MagicUser"); }
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.Illusionist)) { sb.Append(", Illusionist"); }
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.Thief)) { sb.Append(", Thief"); }
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.Assassin)) { sb.Append(", Assassin"); }
			if (cb.IsEligibleFor(CharacterBuilder.CharacterClass.Monk)) { sb.Append(", <b>Monk</b>"); }
			
			if (sb.Length < 3) { sb.Append("  -- cannon fodder --"); }

			AllowedClasses.Text = sb.ToString().Substring(2);
		}

	}
}
