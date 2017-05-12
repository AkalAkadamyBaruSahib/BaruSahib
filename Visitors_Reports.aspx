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

        //function ReportOnChange(control) {
        //    if (control.value == "1") {
        //        $("#divCheckInDate").show();
        //        $("#divEmptyRoomList").hide();
        //        $("#divPlaces").hide();
        //    }
        //    else if (control.value == "2") {
        //        $("#divCheckInDate").hide();
        //        $("#divEmptyRoomList").show();
        //        $("#divPlaces").hide();
        //    }
        //    else if (control.value == "3") {
        //        $("#divCheckInDate").hide();
        //        $("#divEmptyRoomList").hide();
        //        $("#divPlaces").show();
        //    }
        //    else {
        //        $("#divCheckInDate").hide();
        //        $("#divEmptyRoomList").hide();
        //        $("#divPlaces").hide();
        //    }
        //}

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

                <div class="control-group" id="divFilterData" runat="server">
                    <label class="control-label" for="typeahead">Filter By:</label>
                    <asp:DropDownList ID="drpFilterData" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFilterData_SelectedIndexChanged">
                        <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                        <asp:ListItem Text="According Date" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Room Status Report" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Visitors Report By Places" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Permanent Room Detail Report" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Daily Visitor Status Report" Value="7"></asp:ListItem>
                    </asp:DropDownList> 
                    <asp:RequiredFieldValidator ID="reqReport" runat="server" ValidationGroup="vreport" ErrorMessage="Please Select the Report Type" ForeColor="Red" ControlToValidate="drpFilterData" InitialValue="0"></asp:RequiredFieldValidator>
                </div>

                <div class="control-group" id="divCheckInDate" runat="server" visible="false">
                    <label class="control-label" for="typeahead">Check In Date:</label>
                    <asp:TextBox runat="server" ID="txtCheckInDate" CssClass="input-xlarge datepicker" Style="width: 220px; height: 20px;"></asp:TextBox>
                    <label id="lblCheckOut" runat="server" class="control-label" for="typeahead">Check Out Date:</label>
                    <asp:TextBox runat="server" ID="txtCheckOutDate" CssClass="input-xlarge datepicker" Style="width: 220px; height: 20px;"></asp:TextBox>
                </div>

                <div class="control-group" id="divEmptyRooms" runat="server" visible="false">
                    <label class="control-label" for="typeahead">Select Building Name:</label>
                    <asp:DropDownList ID="drpBuildingName" runat="server"></asp:DropDownList>
                </div>
                <br />

                <div class="control-group" id="divPlaces" runat="server" visible="false">
                    Select Country:<asp:DropDownList ID="drpCountry" runat="server" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="-1">All Country</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />
                    Select State:<asp:DropDownList ID="drpState" AutoPostBack="true" OnSelectedIndexChanged="drpState_SelectedIndexChanged" runat="server" Style="float: right; margin-right: 890px;">
                        <asp:ListItem Value="-1">All State</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />
                    <br />
                    Select City:<asp:DropDownList ID="drpCity" runat="server" Style="float: right; margin-right: 890px;">
                        <asp:ListItem Value="-1">All City</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />
                </div>
            </div>
            <div>
                <asp:Button ID="btnDownload" ValidationGroup="vreport" runat="server" Text="Click To Download Report in Excel Sheet" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="355px" />
            </div>
        </div>
    </div>
</asp:Content>

