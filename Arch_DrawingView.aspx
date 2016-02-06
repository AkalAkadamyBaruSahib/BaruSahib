<%@ Page Title="" Language="C#" MasterPageFile="~/ArchMaster.master" AutoEventWireup="true" CodeFile="Arch_DrawingView.aspx.cs" Inherits="Arch_DrawingView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
    <div id="content" class="span10">
      
				            <table width="100%">
                                <tr><td></td></tr>
                                <tr>
                                    <td align="center">Drawing Type: <asp:DropDownList ID="ddlDwgType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDwgType_SelectedIndexChanged"></asp:DropDownList></td>
                                    
                                </tr>
                                 <tr><td></td></tr>
                            </table>
                                                   
    <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> <asp:Label ID="lblMatName" runat="server" text="materialName"> Drawing Details</asp:Label></h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        <div id="divDrawingView" runat="server"></div>
						<div id="divAllDrawingView" runat="server"></div>
					</div>
				</div><!--/span-->
			
			</div>
        </div> 

                                                </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>
