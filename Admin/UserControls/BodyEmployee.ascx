<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyEmployee.ascx.cs" Inherits="Admin_UserControls_BodyEmployee" %>
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
        <div class="row-fluid sortable" runat="server" id="divAllotment">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Create Employee</h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <form class="form-horizontal">

                        <fieldset>
                            <div class="box-content">
                                <table width="100%">
                                    <tr>
                                        <td width="50%">
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead">Name</label>
                                                <div class="controls">
                                                    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                        <td width="50%"></td>
                                    </tr>



                                    <tr>
                                        <td width="50%">
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead"></label>
                                                <div class="controls">
                                                    Mobile
                                                    <asp:TextBox ID="txtMob" runat="server" CssClass="span6 typeahead"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMob" ErrorMessage="Invalid Mob No" ValidationExpression="[0-9]{10}" ForeColor="Red"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </td>
                                        <td width="50%"></td>
                                    </tr>
                                    <tr>
                                        <td width="50%">
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead"></label>
                                                <div class="controls">
                                                    Designation
                                                    <br />
                                                    <asp:DropDownList ID="ddlDesig" runat="server"></asp:DropDownList><br />
                                                    Department
                                                    <br />
                                                    <asp:DropDownList ID="ddlDept" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                        <td width="50%"></td>
                                    </tr>
                                    <tr>
                                        <td width="50%" colspan="2">
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead">
                                                    <h4>Login Details</h4>
                                                </label>
                                                <div class="controls">
                                                    User Type:
                                                    <br />
                                                    <asp:DropDownList ID="ddlUserType" runat="server"></asp:DropDownList><br />
                                                </div>
                                            </div>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td width="50%">
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead"></label>
                                                <div class="controls">
                                                    Login Id:
                                                    <br />
                                                    <asp:TextBox ID="txtLoginId" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtLoginId" ForeColor="Red" ErrorMessage="Invalid Login Id Format!!!! Use The Email Format"></asp:RegularExpressionValidator>
                                                    <br /> 
                                                     Password:<br />
                                                    <asp:TextBox ID="txtUserPwd" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                        <td width="50%"></td>
                                    </tr>
                                </table>


                            </div>


                            <div class="form-actions">

                                <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" runat="server" OnClick="btnSave_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                <asp:Button ID="btnEdit" Text="Edit" CssClass="btn btn-primary" runat="server" OnClick="btnEdit_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                <asp:Button ID="btnExecl" runat="server" Text="Excel Download" CssClass="btn btn-primary" OnClick="btnExecl_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                <asp:Button ID="Button1" Text="Cancel" CssClass="btn" runat="server" OnClick="Button1_Click" />

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
                    <h2><i class="icon-user"></i>Employee Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divDesigDetails" runat="server"></div>
                </div>
            </div>
            <!--/span-->

        </div>
    </div>