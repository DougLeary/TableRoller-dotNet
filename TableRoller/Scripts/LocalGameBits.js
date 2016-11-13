// Client-side GameBits functions

var GameBits = {};

GameBits.Table = function() {
	function Roll(numberOfRolls) {
		$.ajax({
			type: "POST",
			url: "RollableTableService.asmx/RollList",
			data: '{ "TableName" : "' + currentTable + '", "NumberOfRolls" : ' + numberOfRolls + ' }',
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function(msg) { ShowResults(msg.d); }
		});
	}
}

GameBits.Repository = function() {
	var Name;
	this.RenderTable = function(table) {
		var result = '<div><b>' + table.TableName + '</b> (<span>' + table.DiceText + '</span>)</div>'
			+ '<table width="100%" rules="all" style="border-collapse:collapse;" border="1" cellspacing="0">'
			+ '<thead><tr><th scope="col" align="center">Roll</th><th scope="col" align="left">Item</th></thead><tbody>';
		for (var i = 0; i < table.Rows.length; i++) {
			result += '<tr><td align="center">' + table.Rows[i].RollRange + '</td><td>' + table.Rows[i].Item + '</td></tr>';
		}
		result += '</tbody></table>';
		return result;
	}

	this.Get = function(repositoryName) {
		return localStorage.getItem(repositoryName);
	}

	this.Save = function() {
		localStorage.setItem(this.Name, this);
	}

}

// web service proxy methods
GameBits.WebService = function() {
	this.GetRepository = function(accessCode, repositoryName) {
		$.ajax({
			type: "POST",
			url: "RollableTableService.asmx/GetRepository",
			data: '{"accessCode" : "' + accessCode + '", "respositoryName" : "' + respositoryName + '"}',
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function(msg) {
				$("#divTable").empty().append(createRollableTable(msg.d));
				$("#rollButtonsBlock").show();
				$("#divResults").empty();
			},
			failure: function(msg) { alert("Failed to get respository"); }
		});
		return null;
	}

	// get a list of all tables in the current repository
	this.GetTableList = function(onCompletion) {
        $.ajax({
			type: "POST",
			url: "RollableTableService.asmx/GetTableList",
			data: "{}",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function(msg) { ShowTableList(msg.d); },
			failure: function(msg) { alert("Failed to get table list"); }
		});
	}

	// get a table from the server repository
	this.GetTable = function(tableName) {
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
}


