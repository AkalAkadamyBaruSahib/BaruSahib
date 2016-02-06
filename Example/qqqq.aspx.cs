using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Example_qqqq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showCategoryOnGrid();
            //string cate = dsFill.Tables[0].Rows[0]["Category"].ToString();
            //FilCategory(cate);
        }
    }
    protected void showCategoryOnGrid()
    {
        DataSet dsCate = new DataSet();
        dsCate = DAL.DalAccessUtility.GetDataInDataSet("select matId,Matname from material");
        grvCategory.DataSource = dsCate;
        grvCategory.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string tCategory = "";
        string Category = "";
        foreach (GridViewRow gvrow in grvCategory.Rows)
        {


            Label lbl = (Label)gvrow.FindControl("Label1");
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            if (chk != null & chk.Checked)
            {
                tCategory = lbl.Text;
                Category = Category + tCategory + ",";
            }
        }
        lblShow.Text = Category;
    }
    //protected void FilCategory(string Desig)
    //{
    //    string[] words = Desig.Split(',');
    //    foreach (string word in words)
    //    {
    //        for (int m = 0; m < grvCategory.Rows.Count - 1; m++)
    //        {
    //            CheckBox ch = (CheckBox)grvCategory.Rows[m].FindControl("chkSelect");
    //            Label lblc = (Label)grvCategory.Rows[m].FindControl("Label1");
    //            if (word.Trim() == lblc.Text.Trim())
    //            {
    //                ch.Checked = true;
    //            }
    //        }
    //    }

    //}
}