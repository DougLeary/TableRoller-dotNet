<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExampleBindableControl.ascx.cs" Inherits="RealmSmith.ExampleBindableControl" %>
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
<asp:DataList ID="DataList1" runat="server" RepeatColumns="1" 
	ShowFooter="False" ShowHeader="False">
	<ItemTemplate>
		<asp:Label ID="Label1" runat="server" 
			Text='<%# DataBinder.Eval(Container, "TableName") %>'></asp:Label>
	</ItemTemplate>
	<SelectedItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
		Font-Strikeout="False" Font-Underline="False" />
</asp:DataList>