
CREATE TABLE [dbo].[Android_AbsentConductorArray](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransportDailyProformaID] [int] NULL,
	[VehicleID] [int] NULL,
	[Reason] [nvarchar](max) NULL,
 CONSTRAINT [PK_Android_AbsentConductorArray] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Android_AcademyVisitDetail]    Script Date: 2/2/2018 11:01:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Android_AcademyVisitDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AcaCampusVisitTime] [datetime] NULL,
	[AcaCampusVisitSolution] [nvarchar](max) NULL,
	[AcaCampusVisitComplaint] [nvarchar](max) NULL,
	[TransportDailyProformaID] [int] NULL,
 CONSTRAINT [PK_Android_AcademyVisitDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Android_TransportComplaintArray]    Script Date: 2/2/2018 11:01:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Android_TransportComplaintArray](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransportDailyProformaID] [int] NULL,
	[VehicleID] [int] NULL,
	[ComplaintID] [int] NULL,
	[Solution] [nvarchar](max) NULL,
	[OtherComplaint] [nvarchar](max) NULL,
 CONSTRAINT [PK_Android_TransportComplaintArray] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Android_TransportDailyProformaDetail]    Script Date: 2/2/2018 11:01:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Android_TransportDailyProformaDetail](
	[TProformaID] [int] IDENTITY(1,1) NOT NULL,
	[AcaID] [int] NULL,
	[Teacher] [nvarchar](250) NULL,
	[FirstSecurityGuard] [nvarchar](250) NULL,
	[SecondSecurityGuard] [nvarchar](250) NULL,
	[Supervisor] [bit] NULL,
	[PresentStaff] [int] NULL,
	[TotalStaff] [int] NULL,
	[PresentStuMorning] [int] NULL,
	[TotalStuMorning] [int] NULL,
	[TotalStuEvening] [int] NULL,
	[PresentStuEvening] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_Android_TransportDailyProformaDetail] PRIMARY KEY CLUSTERED 
(
	[TProformaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Android_WithoutUniformDriverAndConductorArray]    Script Date: 2/2/2018 11:01:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Android_WithoutUniformDriverAndConductorArray](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransportDailyProformaID] [int] NULL,
	[VehicleID] [int] NULL,
	[Name] [nvarchar](250) NULL,
	[EmployeeTypeId] [int] NULL,
 CONSTRAINT [PK_Android_WithoutUniformDriverAndConductorArray] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LateArrivingVehiclesMorningAndEvening]    Script Date: 2/2/2018 11:01:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LateArrivingVehiclesMorningAndEvening](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransportDailyProformaID] [int] NULL,
	[VehicleID] [int] NULL,
	[TimeOfArrival] [datetime] NULL,
	[ReasonID] [int] NULL,
	[DayType] [nvarchar](50) NULL,
	[OtherReason] [nvarchar](max) NULL,
 CONSTRAINT [PK_LateArrivingVehiclesMorningAndEvening] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Android_AbsentConductorArray]  WITH CHECK ADD  CONSTRAINT [FK_Android_AbsentConductorArray_Android_TransportDailyProformaDetail] FOREIGN KEY([TransportDailyProformaID])
REFERENCES [dbo].[Android_TransportDailyProformaDetail] ([TProformaID])
GO
ALTER TABLE [dbo].[Android_AbsentConductorArray] CHECK CONSTRAINT [FK_Android_AbsentConductorArray_Android_TransportDailyProformaDetail]
GO
ALTER TABLE [dbo].[Android_AbsentConductorArray]  WITH CHECK ADD  CONSTRAINT [FK_Android_AbsentConductorArray_Vehicles] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([ID])
GO
ALTER TABLE [dbo].[Android_AbsentConductorArray] CHECK CONSTRAINT [FK_Android_AbsentConductorArray_Vehicles]
GO
ALTER TABLE [dbo].[Android_AcademyVisitDetail]  WITH CHECK ADD  CONSTRAINT [FK_Android_AcademyVisitDetail_Android_AcademyVisitDetail] FOREIGN KEY([ID])
REFERENCES [dbo].[Android_AcademyVisitDetail] ([ID])
GO
ALTER TABLE [dbo].[Android_AcademyVisitDetail] CHECK CONSTRAINT [FK_Android_AcademyVisitDetail_Android_AcademyVisitDetail]
GO
ALTER TABLE [dbo].[Android_AcademyVisitDetail]  WITH CHECK ADD  CONSTRAINT [FK_Android_AcademyVisitDetail_Android_TransportDailyProformaDetail] FOREIGN KEY([TransportDailyProformaID])
REFERENCES [dbo].[Android_TransportDailyProformaDetail] ([TProformaID])
GO
ALTER TABLE [dbo].[Android_AcademyVisitDetail] CHECK CONSTRAINT [FK_Android_AcademyVisitDetail_Android_TransportDailyProformaDetail]
GO
ALTER TABLE [dbo].[Android_TransportComplaintArray]  WITH CHECK ADD  CONSTRAINT [FK_Android_TransportComplaintArray_Android_TransportDailyProformaDetail] FOREIGN KEY([TransportDailyProformaID])
REFERENCES [dbo].[Android_TransportDailyProformaDetail] ([TProformaID])
GO
ALTER TABLE [dbo].[Android_TransportComplaintArray] CHECK CONSTRAINT [FK_Android_TransportComplaintArray_Android_TransportDailyProformaDetail]
GO
ALTER TABLE [dbo].[Android_TransportComplaintArray]  WITH CHECK ADD  CONSTRAINT [FK_Android_TransportComplaintArray_Transport_ComplaintReason] FOREIGN KEY([ComplaintID])
REFERENCES [dbo].[Transport_ComplaintReason] ([ID])
GO
ALTER TABLE [dbo].[Android_TransportComplaintArray] CHECK CONSTRAINT [FK_Android_TransportComplaintArray_Transport_ComplaintReason]
GO
ALTER TABLE [dbo].[Android_TransportComplaintArray]  WITH CHECK ADD  CONSTRAINT [FK_Android_TransportComplaintArray_Vehicles] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([ID])
GO
ALTER TABLE [dbo].[Android_TransportComplaintArray] CHECK CONSTRAINT [FK_Android_TransportComplaintArray_Vehicles]
GO
ALTER TABLE [dbo].[Android_TransportDailyProformaDetail]  WITH CHECK ADD  CONSTRAINT [FK_Android_TransportDailyProformaDetail_Academy] FOREIGN KEY([AcaID])
REFERENCES [dbo].[Academy] ([AcaId])
GO
ALTER TABLE [dbo].[Android_TransportDailyProformaDetail] CHECK CONSTRAINT [FK_Android_TransportDailyProformaDetail_Academy]
GO
ALTER TABLE [dbo].[Android_TransportDailyProformaDetail]  WITH CHECK ADD  CONSTRAINT [FK_Android_TransportDailyProformaDetail_Incharge] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Incharge] ([InchargeId])
GO
ALTER TABLE [dbo].[Android_TransportDailyProformaDetail] CHECK CONSTRAINT [FK_Android_TransportDailyProformaDetail_Incharge]
GO
ALTER TABLE [dbo].[Android_WithoutUniformDriverAndConductorArray]  WITH CHECK ADD  CONSTRAINT [FK_Android_WithoutUniformDriverAndConductorArray_Android_TransportDailyProformaDetail] FOREIGN KEY([TransportDailyProformaID])
REFERENCES [dbo].[Android_TransportDailyProformaDetail] ([TProformaID])
GO
ALTER TABLE [dbo].[Android_WithoutUniformDriverAndConductorArray] CHECK CONSTRAINT [FK_Android_WithoutUniformDriverAndConductorArray_Android_TransportDailyProformaDetail]
GO
ALTER TABLE [dbo].[Android_WithoutUniformDriverAndConductorArray]  WITH CHECK ADD  CONSTRAINT [FK_Android_WithoutUniformDriverAndConductorArray_Vehicles] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([ID])
GO
ALTER TABLE [dbo].[Android_WithoutUniformDriverAndConductorArray] CHECK CONSTRAINT [FK_Android_WithoutUniformDriverAndConductorArray_Vehicles]
GO
ALTER TABLE [dbo].[LateArrivingVehiclesMorningAndEvening]  WITH CHECK ADD  CONSTRAINT [FK_LateArrivingVehiclesMorningAndEvening_Android_TransportDailyProformaDetail] FOREIGN KEY([TransportDailyProformaID])
REFERENCES [dbo].[Android_TransportDailyProformaDetail] ([TProformaID])
GO
ALTER TABLE [dbo].[LateArrivingVehiclesMorningAndEvening] CHECK CONSTRAINT [FK_LateArrivingVehiclesMorningAndEvening_Android_TransportDailyProformaDetail]
GO
ALTER TABLE [dbo].[LateArrivingVehiclesMorningAndEvening]  WITH CHECK ADD  CONSTRAINT [FK_LateArrivingVehiclesMorningAndEvening_Transport_LateArrivingReason] FOREIGN KEY([ReasonID])
REFERENCES [dbo].[Transport_LateArrivingReason] ([ID])
GO
ALTER TABLE [dbo].[LateArrivingVehiclesMorningAndEvening] CHECK CONSTRAINT [FK_LateArrivingVehiclesMorningAndEvening_Transport_LateArrivingReason]
GO
ALTER TABLE [dbo].[LateArrivingVehiclesMorningAndEvening]  WITH CHECK ADD  CONSTRAINT [FK_LateArrivingVehiclesMorningAndEvening_Vehicles] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([ID])
GO
ALTER TABLE [dbo].[LateArrivingVehiclesMorningAndEvening] CHECK CONSTRAINT [FK_LateArrivingVehiclesMorningAndEvening_Vehicles]
GO
