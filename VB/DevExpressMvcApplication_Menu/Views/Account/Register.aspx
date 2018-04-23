<%@ Page Language="vb" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DevExpressMvcApplication_Menu.Models.RegisterModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Register
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Create a New Account</h2>
	<p>
		Use the form below to create a new account. 
	</p>
	<p>
		Passwords are required to be a minimum of 
<%
		  :
%>
		   characters in length.
	</p>
<%
		  ViewData("PasswordLength") using (Html.BeginForm())
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
				<p>
					<input type="submit" value="Register" />
				</p>
			</fieldset>
		</div>
<%
%>
</asp:Content>