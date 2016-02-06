<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_MaterialVarify.aspx.cs" Inherits="Admin_MaterialVarify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content" class="span10">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> New Material Created By Users</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        
						<div id="divUnitByUser" runat="server" ></div>                
					</div>
				</div><!--/span-->
			
			</div>
        </div>
</asp:Content>

