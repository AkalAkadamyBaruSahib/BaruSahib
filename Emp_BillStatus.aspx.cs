using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emp_BillStatus : System.Web.UI.Page
{

    public static int InchargeID = -1;
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
                InchargeID = Convert.ToInt32(Session["InchargeID"].ToString());
            }

            if (Session["billid"] != null)
            {
                getBillID(Session["billid"].ToString());
            }
            if (Request.QueryString["DltBillID"] != null)
            {
                DeleteBill();
            }
            getBillDetails();
        }
    }
    private void getBillID(string id)
    {
        DataSet dsAgency = new DataSet();
        dsAgency = DAL.DalAccessUtility.GetDataInDataSet("select AgencyName from SubmitBillByUser where SubBillId='" + id + "'");
        if (dsAgency.Tables[0].Rows.Count > 0)
        {
            lblAgency.Text = dsAgency.Tables[0].Rows[0]["AgencyName"].ToString();

        }
        getBillDetails();
    }
    private void getBillDetails()
    {

        //DataSet dsZone = new DataSet();
        //dsZone = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ZoneIdByLoginId '" + lblUser.Text + "'");
        DataSet dsBillDetails = new DataSet();
        dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_BillVarificationDEtailsByUser] '" + lblUser.Text + "'");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> All Bill DEtails</h2>";
        ZoneInfo += "<div class='box-icon'>";
        //ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='10%'>Academy</th>";
        ZoneInfo += "<th width='10%'>Bill No.</th>";
        ZoneInfo += "<th width='13%'>Agency Name</th>";
        ZoneInfo += "<th width='13%'>Amount</th>";
        ZoneInfo += "<th width='13%'>H/Q Activity</th>";
        ZoneInfo += "<th width='13%'>Audit Activity</th>";
        ZoneInfo += "<th width='13%'>Account Activity</th>";
        ZoneInfo += "<th width='13%'>Reciving</th>";
        ZoneInfo += "<th width='13%'>Action</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsBillDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='10%'>" + dsBillDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='10%' align='center'>";
            ZoneInfo += "<a  href='Emp_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
            ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date: " + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
            {
                ZoneInfo += "<a  href='#'><span class='label label-important' style='font-size: 15.998px;'>Pending</span></a>";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date: " + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date: " + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
            {
                ZoneInfo += "<span style='font-size: 15.998px;' class='label label-important' >Pending</span>";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date: " + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date: " + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "")
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date: " + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date: " + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Received to Agency</span></a> <br/> <a href='InVoice.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important'>Print Bill</span></a>  ";
                
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1" && dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
            {
                ZoneInfo += "<a class='btn btn-setting btn-round' href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rajected Date: " + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
            }

            if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
            {
                ZoneInfo += "<td width='13%'><a class='label label-important' href='Emp_BillStatus.aspx?DltBillID=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>Delete</a></td>";
            }
            else
            {
                ZoneInfo += "<td></td>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string BillId = Session["billid"].ToString();
       
       
        if (txtDateOfRec.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Date of Receiving.');", true);
        }
        else if (txtRecipTNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Recipt No .');", true);
        }
        else if (txtRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Varified Remark .');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("update SubmitBillByUser set ReciptNoByEmp='" + txtRecipTNo.Text + "',DateOfReceving='" + txtDateOfRec.Text + "',RecevingRemark=upper('" + txtRemark.Text + "'),RecevingStatus='1',RecevingBy='" + InchargeID + "' where SubBillId='" + BillId + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Varify successfully.');", true);
            getBillDetails();
            Clr();
        }

    }
    protected void Clr()
    {
        txtRecipTNo.Text = "";
        txtRemark.Text = "";
        txtDateOfRec.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string BillId = Session["billid"].ToString();
         if (txtRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Reject Remark .');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("update SubmitBillByUser set RecevingRemark=upper('" + txtRemark.Text + "'),RecevingStatus='0',RecevingBy='" + InchargeID + "',DateOfReceving=getdate() where SubBillId='" + BillId + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Reject successfully.');", true);
            getBillDetails();
            Clr();
        }
    }
    protected DataTable BindBillDetailsEx()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillDetails4Users'" + lblUser.Text + "'");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "BillDetails.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindBillDetailsEx();
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
    protected DataTable BindBillStatusEx()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatus4Users  '" + lblUser.Text + "'");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExeclStatus_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "BillStatus.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindBillStatusEx();
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

    public void DeleteBill()
    {
        int subBillID = int.Parse(Request.QueryString["DltBillID"].ToString());
        ConstructionUserRepository repo = new ConstructionUserRepository(new AkalAcademy.DataContext());
        repo.GetBillDetailToDelete(subBillID);
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Delete successfully.');", true);
    }

}