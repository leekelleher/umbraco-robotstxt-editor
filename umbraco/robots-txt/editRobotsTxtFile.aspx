<%@ Page Language="C#" MasterPageFile="../masterpages/umbracoPage.Master" AutoEventWireup="true" CodeBehind="editRobotsTxtFile.aspx.cs" Inherits="Our.Umbraco.Tree.RobotsTxt.EditRobotsTxtFile" ValidateRequest="False" %>
<%@ Register TagPrefix="umb" Namespace="umbraco.uicontrols" Assembly="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript">
		function robotsTxtInsertRule(rule, defstr) {
			var value = prompt('Please enter a value for the rule', defstr);
			if (value != null && value != '') {
				UmbEditor.Insert('\n' + rule, value, '<%= editorSource.ClientID %>');
			}
		}
		function robotsTxtCommentOutRules() {
			var editor = (!UmbEditor.IsSimpleEditor) ? UmbEditor._editor : jQuery("#<%= editorSource.ClientID %>");
			var snippet = (!UmbEditor.IsSimpleEditor) ? editor.selection() : editor.getSelection().text;
			if (snippet == '') {
				alert('Please select the text to comment out');
			}
			else {
				editor.replaceSelection(snippet.replace(/\n/g, '\n# '));
			}
		}
		function robotsTxtUncommentRules() {
			var editor = (!UmbEditor.IsSimpleEditor) ? UmbEditor._editor : jQuery("#<%= editorSource.ClientID %>");
			var snippet = (!UmbEditor.IsSimpleEditor) ? editor.selection() : editor.getSelection().text;
			if (snippet == '') {
				alert('Please select the text to uncomment');
			}
			else {
				editor.replaceSelection(snippet.replace(/\n# /g, '\n'));
			}
		}
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
	<umb:UmbracoPanel ID="UmbracoPanel1" runat="server" Text="Robots.txt Editor" hasMenu="true">
		
		<umb:Pane ID="Pane1" runat="server" Text="robots.txt">
		
			<umb:Feedback runat="server" ID="Feedback1" Visible="false" />
			
			<umb:PropertyPanel ID="PropertyPanel1" runat="server" Text="Skip testing (ignores errors)">
				<asp:CheckBox ID="SkipTesting" runat="server"></asp:CheckBox>
			</umb:PropertyPanel>
			
			<umb:PropertyPanel ID="PropertyPanel2" runat="server">
				<umb:CodeArea ID="editorSource" runat="server" AutoResize="true" OffSetX="47" OffSetY="47" />
			</umb:PropertyPanel>
			
		</umb:Pane>
		
	</umb:UmbracoPanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server"></asp:Content>