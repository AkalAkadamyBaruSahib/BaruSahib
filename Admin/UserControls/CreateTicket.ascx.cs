using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_CreateTicket : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            txtuserID.Value = Session["InchargeID"].ToString();
            hdnUserType.Value = Session["UserTypeID"].ToString();
            hdnLoginID.Value = Session["EmailId"].ToString();
            hdnUserID.Value = Session["InchargeID"].ToString();
            hdnUserName.Value = Session["InName"].ToString();
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        if (ddlReport.SelectedValue == ((int)TypeEnum.ComplaintTicketStatus.Assigned).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "NewComplaintTicketsReport.xls"));
        }
        else if (ddlReport.SelectedValue == ((int)TypeEnum.ComplaintTicketStatus.InProgres).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "InProgressComplaintsTicketReport.xls"));
        }
        else if (ddlReport.SelectedValue == ((int)TypeEnum.ComplaintTicketStatus.Completed).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CompletedComplaintTicketReport.xls"));
        }

        Response.ContentType = "application/ms-excel";
        DataTable dt = BindDatatable();
        string str = string.Empty;
        string coldata = string.Empty;
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
                coldata = Convert.ToString(dr[j]).Replace('\n', ' ');
                Response.Write(str + coldata);
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        string from = string.Empty;

        if (ddlReport.SelectedValue == ((int)TypeEnum.ComplaintTicketStatus.Assigned).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec GetComplaintsTicketReports " + hdnUserType.Value + "," + hdnUserID.Value + ", 'Assigned'").Tables[0];
        }
        else if (ddlReport.SelectedValue == ((int)TypeEnum.ComplaintTicketStatus.InProgres).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec GetComplaintsTicketReports " + hdnUserType.Value + "," + hdnUserID.Value + ",'In Progress'").Tables[0];
        }
        else if (ddlReport.SelectedValue == ((int)TypeEnum.ComplaintTicketStatus.Completed).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec GetComplaintsTicketReports " + hdnUserType.Value + "," + hdnUserID.Value + ",'Completed'").Tables[0];
        }
        return dt;
    }
}

