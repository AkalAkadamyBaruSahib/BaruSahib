<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_ViewEstMaterial.aspx.cs" Inherits="Workshop_ViewEstMaterial" %>

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
    </script>
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>   
         <div class="box span10"> 
         <div class="box-header well" data-original-title> 
         <h2><i class="icon-user"></i> Material Details for <asp:Label ID="lblZoneName" runat="server"></asp:Label> | <asp:Label ID="lblAcademy" runat="server"></asp:Label> | <asp:Label ID="lblEstId" runat="server"></asp:Label></h2> 
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
                    <asp:BoundField DataField="EstId" HeaderText="EstId" ItemStyle-CssClass="hideGridColumn" />
                    <asp:BoundField DataField="MatName" HeaderText="MatName" />
                    <asp:BoundField DataField="UnitName" HeaderText="UnitName" />
                    <asp:BoundField DataField="Qty" HeaderText="Qty" />

                
                    <asp:TemplateField HeaderText="Tantitive Date">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtTatDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                            <asp:Label runat="server" ID="lblTatDate"  Visible="false" Text="&lt;%# Bind(&quot;TantiveDate&quot;) %&gt;" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="DispatchDate" HeaderText="Dispatch Date"  />--%>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtRemark"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dispatch Date">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="txtDispatchDate" Text="&lt;%# Bind(&quot;DispatchDate&quot;) %&gt;" Visible="false" Style="display: none;"></asp:Label>
                            <asp:Button runat="server" ID="btnDispatch" CssClass="btn btn-warning" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" data-rel="tooltip" data-original-title="Click To Dispatch Material" Text="Dispatch Material" CommandName="DispatchDate" CommandArgument='<%#Eval("Sno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnOk" Text="Submit Tantitive Date" CssClass="btn btn-primary" data-rel="tooltip" OnClientClick="ClientSideClick2(this)" UseSubmitBehavior="False" data-original-title="Click To Submit Tantive Date and Remarks" runat="server" CommandName="Tatitive" CommandArgument='<%#Eval("Sno") %>'/>
                            
                            <%--CommandArgument='<%# Bind("EstId") %>'--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

         </div> 
           
         </div>  
</asp:Content>

