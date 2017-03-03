using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using IronPdf;
using System.Globalization;
using System.Collections;
public partial class Emp_BillSubmit : System.Web.UI.Page
{
    private int BillID = -1;
    private int inchargeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {  
       inchargeID = Convert.ToInt16(Session["InchargeID"].ToString());
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
                hdnInchargeID.Value = Session["InchargeID"].ToString(); 
               
               
            }
            if (Request.QueryString["AcaId"] != null)
            {
                BindNameOfWork(Request.QueryString["AcaId"].ToString());
                hdnAcaID.Value = Request.QueryString["AcaId"].ToString();
                DataTable dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId from Academy where AcaId='" + hdnAcaID.Value + "'").Tables[0];
                hdnZoneID.Value = dsZone.Rows[0]["ZoneId"].ToString();
            }

         
            BindBillType();
            ddlNameOfWork.Visible = false;
            ddlBillType.Visible = false;
            ddlEsimate.Visible = false;
            trMatSelect.Visible = false;
            pnlEstimateDetails.Visible = false;

            if (Request.QueryString["BillID"] != null && Request.QueryString["AcaId"] != null)
            {
                BillID = int.Parse(Request.QueryString["BillID"]);
                ShowBillDetails(Request.QueryString["BillID"].ToString());
            }
           
        }

        if (Request.QueryString["BillID"] != null && Request.QueryString["AcaId"] != null)
        {
            BillID = int.Parse(Request.QueryString["BillID"]);
        }

    }

    

    protected void BindNameOfWork(string id)
    {
        DataTable dsWa = new DataTable();
        dsWa = DAL.DalAccessUtility.GetDataInDataSet("select WAId,WorkAllotName from WorkAllot where AcaId ='" + id + "' and Active=1").Tables[0];
        if (dsWa != null && dsWa.Rows.Count > 0)
        {
            ddlNameOfWork.DataSource = dsWa;
            ddlNameOfWork.DataValueField = "WAId";
            ddlNameOfWork.DataTextField = "WorkAllotName";
            ddlNameOfWork.DataBind();
            ddlNameOfWork.Items.Insert(0, new ListItem("--Select Name Of Work--", "0"));
            ddlNameOfWork.SelectedIndex = 0;
        }
    }

    protected void BindBillType()
    {
        DataTable dsBillType = new DataTable();
        dsBillType = DAL.DalAccessUtility.GetDataInDataSet("select BillTypeId,BillTypeName from BillType where Active=1").Tables[0];
        if (dsBillType != null && dsBillType.Rows.Count > 0)
        {
            ddlBillType.DataSource = dsBillType;
            ddlBillType.DataValueField = "BillTypeId";
            ddlBillType.DataTextField = "BillTypeName";
            ddlBillType.DataBind();
            ddlBillType.SelectedIndex = 0;
        }
    }

    protected void BindEstimate()
    {
        string AcaId = Request.QueryString["AcaId"];
        DataTable dsAcademy = new DataTable();
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateNo '" + AcaId + "'").Tables[0];
        if (dsAcademy.Rows.Count > 0 && dsAcademy != null)
        {
            ddlEsimate.DataSource = dsAcademy;
            ddlEsimate.DataValueField = "EstId";
            ddlEsimate.DataTextField = "EstNo";
            ddlEsimate.DataBind();
            ddlEsimate.Items.Insert(0, new ListItem("--Select Estimate--", "0"));
            ddlEsimate.SelectedIndex = 0;
        }
    }

    protected void ddlBillType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBillType1.SelectedValue == "1")
        {
            pnlEstimateDetails.Visible = false;
            btnAmtTotal.Visible = false;
            lblNameWork.Visible = true;
            ddlNameOfWork.Visible = true;
            trRemarks.Visible = true;
            pnlNonSanction.Visible = false;
            btnSubmit.Visible = false;
            btnShowData.Visible = true;
        }
        else if (ddlBillType1.SelectedValue == "2")
        {
            lblChargeable.Visible = false;
            lblNameWork.Visible = false;
            ddlBillType.Visible = false;
            ddlEsimate.Visible = false;
            pnlSanction.Visible = false;
            pnlNonSanction.Visible = true;
            pnlEstimateDetails.Visible = true;
            trMatSelect.Visible = false;
            btnAmtTotal.Visible = false;
            divFinalButtons.Visible = true;
            trRemarks.Visible = false;
            btnShowData.Visible = false;
            ddlNameOfWork.Visible = false;

            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "BillSumitRateCondition();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Chargeable to.');", true);
        }
    }

    protected void BindMat()
    {
        DataSet dsAcademy = new DataSet();
        pnlEstimateDetails.Visible = false;
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_getEstimateBalanceNew'" + ddlNameOfWork.SelectedValue + "','1'");
        if (dsAcademy.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = dsAcademy;
            GridView1.DataBind();
            ViewState["MaterialBalance"] = dsAcademy.Tables[0];
        }
    }

    protected void ddlEsimate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsDes = new DataSet();
        dsDes = DAL.DalAccessUtility.GetDataInDataSet("SELECT Estimate.SubEstimate, WorkAllot.WorkAllotName FROM Estimate INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId where EstId='" + ddlEsimate.SelectedValue + "'");
        trMatSelect.Visible = true;
        lblNameOfWork.Visible = true;
        lblNameWork.Visible = true;
        lblNameOfWork.Text = dsDes.Tables[0].Rows[0]["WorkAllotName"].ToString();
        BindMat();
    }

    protected void ddlMatType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatType");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMat");
        TextBox txtItemN = (TextBox)row.FindControl("txtItmName");
        //DropDownList ddlUni = (DropDownList)row.FindControl("ddlUnit");
        TextBox txtUni = (TextBox)row.FindControl("txtUnit");
        if (ddlMateType.SelectedItem.Text == "OTHERS")
        {
            ddlMaterail.Visible = false;
            txtItemN.Visible = true;
            // ddlUni.Visible = false;
            txtUni.Visible = true;
        }
        else
        {
            DataSet dsMat = new DataSet();
            dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + ddlMateType.SelectedValue + "'");
            ddlMaterail.DataSource = dsMat;
            ddlMaterail.DataValueField = "MatId";
            ddlMaterail.DataTextField = "MatName";
            ddlMaterail.DataBind();
            ddlMaterail.Items.Insert(0, "Material");
            ddlMaterail.SelectedIndex = 0;
        }
    }

    protected void ddlMat_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMat");
        TextBox txtItem = (TextBox)row.FindControl("txtItmName");
        Label lblUnit = (Label)row.FindControl("lblNonSUnit");
        DataSet dsUnit = DAL.DalAccessUtility.GetDataInDataSet("SELECT Unit.UnitName FROM Material INNER JOIN Unit ON Material.UnitId = Unit.UnitId where Material.MatId='" + ddlMaterail.SelectedValue + "'");
        lblUnit.Text = dsUnit.Tables[0].Rows[0]["UnitName"].ToString();
        if (ddlMaterail.SelectedItem.Text == "OTHERS")
        {
            ddlMaterail.Visible = false;
            txtItem.Visible = true;
        }
        else
        {
            ddlMaterail.Visible = true;
            txtItem.Visible = false;
        }
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQty");
        TextBox txtRa = (TextBox)row.FindControl("txtRate");
        Label lblAm = (Label)row.FindControl("lblAmt");
        //Label lblMId = (Label)row.FindControl("txtMatId");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        //decimal mi = Convert.ToDecimal(lblMId.Text);
        decimal am = qt * ra;
        lblAm.Text = am.ToString();
    }

   
    protected void ddlBillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBillType.SelectedValue == "1")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "4")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "5")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "6")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "7")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "8")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "9")
        {
            ddlNameOfWork.Visible = true;
        }
        else
        {
            //ddlNameOfWork.SelectedValue = "0";
        }
    }

    private void SetInitialRowEstimateDataLoad()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("SerialNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("GateEntryNo", typeof(string)));
        dt.Columns.Add(new DataColumn("StockEntryNo", typeof(string)));
        dt.Columns.Add(new DataColumn("MatId", typeof(string)));
        dt.Columns.Add(new DataColumn("MatName", typeof(string)));
        dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitId", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
        dt.Columns.Add(new DataColumn("Rate", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("Remark", typeof(string)));
        dt.Columns.Add(new DataColumn("EstQty", typeof(string)));
        dt.Columns.Add(new DataColumn("PurQty", typeof(string)));
        dt.Columns.Add(new DataColumn("BalQty", typeof(string)));
        dt.Columns.Add(new DataColumn("EstRate", typeof(string)));


        dr = dt.NewRow();
        dr["SerialNumber"] = 1;
        dr["GateEntryNo"] = "";
        dr["StockEntryNo"] = "";
        dr["MatId"] = "";
        dr["MatName"] = "";
        dr["Quantity"] = "";
        dr["UnitId"] = "";
        dr["UnitName"] = "";
        dr["Rate"] = "";
        dr["Amount"] = "";
        dr["Remark"] = "";
        dr["EstQty"] = "";
        dr["PurQty"] = "";
        dr["BalQty"] = "";
        dr["EstRate"] = "";

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentEstimateTable"] = dt;

        gvAddItems2.DataSource = dt;
        gvAddItems2.DataBind();

    }

    protected void btnShowData_Click(object sender, EventArgs e)
    {
        //string data = "";
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("MatId", typeof(string)));
        dt.Columns.Add(new DataColumn("MatName", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitId", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
        dt.Columns.Add(new DataColumn("EstQty", typeof(string)));
        dt.Columns.Add(new DataColumn("PurQty", typeof(string)));
        dt.Columns.Add(new DataColumn("BalQty", typeof(string)));
        dt.Columns.Add(new DataColumn("EstRate", typeof(string)));
        

        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                CheckBox chkRow = (CheckBox)row.Cells[0].FindControl("chkCtrl");
                HiddenField hdnMatID = (HiddenField)row.Cells[0].FindControl("hdnMatID");
                HiddenField hdnUnitId = (HiddenField)row.Cells[0].FindControl("hdnUnitId");
                if (chkRow.Checked)
                {
                    dr = dt.NewRow();
                    dr["MatId"] = hdnMatID.Value;
                    dr["MatName"] = row.Cells[1].Text;
                    dr["UnitId"] = hdnUnitId.Value;
                    dr["UnitName"] = row.Cells[2].Text;
                    dr["EstQty"] = row.Cells[3].Text;
                    dr["PurQty"] = row.Cells[4].Text;
                    dr["BalQty"] = row.Cells[5].Text;
                    dr["EstRate"] = row.Cells[6].Text;
                    dt.Rows.Add(dr);

                    //data = data + dr["MatName"] + ",";
                }
                //dr["UnitId"] = row.Cells[3].Text;
                //dr["UnitName"] = row.Cells[4].Text;
            }

        }
        dt.AcceptChanges();
        //Session["Data"] = dt;
        //lblData.Text = "You Select " + data;
        SetInitialRowEstimateDataLoad();
        gvAddItems2.DataSource = dt;
        gvAddItems2.DataBind();

        trMatSelect.Visible = false;
        pnlEstimateDetails.Visible = true;
        pnlSanction.Visible = true;
        divFinalButtons.Visible = true;
        btnSubmit.Visible = true;
        // pnlSanction.Visible = false;
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string AcaId = Request.QueryString["AcaId"];
        var AgencyName = Request.Form["txtAgencyName"];
        DataSet dsZ = new DataSet();
        dsZ = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId from Academy where AcaId='" + AcaId + "'");
        string ZoneId = dsZ.Tables[0].Rows[0]["ZoneId"].ToString();
        if (ddlBillType1.SelectedValue == ((int)TypeEnum.BillType.Sanctioned).ToString())
        {
            string FileEx = System.IO.Path.GetExtension(fileAgencyBill.FileName);
            AgencyName = AgencyName.Replace(" ", "");
            AgencyName = AgencyName.Replace("/", "");
            string vendorBillpath = Server.MapPath("~/Bills/VendorBill/" + AgencyName.Trim() + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + txtAgenyBillNo.Text + FileEx);
            fileAgencyBill.PostedFile.SaveAs(vendorBillpath);
            string fileName = "VendorBill/" + AgencyName.Trim() + "_" + BillID + FileEx;

            if (BillID > 0)
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUser " + BillID + ",'" + ddlBillType1.SelectedValue + "','" + txtBillDate.Text + "','" + txtGateEntryNo.Text + "','','" + AgencyName + "','" + txtRemark.Text + "','2','1','" + inchargeID + "','" + -1 + "','" + AcaId + "','" + ZoneId + "','','" + ddlBillType1.SelectedValue + "','" + ddlNameOfWork.SelectedValue + "'," + txtAgenyBillNo.Text + ",'" + fileName + "','" + hdnVandorID.Value + "'");
            }
            else
            {
                Hashtable param = new Hashtable();
                param.Add("SubBillId", string.Empty);
                param.Add("BillTYpeIdChargeableTo", ddlBillType1.SelectedValue);
                param.Add("BillDate", txtBillDate.Text);
                param.Add("GateEntyNo", txtGateEntryNo.Text);
                param.Add("StockEnteryNo", string.Empty);
                param.Add("AgencyName", AgencyName);
                param.Add("Remark", txtRemark.Text);
                param.Add("type", 1);
                param.Add("Active", 1);
                param.Add("CreatedModifyBy", inchargeID);
                param.Add("EstId", string.Empty);
                param.Add("AcaId", AcaId);
                param.Add("ZoneId", ZoneId);
                param.Add("NatureOfBill", string.Empty);
                param.Add("BillTypeText", ddlBillType1.SelectedValue);
                param.Add("NameOfWork", ddlNameOfWork.SelectedValue);
                param.Add("VendorBillNumber", txtAgenyBillNo.Text);
                param.Add("VendorBillPath", fileName);
                param.Add("VendorID", hdnVandorID.Value);

                int billID = DAL.DalAccessUtility.GetDataInScaler("USP_NewSubmitBillByUser", param);

                fileName = "VendorBill/" + AgencyName.Trim() + "_" + billID + FileEx;
                DAL.DalAccessUtility.ExecuteNonQuery("Update SubmitBillByUser set VendorBillPath='" + fileName + "' where SubBillId=" + billID);
            }


            string MatId, Material, Qty, UnitId, UnitName, Rate, StockEntryNo, EstQty1, vat;
            decimal SubTotal = 0;
            decimal totalIncludeVat = 0;
            decimal TotalBillAmount = 0;
            foreach (GridViewRow gvrow in gvAddItems2.Rows)
            {
                HiddenField lblMatId = (HiddenField)gvrow.FindControl("txtMatId");
                Label lblMatName = (Label)gvrow.FindControl("txtMatName");
                TextBox txtqt = (TextBox)gvrow.FindControl("txtQty");
                HiddenField lblUnitId = (HiddenField)gvrow.FindControl("txtUnitId");
                HiddenField lblUnitName = (HiddenField)gvrow.FindControl("txtUnitName");
                TextBox txtra = (TextBox)gvrow.FindControl("txtRateSan");
                Label lblam = (Label)gvrow.FindControl("lblAmtSan");
                TextBox txtST = (TextBox)gvrow.FindControl("txtStockEntry");
                Label lblEstQty = (Label)gvrow.FindControl("lblEstQty");
                Label lblEstRate = (Label)gvrow.FindControl("lblEstRate");
                HiddenField hdnSno = (HiddenField)gvrow.FindControl("hdnSno");
                HiddenField hdnAmtSan = (HiddenField)gvrow.FindControl("hdnAmtSan");
                TextBox txtva = (TextBox)gvrow.FindControl("txtVat");
                CheckBox chkvat = (CheckBox)gvrow.FindControl("chkVat");
                Label tAmt = (Label)gvrow.FindControl("lblTotalEstimateCost");

                MatId = lblMatId.Value;
                Material = lblMatName.Text;
                Qty = txtqt.Text;
                UnitId = lblUnitId.Value;
                UnitName = lblUnitName.Value;
                Rate = txtra.Text;
                StockEntryNo = txtST.Text;
                EstQty1 = lblEstQty.Text;
                vat = txtva.Text;
                decimal totalVatIncluded = 0;


                decimal q = Convert.ToDecimal(Qty);
                decimal r = Convert.ToDecimal(Rate);
                decimal va = 0;

                if (vat != string.Empty)
                {
                    va = Convert.ToDecimal(vat);
                }

                decimal TotalAmount = q * r;


                if (chkvat.Checked == false)
                {
                    SubTotal = (TotalAmount * va) / 100;
                    totalIncludeVat = TotalAmount + SubTotal;
                    totalIncludeVat = Math.Round(totalIncludeVat, 2);
                    TotalBillAmount += totalIncludeVat;
                    totalVatIncluded = Convert.ToDecimal(vat);
                }
                else
                {
                    totalIncludeVat = TotalAmount;
                    TotalBillAmount += totalIncludeVat;
                    totalVatIncluded = 0;
                }

                if (BillID > 0)
                {
                    DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewSubmitBillByUserAndMaterialOthersRelations " + hdnSno.Value + "," + BillID + ",'','" + MatId + "','" + Material + "','" + Qty + "','" + UnitId + "','" + Rate + "','" + totalIncludeVat + "','" + inchargeID + "','2','1','','" + StockEntryNo + "','" + ddlNameOfWork.SelectedValue + "','" + totalVatIncluded + "'");
                }
                else
                {
                    DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewSubmitBillByUserAndMaterialOthersRelations '','','','" + MatId + "','" + Material + "','" + Qty + "','" + UnitId + "','" + Rate + "','" + totalIncludeVat + "','" + inchargeID + "','1','1','','" + StockEntryNo + "','" + ddlNameOfWork.SelectedValue + "','" + totalVatIncluded + "'");
                }
            }
            if (BillID > 0)
            {
                DAL.DalAccessUtility.GetDataInDataSet("update SubmitBillByUser set TotalAmount=" + TotalBillAmount + " where SubBillId=" + BillID);
            }
            else
            {
                DataTable dsBill = DAL.DalAccessUtility.GetDataInDataSet("select MAX(SubBillId)as NewBillId from SubmitBillByUser where CreatedBy='" + inchargeID + "'").Tables[0];
                if (dsBill != null && dsBill.Rows.Count > 0)
                {
                    DAL.DalAccessUtility.GetDataInDataSet("update SubmitBillByUser set TotalAmount=" + TotalBillAmount + " where SubBillId=" + dsBill.Rows[0]["NewBillId"].ToString());
                    hdnBillID.Value = dsBill.Rows[0]["NewBillId"].ToString();
                }
            }
            gvAddItems2.DataSource = null;
            gvAddItems2.DataBind();
            SetInitialRowEstimateDataLoad();

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Submitted Successfully.');", true);

            DataSet dsBillId = DAL.DalAccessUtility.GetDataInDataSet("select MAX(SubBillId)as NewBillId from SubmitBillByUser where CreatedBy='" + hdnInchargeID.Value + "'");

            DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + dsBillId.Tables[0].Rows[0]["NewBillId"].ToString() + "'");

            string msg = "New " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " academy issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + ",  Bill No. is " + dsBillId.Tables[0].Rows[0]["NewBillId"].ToString() + " ";

            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsgToAdmin '" + lblUser.Text + "','" + msg + "'");

            Response.Redirect("Emp_BillDetails.aspx?BillId=" + dsBillId.Tables[0].Rows[0]["NewBillId"].ToString() + "");
        }
    }
    protected void ddlNameOfWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsDes = new DataSet();
        dsDes = DAL.DalAccessUtility.GetDataInDataSet("SELECT Estimate.SubEstimate, WorkAllot.WorkAllotName FROM Estimate INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId where Estimate.WAId='" + ddlNameOfWork.SelectedValue + "'");
        trMatSelect.Visible = true;
        BindMat();
    }

    protected void ShowBillDetails(string ID)
    {

        DataSet dsBill = new DataSet();
        dsBill = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillViewByBillId_V2 '" + ID + "'");
        if (dsBill.Tables[0].Rows[0]["BillType"].ToString() == ((int)TypeEnum.BillType.Sanctioned).ToString())
        {
            afilePath.Visible = true;
            ddlBillType1.Items.FindByText("Sanctioned").Selected = true;
         
            ddlBillType1_SelectedIndexChanged(ddlBillType1, new EventArgs());
            ddlNameOfWork.ClearSelection();
            ddlNameOfWork.Items.FindByValue(dsBill.Tables[0].Rows[0]["WAId"].ToString()).Selected = true;
            txtBillDate.Text = dsBill.Tables[0].Rows[0]["BillDate"].ToString();
            txtGateEntryNo.Text = dsBill.Tables[0].Rows[0]["GateEntryNo"].ToString();
            txtAgenyBillNo.Text = dsBill.Tables[0].Rows[0]["AgencyBillNumber"].ToString();
           // Request.Form["txtAgencyName"] = dsBill.Tables[0].Rows[0]["AgencyName"].ToString();
            afilePath.HRef ="Bills/"+ dsBill.Tables[0].Rows[0]["AgencyBill"].ToString();
            GetAllMaterials(dsBill.Tables[0].Rows[0]["WAId"].ToString(), dsBill.Tables[2]);
            btnSubmit.Visible = true;
       
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "LoadNonSanctionedBillDetailByBillID(" + ID + ");", true);
            lblChargeable.Visible = false;
            lblNameWork.Visible = false;
            ddlBillType.Visible = false;
            ddlEsimate.Visible = false;
            pnlSanction.Visible = false;
            pnlNonSanction.Visible = true;
            pnlEstimateDetails.Visible = true;
            trMatSelect.Visible = false;
            btnAmtTotal.Visible = false;
            divFinalButtons.Visible = true;
            trRemarks.Visible = false;
            btnShowData.Visible = false;
            ddlNameOfWork.Visible = false;
            btnSubmit.Visible = false;
        }
    }

    private void GetAllMaterials(string WorkAllotID, DataTable dtBilledMaterials)
    {
        DataSet dsAcademy = new DataSet();
        pnlEstimateDetails.Visible = false;
        btnSubmit.Visible = true;
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_getEstimateBalanceNew'" + WorkAllotID + "','1'");

        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("MatId", typeof(string)));
        dt.Columns.Add(new DataColumn("MatName", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitId", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
        dt.Columns.Add(new DataColumn("EstQty", typeof(string)));
        dt.Columns.Add(new DataColumn("PurQty", typeof(string)));
        dt.Columns.Add(new DataColumn("BalQty", typeof(string)));
        dt.Columns.Add(new DataColumn("EstRate", typeof(string)));


        for (int i = 0; i < dtBilledMaterials.Rows.Count; i++)
        {
            var results = (from myRow in dsAcademy.Tables[0].AsEnumerable()
                           where myRow.Field<int>("MatID") == int.Parse(dtBilledMaterials.Rows[i]["MatID"].ToString())
                           select myRow).ToList();

            dr = dt.NewRow();


            dr["MatId"] = results[0]["MatId"].ToString();
            dr["MatName"] = results[0]["MatName"].ToString();
            dr["UnitId"] = results[0]["UnitId"].ToString();
            dr["UnitName"] = results[0]["UnitName"].ToString();
            dr["EstQty"] = results[0]["Quantity"].ToString();
            dr["PurQty"] = results[0]["BillQty"].ToString();
            dr["BalQty"] = results[0]["EstBal"].ToString();
            dr["EstRate"] = results[0]["Rate"].ToString();
            dt.Rows.Add(dr);
        }

        dt.AcceptChanges();
        //SetInitialRowEstimateDataLoad();
        gvAddItems2.DataSource = dt;
        gvAddItems2.DataBind();

        double balanceQuantity = 0;
        double totalAmount = 0;
        CultureInfo hindi = new CultureInfo("hi-IN");
        foreach (GridViewRow gvrow in gvAddItems2.Rows)
        {

            var results = (from myRow in dtBilledMaterials.AsEnumerable()
                           where myRow.Field<int>("MatID") == int.Parse(((HiddenField)gvrow.FindControl("txtMatId")).Value)
                           select myRow).ToList();

            balanceQuantity = double.Parse(((Label)gvrow.FindControl("lblBalQty")).Text);

            ((TextBox)gvrow.FindControl("txtQty")).Text = results[0]["Qty"].ToString();
            ((TextBox)gvrow.FindControl("txtRateSan")).Text = results[0]["Rate"].ToString();
            ((TextBox)gvrow.FindControl("txtStockEntry")).Text = results[0]["StockEntryNo"].ToString();
            ((Label)gvrow.FindControl("lblAmtSan")).Text = string.Format(hindi, "{0:C}", Convert.ToDouble(results[0]["Amount"]));

            totalAmount += Convert.ToDouble(results[0]["Amount"]);

            ((HiddenField)gvrow.FindControl("hdnSno")).Value = results[0]["sno"].ToString();
        }

        ((Label)gvAddItems2.FooterRow.FindControl("lblTotalEstimateCost")).Text = string.Format(hindi, "{0:C}", totalAmount);
        
        if (ddlBillType1.SelectedValue == ((int)TypeEnum.BillType.Sanctioned).ToString())
        {
            divFinalButtons.Visible = true;
        }
        pnlNonSanction.Visible = false;
        btnAmtTotal.Visible = false;
        trMatSelect.Visible = false;
        pnlEstimateDetails.Visible = true;
        btnSubmit.Visible = true;
        ViewState["MaterialBalance"] = dsAcademy.Tables[0];
    }
}