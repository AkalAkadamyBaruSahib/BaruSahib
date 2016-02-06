<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_Home.aspx.cs" Inherits="Emp_Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="content" class="span10">
        <div id="divallotedZone" runat="server" class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2>Alloted Zones</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divZone" runat="server"></div>

                </div>
            </div>
            <!--/span-->
        </div>
        <div id="divAcademyDetails" runat="server"></div>
    </div>
</asp:Content>
