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
        if (!Page.IsPostBack)
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
        }
    }
    protected void BinddWGtYPE()
    {
        DataTable dsZone = new DataTable();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("SELECT DwTypeId,DwTypeName FROM DrawingType where Active=1 AND DwTypeName<>'ALL DRAWING'").Tables[0];
        if (dsZone != null && dsZone.Rows.Count > 0)
        {
            ddlDwgType.DataSource = dsZone;
            ddlDwgType.DataValueField = "DwTypeId";
            ddlDwgType.DataTextField = "DwTypeName";
            ddlDwgType.DataBind();
            ddlDwgType.Items.Insert(0, new ListItem("Select Drawing Type", "0"));
            ddlDwgType.SelectedIndex = 0;
        }
    }
    protected void BindZone()
    {
        DataTable dsZone = new DataTable();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName  from Zone where Active=1").Tables[0];
        if (dsZone != null && dsZone.Rows.Count > 0)
        {
            ddlZone.DataSource = dsZone;
            ddlZone.DataValueField = "ZoneId";
            ddlZone.DataTextField = "ZoneName";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("Select Zone", "0"));
            ddlZone.SelectedIndex = 0;
        }
    }
    protected void BindAcademy()
    {
        DataTable dsAca = new DataTable();
        dsAca = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where Active=1 and ZoneId='" + ddlZone.SelectedValue + "'").Tables[0];
        if (dsAca != null && dsAca.Rows.Count > 0)
        {
            ddlAcademy.DataSource = dsAca;
            ddlAcademy.DataValueField = "AcaId";
            ddlAcademy.DataTextField = "AcaName";
            ddlAcademy.DataBind();
            ddlAcademy.Items.Insert(0, new ListItem("Select Academy", "0"));
            ddlAcademy.SelectedIndex = 0;
        }
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
            string fileDwgname = System.IO.Path.GetFileName(fuDwgFile.FileName);
            string fileDwgPath = System.IO.Path.GetFileName(fuDwgFile.FileName);
            string FileDwgEx = System.IO.Path.GetExtension(fuDwgFile.FileName);
            String FDwgNam = System.IO.Path.GetFileNameWithoutExtension(fuDwgFile.FileName);
            string filePdfname = System.IO.Path.GetFileName(fuPdf.FileName).Replace("'", "");
            string filePdfPath = System.IO.Path.GetFileName(fuPdf.FileName).Replace("'", "");
            string FilePdfEx = System.IO.Path.GetExtension(fuPdf.FileName);
            String FPdfNam = System.IO.Path.GetFileNameWithoutExtension(fuPdf.FileName);
            Int64 i = 0;
            if ((FileDwgEx.Contains("dwg") || FileDwgEx.ToLower().Contains("zip")) && (FilePdfEx.Contains("pdf") || FilePdfEx.ToLower().Contains("zip")))
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
                Response.Redirect("Arch_DrawingView.aspx");
                // BindAllDrawing();
            }
        }
    }
  
    protected void BindSubDrawingType()
    {
        DataTable dwgZone = new DataTable();
        dwgZone = DAL.DalAccessUtility.GetDataInDataSet("select Description,id from dbo.SubDrawingTypes where DwgTypeId ='" + ddlDwgType.SelectedValue + "'").Tables[0];
        if (dwgZone != null && dwgZone.Rows.Count > 0)
        {
            ddlSubDrawingType.DataSource = dwgZone;
            ddlSubDrawingType.DataValueField = "ID";
            ddlSubDrawingType.DataTextField = "Description";
            ddlSubDrawingType.DataBind();
            ddlSubDrawingType.Items.Insert(0, new ListItem("Select Sub Drawing Type", "0"));
            ddlSubDrawingType.SelectedIndex = 0;
        }
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