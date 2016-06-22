<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Drawing.aspx.cs" Inherits="Admin_Drawing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/AdminDrawingView.js"></script>
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
      <asp:HiddenField ID="hdnDrawingID" runat="server" />
       <asp:HiddenField ID="hdnAcademyID" runat="server" />
    <div id="content" class="span10">
          <asp:HiddenField ID="hdnInchargeID" runat="server" />
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
                            <legend></legend>
                            <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="drawing" />
                            <table>

                                <tr>
                                    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>--%>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Zone</label>
                                            <div class="controls">
                                                <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlZone" runat="server">
                                                    <asp:ListItem Value="0">--Select Zone--</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="reqddlZone" InitialValue="0" ValidationGroup="drawing" ControlToValidate="ddlZone" ForeColor="Red" ErrorMessage="Please Select the Zone" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </td>
                                    <%-- </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Academy</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlAcademy" runat="server">
                                                       <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredddlAcademy" InitialValue="0" ValidationGroup="drawing" ControlToValidate="ddlAcademy" ForeColor="Red" ErrorMessage="Please Select the Academy" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Drawing Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlDwgType" runat="server">
                                                       <asp:ListItem Value="0">--Select Drawing Type--</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredddlDwgType" InitialValue="0" ValidationGroup="drawing" ControlToValidate="ddlDwgType" ForeColor="Red" ErrorMessage="Please Select the Drawing Type" Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Sub Drawing Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlSubDrawingType" runat="server">
                                                       <asp:ListItem Value="0">--Select Sub Drawing Type--</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredddlSubDrawingType" InitialValue="0" ValidationGroup="drawing" ControlToValidate="ddlSubDrawingType" ForeColor="Red" ErrorMessage="Please Select the Sub Drawing Type" Display="None"></asp:RequiredFieldValidator>

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
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredtxtDrwNo" ValidationGroup="drawing" ControlToValidate="txtDrwNo" ForeColor="Red" ErrorMessage="Please Enter the Drawing No." Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Revision No.</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtRevisionNo" runat="server" Width="100px"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredtxtRevisionNo" ValidationGroup="drawing" ControlToValidate="txtRevisionNo" ForeColor="Red" ErrorMessage="Please Enter the Revision No." Display="None"></asp:RequiredFieldValidator>

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
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredtxtDrwName" ValidationGroup="drawing" ControlToValidate="txtDrwName" ForeColor="Red" ErrorMessage="Please Enter the Drawing Name" Display="None"></asp:RequiredFieldValidator>

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
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredfuDwgFile" ValidationGroup="drawing" ControlToValidate="fuDwgFile" ForeColor="Red" ErrorMessage="Please Upload Auto Cad File" Display="None"></asp:RequiredFieldValidator>

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
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredfuPdf" ValidationGroup="drawing" ControlToValidate="fuPdf" ForeColor="Red" ErrorMessage="Please Upload PDF File" Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>




                            <div class="form-actions">
                                <input type="button" id="btnSave" value="Save" class="btn btn-primary" />
                                 <input type="button" id="btnEdit" value="Edit" class="btn btn-primary" />
                                 <asp:Button ID="btnCl" Visible="false" runat="server" Text="Cancel" CssClass="btn" />
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
                    <table  id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                        <thead>
                            <tr>
                                <th width='40%'>Drawing Details</th>
                                <th width='35%'>Download File</th>
                                <th width='25%'>Uploaded Date</th>
                            </tr>
                        </thead>
                         <tbody id="tbody">
                                </tbody>
                    </table>
                    <%--<div id="divAcademyDetails" runat="server"></div>--%>
                </div>
            </div>
            <!--/span-->

        </div>
    </div>
</asp:Content>

