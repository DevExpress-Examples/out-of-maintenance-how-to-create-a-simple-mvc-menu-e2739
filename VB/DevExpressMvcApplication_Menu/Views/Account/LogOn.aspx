<%@ Page Language="vb" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DevExpressMvcApplication_Menu.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Log On
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Log On</h2>
	<p>
		Please enter your username and password. 
<%
		  :
%>
		   if you don't have an account.
	</p>
<%
		  Html.ActionLink("Register", "Register") using (Html.BeginForm())
%>
<%
		  :
%>
		<div>
			<fieldset>
				<legend>Account Information</legend>
				<div class="editor-label">
<%
%>
				</div>
				<div class="editor-field">
<%
%>
<%
%>
				</div>
				<div class="editor-label">
<%
%>
				</div>
				<div class="editor-field">
<%
%>
<%
%>
				</div>
				<div class="editor-label">
<%
%>
<%
%>
				</div>
				<p>
					<input type="submit" value="Log On" />
				</p>
			</fieldset>
		</div>
<%
%>
</asp:Content>