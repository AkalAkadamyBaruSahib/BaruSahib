using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Example_GridViewWithCheckBox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMat();
            //if (Request.QueryString["EstId"] != null)
            //{
            //    BindMat(Request.QueryString["EstId"].ToString());

            //}
        }
    }
    protected void BindMat()
    {
        DataSet dsAcademy = new DataSet();
        //dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where MatId in (select MatId from EstimateAndMaterialOthersRelations where EstId='" + id + "'  and PSId=1)");
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where MatId in (select MatId from EstimateAndMaterialOthersRelations )");
        GridView1.DataSource = dsAcademy;
        //chkMaterial.DataValueField = "MatId";
        //chkMaterial.DataTextField = "MatName";
        GridView1.DataBind();
    }
    protected void btnShowData_Click(object sender, EventArgs e)
    {
         string data = "";
         DataTable dt = new DataTable();
         DataRow dr = null;
         dt.Columns.Add(new DataColumn("MatId",typeof(string)));
         dt.Columns.Add(new DataColumn("MatName", typeof(string)));
         //dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
         //dt.Columns.Add(new DataColumn("Unit", typeof(string)));
         //dt.Columns.Add(new DataColumn("Rate", typeof(string)));
         //dt.Columns.Add(new DataColumn("Amount", typeof(string)));
         //dt.Columns.Add(new DataColumn("Remark", typeof(string)));
        
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                if (chkRow.Checked)
                {
                    dr = dt.NewRow();
                    dr["MatId"] = row.Cells[1].Text;
                    dr["MatName"] =row.Cells[2].Text;
                    dt.Rows.Add(dr);
                    data = data + dr["MatName"] + ",";
                }
            }

        }
        dt.AcceptChanges();
        
        Session["Data"] = dt;
        //lblZoneId.Text = "You Select " + data;
        
    }

}