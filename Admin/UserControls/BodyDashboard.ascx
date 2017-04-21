<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyDashboard.ascx.cs" Inherits="Admin_UserControls_Dashboard" %>

<style>
    a.checkme[href='http://canvasjs.com/']
    {
        display: none;
    }
</style>
<asp:HiddenField ID="hdnUserType" runat="server" />
<table>
    <tr id="trAdmin" style="display:none;">
        <td>
            <div id="chartContainer" style="height: 300px; width: 300px;"></div>
        </td>
        <td>
            <div id="divDrawingChart" style="height: 300px; width: 300px;"></div>
        </td>
        <td>
            <div id="divBillsChart" style="height: 300px; width: 300px;"></div>
        </td>
    </tr>
    <tr id="trPurchase" style="display:none;">
        <td>
             <div id="divRateNonApprovedChart" style="height: 300px; width: 300px;"></div>
        </td>
    </tr>
    
</table>



