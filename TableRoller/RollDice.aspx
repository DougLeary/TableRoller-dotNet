<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RollDice.aspx.cs" MasterPageFile="~/Main.Master" Inherits="RealmSmith.RollDice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <title>Dice Roller</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
<!-- onload="javascript:document.form1.TextDieRoll.focus();document.form1.TextDieRoll.select();"> -->

    <div>
		Test page for DieRoll.cs and TableRollResults.cs
		<br />
		<br />Die Roll (e.g. 3d8+1) <asp:TextBox ID="TextDieRoll" runat="server" Columns="12"></asp:TextBox>
		<span style="padding-left:20px;">Number of Rolls </span><asp:TextBox ID="TextRollCount" runat="server" Columns="3" Text="1"></asp:TextBox>
		<br /><asp:Button ID="ButtonRoll" runat="server" Text="Roll" OnClick="ButtonRoll_Click" />
		<br /><asp:Label ID="LabelResult" runat="server"></asp:Label>
    </div>
</asp:Content>
