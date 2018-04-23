<%@ Page Language="vb" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DevExpressMvcApplication_Menu.Models.ChangePasswordModel>" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Change Password
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Change Password</h2>
	<p>
		Use the form below to change your password. 
	</p>
	<p>
		New passwords are required to be a minimum of 
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
				<p>
					<input type="submit" value="Change Password" />
				</p>
			</fieldset>
		</div>
<%
%>
</asp:Content>