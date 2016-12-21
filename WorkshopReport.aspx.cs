using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WorkshopReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindWorkshop();
            BindIncharge();
            for (int i = 0; i < chkIncharge.Items.Count; i++)
            {
                chkIncharge.Items[i].Selected = true;
            }
            for (int i = 0; i < chkworkshop.Items.Count; i++)
            {
                chkworkshop.Items[i].Selected = true;
            }
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        if (ddlWorkshopReport.SelectedValue == ((int)TypeEnum.WorkshopReportTypes.InStoreReport).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "InstoreQtyReport.xls"));
        }
        else if (ddlWorkshopReport.SelectedValue == ((int)TypeEnum.WorkshopReportTypes.DispatchMaterial).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DispatchEstimateReport.xls"));
        }
        else
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PendingEstimateReport.xls"));
        }

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

    protected DataTable BindDatatable()
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        int UserID = Convert.ToInt32(Session["InchargeID"].ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string selectedItems = String.Join(",", chkworkshop.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value));
        if (ddlWorkshopReport.SelectedValue == ((int)TypeEnum.WorkshopReportTypes.InStoreReport).ToString())
        {
            if (selectedItems != "")
            {
                dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_WorkshopInstoreMaterialReportByWorkshopID] '" + (selectedItems) + "'").Tables[0];
            }
        }
        else if (ddlWorkshopReport.SelectedValue == ((int)TypeEnum.WorkshopReportTypes.DispatchMaterial).ToString())
        {
            string selectedIncharge = String.Join(",", chkIncharge.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value));
            if (UserTypeID == (int)TypeEnum.UserType.WORKSHOPADMIN)
            {
                if (selectedIncharge != "")
                {
                    dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateStatusReportForWorkshop] '" + txtfirstDate.Text + "','" + txtlastDate.Text + "','" + (int)TypeEnum.PurchaseSourceID.AkalWorkshop + "','" + (selectedIncharge) + "'").Tables[0];
                }
            }
            else
            {
                if (selectedIncharge != "")
                {
                    dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateStatusReportForWorkshopByEmpID] '" + txtfirstDate.Text + "','" + txtlastDate.Text + "','" + selectedIncharge + "','" + (int)TypeEnum.PurchaseSourceID.AkalWorkshop + "'").Tables[0];
                }
            }
        }
        else
        {
            string selectedIncharge = String.Join(",", chkIncharge.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value));
            if (selectedIncharge != "")
            {
                dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_PendingEstimateStatusReportForWorkshop] '" + txtfirstDate.Text + "','" + txtlastDate.Text + "','" + (int)TypeEnum.PurchaseSourceID.AkalWorkshop + "','" + (selectedIncharge) + "'").Tables[0];
            }
        }
        return dt;
    }

    public void BindWorkshop()
    {
        int UserID = Convert.ToInt32(Session["InchargeID"].ToString());
        int UserType = Convert.ToInt32(Session["UserTypeID"].ToString());
        DataTable dsWorkshop = new DataTable();
        dsWorkshop = DAL.DalAccessUtility.GetDataInDataSet("Select A.AcaId,A.AcaName from Academy A INNER JOIN AcademyAssignToEmployee AAE on A.AcaId=AAE.AcaId Where  AAE.EmpId='" + UserID + "'").Tables[0];
        if (dsWorkshop != null && dsWorkshop.Rows.Count>0)
        {
            chkworkshop.DataSource = dsWorkshop;
            chkworkshop.DataValueField = "AcaId";
            chkworkshop.DataTextField = "AcaName";
            chkworkshop.DataBind();
        }
    }


    public void BindIncharge()
    {
        int UserID = Convert.ToInt32(Session["InchargeID"].ToString());
        int UserType = Convert.ToInt32(Session["UserTypeID"].ToString());
        DataTable dsWorkshop = new DataTable();
        if (UserType == (int)TypeEnum.UserType.WORKSHOPADMIN)
        {
            dsWorkshop = DAL.DalAccessUtility.GetDataInDataSet("select InchargeId,InName from Incharge where ModuleID=" + (int)TypeEnum.Module.Workshop + " and  DepId=13").Tables[0];
        }
        else
        {
            dsWorkshop = DAL.DalAccessUtility.GetDataInDataSet("select InchargeId,InName from Incharge where ModuleID=" + (int)TypeEnum.Module.Workshop + " and  InchargeId=" + UserID + "").Tables[0];
        }
        if (dsWorkshop != null && dsWorkshop.Rows.Count > 0)
        {
            chkIncharge.DataSource = dsWorkshop;
            chkIncharge.DataValueField = "InchargeId";
            chkIncharge.DataTextField = "InName";
            chkIncharge.DataBind();
           
        }
     
    }
}