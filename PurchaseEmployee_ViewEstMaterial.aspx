<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="PurchaseEmployee_ViewEstMaterial.aspx.cs" Inherits="PurchaseEmployee_ViewEstMaterial" %>

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
                ret= false;
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
                OnRowCommand="gvMaterailDetailForPurchase_RowCommand" OnRowDataBound="gvMaterailDetailForPurchase_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="SNO">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %> 
                            <asp:HiddenField runat="server" ID="txtMatID" Value='<%#Eval("MatID") %>' />
                            <asp:HiddenField runat="server" ID="txtUnitID" Value='<%#Eval("UnitID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MatName" HeaderText="MatName" />
                    <asp:BoundField DataField="UnitName" HeaderText="UnitName" />
                    <asp:BoundField DataField="Qty" HeaderText="Qty" />
                    
                    <asp:TemplateField HeaderText="Rate">
                        <ItemTemplate>
                            <asp:TextBox runat="server" Width="100px" ID="txtRate"></asp:TextBox>
                            
                             <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$" ForeColor="Red" ErrorMessage="*"
                            ControlToValidate="txtRate" />
                            <asp:HiddenField runat="server" ID="txtEstID" Value='<%#Eval("EstID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtRemark"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase Date">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="txtDispatchDate" Text='<%# Eval("DispatchDate") %>' Visible="false" Style="display: none;"></asp:Label>
                            <asp:Button runat="server" ID="btnDispatch" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" data-rel="tooltip" data-original-title="Click To Dispatch Material" Text="Purchase Material" CommandName="DispatchDate" CommandArgument='<%#Eval("Sno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>


