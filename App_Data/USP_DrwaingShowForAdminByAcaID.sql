CREATE procedure [dbo].[USP_DrwaingShowForAdminByAcaID]            



(







@AcaId as int,

@IsApproved as bit                   







)       



as            































begin            































SELECT     DrawingType.DwTypeName, Drawing.DwgId, Drawing.DwgNo, Drawing.RevisionNo, Drawing.DwgFileName, Drawing.PdfFileName, Drawing.PdfFilePath,       































                      Drawing.DwgFileName AS Expr1, Drawing.DwgFilePath, Drawing.Active, CONVERT(VARCHAR(20), Drawing.CreatedOn, 107) AS CreatedOn, Drawing.CreatedBy,       































                      Drawing.DrawingName, Drawing.AcaId, Drawing.DwTypeId, Zone.ZoneName, Academy.AcaName,    































                      Drawing.IsApproved ,   ISNULL(Drawing.CreatedOn,GETDATE()) AS [CreatedOnDate]  































FROM         Drawing INNER JOIN      































                      DrawingType ON Drawing.DwTypeId = DrawingType.DwTypeId INNER JOIN      































                      Zone ON Drawing.ZoneId = Zone.ZoneId INNER JOIN      































                      Academy ON Drawing.AcaId = Academy.AcaId     































					  WHERE Drawing.Active=1  and  Drawing.AcaId = @AcaId and  Drawing.IsApproved = @IsApproved































                      Order by Drawing.CreatedOn desc    































                                  































end 