using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_VehicleDocuments : System.Web.UI.UserControl
{
    private int VehicleID = -1;
    private int InchargeID = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InchargeID"] != null)
        {
            InchargeID = int.Parse(Session["InchargeID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            BindAcademy();
           
        }
        
    }
    private void bindDocumentGrid()
    {
        DataSet TransportDocuments = DAL.DalAccessUtility.GetDataInDataSet("Select * from TransportDocuments order by displayOrder asc");
        gvDocuments.DataSource = TransportDocuments.Tables[0];
        gvDocuments.DataBind();
    }

    protected void gvDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet ds = new DataSet();

            FileUpload fiupload = e.Row.FindControl("fiupload") as FileUpload;
            Button bt_upload = e.Row.FindControl("bt_upload") as Button;

            Label lblDocumentTypeID = e.Row.FindControl("lblDocumentTypeID") as Label;
            HyperLink hypDoc = e.Row.FindControl("hypDoc") as HyperLink;
            Label lblDocu = e.Row.FindControl("lblDocu") as Label;
            TextBox txtDate = e.Row.FindControl("txtDate") as TextBox;
            Button btn_Approved = e.Row.FindControl("btn_Approved") as Button;

            string path = string.Empty;
            ds = DAL.DalAccessUtility.GetDataInDataSet("SELECT * FROM VechilesDocumentRelation WHERE TransportDocumentID=" + lblDocumentTypeID.Text + " AND VehicleID=" + drpVehicle.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {

                lblDocu.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                hypDoc.NavigateUrl = "../../" + ds.Tables[0].Rows[0]["Path"].ToString();
                path = ds.Tables[0].Rows[0]["Path"].ToString();
                path = path.Substring(path.IndexOf('/') + 1);
                hypDoc.Text = path;
                if (ds.Tables[0].Rows[0]["DocumentEndDate"].ToString() != "")
                {
                    txtDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DocumentEndDate"].ToString()).ToShortDateString();
                }
                btn_Approved.Visible = true;
            }
        }
    }

    protected void btn_Approved_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (GridViewRow)((DataControlFieldCell)((Button)sender).Parent).Parent;
        Button btnapproved = (Button)gr.FindControl("btn_Approved");
        string approvedid = btnapproved.CommandArgument.ToString();
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("SELECT ID,TransportDocumentID FROM VechilesDocumentRelation WHERE TransportDocumentID=" + approvedid + " AND VehicleID=" + drpVehicle.SelectedValue);
        if (dsMat != null && dsMat.Tables[0] != null && dsMat.Tables[0].Rows.Count > 0)
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Delete FROM VechilesDocumentRelation WHERE TransportDocumentID=" + approvedid + " AND VehicleID=" + drpVehicle.SelectedValue);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Vehicle Document Delete Successfully');</script>", false);

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('File does not exit!!!!! Please select the correct File');</script>", false);
        }
        bindDocumentGrid();
    }

    public void BindVehicle()
    {
        TransportUserRepository trepository = new TransportUserRepository(new AkalAcademy.DataContext());
        List<Vehicles> vehicles = trepository.GetAllVehiclesByAcademyID(Convert.ToInt32(drpAcademy.SelectedValue));
        drpVehicle.DataSource = vehicles;
        drpVehicle.DataValueField = "ID";
        drpVehicle.DataTextField = "Number";
        drpVehicle.DataBind();
        drpVehicle.Items.Insert(0, new ListItem("--Select One--", "0"));
    }

    public void BindAcademy()
    {

        UsersRepository users = new UsersRepository(new AkalAcademy.DataContext());
        List<Academy> acaList = new List<Academy>();
        if (Session["UserTypeID"].ToString() == "13")
        {
            acaList = users.GetAllAcademy(2);
        }
        else
        {
            acaList = users.GetAcademyByInchargeID(InchargeID);
        }

        drpAcademy.DataSource = acaList;
        drpAcademy.DataTextField = "AcaName";
        drpAcademy.DataValueField = "AcaID";
        drpAcademy.DataBind();
        drpAcademy.Items.Insert(0, new ListItem("--Select One--", "0"));
    }

    protected void drpVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
       bindDocumentGrid();
       
    }

    protected void ddrAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVehicle();
    }
}