<%@ Page Title="" Language="C#" MasterPageFile="~/ArchMaster.master" AutoEventWireup="true" CodeFile="Arch_UploadDrawing.aspx.cs" Inherits="Arch_UploadDrawing" %>

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
         <div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> New Drawing Upload</h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
						  <fieldset>
                              <table>
                       
                                  <tr> 
                                      
                                      <td>
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Zone</label>
							                  <div class="controls">
                                                  <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
								                <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
							                  </div>
							              </div>
                                      </td>
                                        
                                      <td>
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Academy</label>
							                  <div class="controls">
								                <asp:DropDownList ID="ddlAcademy" runat="server"></asp:DropDownList>
							                  </div>
							              </div>
                                      </td>
                                  </tr>
                          
                                  <tr>
                                      <td>
                                           <div class="control-group">
							                  <label class="control-label" for="typeahead"> Drawing Type</label>
							                  <div class="controls">
								                <asp:DropDownList ID="ddlDwgType" OnSelectedIndexChanged="ddlDwgType_SelectedIndexChanged" AutoPostBack="true"  runat="server"></asp:DropDownList>
							                  </div>
							                </div>
                                      </td>
                                       <td>
                                           <div class="control-group">
							                  <label class="control-label" for="typeahead">Sub Drawing Type</label>
							                  <div class="controls">
								                <asp:DropDownList ID="ddlSubDrawingType" runat="server"></asp:DropDownList>
							                  </div>
							                </div>
                                      </td>
                                      <td>
                                          
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Drawing No.</label>
							                  <div class="controls">
                                                 
								               <asp:TextBox ID="txtDrwNo" runat="server" width="100px"></asp:TextBox>
							                  </div>
							              </div>
                                      </td>
                                      <td>
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Revision No.</label>
							                  <div class="controls">
								                <asp:TextBox ID="txtRevisionNo" runat="server" width="100px"></asp:TextBox>
							                  </div>
							              </div>
                                      </td>
                                  </tr>
                                   <tr>
                                      <td colspan="2" >
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Drawing Name</label>
							                  <div class="controls">
                                                  
								               <asp:TextBox ID="txtDrwName" runat="server" width="550px"></asp:TextBox>
							                  </div>
							              </div>
                                      </td>
                                     
                                  </tr>
                                  <tr>
                                      <td>
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Upload Auto Cad File(for record) Ex:.dwg</label>
							                  <div class="controls">
                                                 
								               <asp:FileUpload ID="fuDwgFile" runat="server"  />
							                  </div>
							              </div>
                                      </td>
                                      <td>
                                        
                                      </td>
                                  </tr>
                                  <tr>
                                      
                                      <td>
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Upload PDF File. Ex:.pdf</label>
							                  <div class="controls">
								                <asp:FileUpload ID="fuPdf" runat="server"  />
							                  </div>
							              </div>
                                      </td>
                                      <td>
                                          
                                      </td>
                                  </tr>
                              </table>
							
							
                             
							
							<div class="form-actions">
							 <asp:Button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" onclientclick="ClientSideClick(this)" UseSubmitBehavior="False"/>
                                <asp:Button id="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false" />
                                <asp:Button id="btnCl" runat="server" Text="Cancel" CssClass="btn" />
							</div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
         </div>
</asp:Content>

