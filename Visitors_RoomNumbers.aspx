<%@ Page Title="" Language="C#" MasterPageFile="~/Visitor_AdminMaster.master" AutoEventWireup="true" CodeFile="Visitors_RoomNumbers.aspx.cs" Inherits="Visitors_RoomNumbers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/AddVisitorRoomNumber.js"></script>
    <asp:HiddenField ID="hdnRoomID" runat="server" />
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2 style="color: #cc3300;"><i class="icon-edit"></i>New Rooms</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <form class="form-horizontal">
                        <fieldset>
                            <legend></legend>
                            <div class="box-content">
                                <table id="tabledata" width="100%">
                                    <tr>
                                        <td>
                                            <label class="control-label" for="typeahead">Building Name:</label>
                                            <asp:DropDownList ID="drpBuildingName" runat="server">
                                                <asp:ListItem Value="0">--Select Building Name--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqBuilding" runat="server" InitialValue="0" ErrorMessage="Please select the Building Name" ForeColor="Red" ControlToValidate="drpBuildingName" ValidationGroup="visitorroom"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <label class="control-label" for="typeahead">Building Floor:</label>
                                            <asp:DropDownList ID="drpBuildingFloor" runat="server">
                                                <asp:ListItem Value="-1">--Select Building Floor--</asp:ListItem>
                                                <asp:ListItem Value="0">Ground Floor</asp:ListItem>
                                                <asp:ListItem Value="1">Floor One</asp:ListItem>
                                                <asp:ListItem Value="2">Floor Two</asp:ListItem>
                                                <asp:ListItem Value="3">Floor Three</asp:ListItem>
                                                <asp:ListItem Value="4">Floor Four</asp:ListItem>
                                                <asp:ListItem Value="5">Floor Five</asp:ListItem>
                                                <asp:ListItem Value="6">Floor Six</asp:ListItem>
                                                <asp:ListItem Value="7">Floor Seven</asp:ListItem>
                                                <asp:ListItem Value="8">Floor Eight</asp:ListItem>
                                                <asp:ListItem Value="9">Floor Nine</asp:ListItem>
                                                <asp:ListItem Value="10">Floor Ten</asp:ListItem>
                                                <asp:ListItem Value="11">Floor Eleven</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the Building Floor" ForeColor="Red" InitialValue="-1" ControlToValidate="drpBuildingFloor" ValidationGroup="visitorroom"></asp:RequiredFieldValidator>
                                        </td>
                                        <br />
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label" for="typeahead">Room Number:</label>
                                            <asp:TextBox ID="txtRoomNumber" runat="server" Style="width: 220px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter the Room Number" ForeColor="Red" ControlToValidate="txtRoomNumber" ValidationGroup="visitorroom"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <label class="control-label" for="typeahead">Number of Bed:</label>
                                            <asp:TextBox ID="txtNoOfBed" runat="server" Style="width: 220px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter the Number of Bed" ForeColor="Red" ControlToValidate="txtNoOfBed" ValidationGroup="visitorroom"></asp:RequiredFieldValidator>
                                        </td>
                                        <br />
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label" for="typeahead">Is Permanent:</label>
                                            <asp:CheckBox ID="chkIsPermant" runat="server"/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnSave" Text="Save" runat="server" ValidationGroup="visitorroom" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                <asp:Button ID="btnEdit"  Text="Edit" runat="server" CssClass="btn btn-primary" />
                                  <asp:Button ID="btnClr"  Text="Clear" runat="server" OnClick="btnClr_Click" />
                            </div>
                        </fieldset>
                    </form>
                </div>
            </div>
            <!--/span-->

        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2 style="color: #cc3300;"><i class="icon-user"></i>Rooms Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">

                    <div id="divMatDetails" runat="server">
                        <div id="divRoomDetails" runat="server">
                            <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <thead>
                                    <tr>
                                        <th style="color: #cc3300;">Building Name</th>
                                        <th style="color: #cc3300;">Building Floor</th>
                                        <th style="color: #cc3300;">Room Number</th>
                                        <th style="color: #cc3300;">Number of Bed</th>
                                        <th style="color: #cc3300;">Action</th>
                                    </tr>
                                </thead>
                                <tbody id="tbody">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <!--/span-->

            </div>
        </div>
    </div>
</asp:Content>

