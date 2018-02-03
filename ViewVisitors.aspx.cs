using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewVisitors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindVisitorType();

            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                hdnUserType.Value = Session["UserTypeID"].ToString();
            }
            if (Request.QueryString["VisitorID"] != null)
            {
                int visitorID = int.Parse(Request.QueryString["VisitorID"]);
                PrintVisitorDetail(visitorID);
            }
            if (Request.QueryString["VisitorPrintID"] != null)
            {
                int visitorID = int.Parse(Request.QueryString["VisitorPrintID"]);
                PrintVisitorDetail(visitorID);
            }
        }
    }

    public class CookieAwareWebClient : WebClient
    {
        public CookieAwareWebClient()
        {
            CookieContainer = new CookieContainer();
        }
        public CookieContainer CookieContainer { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }
    }

    private void BindVisitorType()
    {
        DataTable visitortype = new DataTable();
        visitortype = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.VisitorType").Tables[0];
        if (visitortype != null && visitortype.Rows.Count > 0)
        {
            ddltypeofvisitor.DataSource = visitortype;
            ddltypeofvisitor.DataValueField = "ID";
            ddltypeofvisitor.DataTextField = "VisitorType";
            ddltypeofvisitor.DataBind();
        }
    }

    private void PrintVisitorDetail(int visitorID)
    {
        string htmlCode = string.Empty;
        Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        string appPath = HttpContext.Current.Request.ApplicationPath;

        string pathQuery = myuri.PathAndQuery;
        string FolderPath = myuri.ToString().Replace(pathQuery, "");
        string hostName = FolderPath + appPath;
        Visitors getVisitors = new Visitors();
        VisitorRoomNumbers getRooms = new VisitorRoomNumbers();
        RoomNumbers getRoomsDetail = new RoomNumbers();

        State getState = new State();
        City getCity = new City();
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());

        using (var client = new CookieAwareWebClient())
        {
            htmlCode = client.DownloadString(hostName + "/VisitorBioData.html");
        }

        string fileNameToSave = string.Empty;
        getVisitors = repository.GetVisitorsInfoByVisitorID(visitorID);
        getRooms = repository.GetVisitorsRoomInfoByVisitorID(visitorID);
        getRoomsDetail = repository.GetRoomInfoByRoomID(Convert.ToInt32(getRooms.RoomNumberID));
        getState = repository.GetVisitorsStateDetail(Convert.ToInt32(getVisitors.State));
        getCity = repository.GetVisitorCityDetail(Convert.ToInt32(getVisitors.City));
        htmlCode = htmlCode.Replace("[BUILDING]", getRoomsDetail.BuildingID.ToString());
        if (getRoomsDetail.BuildingFloor.ToString() == "0")
        {
            htmlCode = htmlCode.Replace("[FLOOR]", "Ground Floor");
        }
        else
        {
            htmlCode = htmlCode.Replace("[FLOOR]", getRoomsDetail.BuildingFloor.ToString());
        }
        htmlCode = htmlCode.Replace("[ROOMNO]", getRoomsDetail.Number);
        htmlCode = htmlCode.Replace("[STATE]", getState.StateName);
        htmlCode = htmlCode.Replace("[CITY]", getCity.CityName);
        DataTable dtAdminsDetail = DAL.DalAccessUtility.GetDataInDataSet("SELECT CountryName From Country  WHERE CountryId = " + getVisitors.Country).Tables[0];

        htmlCode = htmlCode.Replace("[COUNTRY]", dtAdminsDetail.Rows[0]["CountryName"].ToString());
        htmlCode = htmlCode.Replace("[src]", Server.MapPath("~/" + getVisitors.VisitorsPhoto));
        htmlCode = htmlCode.Replace("[FULLNAME]", getVisitors.Name);
        htmlCode = htmlCode.Replace("[ADDERSS]", getVisitors.VisitorAddress);
        htmlCode = htmlCode.Replace("[MOBILENO]", getVisitors.ContactNumber);
        htmlCode = htmlCode.Replace("[MEN]", getVisitors.TotalNoOfMen.ToString());
        htmlCode = htmlCode.Replace("[WOMEN]", getVisitors.TotalNoOfWomen.ToString());
        htmlCode = htmlCode.Replace("[CHILD]", getVisitors.TotalNoOfChildren.ToString());
        htmlCode = htmlCode.Replace("[IDPROOF]", getVisitors.Identification);
        htmlCode = htmlCode.Replace("[AUTHORITYPERMISSION]", getVisitors.VisitorReference);
        htmlCode = htmlCode.Replace("[VEHICLENO]", getVisitors.VehicleNo);
        htmlCode = htmlCode.Replace("[DAYS]", getVisitors.NoOfDaysToStay.ToString());
        DateTime from = Convert.ToDateTime(getVisitors.TimePeriodFrom);
        htmlCode = htmlCode.Replace("[FROM]", from.ToString("dd-MM-yyyy"));
        DateTime to = Convert.ToDateTime(getVisitors.TimePeriodTo);
        htmlCode = htmlCode.Replace("[TO]", to.ToString("dd-MM-yyyy"));
        htmlCode = htmlCode.Replace("[LANGERSEWA]", getVisitors.RoomRent.ToString());
        htmlCode = htmlCode.Replace("[PURPOSEOFVISIT]", getVisitors.PurposeOfVisit);
        htmlCode = htmlCode.Replace("[PURPOSEREMARKS]", getVisitors.PurposeOfVisitRemarks);
        pnlHtml.InnerHtml = htmlCode;
        var fileName = "Visitor_Bio_Data_" + getVisitors.Name.Trim() + "(" + getVisitors.ContactNumber + ").pdf";
        string pathToSave = Server.MapPath("VisitorsPhoto");
        Utility.GeneratePDF(htmlCode, fileName, pathToSave);
    }
}