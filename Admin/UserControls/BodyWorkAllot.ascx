<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyWorkAllot.ascx.cs" Inherits="Admin_UserControls_BodyWorkAllot" %>

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

    <div class="row-fluid sortable" id="div1st" runat="server">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>New Work Allot</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <form class="form-horizontal">
                    <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="workallot" />
                    <fieldset>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead">Select Zone</label>
                                    <div class="controls">
                                        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select Zone--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlZone" runat="server" InitialValue="0" Display="None" ValidationGroup="workallot" ErrorMessage="Please select Zone." ForeColor="Red" ControlToValidate="ddlZone"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead">Select Academy</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlAcademy" runat="server">
                                            <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlAcademy" runat="server" InitialValue="0" Display="None" ValidationGroup="workallot" ErrorMessage="Please select Academy." ForeColor="Red" ControlToValidate="ddlAcademy"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="control-group">
                            <label class="control-label" for="typeahead">Name of Work</label>
                            <div class="controls">
                                <asp:TextBox ID="txtWorkAllot" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtWorkAllot" runat="server" Display="None" ErrorMessage="Please enter the  Name of Work." ValidationGroup="workallot" ForeColor="Red" ControlToValidate="txtWorkAllot"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="control-group" style="display: none;">
                            <label class="control-label" for="typeahead">Work Image Name</label>
                            <div class="controls">

                                <asp:TextBox ID="txtImageName" runat="server" Width="550px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="typeahead">Upload Work Alloted File </label>
                            <div class="controls">

                                <asp:FileUpload ID="fuImgeFile" runat="server" />
                                <asp:Image ID="imgWorkAllot" runat="server" Width="75px" Height="75px" Visible="false" />
                                <asp:Label runat="server" ID="lblImgFileName" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <div class="form-actions">
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="workallot" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" ValidationGroup="workallot" Visible="false" OnClick="btnEdit_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                            <asp:Button ID="btnExecl" runat="server" Text="Excel Download" CssClass="btn btn-primary" OnClick="btnExecl_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                            <asp:Button ID="btnCl" runat="server" Text="Cancel" CssClass="btn btn-danger" />
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
                <h2><i class="icon-user"></i>
                    <asp:Label ID="lblWorkMsg" runat="server"></asp:Label></h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <table>
                    <tr>
                        <td>
                            <div id="divAcademy" runat="server">
                                Select Academy:
                                <asp:DropDownList ID="drpAcademy" OnSelectedIndexChanged="drpAcademy_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                       </div>
                        </td>
                        <td style="width:10%"></td>
                        <td>
                            <div id="divWorkAllot" runat="server">
                                Select WorkAllot To show Bills:
                                <asp:DropDownList ID="drpWorkAllot" OnSelectedIndexChanged="drpWorkAllot_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                </table>
               <div id="divAcademyDetails" runat="server"></div>
            </div>
        </div>
        <!--/span-->

    </div>

    <div id="divViewbill" class="modal hide fade" style="width: 700px; height: 380px; display: none;">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Estimate Copy Details:-</h3>
        </div>
        <div class="modal-body" style="width: 650px">
            <table id="grdEstDiscription" style="width: 650px" class='table table-striped table-bordered bootstrap-datatable datatable'>
                <thead>
                    <tr>
                        <th>Estimate No</th>
                        <th>Signed Copy</th>
                    </tr>
                </thead>
                <tbody id="tbody">
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <input id="btnclose" value="Close" style="width: 40px" class="btn btn-primary" data-dismiss="modal" />
        </div>
    </div>

</div>
