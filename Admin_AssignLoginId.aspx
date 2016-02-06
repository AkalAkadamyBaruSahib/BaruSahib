<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_AssignLoginId.aspx.cs" Inherits="Admin_AssignLoginId" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content" class="span10">
         <div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> </h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
                           
						  <fieldset>
                              <div class="box-content">
                              <table width="100%">
                                 
                                   <tr>
                                       <td width="50%"> 
                                           <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
								                 Select Employee <br />
                                                    <asp:DropDownList ID="txtUserType" runat="server" multiple data-rel="chosen"></asp:DropDownList><br />
                                                
							                    </div>
                                           </div>
                                      </td>
                                      <td width="50%"> 
                                          
                                      </td>
                                  </tr>
                                   <tr>
                                       <td width="50%" colspan="2"> 
                                           <div class="control-group">
							                    <label class="control-label" for="typeahead">Login Details</label>
							                    <div class="controls">
								                
							                    </div>
                                           </div>
                                      </td>
                                     
                                  </tr>
                                  <tr>
                                       <td width="50%"> 
                                           <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
								                 Login Id: <br />
                                                    <asp:TextBox ID="txtLoginId" runat="server" CssClass="span6 typeahead"></asp:TextBox><br />
                                                 Password: <br />
                                                    <asp:TextBox ID="txtUserPwd" runat="server" CssClass="span6 typeahead"></asp:TextBox>
							                    </div>
                                           </div>
                                      </td>
                                      <td width="50%"> 
                                          
                                      </td>
                                  </tr>
                              </table>
                             

							</div>
							
							
							<div class="form-actions">
							  
                                <asp:Button id="btnSave" Text="Save" CssClass="btn btn-primary" runat="server" OnClick="btnSave_Click" />
                                <asp:Button id="btnEdit" Text="Edit" CssClass="btn btn-primary" runat="server" />
                                 <asp:Button id="Button1" Text="Cancel" CssClass="btn" runat="server" />
							 
							</div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
        <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Designation Details</h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<div id="divDesigDetails" runat="server"></div>            
					</div>
				</div><!--/span-->
			
			</div>
        </div>
</asp:Content>

