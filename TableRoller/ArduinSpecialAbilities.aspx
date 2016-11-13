<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArduinSpecialAbilities.aspx.cs" MasterPageFile="~/Main.Master"
	Inherits="RealmSmith.ArduinSpecialAbilities" %>
<%@ Register Src="RollableTableControl.ascx" TagName="RollableTableControl" TagPrefix="gb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
	<title>Table Roll Demo</title>
	<style type="text/css">
		td, th  
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
		function RollOnTable(numberOfRolls)
        {
            $.ajax({
                type: "POST",
                url: "RollableTableService.asmx/RollList",
                data: '{ "TableName" : "Arduin Special Abilities", "NumberOfRolls" : ' + numberOfRolls + ' }',
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
 
		$(document).ready(function(){
			$("#SampleXmlLink").click(function(e) {
				GetSampleXml(); 
			});
		});
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
	<div>
		<h1>Arduin Special Abilities</h1>
		<p>Based on the table by David Hargrave in the Arduin Grimoire, Vol 1.</p>
		<div>
			<input type="button" value="Roll Once" onclick="RollOnTable(1);" style="padding-right: 2em;" />&nbsp;
			<input type="button" value="Roll 50 times" onclick="RollOnTable(50);" style="padding-right: 2em;" />
			<table>
				<tr>
					<td style="padding-right: 10px; border-right: 2px solid #a0a0a0;">
						<gb:RollableTableControl ID="RollableTableControl1" runat="server" Width="460px" ></gb:RollableTableControl>
					</td>
					<td>
						<div id="divResults"></div>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<br /><br />
</asp:Content>