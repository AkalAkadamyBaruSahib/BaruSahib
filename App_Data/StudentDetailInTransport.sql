CREATE TABLE [dbo].[StudentDetailInTransport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AdmissionNumber] [int] NULL,
	[Class] [nvarchar](50) NULL,
	[StudentName] [nvarchar](50) NULL,
	[FatherName] [nvarchar](50) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[NameOfVillage] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[AcaID] [int] NULL,
 CONSTRAINT [PK_StudentDetailInTransport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StudentDetailInTransport]  WITH CHECK ADD  CONSTRAINT [FK_StudentDetailInTransport_Academy] FOREIGN KEY([AcaID])
REFERENCES [dbo].[Academy] ([AcaId])
GO
ALTER TABLE [dbo].[StudentDetailInTransport] CHECK CONSTRAINT [FK_StudentDetailInTransport_Academy]
GO
ALTER TABLE [dbo].[StudentDetailInTransport]  WITH CHECK ADD  CONSTRAINT [FK_StudentDetailInTransport_Incharge] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Incharge] ([InchargeId])
GO
ALTER TABLE [dbo].[StudentDetailInTransport] CHECK CONSTRAINT [FK_StudentDetailInTransport_Incharge]
GO
