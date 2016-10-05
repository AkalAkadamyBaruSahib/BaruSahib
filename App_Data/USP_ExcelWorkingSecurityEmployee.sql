ALTER procedure [dbo].[USP_ExcelWorkingSecurityEmployee]
(
@DesigID as int,
@ZoneID as int
)
AS

BEGIN

SELECT      SecurityEmployeeInfo.Name AS Name,
			SecurityEmployeeInfo.MobileNo AS ContactNumber,
			SecurityEmployeeInfo.Salary AS CurrentSalary,
			Academy.AcaName AS Assigned_Academy, 
            Designation.Designation AS Designation,
			CONVERT(VARCHAR(10),SecurityEmployeeInfo.DateOfAppraisal,10) AS DateofAppraisal,
			CONVERT(VARCHAR(10),SecurityEmployeeInfo.DOJ,10) AS DateofJoining,
			CONVERT(VARCHAR(10),SecurityEmployeeInfo.LastAppraisal,10) As LastAppraisal

FROM        SecurityEmployeeInfo INNER JOIN

            Academy ON SecurityEmployeeInfo.AcaId = Academy.AcaId INNER JOIN

            Zone ON SecurityEmployeeInfo.ZoneId = Zone.ZoneId INNER JOIN

            Designation ON SecurityEmployeeInfo.DesigId = Designation.DesgId

WHERE       SecurityEmployeeInfo.IsApproved = 1 and SecurityEmployeeInfo.DesigID = @DesigID 
			and SecurityEmployeeInfo.ZoneID = @ZoneID

END