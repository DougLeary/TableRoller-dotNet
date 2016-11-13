<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditGameObject.ascx.cs"
	Inherits="RealmSmith.EditGameObject" %>
<table>
	<tr>
		<th>
			Object Name
		</th>
		<td>
			<asp:TextBox ID="TextName" runat="server"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<th>
			Count
		</th>
		<td>
			<asp:TextBox ID="TextCount" runat="server"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<th>
			Plural
		</th>
		<td>
			<asp:TextBox ID="TextPlural" runat="server"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<th>
			Description
		</th>
		<td>
			<asp:TextBox ID="TextDescription" runat="server"></asp:TextBox>
		</td>
	</tr>
</table>

<br />
<input type="button" id="BtnSave" value="Save" onclick="saveChanges()" />
&nbsp;&nbsp;
<input type="button" id="BtnCancel" value="Cancel" onclick="cancelChanges()" />

