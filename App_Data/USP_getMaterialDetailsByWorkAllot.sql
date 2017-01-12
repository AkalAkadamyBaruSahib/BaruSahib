CREATE PROCEDURE [dbo].[USP_getMaterialDetailsByWorkAllot]            



(        



 @WaID int,         



 @PsId as int              



)              



AS                



BEGIN      



SELECT distinct  EstimateNumber



				,AcademyName



				,WorkAllotName



				,BillNumber



				,AgencyName



				,MatName



    			,EstQuantity



				,EstRate



				,EstAmount



				,PurchasedQTY



				,PurchasedRate



	      		from view_BillSubmitedDetails WHERE PSId=@PsId 



AND IsApproved=1  AND WAId=@WaID





END
