<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextProviderTest.aspx.cs" Inherits="RealmSmith.TextProviderTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:GridView ID="Grid" runat="server"></asp:GridView>
		<br />
		<asp:Button ID="BtnRoll" runat="server" Text="Roll" OnClick="RollClick" />
		<br />
		<asp:Label ID="Results" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
