<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_MaterialDetails.aspx.cs" Inherits="Admin_MaterialDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content" class="span10">
         <div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> Material's Bill Details</h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					
						<form class="form-horizontal">
                           
						  <fieldset>
                              <div class="box-content">
                              <table width="100%">
                                  <tr>
                                      <td width="50%" colspan="2"> 
                                            <div class="control-group">
							                    <h2><asp:Label ID="lblMaterial" runat="server" ></asp:Label></h2>
                                            </div>
                                      </td>
                                     
                                  </tr>
                                  <tr>
                                       <td width="50%"> 
                                            <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
                                                    ESITMATE NO.
								                <asp:Label ID="lblEstimateNo" runat="server"></asp:Label>
                                                    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
							                    </div>
                                            </div>
                                      </td>
                                       <td width="50%"> 
                                           <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
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
								                 Total Amount: <asp:Label runat="server" ID="lblTtlAmt" ></asp:Label>
                                                   
                                                    
                                                 
							                    </div>
                                                   
                                           </div>
                                      </td>
                                      <td width="50%"> 
                                          
                                      </td>

                                  </tr>
                                  
                                  <tr>
                                       <td width="50%" colspan="2"> 
                                            <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
                                                   Total Qty: <asp:Label runat="server" ID="lblTtlQty" ></asp:Label>
                                                
							                    </div>
                                            </div>
                                      </td>
                                       <td width="50%"> 
                                          
                                      </td>
                                  </tr>
                                   <tr>
                                       <td width="50%"> 
                                            <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
								                  Rate: <asp:Label runat="server" ID="lblEstRate" ></asp:Label>
							                    </div>
                                           </div>
                                      </td>
                                       <td width="50%"> 
                                           
                                      </td>
                                  </tr>
                                    <tr>
                                      <td colspan="2" width="50%" align="left">
                                           <div runat="server" id="divEstimateMaterailView"></div>
                                      </td>
                                  </tr>
                                   <tr>
                                       <td width="50%"> 
                                            <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
                                                    Balance Amount: <asp:Label ID="lblBalAmt" runat="server" ForeColor="Red" Text="00.00"></asp:Label>
							                    </div>
                                            </div>
                                      </td>
                                       <td width="50%"> 
                                           <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
								               Balance Qty: <asp:Label ID="lblBalQty" runat="server" ForeColor="Red" ></asp:Label>
							                    </div>
                                           </div>
                                      </td>
                                  </tr>
                                 
                                <tr>
                                      <th colspan="2" width="50%" align="left">
                                         <h4></h4> 
                                      </th>
                                  </tr>
                                
                                  
                              </table>
                             

							</div>
							
							
							
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->
                   
        
			</div>
</asp:Content>

