<%@ Page Title="" Language="C#" MasterPageFile="~/AccountMaster.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="Account_BillDetails.aspx.cs" Inherits="Account_BillDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>   
         <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Bill Detail</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content" runat="server" id="divMain">
                    <form class="form-horizontal" >
                        <fieldset>
                            <table>
                                    <tr>
                                            <td >
                                                <div class="control-group">
                                                    
                                                    <b>Zone</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblZone" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td >
                                                <div class="control-group">
                                                    
                                                    <b>Academy</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblAca" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="2">
                                                <div class="control-group">
                                                    
                                                    <b>Bill No.</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblBillNo" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                           
                                        </tr>
                               
                                       <%-- <tr>
                                            <td colspan="2">
                                                <div class="control-group">
                                                    <b>Bill Type</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblBillType" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                           
                                        </tr>--%>
                                   <tr>
                                            <td >
                                                <div class="control-group">
                                                    <b>Chargeable To</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblChargeableTo" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td >
                                                <div class="control-group">
                                                   <b>Decription of Bill</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblBillDesc" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                               <tr>
                                            <td >
                                                <div class="control-group">
                                                    <b>Agency Name</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblAgencyName" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td >
                                                <div class="control-group">
                                                    <b>Date Of Submission</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblBillDate" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="2">
                                                <div class="control-group">
                                                    <b>Gate Entry No.</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblGateEntry" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        
                                        </tr>
                                       
                                <tr>
                                            <td colspan="2" width="100%">
                                                <div id="divBillMaterialDetails" runat="server" >
                                                
                                                </div>
                                            </td>
                                        
                                        </tr>
                                      
                                  <tr>
                                            <td colspan="2">
                                               <div class="control-group">
                                                    <b>1st Verfication Details</b>
                                                    <div class="controls">
                                                        Approved By: <asp:Label ID="lblHqUser" runat="server" ForeColor="Red"></asp:Label><br />
                                                        Approved On: <asp:Label ID="lblHqAppDate" runat="server" ForeColor="Green"></asp:Label><br />
                                                        Status: <asp:Label ID="lblHqStatus" runat="server" ForeColor="Maroon"></asp:Label><br />
                                                        Remarks: <asp:Label ID="lblHqRemark" runat="server" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        
                                        </tr>
                                <tr>
                                            <td colspan="2">
                                               <div class="control-group">
                                                    <b>2nd Verification Details</b>
                                                    <div class="controls">
                                                        Approved By: <asp:Label ID="lbl2ndUser" runat="server" ForeColor="Red"></asp:Label><br />
                                                        Approved On: <asp:Label ID="lbl2ndAppOn" runat="server" ForeColor="Green"></asp:Label><br />
                                                        Status: <asp:Label ID="lblAuditStatus" runat="server" ForeColor="Maroon"></asp:Label><br />
                                                        Remarks: <asp:Label ID="lbl2ndRemark" runat="server" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        
                                        </tr>
                                 <tr>
                                            <td colspan="2">
                                               <div class="control-group">
                                                    <b>Payment Details</b>
                                                    <div class="controls">
                                                        Approved By: <asp:Label ID="lbl3rdUser" runat="server" ForeColor="Red"></asp:Label><br />
                                                        Approved On: <asp:Label ID="lbl3rdAppOn" runat="server" ForeColor="Green"></asp:Label><br />
                                                        Status: <asp:Label ID="lblPayStatus" runat="server" ForeColor="Maroon"></asp:Label><br />
                                                        Payment Mod: <asp:Label ID="lblPayMode" runat="server" ForeColor="Green"></asp:Label><br />
                                                        Payment Details: <asp:Label ID="lblPayDetails" runat="server" ForeColor="#006666"></asp:Label><br />
                                                        Remarks: <asp:Label ID="lblAccRemark" runat="server" ForeColor="Blue"></asp:Label>
                                                        <%--Payment Mode: <asp:Label ID="lbl3rdPayMode" runat="server"></asp:Label><br />
                                                        Payment Detail: <asp:Label ID="lbl3rdPayDetails" runat="server"></asp:Label>--%>
                                                    </div>
                                                </div>
                                            </td>
                                        
                                        </tr>
                                 <tr>
                                            <td colspan="2">
                                               <div class="control-group">
                                                    <b>Reciving Details</b>
                                                    <div class="controls">
                                                        Approved By: <asp:Label ID="lblRecUser" runat="server" ForeColor="Red"></asp:Label><br />
                                                        Approved On: <asp:Label ID="lblRecAppOn" runat="server" ForeColor="Green"></asp:Label><br />
                                                        Vochor No: <asp:Label ID="lblRecVocNo" runat="server" ForeColor="Blue"></asp:Label><br />
                                                        Status: <asp:Label ID="lblRecStatus" runat="server" ForeColor="Maroon"></asp:Label><br />
                                                        Remark: <asp:Label ID="lblRecRemark" runat="server" ForeColor="Maroon"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        
                                        </tr>
                                 <tr>
                                            <td colspan="2">
                                               <div class="control-group" runat="server" visible="false">
                                                    <b>Remark</b>
                                                    <div class="controls">
                                                        <h3><asp:Label ID="lblRemark" runat="server" ForeColor="Red"></asp:Label></h3>
                                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="755Px" Height="50Px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </td>
                                        
                                        </tr>
                                 
                            </table>
                              <div class="form-actions" runat="server" id="divFinalButtons" visible="false">
                                <asp:Button ID="btnSave" runat="server" Text="Verify Bill" CssClass="btn btn-primary" BorderColor="DarkGreen"  OnClick="btnSave_Click"/>
                                <asp:Button ID="btnEdit" runat="server" Text="Reject Bill"  CssClass="btn btn-primary" BorderColor="Red" ForeColor="Red" OnClick="btnEdit_Click"/>
                               
                            </div>



                           
                        </fieldset>
                    </form>

                </div>
            </div>
            <!--/span-->

        </div>
       
        </div>  
</asp:Content>

