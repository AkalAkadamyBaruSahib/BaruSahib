<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Academy.aspx.cs" Inherits="Admin_Academy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
         <div class="row-fluid sortable" runat="server" id="divNewAcademy">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> <asp:Label ID="lblHeading" runat="server"></asp:Label></h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content" >
						<div class="form-horizontal" runat="server" id="frmNewAca">
						  <fieldset >
							<div class="control-group">
							  <label class="control-label" for="typeahead">Select Zone</label>
							  <div class="controls">
                                  <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                 
								<asp:DropDownList ID="ddlZone" runat="server" ></asp:DropDownList>
                                
							  </div>
							</div>
							<div class="control-group">
							  <label class="control-label" for="typeahead">Academy's Name</label>
							  <div class="controls">
								<asp:TextBox ID="txtZone" runat="server" CssClass="span6 typeahead"></asp:TextBox>
							  </div>
							</div>
                              <div class="control-group">
							  <label class="control-label" for="typeahead">Academy's Current Status</label>
							  <div class="controls">
								<asp:DropDownList ID="ddlStatusType" runat="server"></asp:DropDownList>
							  </div>
							</div>
                              <%--<div class="control-group">
							                  <label class="control-label" for="typeahead"> Upload Academy Image </label>
							                  <div class="controls">
                                                 
								               <asp:FileUpload ID="fuImgeFile" runat="server"  />
                                                  <asp:Image id="imgAcademy" runat="server" Width="75px" Height="75px" Visible="false"/>
                                                  <asp:Label runat="server" ID="lblImgFileName" Visible="false"></asp:Label>
							                  </div>
							              </div>--%>
							
							<div class="form-actions">
							 <asp:Button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                                <asp:Button id="btnEdit" runat="server" Text="Chane Academy Status" CssClass="btn btn-primary" Visible="false" OnClick="btnEdit_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                                <asp:Button id="btnExecl" runat="server" Text="Excel Download" CssClass="btn btn-primary" OnClick="btnExecl_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                                <asp:Button id="btnCl" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCl_Click"/>
							</div>
						  </fieldset>
						</div>  
                        <div class="form-horizontal" runat="server" id="frmAssignment">
						  <fieldset>
                              <div class="control-group">
							  <label class="control-label" for="typeahead"> Academy</label>
							  <div class="controls">
                                  <asp:Label ID="lblAEAcademy" runat="server" ></asp:Label>
							  </div>
                                  </div>
							<div class="control-group">
							  <label class="control-label" for="typeahead"> User Type</label>
							  <div class="controls">
                               
								<asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"></asp:DropDownList>
							  </div>
							</div>
							<div class="control-group">
							  <label class="control-label" for="typeahead"> Employee</label>
							  <div class="controls">
                                  
								 <asp:DropDownList ID="ddlEmployee" runat="server"></asp:DropDownList> 
							  </div>
							</div>
                             
                              
							
							<div class="form-actions">
							 <asp:Button id="btnAssign" runat="server" Text="Assign To" CssClass="btn btn-primary" />
                                
                                <asp:Button id="Button4" runat="server" Text="Cancel" CssClass="btn" />
							</div>
                          
						  </fieldset>
						</div>
                            

					</div>
				</div><!--/span-->

			</div>
      <%--  <div class="row-fluid sortable" runat="server" id="divAssignLocation">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> Step 2 - Employee Assign</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
						  <fieldset>
                              <div class="control-group">
							  <label class="control-label" for="typeahead"> Academy</label>
							  <div class="controls">
                                  <asp:Label ID="lblAEAcademy" runat="server" ></asp:Label>
							  </div>
							<div class="control-group">
							  <label class="control-label" for="typeahead"> User Type</label>
							  <div class="controls">
                               
								<asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"></asp:DropDownList>
							  </div>
							</div>
							<div class="control-group">
							  <label class="control-label" for="typeahead"> Employee</label>
							  <div class="controls">
                                  
								 <asp:DropDownList ID="ddlEmployee" runat="server"></asp:DropDownList> 
							  </div>
							</div>
                             
                              
							
							<div class="form-actions">
							 <asp:Button id="btnAssign" runat="server" Text="Assign To" CssClass="btn btn-primary" />
                                
                                <asp:Button id="Button4" runat="server" Text="Cancel" CssClass="btn" />
							</div>
                            <div class="form-actions">

                            </div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>--%>
        
        <div class="row-fluid sortable" runat="server" id="divShiftAcademy">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> Academy shift to another Zone</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
						  <fieldset>
                              <div class="control-group">
							  <label class="control-label" for="typeahead">Academy Name</label>
							  <div class="controls">
                                  <asp:Label ID="lblAcaName" runat="server" ></asp:Label>
								
							  </div>
							</div>
							<div class="control-group">
							  <label class="control-label" for="typeahead">Present Zone</label>
							  <div class="controls">
                                  <asp:Label ID="lblPresentZone" runat="server" ></asp:Label>
								
							  </div>
							</div>
							<div class="control-group">
							  <label class="control-label" for="typeahead">Shift to Another Zone</label>
							  <div class="controls">
								<asp:DropDownList ID="ddlShiftedZone" runat="server"></asp:DropDownList>
							  </div>
							</div>
                            
							
							<div class="form-actions">
							 <asp:Button id="btnShift" runat="server" Text="Shift Academy" CssClass="btn btn-primary" OnClick="btnShift_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                               
                                <asp:Button id="btnShitCnl" runat="server" Text="Cancel" CssClass="btn"/>
							</div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
        <div class="row-fluid sortable" runat="server" id="divShitAUdit">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> Academy shift to Audit User</h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
						  <fieldset>
                              <div class="control-group">
							  <label class="control-label" for="typeahead">Academy Name</label>
							  <div class="controls">
                                  <asp:Label ID="lblAuditAcaName" runat="server" ></asp:Label>
								
							  </div>
							</div>
							<div class="control-group">
							  <label class="control-label" for="typeahead">Present Audit User(Mobile No.)</label>
							  <div class="controls">
                                  <asp:Label ID="lblAuditUser" runat="server" ></asp:Label>(<asp:Label ID="lblMobNo" runat="server" ></asp:Label>)
								
							  </div>
							</div>
							<div class="control-group">
							  <label class="control-label" for="typeahead">Audit User Shift to Another Academy</label>
							  <div class="controls">
								<asp:DropDownList ID="ddlAuditAca" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAuditAca_SelectedIndexChanged"></asp:DropDownList>
							  </div>
							</div>
                            
							
							<div class="form-actions">
							 <asp:Button id="btnShiftAudit" runat="server" Text="Shift Academy to Audit User" CssClass="btn btn-primary" OnClick="btnShift_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                               
                                <asp:Button id="Button2" runat="server" Text="Cancel" CssClass="btn"/>
							</div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
        <div class="row-fluid sortable" runat="server" id="divAcaDerailsAction">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Academy Details</h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<div id="divAcademyDetails" runat="server"></div>            
					</div>
				</div><!--/span-->
			
			</div>
        </div>
</asp:Content>

