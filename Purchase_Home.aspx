<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" Async="true" AutoEventWireup="true" CodeFile="Purchase_Home.aspx.cs" Inherits="Purchase_Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="span10">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <asp:HiddenField ID="hbnInchargeID" runat="server" />

        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <marquee behavior='scroll' direction='left'>WELCOME  TO AKALSEWA SOFTWARE</marquee>
                </div>
                <div class="box-content">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div style="text-align: center;">
                                    <span id="spnPendingItem" runat="server" style="font-weight: bolder; font-size: 20px;"><u>Pending Items</u></span>
                                    <span id="spnNotAssigned" runat="server" style="font-weight: bolder; font-size: 20px;"><u>Un-Assign Items</u></span>
                                    <div runat="server" id="divPendingItem" style="width: 100%; height: 400px; overflow-y: auto"></div>
                                </div>
                            </td>
                            <td style="width: 5%"></td>
                            <td>
                                <div style="text-align: center;">
                                     <span id="spnNewRate" runat="server" visible="false" style="font-weight: bolder; font-size: 20px;"><u>New Rate</u></span>
                                    <span id="spnRateAproved" runat="server" visible="false" style="font-weight: bolder; font-size: 20px;"><u>Recent Rate Approved</u></span>
                                    <div runat="server" id="divRecentRateApproved" style="width: 100%; height: 200px; overflow-y: auto"></div>
                                </div>
                                <br />
                                <div id="divPurchaserPending" style="text-align: center; height:10%;" runat="server">
                                    <span id="spnPurchaserPendingItem" runat="server" visible="false" style="font-weight: bolder; font-size: 20px;"><u>Purchaser Pending Items</u></span>
                                    <div runat="server" id="divPurchaserPendingItems" style="width: 100%; height: 200px; overflow-y: auto"></div>
                                </div>

                            </td>
                        </tr>

                    </table>

                </div>
            </div>
            <!--/span-->

        </div>

    </div>
</asp:Content>
