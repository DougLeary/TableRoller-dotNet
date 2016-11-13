<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableRollDemo2.aspx.cs" MasterPageFile="~/Main.Master" 
	Inherits="RealmSmith.TableRollDemo2" %>
<%@ Register Src="RollableTableControl.ascx" TagName="RollableTableControl" TagPrefix="gb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
	<title>Table Roll Demo</title>
	<style type="text/css">
		#TableRollDemo td, th  
		{
			padding-left: 4px;
			padding-right: 4px;
			vertical-align: top;
		}
		#divResults table { border-spacing:0px; border-collapse:collapse; width: 300px; }
		#divResults td,th { border: 1px solid black; }
		
		th
		{
			background-color: #a0ffa0;
		}
		.rollColumn
		{
			text-align: center;
		}
	</style>
	<script type="text/javascript" language="javascript">
		function showEditor()
		{
			$("#objectEditorContent").load("GameObjectEditor.htm");
			$("#objectEditor").show();
		}
		
		function saveEdit()
		{
		}
	</script>
	<script type="text/javascript" language="javascript">
		function RollOnTable(tableName, numberOfRolls)
        {
            $.ajax({
                type: "POST",
                url: "RollableTableService.asmx/RollList",
                data: '{ "TableName" : "' + tableName + '", "NumberOfRolls" : ' + numberOfRolls + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) { BuildTable(msg.d); }
            });
        }
 
		function BuildTable(msg) {
			var table = "<table width='100%'><thead><tr><th>Item</th><th>Count</th></thead><tbody>";
			var arr = jQuery.parseJSON(msg);

			for(var i=0;i<arr.length;i++)
			{
				var obj = arr[i];
				var row = "<tr><td>" + obj.Name + '</td><td align="center">' + obj.Count + "</td></tr>";
				table += row;
			}
			table += "</tbody></table>";
			$("#divResults").html("<b>Results</b>" + table);
		}
 
		function ShowSampleXml(text) {
			$("#divResults").empty();
			$("<textarea rows='35' cols='70' style='font-family:Arial; font-size: 8pt;' />").appendTo("#divResults").val(text);
		}
		
		function GetSampleXml() {
            $.ajax({
                type: "GET",
                url: "./Tables/ADnD1E_Treasure.xml",	// insert an <input> field in divResults and then set the input's val() to the responseText
                complete: function(xhr, status) { ShowSampleXml(xhr.responseText); }
            });
		}
		
		$(document).ready(function(){
			$("#SampleXmlLink").click(function(e) {
				GetSampleXml(); 
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
	<div id="TableRollDemo">
		<h1>Table Roll Demo</h1>
		<p>AD&D 1st Edition Treasure Tables <a id="SampleXmlLink" href="javascript:void();">[see XML]</a>
		</p>
		<div>
			<input type="button" value="Roll Once" onclick='RollOnTable("III. Magic Items", 1);' />&nbsp;
			<input type="button" value="Roll 50 times" onclick='RollOnTable("III. Magic Items", 50);' />&nbsp;
			<br />
			<br />
			<table>
				<tr>
					<td style="padding-right: 10px; border-right: 2px solid #a0a0a0;">
						<gb:RollableTableControl ID="MainTable" runat="server" ></gb:RollableTableControl>
					</td>
					<td>
						<div id="divResults"></div>
					</td>
				</tr>
			</table>
				<br /><br /><hr />
		</div>
		<br />
	</div>

</asp:Content>
