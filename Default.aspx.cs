using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      // Utility.SendEmailUsingAttachments(@"D:\GITAkalSewa\Source\Repos\BaruSahib\EstFile\6808_22112016.xlsx", "itmohali@barusahib.org", "test", "15 Days pending report");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Login Id');", true);
        }
        else if (txtPwd.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Password');", true);
        }
        else
        {
            UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());
            Incharge inchrge = new Incharge();
           
            inchrge = repo.GetLoginUserDetail(txtUserName.Text.Trim(), txtPwd.Text.Trim());
           
            // DataSet dsExit = DAL.DalAccessUtility.GetDataInDataSet("select EmailId,Pwd from Login where Active=1 AND EmailId='" + txtUserName.Text + "'");
            if (inchrge != null && inchrge.InchargeId > 0)
            {


                AdminTypeRelation subadmin = repo.GetAdminType(inchrge.InchargeId);

                Session["InchargeID"] = inchrge.InchargeId;
                Session["UserTypeID"] = inchrge.UserTypeId;
                Session["UserName"] = inchrge.InName;
                Session["EmailId"] = inchrge.LoginId;
                Session["InName"] = inchrge.InName;
                Session["ModuleID"] = inchrge.ModuleID;
                

                if (subadmin != null)
                {
                    switch ((int)subadmin.SubAdminTypeID)
                    {
                        case (int)TypeEnum.SubAdminName.Barusahib:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.Barusahib;
                            break;
                        case (int)TypeEnum.SubAdminName.Electrical:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.Electrical;
                            break;
                        case (int)TypeEnum.SubAdminName.Transport:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.Transport;
                            break;
                        case (int)TypeEnum.SubAdminName.Construction:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.Construction;
                            break;
                        case (int)TypeEnum.SubAdminName.TransportMaintenance:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.TransportMaintenance;
                            break;
                        case (int)TypeEnum.SubAdminName.TransportVehicleMaintenance:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.TransportVehicleMaintenance;
                            break;
                        default:
                            break;
                    }
                }

                if (inchrge.UserTypeId == 1)
                {
                    Response.Redirect("AdminHome.aspx");
                }
                else if (inchrge.UserTypeId == 2 || inchrge.UserTypeId==(int)TypeEnum.UserType.COMPLAINT)
                {
                    Response.Redirect("Emp_Home.aspx");
                }
                else if (inchrge.UserTypeId == 30 || inchrge.UserTypeId == 6 )
                {
                   Response.Redirect("Workshop_Home.aspx");
                }
                else if (inchrge.UserTypeId == 4 || inchrge.UserTypeId == 12 || inchrge.UserTypeId == 23)
                {
                    Response.Redirect("Purchase_Home.aspx");
                }
                else if (inchrge.UserTypeId == 3)
                {
                    Response.Redirect("AuditHome.aspx");
                }
                else if (inchrge.UserTypeId == 5)
                {
                    Response.Redirect("Account_Home.aspx");
                }
                else if (inchrge.UserTypeId == 7)
                {
                    Response.Redirect("ArchHome.aspx");
                }
                else if (inchrge.UserTypeId == 10)
                {
                    Response.Redirect("AcademicUserHome.aspx");
                }
                else if (inchrge.UserTypeId == 9)
                {
                    Response.Redirect("StoreHome.aspx");
                    Response.Redirect("Store_Materials.aspx");
                }
                else if (inchrge.UserTypeId >= 13 && inchrge.UserTypeId <= 20)
                {
                    Response.Redirect("TransportHome.aspx");
                }
                else if (inchrge.UserTypeId == 22 || inchrge.UserTypeId == 32)
                {
                    Response.Redirect("Visitor_Home.aspx");
                }
                else if (inchrge.UserTypeId == 24)
                {
                    Response.Redirect("Security_Home.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Valid Login Details');", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Valid Login Details');", true);
            }
        }
    }
}