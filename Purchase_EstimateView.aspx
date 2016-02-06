<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_EstimateView.aspx.cs" Inherits="Purchase_EstimateView" %>

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
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <div class="row-fluid sortable" runat="server" id="divExcel">



            <div class="box span12">

                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Estimate </h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">

                    <div id="Div1" runat="server"></div>
                    <div id="divDesigDetails" runat="server">

                        <table border="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="width:350px">
                                        <b>Search by EstID:-</b> <asp:TextBox ID="txtEstid" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnEstSearch" Text="Search" runat="server" CssClass="btn btn-primary" OnClick="btnEstSearch_Click" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="btnExecl" runat="server" Text="Estimate Statement" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnExecl_Click" />
                                        <asp:Button ID="btnExcel1" runat="server" Text="Estimate Material Statement" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnExcel1_Click" />
                                        <%--Select Academy: <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged" ></asp:DropDownList>--%>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>

        </div>
        <div id="pnlPdf" runat="server"></div>
        <div id="divEstimateDetails" runat="server"></div>
    </div>
</asp:Content>
