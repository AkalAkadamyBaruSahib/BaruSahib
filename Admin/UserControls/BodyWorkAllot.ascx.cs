using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_UserControls_BodyWorkAllot : System.Web.UI.UserControl
{
    public string InchargeID = string.Empty;
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
                InchargeID = Session["InchargeID"].ToString();
            }

            BindZone();
            BindWorkAllotDetails();
            if (Request.QueryString["WAId"] != null)
            {
                getAcaDetails(Request.QueryString["WAId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
                ddlZone.Enabled = false;
                ddlAcademy.Enabled = false;
                imgWorkAllot.Visible = true;

            }
            if (Request.QueryString["WAIdIA"] != null)
            {
                DeactiveAca(Request.QueryString["WAIdIA"].ToString());
            }
            if (Request.QueryString["WAIdA"] != null)
            {
                ActiveAca(Request.QueryString["WAIdA"].ToString());
            }
            if (Request.QueryString["AcaId"] != null)
            {
                GetWorkAllotDetailsByClick(Request.QueryString["AcaId"].ToString());
                div1st.Visible = false;
            }
        }
    }
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelWorkAllot");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "WorkAllot.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindDatatable();
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    protected void GetWorkAllotDetailsByClick(string id)
    {
        DataSet dsSateDetails = new DataSet();
        dsSateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowWorkAllotDetailsByAcaId '" + id + "'");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='20%'>Zone & Academy</th>";
        ZoneInfo += "<th width='25%'>Name of Work</th>";
        ZoneInfo += "<th width='25%'>Image of Work</th>";
        ZoneInfo += "<th width='10%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";

        for (int i = 0; i < dsSateDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='20%'><table><tr><td><b>Zone:</b> " + dsSateDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr>";
            ZoneInfo += " <tr><td><b>Academy:</b> " + dsSateDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td width='25%'><a href='ViewAdminWorkDetails.aspx?WAID='" + dsSateDetails.Tables[0].Rows[i]["WAID"].ToString() + ">" + dsSateDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='25%'><ul class='thumbnails gallery><li id='image-1' class='thumbnail'>";
            ZoneInfo += "<a  style='background:url(" + dsSateDetails.Tables[0].Rows[i]["ImageFilePath"].ToString() + ")'  href='" + dsSateDetails.Tables[0].Rows[i]["ImageFilePath"].ToString() + "'>";
            ZoneInfo += "<img class='grayscale' width='75Px' height='75PX' src='" + dsSateDetails.Tables[0].Rows[i]["ImageFilePath"].ToString() + "' ></a></li></ul></td>";
            ZoneInfo += "<td class='center' width='10%'>";
            if (dsSateDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_WorkAllot.aspx?WAIdA=" + dsSateDetails.Tables[0].Rows[i]["WAId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-info' href='Admin_WorkAllot.aspx?WAId=" + dsSateDetails.Tables[0].Rows[i]["WAId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_WorkAllot.aspx?WAIdIA=" + dsSateDetails.Tables[0].Rows[i]["WAId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void BindZone()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select distinct Z.ZoneId,Z.ZoneName  from Zone Z INNER JOIN AcademyAssignToEmployee AE on AE.ZoneId=Z.ZoneId where Z.Active=1 and AE.EmpId='" + InchargeID + "'");
        ddlZone.DataSource = dsZone;
        ddlZone.DataValueField = "ZoneId";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, "SELECT ZONE");
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
        ddlAcademy.Items.Insert(0, "SELECT ACADEMY");
        ddlAcademy.SelectedIndex = 0;
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,AcaId,WorkAllotName from WorkAllot where ZoneId='" + ddlZone.SelectedValue + "' and AcaId='" + ddlAcademy.SelectedValue + "' and WorkAllotName='" + txtWorkAllot.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Same Alloted Work already assigned to selected Zone and Academy');", true);
        }
        else
        {
            if (txtWorkAllot.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Name Of Work.');", true);
            }

            if (ddlZone.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Zone.');", true);
            }
            if (ddlAcademy.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Academy .');", true);
            }
            else
            {
                string fileImgName = System.IO.Path.GetFileName(fuImgeFile.FileName);
                string fileImgPath = System.IO.Path.GetFileName(fuImgeFile.FileName);
                string FileImgEx = System.IO.Path.GetExtension(fuImgeFile.FileName);
                String FImgNam = System.IO.Path.GetFileNameWithoutExtension(fuImgeFile.FileName);
                Int64 i = 0;
                fileImgPath = "/AkalAcademy/AllotedWorkImage/" + fileImgName;
                //onserver
                //fileImgPath = "../AllotedWorkImage/" + fileImgName;
                //if (fileDwgPath == ".dwg" & fuDwgFile.HasFile == true && filePdfPath == ".pdf" & fuPdf.HasFile == true)
                //{
                i = DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewWorkAllot '','" + ddlZone.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + txtWorkAllot.Text + "','" + fileImgName + "','" + fileImgPath + "','1','" + lblUser.Text + "','1'");
                if (i > 0)
                {
                    if (fileImgName != "")
                    {
                        fuImgeFile.SaveAs(Server.MapPath("AllotedWorkImage/") + fileImgName);
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Name of Work Create Successfully!!.');", true);
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Upload Only .dwg,.pdf');", true);
                //}
                //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewWorkAllot '','" + ddlZone.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + fileImgName + "','" + fileImgPath + "','" + txtWorkAllot.Text + "','1','" + lblUser.Text + "','1'");
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Name of Work Create Successfully.');", true);
                SendSMS(ddlAcademy.SelectedValue);
                BindWorkAllotDetails();
                txtWorkAllot.Text = "";
                ddlZone.SelectedIndex = 0;
                ddlAcademy.SelectedIndex = 0;
            }
        }
    }


    private void SendSMS(string AcaID)
    {
        const int CONST_USERTYPE = 2;

        string smsTo = string.Empty;
        InchargeController conrtoller = new InchargeController();
        List<string> incharges = conrtoller.GetUsersByUserTypeAndAcademic(CONST_USERTYPE, int.Parse(AcaID));
        foreach (string inchargeNumber in incharges)
        {
            smsTo += inchargeNumber + ",";
        }
        if (smsTo != "")
        {
            smsTo = smsTo.Substring(0, smsTo.Length - 1);
        }
        Utility.SendSMS(smsTo, "New Work for " + ddlAcademy.SelectedItem.Text + " has been uploaded to www.Akalsewa.org.");
    }

    protected void BindWorkAllotDetails()
    {
        DataSet dsSateDetails = new DataSet();
        dsSateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowWorkAllotDetails_ByUser '" + lblUser.Text + "'");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='20%'>Zone & Academy</th>";
        ZoneInfo += "<th width='25%'>Name of Work</th>";
        ZoneInfo += "<th width='25%'>Image of Work</th>";
        ZoneInfo += "<th width='10%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";

        for (int i = 0; i < dsSateDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='20%'><table><tr><td><b>Zone:</b> " + dsSateDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr>";
            ZoneInfo += " <tr><td><b>Academy:</b> " + dsSateDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td width='25%'><a href='ViewAdminWorkDetails.aspx?WAID=" + dsSateDetails.Tables[0].Rows[i]["WAID"].ToString() + "'>" + dsSateDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='25%'><ul class='thumbnails gallery><li id='image-1' class='thumbnail'>";
            ZoneInfo += "<a  style='background:url(" + GetImageURL(dsSateDetails.Tables[0].Rows[i]["ImageFilePath"].ToString()) + ")'  href='" + GetImageURL(dsSateDetails.Tables[0].Rows[i]["ImageFilePath"].ToString()) + "'>";
            ZoneInfo += "<img class='grayscale' width='75Px' height='75PX' src='" + GetImageURL(dsSateDetails.Tables[0].Rows[i]["ImageFilePath"].ToString()) + "' ></a></li></ul></td>";
            ZoneInfo += "<td class='center' width='10%'>";
            if (dsSateDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_WorkAllot.aspx?WAIdA=" + dsSateDetails.Tables[0].Rows[i]["WAId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>";
            ZoneInfo += "<a class='btn btn-info' href='Admin_WorkAllot.aspx?WAId=" + dsSateDetails.Tables[0].Rows[i]["WAId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_WorkAllot.aspx?WAIdIA=" + dsSateDetails.Tables[0].Rows[i]["WAId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();
    }

    private string GetImageURL(string path)
    {
        string newImagePath = string.Empty;
        newImagePath = path.Substring(13);
        return newImagePath;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string Acaid = Request.QueryString["WAId"];
        DataSet dsExist = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,AcaId,WorkAllotName from WorkAllot where ZoneId='" + ddlZone.SelectedValue + "' and AcaId='" + ddlAcademy.SelectedValue + "' and WorkAllotName='" + txtWorkAllot.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Same Alloted Work already assigned to selected Zone and Academy');", true);
        }
        else
        {
            if (txtWorkAllot.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Name Of Work.');", true);
            }

            if (ddlZone.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Zone.');", true);
            }
            if (ddlAcademy.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Academy .');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewWorkAllot '" + Acaid + "','" + ddlZone.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + txtWorkAllot.Text + "','" + lblImgFileName.Text + "','" + imgWorkAllot.ImageUrl + "','1','" + lblUser.Text + "','2'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Name Of Work edit Successfully.');", true);
                BindWorkAllotDetails();
                txtWorkAllot.Text = "";
                ddlZone.SelectedIndex = 0;
                ddlAcademy.SelectedIndex = 0;
            }
        }
    }
    protected void DeactiveAca(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewWorkAllot '" + ID + "','','','','','','0','" + lblUser.Text + "','4'");
        BindWorkAllotDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Work deactive successfully.');", true);

    }
    protected void ActiveAca(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewWorkAllot '" + ID + "','','','','','','1','" + lblUser.Text + "','4'");
        BindWorkAllotDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Work active successfully.');", true);

    }
    private void getAcaDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("select WorkAllotName,ZoneId,AcaId,ImageFilePath,ImageFileName from WorkAllot where Active=1 and WAId='" + ID + "'");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtWorkAllot.Text = dsCouDetails.Tables[0].Rows[0]["WorkAllotName"].ToString();
            BindZone();
            ddlZone.SelectedIndex = ddlZone.Items.IndexOf(ddlZone.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["ZoneId"].ToString().Trim()));
            BindAcademy();
            ddlAcademy.SelectedIndex = ddlAcademy.Items.IndexOf(ddlAcademy.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["AcaId"].ToString().Trim()));
            imgWorkAllot.ImageUrl = dsCouDetails.Tables[0].Rows[0]["ImageFilePath"].ToString();
            lblImgFileName.Text = dsCouDetails.Tables[0].Rows[0]["ImageFileName"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active Work Allot Status before edit..');", true);
        }
        BindWorkAllotDetails();
    }
}