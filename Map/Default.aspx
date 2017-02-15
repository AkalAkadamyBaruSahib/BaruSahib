<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        #dvMap
        {
            width: 500px;
            height: 400px;
        }

        html, body,#divloadMap {  
        height: 100%;  
        margin: 0px;  
        padding: 0px;  
        margin-top:15px  
      }  
      
      .apply {  
        margin-top: 16px;  
        border: 1px solid transparent;  
        border-radius: 2px 0 0 2px;  
        box-sizing: border-box;  
        -moz-box-sizing: border-box;  
        height: 32px;  
        outline: none;  
        box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);  
      }  
  
      #txtsearch {  
        background-color: #fff;  
        padding: 0 11px 0 13px;  
        width: 400px;  
        font-family: Roboto;  
        font-size: 15px;  
        font-weight: 300;  
        text-overflow: ellipsis;  
      }  
  
      #txtsearch:focus {  
        border-color: #4d90fe;  
        margin-left: -1px;  
        padding-left: 14px;    
        width: 401px;  
      }  
  
      .pac-container {  
        font-family: Roboto;  
      }  
  
      #type-selector {  
        color: #fff;  
        background-color: #4d90fe;  
        padding: 5px 11px 0px 11px;  
      }  
  
      #type-selector label {  
        font-family: Roboto;  
        font-size: 13px;  
        font-weight: 300;  
      }  
    </style>
    <title>Google Map</title>

    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyDRecgD4vzlMeNBIXZJ_2YX7DYRs7_TP1E&libraries=places"></script>

    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>

    <script src="Scripts/googlemap.js" type="text/javascript"></script>

    <script src="Scripts/jquery.tablednd.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <input id="txtsearch" class="apply" type="text" placeholder="Enter Search Place e.g C# Corner Noida" />
        <div id="dvMap" style="height: 600px; width: 80%;">
        </div>
        <br />
        Average Speed (km/hr) :
    <asp:TextBox ID="txtAvgSpeed" runat="server"></asp:TextBox>
        <br />
        <br />
        <div align="left" style="width: 100%;">
            <table id="HtmlTable1" width="80%" border="1" style="border-color: #275D18; text-align: center;"
                cellpadding="3" cellspacing="0">
                <tr style="background-color: #D1E8B0;">
                    <td style="width: 80px; color: #275D18; font-weight: bold;">Sr No.
                    </td>
                    <td style="width: 80px; color: #275D18; font-weight: bold;">Location Name
                    </td>
                    <td style="width: 100px; color: #275D18; font-weight: bold;">Latitude
                    </td>
                    <td style="width: 100px; color: #275D18; font-weight: bold;">Longitude
                    </td>
                    <td style="width: 100px; color: #275D18; font-weight: bold;">Distance (Meters)
                    </td>
                    <td style="width: 70px; color: #275D18; font-weight: bold;">Time (Minutes)
                    </td>
                    <td style="width: 60px; color: #275D18; font-weight: bold;">Delete
                    </td>
                </tr>
            </table>
        </div>
        <div align="left" style="width: 100%;">
            <table id="HtmlTable" width="80%" border="1" style="border-color: #275D18; text-align: center;"
                cellpadding="3" cellspacing="0">
            </table>
        </div>

        <script type="text/javascript" language="javascript">
            InitializeMap();
        </script>

    </form>
</body>
</html>
