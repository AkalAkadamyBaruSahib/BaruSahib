<%@ Page Title="" Language="C#" MasterPageFile="Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_ReportDetails.aspx.cs" Inherits="Transport_ReporteDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ReportOnChange(control) {
            if (control.value == "2") {
                $("#divPendingDocumentReport").show();
                $("#divDailyReport").hide();
                $("#divSummaryReport").hide();
            }
            else if (control.value == "1") {
                $("#divPendingDocumentReport").hide();
                $("#divDailyReport").show();
                $("#divSummaryReport").hide();
            }
            else if (control.value == "3") {
                $("#divPendingDocumentReport").hide();
                $("#divDailyReport").hide();
                $("#divSummaryReport").show();
            }
            else {
                $("#divPendingDocumentReport").hide();
                $("#divDailyReport").hide();
                $("#divSummaryReport").hide();
            }
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
                    </asp:DropDownList><br />
                </div>
                <div class="box-content" id="divPendingDocumentReport" style="display: none">
                    Select Zone to Download Data:
                    <asp:DropDownList ID="ddlZone" runat="server">
                    </asp:DropDownList><br />
                    <div>
                        <div class="box-content">
                            Select Button To Download Future Expire Document Report:<br />
                            <div class="box-content">
                                <asp:RadioButton ID="rb15Days" runat="server" GroupName="ExpireReprt" />
                                <asp:Label ID="lblrb15days" runat="server">15 days</asp:Label>
                                <asp:RadioButton ID="rb30Days" runat="server" GroupName="ExpireReprt" />
                                <asp:Label ID="lblrb30days" runat="server">30 Days</asp:Label>
                                <asp:RadioButton ID="rb45Days" runat="server" GroupName="ExpireReprt" />
                                <asp:Label ID="lblrb45days" runat="server">45 Days</asp:Label>
                                <asp:RadioButton ID="rb60Days" runat="server" GroupName="ExpireReprt" />
                                <asp:Label ID="lblrb60days" runat="server">60 Days</asp:Label>
                            </div>
                        </div>
                    </div>
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
            </div>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Click To Download Excel" OnClientClick="test();" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="235px" />
    </div>
</asp:Content>

