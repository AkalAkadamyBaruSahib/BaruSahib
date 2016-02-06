<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_EstimateAcademyWise.aspx.cs" Inherits="Emp_EstimateAcademyWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Estimate Search</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="pnlPdf" runat="server"></div>
                    <div id="divDesigDetails" runat="server">
                        <table border="0" width="100%">
                            <tbody>
                                <tr>
                                    <td>Select Academy:
                                        <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExecl" runat="server" Text="Estimate Statement" CssClass="btn btn-primary" OnClick="btnExecl_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                        <asp:Button ID="btnExcel1" runat="server" Text="Estimate Material Statement" CssClass="btn btn-primary" OnClick="btnExcel1_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                        <asp:Button id="btnNonApproved" runat="server" Text="View Rejected\Non Approved Estimates" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)" title="Click this button you get Estimate which are uploaded by user" data-rel="tooltip" OnClick="btnNonApproved_Click"/>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div id="divEstimateDetails" runat="server"></div>
    </div>
</asp:Content>