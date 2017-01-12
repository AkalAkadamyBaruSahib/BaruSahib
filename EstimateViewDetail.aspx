<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="EstimateViewDetail.aspx.cs" Inherits="EstimateViewDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:HiddenField ID="hdnWorkAllotID" runat="server" />
<div id="content" class="span10">
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well">
                <h2><i class="icon-edit"></i><asp:Label ID="lblHeader" runat="server" Text="Estimate Details"></asp:Label></h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div id="divEstimateDetails" runat="server"></div>
        </div>
    </div>
</div>
</asp:Content>

