<%@ Page Title="" Language="C#" MasterPageFile="~/Common.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

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
						<h2><i class="icon-edit"></i> Change Password</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
						  <fieldset>
							
							<div class="control-group">
							  <label class="control-label" for="typeahead">Login Id</label>
							  <div class="controls">
								<asp:TextBox ID="txtLoginId" runat="server" CssClass="span6 typeahead" Width="250px"></asp:TextBox>
							  </div>
							</div>
                              <div class="control-group">
							  <label class="control-label" for="typeahead">Old Password</label>
							  <div class="controls">
								<asp:TextBox ID="txtOldPwd" runat="server" CssClass="span6 typeahead" Width="150px" TextMode="Password"></asp:TextBox>
							  </div>
							</div>
                             <div class="control-group">
							  <label class="control-label" for="typeahead">New Password</label>
							  <div class="controls">
								<asp:TextBox ID="txtNewPwd" runat="server" CssClass="span6 typeahead" Width="150px" TextMode="Password"></asp:TextBox>
							  </div>
							</div>
							     <div class="control-group">
							  <label class="control-label" for="typeahead">Re-Enter Password</label>
							  <div class="controls">
								<asp:TextBox ID="txtRePwd" runat="server" CssClass="span6 typeahead" Width="150px" TextMode="Password"></asp:TextBox>
                                  <asp:CompareValidator runat="server" ID="aspCompValidate" ControlToValidate="txtNewPwd" ControlToCompare="txtRePwd" ErrorMessage="New Password and Re Password must be same!" ForeColor="Red"></asp:CompareValidator>
							  </div>
							</div>
							<div class="form-actions">
							 
                                <asp:Button id="btnSave" runat="server" Text="Change Password" CssClass="btn btn-primary" OnClick="btnSave_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                               
                                <asp:Button id="btnCl" runat="server" Text="Cancel" CssClass="btn"/>
							</div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
       
        </div>
</asp:Content>

