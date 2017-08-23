USE [Akal]
GO
/****** Object:  Table [dbo].[ProformaDetail]    Script Date: 8/22/2017 4:17:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProformaDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AcaID] [int] NULL,
	[VehicleID] [int] NULL,
	[ProformaType] [int] NULL,
	[TyreTotalRunningKm] [decimal](18, 0) NULL,
	[FrontLeftSerialNum] [varchar](50) NULL,
	[FrontRightSerialNum] [varchar](50) NULL,
	[FrontLeftKm] [int] NULL,
	[FrontRightKm] [int] NULL,
	[RearLeftKm] [int] NULL,
	[RearRightKm] [int] NULL,
	[RearLeftSerialNum] [varchar](50) NULL,
	[RearRightSerialNum] [varchar](50) NULL,
	[FrontLeftOldTyreCondition] [varchar](150) NULL,
	[FrontRightOldTyreCondition] [varchar](150) NULL,
	[RearLeftOldTyreCondition] [varchar](150) NULL,
	[RearRightOldTyreCondition] [varchar](150) NULL,
	[StafneyOldTyreCondition] [varchar](150) NULL,
	[FrontLeftNewTyreRequired] [int] NULL,
	[FrontRightNewTyreRequired] [int] NULL,
	[RearLeftNewTyreRequired] [int] NULL,
	[RearRightNewTyreRequired] [int] NULL,
	[StafneyNewTyreRequired] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[StafneyKm] [int] NULL,
	[StafneySerialNum] [varchar](50) NULL,
	[LastDateTyreChanged] [datetime] NULL,
	[TyreSize] [nvarchar](150) NULL,
	[NoOfTyreRequired] [int] NULL,
	[CurrentMeterReading] [int] NULL,
	[NewTyreAmount] [decimal](18, 2) NULL,
	[LastMeterReadingOfTyreChanged] [int] NULL,
	[OldTyreSaleAmount] [decimal](18, 2) NULL,
	[ApprovalAmount] [decimal](18, 2) NULL,
	[TyreChangOnlastMeterReading] [int] NULL,
	[MrfRates] [decimal](18, 2) NULL,
	[MrfQty] [decimal](18, 2) NULL,
	[MrfAmount] [decimal](18, 2) NULL,
	[ApoloRates] [decimal](18, 2) NULL,
	[ApoloQty] [decimal](18, 2) NULL,
	[ApoloAmount] [decimal](18, 2) NULL,
	[CeatRates] [decimal](18, 2) NULL,
	[CeatQty] [decimal](18, 2) NULL,
	[CeatAmount] [decimal](18, 2) NULL,
	[JkRates] [decimal](18, 2) NULL,
	[JkQty] [decimal](18, 2) NULL,
	[JkAmount] [decimal](18, 2) NULL,
	[BatteryBillNo] [int] NULL,
	[BatteryType] [nvarchar](150) NULL,
	[InvertarCompany] [nvarchar](250) NULL,
	[NoOfRequiredNewBattery] [int] NULL,
	[NewMakeOfBattery] [nvarchar](250) NULL,
	[NewBatteryCapacity] [nvarchar](250) NULL,
	[NewBatterySrNo] [nvarchar](150) NULL,
	[NewBatteryLifeInYears] [nvarchar](150) NULL,
	[MakeOfBatteryAndCapacityOldBattery] [nvarchar](250) NULL,
	[OldBatterySrNo] [nvarchar](150) NULL,
	[OldBatteryPurchaseDate] [datetime] NULL,
	[OldBatterySalePrice] [decimal](18, 2) NULL,
	[MicrotekSizeOfBattery] [nvarchar](150) NULL,
	[MicrotekNoOfRequired] [int] NULL,
	[MicrotekPriceOfBattery] [decimal](18, 2) NULL,
	[TataSizeOfBattery] [nvarchar](150) NULL,
	[TataNoOfRequired] [int] NULL,
	[TataPriceOfBattery] [decimal](18, 2) NULL,
	[ExideSizeOfBattery] [nvarchar](150) NULL,
	[ExideNoOfRequired] [int] NULL,
	[ExidePriceOfBattery] [decimal](18, 2) NULL,
	[OkayaSizeOfBattery] [nvarchar](150) NULL,
	[OkayaNoOfRequired] [int] NULL,
	[OkayaPriceOfBattery] [decimal](18, 2) NULL,
	[GensetCompany] [nvarchar](250) NULL,
	[GensetSrNo] [nvarchar](150) NULL,
	[GensetPowerInKVA] [nvarchar](250) NULL,
	[GensetLastRepairDate] [datetime] NULL,
	[GensetLastQuotationAmount] [decimal](18, 2) NULL,
	[GensetCurrentQuotationAmount] [decimal](18, 2) NULL,
	[GensetTotalRunning] [nvarchar](150) NULL,
	[AverageRunning] [decimal](18, 2) NULL,
	[ServicePlaceAgency] [nvarchar](250) NULL,
	[ServiceLastMeterReading] [nvarchar](250) NULL,
	[ServiceCurrentMeterReading] [nvarchar](250) NULL,
	[ServiceQuotationAmount] [decimal](18, 2) NULL,
	[ServiceApprovalAmount] [decimal](18, 2) NULL,
	[AverageOfVehicle] [nvarchar](250) NULL,
	[GensetDate] [datetime] NULL,
 CONSTRAINT [PK_ProformaDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProformaMaterialDetail]    Script Date: 8/22/2017 4:17:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProformaMaterialDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProformaID] [int] NULL,
	[MatID] [int] NULL,
	[UnitID] [int] NULL,
	[Qty] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ProformaMaterialDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProformaDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProformaDetail_Academy] FOREIGN KEY([AcaID])
REFERENCES [dbo].[Academy] ([AcaId])
GO
ALTER TABLE [dbo].[ProformaDetail] CHECK CONSTRAINT [FK_ProformaDetail_Academy]
GO
ALTER TABLE [dbo].[ProformaDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProformaDetail_Incharge] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Incharge] ([InchargeId])
GO
ALTER TABLE [dbo].[ProformaDetail] CHECK CONSTRAINT [FK_ProformaDetail_Incharge]
GO
ALTER TABLE [dbo].[ProformaDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProformaDetail_Vehicles] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([ID])
GO
ALTER TABLE [dbo].[ProformaDetail] CHECK CONSTRAINT [FK_ProformaDetail_Vehicles]
GO
ALTER TABLE [dbo].[ProformaMaterialDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProformaMaterialDetail_Material] FOREIGN KEY([MatID])
REFERENCES [dbo].[Material] ([MatId])
GO
ALTER TABLE [dbo].[ProformaMaterialDetail] CHECK CONSTRAINT [FK_ProformaMaterialDetail_Material]
GO
ALTER TABLE [dbo].[ProformaMaterialDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProformaMaterialDetail_ProformaDetail] FOREIGN KEY([ProformaID])
REFERENCES [dbo].[ProformaDetail] ([ID])
GO
ALTER TABLE [dbo].[ProformaMaterialDetail] CHECK CONSTRAINT [FK_ProformaMaterialDetail_ProformaDetail]
GO
ALTER TABLE [dbo].[ProformaMaterialDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProformaMaterialDetail_Unit] FOREIGN KEY([UnitID])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[ProformaMaterialDetail] CHECK CONSTRAINT [FK_ProformaMaterialDetail_Unit]
GO
