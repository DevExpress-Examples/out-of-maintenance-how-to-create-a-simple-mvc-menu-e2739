<%@ Control Language="vb" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
	If Request.IsAuthenticated Then
%>
		Welcome <b>
<%
		  :
%>
		  </b>!
		[ 
<%
		  Page.User.Identity.Name :
%>
		   ]
<%
		  Html.ActionLink("Log Off", "LogOff", "Account")
	Else
%> 
		[ 
<%
		  :
%>
		   ]
<%
%>