<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_WorkAllot.aspx.cs" Inherits="Admin_WorkAllot" %>

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
                        <fieldset>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead">Select Zone</label>
                                        <div class="controls">
                                            <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead">Select Academy</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddlAcademy" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="control-group">
                                <label class="control-label" for="typeahead">Name of Work</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtWorkAllot" runat="server" CssClass="span6 typeahead"></asp:TextBox>
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
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false" OnClick="btnEdit_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                <asp:Button ID="btnExecl" runat="server" Text="Excel Download" CssClass="btn btn-primary" OnClick="btnExecl_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
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
                    <h2><i class="icon-user"></i>Work Allot Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divAcademyDetails" runat="server"></div>
                </div>
            </div>
            <!--/span-->

        </div>

    </div>
</asp:Content>
