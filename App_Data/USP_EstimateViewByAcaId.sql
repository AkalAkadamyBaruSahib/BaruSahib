ALTER procedure [dbo].[USP_EstimateViewByAcaId]          







(          







@AcaId as int         







)          







as          







begin          







SELECT     Estimate.EstId, Estimate.EstmateCost, CONVERT(VARCHAR(20), Estimate.SanctionDate, 107) AS dt, Estimate.SubEstimate, Zone.ZoneName, Academy.AcaName,   







                      WorkAllot.WorkAllotName, Academy.AcaId, Estimate.FileNme, Estimate.FilePath,Estimate.IsApproved  







FROM         Estimate INNER JOIN  







                      Zone ON Estimate.ZoneId = Zone.ZoneId INNER JOIN  







                      Academy ON Estimate.AcaId = Academy.AcaId INNER JOIN  







                      WorkAllot ON Estimate.WAId = WorkAllot.WAId where Estimate.AcaId=@AcaId  order by     Estimate.EstId desc    







end
