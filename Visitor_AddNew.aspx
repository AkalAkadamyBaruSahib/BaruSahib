<%@ Page Title="Akal Academy | Visitor Record" Language="C#" MasterPageFile="~/Visitor_AdminMaster.master" AutoEventWireup="true" CodeFile="Visitor_AddNew.aspx.cs" Inherits="Visitor_AddNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                myButton.className = "btn btn-primary";
                myButton.value = "Please Wait...";
            }
            return true;
        }
    </script>
    <style>
        #holder
        {
            height: 750px;
            width: 740px;
            background-color: #F5F5F5;
            border: 1px solid #A4A4A4;
            margin-left: 10px;
            overflow: auto;
        }

        #place
        {
            margin: 7px;
        }

            /*li {
            line-height: 68px;
        }*/

            #place a
            {
                font-size: 0.6em;
            }

            #place li
            {
                list-style: none outside none;
            }

                #place li:hover
                {
                    background-color: yellow;
                }

            #place .seat
            {
                background: url("img/available_seat_img.gif") no-repeat scroll 0 0 transparent;
                height: 50px;
                width: 33px;
                display: block;
            }

            #place .selectedSeat
            {
                background: url("img/booked_seat_img.gif") no-repeat scroll 0 0 transparent;
                height: 50px;
                width: 33px;
                display: block;
            }

            #place .selectingSeat
            {
                background-image: url("img/selected_seat_img.gif");
            }

            #place .row-3, #place .row-4
            {
                margin-top: 10px;
            }

        #seatDescription li
        {
            vertical-align: top;
            list-style: none outside none;
            padding-left: 20px;
            float: left;
        }

        .controls
        {
            width: 519px;
        }
    </style>

    <script src="JavaScripts/Visitor.js"></script>

    <script type="text/javascript">
        function validate() {
            var selct = document.getElementById('drpbuilding');
            var msg = "";
            var flag = true;
            if (msg.length == null) {
                msg += "Please Select the item";
                flag = false;
            }
            else {
                return flag;

            }
        }
    </script>

    <div id="content" class="span10">
        <asp:HiddenField ID="hdnbookedSeats" runat="server" />
        <asp:HiddenField ID="hdnNewSeats" runat="server" />
        <asp:HiddenField ID="hdnBuildingID" runat="server" />
        <asp:HiddenField ID="hdnVisitorID" runat="server" />

        <div class="row-fluid sortable" runat="server" id="divAllotment">
            <div class="box span12">
                <div class="box-header well" data-original-title>

                    <h2><i class="icon-user"></i>
                        <asp:Label ID="lblTag" runat="server" Text="" ForeColor="#cc3300"></asp:Label></h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">

                    <fieldset>
                        <legend></legend>
                        <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="visitor" />
                        <div class="box-content">
                            <table id="tabledata" width="100%">
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Full Name:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="span6 typeahead" Style="width: 220px;"></asp:TextBox>

                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="reqName"
                                                    ControlToValidate="txtName" ErrorMessage="Please enter the Full Name Of Visitor" />
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Visitor Contact Number:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtContactNumber" CssClass="span6 typeahead" runat="server" Style="width: 220px;"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="regxnumbervalidator" runat="server" ControlToValidate="txtContactNumber" ForeColor="Red" Font-Size="13px" ErrorMessage="Please Enter only at least 10 Digit Numbers" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator Display="None" runat="server"
                                                    ID="RequiredFieldValidator3" ControlToValidate="txtContactNumber"
                                                    ErrorMessage="Please enter the contact Number" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Address:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server" Style="width: 220px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor"
                                                    ID="RequiredFieldValidator2" ControlToValidate="txtAddress"
                                                    ErrorMessage="Please enter Visitor Address" />
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">City:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtcity" runat="server" CssClass="span6 typeahead" Style="width: 220px;"></asp:TextBox>

                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator6"
                                                    ControlToValidate="txtcity" ErrorMessage="Please Enter the City" />
                                            </div>
                                        </div>
                                    </td>




                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">State:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtstate" runat="server" CssClass="span6 typeahead" Style="width: 220px;"></asp:TextBox>

                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator4"
                                                    ControlToValidate="txtstate" ErrorMessage="Please Enter the State" />
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Country:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtcountry" runat="server" CssClass="span6 typeahead" Style="width: 220px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator1"
                                                    ControlToValidate="txtcountry" ErrorMessage="Please Enter the Country" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>


                                <tr>
                                    <td colspan="1" width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Select Building Name with Room Number:</label>
                                            <div class="controls">
                                                <a id="aRoomNumber" style="font-size: 13px;" href="#">Allocated Rooms: </a>
                                                <div id="selectdiv">
                                                    <select id="drpbuilding">
                                                        <option></option>
                                                    </select>

                                                </div>

                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group" id="dividphoto" runat="server">
                                            <label class="control-label" for="typeahead">Upload Photo:</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fileUploadphoto" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorphoto" ControlToValidate="fileUploadphoto" runat="server" ValidationGroup="visitor" Display="None" ErrorMessage="Please Upload the Photo"></asp:RequiredFieldValidator>
                                                <a id="aPhoto" style="font-size: 13px;" target="_blank">Photo</a>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Identity proof type:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="drpProofType" runat="server">
                                                    <asp:ListItem Text="" Value="NULL">--Select One--</asp:ListItem>
                                                    <asp:ListItem Text="Licence" Value="Licence"></asp:ListItem>
                                                    <asp:ListItem Text="Voter Card" Value="Voter Card"></asp:ListItem>
                                                    <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                                    <asp:ListItem Text="Adhaar Card" Value="Adhaar Card"></asp:ListItem>
                                                    <asp:ListItem Text="Identity Card (Any)" Value="Identity Card (Any)"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Upload Identity Proof:</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fileUploadIdentity" runat="server" />
                                                <asp:RequiredFieldValidator ID="rerurduploadimage" ControlToValidate="fileUploadIdentity" runat="server" ValidationGroup="visitor" Display="None" ErrorMessage="Please Upload the Id Proof"></asp:RequiredFieldValidator>
                                                <a id="aIdentityProof" style="font-size: 13px;" target="_blank">Visitor Proof</a>
                                            </div>
                                        </div>
                                    </td>
                                </tr>


                                <tr>
                                    <td width="50%">
                                        <div class="control-group" id="divRefenceNo">
                                            <label class="control-label" for="typeahead">If No Identity proof, Any Reference:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtReference" CssClass="span6 typeahead" Style="width: 220px;" runat="server"></asp:TextBox>

                                            </div>
                                        </div>
                                    </td>

                                    <td width="50%">
                                        <div class="control-group" id="divddlpurpose" runat="server">
                                            <label class="control-label" for="typeahead">Purpose of Visit:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlpurpose" runat="server">
                                                    <asp:ListItem Text="" Value="NULL">--Select One--</asp:ListItem>
                                                    <asp:ListItem Text="Parents Meeting" Value="Parents Meeting"></asp:ListItem>
                                                    <asp:ListItem Text="Office Meeting" Value="Office Meeting"></asp:ListItem>
                                                    <asp:ListItem Text="Sports Meet" Value="Sports Meet"></asp:ListItem>
                                                    <asp:ListItem Text="Training" Value="Training"></asp:ListItem>
                                                    <asp:ListItem Text="Samagam" Value="Samagam"></asp:ListItem>
                                                    <asp:ListItem Text="Annual Day Function" Value="Annual Day Function"></asp:ListItem>
                                                    <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator5"
                                                    ControlToValidate="ddlpurpose" ErrorMessage="Please select any Purpose Of Visit" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group" id="divddlntypeofvisitor" runat="server">
                                            <label class="control-label" for="typeahead">Employee Type:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlntypeofvisitor" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group" id="divfileUploadauthority" runat="server">
                                            <label class="control-label" for="typeahead">Application Of Room From Authority:</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fileUploadauthority" runat="server" />
                                                <a id="aAuthorityLetter" style="font-size: 13px;" target="_blank">Authority Letter</a>

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group" id="divtxtvehicle" runat="server">
                                            <label class="control-label" for="typeahead">Vehicle Number, if any? :</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtvehicle" CssClass="span6 typeahead" runat="server" Style="width: 220px;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group" id="divtxtnoofperson" runat="server">
                                            <label class="control-label" for="typeahead">Number of Person:</label>
                                            <div class="controls">

                                                <asp:TextBox ID="txtnoofperson" runat="server" CssClass="span6 typeahead" Style="width: 220px;"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Regxnoofprsn" runat="server" ControlToValidate="txtnoofperson" Font-Size="13px" ForeColor="Red" ErrorMessage="Please Enter only Numbers" ValidationExpression="\d+"></asp:RegularExpressionValidator>

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group" id="divddlroomservice" runat="server">
                                            <label class="control-label" for="typeahead">Room Rent:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlroomservice" runat="server">
                                                    <asp:ListItem Text="" Value="NULL">--Select One--</asp:ListItem>
                                                    <asp:ListItem Text="Paid By Trust" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Paid By employee" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group" id="divddlelectricitybill" runat="server">
                                            <label class="control-label" for="typeahead">Electricity Bill:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlelectricitybill" runat="server">
                                                    <asp:ListItem Text="" Value="NULL">--Select One--</asp:ListItem>
                                                    <asp:ListItem Text="Paid By Trust" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Paid By employee" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>

                                    <td width="50%">
                                        <div class="control-group" id="divtxtfirstDate" runat="server">
                                            <label class="control-label" for="typeahead">Time Period For Stay:</label>
                                            <div>
                                                <label class="control-label" for="typeahead">From:</label>
                                                <asp:TextBox runat="server" ID="txtfirstDate" CssClass="input-xlarge datepicker" Style="width: 220px; height: 20px;"></asp:TextBox>
                                            </div>
                                            <div>
                                                <label class="control-label" for="typeahead">To:</label>
                                                <asp:TextBox runat="server" ID="txtlastDate" CssClass="input-xlarge datepicker" Style="width: 220px; height: 20px;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group" id="divdrpNumberOfDays" runat="server">
                                            <label class="control-label" for="typeahead">Number of Days to stay:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="drpNumberOfDays" runat="server">
                                                    <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>

                                </tr>



                            </table>
                        </div>
                        <div class="form-actions">
                            <%--<input id="btnSave" value="Save" class="btn btn-primary" />--%>
                            <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn btn-primary"  OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" ValidationGroup="visitor" OnClick="btnSave_Click" />
                        </div>
                    </fieldset>
                </div>
            </div>
            <!--/span-->
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2 style="color: #cc3300;"><i class="icon-user"></i>Visitor Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <asp:DropDownList ID="ddltypeofvisitor" runat="server" OnSelectedIndexChanged="ddltypeofvisitor_SelectedIndexChanged"></asp:DropDownList>
                    <div id="divMatDetails" runat="server">
                        <div id="divVisitorDetails" runat="server">
                            <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <thead>
                                    <tr>
                                        <th style="color: #cc3300;">Visitor Name</th>
                                        <th style="color: #cc3300;">Room(s) Allocated</th>
                                        <th style="color: #cc3300;">Arrived On</th>
                                        <th style="color: #cc3300;">No Of Days To Stay</th>
                                        <th style="color: #cc3300;">Identity Proof</th>
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

        <div id="myModal" class="modal hide fade" style="display: none; width: 800px; height: 500px">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Visitor <span id="spnName"></span>| <span id="spanidentityfication"></span></h3>
             
            </div>
            <div class="modal-body">
                <iframe id="iframeDailog" style="width: 750px; height: 500px"></iframe>
            </div>
        </div>

        <div id="divRoomNumbers" class="modal hide fade" style="display: none; width: 800px; height: 500px">
            <div class="modal-header">
                <input id="btnclose" value="Ok" style="width: 40px; float: right;" class="btn btn-primary" data-dismiss="modal" />
                <h3>Rooms Allocation | <span id="spnBuildingName"></span></h3> 
                
            </div>
            <div class="modal-body">
                <div class="box-content" style="width: 800px; height: 500px">
                    <h4>Choose rooms by clicking the corresponding seat in the layout below:</h4>
                    <div style="float: left;">
                        <ul id="seatDescription">
                            <li style="background: url('img/available_seat_img.gif') no-repeat scroll 0 0 transparent;">Available Seat</li>
                            <li style="background: url('img/booked_seat_img.gif') no-repeat scroll 0 0 transparent;">Booked Seat</li>
                            <li style="background: url('img/selected_seat_img.gif') no-repeat scroll 0 0 transparent;">Selected Seat</li>
                        </ul>
                    </div>
                    <br />
                    <br />
                    <div id="holder" style="-moz-column-count: 8; -moz-column-gap: 5px; -webkit-column-count: 8; -webkit-column-gap: 5px; column-count: 8; column-gap: 5px">
                        <ul id="place">
                        </ul>
                    </div>
                    <%--<div style="clear: both; width: 100%">
                    <input type="button" id="btnShowNew" value="Show Selected Seats" />
                    <input type="button" id="btnShow" value="Show All" />
                </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

