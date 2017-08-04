<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="PurchaseReportByMaterial.aspx.cs" Inherits="PurchaseReportByMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/MaterialSearch.js"></script>
    <div class="box span10">

        <div class="box-header well" data-original-title>
            <h2><i class="icon-user">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </i>Year and Month Wise Purchase Material Reports</h2>
            <div class="box-icon">
                <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
            </div>
        </div>
        <div class="box-content">
            <div id="divDesigDetails" runat="server">

                <table border="0" width="100%">
                    <tbody>
                        <tr>
                            <td>Select Material Name: 
                                   <input id="txtMaterial" name="txtMaterial" /><br />
                                  <div id="menu-container" style="position: absolute; width: 500px;"></div>
                            </td>
                            <td colspan="23">Select First Date:          
                            <asp:TextBox runat="server" ID="txtfirstDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                            </td>
                            <td>Select Last Date:
                                   <asp:TextBox runat="server" ID="txtlastDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                            </td>
                        </tr>


                    </tbody>
                </table>
            </div>
            <div>
                <asp:Button ID="btnDownload" runat="server" Text="Click To Download Report in Excel Sheet" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" Width="355px" OnClick="btnDownload_Click" />
            </div>
        </div>
    </div>

</asp:Content>
