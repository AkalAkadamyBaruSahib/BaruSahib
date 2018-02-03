<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkshopReport.aspx.cs" Inherits="WorkshopReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/MaterialSearch.js"></script>
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
                myButton.className = "btn btn-success";
                myButton.value = "Please Wait...";
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function confirmation() {
            if (confirm('ARE YOU SURE FOR THIS ACTION?')) {
                return true;
            } else {
                return false;
            }
        }
        function ReportOnChange(ddlselectedvalue) {
            if (ddlselectedvalue.value == "1") {
                $("#divDesigDetails").hide();
                $("#divRadioButton").show();
                $("#divCheckIncharge").hide();
                $("#divMaterialReportByMatName").hide();
                $("#divMaterialReportByAcaName").hide();
            }
            else if (ddlselectedvalue.value == "2") {
                $("#divRadioButton").hide();
                $("#divDesigDetails").show();
                $("#divCheckIncharge").show();
                $("#divMaterialReportByMatName").hide();
                $("#divMaterialReportByAcaName").hide();
            }
            else if (ddlselectedvalue.value == "3") {
                $("#divRadioButton").hide();
                $("#divDesigDetails").show();
                $("#divCheckIncharge").show();
                $("#divMaterialReportByMatName").hide();
                $("#divMaterialReportByAcaName").hide();
            }
            else if (ddlselectedvalue.value == "4") {
                $("#divRadioButton").hide();
                $("#divDesigDetails").show();
                $("#divCheckIncharge").hide();
                $("#divMaterialReportByMatName").show();
                $("#divMaterialReportByAcaName").hide();
            }
            else if (ddlselectedvalue.value == "5") {
                $("#divRadioButton").hide();
                $("#divDesigDetails").show();
                $("#divCheckIncharge").hide();
                $("#divMaterialReportByMatName").hide();
                $("#divMaterialReportByAcaName").show();
            }
            else if (ddlselectedvalue.value == "6") {
                $("#divRadioButton").hide();
                $("#divDesigDetails").show();
                $("#divCheckIncharge").hide();
                $("#divMaterialReportByMatName").hide();
                $("#divMaterialReportByAcaName").hide();
            }
            else if (ddlselectedvalue.value == "7") {
                $("#divRadioButton").hide();
                $("#divDesigDetails").show();
                $("#divCheckIncharge").hide();
                $("#divMaterialReportByMatName").hide();
                $("#divMaterialReportByAcaName").hide();
            }
            else if (ddlselectedvalue.value == "8") {
                $("#divRadioButton").hide();
                $("#divDesigDetails").show();
                $("#divCheckIncharge").hide();
                $("#divMaterialReportByMatName").hide();
                $("#divMaterialReportByAcaName").hide();
            }
            else {
                $("#divRadioButton").hide();
                $("#divDesigDetails").hide();
                $("#divCheckIncharge").hide();
                $("#divMaterialReportByMatName").hide();
                $("#divMaterialReportByAcaName").hide();
            }
        }
    </script>

    <div class="box span10">
        <div class="box-header well" data-original-title>
            <h2><i class="icon-user"></i>Year and Month Wise Workshop Material Reports</h2>
            <div class="box-icon">
                <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
            </div>
        </div>
        <div class="box-content">
            <div class="box-content">
                <table border="0">
                    <tbody>
                        <tr>
                            <td>Select Report to Download Data:
                         <asp:DropDownList ID="ddlWorkshopReport" runat="server" onchange="ReportOnChange(this);" Style="width: 200px; float: right; margin-left: 210px; margin-top: -23px;">
                        <asp:ListItem Value="0">--Select Report--</asp:ListItem>
                        <asp:ListItem Value="1">InStore Report</asp:ListItem>
                        <asp:ListItem Value="2">Dispatch Estimate Report</asp:ListItem>
                        <asp:ListItem Value="3">Pending Estimate Report</asp:ListItem>
                        <asp:ListItem Value="4">Disptch Material Report By Material Name</asp:ListItem>
                        <asp:ListItem Value="5">Disptch Material Report By Academy Name</asp:ListItem>
                        <asp:ListItem Value="6">Summary Report For Disptch Material</asp:ListItem>
                        <asp:ListItem Value="7">Summary Report By Material Name</asp:ListItem>
                        <asp:ListItem Value="8">Dispatch And Pending Material Report</asp:ListItem>
                    </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="box-content" id="divRadioButton" style="display: none">
                Select CheckBox To Download Report:<br />
                <div class="box-content">
                    <asp:CheckBoxList ID="chkworkshop" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                </div>
            </div>
            <div class="box-content" id="divCheckIncharge" style="display: none">
                Select CheckBox To Download Report:<br />
                <div class="box-content">
                    <asp:CheckBoxList ID="chkIncharge" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                </div>
            </div>
            <div id="divDesigDetails" style="display: none">
                <table border="0">
                    <tbody>
                        <tr>
                            <td>Start Date:<asp:TextBox runat="server" ID="txtfirstDate" CssClass="input-xlarge datepicker" Width="100px"></asp:TextBox></td>
                            <td style="margin-left: 150px; float: left;">End Date:<asp:TextBox runat="server" ID="txtlastDate" CssClass="input-xlarge datepicker" Width="100px"></asp:TextBox></td>
                            <td id="divMaterialReportByMatName" style="margin-left: 150px; float: right;">Material Name:
                                <input id="txtMaterial" name="txtMaterial" /><br />
                                <div id="menu-container" style="position: absolute; width: 500px;"></div>
                            </td>
                            <td id="divMaterialReportByAcaName" style="margin-left: 150px; float: right;">Academy Name:
                                <asp:DropDownList runat="server" ID="drpAcaName" Style="float: right; width: 200px;" CssClass="input-xlarge datepicker">
                                    <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                </asp:DropDownList></td>

                        </tr>
                    </tbody>
                </table>
            </div>
            <div>
                <asp:Button ID="btnDownload" runat="server" Text="Download Report" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="120px" />
            </div>
        </div>
    </div>
</asp:Content>


