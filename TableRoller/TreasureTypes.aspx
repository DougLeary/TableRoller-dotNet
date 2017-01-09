<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreasureTypes.aspx.cs" MasterPageFile="~/Main.Master" 
	Inherits="TableRoller.TreasureTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
	<title>AD&D 1E Treasure Types</title>
	<script type="text/javascript">
		function RollTreasure()
		{
			var st = '{ "TreasureTypes" : "' + $("#treasureType").val() + '" }';
			$.ajax({
				type: "POST",
				url: "TreasureService.asmx/GetADnD1ETreasureByType",
				data: st,
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (msg) {
				    $("#treasureResult").html(msg.d);
				},
				//error: function (xhr, ajaxOptions, thrownError) {
				//    alert(xhr.status);
				//    alert(thrownError);
				//}
				error: function (msg) {
					alert("data: " + st + "; something went wrong");
				}
			});

		}

		$(document).ready(function () {
			$("#btnRoll").click(function (e) {
				RollTreasure();
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
	<div>
		<h1>AD&amp;D 1E Treasure Types</h1>
		<p>This is a treasure generator based on the Treasure Types table at the back of the AD&amp;D 1st Edition Monster Manual. 
			<br />Another test page for the generalized rollable table object classes I've been working on. 
		</p> 
		<p>Enter a treasure type letter A through Z (or a comma-separated list A,H,Z etc) and click Roll.</p> 
		<p style="margin-left:20px; font-size: 8pt;">
			Note: Each treasure type is a collection of one or more types of loot (gold, silver, gems, scrolls, etc), 
			each of which is only rolled a certain percentage of the time. 
			If all the percent rolls fail, the final result can be Nothing. 
		</p>
	</div>
    <div>
		<input type="text" id="treasureType" value="A" />
		<input type="button" id="btnRoll" value="Roll" />
		<div id="treasureResult" style="font-family: 'Courier New'; margin-left: 20px;"></div>
    </div>
</asp:Content>
