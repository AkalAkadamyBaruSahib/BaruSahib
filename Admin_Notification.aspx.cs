using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_Notification : System.Web.UI.Page
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
        DataSet dsBillDetails = new DataSet();
        dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("select MsgId,MsgContent,MsgByUserType from msg where MsgToUserType=1");
        divNotification.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        for (int i = 0; i < dsBillDetails.Tables[0].Rows.Count; i++)
        {
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "11")
            {
                ZoneInfo += "<div class='alert alert-error'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "2")
            {
                ZoneInfo += "<div class='alert alert-success'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "3")
            {
                ZoneInfo += "<div class='alert alert-info'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "4")
            {
                ZoneInfo += "<div class='alert alert-block '>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "5")
            {
                ZoneInfo += "<div class='alert alert-success'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "6")
            {
                ZoneInfo += "<div class='alert alert-info'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "7")
            {
                ZoneInfo += "<div class='alert alert-error'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "8")
            {
                ZoneInfo += "<div class='alert alert-success'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "9")
            {
                ZoneInfo += "<div class='alert alert-info'>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
            if (dsBillDetails.Tables[0].Rows[i]["MsgByUserType"].ToString() == "10")
            {
                ZoneInfo += "<div class='alert alert-block '>";
                ZoneInfo += "<button type='button' class='close' data-dismiss='alert'><a href='Admin_Notification?MsgId=" + dsBillDetails.Tables[0].Rows[0]["MsgId"].ToString() + "'>×</a></button>";
                ZoneInfo += "" + dsBillDetails.Tables[0].Rows[i]["MsgContent"].ToString() + "";
                ZoneInfo += "</div>";
            }
        }
        divNotification.InnerHtml = ZoneInfo.ToString();

    }
}