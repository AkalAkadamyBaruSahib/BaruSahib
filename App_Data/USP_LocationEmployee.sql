CREATE procedure [dbo].[USP_LocationEmployee]  

  

as    

begin    

SELECT distinct Incharge.InchargeId, Incharge.InName, Incharge.InMobile, Designation.Designation, Department.department,AAE.ZoneId

FROM         Incharge 

INNER JOIN Designation ON Incharge.DesigId = Designation.DesgId

INNER JOIN AcademyAssignToEmployee AAE ON AAE.EmpId= Incharge.InchargeId

INNER JOIN Department ON Incharge.DepId = Department.DepId

end
