<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="PurchaserPendencyView.aspx.cs" Inherits="PurchaserPendencyView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <div id="content" class="span10">
    <div class="row-fluid sortable" runat="server" id="divAllotment">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-user"></i>Pendency Details For:-
                    <asp:Label ID="lblPurchaserName" runat="server"></asp:Label></h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <div id="divPurchaserPendencyView" runat="server">
                </div>
            </div>
            <!--/span-->
        </div></div></div>
</asp:Content>

