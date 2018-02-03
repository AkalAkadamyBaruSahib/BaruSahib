<%@ Page Title="" Language="C#" MasterPageFile="Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_ReportDetails.aspx.cs" Inherits="Transport_ReporteDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ReportOnChange(control) {
            if (control.value == "2") {
                $("#divPendingDocumentReport").show();
                $("#divDailyReport").hide();
                $("#divSummaryReport").hide();
                $("#divFutureExire").hide();
            }
            else if (control.value == "1") {
                $("#divPendingDocumentReport").hide();
                $("#divDailyReport").show();
                $("#divSummaryReport").hide();
                $("#divFutureExire").hide();
            }
            else if (control.value == "3") {
                $("#divPendingDocumentReport").hide();
                $("#divDailyReport").hide();
                $("#divSummaryReport").show();
                $("#divFutureExire").hide();
            }
            else if (control.value == "4") {
                $("#divPendingDocumentReport").hide();
                $("#divDailyReport").hide();
                $("#divSummaryReport").hide();
                $("#divFutureExire").show();
            }
            else if (control.value == "5") {
                $("#divPendingDocumentReport").show();
                $("#divDailyReport").hide();
                $("#divSummaryReport").hide();
                $("#divFutureExire").hide();
            }
            else if (control.value == "6") {
                $("#divPendingDocumentReport").hide();
                $("#divDailyReport").show();
                $("#divSummaryReport").hide();
                $("#divFutureExire").hide();
            }
            else {
                $("#divPendingDocumentReport").hide();
                $("#divDailyReport").hide();
                $("#divSummaryReport").hide();
                $("#divFutureExire").hide();
            }
        }
        function RadioButtonOnChange() {
            $("#divRadioButton").show();
        }

    </script>

    <div id="content" class="span10">

        <div class="box-header well">
            <h2><i class="icon-user"></i>Download Transport Reports</h2>
            <div class="box-icon">
                <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
            </div>
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-content">
                    Select Report to Download Data:
                    <asp:DropDownList ID="ddlReport" runat="server" onchange="ReportOnChange(this);">
                        <asp:ListItem Text="--Choose Report Type--" Selected="True" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Daily Document Uploaded Report" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Pending Documents" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Summary Report" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Future Expire Report" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Non Approved Vehicles Report" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Insurance Renewal Report" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Non Approved Vehicles Docs Report" Value="7"></asp:ListItem>

                    </asp:DropDownList><br />
                </div>
                <div class="box-content" id="divPendingDocumentReport" style="display: none">
                    Select Zone to Download Data:
                    <asp:DropDownList ID="ddlZone" runat="server">
                    </asp:DropDownList><br />

                </div>
                <div class="box-content" id="divDailyReport" style="display: none">
                    Date From:                
                            <asp:TextBox runat="server" ID="txtfirstDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>

                    Date To:
                                   <asp:TextBox runat="server" ID="txtlastDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                </div>
                <div class="box-content" id="divSummaryReport" style="display: none">
                    Select Zone to Summary Report:
                    <asp:DropDownList ID="ddlALLZone" runat="server">
                    </asp:DropDownList><br />


                </div>

                <div class="box-content" id="divFutureExire" style="display: none">
                    Select Zone to Future Expire Report:
                    <asp:DropDownList ID="ddlFutureZone" runat="server" onchange="RadioButtonOnChange();">
                    </asp:DropDownList><br />

                    <div class="box-content" id="divRadioButton" style="display: none">
                        Select Button To Download Future Expire Document Report:<br />
                        <div class="box-content">
                            <asp:RadioButtonList ID="rbFutureExpire" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="15" Text="15 days"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30 days"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45 days"></asp:ListItem>
                                <asp:ListItem Value="60" Text="60 days"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Click To Download Excel" OnClientClick="test();" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="235px" />
    </div>
</asp:Content>

