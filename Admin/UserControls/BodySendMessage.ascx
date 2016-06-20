<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodySendMessage.ascx.cs" Inherits="Admin_UserControls_BodySendMessage" %>


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
    <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
    <script type="text/javascript">

        // Load the Google Transliterate API
        google.load("elements", "1", { packages: "transliteration" });

        function onLoad() {
            var options = {
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage: [google.elements.transliteration.LanguageCode.PUNJABI],
                transliterationEnabled: true
            };
            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable([document.getElementById('<%= txtMessage.ClientID %>')]);
        }
        google.setOnLoadCallback(onLoad);
    </script>


    <div id="content" class="span10">

        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Send Message To Security Employee</h2>
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
                            <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="security" />

                            <table>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <tr>
                                            <td style="width: 50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Zone:</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlZone" Width="200px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator9" ForeColor="Red" ControlToValidate="ddlZone" ErrorMessage="Please Select the Zone" />
                          
                                                    </div>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Academy:</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlAcademy" Width="200px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged"></asp:DropDownList><br />
                                                       <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="ddlAcademy" ErrorMessage="Please Select the Academy" />
                          
                                                    </div>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div class="control-group" id="divsecurityemp" runat="server">
                                                    <label class="control-label" for="typeahead">Security Employee:</label>
                                                    <div class="controls">
                                                        <asp:Label ID="txtRecipientNumber" Visible="false" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>

                                        </tr>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="Google-transliteration-Way2blogging">
                                            <tr>
                                                <td style="width: 52%">
                                                    <div class="control-group">
                                                        <label class="control-label" for="typeahead">Compose Message:</label>
                                                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Width="300px" Height="100px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="txtMessage" ErrorMessage="Please Enter the Message" />
                          
                                                        <%-- <cc1:Editor ID="txtMessage" runat="server" />--%>
                                                        <div class="controls">
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                            </div>
                                            <td>
                                                <textarea id="transliterateTextarea" style="width: 300px; height: 100px;"></textarea>
                                            </td>
                                        </tr>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </table>

                            <div class="form-actions">
                                <asp:Button ID="btnSave" runat="server" Text="Send" Height="30px" Width="90px" ValidationGroup="security" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnSave_Click" />
                            </div>
                        </fieldset>
                    </form>

                </div>
            </div>
            <!--/span-->
        </div>
    </div>
