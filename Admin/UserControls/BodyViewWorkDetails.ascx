<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyViewWorkDetails.ascx.cs" Inherits="Admin_UserControls_BodyViewWorkDetails" %>


<asp:HiddenField ID="hdnWorkAllotID" runat="server" />
<div id="content" class="span10">
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well">
                <h2><i class="icon-edit"></i><asp:Label ID="lblHeader" runat="server" Text="Work Details"></asp:Label></h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered bootstrap-datatable datatable">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No.">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" Text='<%# Container.DataItemIndex + 1  %>' runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnMatID" Value='<%# Eval("MatId") %>' runat="server" />
                                <asp:HiddenField ID="hdnUnitId" Value='<%# Eval("UnitId") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Name">
                            <ItemTemplate>
                                <a href="#" class="btn-setting" onclick=<%# "javascript:GetMaterialDetails('" + Eval("MatId") + "')" %>><%# Eval("MatName") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UnitName" HeaderText="Unit" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                        <asp:BoundField DataField="EstBal" HeaderText="Estimate Balance" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="modal hide fade" style="width: 900px; height: 580px;" id="myModal">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Work Allot Details</h3>
        </div>
        <div class="modal-body">
            <table id="grdMatDiscription" style="width: 750px" class="table table-striped table-bordered bootstrap-datatable datatable">
                <thead>
                    <tr>
                        <th>Bill No</th>
                        <th>Agency Name</th>
                        <th>Mat Name</th>
                        <th>Quantity</th>
                        <th>Rate</th>
                        <th>Stock Entry No.</th>
                        <th>Created On</th>
                    </tr>
                </thead>
                <tbody id="tbody"></tbody>
            </table>
        </div> 
        <div class="modal-footer">
        <a href="#" class="btn" data-dismiss="modal">Close</a>
    </div>
    </div>
   
</div>
