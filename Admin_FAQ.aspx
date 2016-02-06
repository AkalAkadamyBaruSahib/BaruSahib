<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_FAQ.aspx.cs" Inherits="Admin_FAQ" %>

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
						<h2><i class="icon-edit"></i> Fequently Asked Questions</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
						  <fieldset>
							
							<div class="control-group">
							  <label class="control-label" for="typeahead">Question</label>
							  <div class="controls">
                                  <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
								<asp:TextBox ID="txtQues" runat="server" CssClass="span6 typeahead"></asp:TextBox>
							  </div>
							</div>
                              <div class="control-group">
							  <label class="control-label" for="typeahead">Answer</label>
							  <div class="controls">
								<asp:TextBox ID="txtAns" runat="server" CssClass="span6 typeahead" TextMode="MultiLine"></asp:TextBox>
							  </div>
							</div>
							
							<div class="form-actions">
							 
                                <asp:Button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"  OnClick="btnSave_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                                <asp:Button id="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false" OnClick="btnEdit_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"/>
                                <asp:Button id="btnCl" runat="server" Text="Clear" CssClass="btn" OnClick="btnCl_Click" />
							</div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
        <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> FAQs Details</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        <div id="divZoneDetails" runat="server"></div>
						                
					</div>
				</div><!--/span-->
			
			</div>
        </div>
</asp:Content>
