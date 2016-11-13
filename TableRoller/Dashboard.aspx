<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="RealmSmith.Dashboard"
	MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
	<title>GameBits Dashboard</title>
	<style type="text/css">
		td, th  
		{
			padding-left: 4px;
			padding-right: 4px;
			vertical-align: top;
		}
		
		#divResults table { border-spacing:0px; border-collapse:collapse; width: 300px; }
		#divResults td,th { border: 1px solid black; }
		
		#divXmlLoader { padding: 5px; width: 300px; }
		#divTables { padding: 10px; width: 300px; margin-bottom: 10px; }
		#divTables ul { list-style: none; list-style-type: none; padding-left: 0; margin-left: 0; }
		#divTables ul li { cursor: pointer; }
		#divTables ul li:hover { background-color: #efef00; }
		
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
		var currentTable = "";
		var defaultXmlPath = "./Tables/ADnD1E_Treasure.xml";
		
		function RollOnTable(numberOfRolls)
        {
            $.ajax({
				type: "POST",
				url: "RollableTableService.asmx/RollList",
				data: '{ "TableName" : "' + currentTable + '", "NumberOfRolls" : ' + numberOfRolls + ' }',
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function(msg) { ShowResults(msg.d); }
			});
        }

        function ShowResults(msg) {
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

		$(document).ready(function () {
			$("#SampleXmlLink").click(function (e) {
				GetSampleXml();
			});

			$("#txtFilePath").val(defaultXmlPath);
			$("#txtFilePath").keypress(function (e) {
				if (e.which == 13) {
					LoadXmlTable();
				}
			});

			GetTableList();
			
			// set focus at the end of the file input
			var input = $('#txtFilePath');
			var value = input.val();
			input.focus().val("").val(value);
		});

		function LoadXmlTable() {
			$.ajax({
				type: "POST",
				url: "RollableTableService.asmx/LoadXml",
				data: '{ "filePath" : "' + $("#txtFilePath").val() + '" }',
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function(msg) { GetTableList(); },
				failure: function(msg) { alert("Failed to load xml file " + $("#txtFilePath").val()); }
			});
		}
		
		function GetTableList() {
            $.ajax({
				type: "POST",
				url: "RollableTableService.asmx/GetTableList",
				data: '{ "filePath" : "' + $("#txtFilePath").val() + '" }',
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function(msg) { ShowTableList(msg.d); },
				failure: function(msg) { alert("Failed to get table list"); }
			});
		}
		
		function ShowTableList(msg) {
			$("#divTable").empty()
			$("#rollButtonsBlock").hide();
			$("#divResults").empty();
			var list = "<ul>";
			var arr = jQuery.parseJSON(msg);

			for(var i=0;i<arr.length;i++)
			{
				var row = "<li>" + arr[i] + "</li>";
				list += row;
			}
			list += "</ul>";
			$("#divTableList").html(list);
			$("#divTableList ul li").click(function(e) {
				ShowTable($(this).text());
			});
		}

		function appendRollableTable($obj, msg) {
			$obj.append(getRollableTable(msg));
		}

		function getRollableTable(msg) {
			var table = eval('(' + msg + ')');
			var result = '<div><b>' + table.TableName + '</b> (<span>' + table.DiceText + '</span>)</div>'
				+ '<div><table width="100%" rules="all" style="border-collapse:collapse;" border="1" cellspacing="0">'
				+ '<thead><tr><th scope="col" align="center">Roll</th><th scope="col" align="left">Item</th></thead><tbody>';
			for (var i = 0; i < table.Rows.length; i++) {
				result += '<tr><td align="center">' + table.Rows[i].RollRange + '</td><td>' + table.Rows[i].Item + '</td></tr>';
			}
			result += '</tbody></table></div>';
			return result;
		}

		function ShowTable(tableName) {
			currentTable = tableName;
			$.ajax({
				type: "POST",
				url: "RollableTableService.asmx/GetTable",
				data: '{"tableName" : "' + tableName + '"}',
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (msg) {
					$("#divTable").empty().html(getRollableTable(msg.d));
					$("#rollButtonsBlock").show();
					$("#divResults").empty();
				},
				failure: function (msg) { alert("Failed to get table"); }
			});
			
		}


		function showObject(obj) {
			for (thing in obj) {
				alert(thing);
			}
		}

		function ClearAll() {
			$.ajax({
				type: "POST",
				url: "RollableTableService.asmx/ClearRepository",
				data: '',
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (msg) { GetTableList(); },
				failure: function (msg) { alert("Operation failed."); }
			});
		}

		function dostuff(msg)  {
			alert(msg);
			}

	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
	<div>
		<h1>GameBits Dashboard</h1>
		<Div>
			This demo lets you load tables from XML files, select tables and roll on them. 
		</div>
		<div>
			<table>
				<tr>
					<td style="padding-right: 10px;"
						<div id="divXmlLoader">
							<span style="padding-right: 10px;">XML file path: </span>
							<input id="txtFilePath" type="text" style="width: 270px; margin-right: 10px;" />
							<input id="btnLoadXml" type="button" value="Load" onclick="LoadXmlTable();" />
							<input id="btnClear" type="button" value="Clear" onclick="ClearAll();" style="margin-left: 20px;" />
						</div>
						<div id="divTables">
							<b>Tables</b><br />
							<div id="divTableList">No tables loaded</div>
						</div>
					</td>
					<td>
						<div id="divTable"></div>
						<div id="rollButtonsBlock" style="display:none;">
							<input type="button" value="Roll Once" onclick="RollOnTable(1);" style="padding-right: 2em;" />&nbsp;
							<input type="button" value="Roll 50 times" onclick="RollOnTable(50);" style="padding-right: 2em;" />
						</div>
						<div id="divResults"></div>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<br /><br />
</asp:Content>
