using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_Gallery : System.Web.UI.Page
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
            BindZone();
            BindGalleryDetails();
           
        }
    }
    protected void BindGalleryDetails()
    {
        DataSet dsSateDetails = new DataSet();
        dsSateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowGalleryDetails_ByUser '" + lblUser.Text + "'");
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
    protected void BindZone()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName  from Zone where Active=1");
        ddlZone.DataSource = dsZone;
        ddlZone.DataValueField = "ZoneId";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }
    protected void BindAcademy()
    {
        DataSet dsAca = new DataSet();
        dsAca = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where Active=1 and ZoneId='" + ddlZone.SelectedValue + "'");
        ddlAcademy.DataSource = dsAca;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, "Select Academy");
        ddlAcademy.SelectedIndex = 0;
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
            if (txtImgDes.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Describtion of Images.');", true);
            }

            //if (ddlZone.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Zone.');", true);
            //}
            //if (ddlAcademy.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Academy .');", true);
            //}
            else
            {
                string fileImgName = System.IO.Path.GetFileName(fuImg.FileName);
                string fileImgPath = System.IO.Path.GetFileName(fuImg.FileName);
                string FileImgEx = System.IO.Path.GetExtension(fuImg.FileName);
                String FImgNam = System.IO.Path.GetFileNameWithoutExtension(fuImg.FileName);
                Int64 i = 0;
                //OnLocal
                //fileImgPath = "../AkalAcademy/Gallery/" + fileImgName;
                //onserver
                fileImgPath = "../Gallery/" + fileImgName;
                //if (fileDwgPath == ".dwg" & fuDwgFile.HasFile == true && filePdfPath == ".pdf" & fuPdf.HasFile == true)
                //{
                //i = DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewGallery '','" + ddlZone.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + txtImgDes.Text + "','" + fileImgName + "','" + fileImgPath + "','1','" + lblUser.Text + "','1'");
                i = DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewGallery '','" + 0 + "','" + 0 + "','" + txtImgDes.Text + "','" + fileImgName + "','" + fileImgPath + "','1','" + lblUser.Text + "','1'");

                if (i > 0)
                {
                    fuImg.SaveAs(Server.MapPath("Gallery/") + fileImgName);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Image Submit Successfully!!.');", true);
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Upload Only .dwg,.pdf');", true);
                //}
                //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewWorkAllot '','" + ddlZone.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + fileImgName + "','" + fileImgPath + "','" + txtWorkAllot.Text + "','1','" + lblUser.Text + "','1'");
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Name of Work Create Successfully.');", true);
                BindGalleryDetails();
                txtImgDes.Text = "";
                //ddlZone.SelectedIndex = 0;
                //ddlAcademy.SelectedIndex = 0;
            }
       
    }
}