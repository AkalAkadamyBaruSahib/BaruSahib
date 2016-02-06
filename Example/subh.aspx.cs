using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Example_subh : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Select CityId, CityName from City", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                DropDownList1.DataTextField = "CityName";
                DropDownList1.DataValueField = "CityId";
                DropDownList1.DataSource = rdr;
                DropDownList1.DataBind();
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ClientScript.IsStartupScriptRegistered("alert"))
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(),
                "alert", "openpopup();", true);
        }


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "zxcszc";
    }
}