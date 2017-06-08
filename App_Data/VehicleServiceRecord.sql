USE [Akal]
GO
/****** Object:  Table [dbo].[VehicleServiceRecord]    Script Date: 6/7/2017 5:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VehicleServiceRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AcaID] [int] NULL,
	[VehicleID] [int] NULL,
	[CurrentKm] [decimal](18, 0) NULL,
	[FrontLeftSerialNum] [varchar](50) NULL,
	[FrontRightSerialNum] [varchar](50) NULL,
	[FrontLeftKm] [int] NULL,
	[FrontRightKm] [int] NULL,
	[RearLeftOneKm] [int] NULL,
	[RearLeftSecondKm] [int] NULL,
	[RearRightOneKm] [int] NULL,
	[RearRightSecondKm] [int] NULL,
	[RearLeftOneSerialNum] [varchar](50) NULL,
	[RearLeftSecondSerialNum] [varchar](50) NULL,
	[RearRightOneSerialNum] [varchar](50) NULL,
	[RearRightSecondSerialNum] [varchar](50) NULL,
	[LastServiceKm] [int] NULL,
	[MeterReadingFilePath] [nvarchar](max) NULL,
	[BatteryInstalationDate] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[StafneyKm] [int] NULL,
	[StafneySerialNum] [varchar](50) NULL,
	[MakeofBattery] [nvarchar](50) NULL,
	[BatteryCapacity] [varchar](50) NULL,
	[BatterySerialNum] [varchar](50) NULL,
	[BatteryLifeInYears] [int] NULL,
	[LastServiceDate] [datetime] NULL,
 CONSTRAINT [PK_VehicleServiceRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[VehicleServiceRecord]  WITH CHECK ADD  CONSTRAINT [FK_VehicleServiceRecord_Academy] FOREIGN KEY([AcaID])
REFERENCES [dbo].[Academy] ([AcaId])
GO
ALTER TABLE [dbo].[VehicleServiceRecord] CHECK CONSTRAINT [FK_VehicleServiceRecord_Academy]
GO
ALTER TABLE [dbo].[VehicleServiceRecord]  WITH CHECK ADD  CONSTRAINT [FK_VehicleServiceRecord_Incharge] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Incharge] ([InchargeId])
GO
ALTER TABLE [dbo].[VehicleServiceRecord] CHECK CONSTRAINT [FK_VehicleServiceRecord_Incharge]
GO
ALTER TABLE [dbo].[VehicleServiceRecord]  WITH CHECK ADD  CONSTRAINT [FK_VehicleServiceRecord_Vehicles] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([ID])
GO
ALTER TABLE [dbo].[VehicleServiceRecord] CHECK CONSTRAINT [FK_VehicleServiceRecord_Vehicles]
GO
