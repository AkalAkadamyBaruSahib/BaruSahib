ALTER procedure [dbo].[USP_EmpEstimateAcaWise]    

(    

@User int,    

@AcaId as int ,

@ModuleID as int      

)    

as    

begin    

SELECT DISTINCT Estimate.EstId, Estimate.ZoneId,ISNULL(Estimate.IsApproved,1) AS IsApproved,

ISNULL(Estimate.IsRejected,0) AS IsRejected,Z.ZoneName,A.AcaName, Estimate.SubEstimate, 

CONVERT(nvarchar(20), Estimate.SanctionDate, 107) AS SanctionDate, Estimate.EstmateCost, 

Estimate.AcaId, WorkAllot.WorkAllotName,ISNULL(Estimate.IsItemRejected,0) AS IsItemRejected      



FROM  Incharge     

INNER JOIN AcademyAssignToEmployee ON Incharge.InchargeId = AcademyAssignToEmployee.EmpId     

INNER JOIN Estimate ON AcademyAssignToEmployee.AcaId = Estimate.AcaId   

INNER JOIN Zone Z ON Z.ZoneID=Estimate.ZoneId      

INNER JOIN Academy A ON A.AcaId=Estimate.AcaId  

INNER JOIN  WorkAllot ON Estimate.WAId = WorkAllot.WAId     

where     



Incharge.InchargeId=@User     

and Estimate.AcaId=@AcaId 

and Estimate.ModuleID = @ModuleID   



order by Estimate.EstId desc    



end  
