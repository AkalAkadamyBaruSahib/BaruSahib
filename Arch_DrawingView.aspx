<%@ Page Title="" Language="C#" MasterPageFile="~/ArchMaster.master" AutoEventWireup="true" CodeFile="Arch_DrawingView.aspx.cs" Inherits="Arch_DrawingView" %>
<%@ Register Src="~/Admin/UserControls/EmailTemplate.ascx" TagPrefix="uc1" TagName="EmailTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ClientSideClick(myButton) {
            // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // diable the button
                myButton.disabled = true;
                myButton.className = "btn btn-primary";
                myButton.value = "Please Wait...";
            }
            return true;
        }
    </script>
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>

    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">

                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Drawing Search</h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">


                    <div id="divDesigDetails" runat="server">

                        <table border="0" width="100%">
                            <%--class="table table-striped table-bordered bootstrap-datatable datatable"--%>
                            <tbody>
                                <tr>
                                    <td>Drawing Type:
                            <asp:DropDownList ID="ddlDwgType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDwgType_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td>Select Academy:
        <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExecl" runat="server" Text="Drawing Excel" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnExecl_Click" Width="120px" Height="30px" Font-Bold="True" ForeColor="Black" title="Click this button you get Drawing Execl." data-rel="tooltip" />
                                        <asp:Button ID="btnNonApproved" runat="server" Text="View Non Approved Drawings" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" Font-Bold="True" ForeColor="Black" OnClick="btnNonApproved_Click" Width="235px" />
                                    </td>
                                    <td>
                                        
                                    </td>
                                    
                                </tr>

                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
            <!--/span-->

        </div>
        <%--<table width="100%">
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnExecl" runat="server" Text="Drawing Excel" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnExecl_Click" Width="220px" Height="40px" Font-Bold="True" Font-Size="16pt" ForeColor="Black" title="Click this button you get Drawing Execl." data-rel="tooltip" /></td>
                        <td align="center">Drawing Type:
                            <asp:DropDownList ID="ddlDwgType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDwgType_SelectedIndexChanged"></asp:DropDownList></td>

                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>--%>

        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>
                        <asp:Label ID="lblMatName" runat="server" Text="materialName"> Drawing Details</asp:Label></h2>

                    <div class="box-icon">
                        <h2>
                            <button type="button" id="btnshare" class="btn btn-primary">Email Drawings</button>
                        </h2>
                    </div>
                </div>

                <div class="box-content">
                    <div id="divDrawingView" runat="server"></div>
                    <div id="divAllDrawingView" runat="server"></div>
                </div>
            </div>
            <!--/span-->

        </div>
    </div>
    <div id="divEmailDialog" style="display: none">
        <uc1:EmailTemplate runat="server" ID="EmailTemplate" />
    </div>

</asp:Content>
