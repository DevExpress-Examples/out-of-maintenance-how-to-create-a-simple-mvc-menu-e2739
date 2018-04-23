<%@ Page Language="vb" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
<%
	  :
%>
	  </h2>
	<p>
		To learn more about DevExpress Extensions for ASP.NET MVC visit <a href="http://devexpress.com/Products/NET/Controls/ASP-NET-MVC/" title="ASP.NET MVC Website">http://devexpress.com/Products/NET/Controls/ASP-NET-MVC/</a>.
	</p>
<% 
	  ViewData("Message") Html.DevExpress().Menu(Function(settings) AnonymousMethod1(settings)).Render()
%>




</asp:Content>


private bool AnonymousMethod1(object settings)
{
	settings.Name = "menu1";
	settings.ShowPopOutImages = DefaultBoolean.True;
	var item = settings.Items.Add("Home");
	var subItem = item.Items.Add("News");
	subItem.Items.Add("For Developers");
	subItem.Items.Add("Website News");
	item.Items.Add("Our Mission");
	item.Items.Add("Our Customers");
	item = settings.Items.Add("Products");
	item.Items.Add("Subscriptions - Packs");
	item.Items.Add(".NET Windows Forms Components");
	item.Items.Add("ASP.NET Components");
	item = settings.Items.Add("Support");
	item.Items.Add("Documentation");
	item.Items.Add("Support Center");
	Return True;
}