<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="AdminHome" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="content" class="span10">
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
                                    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                    <table width="100%" class="breadcrumb">
                                        <tr>
                                            <th width="20%">
                                                <img src="img/KalgidharTrust.png" /></th>
                                        </tr>
                                        <tr>
                                            <th width="20%" class="breadcrumb">
                                                <h1>Summary<br />
                                                    Of<br />
                                                    Project Construction Management Details
                                                </h1>
                                            </th>
                                        </tr>
                                    </table>
                                </div>
                            </div>


                        </fieldset>
                    </form>

                </div>
            </div>
            <!--/span-->

        </div>
        <%-- <div class="sortable row-fluid">
				<a data-rel="tooltip" title="New Material By User" class="well span3 top-block" href="#" style="width:auto;">
					<div>Material</div>
					<span class="notification">6</span>
				</a>

				<a data-rel="tooltip" title="New Unit By User" class="well span3 top-block" href="#" style="width:auto;">
					<div>Unit</div>
					<span class="notification green">4</span>
				</a>

				<a data-rel="tooltip" title="New Drawing uploaded by User" class="well span3 top-block" href="#" style="width:auto;">
					<div>Drawings</div>
					<span class="notification yellow">34</span>
				</a>
				
				<a data-rel="tooltip" title="Bills Submit by Users" class="well span3 top-block" href="#" style="width:auto;">
					<div>Bills</div>
					<span class="notification red">12</span>
				</a>
                    <a data-rel="tooltip" title="New Messages" class="well span3 top-block" href="#" style="width:auto;">
					<div>New Messages</div>
					<span class="notification red">120</span>
				</a>
                    <a data-rel="tooltip" title="New Drawing uploaded by User" class="well span3 top-block" href="#" style="width:auto;">
					<div>Verification</div>
					<span class="notification yellow">34</span>
				</a>
			</div>--%>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Our Zones</h2>
                    <div class="box-icon">
                        <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">

                    <div id="divZone" runat="server"></div>

                </div>
            </div>
            <!--/span-->
        </div>
        <%--     <div class="modal hide fade" id="myModal">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal">×</button>
				<h3><b><asp:Label ID="lblZone" runat="server" ForeColor="Red"></asp:Label></b> Assign to Incharge</h3>
			</div>
			<div class="modal-body">
				<div class="controls">                      
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        
				        Department <br />
                        <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList><br />
                        Employee <br />
                        <asp:DropDownList ID="ddlEmpl" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEmpl_SelectedIndexChanged" ></asp:DropDownList> <asp:Label ID="Label1" Visible="false" runat="server" ></asp:Label>  <asp:Label ID="lblDesignation" runat="server" ></asp:Label><br />
                    </ContentTemplate>
                    </asp:UpdatePanel>
			    </div>
			</div>
			<div class="modal-footer">
                <asp:Button id="btnSave" Text="Assign Incharge" CssClass="btn btn-primary" runat="server" OnClick="btnSave_Click"/>
                <a href="Admin_Incharge.aspx" class="btn">Create Incharge</a>
				<a href="#" class="btn" data-dismiss="modal">Close</a>
				
			</div>
		</div>--%>
        <%--           <div class="modal hide fade" id="myModal">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal">×</button>
				<h3><b><asp:Label ID="lblZone" runat="server" ForeColor="Red"></asp:Label></b> Details</h3>
			</div>
			<div class="modal-body">
				<div class="controls">                      
                   Department:<asp:Label runat="server" ID="lblDept"></asp:Label><br />
                   Designation:<asp:Label runat="server" ID="lblDesig"></asp:Label><br />
                    Mobile:<asp:Label runat="server" ID="lblMob"></asp:Label><br />
			    </div>
			</div>
			<div class="modal-footer">
                <%--<asp:Button id="btnSave" Text="Assign Incharge" CssClass="btn btn-primary" runat="server" />
                <a href="Admin_Incharge.aspx" class="btn">Create Incharge</a>
				<a href="#" class="btn" data-dismiss="modal">Close</a>
				
			</div>
		</div>--%>
    </div>
</asp:Content>
