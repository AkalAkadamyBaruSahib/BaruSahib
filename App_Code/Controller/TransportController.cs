﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for TransportController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TransportController : System.Web.Services.WebService
{

    public TransportController()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int SaveNewTransportEmployee(VehicleEmployee VehicleEmployee)
    {
        TransportUserRepository tr = new TransportUserRepository(new AkalAcademy.DataContext());
        return tr.SaveNewTransportEmployee(VehicleEmployee);

    }

    [WebMethod]
    public void SaveTransEmpRelation(TransportEmployeeRelation TransportEmployeeRelation)
    {
        TransportUserRepository tr = new TransportUserRepository(new AkalAcademy.DataContext());
        tr.SaveTransEmpRelation(TransportEmployeeRelation);
    }

    [WebMethod]
    public List<VehicleEmployeeDTO> GeTransportEmployeeInformation(int VehicleEmployeeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GeTransportEmployeeInformation(VehicleEmployeeID);

    }

    [WebMethod]
    public VehicleEmployeeDTO GetTranportEmployeeInfoToUpdate(int VehicleEmployeeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GetTranportEmployeeInfoToUpdate(VehicleEmployeeID);
    }

    [WebMethod]
    public void UpdateTransportEmployeeInfo(VehicleEmployee VehicleEmployee)
    {
        TransportUserRepository tr = new TransportUserRepository(new AkalAcademy.DataContext());
        tr.UpdateTransportEmployeeInfo(VehicleEmployee);
    }

    [WebMethod]
    public void TranportEmployeeInfoToDelete(int VehicleEmployeeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        repository.TranportEmployeeInfoToDelete(VehicleEmployeeID);
    }

}
