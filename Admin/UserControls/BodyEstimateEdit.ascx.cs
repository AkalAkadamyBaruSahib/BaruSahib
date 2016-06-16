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

    protected void Page_Load(object sender, EventArgs e)
    {
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
                btnUpload.Text = "Save Changes";
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
            else
            {
                Request.QueryString["EstId"].ToString();
                //GetEstimateDetails(Request.QueryString["EstId"].ToString());
            }
        }

    }

    private void GetEstimateDetails()
    {
        string id = Request.QueryString["EstId"].ToString();
        DataSet dsEstimate1Details = new DataSet();
        //dsEstimate1Details = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetails  '" + ID + "'");
        dsEstimate1Details = DAL.DalAccessUtility.GetDataInDataSet("SELECT Academy.AcaName, Academy.AcaID,Zone.ZoneID, Zone.ZoneName, TypeOfWork.TypeWorkName, Estimate.EstId, Estimate.SubEstimate,CONVERT(NVARCHAR(20), Estimate.ModifyOn,107) AS SanctionDate, Estimate.Active, Estimate.CreatedBy, Estimate.CreatedOn, Estimate.EstmateCost, Estimate.ModifyOn, Estimate.ModifyBy, Academy.AcId, Zone.ZoId, WorkAllot.WorkAllotName,Estimate.IsApproved  FROM Estimate INNER JOIN  Academy ON Estimate.AcaId = Academy.AcaId INNER JOIN Zone ON Estimate.ZoneId = Zone.ZoneId INNER JOIN  TypeOfWork ON Estimate.TypeWorkId = TypeOfWork.TypeWorkId INNER JOIN  WorkAllot ON Estimate.WAId = WorkAllot.WAId where Estimate.EstId='" + id + "'");
        lblEstimateNo.Text = dsEstimate1Details.Tables[0].Rows[0]["EstId"].ToString();
        lblZoneCode.Text = dsEstimate1Details.Tables[0].Rows[0]["ZoneName"].ToString();
        //lblZoneCode.Text = dsEstimate1Details.Tables[0].Rows[0]["ZoId"].ToString();
        lblAcaCode.Text = dsEstimate1Details.Tables[0].Rows[0]["AcaName"].ToString();
        //lblAcaCode.Text = dsEstimate1Details.Tables[0].Rows[0]["AcId"].ToString();
        lblSubEstimate.Text = dsEstimate1Details.Tables[0].Rows[0]["SubEstimate"].ToString();
        lblTpeofWork.Text = dsEstimate1Details.Tables[0].Rows[0]["TypeWorkName"].ToString();
        lblSanctiondate.Text = dsEstimate1Details.Tables[0].Rows[0]["SanctionDate"].ToString();
        lblEstimateCost.Text = dsEstimate1Details.Tables[0].Rows[0]["EstmateCost"].ToString();
        lblWorkName.Text = dsEstimate1Details.Tables[0].Rows[0]["WorkAllotName"].ToString();
        hdnIsApproved.Value = dsEstimate1Details.Tables[0].Rows[0]["IsApproved"].ToString();
        BindWork(dsEstimate1Details.Tables[0].Rows[0]["AcaID"].ToString(), dsEstimate1Details.Tables[0].Rows[0]["ZoneID"].ToString());
        ddlWorkType.ClearSelection();
        try
        {
            ddlWorkType.Items.FindByText(lblWorkName.Text).Selected = true;
        }
        catch (Exception ex)
        { }
        lblWorkName.Visible = false;
        tdWorkAllot.Visible = true;
        // divbtnupload.Visible = false;
        //divuploadfile.Visible = false;
        if (hdnIsApproved.Value == "True")
        {
            gvDetails.ShowFooter = false;
        }

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
        dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1");
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

        DropDownList ddlSourceType = ((DropDownList)gvDetails.Rows[e.NewEditIndex].Cells[8].FindControl("ddlPs"));
        DataSet dsSourcType = new DataSet();
        dsSourcType = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where Active=1");
        ddlSourceType.DataSource = dsSourcType;
        ddlSourceType.DataValueField = "PSId";
        ddlSourceType.DataTextField = "PSName";
        ddlSourceType.DataBind();
        ddlSourceType.Items.Insert(0, "Source Type");
        ddlSourceType.Items.FindByText(ddlPs).Selected = true;
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

        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewEstimate '0','0','0','0',''," + empid + ",'5','" + Request.QueryString["EstId"].ToString() + "','','0.0','" + ddlWorkType.SelectedValue + "','Singed Copy','" + fileNameToSave + "'," + IsApproved + ",'" + txtRemark.Text + "'," + !IsApproved + "," + IsItemRejected);
        DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set IsApproved = 1,remarkByPurchase='' where estid = '" + Request.QueryString["EstId"].ToString() + "'");


        if (UserTypeID == 1 || UserTypeID == 21)
        {
            Response.Redirect("Admin_EstimateView.aspx");
        }
        else
        {
            Response.Redirect("Emp_EstimateAcademyWise.aspx");
        }
    }

    protected void ddlMatTId_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatTId");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMatId");
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + ddlMateType.SelectedValue + "'");
        ddlMaterail.DataSource = dsMat;
        ddlMaterail.DataValueField = "MatId";
        ddlMaterail.DataTextField = "MatName";
        ddlMaterail.DataBind();
        ddlMaterail.Items.Insert(0, "Material");
        ddlMaterail.SelectedIndex = 0;
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
        DAL.DalAccessUtility.ExecuteNonQuery("update Estimate set EstmateCost='" + dsTotalAmt.Tables[0].Rows[0]["TtlAmt"].ToString() + "',ModifyBy='" + InchargeID + "',ModifyOn='" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' where EstId='" + lbEstId.Text + "'");
        gvDetails.EditIndex = -1;
        GetEstimateDetails();
        BindGrid();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Row Update Successfully.');", true);
    }

    protected void ddlMatId_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        Label UnitName = (Label)row.FindControl("lblUnitEdit");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMatId");
        DataSet dsUName = DAL.DalAccessUtility.GetDataInDataSet("SELECT Unit.UnitName FROM Material INNER JOIN Unit ON Material.UnitId = Unit.UnitId where Material.MatId='" + ddlMaterail.SelectedValue + "'");
        UnitName.Text = dsUName.Tables[0].Rows[0]["UnitName"].ToString();
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
        lblAm.Text = am.ToString();
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
            DropDownList dlSt = (DropDownList)gvDetails.FooterRow.FindControl("ddlPsFooter");
            Label lbUnit = (Label)gvDetails.FooterRow.FindControl("lblUnitFooter");
            if (lbUnit.Text == null)
            {

            }
            else
            {
                DataSet dsUnitId = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from Unit where UnitName='" + lbUnit.Text + "'");
                int uId = Convert.ToInt32(dsUnitId.Tables[0].Rows[0]["UnitId"].ToString());
                Label lbAmt = (Label)gvDetails.FooterRow.FindControl("lblAmtFooter");
                DAL.DalAccessUtility.ExecuteNonQuery("insert into EstimateAndMaterialOthersRelations(EstId,MatTypeId,MatId,PSId,Qty,UnitId,Rate,Amount,Active,CreatedBy,CreatedOn,PurchaseEmpID) values ('" + id + "','" + dlMatT.SelectedValue + "','" + dlMat.SelectedValue + "','" + dlSt.SelectedValue + "','" + txQty.Text + "','" + uId + "','" + txRate.Text + "','" + lbAmt.Text + "','1','" + InchargeID + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "',0)");
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
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + ddlMateType.SelectedValue + "' order by MatName asc");
        ddlMaterail.DataSource = dsMat;
        ddlMaterail.DataValueField = "MatId";
        ddlMaterail.DataTextField = "MatName";
        ddlMaterail.DataBind();
        ddlMaterail.Items.Insert(0, "Material");
        ddlMaterail.SelectedIndex = 0;
    }

    protected void ddlMatIdFooter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        Label UnitName = (Label)row.FindControl("lblUnitFooter");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMatIdFooter");
        DataSet dsUName = DAL.DalAccessUtility.GetDataInDataSet("SELECT Unit.UnitName FROM Material INNER JOIN Unit ON Material.UnitId = Unit.UnitId where Material.MatId='" + ddlMaterail.SelectedValue + "'");
        UnitName.Text = dsUName.Tables[0].Rows[0]["UnitName"].ToString();
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
        dsMatTypef = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1");
        DataSet dsSourcTypef = new DataSet();
        dsSourcTypef = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where Active=1");
        DataSet dsremarks = new DataSet();
        //dsremarks = DAL.DalAccessUtility.GetDataInDataSet("select remarkByPurchase from EstimateAndMaterialOthersRelations where Active=1");


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
            }
        }

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        bool IsApproved = false;
        bool IsItemRejected = true;
        if (UserTypeID == 1 || UserTypeID == 21 || UserTypeID == 2)
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

}
   
