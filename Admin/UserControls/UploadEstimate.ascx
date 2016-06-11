<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadEstimate.ascx.cs"
    Inherits="Admin_UserControls_UploadEstimate" %>

<script type="text/javascript">
    function ClientSideClick(myButton) {

        if (typeof (Page_ClientValidate) == 'function') {
            if (Page_ClientValidate() == false)
            { return false; }
        }
        if (myButton.getAttribute('type') == 'button') {
            myButton.disabled = true;
            myButton.className = "btn btn-success";
            myButton.value = "Please Wait...";
        }
        return true;
    }

    function validateFileSize() {
        var uploadControl = document.getElementById('<%= fileUploadSignedCopy.ClientID%>').files[0].size;
          if (uploadControl > 1048576) {
              document.getElementById('dvMsg').style.display = "block";
              return false;
          }
          else {
              document.getElementById('dvMsg').style.display = "none";
              return true;
          }
      }
  

</script>
<div id="content" class="span10">
    <asp:HiddenField ID="hdnEstimateID" runat="server" />
    <asp:HiddenField ID="hdnSelectedItems" runat="server" />
    <asp:HiddenField ID="hdnItemsLength" runat="server" />
    <asp:HiddenField ID="hdnIsAdmin" runat="server" />
    <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Create Estimate</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <fieldset>
                <legend></legend>
                <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="visitor" />
                <div class="box-content">
                    <table width="100%">
                        <tr>
                            <td width="50%" colspan="2">
                                <div class="control-group">
                                    <h2>
                                        <label id="lblWorkNameReflect"></label>
                                </div>
                            </td>

                        </tr>
                        <tr id="trEstimateNo" runat="server" visible="false">
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Estimate No.
								                <asp:Label ID="lblEstimateNo" runat="server" Visible="false"></asp:Label>
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
                        <tr id="trZone" runat="server">
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Zone 
                                        <br />
                                        <asp:DropDownList ID="ddlZone" runat="server">
                                            <asp:ListItem Value="0">--Select Zone--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" InitialValue="0" Display="None" ValidationGroup="visitor" ID="ddlZone_RequiredFieldValidator"
                                            ControlToValidate="ddlZone" ErrorMessage="Please Select Any Zone" ForeColor="#ff0000"></asp:RequiredFieldValidator><br />
                                    </div>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Academy
                                                    <br />
                                        <asp:DropDownList ID="ddlAcademy" runat="server">
                                            <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" InitialValue="0" ForeColor="#ff0000" ID="ddlAcademy_RequiredFieldValidator"
                                            ControlToValidate="ddlAcademy" ErrorMessage="Please Select Any Academy" /><br />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" id="tdWorkAllot">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Select Work Allot
                                        <br />
                                        <asp:DropDownList ID="ddlWorkAllot" runat="server" Width="500px">
                                            <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" InitialValue="0" ID="ddlWorkAllot_RequiredFieldValidator"
                                            ControlToValidate="ddlWorkAllot" ForeColor="#ff0000" ErrorMessage="Please Select Any Work" />
                                        <br />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%" colspan="2">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Sub Estimate
								                <asp:TextBox ID="txtSubEstimate" runat="server" CssClass="span6 typeahead" Width="910px"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" ID="txtSubEstimate_RequiredFieldValidator"
                                            ControlToValidate="txtSubEstimate" ForeColor="#ff0000" ErrorMessage="Please Enter The Sub Estimate" />
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
                            <td colspan="2" width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Type Of work
                                                    <br />
                                        <asp:DropDownList ID="ddlTypeOfWork" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" ID="ddlTypeOfWork_RequiredFieldValidator"
                                            ControlToValidate="ddlTypeOfWork" ForeColor="#ff0000" ErrorMessage="Please Select Any Type Of Work" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>

                    <table width="100%">
                        <tr id="trCost" runat="server" visible="false">
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Estimate Cost
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
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        File Name
                                                    <br />
                                        <asp:TextBox runat="server" ID="txtFileName"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" ID="txtFileName_RequiredFieldValidator"
                                            ControlToValidate="txtFileName" ForeColor="#ff0000" ErrorMessage="Please Enter The File Name" />
                                        <div id="dvMsg" style="color:Red; width:190px;float: right; margin-right: -395px; padding:3px; display:none;" >Maximum size allowed Less than is 1 MB                                   
                                    </div>
                                </div>
                            </td>

                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Upload File
                                        <br />
                                        <asp:FileUpload ID="fileUploadSignedCopy" runat="server" AllowMultiple="true" onchange="validateFileSize();" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidatorphoto" ControlToValidate="fileUploadSignedCopy" runat="server" ValidationGroup="visitor" Display="None" ErrorMessage="Please Upload the File" ></asp:RequiredFieldValidator>
                                       </div>
                                </div>
                            </td>
                        </tr>

                    </table>

                    <table style="width: 100%" border="0">

                        <tr>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Select  Material Type
                                        <br />
                                        <%--<input type="text" name="txtMaterial" id="txtMaterial" />--%>
                                        <select id="drpMaterialType" multiple="multiple" style="width: 400px; height: 150px;">
                                        </select>
                                    </div>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Select  Material Names
                                        <br />
                                        <select id="drpMaterialName" multiple="multiple" style="width: 400px; height: 150px;"></select>
                                        <br />
                                        <asp:Button ID="btnloadMaterials" Text="Load Material" CssClass="btn btn-success" runat="server" />
                                    </div>
                                </div>
                            </td>
                        </tr>

                    </table>
                </div>
                <div class="row-fluid sortable">
                    <div class="box span12">
                        <div class="box-content">
                            <div id="trEstimateDetail">

                                <table id="grid" style="width: 1000px;" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                    <thead>
                                        <tr>
                                            <th style="color: #cc3300; width:344px;">MaterialName</th>
                                            <th style="color: #cc3300; width: 239px;">Source Type</th>
                                            <th style="color: #cc3300; width: 120px;">Quantity</th>
                                            <th style="color: #cc3300; width: 28px;">Unit</th>
                                            <th style="color: #cc3300; width: 121px">Rate</th>
                                            <th style="color: #cc3300; width: 45px;">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbody">
                                    </tbody>
                                </table>
                               
                                  <input type="button" id="btnTotalCost" value="Toatl Amount"  title="Toatl Amount" style="margin-right: 180px;float: right;" class="btn btn-success" />
                                 <asp:Label ID="Label1" runat="server" ForeColor="Red" style="margin-right: 125px;float: right; margin-top: -23px;">Estimate Cost:</asp:Label>
                                  <asp:Label ID="lblAmt" runat="server" ForeColor="Red" style="margin-right: 22px; margin-top: -23px; float: right;" Text="00.00"></asp:Label>
                                 <%--  <label id="lblAmt"></label>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-actions" style="text-align: center">
                    <%--<asp:Button ID="btnSubEstimate" Width="200px" Height="40px" Text="Submit Estimate" CssClass="btn btn-success" runat="server" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" BackColor="Green" Font-Bold="True" Font-Size="16pt" ForeColor="Black" />--%>
                    <input type="button" id="btnSubEstimate" value="Submit Estimate" title="Submit Estimate" class="btn btn-success" />
                </div>
            </fieldset>
        </div>
    </div>
</div>
<div style="display: none" id="progress">
        <table style="text-align:center">
            <tr>
                <td style="text-align:center">
                    <img src="img/animated.gif" />
                </td>
            </tr>
            <tr>
                <td>Wait while estimate is uploading....
                </td>
            </tr>
        </table>
    </div>