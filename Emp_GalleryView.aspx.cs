using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emp_GalleryView : System.Web.UI.Page
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
            
            BindGalleryDetails();

        }
    }

    protected void BindGalleryDetails()
    {
        DataSet dsSateDetails = new DataSet();
        dsSateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowGalleryDetails4User ");
        divGallery.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;

        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-picture'></i> Gallery</h2>";
        ZoneInfo += "<div class='box-icon'>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<ul class='thumbnails gallery'>";
        for (int i = 0; i < dsSateDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<li id='image-1' class='thumbnail'>";
            ZoneInfo += "<a style='background:url(" + dsSateDetails.Tables[0].Rows[i]["ImgPath"].ToString() + ")' title='Image Des:" + dsSateDetails.Tables[0].Rows[i]["PicDes"].ToString() + "' href='" + dsSateDetails.Tables[0].Rows[i]["ImgPath"].ToString() + "'><img class='grayscale' width='50Px' height='50px' src='" + dsSateDetails.Tables[0].Rows[i]["ImgPath"].ToString() + "' alt='" + dsSateDetails.Tables[0].Rows[i]["PicDes"].ToString() + "'></a>";
            ZoneInfo += "</li>";
        }
        ZoneInfo += "</ul>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divGallery.InnerHtml = ZoneInfo.ToString();
    }
}