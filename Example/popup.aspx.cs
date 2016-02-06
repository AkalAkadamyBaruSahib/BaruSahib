using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class popup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["EstId"] != null)
            {
                BindMat(Request.QueryString["EstId"].ToString());
               
            }
        }
        
    }
    protected void BindMat(string id)
    {
        DataSet dsAcademy = new DataSet();
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where MatId in (select MatId from EstimateAndMaterialOthersRelations where EstId='" + id + "'  and PSId=1)");
        chkMaterial.DataSource = dsAcademy;
        chkMaterial.DataValueField = "MatId";
        chkMaterial.DataTextField = "MatName";
        chkMaterial.DataBind();
    }

}