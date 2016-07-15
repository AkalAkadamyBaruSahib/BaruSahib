<%@ Page Title="" Language="C#" MasterPageFile="~/StoreMaster.master" AutoEventWireup="true" CodeFile="StoreHome.aspx.cs" Inherits="StoreHome" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="span10">
        <!-- content starts -->
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>

        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2></h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <form class="form-horizontal">
                        <fieldset>
                            <legend></legend>
                            <div class="control-group">

                                <div class="controls">

                                    <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>

                                </div>
                            </div>
                            <div class="control-group">

                                <div class="controls">
                                    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                    <table width="100%" class="breadcrumb">
                                        <tr>
                                            <th width="20%">
                                                <img src="img/KalgidharTrust.png" /></th>
                                        </tr>
                                        <tr>
                                            <th width="20%" class="breadcrumb">
                                                <h1>Store Management Details
                                                </h1>
                                            </th>
                                        </tr>
                                    </table>
                                </div>
                            </div>


                        </fieldset>
                    </form>
                </div>
            </div>
            <!--/span-->
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2></h2>
                    <div class="box-icon">
                        <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divZone" runat="server"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

