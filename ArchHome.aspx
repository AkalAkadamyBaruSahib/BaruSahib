<%@ Page Title="" Language="C#" MasterPageFile="~/ArchMaster.master" AutoEventWireup="true" CodeFile="ArchHome.aspx.cs" Inherits="ArchHome" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    	<div id="content" class="span10">
			<!-- content starts -->
			

			<div>
			
                <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                <table width="100%" class="breadcrumb">
                    <tr>
                        <th width="20%"><img src="img/KalgidharTrust.png" /></th>
                    </tr>
                    <tr>
                        <th width="20%" class="breadcrumb"><h1>Summary<br />
                            Of<br />
                            Project Construction Management Details
                                                           </h1>
                        </th>
                    </tr>
                </table>
			</div>
           <div id="divZoneDetails" runat="server"></div>	
            
			</div>
		
           
            
                                                    
			
</asp:Content>