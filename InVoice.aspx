<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InVoice.aspx.cs" Inherits="Example_InVoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
<title>Akal Academy</title>
    <style type="text/css">
<!--
body {
  font-family:Tahoma;
}

img {
  border:0;
}

#page {
  width:800px;
  margin:0 auto;
  padding:15px;

}

#logo {
  float:left;
  margin:0;
}

#address {
  height:181px;
  margin-left:250px; 
}

table {
  width:100%;
}

td {
padding:5px;
}

tr.odd {
  background:#e1ffe1;
}
-->
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
  <asp:Panel runat="server" ID="pnlContents">
  <div id="page">
  <div id="logo">
    <a href="http://akalacademy.org/AA/"><img src="img/logoBiil.png" width="50%" height="50%"></a>
  </div><!--end logo-->
  
  <div id="address">

    <p><b style="font-size:medium;">The Kalgidhar Socity</b><br />
        The Way to Establish Permanet Peace
   <%-- <a href="mailto:youremail@somewhere.com">youremail@somewhere.com</a>--%>
    <br /><br />
    Bill ID : <asp:Label ID="lblTranId" runat="server" ></asp:Label><br />
    Created on : <asp:Label ID="lblCreatedOn" runat="server" ></asp:Label><br />
    </p>
  </div><!--end address-->

  <div id="content">
      <table>
          <tr>
              <td>
             <p>
                  <strong><asp:Label ID="lblAgency" runat="server" ></asp:Label></strong><br />
                  Zone: <asp:Label ID="lblZone" runat="server" Text="Zone Name"></asp:Label><br />
                 
              </td>
              <td>
             <p>
                  
                 <br />
                  Academy: <asp:Label ID="lblAca" runat="server" Text="Academy Name"></asp:Label><br />
                 
              </td>
          </tr>
           <tr>
              <td colspan="2">
                  Decription of Bill: <asp:Label ID="lblDesc" runat="server" Text="Academy Name requiremet of cement begs  dsfdsg sdf sedfvc se ew"></asp:Label>
              </td>
          </tr>
      </table>
   
    <hr>
      <div id="divBillDetails" runat="server" >
         
      </div>
    
    <hr>
    <p>
      Thank you for your order!  This transaction will appear on your billing statement as <a href="http://barusahib.org/">The Kalgidhar Socity</a>.<br />
      If you have any questions, please feel free to <a href="ContactUs.aspx">contact us</a> and send us yours valuable  <a href="Feedback.aspx">Feedback</a>.
    </p>

    <hr>
    <p>
      <center><small> 
      <br /><br />
      &copy; The Kalgidhar Socity All Rights Reserved
      </small></center>
    </p>
    
  </div><!--end content-->
</div>
  </asp:Panel>
          
    </div>

    </form>
</body>
</html>
