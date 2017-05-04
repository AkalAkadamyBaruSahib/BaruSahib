using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TypeEnum
/// </summary>
public class TypeEnum
{
	public TypeEnum()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    
    public enum Module : int
    {
        Purchase = 1,
        Transport = 2,
        Security = 3,
        Workshop = 4
    }

    public enum UserRole : int
    {
        Complaint = 1,
        TransportMaintenance = 2,
        TransportVehicleMaintenance = 3
    }

    public enum TransportEmployeeType : int
    {
        FamilyMember = 1,
        Reference = 2
    }

    public enum TransportVehicleEmployeeType : int
    {
        Driver = 1,
        Conductor = 2
    }

    public enum CommentType : int
    {
        Complaint = 1,
        Estimate = 2,
        Purchase =3
    }

    public enum MatTypeID : int
    {
        TRANSPORTMATERIAL = 49,
        ELECTRICALMATERIAL = 22,
        MOTORSANDPUMPS = 35,
        EXTERNALELECTRICALWORK = 48,
        BHUILDINGMATERIAL = 24,
        TILES = 29,
        RATION = 52,
        VEGETABLESANDFRUITS = 53,
        WOODMATERIAL = 65
    }

    public enum MatID : int
    {
        ANGLEPATTI460MM = 844,
        SARIA = 2387
    }

    public enum TransportDocumentType : int
    {
        Registration = 1,
        Pollution = 2,
        Permit = 3,
        Tax = 4,
        Passing = 5,
        Insurance = 6,
        WrittenContract = 8,
        RouteMap = 9
    }

    public enum UserType : int
    {
        ADMIN = 1,
        CONSTRUCTION = 2,
        AUDIT = 3,
        PURCHASE = 4,
        ACCOUNT = 5,
        WORKSHOPADMIN = 6,
        ARCHITECTURAL = 7,
        MAINTANENCE = 8,
        STORE = 9,
        ACADEMIC = 10,
        HR = 11,
        PURCHASEEMPLOYEE = 12,
        TRANSPORTADMIN = 13,
        TRANSPORTMANAGER = 14,
        BACKOFFICE = 15,
        INSURANCECOORDINATOR = 16,
        TRANSPORTINCHARGE = 17,
        BACKOFFICEHO = 18,
        TRANSPORTTRAINEE = 19,
        BACKOFFICETRAINEE = 20,
        CONSTRUCTIONSUBADMIN = 21,
        FrontDesk = 22,
        PURCHASECOMMITTEE = 23,
        SECURITY = 24,
        EMPLOYEESUBADMIN = 28,
        TRANSPORTSUBADMIN = 29,
        WORKSHOPEMPLOYEE = 30,
        SECURITYSUPERVISOR = 31,
        RECEPTIONADMIN = 32,
        COMPLAINT = 33,
        ELECTRICAL=34
    }
    public enum TransportNormsType : int
    {
        YellowColor = 1,
        Grill = 2,
        Fire = 3,
        FirstAidBox = 4,
        SchoolNamebothside = 5,
        Incaseofrashdriving = 6,
        Uniform = 7,
        SpeedGoverner = 8,
        QualityLock = 9,
        EmergencyWindows = 10,
        ToolsStemney = 11,
        GPS = 13,
        FemaleConductor = 14,
        Camera = 15,
        HydraulicAutomaticDoor = 16
    }

    public enum SubAdminName : int
    {
        Electrical = 1,
        Barusahib = 2,
        Transport = 3,
        Construction = 4,
        TransportMaintenance = 5,
        TransportVehicleMaintenance = 6
    }

    public enum TransportType : int
    {
        Trust = 1,
        Contractual = 2,
        MaterialMovementVehicle = 3,
        DailyWages = 4,
        Twowheeler = 5,
        Passengervehicle = 6,
        Ambulance = 7,
        SewaDarVehicle = 8,
        CivilEquipmentVehicle = 9
    }
    public enum TransportDLType : int
    {
        LMV = 1,
        LMVGV = 2,
        LPV = 3,
        HMV = 4,
        HTV = 5,
        PSVBUS = 6,
        TRANS = 7,
        CHASSIS=8
    }

    public enum WorkshopReportTypes : int
    {
        InStoreReport = 1,
        DispatchMaterial = 2,
        PendingMaterial =3
    }

    public enum PurchaseSourceID : int
    {
        Local = 1,
        Mohali = 2,
        AkalWorkshop = 3
    }

    public enum RoomRentType : int
    {
        PaidByTrust = 1,
        PaidByEmployee = 2,
        VisitorSelfPaid = 3
    }

    public enum ReceptionCashType : int
    {
        ByCash = 1,
        ByCheque = 2,
        ByDD = 3
    }

    public enum VisitoryType : int
    {
        Visitor = 1,
        Sewadar = 2,
        Employee = 3,
        Staff = 4,
        Volunteer = 5
    }
    public enum VisitorReportTypes : int
    {
        AccordingDate = 1,
        ViewVacantRoomList = 2,
        VisitorsReportByPlaces = 3,
        PermanentRoomDetailReport = 4,
        ViewBookedRoomList = 5,
        RoomStatus = 6
    }
    public enum TransportProformaType : int
    {
        GENSETREAPIRANDSERVICE = 1,
        BATTERYQUOTATION = 2,
        TYREREQUIREMENTORQUOTATION = 3,
        SERVICEOTHERREAPIRSOFVEHICLE = 4
    }
    public enum AutoGenerateReportType : int
    {
        PendingReport = 1,
        VisitorRoomStatus = 2,
        CompliantReport = 3
    }
    public enum ComplaintTicketStatus : int
    {
        Assigned = 1,
        InProgres = 2,
        Completed = 3,
    }

    public enum BillType : int
    {
        Sanctioned = 1,
        NonSanctioned = 2
    }
}