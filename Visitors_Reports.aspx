<%@ Page Title="" Language="C#" MasterPageFile="~/Visitor_AdminMaster.master" AutoEventWireup="true" CodeFile="Visitors_Reports.aspx.cs" Inherits="Visitors_Reports" %>

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
            <h2 style="color: #cc3300;"><i class="icon-user"></i>Visitors Reports</h2>
            <div class="box-icon">
                <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
            </div>
        </div>
        <div class="box-content">
            <div id="divDesigDetails" runat="server">

                <table border="0" width="100%">
                    <%--class="table table-striped table-bordered bootstrap-datatable datatable"--%>
                    <tbody>
                        <tr>
                            <td width="50%">
                                <div class="control-group" id="divFilterData" runat="server">
                                    <label class="control-label" for="typeahead">Filter By:</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="drpFilterData" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpFilterData_SelectedIndexChanged">
                                            <asp:ListItem Text="" Value="NULL">--Select One--</asp:ListItem>
                                            <%--   <asp:ListItem Text="Visitors" Value="Visitors"></asp:ListItem>
                                                    <asp:ListItem Text="Sewadar" Value="Sewadar"></asp:ListItem>
                                                    <asp:ListItem Text="Employee" Value="Employee"></asp:ListItem>--%>
                                            <asp:ListItem Text="Date" Value="Date"></asp:ListItem>
                                            <%--  <asp:ListItem Text="Reference By" Value="Reference By"></asp:ListItem>
                                                    <asp:ListItem Text="Room According" Value="Room According"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <div class="control-group" id="divCheckOutDate" runat="server">
                                    <label class="control-label" for="typeahead">Check Out Date:</label>
                                    <asp:TextBox runat="server" ID="txtCheckOutDate" CssClass="input-xlarge datepicker" Style="width: 220px; height: 20px;"></asp:TextBox>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group" id="divCheckInDate" runat="server">
                                    <label class="control-label" for="typeahead">Check In Date:</label>
                                    <asp:TextBox runat="server" ID="txtCheckInDate" CssClass="input-xlarge datepicker" Style="width: 220px; height: 20px;"></asp:TextBox>
                                </div>
                            </td>



                        </tr>
                        <tr>
                            <td width="50%">
                                <div class="control-group" id="divName" runat="server">
                                    <label class="control-label" for="typeahead">Name:</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlName" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div>
                <asp:Button ID="btnDownload" runat="server" Text="Click To Download Report in Excel Sheet" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="355px" />
            </div>
        </div>
    </div>
</asp:Content>

