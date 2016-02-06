<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_RejectedBills.aspx.cs" Inherits="Emp_RejectedBills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<style type="text/css">
        a span {
            color: #000000;
        }
    </style>--%>
   
    <div id="content" class="span10">


        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>

        <div id="divAcademyDetails" runat="server"></div>
        <div class="modal hide fade" id="myModal">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal">×</button>
				<h3>Proceed Bill for Again Verification</h3>
			</div>
			<div class="modal-body">
                <asp:Label runat="server" Visible="false" ID="lblBillId"></asp:Label>
				Proceed Remark: <asp:TextBox runat="server" ID="txtProRemark" TextMode="MultiLine"></asp:TextBox>
			</div>
			<div class="modal-footer">
				<a href="#" class="btn" data-dismiss="modal">Close</a>
				<asp:LinkButton CssClass="btn btn-primary" runat="server" ID="lbProceed" OnClick="lbProceed_Click">Proceed Bill</asp:LinkButton>
			</div>
		</div>
    </div>
  </asp:Content>