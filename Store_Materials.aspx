<%@ Page Title="" Language="C#" MasterPageFile="~/StoreMaster.master" AutoEventWireup="true" CodeFile="Store_Materials.aspx.cs" Inherits="Store_Materials" %>

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

        function OpenReceivedMaterial(EMRId, qty, rate) {
            $("input[id*='hdnIsReceived']").val(1);
            $("input[id*='hdnEMRId']").val(EMRId);
            $("input[id*='txtReceivedQty']").val(qty);
            $("input[id*='txtRate']").val(rate);
            $("#divIsReceived").modal('show');
        }

        function OpenDispatchMaterial(EMRId)
        {
            $("input[id*='hdnIsReceived']").val(0);
            $("input[id*='hdnEMRId']").val(EMRId);
            $("input[id*='btnSave']").val('Dispatch');
            $("#trupload").hide();
            $("#divIsReceived").modal('show');
        }

        function OpenUploadbill(EstID) {
            $("input[id*='hdnEstID']").val(EstID);
            $("#divUploadBill").modal('show');
        }

    </script>
    <div id="content" class="span10">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-user"></i>Stock Register</h2>
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
                                    <td>
                                        Select Academy: <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged" ></asp:DropDownList>
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

    <div id="divIsReceived" class="modal hide fade" style="display: none;width:500px">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Stock Register</h3>
        </div>
        <div class="modal-body">
            <asp:HiddenField ID="hdnEMRId" runat="server" />
                           <asp:HiddenField ID="hdnIsReceived" runat="server" />
            <table>
                <tr>
                    <td>Enter Received Quantity:
                        <asp:TextBox ID="txtReceivedQty" Width="100px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trupload">
                    <td>Enter Purchased Bill No:
                        <asp:TextBox ID="txtLinkBillNo" Width="100px" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Received" CssClass="btn btn-primary" />
            <input id="Text1" value="Close" style="width:100px" class="btn btn-primary" data-dismiss="modal" />
        </div>
    </div>

    <div id="divUploadBill"  class="modal hide fade" style="display: none;width:500px">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Stock Register: Upload Bill</h3>
        </div>
        <div class="modal-body">
            <table>
                <tr>
                    <td>Bill No:
                        <asp:TextBox ID="txtBillNo" Width="100px" runat="server"></asp:TextBox>
                    </td>
                    <td>Bill Name:
                        <asp:TextBox ID="txtBillName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="tr1">
                    <td>Upload Purchase Bill:
                        <asp:FileUpload ID="fileUploadBill" runat="server"></asp:FileUpload>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdnEstID" runat="server" />
                           <asp:HiddenField ID="HiddenField2" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnUploadSave" runat="server" OnClick="btnUploadSave_Click" Text="Received" CssClass="btn btn-primary" />
            <input id="btncloase" value="Close" style="width:100px" class="btn btn-primary" data-dismiss="modal" />
            
        </div>
    </div>
    
</asp:Content>

