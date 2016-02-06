<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_BillStatus.aspx.cs" Inherits="Emp_BillStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script type="text/javascript">
      function confirmation() {
          if (confirm('ARE YOU SURE FOR THIS ACTION?')) {
              return true;
          } else {
              return false;
          }
      }
    </script> 
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
				<div class="box span12">
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Select Search Type</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        

						<div id="divDesigDetails" runat="server">
                        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate>  --%>       
                         <table border="0" WIDTH="100%">  <%--class="table table-striped table-bordered bootstrap-datatable datatable"--%>
                         <tbody> 
                        <tr>
                            <td colspan="23">
                          
                                  &nbsp;&nbsp;&nbsp;
                                <a href="Emp_MonthYearWise.aspx"><span class="label label-info" style="font-size:medium;">Month & Year Wise</span></a>
                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                <a href="Emp_AcademyWise.aspx"><span class="label label-info" style="font-size:medium;">Academy Wise</span></a>
                                 &nbsp;&nbsp;&nbsp;
                                <a href="Emp_PaidBills.aspx"><span class="label label-info" style="font-size:medium;">Paid Bills</span></a>
                                 &nbsp;&nbsp;&nbsp;
                                <a href="Emp_BillWorkAllotWise.aspx"><span class="label label-info" style="font-size:medium;">Work Wise Bills</span></a>
                                 &nbsp;&nbsp;&nbsp;
                                <asp:Button id="btnExeclStatus" runat="server" Text="Bill Status Excel Download" CssClass="btn btn-primary"  onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" OnClick="btnExeclStatus_Click"/>
                                 &nbsp;&nbsp;&nbsp;
                                <asp:Button id="btnExecl" runat="server" Text="Bill Detail Excel Download" CssClass="btn btn-primary"  onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" OnClick="btnExecl_Click"/>
                            </td>
                        </tr>
             
                           
                         </tbody> 
                         </table> 
                              <%--</ContentTemplate>

                             </asp:UpdatePanel>--%>
						</div>            
					</div>
				</div><!--/span-->
			
			</div>
         <asp:Panel ID="pnlBillMonthDetails" runat="server" Visible="false">
             <div id="div2" runat="server"></div>
         </asp:Panel>
         </div>
    <div id="content" class="span10">


        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>

        <div id="divAcademyDetails" runat="server"></div>
    </div>


    
    <div class="modal hide fade" id="myModal">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3><b>
                <asp:Label ID="Label1" runat="server"></asp:Label></b> Reciving Details</h3>
        </div>
        <div class="modal-body">
            <div class="controls">
                <table align="Center">
                    <tr>
                        <td>Agency Name</td>
                        <td><asp:Label ID="lblAgency" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Recipt/ Voucher No.</td>
                        <td><asp:TextBox runat="server" ID="txtRecipTNo"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Date Of Recieving</td>
                        <td><asp:TextBox runat="server" ID="txtDateOfRec" Width="150Px" CssClass="input-xlarge datepicker"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Remark</td>
                        <td><asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="modal-footer">
            <a href="#" class="btn" data-dismiss="modal">Close</a>
            <asp:Button ID="Button1" Text="Submit Details" CssClass="btn btn-primary" runat="server" OnClick="Button1_Click" OnClientClick="return confirmation();"/>
            <asp:Button ID="Button2" Text="Reject Bill" CssClass="btn btn-danger" runat="server" OnClientClick="return confirmation();" OnClick="Button2_Click"/>
            <%--<asp:LinkButton runat="server" class='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>
            <a class='btn btn-setting btn-round' href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>--%>
        </div>
    </div>
</asp:Content>

