CREATE PROCEDURE [dbo].GetExpireInsuranceRenewalInfo    

(

@StartDate as DateTime,

@EndDate as DateTime

)

AS    

BEGIN    

SELECT DISTINCT Aca.AcaName AS AA_ACADEMY,T.Type AS TYPE_OF_VEHICLE,V.number AS VEHICLE_NUMBER,

datename(DAY,VDR.DocumentEndDate)+' '+datename(M,VDR.DocumentEndDate)+' '+cast(datepart(YYYY,VDR.DocumentEndDate) as varchar) AS INSURANCE_DUE_DATE,

INC.InName AS TRANSPORT_MANAGER

FROM Vehicles V

INNER JOIN dbo.AcademyAssignToEmployee A ON A.AcaID=V.AcademyID     

INNER JOIN Incharge INC ON INC.InchargeID=A.EMPID     

INNER JOIN Academy Aca ON Aca.AcaID = A.AcaID   

INNER JOIN TransportTypes T ON T.ID =V.TypeID

INNER JOIN VechilesDocumentRelation VDR ON VDR.VehicleID = V.ID

WHERE INC.ModuleID=2 and V.IsApproved=1 and VDR.TransportDocumentID=6 and V.TypeID!=2  and INC.UserTypeId=14 and INC.Active=1

and VDR.DocumentEndDate >= @StartDate and VDR.DocumentEndDate <= @EndDate

END