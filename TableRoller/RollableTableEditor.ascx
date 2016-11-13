<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RollableTableEditor.ascx.cs" Inherits="RealmSmith.RollableTableEditor" %>

<script type="text/javascript">
	function parseItem(itemText) {
		var parts = itemText.split(",");
		var objects = new Array(parts.length);
		for (var i = 0; i < parts.length; i++) {
			objects[i] = parseObject(parts[i]);
		}
	}
	
	function parseObject(objectText) {
		// test objectText against several regexps, looking for the most complex pattern that matches
		// Actually this would be a GREAT application for that grammar parser if we do it server side
	}
	
	function editRow(e) {
		// add editRow click to any row that contains an input
		$("input:first", $(this).parent()).parent().parent().bind("click", editRow);
		// replace inputs with spans
		$("input", $(this).parent()).each(function() {
			var $span = $("<span></span>");
			$span.html($(this).val());
			$span.appendTo($(this).parent());
			$span.addClass($(this).attr("class").replace("Edit",""));
			$(this).remove();
		});
		// replace the span in each td in the current row with an input
		$("span", this).each(function() { 
			var $input = $('<input type="text" />');
			$input.val($(this).html());
			$input.appendTo($(this).parent());
			$input.addClass($(this).attr("class") + "Edit");
			$input.bind("change", rowChanged);
			$(this).remove();
		});
		$(this).unbind("click", editRow);
	}

	function rowChanged(e) {
		// input changed
		var inputs = $("input", $(this).parent().parent());
		$("#divTest").html("roll: " + inputs[0].value + ", item: " + inputs[1].value);
	}
		
	function initRollableTableEditor(tableId, hoverColor, normalColor)
	{
		// row hover
		var tableRows = $("#" + tableId + " tr").not(':first');
		tableRows.hover(
			function () {
				$(this).css("background",hoverColor);
			}, 
			function () {
				$(this).css("background",normalColor);
			}
		);
		// row click
		tableRows.bind("click", editRow);
	}
</script>

<div>
	<asp:Repeater ID="Repeater1" runat="server">
		<HeaderTemplate>
			<div style="margin-top:4px;">
				<span style="padding-right:10px; font-weight: bold;">Table Name</span>
				<input type="text" style="width:250px;" value="<%# TitleText %>" /> <!-- tablename -->
			</div>
			<div style="margin-top:4px;">
				<span style="padding-right:10px; font-weight: bold;">Die Roll</span>
				<input type="text" style="width:50px;" value="<%# DieRollText %>" /> <!-- dieroll -->
			</div>
			<table class="TableEditor" id="<%# ClientId %>" cellpadding="1" cellspacing="0">
				<thead>
					<th>Roll</th>
					<th style="padding-left: 5px; text-align: left;">Item</th>
				</thead>
				<tbody>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td><span class="rollableTableRange"><%# DataBinder.Eval(Container, "DataItem.RangeText")%></span></td>
				<td><span class="rollableTableItem"><%# DataBinder.Eval(Container, "DataItem.Item")%></span></td>
<%--
				<td><input type="text" style="width:40px; text-align: center;" value="<%# DataBinder.Eval(Container, "DataItem.RangeText")%>" /></td>
				<td><input type="text" style="width:300px;" value="<%# DataBinder.Eval(Container, "DataItem.Item")%>" /></td>
--%>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
				</tbody>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<div style="margin-top:4px;">
		<input id="SaveButton" type="button" value="Save" />
	</div>
	<div id="divTest" style="border:1px solid green; padding:5px;"></div>
</div>

<script type="text/javascript">
	function clickSaveButton(event) {
		saveTable();
	}
	function saveTable() {
		alert("saving table");
		
	}
</script>

<script type="text/javascript">
	$(document).ready( function(event) {
		$("#SaveButton").click(clickSaveButton);
		$("th").css("background", "#ffcc00");
		initRollableTableEditor("gb_Monsters_Table_1", "yellow", "");
	});
</script>

