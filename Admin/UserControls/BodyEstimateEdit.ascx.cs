using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Admin_UserControls_BodyEstimateEdit : System.Web.UI.UserControl
{
    public static int UserTypeID = -1;
    private string lbEstId;
    public static int InchargeID = -1;
    public int ModuleID = -1;
    public int AcaID { get; set; }
    public bool IsApprove { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["ModuleID"] != null)
        {
            ModuleID = int.Parse(Session["ModuleID"].ToString());
            AcaID = Request.QueryString["AcaID"] != null ? int.Parse(Request.QueryString["AcaID"].ToString()) : 0;
            IsApprove = Request.QueryString["isApproved"] != null ? Convert.ToBoolean(Request.QueryString["isApproved"].ToString()) : true;
        }

        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
                UserTypeID = int.Parse(Session["UserTypeID"].ToString());
                InchargeID = int.Parse(Session["InchargeID"].ToString());
                
            }

            if (UserTypeID != 1)
            {
                //trReivew.Visible = false;
                btnUpload.Text = "Approved";
                btnRejectEdit.Text = "Re-Send Estimate";
            }
            GetEstimateDetails();
            BindGrid();
            if (Request.QueryString["IsRejected"] != null && Request.QueryString["IsRejected"].ToString() == "1")
            {
                btnRejectEdit.Visible = false;
            }
            if (Request.QueryString["EstId"] == null)
            {
                Response.Redirect("Emp_Home.aspx");
            }
            
        }

    }

    private void GetEstimateDetails()
    {
        string id = Request.QueryString["EstId"].ToString();
        DataSet dsEstimate1Details = new DataSet();
        //dsEstimate1Details = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetails  '" + ID + "'");
        dsEstimate1Details = DAL.DalAccessUtility.GetDataInDataSet("SELECT Academy.AcaName, Academy.AcaID,Zone.ZoneID, Zone.ZoneName, TypeOfWork.TypeWorkName, Estimate.EstId, Estimate.SubEstimate,CONVERT(NVARCHAR(20), Estimate.SanctionDate,107) AS SanctionDate, Estimate.Active, Estimate.CreatedBy, Estimate.CreatedOn, Estimate.EstmateCost, Estimate.ModifyOn, Estimate.ModifyBy, Academy.AcId, Zone.ZoId, WorkAllot.WorkAllotName,Estimate.IsApproved,Estimate.FilePath,Estimate.FileNme  FROM Estimate INNER JOIN  Academy ON Estimate.AcaId = Academy.AcaId INNER JOIN Zone ON Estimate.ZoneId = Zone.ZoneId INNER JOIN  TypeOfWork ON Estimate.TypeWorkId = TypeOfWork.TypeWorkId INNER JOIN  WorkAllot ON Estimate.WAId = WorkAllot.WAId where Estimate.EstId='" + id + "'");
        lblEstimateNo.Text = dsEstimate1Details.Tables[0].Rows[0]["EstId"].ToString();
        lblZoneCode.Text = dsEstimate1Details.Tables[0].Rows[0]["ZoneName"].ToString();
        lblAcaCode.Text = dsEstimate1Details.Tables[0].Rows[0]["AcaName"].ToString();
        txtSubEstimate.Text = dsEstimate1Details.Tables[0].Rows[0]["SubEstimate"].ToString();
        lblTpeofWork.Text = dsEstimate1Details.Tables[0].Rows[0]["TypeWorkName"].ToString();
        lblCreatedOn.Text = dsEstimate1Details.Tables[0].Rows[0]["CreatedOn"].ToString();
        lblEstimateCost.Text = dsEstimate1Details.Tables[0].Rows[0]["EstmateCost"].ToString();
        lblWorkName.Text = dsEstimate1Details.Tables[0].Rows[0]["WorkAllotName"].ToString();
        hdnIsApproved.Value = dsEstimate1Details.Tables[0].Rows[0]["IsApproved"].ToString();
        signedcopyView.Text = GetFileName(dsEstimate1Details.Tables[0].Rows[0]["FilePath"].ToString(), dsEstimate1Details.Tables[0].Rows[0]["FileNme"].ToString());
        BindWork(dsEstimate1Details.Tables[0].Rows[0]["AcaID"].ToString(), dsEstimate1Details.Tables[0].Rows[0]["ZoneID"].ToString());
        ddlWorkType.ClearSelection();
        BindTypeofWork();
        ddlTypeOfWork.ClearSelection();
        try
        {
            ddlWorkType.Items.FindByText(lblWorkName.Text).Selected = true;
            ddlTypeOfWork.Items.FindByText(lblTpeofWork.Text).Selected = true;
        }
        catch (Exception ex)
        { }
        
        lblWorkName.Visible = false;
        tdWorkAllot.Visible = true;
        // divbtnupload.Visible = false;
        //divuploadfile.Visible = false;

        //if (hdnIsApproved.Value == "True")
        //{
        //    gvDetails.ShowFooter = false;
        //}

    }

    protected void BindGrid()
    {
        string id = Request.QueryString["EstId"].ToString();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EDITEstimate '" + id + "'");
        if (Request.QueryString["IsRejected"] != null && Request.QueryString["IsRejected"].ToString() == "1" && hdnIsApproved.Value == "True")
        {
            DataTable dtApproved = null;

            dtApproved = (from mytable in ds.Tables[0].AsEnumerable()
                          where mytable.Field<bool>("IsItemApproved") == false
                          select mytable).CopyToDataTable();
            lblmsg.Text = "Below Items Has Been Rejected By The Purchaser. Please See The Remarks for More Information:";

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = dtApproved;
                gvDetails.DataBind();
                //UpdatePanel3.Update();
            }
            else
            {
                Response.Write("no data");
            }
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                //UpdatePanel3.Update();
            }
            else
            {
                Response.Write("no data");
            }
        }
    }

    protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvDetails.EditIndex = e.NewEditIndex;
        string ddlMatTId = ((Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblMatT")).Text;
        string lblMat = ((Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblMat")).Text;
        string ddlPs = ((Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblPs")).Text;
  
        BindGrid();
        DropDownList ddlMateType = ((DropDownList)gvDetails.Rows[e.NewEditIndex].Cells[3].FindControl("ddlMatTId"));
        DataSet dsMatType = new DataSet();
        if (ModuleID == 2)
        {
            dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1 and MatTypeId = 49");
        }
        else
        {
            dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1 order by MatTypeName");
        }
        ddlMateType.DataSource = dsMatType;
        ddlMateType.DataValueField = "MatTypeId";
        ddlMateType.DataTextField = "MatTypeName";
        ddlMateType.DataBind();
        ddlMateType.Items.Insert(0, "Material Type");
        ddlMateType.ClearSelection();

        ddlMateType.Items.FindByText(ddlMatTId).Selected = true;

        ddlMatTId_SelectedIndexChanged(ddlMateType, new EventArgs());

        DropDownList ddlMaterail = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlMatId");
        ddlMaterail.ClearSelection();
        ddlMaterail.Items.FindByText(lblMat).Selected = true;

        DropDownList ddlPurchaseSource = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlPs");
        ddlPurchaseSource.ClearSelection();
        ddlPurchaseSource.Items.FindByText(ddlPs).Selected = true;
    }

    private void SaveFiles(bool IsApproved, bool IsItemRejected)
    {
        string fileNameToSave = string.Empty;
        if (fuFile.HasFile)
        {
            foreach (HttpPostedFile postedFile in fuFile.PostedFiles)
            {
                string fileDwgname = System.IO.Path.GetFileName(postedFile.FileName);
                string fileDwgPath = System.IO.Path.GetFileName(postedFile.FileName);
                string FileDwgEx = System.IO.Path.GetExtension(postedFile.FileName);
                String FDwgNam = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
                Int64 i = 0;
                fileDwgPath = "/EstFile/" + fileDwgname;
                string dwgfilepath = "EstFile/" + Regex.Replace(FDwgNam + "-" + System.DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "-").Replace(' ', '-').ToString() + FileDwgEx;
                postedFile.SaveAs(Server.MapPath(dwgfilepath));
                dwgfilepath = dwgfilepath + ",";

                fileNameToSave += dwgfilepath;
            }
        }
        if (fileNameToSave.Length > 0)
        {
            fileNameToSave = fileNameToSave.Substring(0, fileNameToSave.Length - 1);
        }

        int empid = int.Parse(Session["InchargeID"].ToString());

        string remark = "<span style='color:red'>" + txtRemark.Text + "</span>";
        if (UserTypeID != 1)
        {
            remark = "<span style='color:green'>" + txtRemark.Text + "</span>";
        }

        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewEstimate '0','0','" + txtSubEstimate.Text + "','" + ddlTypeOfWork.SelectedValue + "',''," + empid + ",'5','" + Request.QueryString["EstId"].ToString() + "','','0.0','" + ddlWorkType.SelectedValue + "','Singed Copy','" + fileNameToSave + "'," + IsApproved + ",'" + txtRemark.Text + "'," + !IsApproved + "," + IsItemRejected);

        if (hdnIsApproved.Value.ToLower() == "false")
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Update Estimate set SanctionDate ='" + Utility.GetLocalDateTime(DateTime.UtcNow) + "' where estid = '" + Request.QueryString["EstId"].ToString() + "'");
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Update Estimate set  ModifyOn='" + Utility.GetLocalDateTime(DateTime.UtcNow) + "' where estid = '" + Request.QueryString["EstId"].ToString() + "'");
        }

        EventLog();
        DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set IsApproved = 1,remarkByPurchase='' where estid = '" + Request.QueryString["EstId"].ToString() + "'");


        if (UserTypeID == (int)(TypeEnum.UserType.ADMIN))
        {
            if (AcaID > 0)
            {
                Response.Redirect("Admin_EstimateView.aspx?AcaID=" + AcaID);
            }
            else
            {
                if (IsApprove == false)
                {
                    Response.Redirect("Admin_EstimateView.aspx?isApproved=" + IsApprove);
                }
                else
                {
                    Response.Redirect("Admin_EstimateView.aspx");
                }
            }
        }
        else if (UserTypeID == (int)(TypeEnum.UserType.TRANSPORTADMIN))
        {
            Response.Redirect("Transport_EstimateView.aspx");
        }
        else if (UserTypeID == (int)(TypeEnum.UserType.WORKSHOPADMIN))
        {
            Response.Redirect("WorkshopAdmin_EstimateView.aspx");
        }
        else if (UserTypeID == (int)(TypeEnum.UserType.TRANSPORTMANAGER) || UserTypeID == (int)(TypeEnum.UserType.BACKOFFICE) || UserTypeID == (int)(TypeEnum.UserType.TRANSPORTINCHARGE))
        {
            Response.Redirect("Transport_EstimateAcademyWise.aspx");
        }
        else
        {
            Response.Redirect("Emp_EstimateAcademyWise.aspx");
        }
    }

    private void EventLog()
    {
        EstimateLog log = new EstimateLog();
        log.EstimateNumber = Convert.ToInt32(Request.QueryString["EstId"].ToString());
        log.ModifyBy = int.Parse(Session["InchargeID"].ToString());
        log.ModifyOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        repo.AddEstimateChangeInfo(log);
    }

    protected void ddlMatTId_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatTId");
        Label UnitName = (Label)row.FindControl("lblUnitEdit");
        TextBox txtRa = (TextBox)row.FindControl("txtRate");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMatId");
        DropDownList ddlPurchaseSource = (DropDownList)row.FindControl("ddlPs");
        ddlMaterail.ClearSelection();
       
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + ddlMateType.SelectedValue + "' order by MatName asc");
        ddlMaterail.DataSource = dsMat;
        ddlMaterail.DataValueField = "MatId";
        ddlMaterail.DataTextField = "MatName";
        ddlMaterail.DataBind();
        ddlMaterail.Items.Insert(0, "Material");
        ddlMaterail.SelectedIndex = 0;


        DataTable dsSourcTypef = new DataTable();
        if (ddlMateType.SelectedValue == "83")
        {
            dsSourcTypef = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where  Active=1 and PSId=" + (int)TypeEnum.PurchaseSourceID.AkalWorkshop).Tables[0];
        }
        else
        {
            dsSourcTypef = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where  Active=1 and PSId!=" + (int)TypeEnum.PurchaseSourceID.AkalWorkshop).Tables[0];
        }
        if (dsSourcTypef != null)
        {
            ddlPurchaseSource.DataSource = dsSourcTypef;
            ddlPurchaseSource.DataValueField = "PSId";
            ddlPurchaseSource.DataTextField = "PSName";
            ddlPurchaseSource.DataBind();
            ddlPurchaseSource.Items.Insert(0, "Source Type");
            ddlPurchaseSource.SelectedIndex = 0;
        }
    }

    protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDetails.EditIndex = -1;
        BindGrid();
    }

    protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int Sno = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Value.ToString());
        Label lbEstId = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblEstIdEdit");
        DropDownList dlMatT = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlMatTId");
        DropDownList dlMat = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlMatId");

        TextBox txQty = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtQty");
        TextBox txRemarks = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtEditRemark");
        TextBox txRate = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRate");
        DropDownList dlSt = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlPs");
        Label lbUnit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblUnitEdit");
        DataSet dsUnitId = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from Unit where UnitName='" + lbUnit.Text + "'");
        int uId = Convert.ToInt32(dsUnitId.Tables[0].Rows[0]["UnitId"].ToString());
        Label lbAmt = (Label)gvDetails.Rows[e.RowIndex].FindControl("txtAmtEdit");
        //lbAmt.Text = (Convert.ToDecimal(txQty.Text) * Convert.ToDecimal(txRate.Text)).ToString();
        DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set MatTypeId='" + dlMatT.SelectedValue + "',MatId='" + dlMat.SelectedValue + "',Qty='" + txQty.Text + "',Rate='" + txRate.Text + "',PSId='" + dlSt.SelectedValue + "',UnitId='" + uId + "',Amount='" + lbAmt.Text + "',ModifyBy='" + InchargeID + "',ModifyOn='" + System.DateTime.Now.ToString("yyyy-MM-dd") + "',remarkByPurchase='" + txRemarks.Text + "' where Sno='" + Sno + "'");
        DataSet dsTotalAmt = DAL.DalAccessUtility.GetDataInDataSet("select SUM(Amount)as TtlAmt from EstimateAndMaterialOthersRelations where EstId='" + lbEstId.Text + "'");
        DAL.DalAccessUtility.ExecuteNonQuery("update Estimate set EstmateCost='" + dsTotalAmt.Tables[0].Rows[0]["TtlAmt"].ToString() + "',ModifyBy='" + InchargeID + "' where EstId='" + lbEstId.Text + "'");
        gvDetails.EditIndex = -1;
        GetEstimateDetails();
        BindGrid();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Row Update Successfully.');", true);
    }

    protected void ddlMatId_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        Label UnitName = (Label)row.FindControl("lblUnitEdit");
        TextBox txtRa = (TextBox)row.FindControl("txtRate");
        TextBox txtQt = (TextBox)row.FindControl("txtQty");
        DropDownList dlMatT = (DropDownList)row.FindControl("ddlMatTId");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMatId");
        Label lblAm = (Label)row.FindControl("txtAmtEdit");
        DataSet dsUName = DAL.DalAccessUtility.GetDataInDataSet("SELECT Unit.UnitName,Material.MatCost,Material.LocalRate,Material.AkalWorkshopRate FROM Material INNER JOIN Unit ON Material.UnitId = Unit.UnitId where Material.MatId='" + ddlMaterail.SelectedValue + "'");
        UnitName.Text = dsUName.Tables[0].Rows[0]["UnitName"].ToString();
        if (dlMatT.SelectedValue == "83")
        {
            txtRa.Text = dsUName.Tables[0].Rows[0]["AkalWorkshopRate"].ToString();
        }
        else
        {
            txtRa.Text = dsUName.Tables[0].Rows[0]["MatCost"].ToString();
        }
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        decimal am = qt * ra;
        lblAm.Text = string.Format("{0:#.00}", am.ToString());
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQty");
        TextBox txtRa = (TextBox)row.FindControl("txtRate");
        Label lblAm = (Label)row.FindControl("txtAmtEdit");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        decimal am = qt * ra;
        lblAm.Text = string.Format("{0:#.00}", am.ToString());
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            Label lbEstId = (Label)gvDetails.FooterRow.FindControl("lblEstIdFooter");
            string EId = lbEstId.Text;
            string id = Request.QueryString["EstId"].ToString();
            DropDownList dlMatT = (DropDownList)gvDetails.FooterRow.FindControl("ddlMatTIdFooter");
            DropDownList dlMat = (DropDownList)gvDetails.FooterRow.FindControl("ddlMatIdFooter");

            TextBox txQty = (TextBox)gvDetails.FooterRow.FindControl("txtQtyFooter");
            TextBox txRate = (TextBox)gvDetails.FooterRow.FindControl("txtRateFooter");
            DropDownList ddlPsFooter = (DropDownList)gvDetails.FooterRow.FindControl("ddlPsFooter");
            Label lbUnit = (Label)gvDetails.FooterRow.FindControl("lblUnitFooter");
            if (lbUnit.Text == null)
            {

            }
            else
            {
                DataSet dsUnitId = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from Unit where UnitName='" + lbUnit.Text + "'");
                int uId = Convert.ToInt32(dsUnitId.Tables[0].Rows[0]["UnitId"].ToString());
                Label lbAmt = (Label)gvDetails.FooterRow.FindControl("lblAmtFooter");
                DAL.DalAccessUtility.ExecuteNonQuery("insert into EstimateAndMaterialOthersRelations(EstId,MatTypeId,MatId,PSId,Qty,UnitId,Rate,Amount,Active,CreatedBy,CreatedOn,PurchaseEmpID,VendorID,PurchaseQty,DispatchStatus,MRP,Discount,Vat,DirectPurchase,ModifyOn,ModifyBy) values ('" + id + "','" + dlMatT.SelectedValue + "','" + dlMat.SelectedValue + "','" + ddlPsFooter.SelectedValue + "','" + txQty.Text + "','" + uId + "','" + txRate.Text + "','" + lbAmt.Text + "','1','" + InchargeID + "','" + Utility.GetLocalDateTime(DateTime.UtcNow) + "',0,0,0,0,0,0,0,'" + false + "','" + Utility.GetLocalDateTime(DateTime.UtcNow) + "','" + InchargeID + "')");
                DataSet dsTotalAmt = DAL.DalAccessUtility.GetDataInDataSet("select SUM(Amount)as TtlAmt from EstimateAndMaterialOthersRelations where EstId='" + id + "'");
                DAL.DalAccessUtility.ExecuteNonQuery("update Estimate set EstmateCost='" + dsTotalAmt.Tables[0].Rows[0]["TtlAmt"].ToString() + "',ModifyBy='" + InchargeID + "',ModifyOn='" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' where EstId='" + id + "'");
                GetEstimateDetails();
                BindGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Row Insert Successfully.');", true);
            }
        }
        else if (e.CommandName.Equals("Delete"))
        {
            string ID = e.CommandArgument.ToString();
            DAL.DalAccessUtility.ExecuteNonQuery("DELETE FROM EstimateAndMaterialOthersRelations WHERE Sno=" + ID);
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Row deleted Successfully.');", true);
            Response.Redirect("Admin_EstimateEdit.aspx?EstId=" + Request.QueryString["EstId"].ToString());
        }
    }

    protected void ddlMatTIdFooter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatTIdFooter");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMatIdFooter");
        Label UnitName = (Label)row.FindControl("lblUnitFooter");
        TextBox txtRa = (TextBox)row.FindControl("txtRateFooter");
        DropDownList ddlPurchaseSource = (DropDownList)row.FindControl("ddlPsFooter");
        ddlMaterail.ClearSelection();
      

        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + ddlMateType.SelectedValue + "' order by MatName asc");
        ddlMaterail.DataSource = dsMat;
        ddlMaterail.DataValueField = "MatId";
        ddlMaterail.DataTextField = "MatName";
        ddlMaterail.DataBind();
        ddlMaterail.Items.Insert(0, "Material");
        ddlMaterail.SelectedIndex = 0;


        DataTable dsSourcTypef = new DataTable();
            if (ddlMateType.SelectedValue == "83")
            {
                dsSourcTypef = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where  Active=1 and PSId=" + (int)TypeEnum.PurchaseSourceID.AkalWorkshop).Tables[0];
            }
            else
            {
                dsSourcTypef = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where  Active=1 and PSId!=" + (int)TypeEnum.PurchaseSourceID.AkalWorkshop).Tables[0];
            }
        if (dsSourcTypef != null)
        {
            ddlPurchaseSource.DataSource = dsSourcTypef;
            ddlPurchaseSource.DataValueField = "PSId";
            ddlPurchaseSource.DataTextField = "PSName";
            ddlPurchaseSource.DataBind();
            ddlPurchaseSource.Items.Insert(0, "Source Type");
            ddlPurchaseSource.SelectedIndex = 0;
        }
    }

    protected void ddlMatIdFooter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        Label UnitName = (Label)row.FindControl("lblUnitFooter");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMatIdFooter");
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatTIdFooter");
        TextBox txtQt = (TextBox)row.FindControl("txtQtyFooter");
        TextBox txtRa = (TextBox)row.FindControl("txtRateFooter");
        Label lblAm = (Label)row.FindControl("lblAmtFooter");
        DataSet dsUName = DAL.DalAccessUtility.GetDataInDataSet("SELECT Unit.UnitName,Material.MatCost,Material.LocalRate,Material.AkalWorkshopRate FROM Material INNER JOIN Unit ON Material.UnitId = Unit.UnitId where Material.MatId='" + ddlMaterail.SelectedValue + "'");
        UnitName.Text = dsUName.Tables[0].Rows[0]["UnitName"].ToString();
        if (ddlMateType.SelectedValue == "83")
        {
            txtRa.Text = dsUName.Tables[0].Rows[0]["AkalWorkshopRate"].ToString();
        }
        else
        {
            txtRa.Text = dsUName.Tables[0].Rows[0]["MatCost"].ToString();
        }
        //decimal qt = Convert.ToDecimal(txtQt.Text);
        //decimal ra = Convert.ToDecimal(txtRa.Text);
        //decimal am = qt * ra;
        //lblAm.Text = string.Format("{0:#.00}", am.ToString());
    }

    protected void txtRateFooter_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQtyFooter");
        TextBox txtRa = (TextBox)row.FindControl("txtRateFooter");
        Label lblAm = (Label)row.FindControl("lblAmtFooter");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        decimal am = qt * ra;
        lblAm.Text = am.ToString();
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataSet dsMatTypef = new DataSet();
        if (ModuleID == 2)
        {
            dsMatTypef = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1 and MatTypeId = 49");
        }
        else
        {
            dsMatTypef = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1 order by MatTypeName asc");
        }
        DataSet dsSourcTypef = new DataSet();
        dsSourcTypef = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where Active=1");
       
        DataSet dsremarks = new DataSet();

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlMateTypef = ((DropDownList)e.Row.FindControl("ddlMatTIdFooter"));
            //DataSet dsMatTypef = new DataSet();
            //dsMatTypef = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1");
            ddlMateTypef.DataSource = dsMatTypef;
            ddlMateTypef.DataValueField = "MatTypeId";
            ddlMateTypef.DataTextField = "MatTypeName";
            ddlMateTypef.DataBind();
            ddlMateTypef.Items.Insert(0, "Material Type");
            ddlMateTypef.SelectedIndex = 0;

            DropDownList ddlSourceTypef = ((DropDownList)e.Row.FindControl("ddlPsFooter"));
            //DataSet dsSourcTypef = new DataSet();
            //dsSourcTypef = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where Active=1");
            ddlSourceTypef.DataSource = dsSourcTypef;
            ddlSourceTypef.DataValueField = "PSId";
            ddlSourceTypef.DataTextField = "PSName";
            ddlSourceTypef.DataBind();
            ddlSourceTypef.Items.Insert(0, "Source Type");
            ddlSourceTypef.SelectedIndex = 0;

        

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool IsItemRejected = Request.QueryString["IsRejected"] != null ? true : false;
            if (!IsItemRejected && Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "PurchaseEmpID").ToString()) != 0)
            {
                ImageButton imgbtnEdit = (ImageButton)e.Row.FindControl("imgbtnEdit");
                ImageButton imgbtnDelete = (ImageButton)e.Row.FindControl("imgbtnDelete");
                imgbtnEdit.Visible = imgbtnDelete.Visible = false;
                btnRejectEdit.Visible = false;
            }
        }

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        bool IsApproved = false;
        bool IsItemRejected = true;
        if (UserTypeID == (int)(TypeEnum.UserType.ADMIN) || UserTypeID == (int)(TypeEnum.UserType.CONSTRUCTION) || UserTypeID == (int)(TypeEnum.UserType.TRANSPORTADMIN) || UserTypeID == (int)(TypeEnum.UserType.WORKSHOPADMIN))
        {
            IsApproved = ((Button)sender).ID == "btnRejectEdit" ? false : true;
            IsItemRejected = ((Button)sender).ID == "btnRejectEdit" ? true : false;
        }

        SaveFiles(IsApproved, IsItemRejected);
    }

    private void SendSMS(string AcaID, bool IsPurchase, bool IsWorkshop)
    {
        const int CONST_USERTYPE = 2;
        const int PURC_USERTYPE = 4;
        const int WORKSHOP_USERTYPE = 6;

        string smsTo = string.Empty;
        InchargeController conrtoller = new InchargeController();
        List<string> incharges = conrtoller.GetUsersByUserTypeAndAcademic(CONST_USERTYPE, int.Parse(AcaID));
        foreach (string inchargeNumber in incharges)
        {
            smsTo += inchargeNumber + ",";
        }

        string adminNumber = System.Configuration.ConfigurationManager.AppSettings["AdminToSendDrawingSMS"].ToString();
        if (adminNumber != string.Empty)
        {
            smsTo += adminNumber + ",";
        }
        List<Incharge> InchargeInfo = conrtoller.GetUsersByUserType(PURC_USERTYPE);

        // Purchase 
        if (IsPurchase)
        {
            foreach (Incharge inchargeNumber in InchargeInfo)
            {
                smsTo += inchargeNumber.InMobile + ",";
            }
        }

        if (IsWorkshop)
        {
            InchargeInfo = conrtoller.GetUsersByUserType(WORKSHOP_USERTYPE);
            foreach (Incharge inchargeNumber in InchargeInfo)
            {
                smsTo += inchargeNumber.InMobile + ",";
            }
        }
        smsTo = smsTo.Substring(0, smsTo.Length - 1);
        Utility.SendSMS(smsTo, "Estimate Number " + lbEstId + " has been Edited and Re-Uploaded please check www.Akalsewa.org.");

    }

    protected void BindWork(string AcaID, string Zone)
    {
        DataSet dsWork = new DataSet();
        dsWork = DAL.DalAccessUtility.GetDataInDataSet("select WAId,WorkAllotName from WorkAllot where AcaId='" + AcaID + "' and ZoneId='" + Zone + "' and active=1");
        ddlWorkType.DataSource = dsWork;
        ddlWorkType.DataValueField = "WAId";
        ddlWorkType.DataTextField = "WorkAllotName";
        ddlWorkType.DataBind();
        ddlWorkType.Items.Insert(0, "Select Work Allot");
        ddlWorkType.SelectedIndex = 0;
    }

    private string GetFileName(string filepaths, string fileName)
    {
        string anchorLink = string.Empty;
        string[] filePath = filepaths.Split(',');
        int count = 0;
        foreach (string path in filePath)
        {
            count++;
            anchorLink += "<a href='" + path + "' target='_blank'>" + fileName + "_" + count + "</a> , ";
        }

        return anchorLink.Substring(0, anchorLink.Length - 3);

    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQty");
        TextBox txtRa = (TextBox)row.FindControl("txtRate");
        Label lblAm = (Label)row.FindControl("txtAmtEdit");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        decimal am = qt * ra;
        lblAm.Text = string.Format("{0:#.00}", am.ToString());
    }

    protected void txtQtyFooter_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQtyFooter");
        TextBox txtRa = (TextBox)row.FindControl("txtRateFooter");
        Label lblAm = (Label)row.FindControl("lblAmtFooter");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        decimal am = qt * ra;
        lblAm.Text = string.Format("{0:#.00}", am.ToString());
    }

    protected void BindTypeofWork()
    {
        DataTable dsWork = new DataTable();
        dsWork = DAL.DalAccessUtility.GetDataInDataSet("select TypeWorkId,TypeWorkName from TypeOfWork Where Active=1 order by TypeWorkName").Tables[0];
        if (dsWork != null && dsWork.Rows.Count > 0)
        {
            ddlTypeOfWork.DataSource = dsWork;
            ddlTypeOfWork.DataValueField = "TypeWorkId";
            ddlTypeOfWork.DataTextField = "TypeWorkName";
            ddlTypeOfWork.DataBind();
            ddlTypeOfWork.Items.Insert(0, new ListItem("--Select Type of Work--", "0"));
            ddlTypeOfWork.SelectedIndex = 0;
        }
    }

}
