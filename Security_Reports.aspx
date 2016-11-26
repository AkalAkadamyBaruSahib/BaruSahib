<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_Reports.aspx.cs" Inherits="Security_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div id="content" class="span10">

        <div class="box-header well">
            <h2><i class="icon-user"></i>Download Security Employee Detail Reports</h2>
            <div class="box-icon">
                <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
            </div>
        </div>


        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-content" id="divAccordingToZone">
                    Select Zone:
                    <asp:DropDownList ID="ddlZone" runat="server" style="margin-left: 41px;"></asp:DropDownList><br />
                      Select Designation:
                    <asp:DropDownList ID="ddlDesignation" runat="server"></asp:DropDownList><br />
               </div>
               </div>
        </div>
        <asp:Button ID="btndownload" runat="server" Text="Click To Download Execl" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="235px" />
    </div>
</asp:Content>

