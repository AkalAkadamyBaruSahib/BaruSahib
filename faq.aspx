<%@ Page Title="" Language="C#" MasterPageFile="~/Common.master" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="faq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id="content" class="span10">
         <div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-edit"></i> FAQs</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<div class="row-fluid">
                            <div runat="server" id="divFaqDetails"></div>
							  
					</div>
				</div><!--/span-->

			</div>
       
        </div>
</asp:Content>

