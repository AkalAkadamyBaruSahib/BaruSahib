ALTER PROCEDURE [dbo].[USP_AllEstimateView]         



           



as            



begin            



SELECT     Estimate.EstId, Estimate.EstmateCost, CONVERT(NVARCHAR(20),  Estimate.SanctionDate, 107) AS dt, Estimate.SubEstimate, Zone.ZoneName, Academy.AcaName,  Academy.AcaID,   



                      WorkAllot.WorkAllotName, Estimate.FileNme, Estimate.FilePath,Estimate.CreatedOn, Estimate.ModifyOn  



FROM         Estimate INNER JOIN    



                      Zone ON Estimate.ZoneId = Zone.ZoneId 



                      INNER JOIN Academy ON Estimate.AcaId = Academy.AcaId 



                      INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId 







where Estimate.Active=1 



                      



                      order by      Estimate.EstId desc     



end 
