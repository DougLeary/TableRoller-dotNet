<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreasureTypes.aspx.cs" Inherits="TableRoller.TreasureTypes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:TextBox ID="TreasureType" runat="server" Text="A"></asp:TextBox>
		<asp:Button ID="BtnRoll" runat="server" Text="Roll" OnClick="RollClick" />
		<br />
		<asp:Label ID="Results" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
