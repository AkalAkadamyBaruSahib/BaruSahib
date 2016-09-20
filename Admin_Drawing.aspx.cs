using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

public partial class Admin_Drawing : System.Web.UI.Page
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
            //SetInitialRowEstimate();
            BindZone();
            BinddWGtYPE();
            //BindAllDrawing();
        }
    }
    protected void Clr()
    {
        txtDrwName.Text = "";
        txtDrwNo.Text = "";
        txtRevisionNo.Text = "";
        ddlAcademy.SelectedIndex = 0;
        ddlZone.SelectedIndex = 0;
        ddlDwgType.SelectedIndex = 0;
        fuDwgFile.Dispose();
        fuPdf.Dispose();
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
            ddlDwgType.Items.Insert(0, "SELECT DRAWING TYPE");
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
            ddlZone.Items.Insert(0, "Select Zone");
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
            ddlAcademy.Items.Insert(0, "Select Academy");
            ddlAcademy.SelectedIndex = 0;
        }
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(1000);
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
            else if (txtRevisionNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Drawing's Revision No.');", true);
            }

            else if (ddlDwgType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Drawing Type .');", true);
            }
            else if (string.IsNullOrEmpty(fuDwgFile.FileName))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please upload Auto Cad drawing file .');", true);
            }
            else if (string.IsNullOrEmpty(fuPdf.FileName))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please upload .PDF drawing file .');", true);
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
                //<OnServerCode>
                fileDwgPath = "~/AutoCad/" + fileDwgname;
                filePdfPath = "~/PDF/" + filePdfname;
                //<OnLocalHost>
                //fileDwgPath = "../AkalAcademy/AutoCad/" + fileDwgname;
                //filePdfPath = "../AkalAcademy/PDF/" + filePdfname;
                //if (fileDwgPath == ".dwg" & fuDwgFile.HasFile == true && filePdfPath == ".pdf" & fuPdf.HasFile == true)
                //{
                string dwgfilepath = "AutoCad/" + Regex.Replace(FDwgNam + "-" + System.DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "-").Replace(' ', '-').ToString() + FileDwgEx;
                string pdffilepath = "PDF/" + Regex.Replace(FPdfNam + "-" + System.DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "-").Replace(' ', '-').ToString() + FilePdfEx;
                i = DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewDrawingProc '','" + ddlZone.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + ddlDwgType.SelectedValue + "','" + txtDrwNo.Text + "','" + txtRevisionNo.Text + "','" + txtDrwName.Text + "','" + fileDwgname + "','" + dwgfilepath + "','" + filePdfname + "','" + pdffilepath + "','" + int.Parse(Session["InchargeID"].ToString()) + "','1','1',1," + ddlSubDrawingType.SelectedValue);
                if (i > 0)
                {
                    fuDwgFile.SaveAs(Server.MapPath(dwgfilepath));
                    fuPdf.SaveAs(Server.MapPath(pdffilepath));
                    Response.Redirect("Admin_DrawingView.aspx");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Drawing submit Successfully!!.');", true);
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Upload Only .dwg,.pdf');", true);
                //}

                // SendNotificationToEmp();
                SendSMSToConstructionUsers(int.Parse(ddlAcademy.SelectedItem.Value), txtDrwName.Text);
              
                Clr();
            }
        }
    }

  
    protected void rptDrawings_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "dwg")
        {
            string filename = e.CommandArgument.ToString();
            string path = MapPath("../AutoCad/" + filename);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" +
            filename);
            Response.BinaryWrite(bts);
            Response.Flush();

        }
        if (e.CommandName == "pdf")
        {
            string filename = e.CommandArgument.ToString();
            string path = MapPath("../PDF/" + filename);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" +
            filename);
            Response.BinaryWrite(bts);
            Response.Flush();

        }
        if (e.CommandName == "EditDwg")
        {
            //string DwgId = e.CommandArgument.ToString();
            Session["DwgId"] = e.CommandArgument.ToString();
            DataSet dsDwg = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,AcaId,DwTypeId,DwgNo,RevisionNo,DrawingName from Drawing where DwgId='" + Session["DwgId"].ToString() + "' order by DwTypeId desc");
            BindZone();
            ddlZone.SelectedIndex = ddlZone.Items.IndexOf(ddlZone.Items.FindByValue(dsDwg.Tables[0].Rows[0]["ZoneId"].ToString().Trim()));
            ddlZone.Enabled = false;
            BindAcademy();
            ddlAcademy.SelectedIndex = ddlAcademy.Items.IndexOf(ddlAcademy.Items.FindByValue(dsDwg.Tables[0].Rows[0]["AcaId"].ToString().Trim()));
            ddlAcademy.Enabled = false;
            txtDrwName.Text = dsDwg.Tables[0].Rows[0]["DrawingName"].ToString();
            txtDrwName.Enabled = false;
            txtDrwNo.Text = dsDwg.Tables[0].Rows[0]["DwgNo"].ToString();
            txtDrwNo.Enabled = false;
            txtRevisionNo.Text = dsDwg.Tables[0].Rows[0]["RevisionNo"].ToString();
            txtRevisionNo.Enabled = false;
            btnEdit.Visible = true;
            btnSave.Visible = false;


        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string fileDwgname = System.IO.Path.GetFileName(fuDwgFile.FileName);
        string fileDwgPath = System.IO.Path.GetFileName(fuDwgFile.FileName);
        string FileDwgEx = System.IO.Path.GetExtension(fuDwgFile.FileName);
        String FDwgNam = System.IO.Path.GetFileNameWithoutExtension(fuDwgFile.FileName);
        string filePdfname = System.IO.Path.GetFileName(fuPdf.FileName);
        string filePdfPath = System.IO.Path.GetFileName(fuPdf.FileName);
        string FilePdfEx = System.IO.Path.GetExtension(fuPdf.FileName);
        String FPdfNam = System.IO.Path.GetFileNameWithoutExtension(fuPdf.FileName);
        Int64 i = 0;
        //<OnServerCode>
        fileDwgPath = "../AutoCad/" + fileDwgname;
        filePdfPath = "../PDF/" + filePdfname;
        //<OnLocalHost>
        //fileDwgPath = "../AkalAcademy/AutoCad/" + fileDwgname;
        //filePdfPath = "../AkalAcademy/PDF/" + filePdfname;
        //if (fileDwgPath == ".dwg" & fuDwgFile.HasFile == true && filePdfPath == ".pdf" & fuPdf.HasFile == true)
        //{
        i = DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewDrawingProc '" + Session["DwgId"].ToString() + "','','','','','','','" + fileDwgname + "','" + fileDwgPath + "','" + filePdfname + "','" + filePdfPath + "','" + int.Parse(Session["InchargeID"].ToString()) + "','2','1',1," + ddlSubDrawingType.SelectedValue);
        if (i > 0)
        {
            fuDwgFile.SaveAs(Server.MapPath("AutoCad/") + fileDwgname);
            fuPdf.SaveAs(Server.MapPath("PDF/") + filePdfname);
           
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Drawing Updated Successfully!!.');", true);
        }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Upload Only .dwg,.pdf');", true);
        //}
        //BindAllDrawing();
        SendSMSToConstructionUsers(int.Parse(ddlAcademy.SelectedItem.Value), txtDrwName.Text);
       
        Clr();
    }

    private void SendNotificationToEmp()
    {
        DataSet dsdrawingID = DAL.DalAccessUtility.GetDataInDataSet("select top 1 * from Drawing order by DwgId desc");

        string msg = "New Drawing has been uploaded by " + lblUser.Text;

        try
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendDrawingMsgToUserType '" + lblUser.Text + "','2','" + msg + "','" + dsdrawingID.Tables[0].Rows[0]["AcaId"].ToString() + "'");
        }
        catch (Exception ex)
        { }
    }

    private void SendSMSToConstructionUsers(int AcaID, string drawingName)
    {
        const int USERTYPE = 2;
        string smsTo = string.Empty;
        InchargeController conrtoller = new InchargeController();
        List<string> incharges = conrtoller.GetUsersByUserTypeAndAcademic(USERTYPE, AcaID);
        foreach (string inchargeNumber in incharges)
        {
            smsTo += inchargeNumber + ",";
        }

        string adminNumber = System.Configuration.ConfigurationManager.AppSettings["AdminToSendDrawingSMS"].ToString();
        if (btnSave.Visible == true)
        {

            {
                smsTo += adminNumber + ",";
            }
            smsTo = smsTo.Substring(0, smsTo.Length - 1);
            Utility.SendSMS(smsTo, "Drawing of " + ddlAcademy.SelectedItem.Text + " has been uploaded to www.Akalsewa.org.");
        }
        else if (btnEdit.Visible == true)
        {
            {
                smsTo += adminNumber + ",";
            }
            smsTo = smsTo.Substring(0, smsTo.Length - 1);
            Utility.SendSMS(smsTo, "Drawing of " + ddlAcademy.SelectedItem.Text + " has been Edited and Uploaded to www.Akalsewa.org.");
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
            ddlSubDrawingType.Items.Insert(0, new ListItem("SELECT SUB DRAWING TYPE", "0"));
            ddlSubDrawingType.SelectedIndex = 0;
        }
    }
    protected void ddlDwgType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubDrawingType();
    }

}