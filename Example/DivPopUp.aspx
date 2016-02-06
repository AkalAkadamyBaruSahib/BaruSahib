<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivPopUp.aspx.cs" Inherits="Example_DivPopUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div id="helptopic1">
 		<h2>Help topic 1</h2>
 		<p>Blah blah blah...</p>
 	</div>
 
 	<div id="helptopic2">
 		<h2>Help topic 2</h2>
 		<p>Blah blah blah...</p>
 	</div>
        <a href="DivPopUp.aspx#helptopic2" onclick="window.open(this.href,'_blank'); return false;">Open me</a>
    </div>
    </form>
</body>
</html>
