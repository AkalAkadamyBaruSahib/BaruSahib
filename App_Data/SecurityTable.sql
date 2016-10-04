USE [Akal]
GO
/****** Object:  Table [dbo].[EmployeeTransfer]    Script Date: 10/4/2016 4:55:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeeTransfer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpID] [int] NULL,
	[OldAcaID] [int] NULL,
	[NewAcaID] [int] NULL,
	[OldZoneID] [int] NULL,
	[NewZoneID] [int] NULL,
	[DateOfTransfer] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[TransferLatter] [varchar](200) NULL,
 CONSTRAINT [PK_EmployeeTransfer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SecurityEmployeeInfo]    Script Date: 10/4/2016 4:55:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SecurityEmployeeInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Address] [varchar](200) NULL,
	[MobileNo] [varchar](50) NULL,
	[Salary] [varchar](50) NULL,
	[Deduction] [int] NULL,
	[AcaID] [int] NULL,
	[ZoneID] [int] NULL,
	[Education] [varchar](200) NULL,
	[DesigID] [int] NULL,
	[AppointmentLetter] [varchar](200) NULL,
	[ExperienceLetter] [varchar](200) NULL,
	[FamilyRationCard] [varchar](200) NULL,
	[PCC] [varchar](200) NULL,
	[QualificationLetter] [varchar](200) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifyOn] [datetime] NULL,
	[IsApproved] [bit] NULL,
	[Photo] [varchar](200) NULL,
	[DOJ] [datetime] NULL,
	[DateOfAppraisal] [datetime] NULL,
	[LastAppraisal] [varchar](50) NULL,
 CONSTRAINT [PK_SecurityEmployeeInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[EmployeeTransfer]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTransfer_Academy] FOREIGN KEY([NewAcaID])
REFERENCES [dbo].[Academy] ([AcaId])
GO
ALTER TABLE [dbo].[EmployeeTransfer] CHECK CONSTRAINT [FK_EmployeeTransfer_Academy]
GO
ALTER TABLE [dbo].[EmployeeTransfer]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTransfer_Academy1] FOREIGN KEY([OldAcaID])
REFERENCES [dbo].[Academy] ([AcaId])
GO
ALTER TABLE [dbo].[EmployeeTransfer] CHECK CONSTRAINT [FK_EmployeeTransfer_Academy1]
GO
ALTER TABLE [dbo].[EmployeeTransfer]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTransfer_Incharge] FOREIGN KEY([EmpID])
REFERENCES [dbo].[Incharge] ([InchargeId])
GO
ALTER TABLE [dbo].[EmployeeTransfer] CHECK CONSTRAINT [FK_EmployeeTransfer_Incharge]
GO
ALTER TABLE [dbo].[EmployeeTransfer]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTransfer_Incharge1] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Incharge] ([InchargeId])
GO
ALTER TABLE [dbo].[EmployeeTransfer] CHECK CONSTRAINT [FK_EmployeeTransfer_Incharge1]
GO
ALTER TABLE [dbo].[EmployeeTransfer]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTransfer_Zone] FOREIGN KEY([OldZoneID])
REFERENCES [dbo].[Zone] ([ZoneId])
GO
ALTER TABLE [dbo].[EmployeeTransfer] CHECK CONSTRAINT [FK_EmployeeTransfer_Zone]
GO
ALTER TABLE [dbo].[EmployeeTransfer]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTransfer_Zone1] FOREIGN KEY([NewZoneID])
REFERENCES [dbo].[Zone] ([ZoneId])
GO
ALTER TABLE [dbo].[EmployeeTransfer] CHECK CONSTRAINT [FK_EmployeeTransfer_Zone1]
GO
ALTER TABLE [dbo].[SecurityEmployeeInfo]  WITH CHECK ADD  CONSTRAINT [FK_SecurityEmployeeInfo_Academy] FOREIGN KEY([AcaID])
REFERENCES [dbo].[Academy] ([AcaId])
GO
ALTER TABLE [dbo].[SecurityEmployeeInfo] CHECK CONSTRAINT [FK_SecurityEmployeeInfo_Academy]
GO
ALTER TABLE [dbo].[SecurityEmployeeInfo]  WITH CHECK ADD  CONSTRAINT [FK_SecurityEmployeeInfo_Designation] FOREIGN KEY([DesigID])
REFERENCES [dbo].[Designation] ([DesgId])
GO
ALTER TABLE [dbo].[SecurityEmployeeInfo] CHECK CONSTRAINT [FK_SecurityEmployeeInfo_Designation]
GO
ALTER TABLE [dbo].[SecurityEmployeeInfo]  WITH CHECK ADD  CONSTRAINT [FK_SecurityEmployeeInfo_Zone] FOREIGN KEY([ZoneID])
REFERENCES [dbo].[Zone] ([ZoneId])
GO
ALTER TABLE [dbo].[SecurityEmployeeInfo] CHECK CONSTRAINT [FK_SecurityEmployeeInfo_Zone]
GO
