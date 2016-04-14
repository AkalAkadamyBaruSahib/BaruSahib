<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_SendMessage.aspx.cs" Inherits="Security_SendMessage" %>

<%@ Register Src="~/Admin/UserControls/BodySendMessage.ascx" TagPrefix="uc1" TagName="BodySendMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <uc1:BodySendMessage runat="server" ID="BodySendMessage" />
 
</asp:Content>

