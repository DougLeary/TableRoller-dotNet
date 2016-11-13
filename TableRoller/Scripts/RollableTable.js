(function ($) {
	jQuery.fn.RollableTable = function (tableName) {
		$.ajax({
			type: "POST",
			url: "RollableTableService.asmx/GetTable",
			data: '{"tableName" : "' + tableName + '"}',
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (msg) {
				$("#divTable").empty().append(createRollableTable(msg.d));
				$("#rollButtonsBlock").show();
				$("#divResults").empty();
			},
			failure: function (msg) { alert("Failed to get table"); }
		});
	};
});  ( jQuery );

function createRollableTable(msg) {
	var table = eval('(' + msg + ')');
	var result = '<div><b>' + table.TableName + '</b> (<span>' + table.DiceText + '</span>)</div>'
				+ '<table width="100%" rules="all" style="border-collapse:collapse;" border="1" cellspacing="0">'
				+ '<thead><tr><th scope="col" align="center">Roll</th><th scope="col" align="left">Item</th></thead><tbody>';
	for (var i = 0; i < table.Rows.length; i++) {
		result += '<tr><td align="center">' + table.Rows[i].RollRange + '</td><td>' + table.Rows[i].Item + '</td></tr>';
	}
	result += '</tbody></table>';
	return result;
}

