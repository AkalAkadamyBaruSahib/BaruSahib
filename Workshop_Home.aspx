<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_Home.aspx.cs" Inherits="Workshop_Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    	<div id="content" class="span10">
			<!-- content starts -->
			
<asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>

			<div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						    <marquee behavior='scroll' direction='left'>WELCOME  TO AKALSEWA SOFTWARE</marquee>
					</div>
					<div class="box-content">
						<form class="form-horizontal">
						  <fieldset>
							
							<div class="control-group">
							 
							  <div class="controls">
								<%--<input type="text" class="span6 typeahead" id="typeahead"  data-provide="typeahead" data-items="4" data-source='["Alabama","Alaska","Arizona","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky","Louisiana","Maine","Maryland","Massachusetts","Michigan","Minnesota","Mississippi","Missouri","Montana","Nebraska","Nevada","New Hampshire","New Jersey","New Mexico","New York","North Dakota","North Carolina","Ohio","Oklahoma","Oregon","Pennsylvania","Rhode Island","South Carolina","South Dakota","Tennessee","Texas","Utah","Vermont","Virginia","Washington","West Virginia","Wisconsin","Wyoming"]'>--%>
                                  <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
								
							  </div>
							</div>
                              <div class="control-group">
							                   
							                    <div class="controls">
                                                    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                <table width="100%" class="breadcrumb">
                    <tr>
                        <th width="20%"><img src="img/KalgidharTrust.png" /></th>
                    </tr>
                    <tr>
                        <th width="20%" class="breadcrumb"><h1>Summary<br />
                            Of<br />
                            Project Workshop Management Details
                                                           </h1>
                        </th>
                    </tr>
                </table>
							                    </div>
                                            </div>
							
							
						  </fieldset>
						</form>   

					</div>
				</div><!--/span-->

			</div>
            <div class="row-fluid">
				<div class="box span12">
					<div class="box-header well">
						<h2><i class="icon-info-sign"></i> Introduction</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<h1>Welcome to Akal Academy <%--<small>sssss</small>--%></h1>
						<p>Its internal erp of <a href="http://barusahib.org/">Kalgidhar Trust - Baru Sahib</a>...................................................................................................................................</p>
						<p><b>In This organisation you'r as <asp:Label runat="server" ID="lblDesignation" Text="Workshop Supervisor" ForeColor="Blue"></asp:Label>, and we were assign you these location <asp:Label ID="lblLocation" runat="server" Text="zone name" ForeColor="Violet"></asp:Label></b></p>
						
						<p class="center">
							<%--<a href="http://usman.it/free-responsive-admin-template" class="btn btn-large btn-primary"><i class="icon-chevron-left icon-white"></i> Back to article</a>--%> 
							<a href="#" class="btn btn-large"><i class="icon-download-alt"></i> Download manual </a>
						</p>
						<div class="clearfix"></div>
					</div>
				</div>
			</div>
            <div class="row-fluid sortable">	
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2>Our Zones</h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        <div id="divZone" runat="server"></div>
						  
					</div>
				</div><!--/span-->
			</div>
			

		  
       
					<!-- content ends -->
           
            
                                                    
			</div>
</asp:Content>
