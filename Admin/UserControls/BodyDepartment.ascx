<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyDepartment.ascx.cs" Inherits="Admin_UserControls_BodyDepartment" %>
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
						<h2><i class="icon-edit"></i> New Department</h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
						  <fieldset>
							
							<div class="control-group">
							  <label class="control-label" for="typeahead">Department's Name</label>
							  <div class="controls">
								<%--<input type="text" class="span6 typeahead" id="typeahead"  data-provide="typeahead" data-items="4" data-source='["Alabama","Alaska","Arizona","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky","Louisiana","Maine","Maryland","Massachusetts","Michigan","Minnesota","Mississippi","Missouri","Montana","Nebraska","Nevada","New Hampshire","New Jersey","New Mexico","New York","North Dakota","North Carolina","Ohio","Oklahoma","Oregon","Pennsylvania","Rhode Island","South Carolina","South Dakota","Tennessee","Texas","Utah","Vermont","Virginia","Washington","West Virginia","Wisconsin","Wyoming"]'>--%>
								<asp:Label runat="server" ID="lblUser" Visible="false"></asp:Label>
                                  <asp:TextBox ID="txtDept" runat="server" CssClass="span6 typeahead"></asp:TextBox>
							  </div>
							</div>
							
							<div class="form-actions">
							   <asp:Button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                                <asp:Button id="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false" OnClick="btnEdit_Click1" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"  />
                                <asp:Button id="btnCl" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCl_Click" />
							</div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
        <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Department Details</h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<div runat="server" id="divDeptDetails"></div>
					</div>
				</div><!--/span-->
			
			</div>
        </div>
