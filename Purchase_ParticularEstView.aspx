<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_ParticularEstView.aspx.cs" Inherits="Purchase_ParticularEstView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content" class="span10">
         <div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> Estimate</h2>
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
							                    <h2><asp:Label ID="lblWorkAllotName" runat="server" ></asp:Label></h2>
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
                                  </tr> <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                  <tr>
                                       
                                     <td width="50%"> 
                                           <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
                                               
							                    <div class="controls">
								                 ZONE: <asp:Label runat="server" ID="lblZone" ></asp:Label>( <asp:Label runat="server" ID="lblZoneCode" ></asp:Label>)
                                                   
                                                    
                                                 
							                    </div>
                                                   
                                           </div>
                                      </td>
                                      <td width="50%"> 
                                           <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
								                 ACADEMY: <asp:Label runat="server" ID="lblAca" ></asp:Label>( <asp:Label runat="server" ID="lblAcaCode" ></asp:Label>)
                                                 
                                                  
							                    </div>
                                               
                                           </div>
                                      </td>

                                  </tr>
                                   </ContentTemplate>
                                                    </asp:UpdatePanel>
                                  <tr>
                                       <td width="50%" colspan="2"> 
                                            <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
                                                    SUB ESTIMATE: <asp:Label runat="server" ID="lblSubEstimate" ></asp:Label>
                                                
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
								                 TYPE OF WORK: <asp:Label runat="server" ID="lblTypeOfWork" ></asp:Label>
							                    </div>
                                           </div>
                                      </td>
                                       <td width="50%"> 
                                           <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
								                 SANCTION DATE: <asp:Label runat="server" ID="lblSanctionDate" ></asp:Label><br />
							                    </div>
                                           </div>
                                      </td>
                                  </tr>
                                   <tr>
                                       <td width="50%"> 
                                            <div class="control-group">
							                    <label class="control-label" for="typeahead"></label>
							                    <div class="controls">
                                                    ESTIMATE COST: <asp:Label ID="lblEstimateCost" runat="server" ForeColor="Red" Text="00.00"></asp:Label>
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
                                      <th colspan="2" width="50%" align="left">
                                         <h4></h4> 
                                      </th>
                                  </tr>
                                  <tr>
                                      <td colspan="2" width="50%" align="left">
                                           <div runat="server" id="divEstimateMaterailView"></div>
                                      </td>
                                  </tr>
                                  
                              </table>
                             

							</div>
							
							
							
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->
                   
        
			</div>
        
      
                             
        
      
</asp:Content>