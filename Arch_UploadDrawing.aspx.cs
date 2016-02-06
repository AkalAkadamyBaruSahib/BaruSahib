using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

public partial class Arch_UploadDrawing : System.Web.UI.Page
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
            BinddWGtYPE();
            BindAllDrawing();
        }
    }
    protected void BinddWGtYPE()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("SELECT DwTypeId,DwTypeName FROM DrawingType where Active=1 AND DwTypeName<>'ALL DRAWING'");
        ddlDwgType.DataSource = dsZone;
        ddlDwgType.DataValueField = "DwTypeId";
        ddlDwgType.DataTextField = "DwTypeName";
        ddlDwgType.DataBind();
        ddlDwgType.Items.Insert(0, "SELECT DRAWING TYPE");
        ddlDwgType.SelectedIndex = 0;
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
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct ZoneId,AcaId,DwgFileName from Drawing where ZoneId='" + ddlZone.SelectedValue + "' and AcaId='" + ddlAcademy.SelectedValue + "' and DwgFileName='" + txtDrwName.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Same Drawing Name already inserted to selected Zone and Academy');", true);
        }
        else
        {
            if (txtDrwName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Name Of Drawing.');", true);
            }
            else if (ddlZone.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Zone.');", true);
            }
            else if (ddlAcademy.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Academy .');", true);
            }
            else if (txtDrwNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Drawing No.');", true);
            }

            else if (ddlDwgType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Drawing Type .');", true);
            }

            else
            {
                string fileDwgname = System.IO.Path.GetFileName(fuDwgFile.FileName);
                string fileDwgPath = System.IO.Path.GetFileName(fuDwgFile.FileName);
                string FileDwgEx = System.IO.Path.GetExtension(fuDwgFile.FileName);
                String FDwgNam = System.IO.Path.GetFileNameWithoutExtension(fuDwgFile.FileName);
                string filePdfname = System.IO.Path.GetFileName(fuPdf.FileName).Replace("'", "");
                string filePdfPath = System.IO.Path.GetFileName(fuPdf.FileName).Replace("'", "");
                string FilePdfEx = System.IO.Path.GetExtension(fuPdf.FileName);
                String FPdfNam = System.IO.Path.GetFileNameWithoutExtension(fuPdf.FileName);
                Int64 i = 0;
                if (FileDwgEx.Contains("dwg") && FilePdfEx.Contains("pdf"))
                {
                    fileDwgPath = "~/AutoCad/" + fileDwgname;
                    filePdfPath = "~/PDF/" + filePdfname;
                    string dwgfilepath = "AutoCad/" + Regex.Replace(FDwgNam + "-" + System.DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "-").Replace(' ', '-').ToString() + FileDwgEx;
                    string pdffilepath = "PDF/" + Regex.Replace(FPdfNam + "-" + System.DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "-").Replace(' ', '-').ToString() + FilePdfEx;
                    i = DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewDrawingProc '','" + ddlZone.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + ddlDwgType.SelectedValue + "','" + txtDrwNo.Text + "','" + txtRevisionNo.Text + "','" + txtDrwName.Text + "','" + fileDwgname + "','" + dwgfilepath + "','" + filePdfname + "','" + pdffilepath + "','" + int.Parse(Session["InchargeID"].ToString()) + "','1','1',0," + ddlSubDrawingType.SelectedValue);
                    if (i > 0)
                    {
                        fuDwgFile.SaveAs(Server.MapPath(dwgfilepath));
                        fuPdf.SaveAs(Server.MapPath(pdffilepath));
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Drawing submit Successfully!!.');", true);
                    }
                    SendSMStoAdmin();
                    BindAllDrawing();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Upload Only .dwg,.pdf files');", true);
                }

            }
        }
    }
    protected void BindAllDrawing()
    {
        int UserID = int.Parse(Session["InchargeID"].ToString());
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DrwaingShowForArcDep '" + UserID + "' ");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='50%'>Drawing Details</th>";
        ZoneInfo += "<th width='25%'>Drawing File</th>";
        ZoneInfo += "<th width='25%'>Uploaded Date</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";

        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='50%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Drawing No: " + dsZoneDetails.Tables[0].Rows[i]["DwgNo"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Revision No: " + dsZoneDetails.Tables[0].Rows[i]["RevisionNo"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Drawing Name: " + dsZoneDetails.Tables[0].Rows[i]["DrawingName"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td width='25%'><table><tr><td>PDF: <a href='" + dsZoneDetails.Tables[0].Rows[i]["PdfFilePath"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["PdfFileName"].ToString() + "</a> </td></tr><tr><td> DWG: <a href='" + dsZoneDetails.Tables[0].Rows[i]["DwgFilePath"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["DwgFileName"].ToString() + "</a> </td></tr></table></td>";
            ZoneInfo += "<td width='25%'>" + dsZoneDetails.Tables[0].Rows[i]["CreatedOn"].ToString() + "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();

    }

    protected void BindSubDrawingType()
    {
        DataSet dwgZone = new DataSet();
        dwgZone = DAL.DalAccessUtility.GetDataInDataSet("select Description,id from dbo.SubDrawingTypes where DwgTypeId ='" + ddlDwgType.SelectedValue + "'");
        ddlSubDrawingType.DataSource = dwgZone;
        ddlSubDrawingType.DataValueField = "ID";
        ddlSubDrawingType.DataTextField = "Description";
        ddlSubDrawingType.DataBind();
        ddlSubDrawingType.Items.Insert(0, new ListItem("SELECT SUB DRAWING TYPE", "0"));
        ddlSubDrawingType.SelectedIndex = 0;
    }

    protected void ddlDwgType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubDrawingType();
    }

    private void SendSMStoAdmin()
    {
        int AcaID = Convert.ToInt32(ddlAcademy.SelectedValue);
        const int USERTYPE = 7;
        InchargeController conrtoller = new InchargeController();
        List<string> incharges = conrtoller.GetUsersByUserTypeAndAcademic(USERTYPE, AcaID);

        string adminNumber = System.Configuration.ConfigurationManager.AppSettings["AdminToSendDrawingSMS"].ToString();
        if (btnSave.Visible == true)
        {
            Utility.SendSMS(adminNumber, " Non-Approved Drawing of " + ddlAcademy.SelectedItem.Text + " has been uploaded to www.Akalsewa.org.");
        }
    }
}