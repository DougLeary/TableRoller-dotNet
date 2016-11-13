<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowTable.aspx.cs" Inherits="RealmSmith.ShowTable" %>
<%@ Register Src="RollableTableControl.ascx" TagName="RollableTableControl" TagPrefix="gb" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
		<gb:RollableTableControl ID="RollableTable1" runat="server" ></gb:RollableTableControl>
    </form>
</body>
</html>
