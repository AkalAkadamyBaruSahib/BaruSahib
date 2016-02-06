<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_ReporteDetails.aspx.cs" Inherits="Transport_ReporteDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                myButton.className = "btn btn-success";
                myButton.value = "Please Wait...";
            }
            return true;
        }
    </script>
     <div class="box span10">
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </i> Year and Month Wise Purchase Material Reports</h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>				
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
					</div>
                        </div>
					<div class="box-content">
    	<div id="divDesigDetails" runat="server">
                     
                         <table border="0" WIDTH="100%">  <%--class="table table-striped table-bordered bootstrap-datatable datatable"--%>
                         <tbody> 
                        <tr>
                            <td colspan="23">
                                Select First Date:                
                            <asp:TextBox runat="server" ID="txtfirstDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                            </td>
                            <td>
                                Select Last Date:
                                   <asp:TextBox runat="server" ID="txtlastDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox> 
                            </td>
                             </tr> 
                              </tbody>
                        </table>   
                             </div>
                        <div>
                            <asp:Button ID="btnDownload" runat="server" Text="Latest Uploaded Data " CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="355px" />
                        </div>
                      </div>
      </div>
</asp:Content>

