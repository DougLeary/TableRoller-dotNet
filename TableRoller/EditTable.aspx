<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTable.aspx.cs" MasterPageFile="~/Main.Master" 
	Inherits="RealmSmith.EditTable" %>
<%@ Register Src="RollableTableEditor.ascx" TagName="RollableTableEditor" TagPrefix="gb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
	<gb:RollableTableEditor ID="Monsters1" runat="server" ></gb:RollableTableEditor>
</asp:Content>
