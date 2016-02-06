<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Example.aspx.cs" Inherits="Example_Example" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-responsive.css" rel="stylesheet"/>
	<link href="../css/charisma-app.css" rel="stylesheet"/>
	<link href="../css/jquery-ui-1.8.21.custom.css" rel="stylesheet"/>
	<link href='../css/fullcalendar.css' rel='stylesheet'/>
	<link href='../css/fullcalendar.print.css' rel='stylesheet'  media='print'/>
	<link href='../css/chosen.css' rel='stylesheet'/>
	<link href='../css/uniform.default.css' rel='stylesheet'/>
	<link href='../css/colorbox.css' rel='stylesheet'/>
	<link href='../css/jquery.cleditor.css' rel='stylesheet'/>
	<link href='../css/jquery.noty.css' rel='stylesheet'/>
	<link href='../css/noty_theme_default.css' rel='stylesheet'/>
	<link href='../css/elfinder.min.css' rel='stylesheet'/>
	<link href='../css/elfinder.theme.css' rel='stylesheet'/>
	<link href='../css/jquery.iphone.toggle.css' rel='stylesheet'/>
	<link href='../css/opa-icons.css' rel='stylesheet'/>
	<link href='../css/uploadify.css' rel='stylesheet'/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <div class="control-group">
								<label class="control-label" for="selectError1">Multiple Select / Tags</label>
								<div class="controls">
   <%--<asp:DropDownList runat="server" ID="ddlTest" ></asp:DropDownList>--%>
                                    <asp:TextBox runat="server" ID="ddlTest" CssClass="span6 typeahead"></asp:TextBox>
                                    </div>
             </div>
    </div>
    </form>
</body>
</html>
