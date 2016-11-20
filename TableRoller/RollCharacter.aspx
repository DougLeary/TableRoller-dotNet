<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RollCharacter.aspx.cs" MasterPageFile="~/Main.Master" Inherits="TableRoller.RollCharacter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <title>Character Roller</title>
    <style type="text/css">
		#CharacterRoller th { text-align: left; }
		.RollingMethod td { text-align: left; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="CharacterRoller">
	    <h3>Random Character Roller</h3>
		<b>Rolling method</b>
		<div style="padding-left:15px;">
			<asp:RadioButtonList ID="RollingMethod" runat="server" CssClass="RollingMethod">
				<asp:ListItem Value="Simple" Text="Roll 3 dice for each attribute."></asp:ListItem>
				<asp:ListItem Value="Method1" Text="Roll 4 dice and take the top 3." Selected="True"></asp:ListItem>
				<asp:ListItem Value="Method2" Text="Roll 3d6 12 times and keep the top 6 results."></asp:ListItem>
				<asp:ListItem Value="Method3" Text="For each ability roll 3d6 6 times and keep the top roll."></asp:ListItem>
			</asp:RadioButtonList>
		</div>
		<asp:Button ID="ButtonRoll" runat="server" Text="Roll" OnClick="ButtonRoll_Click" />
		<table cellpadding="0" cellspacing="5">
			<tr><th style="width:40px;">Str:</th><td style="width:20px;"><asp:Label ID="LabelStr" runat="server"></asp:Label></td></tr>
			<tr><th>Int:</th><td><asp:Label ID="LabelInt" runat="server"></asp:Label></td></tr>
			<tr><th>Wis:</th><td><asp:Label ID="LabelWis" runat="server"></asp:Label></td></tr>
			<tr><th>Dex:</th><td><asp:Label ID="LabelDex" runat="server"></asp:Label></td></tr>
			<tr><th>Con:</th><td><asp:Label ID="LabelCon" runat="server"></asp:Label></td></tr>
			<tr><th>Cha:</th><td><asp:Label ID="LabelCha" runat="server"></asp:Label></td></tr>
		</table>
		<h3><asp:Label ID="AllowedClassesHeading" runat="server" Text="Allowed Classes"></asp:Label></h3>
		<asp:Label ID="AllowedClasses" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
