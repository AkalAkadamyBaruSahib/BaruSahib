<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkShop_Gallery.aspx.cs" Inherits="WorkShop_Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="content" class="span10">
            <div class="row-fluid sortable">
                <div runat="server" id="divGallery"></div>
			</div>
        </div>
</asp:Content>

