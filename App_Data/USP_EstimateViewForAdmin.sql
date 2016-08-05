

CREATE procedure [dbo].[USP_EstimateViewForAdmin]                    

(

@ModuleID as int,

@InchargeID as int                    

)                    

AS

begin                    

SELECT     Estimate.EstId,Academy.AcaId,Estimate.CreatedOn,Estimate.EstmateCost,ISNULL(Estimate.IsItemRejected,0) AS IsItemRejected,

ISNULL(Estimate.IsApproved,1) AS IsApproved,ISNULL(Estimate.IsRejected,0) AS IsRejected, CONVERT(VARCHAR(20), Estimate.ModifyOn, 107) AS dt, 

Estimate.SubEstimate, Zone.ZoneName, Academy.AcaName, WorkAllot.WorkAllotName, Estimate.FileNme, Estimate.FilePath,Estimate.ModifyOn

FROM       Estimate INNER JOIN              

Zone ON Estimate.ZoneId = Zone.ZoneId INNER JOIN              

Academy ON Estimate.AcaId = Academy.AcaId INNER JOIN              

WorkAllot ON Estimate.WAId = WorkAllot.WAId INNER JOIN  

AcademyAssignToEmployee AAE ON AAE.AcaId = Academy.AcaId INNER JOIN

Incharge Inc ON Inc.InchargeId = AAE.EmpId

WHERE Estimate.IsActive =1 and  Estimate.ModuleID = @ModuleID and inc.InchargeId=@InchargeID

order by  Estimate.ModifyOn desc    

-- order by  Estimate.IsItemRejected desc            

END 