ALTER procedure [dbo].[USP_EstimateDetailByWorkAllot]          

(          

@WorkAllotId as int,

@PSID as int

)         

AS          

BEGIN          

SELECT    distinct Estimate.EstId, SUM( distinct EM.Amount) AS EstimateCost, Estimate.SubEstimate, Zone.ZoneName, Academy.AcaName,   

           WorkAllot.WorkAllotName,Estimate.IsApproved

FROM       Estimate INNER JOIN 

           EstimateAndMaterialOthersRelations EM ON Estimate.EstId=EM.EstId INNER JOIN  

           Zone ON Estimate.ZoneId = Zone.ZoneId INNER JOIN  

           Academy ON Estimate.AcaId = Academy.AcaId INNER JOIN  

           WorkAllot ON Estimate.WAId = WorkAllot.WAId where Estimate.WAId=@WorkAllotId and EM.PSId=@PSID and  Estimate.IsApproved = 1 

		   Group by Estimate.EstId,Estimate.SubEstimate,Zone.ZoneName,Academy.AcaName,WorkAllot.WorkAllotName,Estimate.IsApproved

END
