ALTER procedure [dbo].[USP_EstimateStatusReportForWorkshop]            
(              
@StartDate datetime,
@EndDate datetime,              
@PsId as int
)              
AS                
BEGIN      
SELECT E.EstId,Z.ZoneName as Zone,A.AcaName as Academy,MT.MatTypeName as MaterialType,MA.MatName as Material,U.UnitName as Unit,
M.Qty,MA.AkalWorkshopRate as Rate,M.Qty * MA.AkalWorkshopRate as Amount,      
ISNULL(INC.InName,'') AS WorkshopName,E.CreatedOn AS CreatedOnDate,convert(nvarchar(20),M.EmployeeAssignDateTime,101) AS WorkshopAssignDate,convert(nvarchar(20),M.DispatchDate,100)as DispatchDate,case M.DispatchStatus when '1' then 'Dispatch' else 'Pending' end as DispatchStatus,M.remarkByPurchase as 
Remarks  
FROM Estimate E      
INNER JOIN EstimateAndMaterialOthersRelations M ON E.EstId = M.EstId    
INNER JOIN Academy A ON E.AcaId = A.AcaId      
INNER JOIN Zone Z ON E.ZoneId = Z.ZoneId      
INNER JOIN Workallot W ON E.WAId = W.WAId      
INNER JOIN Material MA ON M.MatId = MA.MatId   
INNER JOIN MaterialType MT ON M.MatTypeId = MT.MatTypeId     
INNER JOIN Unit U ON U.UnitId = M.UnitId      
INNER JOIN PurchaseSource P ON P.PSId=m.PSId      
LEFT OUTER JOIN INCHARGE INC ON INC.InchargeId=M.PurchaseEmpID      
WHERE E.IsApproved=1 AND m.PSId=@PsId AND E.ModifyOn >= @StartDate and E.ModifyOn <= @EndDate       
order by  E.EstId DESC      
END