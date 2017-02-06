using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_UserControls_BodyRejectedBills : System.Web.UI.UserControl
{
   
    private int inchargeID = -1;
    private int UserTypeID { get; set; }

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
                inchargeID = Convert.ToInt16(Session["InchargeID"].ToString());
                UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
            }
            if (Request.QueryString["IsDeleted"] != null)
            {
                DeleteBill();
            }

            getBillDetails();
        }
    }

    private void DeleteBill()
    {
        int subBillID = int.Parse(Request.QueryString["BillID"].ToString());
        DAL.DalAccessUtility.ExecuteNonQuery("delete from SubmitBillByUserAndMaterialOthersRelation where subBillID='" + subBillID + "';delete from SubmitBillByUser where subBillID='" + subBillID + "';");
        ClearQueryString();
    }

    private void ClearQueryString()
    {
        System.Reflection.PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        isreadonly.SetValue(this.Request.QueryString, false, null);
        this.Request.QueryString.Clear();
    }

    private void getBillDetails()
    {
        //DataSet dsZone = new DataSet();
        //dsZone = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ZoneIdByLoginId '" + lblUser.Text + "'");
        DataSet dsBillDetails = new DataSet();
        dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillRejectedDEtails4User '" + inchargeID + "'");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Material List</h2>";
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
        ZoneInfo += "<th width='10%'>Academy</th>";
        ZoneInfo += "<th width='10%'>Bill No.</th>";
        ZoneInfo += "<th width='13%'>Agency Name</th>";
        ZoneInfo += "<th width='13%'>Amount</th>";
        ZoneInfo += "<th width='13%'>H/Q-Admin</th>";
        ZoneInfo += "<th width='13%'>Audit</th>";
        ZoneInfo += "<th width='13%'>Account</th>";
        //ZoneInfo += "<th width='13%'>Reciving</th>";
        ZoneInfo += "<th width='13%'>Action</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsBillDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
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
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title= 'Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
            {
                ZoneInfo += "<a  href='#'><span class='label label-important' style='font-size: 15.998px;'>Pending</span></a>";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title= 'Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title= 'Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
            {
                ZoneInfo += "<span style='font-size: 15.998px;' class='label label-important' >Pending</span>";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title= 'Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title= 'Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "")
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title= 'Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
            }
            ZoneInfo += "</td>";
            //ZoneInfo += "<td class='center' width='13%' align='center'>";
            //if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "1")
            //{
            //    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
            //    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title= 'Varified Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Received to Agency</span></a>  ";
            //}
            //else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1" && dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
            //{
            //    ZoneInfo += "<a class='btn btn-setting btn-round' href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
            //    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
            //    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
            //}
            //else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
            //{
            //    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
            //    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title= 'Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
            //}
            //ZoneInfo += "</td>";
            //if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0" || dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0" || dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
            if (dsBillDetails.Tables[0].Rows[i]["UserProStatus"].ToString() == null || dsBillDetails.Tables[0].Rows[i]["UserProStatus"].ToString() == "")
            {
                // ZoneInfo += "<td width='13%'><a class='btn btn-setting btn-round' data-rel='tooltip' title='Proceed bill to again verify.' href='Emp_RejectedBills.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>Proceed</a></td>";
                //ZoneInfo += "<td width='13%'><a class='btn btn-setting btn-round' data-rel='tooltip' title='Proceed bill to again verify.' href='#'>Proceed</a>/<a class='btn btn-round' href='Emp_BillSubmit.aspx?BillID=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "&AcaId=" + dsBillDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>Edit</a></td>";
                if (UserTypeID == (int)TypeEnum.UserType.ADMIN)
                {
                    ZoneInfo += "<td width='13%'><a class='label label-important' href='Admin_RejectedBills.aspx?IsDeleted=1&BillID=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>Delete</a></td>";
                }
                else
                {
                    ZoneInfo += "<td width='13%'><a class='label label-important' href='Emp_BillSubmit.aspx?BillID=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "&AcaId=" + dsBillDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>Edit</a> <a class='label label-important' href='Emp_RejectedBills.aspx?IsDeleted=1&BillID=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>Delete</a></td>";
                }
                Session["Biil"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
            }
            else
            {
                ZoneInfo += "<td width='13%'>Self Proceed</td>";
            }
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();

    }
    //protected void ProceedBill4Varification(string id)
    //{
    //DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + id + "'");
    //string msg = "Please varified " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill, Bill No.:" + id + " for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " and bill issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + " ";
    //DataSet dsUserName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where LoginId='" + lblUser.Text + "'");
    //string msgForAgain = "Please varified again " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill, Bill No.:" + id + " for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " and bill issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + " Proceed by Mr. " + dsUserName.Tables[0].Rows[0]["InName"].ToString() + "";
    //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsgToAdmin '" + lblUser.Text + "','" + msg + "'");
    //DataSet dsVariDetails = DAL.DalAccessUtility.GetDataInDataSet("select FirstVarify,FirstVarifyStatus,SeccondVarify,SecondVarifyStatus,ThirdVarifyBy,PaymentStatus,RecevingBy,RecevingStatus from SubmitBillByUser where SubBillId='" + id + "'");
    //if (dsVariDetails.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "0")
    //{
    //    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["FirstVarify"].ToString() + "','" + msgForAgain + "'");
    //}
    //else if (dsVariDetails.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "0")
    //{
    //    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["SeccondVarify"].ToString() + "','" + msgForAgain + "'");
    //}
    //else if (dsVariDetails.Tables[0].Rows[0]["PaymentStatus"].ToString() == "0")
    //{
    //    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["ThirdVarifyBy"].ToString() + "','" + msgForAgain + "'");
    //}
    //else if (dsVariDetails.Tables[0].Rows[0]["RecevingStatus"].ToString() == "0")
    //{
    //    //string msgForRec = "<a href='Emp_BillDetails.aspx?BillId=" + id + "'>Please varified again " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill, Bill No.:" + id + " for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " and bill issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + " Proceed by Mr. " + dsUserName.Tables[0].Rows[0]["InName"].ToString() + "";
    //    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["RecevingBy"].ToString() + "','" + msgForAgain + "'");
    //}
    //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_ProceedBill '" + id + "','1','" + lblUser.Text + "','2','"+ txtProRemark.Text +"'");
    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Proceed to Again Verification.');", true);
    //}
    protected void lbProceed_Click(object sender, EventArgs e)
    {

        lblBillId.Text = Session["Biil"].ToString();
        DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + lblBillId.Text + "'");
        string msg = "Please varified " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill, Bill No.:" + lblBillId.Text + " for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " and bill issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + " ";
        DataSet dsUserName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where LoginId='" + lblUser.Text + "'");
        string msgForAgain = "Please varified again " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill, Bill No.:" + lblBillId.Text + " for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " and bill issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + " and Proceed by Mr. " + dsUserName.Tables[0].Rows[0]["InName"].ToString() + "";
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsgToAdmin '" + lblUser.Text + "','" + msg + "'");
        DataSet dsVariDetails = DAL.DalAccessUtility.GetDataInDataSet("select FirstVarify,FirstVarifyStatus,SeccondVarify,SecondVarifyStatus,ThirdVarifyBy,PaymentStatus,RecevingBy,RecevingStatus from SubmitBillByUser where SubBillId='" + lblBillId.Text + "'");
        if (dsVariDetails.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "0")
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["FirstVarify"].ToString() + "','" + msgForAgain + "'");
        }
        else if (dsVariDetails.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "0")
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["SeccondVarify"].ToString() + "','" + msgForAgain + "'");
        }
        else if (dsVariDetails.Tables[0].Rows[0]["PaymentStatus"].ToString() == "0")
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["ThirdVarifyBy"].ToString() + "','" + msgForAgain + "'");
        }
        else if (dsVariDetails.Tables[0].Rows[0]["RecevingStatus"].ToString() == "0")
        {
            //string msgForRec = "<a href='Emp_BillDetails.aspx?BillId=" + id + "'>Please varified again " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill, Bill No.:" + id + " for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " and bill issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + " Proceed by Mr. " + dsUserName.Tables[0].Rows[0]["InName"].ToString() + "";
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["RecevingBy"].ToString() + "','" + msgForAgain + "'");
        }
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_ProceedBill '" + lblBillId.Text + "','1','" + lblUser.Text + "','2','" + txtProRemark.Text + "'");
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Proceed to Again Verification.');", true);
        getBillDetails();

    }
}