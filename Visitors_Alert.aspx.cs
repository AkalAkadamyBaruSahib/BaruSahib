using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Visitors_Alert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
            }

            if (Request.QueryString["visitorIDCheckOut"] != null)
            {
                CheckOutVisitor(Request.QueryString["visitorIDCheckOut"].ToString());
            }

            getNotification();
        }
    }

    private void getNotification()
    {
   
        VisitorUserRepository repo = new VisitorUserRepository(new AkalAcademy.DataContext());
        List<Visitors> visitors = repo.GetUnCheckOutVisitor(DateTime.Now, true);

        divNotification.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;

        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='20%' style='color: #cc3300;'>Name</th>";
        ZoneInfo += "<th width='20%' style='color: #cc3300;'>Contact Number</th>";
        ZoneInfo += "<th width='20%' style='color: #cc3300;'>Building Name</th>";
        ZoneInfo += "<th width='20%' style='color: #cc3300;'>Room Numbers</th>";
        ZoneInfo += "<th width='20%' style='color: #cc3300;'>Action</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>"; 
     
        ZoneInfo += "<tbody>";
        string roomnumbers = string.Empty;

        foreach (Visitors visitor in visitors)
        {
            roomnumbers = string.Empty;
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='20%'>" + visitor.Name + "</td>";
            ZoneInfo += "<td width='20%'>" + visitor.ContactNumber + "</td>";
            if (visitor.VisitorRoomNumbers.Count > 0)
            {
                ZoneInfo += "<td width='20%'>" + visitor.VisitorRoomNumbers[0].RoomNumbers.BuildingName.Name + "</td>";
                foreach (VisitorRoomNumbers room in visitor.VisitorRoomNumbers)
                {
                    roomnumbers += room.RoomNumbers.Number + ",";
                }
                if (roomnumbers.Length > 1)
                {
                    roomnumbers = roomnumbers.Substring(0, roomnumbers.Length - 1);
                }
            }
            ZoneInfo += "<td width='20%'>" + roomnumbers + "</td>";
            ZoneInfo += "<td  width='15%' class='center'><a href='Visitors_Alert.aspx?visitorIDCheckOut=" + visitor.ID + "'>Check Out</a></td>";
            ZoneInfo += "</tr>";
        }
        
        ZoneInfo += "</tbody>"; 
        ZoneInfo += "</table>";
       

        divNotification.InnerHtml = ZoneInfo.ToString();
    }

    protected void CheckOutVisitor(string vid)
    {
        VisitorUserController visitorcontroller = new VisitorUserController();
        visitorcontroller.CheckOutVisitor(Convert.ToInt32(vid));
        getNotification();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Visitor has been Check Out Successfully.');", true);
    }
}
