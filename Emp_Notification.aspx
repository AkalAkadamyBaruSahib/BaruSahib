<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_Notification.aspx.cs" Inherits="Emp_Notification" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div class="box span5" style="width:80%">
					<div class="box-header well">
						<h2><i class="icon-bullhorn"></i> Notification</h2>
						<div class="box-icon">
							
							<%--<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>--%>
						</div>
					</div>
					<div class="box-content alerts">
                        <div  runat="server" id="divNotification">
                            
                        </div>
                   <a href="Emp_Notification.aspx?Notification=All">See More..</a>
					</div>	
				</div>
</asp:Content>
