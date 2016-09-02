<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyMaterials.ascx.cs" Inherits="Admin_UserControls_BodyMaterials" %>
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
    <div id="divAddNew" runat="server">
    <div class="sortable row-fluid">
        <a data-rel="tooltip" title="New Material By User" class="well span3 top-block" href="Admin_MaterialVarify.aspx" style="width: auto;">
            <div style="color: red;">New Material By User</div>
            <span class="notification">
                <asp:Label ID="lblMatCount" runat="server"></asp:Label></span>
        </a>
    </div>
    <div class="row-fluid sortable" id="divMain" runat="server">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>New Material</h2>
                <div class="box-icon">

                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <form class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label" for="typeahead">Select Material Type</label>
                            <div class="controls">
                                <asp:DropDownList ID="ddlMatType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMatType_SelectedIndexChanged1"></asp:DropDownList>
                            </div>
                        </div>
                         <div id="divworkshop" runat="server" class="control-group" visible="false">
                            <label class="control-label" for="typeahead">Select Workshop Type</label>
                            <div class="controls">
                                 <asp:CheckBoxList ID="chkWorkshop" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="typeahead"></label>
                            <div class="controls">
                                <%--<input type="text" class="span6 typeahead" id="typeahead"  data-provide="typeahead" data-items="4" data-source='["Alabama","Alaska","Arizona","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky","Louisiana","Maine","Maryland","Massachusetts","Michigan","Minnesota","Mississippi","Missouri","Montana","Nebraska","Nevada","New Hampshire","New Jersey","New Mexico","New York","North Dakota","North Carolina","Ohio","Oklahoma","Oregon","Pennsylvania","Rhode Island","South Carolina","South Dakota","Tennessee","Texas","Utah","Vermont","Virginia","Washington","West Virginia","Wisconsin","Wyoming"]'>--%>
                                <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                Material's Name
								  <asp:TextBox ID="txtMat" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                Unit<br />
                                <asp:DropDownList ID="ddlUnit" runat="server"></asp:DropDownList>
                                <a href="Admin_Unit.aspx">New Unit</a>
                                <asp:TextBox ID="txtCost" runat="server" CssClass="span6 typeahead" Visible="false"></asp:TextBox>
                                <%--<table>
                                      <tr>
                                          <td>Cost</td>
                                          <td>Unit</td>
                                         
                                      </tr>
                                      <tr>
                                          <td><asp:TextBox ID="txtCost" runat="server" CssClass="span6 typeahead"></asp:TextBox> </td>
                                          <td><asp:DropDownList ID="ddlUnit" runat="server"></asp:DropDownList></td>
                                         
                                      </tr>
                                       <tr>
                                          
                                      </tr>
                                  </table>
                                --%>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="typeahead">Rate</label>
                            <div class="controls">
                                <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="typeahead">Material Picture</label>
                            <div class="controls">
                                <asp:FileUpload ID="fuMaterialpic" runat="server" />
                            </div>
                        </div>

                        <div class="form-actions">

                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false" OnClick="btnEdit_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                            <asp:Button ID="btnExecl" runat="server" Text="Excel Download" CssClass="btn btn-primary" OnClick="btnExecl_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                            <asp:Button ID="btnCl" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCl_Click" />
                        </div>
                    </fieldset>
                </form>

            </div>
        </div>
        <!--/span-->

    </div>
    </div>
    
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-user"></i>Material Details</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
                
            </div>
            <div class="box-content">
                <asp:DropDownList ID="ddlMatTypegrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMatType_SelectedIndexChanged"></asp:DropDownList>
                <div id="divMatDetails" runat="server"></div>

            </div>
        </div>
        <!--/span-->

    </div>
           
</div>
