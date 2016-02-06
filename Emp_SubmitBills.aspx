<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_SubmitBills.aspx.cs" Inherits="Emp_SubmitBills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>
    <link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css" />
    <script language="javascript" type="text/javascript">
        function popup() {
            var dd = document.getElementById('<%= ddlEstimateNo.ClientID %>').selectedIndex;
            //alert(dd);
            var yr = document.getElementById('<%= ddlEstimateNo.ClientID %>').options[dd].value;
            //alert(yr);
            if (yr == "-1") {
                alert('Select Something');
            }
            else {
                window.open("../AkalAcademy/Example/GridViewWithCheckBox.aspx?EstId=" + yr, "List", "scrollbars=no,resizable=no,width=300,height=280");
                
            }
        }
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('a#popup').live('click', function (e) {

                var page = $(this).attr("href")

                var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px; " src="' + page + '" width="200%" height="200%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 200,
                    width: 290,
                    title: "Itmes",
                    buttons: {
                        "Close": function () { $dialog.dialog('close'); }
                    },
                    close: function () {


                        $("[id*=btnJ]").click();
                    }
                });
                $dialog.dialog('open');
                e.preventDefault();
            });
        });
    </script>

    <div id="content" class="span10">
         <div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> Submit Bill</h2>
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
                                  
                                  <asp:Button id="btnJ" runat="server" Visible="false" OnClick="btnJ_Click"/>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                  <tr>
                                      <td colspan="2">
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Academy</label>
							                  <div class="controls">
                                                  <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
								                <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged" ></asp:DropDownList>
							                  </div>
							              </div>
                                      </td>
                                    
                                  </tr>

                           
                                  <tr>
                                      <td>
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Estimate No</label>
							                  <div class="controls">
                                                  
								                <asp:DropDownList ID="ddlEstimateNo" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlEstimateNo_SelectedIndexChanged"></asp:DropDownList>
							                  </div>
							              </div>
                                      </td>
                                      <td>
                                          
                                      </td>
                                  </tr>
                                   </ContentTemplate>
                        </asp:UpdatePanel>
                                  <tr>
                                     <td colspan="2">
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Estimate Sub Head.</label>
							                  <div class="controls">
                                                 
								               <h3><asp:Label ID="lblEstSub" runat="server" ></asp:Label></h3>
							                  </div>
							              </div>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Agency Name.</label>
							                  <div class="controls">
                                                 
								               <asp:TextBox ID="txtAgencyName" runat="server" width="200px"></asp:TextBox>
							                  </div>
							              </div>
                                      </td>
                                      <td>
                                         
                                      </td>
                                  </tr>
                                   <tr>
                                     
                                      <td >
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Natur of Bill</label>
							                  <div class="controls">
								                <asp:TextBox ID="TextBox2" runat="server" width="100px"></asp:TextBox>
							                  </div>
							              </div>
                                      </td>
                                       <td >
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Gate Entry No</label>
							                  <div class="controls">
								                <asp:TextBox ID="TextBox1" runat="server" width="100px"></asp:TextBox>
							                  </div>
							              </div>
                                        
                                      </td>
                                  </tr>
                                   <tr>
                                      <td >
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Date Of Supply</label>
							                  <div class="controls">
                                                  
								               <asp:TextBox ID="txtDateOfSupply" runat="server" width="150px"></asp:TextBox>
							                  </div>
							              </div>
                                      </td>
                                       <td>
                                              <div class="control-group">
							                  <label class="control-label" for="typeahead"> Stock Entry No</label>
							                  <div class="controls">
								                <asp:TextBox ID="TextBox3" runat="server" width="100px"></asp:TextBox>
							                  </div>
							              </div>
                                       </td>
                                     <td>
                                        
                                      </td>
                                  </tr>
                                    <tr>
                                      <td >
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"></label>
							                  <div class="controls">
								              <a href="#" onclick="javascript:popup();return false;"> Add Material</a>
                                                  <a id="popup" href='../AkalAcademy1/Example/GridViewWithCheckBox.aspx'  >Add</a>
							                  </div>
							              </div>
                                      </td>
                                      
                                     <td>
                                        
                                      </td>
                                  </tr>
                                    <tr>
                                      <td >
                                          <div class="control-group">
							                  <label class="control-label" for="typeahead"> Add Items</label>
							                  <div class="controls">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                          <asp:GridView runat="server" ID="gvAddItems" AutoGenerateColumns="false" style="font-family:Arial, Helvetica, sans-serif; font-size:12px; color:black;width:550px; height:auto;"   DataKeyNames="MatId" > 
                                            <Columns>
                                                <asp:BoundField DataField="MatId" HeaderText="Mat Id" ItemStyle-Width="70" Visible="false"/>
                                                <asp:BoundField DataField="MatName" HeaderText="Mat Name" ItemStyle-Width="70" />
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="false">
                                                    <ItemTemplate >
                                                        <div style="text-align: right;">
                                                       <asp:Label ID="lblSno" runat="server" class="control-label" ></asp:Label>
                                                            </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                 <asp:TemplateField HeaderText="Item Name" Visible="false"  ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <%--<asp:DropDownList runat="server" ID="ddlMat" width="85Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMat_SelectedIndexChanged"></asp:DropDownList>--%>
                                                        <asp:TextBox ID="txtItmName" runat="server" CssClass="span6 typeahead" width="70Px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Gate Entery No"  ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtGateEntry" runat="server" CssClass="span6 typeahead" width="70Px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock Entery No"  ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtStockEntry" runat="server" CssClass="span6 typeahead" width="70Px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQty" runat="server" CssClass="span6 typeahead" width="70Px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlUnit" width="70Px"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="txtRate" runat="server" CssClass="span6 typeahead" width="70Px"  AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmt" runat="server" class="control-label" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <%--<asp:TemplateField HeaderText="Add More" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                     <ItemTemplate >
                                     
                                                        <asp:Button ID="ButtonAdd2" runat="server" Text="+" ToolTip="Add New Row" OnClick="ButtonAdd2_Click"/>
                                            
                                                     </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                           </asp:GridView>
                                                    </ContentTemplate>
                                               </asp:UpdatePanel>
							                  </div>
							              </div>
                                      </td>
                                      <td><%--<asp:Button runat="server" ID="btnAmtTotal" Text="Total Amount" CssClass="btn btn-success" OnClick="btnAmtTotal_Click"/>--%></td>
                                  </tr>
                                 
                              </table>
							
							
                             
							
							<div class="form-actions">
							 <asp:Button id="btnLoadItem" runat="server" Text="Load Items" CssClass="btn btn-primary" OnClick="btnLoadItem_Click" />
                                
							</div>
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
        <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Previous Bill Details</h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
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

    <div id="divPopUpEstimateMaterial" runat="server">
          <div class="modal hide fade" id="myModal">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal">×</button>
				<h3><b><asp:Label ID="lblZone" runat="server"></asp:Label></b> Assign to Incharge</h3>
			</div>
			<div class="modal-body">
				<%--<div class="controls">                      
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
				        Department <br />
                        <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList><br />
                        Employee <br />
                        <asp:DropDownList ID="ddlEmpl" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEmpl_SelectedIndexChanged" ></asp:DropDownList> <asp:Label ID="Label2" Visible="false" runat="server" ></asp:Label>  <asp:Label ID="lblDesignation" runat="server" ></asp:Label><br />
                    </ContentTemplate>
                    </asp:UpdatePanel>
			    </div>--%>
			</div>
			<div class="modal-footer">
				<a href="#" class="btn" data-dismiss="modal">Close</a>
				<asp:Button id="Button1" Text="Assign Incharge" CssClass="btn btn-primary" runat="server" />
			</div>
		</div>
    </div>
</asp:Content>


