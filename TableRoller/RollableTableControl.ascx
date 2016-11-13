<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RollableTableControl.ascx.cs" 
	Inherits="RealmSmith.RollableTableControl" %>

<b><asp:Literal ID="TableTitle" runat="server" Text="<%# TitleText %>"></asp:Literal></b>
	 (<asp:Label ID="DiceDisplay" runat="server"></asp:Label>)
<asp:GridView ID="Grid" runat="server" AutoGenerateColumns="false">
	<Columns>
		<asp:BoundField DataField="RangeText" HeaderText="Roll" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
		<asp:BoundField DataField="Item" HeaderText="Item" HeaderStyle-HorizontalAlign="Left" />
	</Columns>
</asp:GridView>

