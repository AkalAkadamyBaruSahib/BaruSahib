<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_Home.aspx.cs" Inherits="Emp_Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="content" class="span10">
        <div id="divallotedZone" runat="server" class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                       <marquee behavior='scroll' direction='left'>WELCOME TO AKALSEWA SOFTWARE | ALLOTED ZONES</marquee>
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
