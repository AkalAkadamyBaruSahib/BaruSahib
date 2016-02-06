using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Example_GridTemplating : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        DataSet ds= new DataSet();
        ds=DAL.DalAccessUtility.GetDataInDataSet("select Tid,FId,Tname,Tmob,TAddr,Temail from Test");
        GridView1.DataSource = ds;
        GridView1.DataBind();
       
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        TextBox txtname = (TextBox)GridView1.FooterRow.FindControl("txt_Name");
        TextBox txtMob = (TextBox)GridView1.FooterRow.FindControl("txt_Mob");
        TextBox txtAddr = (TextBox)GridView1.FooterRow.FindControl("txt_Addr");
        TextBox txtEmail = (TextBox)GridView1.FooterRow.FindControl("txt_Email");
        DAL.DalAccessUtility.ExecuteNonQuery("insert into Test(Tname,Tmob,TAddr,TEmail) values ('" + txtname.Text + "','" + txtMob.Text + "','" + txtAddr.Text + "','" + txtEmail.Text + "')");
 
        BindGridView();
    }
}