<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_VehicleSearch.aspx.cs" Inherits="Transport_VehicleSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/VehicleSearch.js"></script> 
    <script type="text/javascript">
        function openModelPopUp(number) {
            var url = "http://fleetvts.com/current_vehicle_location.php?locate_vehicle=" + number;
            $("#iframeVehicleLocation").attr("src", url);
            $("#divTransportGps").show();
            $("#divbtnapprved").hide();
            $("#divVehicleDetails").hide();
        }
    </script>
   
    <div id="content" class="span10">
        <asp:HiddenField ID="hdnInchargeID" runat="server" />
        <asp:HiddenField ID="hdnUserTypeID" runat="server" />
        <div id="divbtnapprved" class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-edit"></i>Vehicle Search</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="control-group">
                        <label class="control-label" for="typeahead">Vehicle Number</label>
                        <div class="controls">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                        <input id="txtVehicle" name="txtVehicle" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>


                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divTransportGps" style="width: 1100px; height: 520px; display: none;">
            <div class="box-header well" data-original-title>
                <asp:Button ID="btnClose" Text="x" runat="server" CssClass="btn btn-primary" Style="float: right;" />
                <h2><i>
                    <img src="img/abc.jpg" /></i>Vehicle Location</h2>

            </div>
            <div>
                <iframe id="iframeVehicleLocation" style="width: 1100px; height: 500px; border: 1.5pt inset; vertical-align: top;"></iframe>
            </div>
        </div>

        <div id="divVehicleDetails" runat="server"></div>

    </div>
</asp:Content>

