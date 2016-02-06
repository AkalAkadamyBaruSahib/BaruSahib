<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Drawing.aspx.cs" Inherits="Admin_Drawing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                    <h2><i class="icon-edit"></i>New Drawing Upload</h2>
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
                                    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>--%>
                                            <td>
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Zone</label>
                                                    <div class="controls">
                                                        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </td>
                                       <%-- </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Academy</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlAcademy" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Drawing Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlDwgType" OnSelectedIndexChanged="ddlDwgType_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
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
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Drawing No.</label>
                                            <div class="controls">

                                                <asp:TextBox ID="txtDrwNo" runat="server" Width="100px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Revision No.</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtRevisionNo" runat="server" Width="100px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Drawing Name</label>
                                            <div class="controls">

                                                <asp:TextBox ID="txtDrwName" runat="server" Width="550px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Upload Auto Cad File(for record) Ex:.dwg</label>
                                            <div class="controls">

                                                <asp:FileUpload ID="fuDwgFile" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>

                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Upload PDF File. Ex:.pdf</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fuPdf" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>




                            <div class="form-actions">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnEdit_Click" />
                                <asp:Button ID="btnCl" runat="server" Text="Cancel" CssClass="btn" />
                            </div>
                        </fieldset>
                    </form>

                </div>
            </div>
            <!--/span-->

        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Drawing Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <table class='table table-striped table-bordered bootstrap-datatable datatable'>
                        <thead>
                            <tr>
                                <th style="display:none;"></th>
                                <th width='40%'>Drawing Details</th>
                                <th width='35%'>Download File</th>
                                <th width='25%'>Uploaded Date</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptDrawings" OnItemCommand="rptDrawings_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td style="display:none;">1</td>
                                        <td width='40%'>
                                            <table>
                                                <tr>
                                                    <td>Zone:
                                                        <asp:Label ID="lblZone" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ZoneName")%>'></asp:Label></td>
                                                    <td>Drawing No:
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DwgNo")%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Academy:
                                                        <asp:Label ID="lblAca" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AcaName")%>'></asp:Label></td>
                                                    <td>Revision No:
                                                        <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RevisionNo")%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Drawing Name:
                                                        <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DrawingName")%>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--<td width='35%'><table><tr><td>.pdf File: <asp:LinkButton ID="LinkButton10" runat="server" CommandName="pdf" CommandArgument='<%#Bind("PdfFilePath") %>' ><asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PdfFileName")%>'></asp:Label> </asp:LinkButton></td></tr>
                                        <tr><td> .dwg File: <asp:LinkButton ID="LinkButton1" runat="server" CommandName="dwg" CommandArgument='<%#Bind("DwgFilePath") %>' ><asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DwgFileName")%>'></asp:Label> </asp:LinkButton></td></tr></table></td>--%>
                                        <td width='35%'>
                                            <table>
                                                <tr>
                                                    <td>.PDF File:
                                                        <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PdfFileName")%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>.DWG File:
                                                        <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DwgFileName")%>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width='25%'>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedOn")%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnDwgEdit" Text="Edit Drawing" CssClass="btn btn-info" CommandName="EditDwg" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"DwgId") %>' /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <%--<div id="divAcademyDetails" runat="server"></div>--%>
                </div>
            </div>
            <!--/span-->

        </div>
    </div>
</asp:Content>

