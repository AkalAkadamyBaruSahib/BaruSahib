<%@ Page Title="" Language="C#" MasterPageFile="~/ArchMaster.master" AutoEventWireup="true" CodeFile="Arch_UploadDrawing.aspx.cs" Inherits="Arch_UploadDrawing" %>

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

        function validateFileDwg() {
            var uploadControl = document.getElementById('<%= fuDwgFile.ClientID%>').files[0].name;
            var ext = uploadControl.split('.').pop().toLowerCase();
            if ($.inArray(ext, ['dwg','Zip','zip']) == -1) {
                document.getElementById('<%= fuDwgFile.ClientID%>').value = '';
                alert('Invalid extension! Please Upload the .dwg or zip Files');
                return false;
            }
        }

        function validateFilePdf() {
            var uploadControl = document.getElementById('<%= fuPdf.ClientID%>').files[0].name;
             var ext = uploadControl.split('.').pop().toLowerCase();
             if ($.inArray(ext, ['pdf', 'Zip', 'zip']) == -1) {
                 document.getElementById('<%= fuPdf.ClientID%>').value = '';
                 alert('Invalid extension! Please Upload the .pdf or zip Files');
                 return false;
             }
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
                            <legend></legend>
                            <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="drawing" />
                            <table>

                                <tr>

                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Zone</label>
                                            <div class="controls">
                                                <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="reqddlZone" InitialValue="0" ValidationGroup="drawing" ControlToValidate="ddlZone" ForeColor="Red" ErrorMessage="Please Select the Zone" Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Academy</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlAcademy" runat="server"></asp:DropDownList>
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
                                                <asp:DropDownList ID="ddlDwgType" OnSelectedIndexChanged="ddlDwgType_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredddlDwgType" InitialValue="0" ValidationGroup="drawing" ControlToValidate="ddlDwgType" ForeColor="Red" ErrorMessage="Please Select the Drawing Type" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Sub Drawing Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlSubDrawingType" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredddlSubDrawingType" InitialValue="0" ValidationGroup="drawing" ControlToValidate="ddlSubDrawingType" ForeColor="Red" ErrorMessage="Please Select the Sub Drawing Type" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </td>
                                    <td></td>
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
                                            <label class="control-label" for="typeahead">Upload Auto Cad File(for record) Ex:.dwg or .zip</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fuDwgFile" runat="server" onchange="validateFileDwg();" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredfuDwgFile" ValidationGroup="drawing" ControlToValidate="fuDwgFile" ForeColor="Red" ErrorMessage="Please Upload Auto Cad File" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Upload PDF File. Ex:.pdf or .zip</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fuPdf" runat="server" onchange="validateFilePdf();" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredfuPdf" ValidationGroup="drawing" ControlToValidate="fuPdf" ForeColor="Red" ErrorMessage="Please Upload PDF File" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>




                            <div class="form-actions">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="drawing"  UseSubmitBehavior="False" OnClick="btnSave_Click" OnClientClick="ClientSideClick(this)" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false" />
                                <asp:Button ID="btnCl" runat="server" Text="Cancel" CssClass="btn" />
                            </div>
                        </fieldset>
                    </form>

                </div>
            </div>
            <!--/span-->

        </div>
    </div>
</asp:Content>

