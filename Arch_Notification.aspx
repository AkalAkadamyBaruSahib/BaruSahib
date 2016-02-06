<%@ Page Title="" Language="C#" MasterPageFile="~/ArchMaster.master" AutoEventWireup="true" CodeFile="Arch_Notification.aspx.cs" Inherits="Arch_Notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div class="box span5" style="width:1270px">
					<div class="box-header well">
						<h2><i class="icon-bullhorn"></i> Notification</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content alerts">
                        <div  runat="server" id="divNotification">
                            We are working on it, this is reflect shortly......
                        </div>
					</div>	
				</div>
</asp:Content>

