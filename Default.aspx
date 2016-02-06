<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html lang="en">
<head>
	
	<meta charset="utf-8">
	<title>Akal Academy</title>
	

	<!-- The styles -->
	<link id="bs-css" href="css/bootstrap-united.css" rel="stylesheet">
	<style type="text/css">
	  body {
		padding-bottom: 40px;
	  }
	  .sidebar-nav {
		padding: 9px 0;
	  }
	    .auto-style1 {
            color: #FF0000;
            font-size: xx-large;
        }
	</style>
	<link href="css/bootstrap-responsive.css" rel="stylesheet">
	<link href="css/charisma-app.css" rel="stylesheet">
	<link href="css/jquery-ui-1.8.21.custom.css" rel="stylesheet">
	<link href='css/fullcalendar.css' rel='stylesheet'>
	<link href='css/fullcalendar.print.css' rel='stylesheet'  media='print'>
	<link href='css/chosen.css' rel='stylesheet'>
	<link href='css/uniform.default.css' rel='stylesheet'>
	<link href='css/colorbox.css' rel='stylesheet'>
	<link href='css/jquery.cleditor.css' rel='stylesheet'>
	<link href='css/jquery.noty.css' rel='stylesheet'>
	<link href='css/noty_theme_default.css' rel='stylesheet'>
	<link href='css/elfinder.min.css' rel='stylesheet'>
	<link href='css/elfinder.theme.css' rel='stylesheet'>
	<link href='css/jquery.iphone.toggle.css' rel='stylesheet'>
	<link href='css/opa-icons.css' rel='stylesheet'>
	<link href='css/uploadify.css' rel='stylesheet'>

	<!-- The HTML5 shim, for IE6-8 support of HTML5 elements -->
	<!--[if lt IE 9]>
	  <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->

	<!-- The fav icon -->
	<link rel="shortcut icon" href="img/favicon.ico">
		
   <%-- slider--%>
    <link rel="stylesheet" href="themes/default/default.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="SliderTheme/SliderJs/nivo-slider.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="style.css" type="text/css" media="screen" />
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
    <script type="text/javascript" src="SliderTheme/SliderJs/jquery.nivo.slider.js"></script>
    <script type="text/javascript">

        //<!--  Load the slider  --> 
        $(window).load(function () {
            $('#slider').nivoSlider();
        });

    </script>
</head>

<body>
		<div class="container-fluid">
		<div class="row-fluid">
		
			<div class="row-fluid">
				
			</div><!--/row-->
			
			<div class="row-fluid">
                <div>
				
                <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                <table width="100%" class="breadcrumb">
                    <tr>
                        <th width="20%" class="auto-style1"><img src="img/KalgidharTrust.png"/>
                        </th>
                    </tr>
                    <tr>
                        
                        <th width="20%" class="breadcrumb"><h1>Kalgidhar Management System</h1>
                        </th>
                    </tr>
                    
                </table>
			</div>
                <div class="row-fluid">
                    <table width="100%">
                        <tr>
                            <td width="50%">
                                <div style="width: 950px; height: 500px; background-color: #000; overflow: hidden; ">
                                            <div class="slider-wrapper theme-default">
                                            <div id="slider" class="nivoSlider">
            
                                                <!--  Images to slide through.  -->
                                                <img src="SliderTheme/SliderImages/24Jaswinder01Sng.jpg" data-thumb="images/24Jaswinder01Sng.jpg" alt="" />
                                                <img src="SliderTheme/SliderImages/akal.jpg" data-thumb="images/akal.jpg" alt="" title="This is an example of a caption" />
                                                <img src="SliderTheme/SliderImages/DSC_0389.JPG" data-thumb="images/DSC0389.JPG" alt="" data-transition="slideInLeft" />
                                                <img src="SliderTheme/SliderImages/PIX.JPG" data-thumb="images/PIX.JPG" alt="" title="#htmlcaption" />

                                                <img src="SliderTheme/SliderImages/DSC_0392.jpg" data-thumb="images/DSC_0392.jpg" alt="" />
                                                <img src="SliderTheme/SliderImages/DSC_0403.jpg" data-thumb="images/DSC_0403.jpg" alt="" title="This is an example of a caption" />
                                                <img src="SliderTheme/SliderImages/DSC_0167.JPG" data-thumb="images/walle.jpg" alt="" data-transition="slideInLeft" />
                                                <img src="SliderTheme/SliderImages/DSC_0185.JPG" data-thumb="images/nemo.jpg" alt="" title="#htmlcaption" />
                                                <img src="SliderTheme/SliderImages/Trans1.jpg" data-thumb="images/Trans1.jpg" alt="" />
                                                <img src="SliderTheme/SliderImages/DSC_3850.JPG" data-thumb="images/up.jpg" alt="" title="This is an example of a caption" />
                                                <img src="SliderTheme/SliderImages/DSC_3860.JPG" data-thumb="images/walle.jpg" alt="" data-transition="slideInLeft" />
                                                <img src="SliderTheme/SliderImages/Image00025.jpg" data-thumb="images/nemo.jpg" alt="" title="#htmlcaption" />
                                                <div class="nivo-directionNav">
                                                        <a class="nivo-prevNav">Prev</a>
                                                        <a class="nivo-nextNav">Next</a>
                                                </div>
                                            </div>
            
                                            <!--  Captions to show for images  -->
                                            <div id="htmlcaption" class="nivo-html-caption">
                                                <strong>This</strong> is an example of a <em>HTML</em> caption with <a href="#">a link</a>. 
                                            </div>
                                            </div>

                                </div>
                             

                            </td>
                            <td align="right">

                                <div class="well span5 center login-box" style="width:300px; height:376px;margin-top:-96px;margin-left:100px;">
					<div class="alert alert-info">
						<h1>LOGIN</h1>
					</div>
					<form id="Form1" class="form-horizontal" runat="server">
						<fieldset>
                         
							<div class="clearfix"></div>
							<div class="input-prepend" title="Username" data-rel="tooltip">
                                User Name<br />
								<span class="add-on"><i class="icon-user"></i></span>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="input-large span10" ></asp:TextBox>
							</div>
							<div class="clearfix"></div>

							<div class="input-prepend" title="Password" data-rel="tooltip">
                                Password<br />
								<span class="add-on"><i class="icon-lock"></i></span>
                                <asp:TextBox ID="txtPwd" runat="server" CssClass="input-large span10" TextMode="Password"></asp:TextBox>
							</div>
							<div class="clearfix"></div>

							<div class="input-prepend">
							<label class="remember" for="remember"> <asp:CheckBox runat="server"  ID="chkRemMe"/>Remember me</label><br />
                                <a href="ChangePassword.aspx">Forget Password</a>
							</div>
                          
							<div class="clearfix"></div>

							<p class="center span5">
							<%--<button type="submit" class="btn btn-primary">Login</button>--%>
                                <asp:Button runat="server" Text="Login" CssClass="btn btn-primary" ID="btnLogin" OnClick="btnLogin_Click"/>
							</p>
						</fieldset>
                       
					</form>
				</div>

                            </td>
                        </tr>
                    </table>
                
                </div>
				
                <!--/span-->
			</div><!--/row-->
				</div><!--/fluid-row-->
		
	</div><!--/.fluid-container-->

	<!-- external javascript
	================================================== -->
	<!-- Placed at the end of the document so the pages load faster -->

	<!-- jQuery -->
<%--	<script src="js/jquery-1.7.2.min.js"></script>
	<!-- jQuery UI -->
	<script src="js/jquery-ui-1.8.21.custom.min.js"></script>
	<!-- transition / effect library -->
	<script src="js/bootstrap-transition.js"></script>
	<!-- alert enhancer library -->
	<script src="js/bootstrap-alert.js"></script>
	<!-- modal / dialog library -->
	<script src="js/bootstrap-modal.js"></script>
	<!-- custom dropdown library -->
	<script src="js/bootstrap-dropdown.js"></script>
	<!-- scrolspy library -->
	<script src="js/bootstrap-scrollspy.js"></script>
	<!-- library for creating tabs -->
	<script src="js/bootstrap-tab.js"></script>
	<!-- library for advanced tooltip -->
	<script src="js/bootstrap-tooltip.js"></script>
	<!-- popover effect library -->
	<script src="js/bootstrap-popover.js"></script>
	<!-- button enhancer library -->
	<script src="js/bootstrap-button.js"></script>
	<!-- accordion library (optional, not used in demo) -->
	<script src="js/bootstrap-collapse.js"></script>
	<!-- carousel slideshow library (optional, not used in demo) -->
	<script src="js/bootstrap-carousel.js"></script>
	<!-- autocomplete library -->
	<script src="js/bootstrap-typeahead.js"></script>
	<!-- tour library -->
	<script src="js/bootstrap-tour.js"></script>
	<!-- library for cookie management -->
	<script src="js/jquery.cookie.js"></script>
	<!-- calander plugin -->
	<script src='js/fullcalendar.min.js'></script>
	<!-- data table plugin -->
	<script src='js/jquery.dataTables.min.js'></script>

	<!-- chart libraries start -->
	<script src="js/excanvas.js"></script>
	<script src="js/jquery.flot.min.js"></script>
	<script src="js/jquery.flot.pie.min.js"></script>
	<script src="js/jquery.flot.stack.js"></script>
	<script src="js/jquery.flot.resize.min.js"></script>
	<!-- chart libraries end -->

	<!-- select or dropdown enhancer -->
	<script src="js/jquery.chosen.min.js"></script>
	<!-- checkbox, radio, and file input styler -->
	<script src="js/jquery.uniform.min.js"></script>
	<!-- plugin for gallery image view -->
	<script src="js/jquery.colorbox.min.js"></script>
	<!-- rich text editor library -->
	<script src="js/jquery.cleditor.min.js"></script>
	<!-- notification plugin -->
	<script src="js/jquery.noty.js"></script>
	<!-- file manager library -->
	<script src="js/jquery.elfinder.min.js"></script>
	<!-- star rating plugin -->
	<script src="js/jquery.raty.min.js"></script>
	<!-- for iOS style toggle switch -->
	<script src="js/jquery.iphone.toggle.js"></script>
	<!-- autogrowing textarea plugin -->
	<script src="js/jquery.autogrow-textarea.js"></script>
	<!-- multiple file upload plugin -->
	<script src="js/jquery.uploadify-3.1.min.js"></script>
	<!-- history.js for cross-browser state change on ajax -->
	<script src="js/jquery.history.js"></script>
	<!-- application script for Charisma demo -->
	<script src="js/charisma.js"></script>--%>
	
		
</body>
</html>