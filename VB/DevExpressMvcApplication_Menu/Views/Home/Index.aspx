<%@ Page Language="vb" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%:ViewData("Message")%></h2>
    <p>
        To learn more about DevExpress Extensions for ASP.NET MVC visit <a href="http://devexpress.com/Products/NET/Controls/ASP-NET-MVC/" title="ASP.NET MVC Website">http://devexpress.com/Products/NET/Controls/ASP-NET-MVC/</a>.
    </p>


<% 
    Html.DevExpress().Menu(Sub(settings)
        settings.Name = "menu1"
        settings.ShowPopOutImages = DefaultBoolean.True
        Dim item = settings.Items.Add("Home")
        Dim subItem = item.Items.Add("News")
        subItem.Items.Add("For Developers")
        subItem.Items.Add("Website News")
        item.Items.Add("Our Mission")
        item.Items.Add("Our Customers")
        item = settings.Items.Add("Products")
        item.Items.Add("Subscriptions - Packs")
        item.Items.Add(".NET Windows Forms Components")
        item.Items.Add("ASP.NET Components")
        item = settings.Items.Add("Support")
        item.Items.Add("Documentation")
        item.Items.Add("Support Center")
    End Sub).Render()
%>




</asp:Content>