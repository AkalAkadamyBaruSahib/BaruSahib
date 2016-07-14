ALTER procedure [dbo].[USP_DrwaingShowForAdmin]            
(
	@IsApproved as bit                   
)       
AS          
BEGIN

SELECT DrawingType.DwTypeName, Drawing.DwgId, Drawing.DwgNo, Drawing.RevisionNo,Drawing.DwgFileName, Drawing.PdfFileName, Drawing.PdfFilePath,
Drawing.DwgFileName AS Expr1,Drawing.DwgFilePath, Drawing.Active,CONVERT(VARCHAR(20), Drawing.CreatedOn, 107) AS CreatedOn, 
Drawing.CreatedBy, inc.InName,Drawing.DrawingName, Drawing.AcaId, Drawing.DwTypeId, Zone.ZoneName, Academy.AcaName,Drawing.IsApproved ,   ISNULL(Drawing.CreatedOn,GETDATE()) AS [CreatedOnDate]  

FROM Drawing 
INNER JOIN DrawingType ON Drawing.DwTypeId = DrawingType.DwTypeId
INNER JOIN Zone ON Drawing.ZoneId = Zone.ZoneId
INNER JOIN Academy ON Drawing.AcaId = Academy.AcaId
INNER JOIN Incharge inc on inc.inchargeID=Drawing.CreatedBy
WHERE Drawing.CreatedOn >= DATEADD(day,-60,getdate())

ORDER BY Drawing.CreatedOn DESC    































































                                  































































end 