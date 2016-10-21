<%@ Page Title="" Language="C#" MasterPageFile="~/Visitor_AdminMaster.master" AutoEventWireup="true" CodeFile="ReceptionReceipt.aspx.cs" Inherits="ReceptionReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function CashTypeOnChange(control) {
            if (control.value == "2" || control.value == "3") {
                $("#tdNumber").show();
                $("input[id*='txtNumber']").val("");
            }
            else {
                $("#tdNumber").hide();
                $("input[id*='txtNumber']").val("0");
            }
        }

    </script>
    <div id="content" class="span10">
        <div class="row-fluid sortable" runat="server" id="divAllotment">
            <div class="box span12">
                <div class="box-header well" data-original-title>

                    <h2><i class="icon-user"></i>Reception Receipt
                    </h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="box-content">
                        <asp:HiddenField ID="hdnInchargeID" runat="server" />
                        <asp:HiddenField ID="hdnRecepitNumber" runat="server" />
                        <table>
                            <tr>
                                <td>
                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                                        <tr>
                                            <td colspan="2">
                                                <span style="font-size: 25px; font-weight: bolder; margin-left: 340px;"><b>GURUDWARA BARU SAHIB</b></span><br />
                                                <span style="font-size: 20px; margin-left: 330px;">Sant Attar Singh Hari Sadhu Ashram</span><br />
                                                <span style="font-size: 15px; margin-left: 360px;">Baru Sahib,Distt. Sirmour(H.P.)173101</span><br />
                                                <span style="font-size: 15px; margin-left: 390px;">Tele:01799-276031,276041</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="float: right; margin-right: 430px; margin-top: 20px;">Date:<asp:Label ID="lblDate" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 250px">
                                    <table style="border-radius: 25px; background:#ddd; padding: 20px;  width: 200px;height: 150px;margin-left: 75px;float: left;">
                                        <tr>
                                            <td style="width: 200px; margin-top: 11px;">Received With Thanks From:</td>
                                            <td>
                                                <asp:TextBox ID="txtReceived" runat="server" CssClass="span6 typeahead" Style="width: 220px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator Style="float: right; margin-right: 460px; margin-top: -28px;" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="txtReceived" ErrorMessage="*" /></td>
                                            <td style="width: 300px"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px;">A Sum of Rupees:</td>
                                            <td>
                                                <asp:TextBox ID="txtSumofRupees" CssClass="span6 typeahead" runat="server" Style="width: 220px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator Style="float: right; margin-right: 460px; margin-top: -28px;" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtSumofRupees" ErrorMessage="*" /></td>
                                        </tr>
                                        <tr>
                                            <td>by Cash/Cheque/D.D. No:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlCashType" runat="server" onchange="CashTypeOnChange(this);">
                                                    <asp:ListItem Value="0">--Select Cash Type--</asp:ListItem>
                                                    <asp:ListItem Value="1">By Cash</asp:ListItem>
                                                    <asp:ListItem Value="2">By Cheque</asp:ListItem>
                                                    <asp:ListItem Value="3">By D.D.</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator2" InitialValue="0" Style="float: right; margin-right: 460px;" ForeColor="Red" ControlToValidate="ddlCashType" ErrorMessage="*" /></td>

                                            <td style="width: 70px;display:none;">Date:<asp:Label ID="lblCurrentDate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="tdNumber" style="width: 300px; display: none;">
                                            <td>Enter Cheque/DD Number:</td>
                                            <td>
                                                <asp:TextBox ID="txtNumber" runat="server" ></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtNumber" ErrorMessage="*" /></td>
                                        </tr>
                                    <tr>
                                        <td style="width: 70px; float: left; margin-left: 153px; margin-top: 5px; ">Rs:</td>
                                        <td>
                                            <asp:TextBox ID="txtrs" runat="server" Style="width: 220px; float: right; margin-right: 462px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Style="float: right; margin-right: 450px; margin-top: -28px;" ForeColor="Red" ValidationGroup="visitor" ID="RequiredFieldValidator5" ControlToValidate="txtrs" ErrorMessage="*" /></td>
                                    </tr>
                                </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="form-actions">
                        <asp:Button ID="btnGenerateReceipt" Text="Generate Receipt" ValidationGroup="visitor" runat="server" CssClass="btn btn-primary" OnClick="btnGenerateReceipt_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div id="pnlHtml" runat="server"></div>
</asp:Content>
