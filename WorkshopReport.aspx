<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkshopReport.aspx.cs" Inherits="WorkshopReport" %>

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
            <div id="divDesigDetails" runat="server">
                <table border="0">
                    <tbody>
                        <tr>
                            <td>Start Date:<asp:TextBox runat="server" ID="txtfirstDate" CssClass="input-xlarge datepicker" Width="100px"></asp:TextBox>                      
                           </td>
                            <td style="margin-left:140px; float:left;" >End Date:<asp:TextBox runat="server" ID="txtlastDate" CssClass="input-xlarge datepicker" Width="100px"></asp:TextBox>
                            </td>
                            <td></td>
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


