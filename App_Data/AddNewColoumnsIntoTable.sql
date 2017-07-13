ALTER TABLE EstimateAndMaterialOthersRelations ADD GST decimal(18, 2)
ALTER TABLE Material ADD GST decimal(18, 2)
ALTER TABLE MaterialNonApprovedRate ADD GST decimal(18, 2)
ALTER TABLE PurchaseOrderDetail ADD GST decimal(18, 2)

Update Material Set GST=0
Update EstimateAndMaterialOthersRelations Set GST=0
Update MaterialNonApprovedRate Set GST=0
Update PurchaseOrderDetail Set GST=0

ALTER TABLE PurchaseOrderDetail DROP COLUMN Excise