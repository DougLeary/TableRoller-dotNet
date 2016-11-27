<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RollableTableControl.ascx.cs" 
	Inherits="TableRoller.RollableTableControl" %>

<b><asp:Literal ID="TableTitle" runat="server" Text="<%# TitleText %>"></asp:Literal></b>
	 (<asp:Label ID="DiceDisplay" runat="server"></asp:Label>)
<asp:GridView ID="Grid" runat="server" AutoGenerateColumns="false" CssClass="RollableTableControl">
	<Columns>
		<asp:BoundField DataField="RangeText" HeaderText="Roll" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="RollableTableControl-RollColumn" />
		<asp:BoundField DataField="Item" HeaderText="Item" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="RollableTableControl-ResultColumn" />
	</Columns>
</asp:GridView>

