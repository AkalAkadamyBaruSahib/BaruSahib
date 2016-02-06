<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_BillStatus.aspx.cs" Inherits="Admin_BillStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<style type="text/css">
        a span {
            color: #000000;
        }
    </style>--%>
   
    <div id="content" class="span10">


        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>

        <div id="divAcademyDetails" runat="server"></div>
    </div>


    
    <div class="modal hide fade" id="myModal">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3><b>
                <asp:Label ID="Label1" runat="server"></asp:Label></b> Reciving Details</h3>
        </div>
        <div class="modal-body">
            <div class="controls">
                <table align="Center">
                    <tr>
                        <td>Agency Name</td>
                        <td><asp:Label ID="lblAgency" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Recipt/ Voucher No.</td>
                        <td><asp:TextBox runat="server" ID="txtRecipTNo"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Date Of Recieving</td>
                        <td><asp:TextBox runat="server" ID="txtDateOfRec" Width="150Px" CssClass="input-xlarge datepicker"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Remark</td>
                        <td><asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="modal-footer">
            <a href="#" class="btn" data-dismiss="modal">Close</a>
            <asp:Button ID="Button1" Text="Submit Details" CssClass="btn btn-primary" runat="server" OnClick="Button1_Click" />
            <%--<asp:LinkButton runat="server" class='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>
            <a class='btn btn-setting btn-round' href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>--%>
        </div>
    </div>
</asp:Content>

