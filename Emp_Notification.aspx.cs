using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Emp_Notification : System.Web.UI.Page
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

            getNotification();
        }
    }
    private void getNotification()
    {
        DataSet dsNotificationDetails = new DataSet();
        DataSet dsMsgTo = DAL.DalAccessUtility.GetDataInDataSet("select InchargeId from Incharge where LoginId='"+ lblUser.Text +"' and UserTypeId=2");
        //dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("select MsgId,MsgContent,MsgByUserType from msg where MsgToUserType='" + dsMsgTo.Tables[0].Rows[0]["InchargeId"].ToString() + "'");
        if (Request.QueryString["Notification"] != null && Request.QueryString["Notification"].ToString() == "All")
        {
            dsNotificationDetails = DAL.DalAccessUtility.GetDataInDataSet("select MsgId,MsgContent,MsgByUserType,ID from msg where MsgToUserType='2' order by MsgOn desc");
        }
        else
        {
            dsNotificationDetails = DAL.DalAccessUtility.GetDataInDataSet("select MsgId,MsgContent,MsgByUserType,ID from msg where MsgToUserType='2' and Active=1 order by MsgOn desc");
        }

        divNotification.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        for (int i = 0; i < dsNotificationDetails.Tables[0].Rows.Count; i++)
        {
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "11")
            {
                ZoneInfo += "<div class='alert alert-error'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "2")
            {
                ZoneInfo += "<div class='alert alert-success'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "3")
            {
                ZoneInfo += "<div class='alert alert-info'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "4")
            {
                ZoneInfo += "<div class='alert alert-block '>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "5")
            {
                ZoneInfo += "<div class='alert alert-success'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "6")
            {
                ZoneInfo += "<div class='alert alert-info'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "7")
            {
                ZoneInfo += "<div class='alert alert-error'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "8")
            {
                ZoneInfo += "<div class='alert alert-success'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "9")
            {
                ZoneInfo += "<div class='alert alert-info'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "10")
            {
                ZoneInfo += "<div class='alert alert-block '>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsNotificationDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "1")
            {
                ZoneInfo += "<div class='alert alert-block '>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsNotificationDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                if (!string.IsNullOrEmpty(dsNotificationDetails.Tables[0].Rows[i]["ID"].ToString()))
                {
                    ZoneInfo += "<a href='Emp_DrawingView.aspx?AcaId=" + dsNotificationDetails.Tables[0].Rows[i]["ID"].ToString() + "'>" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "</a>";//"" + dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                }
                else
                    ZoneInfo += dsNotificationDetails.Tables[0].Rows[i]["MsgContent"].ToString();

                ZoneInfo += "</div>";
            }
        }
        divNotification.InnerHtml = ZoneInfo.ToString();

    }
}