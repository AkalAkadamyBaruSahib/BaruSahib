﻿<%@ Page Title="Akal Academy | Visitor Record" Language="C#" MasterPageFile="~/Visitor_AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Visitor_AddNew.aspx.cs" Inherits="Visitor_AddNew" %>

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

        function PurposeOnChange(control) {
            if (control.value == "Parents Meeting") {
                $("#divAdminsnNo").show();
                $("#divPurposeVisit").hide();
            }
            else if (control.value == "Others") {
                $("#divAdminsnNo").hide();
                $("#divPurposeVisit").show();
            }
            else {
                $("#divAdminsnNo").hide();
                $("#divPurposeVisit").hide();
            }
        }

        function validateFileSize(controlID) {
            var id = controlID.files[0].size;
            if (id > 900000) {
                $('#dvMsg').show();
                controlID.value ='';
                return false;
            }
            else {
                $('#dvMsg').hide();
                return true;
            }
        }

    </script>
    <style>
        #holder
        {
            height: 99%;
            width: 99%;
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
                font-size: 1.0em;
                margin-left: -25px;
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
                width: 50px;
                margin-left: 20px;
            }

            #place .selectedSeat
            {
                background: url("img/booked_seat_img.gif") no-repeat scroll 0 0 transparent;
                height: 50px;
                width: 50px;
                margin-left: 20px;
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

        #place .selectedSeatID
        {
            background: url("img/booked_seat_img.gif") no-repeat scroll 0 0 transparent;
            height: 50px;
            width: 50px;
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
          <asp:HiddenField ID="hdnVID" runat="server" />
        <asp:HiddenField ID="hdnVisitorType" runat="server" />
        <asp:HiddenField ID="hdnUserType" runat="server" />
        <div class="row-fluid sortable" runat="server" id="divAllotment">
            <div class="box span12">
                <div class="box-header well" data-original-title>

                    <h2><i class="icon-user"></i>
                        <asp:Label ID="lblTag" runat="server" Text="" ForeColor="#cc3300"></asp:Label>
                        <label id="lblStudentName" style="color: #cc3300; font-weight: 600; font-size: 20px"></label>
                    </h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="visitor" />
               <div id="dvMsg" style="color: Red; width: 250px; display: none;">Maximum size allowed Less than is 900 KB </div>
                       
                <%-- <div class="well bs-component form-horizontal">--%>
                <div class="well bs-component">
                    <fieldset>
                        <legend>Personal Information
                            <div>
                                <label class="control-label" for="typeahead">Fill data through Admission Number:</label>
                                <input type="checkbox" id="chkAdminsnNumber" />
                            </div>
                        </legend>
                        <div class="box-content">
                            <div id="trStudentSearch" style="display: none;">
                                <table>
                                    <tr>
                                        <td>
                                            <label class="control-label" for="typeahead">Admission Number:</label>
                                            <input type="text" id="txtAdmisnNo" class="span6 typeahead" name="txtAdmisnNo" maxlength="8" style="width: 200px" />
                                        </td>
                                        <td>
                                            <input type="button" id="btnSearch" value="Search" class="btn btn-primary" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divVisitorInfo" runat="server" visible="false">
                                <table id="tabledata" width="100%" border="0">
                                    <tr>
                                        <td style="width: 30%">
                                            <label class="control-label" for="typeahead">Full Name:</label>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="span6 typeahead" Style="width: 220px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="reqName"
                                                ControlToValidate="txtName" ErrorMessage="Please enter the Full Name Of Visitor" />
                                        </td>
                                        <td style="width: 30%">
                                            <label class="control-label" for="typeahead">Visitor Contact Number:</label>
                                            <asp:TextBox ID="txtContactNumber" CssClass="span6 typeahead" runat="server" Style="width: 220px;"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="regxnumbervalidator" ValidationGroup="visitor" runat="server" ControlToValidate="txtContactNumber" ForeColor="Red" Font-Size="13px" ErrorMessage="Please Enter only at least 10 Digit Numbers In Contact Number" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator3" ControlToValidate="txtContactNumber" ErrorMessage="Please enter the Visitor Contact Number" />

                                        </td>
                                        <td style="width: 40%">
                                            <label class="control-label" for="typeahead">Address:</label>
                                            <asp:TextBox ID="txtAddress" CssClass="span6 typeahead" TextMode="MultiLine" runat="server" Style="width: 220px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor"
                                                ID="RequiredFieldValidator2" ControlToValidate="txtAddress"
                                                ErrorMessage="Please enter Visitor Address" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ValidationGroup="visitor" Display="Dynamic" ControlToValidate="txtAddress" ErrorMessage="Special Character are Invalid!!!" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9 #,&()-.:/]*$"></asp:RegularExpressionValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>

                                            <label class="control-label" for="typeahead">Country:</label>

                                            <asp:DropDownList ID="drpCountry" CssClass="span6 typeahead" runat="server" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select Country</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator1"
                                                ControlToValidate="drpCountry" InitialValue="0" ErrorMessage="Please select the Country" />

                                        </td>
                                        <td>
                                            <label class="control-label" for="typeahead">State:</label>

                                            <asp:DropDownList ID="drpState" CssClass="span6 typeahead" AutoPostBack="true" OnSelectedIndexChanged="drpState_SelectedIndexChanged" runat="server">
                                                <asp:ListItem Value="0">Select State</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator4"
                                                ControlToValidate="drpState" InitialValue="0" ErrorMessage="Please select the State" />

                                        </td>
                                        <td>

                                            <label class="control-label" for="typeahead">City:</label>

                                            <asp:DropDownList ID="drpCity" CssClass="span6 typeahead" runat="server">
                                                <asp:ListItem Value="0">Select City</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator6"
                                                ControlToValidate="drpCity" InitialValue="0" ErrorMessage="Please select the City" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="span6 typeahead">
                                                <label class="control-label" for="typeahead">Select Building Name with Room Number:</label>
                                                <a id="aRoomNumber" style="font-size: 13px;" href="#">Allocated Rooms: </a>
                                                <div id="selectdiv">
                                                    <select id="drpbuilding">
                                                        <option></option>
                                                    </select>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="span6 typeahead">
                                                <label class="control-label" for="typeahead">Upload Photo:</label>

                                                <asp:FileUpload ID="fileUploadphoto" runat="server" onchange="validateFileSize(this);" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorphoto" ControlToValidate="fileUploadphoto" runat="server" ValidationGroup="visitor" Display="None" ErrorMessage="Please Upload the Photo"></asp:RequiredFieldValidator>
                                                <a id="aPhoto" style="font-size: 13px;" target="_blank">Photo</a>
                                            </div>
                                        </td>
                                        <td>
                                            <label class="control-label" for="typeahead">Identity proof type:</label>
                                            <asp:DropDownList ID="drpProofType" CssClass="span6 typeahead" runat="server">
                                                <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                                <asp:ListItem Text="Licence" Value="Licence"></asp:ListItem>
                                                <asp:ListItem Text="Voter Card" Value="Voter Card"></asp:ListItem>
                                                <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                                <asp:ListItem Text="Adhaar Card" Value="Adhaar Card"></asp:ListItem>
                                                <asp:ListItem Text="Identity Card (Any)" Value="Identity Card (Any)"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="span6 typeahead">
                                                <label class="control-label" for="typeahead">Upload Identity Proof:</label>
                                                <asp:FileUpload ID="fileUploadIdentity" runat="server" onchange="validateFileSize(this);" />
                                                <a id="aIdentityProof" style="font-size: 13px;" target="_blank">Visitor Proof</a>
                                                                                
                                            </div>
                                        </td>
                                        <td>
                                            <label class="control-label" for="typeahead">If No Identity proof, Any Reference:</label>
                                            <asp:TextBox ID="txtReference" CssClass="span6 typeahead" Style="width: 220px;" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <div class="span6 typeahead">
                                                <label class="control-label" for="typeahead">Application Of Room From Authority:</label>
                                                <asp:FileUpload ID="fileUploadauthority" CssClass="span6 typeahead" runat="server" onchange="validateFileSize(this);" />
                                                <a id="aAuthorityLetter" style="font-size: 13px;" target="_blank">Authority Letter</a>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="control-group" id="divtxtnoofperson" runat="server">
                                                <label class="control-label" for="typeahead">Number of Person:</label>
                                                <div class="controls">
                                                    <div>
                                                        <label class="control-label" for="typeahead">Men:</label>
                                                        <asp:TextBox ID="txtnoofperson" runat="server" CssClass="span6 typeahead" Style="width: 50px;"></asp:TextBox>
                                                    </div>
                                                    <div style="margin-left: 73px; margin-top: -56px;">
                                                        <label class="control-label" for="typeahead">Women:</label>
                                                        <asp:TextBox ID="txtNoOfFemale" runat="server" CssClass="span6 typeahead" Style="width: 50px;"></asp:TextBox>
                                                    </div>
                                                    <div style="margin-left: 150px; margin-top: -56px;">
                                                        <label class="control-label" for="typeahead">Children:</label>
                                                        <asp:TextBox ID="txtNoOfChildren" runat="server" CssClass="span6 typeahead" Style="width: 50px;"></asp:TextBox>
                                                    </div>
                                                    <asp:RegularExpressionValidator ID="Regxnoofprsn" runat="server" ControlToValidate="txtnoofperson" Font-Size="13px" ForeColor="Red" ErrorMessage="Please Enter only Numbers" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="span6 typeahead">
                                                <label class="control-label" for="typeahead">Langer Sewa:</label>
                                                <asp:DropDownList ID="ddlRoomRent" runat="server">
                                                    <asp:ListItem Text="" Value="-1">--Select One--</asp:ListItem>
                                                    <asp:ListItem Text="Free" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td>

                                            <label class="control-label" for="typeahead">Vehicle Number, if any? :</label>

                                            <asp:TextBox ID="txtvehicle" CssClass="span6 typeahead" runat="server" Style="width: 220px;"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label" for="typeahead">Number of Days to stay:</label>
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
                                        </td>
                                        <td>
                                            <div class="span6 typeahead">
                                                <label class="control-label" for="typeahead">Purpose of Visit:</label>
                                                <asp:DropDownList ID="ddlpurpose" runat="server" onchange="PurposeOnChange(this);">
                                                    <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                                    <asp:ListItem Text="Annual Day Function" Value="Annual Day Function"></asp:ListItem>
                                                    <asp:ListItem Text="Darshan Mela" Value="Darshan Mela"></asp:ListItem>
                                                    <asp:ListItem Text="New Admission" Value="New Admission"></asp:ListItem>
                                                    <asp:ListItem Text="Office Meeting" Value="Office Meeting"></asp:ListItem>
                                                    <asp:ListItem Text="Parents Meeting" Value="Parents Meeting"></asp:ListItem>
                                                    <asp:ListItem Text="Samagam" Value="Samagam"></asp:ListItem>
                                                    <asp:ListItem Text="Sports Meet" Value="Sports Meet"></asp:ListItem>
                                                    <asp:ListItem Text="Training" Value="Training"></asp:ListItem>
                                                    <asp:ListItem Text="Night Stay" Value="Night Stay"></asp:ListItem>
                                                    <asp:ListItem Text="Interview" Value="Interview"></asp:ListItem>
                                                    <asp:ListItem Text="Exam" Value="Exam"></asp:ListItem>
                                                    <asp:ListItem Text="Hospital" Value="Hospital"></asp:ListItem>
                                                    <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator5"
                                                    ControlToValidate="ddlpurpose" ErrorMessage="Please select any Purpose Of Visit" />
                                            </div>
                                        </td>
                                        <td>
                                            <div class="span6 typeahead">
                                                <label class="control-label" cssclass="span6 typeahead" for="typeahead">Time Period For Stay:</label><br />
                                                <label class="control-label" for="typeahead">From:</label>
                                                <asp:TextBox runat="server" ID="txtfirstDate" CssClass="input-xlarge datepicker" Style="width: 70px;"></asp:TextBox>
                                                <label class="control-label" for="typeahead">To:</label>
                                                <asp:TextBox runat="server" ID="txtlastDate" CssClass="input-xlarge datepicker" Style="width: 70px;"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="regPurchaseDate" runat="server" ErrorMessage="Invalid Format.Use(MM/DD/YYYY)." ForeColor="Red" ControlToValidate="txtfirstDate" SetFocusOnError="true" ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Format.Use(MM/DD/YYYY)." ForeColor="Red" ControlToValidate="txtlastDate" SetFocusOnError="true" ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator14" ControlToValidate="txtfirstDate" ErrorMessage="Please enter the CheckIn Date" />
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator15" ControlToValidate="txtlastDate" ErrorMessage="Please enter the CheckOut Date" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="control-group" id="divPurposeVisit" style="display: none; margin-top: -5px;">
                                                <label class="control-label" for="typeahead">Purpose Of Vist:</label>
                                                <div class="control-group">
                                                    <asp:TextBox ID="txtPurposevisit" CssClass="span6 typeahead" runat="server" Style="width: 220px;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="control-group" id="divAdminsnNo" style="display: none; margin-top: -5px;">
                                                <label class="control-label" for="typeahead">Admission Number:</label>
                                                <div class="control-group">
                                                    <asp:TextBox ID="txtAdmissionNo" CssClass="span6 typeahead" runat="server" Style="width: 220px;"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4"  runat="server" ControlToValidate="txtAdmissionNo" ForeColor="Red" Font-Size="13px" ErrorMessage="Please Enter Digit Numbers" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                             </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divPrmanent" runat="server" visible="false">
                                <table id="tablePrmanent" width="100%" border="0">
                                    <tr>
                                        <td style="width: 30%;">
                                            <label class="control-label" for="typeahead">Full Name:</label>
                                            <asp:TextBox ID="txtPrmntName" runat="server" CssClass="span6 typeahead" Style="width: 220px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator7" ControlToValidate="txtPrmntName" ErrorMessage="Please enter the Full Name Of Pemranent Employee" />
                                        </td>
                                        <td style="width: 30%;">
                                            <label class="control-label" for="typeahead">Visitor Contact Number:</label>
                                            <asp:TextBox ID="txtPrmntContactNo" CssClass="span6 typeahead" runat="server" Style="width: 220px;"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPrmntContactNo" ForeColor="Red" Font-Size="13px" ErrorMessage="Please Enter only at least 10 Digit Numbers" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator8" ControlToValidate="txtContactNumber" ErrorMessage="Please enter the Visitor Contact Number" />
                                        </td>
                                        <td style="width: 40%;">
                                            <label class="control-label" for="typeahead">Address:</label>
                                            <asp:TextBox ID="txtPrmntAddress" CssClass="span6 typeahead" TextMode="MultiLine" runat="server" Style="width: 220px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator9" ControlToValidate="txtPrmntAddress" ErrorMessage="Please enter Pemananet Employee Address" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="visitor" Display="Dynamic" ControlToValidate="txtPrmntAddress" ErrorMessage="Special Character are Invalid!!!" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9 #,&()-.:]*$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label" for="typeahead">Country:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlPrmntCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPrmntCountry_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Country</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator10" ControlToValidate="ddlPrmntCountry" InitialValue="0" ErrorMessage="Please select the Country" />
                                            </div>
                                        </td>
                                        <td>
                                            <label class="control-label" for="typeahead">State:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlPrmntState" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPrmntState_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select State</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator11" ControlToValidate="ddlPrmntState" InitialValue="0" ErrorMessage="Please select the State" />
                                            </div>
                                        </td>
                                        <td>
                                            <label class="control-label" for="typeahead">City:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlPrmntCity" runat="server">
                                                    <asp:ListItem Value="0">Select City</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="visitor" ID="RequiredFieldValidator12" ControlToValidate="ddlPrmntCity" InitialValue="0" ErrorMessage="Please select the City" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead">Select Building Name with Room Number:</label>
                                                <div class="controls">
                                                    <a id="aPrmnentRoomNumber" style="font-size: 13px;" href="#">Allocated Rooms: </a>
                                                    <div id="divPrmntBuilding">
                                                        <select id="drpPrmntBuilding">
                                                            <option></option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="control-group" id="div2" runat="server">
                                                <label class="control-label" for="typeahead">Upload Photo:</label>
                                                <div class="controls">
                                                    <asp:FileUpload ID="fileUploadPrmntPhoto" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="fileUploadPrmntPhoto" runat="server" ValidationGroup="visitor" Display="None" ErrorMessage="Please Upload the Photo"></asp:RequiredFieldValidator>
                                                    <a id="aPrmntPhoto" style="font-size: 13px;" target="_blank">Photo</a>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead">Identity proof type:</label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlPrmntIdntity" runat="server">
                                                        <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                                        <asp:ListItem Text="Licence" Value="Licence"></asp:ListItem>
                                                        <asp:ListItem Text="Voter Card" Value="Voter Card"></asp:ListItem>
                                                        <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                                        <asp:ListItem Text="Adhaar Card" Value="Adhaar Card"></asp:ListItem>
                                                        <asp:ListItem Text="Identity Card (Any)" Value="Identity Card (Any)"></asp:ListItem>
                                                    </asp:DropDownList>
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
                                        <td>
                                            <div class="control-group" id="divddlelectricitybill" runat="server">
                                                <label class="control-label" for="typeahead">Electricity Bill Type:</label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlelectricitybill" runat="server">
                                                        <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                                        <asp:ListItem Text="Paid By Trust" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Paid By employee" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <label class="control-label" for="typeahead">Application Of Room From Authority:</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fileUploadPrmntAuthority" runat="server" />
                                                <a id="aPrmntAuthority" style="font-size: 13px;" target="_blank">Authority Letter</a>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead">Upload Identity Proof:</label>
                                                <div class="controls">
                                                    <asp:FileUpload ID="fileUploadPrmntProof" runat="server" />
                                                    <a id="aPrmntProof" style="font-size: 13px;" target="_blank">Visitor Proof</a>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="control-group" id="divddlroomservice" runat="server">
                                                <label class="control-label" for="typeahead">Room Rent Type:</label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlroomservice" runat="server">
                                                        <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                                        <asp:ListItem Text="Paid By Trust" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Paid By employee" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <label class="control-label" for="typeahead">Time Period For Stay:</label><br />
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead">From:</label>
                                                <asp:TextBox runat="server" ID="txtprmntTo" CssClass="input-xlarge datepicker" Style="width: 70px; height: 20px;"></asp:TextBox>
                                               <%-- <label class="control-label" for="typeahead">To:</label>
                                                <asp:TextBox runat="server" ID="txtprmntFrom" CssClass="input-xlarge datepicker" Style="width: 70px; height: 20px;"></asp:TextBox>--%>
                                                <asp:RegularExpressionValidator ID="RegularExpressiontxtprmntTo" runat="server" ErrorMessage="Invalid Format.Use(MM/DD/YYYY)." ForeColor="Red" ControlToValidate="txtprmntTo" SetFocusOnError="true" ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"></asp:RegularExpressionValidator>
                                                <%--<asp:RegularExpressionValidator ID="RegularExpressiontxtprmntFrom" runat="server" ErrorMessage="Invalid Format.Use(MM/DD/YYYY)." ForeColor="Red" ControlToValidate="txtprmntFrom" SetFocusOnError="true" ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"></asp:RegularExpressionValidator>--%>
                                            </div>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </div>
                        <div class="form-actions">
                            <%--<input id="btnSave" value="Save" class="btn btn-primary" />--%>
                            <asp:Button ID="btnSave" Text="Save & Print" runat="server" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" ValidationGroup="visitor" OnClick="btnSave_Click" />
                        </div>
                    </fieldset>
                </div>
            </div>
            <!--/span-->
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

        <div id="divRoomNumbers" class="modalFullScreen hide fade" style="display: none; width: 90%; height: 600px">
            <div class="modal-header">
                <input id="btnclose" value="Ok" style="width: 40px; float: right;" class="btn btn-primary" data-dismiss="modal" />
                <h3>Rooms Allocation | <span id="spnBuildingName"></span></h3>
            </div>
            <div style="width: 100%">
                <div class="box-content" style="width: 99%; height: 99%">
                    <h4>Choose rooms by clicking the corresponding seat in the layout below:</h4>
                    <div style="float: left;">
                        <ul id="seatDescription">
                            <li style="background: url('img/available_seat_img.gif') no-repeat scroll 0 0 transparent;">&nbsp;&nbsp;&nbsp;Available Seat</li>
                            <li style="background: url('img/booked_seat_img.gif') no-repeat scroll 0 0 transparent;">&nbsp;&nbsp;&nbsp;Booked Seat</li>
                            <li style="background: url('img/selected_seat_img.gif') no-repeat scroll 0 0 transparent;">&nbsp;&nbsp;&nbsp;Selected Seat</li>
                        </ul>
                    </div>
                    <div id="holder" style="width: 99%; -moz-column-count: 8; -moz-column-gap: 5px; -webkit-column-count: 8; -webkit-column-gap: 5px; column-count: 8; column-gap: 5px">
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
       <div id="pnlHtml" runat="server"></div>
</asp:Content>

