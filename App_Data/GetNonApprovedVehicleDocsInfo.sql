ALTER PROCEDURE [dbo].GetNonApprovedVehicleDocsInfo    















(















@CurrentDate as DateTime,







@NextDate as DateTime















)















AS    















BEGIN    















SELECT DISTINCT Aca.AcaName AS AA_ACADEMY,V.number AS VEHICLE_NUMBER,T.Type AS TYPE_OF_VEHICLE,

TD.DocumentName AS DOCUMENT_NAME,

datename(DAY,VDR.DocumentEndDate)+' '+datename(M,VDR.DocumentEndDate)+' '+cast(datepart(YYYY,VDR.DocumentEndDate) as varchar) AS DOCUMENT_DUE_DATE

FROM Vehicles V

INNER JOIN dbo.AcademyAssignToEmployee A ON A.AcaID=V.AcademyID     

INNER JOIN Incharge INC ON INC.InchargeID=A.EMPID     

INNER JOIN Academy Aca ON Aca.AcaID = A.AcaID   

INNER JOIN TransportTypes T ON T.ID =V.TypeID

INNER JOIN VechilesDocumentRelation VDR ON VDR.VehicleID = V.ID

INNER JOIN TransportDocuments TD ON TD.ID = VDR.TransportDocumentID

WHERE INC.ModuleID=2 and VDR.IsApproved=0  and V.IsApproved=1 and VDR.DocumentEndDate >=@NextDate



--and VDR.CreatedOn >=@CurrentDate and VDR.CreatedOn <=@NextDate















END