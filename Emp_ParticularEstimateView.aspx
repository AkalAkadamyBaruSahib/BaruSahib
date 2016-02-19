<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_ParticularEstimateView.aspx.cs" Inherits="Emp_ParticularEstimateView" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnIsApproved" runat="server" />
    <asp:HiddenField ID="hdnIsItemRejected" runat="server" />
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Estimate</h2>
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
                                            <h2>ESTIMATE FOR DEVLOPMENT OF ACADEMY</h2>
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
                                                ZONE:
                                                <asp:Label runat="server" ID="lblZone"></asp:Label>(
                                                <asp:Label runat="server" ID="lblZoneCode"></asp:Label>)
                                                   
                                                    
                                                 
                                            </div>

                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                ACADEMY:
                                                <asp:Label runat="server" ID="lblAca"></asp:Label>(
                                                <asp:Label runat="server" ID="lblAcaCode"></asp:Label>)
                                                 
                                                  
                                            </div>

                                        </div>
                                    </td>

                                </tr>

                                <tr>
                                    <td width="50%" colspan="2">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                SUB ESTIMATE:
                                                <asp:Label runat="server" ID="lblSubEstimate"></asp:Label>

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
                                                TYPE OF WORK:
                                                <asp:Label runat="server" ID="lblTypeOfWork"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                SANCTION DATE:
                                                <asp:Label runat="server" ID="lblSanctionDate"></asp:Label><br />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                ESTIMATE COST:
                                                <asp:Label ID="lblEstimateCost" runat="server" ForeColor="Red" Text="00.00"></asp:Label>
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
                                <tr>
                                    <td colspan="2" width="50%">
                                        <div id="pnlPdf" runat="server"></div>
                                        <%--<asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound" Visible="false"></asp:GridView>--%>
                                        <asp:Button ID="btnPdf" runat="server" Visible="false" Text="Estimate Download" CssClass="btn btn-primary" Width="320px" Height="40px" Font-Bold="True" Font-Size="16pt" ForeColor="Black" title="Click this button you get Estimate Statement with Material Details in PDF" data-rel="tooltip" OnClick="btnPdf_Click" />
                                        <%--  onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" --%>
                                    </td>
                                </tr>

                            </table>


                        </div>


                        <%----%>
                    </fieldset>
                </form>

            </div>
        </div>
        <!--/span-->


    </div>





</asp:Content>
