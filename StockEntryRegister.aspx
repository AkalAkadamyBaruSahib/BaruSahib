<%@ Page Title="" Language="C#" MasterPageFile="~/StoreMaster.master" AutoEventWireup="true" CodeFile="StockEntryRegister.aspx.cs" Inherits="StockEntryRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-user"></i>Material dispatch status</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divDesigDetails" runat="server">
                        <table border="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnExecl" runat="server" Text="Dispatch Excel Download" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnExecl_Click" />
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


        <div id="divReceivedItem" class="modal hide fade">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Enter the Received Material Details</h3>
            </div>
            <div class="modal-body">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMaterialName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Rate:<asp:TextBox ID="txtRate" runat="server"></asp:TextBox></td>
                        <td>
                            Bill Scan Copy: <asp:FileUpload ID="upbill" runat="server" AllowMultiple="true" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center">
                            
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modal-footer">
                <%--<asp:Button ID="btnSave" runat="server" Text="Received" OnClick="btnSave_Click" CssClass="btn-primary" />
                <a href="#" class="btn btn-primary" data-dismiss="modal">Close</a>--%>
            </div>
        </div>



    </div>

</asp:Content>

