<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyViewEstimateMaterial.ascx.cs" Inherits="Admin_UserControls_BodyViewEstimateMaterial" %>
<script type="text/javascript">
    function OnClientClick() {
        // Client side validation
        if (Page_ClientValidate()) {
            var gvcheck = document.getElementById('<%= gvMaterailDetailForPurchase.ClientID %>');
            if ($('td :checkbox', gvcheck).prop("checked") == true) {
                if (Page_ClientValidate()) {
                    if (confirm('Are you sure you want to send this Material Directly?'))
                        return true;
                    else
                        return false;
                }
            }
        }
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

    function myFunction() {
        var x, text;
        var ret = true;
        // Get the value of the input field with id="numb"
        x = document.getElementById("txtRate").value;

        // If x is Not a Number or less than one or greater than 10
        if (isNaN(x) || x < 1) {
            ret = false;
        }
        return ret;
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
            <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
            <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
            <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
        </div>
    </div>
    <div class="box-content">
        <asp:GridView runat="server" AutoGenerateColumns="false" DataKeyNames="EstId" ID="gvMaterailDetailForPurchase"
            class="table table-striped table-bordered bootstrap-datatable datatable"
            OnRowCommand="gvMaterailDetailForPurchase_RowCommand" OnRowDataBound="gvMaterailDetailForPurchase_RowDataBound" Visible="false">
            <Columns>
                <asp:TemplateField HeaderText="SNO">
                    <ItemTemplate>
                        <%# Container.DataItemIndex+1 %>
                        <asp:HiddenField runat="server" ID="txtMatID" Value='<%#Eval("MatID") %>' />
                        <asp:HiddenField runat="server" ID="hdnMatTypeID" Value='<%#Eval("MatTypeID") %>' />
                        <asp:HiddenField runat="server" ID="txtUnitID" Value='<%#Eval("UnitID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField  DataField="MatName" HeaderText="MatName" />
                <asp:BoundField DataField="UnitName" HeaderText="UnitName" />
                <asp:BoundField DataField="Qty" HeaderText="RequiredQty" />
                <asp:BoundField DataField="PurchaseQty" HeaderText="Purchased Qty" />
                <asp:TemplateField HeaderText="Purchase">
                    <ItemTemplate>
                        <asp:TextBox runat="server" Width="100px" ID="txtPurchaseQty"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate">
                    <ItemTemplate>
                        <asp:TextBox runat="server" Width="100px" ID="txtRate"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$" ForeColor="Red" ErrorMessage="*"
                            ControlToValidate="txtRate" />
                        <asp:HiddenField runat="server" ID="txtEstID" Value='<%#Eval("EstID") %>' />
                        <asp:HiddenField runat="server" ID="hdnPurchaseQty" Value='<%#Eval("PurchaseQty") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Direct Purchase">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDirectPurchase" runat="server" Checked="false" Visible="false" ToolTip="Direct Purchase"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Purchase Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="txtDispatchDate" Text='<%# Eval("DispatchDate") %>' Visible="false" Style="display: none;"></asp:Label>
                        <asp:Button runat="server" ID="btnDispatch" CssClass="btn btn-primary" OnClientClick=" if (Page_ClientValidate()) { var gvcheck = document.getElementById('<%= gvMaterailDetailForPurchase.ClientID %>'); if ($('td :checkbox', gvcheck).prop('checked') == true) {if (Page_ClientValidate()) {  if (confirm('Are you sure you want to send this Material Directly?'))
                        return true; else return false; } } }"  data-rel="tooltip" data-original-title="Click To Dispatch Material" Text="Purchase Material" CommandName="DispatchDate" CommandArgument='<%#Eval("Sno") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:GridView runat="server" AutoGenerateColumns="false" DataKeyNames="EstId" ID="gvWorkShopMaterial"
            class="table table-striped table-bordered bootstrap-datatable datatable" Visible="false" OnRowDataBound="gvWorkShopMaterial_RowDataBound">
           
            <Columns>
                <asp:TemplateField HeaderText="SNO">
                    <ItemTemplate>
                        <%# Container.DataItemIndex+1 %>
                         <asp:HiddenField runat="server" ID="hdnEstID" Value='<%#Eval("EstID") %>' />
                        <asp:HiddenField runat="server" ID="hdnMatID" Value='<%#Eval("MatID") %>' />
                        <asp:HiddenField runat="server" ID="hdnSno" Value='<%#Eval("Sno") %>' />
                        <asp:HiddenField runat="server" ID="hdnUnitID" Value='<%#Eval("UnitID") %>' />
                        <asp:HiddenField runat="server" ID="hdnDispatchQty" Value='<%#Eval("DispatchQty") %>' />
                          <asp:HiddenField runat="server" ID="hdnInStoreQty" Value='<%#Eval("InStoreQty") %>' />
                             <asp:HiddenField runat="server" ID="hdnQty" Value='<%#Eval("Qty") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Material Name">
                    <ItemTemplate>
                     <asp:Label ID="lblMatName" runat="server" Text='<%# Eval("MatName")+ "(" + Eval("UnitName") +")"%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Required Qty">
                    <ItemTemplate>
                     <asp:Label ID="lblRequiredQty" runat="server" Text='<%#Eval("Qty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Rate">
                    <ItemTemplate>
                   <asp:Label ID="lblRate" runat="server" Text='<%#Eval("AkalWorkshopRate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="InStoreQty">
                    <ItemTemplate>
                      <asp:Label runat="server" Width="100px" ID="lblInStoreQty" Text='<%#Eval("InStoreQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="AlreadyDispatchQty">
                    <ItemTemplate>
                      <asp:Label runat="server" Width="100px" ID="lblAlreadyDispatchQty" Text='<%#Eval("DispatchQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="DispatchQty">
                    <ItemTemplate>
                        <asp:TextBox runat="server" Width="100px" ID="txtDispatchQty"></asp:TextBox>
                      </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dispatch Material">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnDispatchWorkshop" CssClass="btn btn-primary" data-rel="tooltip" data-original-title="Click To Dispatch Material" Text="Dispatch Material" CommandArgument='<%#Eval("Sno") %>' OnClick="btnDispatchWorkshop_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>
