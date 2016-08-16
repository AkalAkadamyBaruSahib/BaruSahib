CREATE PROCEDURE [dbo].[USP_PendingStatusForPurchaser]         

(        

@StartDate datetime,

@EndDate datetime,

@PsId as int

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

AND ISNULL(EmployeeDispatchStatus,0)=0

AND ModifyOn >= @StartDate and ModifyOn <= @EndDate



END