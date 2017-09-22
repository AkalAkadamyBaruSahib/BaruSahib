CREATE procedure [dbo].[USP_GetMaterialTotalQtyInWorkshop]            



(              



@MatID int





)              



AS                



BEGIN 



SELECT SUM(W.DispatchQty) AS DispatchQty



FROM WorkshopDispatchMaterial W



WHERE  W.MatID = @MatID 



END




