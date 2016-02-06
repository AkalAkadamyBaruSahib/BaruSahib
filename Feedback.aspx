<%@ Page Title="" Language="C#" MasterPageFile="~/Common.master" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Feedback" %>

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
    </script>
    <div id="content" class="span10">
         <div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> User Feedback</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
						  <fieldset>
							
							<div class="control-group">
							  <label class="control-label" for="typeahead">Feedback Type</label>
							  <div class="controls">
                              <asp:DropDownList runat="server" ID="ddlFbType"></asp:DropDownList>
							  </div>
							</div>
                              <div class="control-group">
							  <label class="control-label" for="typeahead">Are you employee of Akal Academy</label>
							  <div class="controls">
                              <asp:DropDownList runat="server" ID="ddlIsEmp" AutoPostBack="true" OnSelectedIndexChanged="ddlIsEmp_SelectedIndexChanged">
                                  <asp:ListItem Value="0">Select</asp:ListItem>
                                  <asp:ListItem Value="1">Yes</asp:ListItem>
                                  <asp:ListItem Value="2">No</asp:ListItem>
                              </asp:DropDownList>
							  </div>
                                  <div runat="server" id="divIsEmp">
                                        <div class="control-group">
							              <label class="control-label" for="typeahead">Department</label>
							              <div class="controls">
                                          <asp:DropDownList runat="server" ID="ddlDep"></asp:DropDownList>
							              </div>
							            </div>
                                      <div class="control-group">
							              <label class="control-label" for="typeahead">User/ Login Id</label>
							              <div class="controls">
                                          <asp:TextBox runat="server" ID="txtUserId"></asp:TextBox>
							              </div>
							            </div>
                                  </div>
							 <div runat="server" id="divIsNotEmp">
                               <div class="control-group">
							              <label class="control-label" for="typeahead">Name</label>
							              <div class="controls">
                                          <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
							              </div>
							            </div>
                             
                                    <div class="control-group">
							              <label class="control-label" for="typeahead">Email-Id</label>
							              <div class="controls">
                                          <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
							              </div>
							            </div>
                                <div class="control-group">
							              <label class="control-label" for="typeahead">Mobile</label>
							              <div class="controls">
                                          <asp:TextBox runat="server" ID="txtMob"></asp:TextBox>
							              </div>
							            </div>
                                 </div>
                             </div>
							  <label class="control-label" for="typeahead">Message</label>
							  <div class="controls">
								<asp:TextBox ID="txtAns" runat="server" CssClass="span6 typeahead" TextMode="MultiLine"></asp:TextBox>
							  </div>
							</div>
							
							<div class="form-actions">
							 
                                <asp:Button id="btnSave" runat="server" Text="Send Feedback" CssClass="btn btn-primary"  onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" OnClick="btnSave_Click"/>
                                <asp:Button id="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                                <asp:Button id="btnCl" runat="server" Text="Clear" CssClass="btn"  />
							</div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
     
        </div>
  
</asp:Content>

