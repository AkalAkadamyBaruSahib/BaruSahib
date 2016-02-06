<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_ViewEstMaterial.aspx.cs" EnableEventValidation="false" Inherits="Purchase_ViewEstMaterial" %>

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
                myButton.className = "btn btn-warning";
                myButton.value = "Please Wait...";
            }
            return true;
        }
        function openModelPopUp() {
            $("#divAssignEmployee").modal('show');
        }
    </script>
    <script type="text/javascript">
        function ClientSideClick2(myButton) {
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
    <div class="box span10">
        <div class="box-header well" data-original-title>
            <h2><i class="icon-user"></i>Material Details for
                <asp:Label ID="lblZoneName" runat="server"></asp:Label>
                |
                <asp:Label ID="lblAcademy" runat="server"></asp:Label>
                |
                <asp:Label ID="lblEstId" runat="server"></asp:Label></h2>
            <div class="box-icon">
                <input type="button" class="btn-primary" onclick="javascript: openModelPopUp();" value="Assign Purchase Officer" />
                <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>--%>
            </div>
        </div>
        <div class="box-content">
            <asp:GridView runat="server" AutoGenerateColumns="false" DataKeyNames="EstId" ID="gvMaterailDetailForPurchase"
                class="table table-striped table-bordered">
                <Columns>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBok" runat="server" />
                            <asp:HiddenField ID="hdnSno" runat="server" Value='<%# Eval("Sno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="EstId" HeaderText="EstId" ItemStyle-CssClass="hideGridColumn" />
                    <asp:BoundField DataField="MatName" HeaderText="MatName" />
                    <asp:BoundField DataField="UnitName" HeaderText="UnitName" />
                    <asp:BoundField DataField="Qty" HeaderText="Qty" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Purchaser Name" />
                </Columns>
            </asp:GridView>

        </div>

        <div id="divAssignEmployee" class="modal hide fade">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Select Purchase Employee</h3>
            </div>
            <div class="modal-body">
                <table>
                    <tr>
                        <td>Select Employee</td>
                        <td>
                            <asp:DropDownList ID="ddlEmployee" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center"></td>
                    </tr>
                </table>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn-primary" />
                <a href="#" class="btn btn-primary" data-dismiss="modal">Close</a>
            </div>
        </div>
    </div>
</asp:Content>

