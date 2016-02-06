<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailTemplate.ascx.cs" Inherits="Admin_UserControls_EmailTemplate" %>

<asp:HiddenField ID="hdnfiles" runat="server" />

<div id="progressBar" style="display:none">
        <div style="text-align: center">
            <img src="img/animated.gif" />
        </div>
</div>
<table style="width:600px" border="0">
    <tr>
        <td>To:</td>
        <td><input id="txtTo" name="txtTo" type="email" required="" /></td>
    </tr>
    <tr>
        <td>Subject:</td>
        <td><input id="txtSubject" name="txtSubject" type="text" required="" /></td>

    </tr>
    <tr>
        <td>Body:</td>
        <td><textarea id="txtBody" rows="10" style="width: 366px; height: 188px;" cols="100" name="txtBody" required></textarea>
            </td>
    </tr>
    <tr>
        <td style="text-align:center" colspan="2" ><input ID="btnSend" name="btnSend" class="btn btn-primary" type="submit" value="Send" /> <input ID="btnCancle" class="btn btn-primary" type="button" value="Cancle" /> </td>
    </tr>
</table>

