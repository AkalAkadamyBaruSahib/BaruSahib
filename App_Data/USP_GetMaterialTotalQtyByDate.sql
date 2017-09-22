CREATE proc USP_GetMaterialTotalQtyByDate 



(              



@MatID int,



@DispatchDate varchar(10) 



)              







AS



BEGIN



SELECT  SUM(W.DispatchQty) AS QTY



FROM WorkshopDispatchMaterial W



INNER JOIN EstimateAndMaterialOthersRelations EMR ON W.EMRID = EMR.Sno 



WHERE W.MatID=@MatID and convert(varchar(10), W.DispatchOn, 105)=@DispatchDate



END