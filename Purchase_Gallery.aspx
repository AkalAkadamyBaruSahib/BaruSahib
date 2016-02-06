<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_Gallery.aspx.cs" Inherits="Purchase_Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="content" class="span10">
            <div class="row-fluid sortable">
                <div runat="server" id="divGallery"></div>
			</div>
        </div>
</asp:Content>

