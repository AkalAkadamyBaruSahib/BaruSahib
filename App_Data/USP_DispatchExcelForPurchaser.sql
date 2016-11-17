CREATE PROCEDURE [dbo].[USP_DispatchExcelForPurchaser]         
(        
@StartDate datetime,
@EndDate datetime,
@PsId as int,
@PurchaseEmpID as int =0
)              
AS                
BEGIN      
SELECT distinct  EstId
				,Zone
				,Academy
				,MaterialType
				,Material
    			,UnitName
				,[EstimateQuantity]
				,[PurchaseQuantity]
				,ReceivedStoreQuantity
				,DispatchQuantity
				,[PendingQuantity]
				,Rate
				,Amount
				,CreatedOnDate
				,EmployeeAssignDate
    			,PurchaseDate
				,PurchaserName,  
				[Dispatch Status],  
                [Received BillNumber]
FROM EstimateReport 
WHERE PSId=@PsId
AND IsApproved=1 
AND ISNULL(IsReceived,0)=0
AND ((PurchaseEmpID = @PurchaseEmpID) AND (@PurchaseEmpID=@PurchaseEmpID)) AND ModifyOn >= @StartDate and ModifyOn <= @EndDate
END