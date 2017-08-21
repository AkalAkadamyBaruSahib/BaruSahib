
CREATE TABLE [dbo].[StaffDetailInTransport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StaffName] [nvarchar](250) NULL,
	[StaffType] [int] NULL,
	[FatherName] [nvarchar](250) NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_StaffDetailInTransport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StaffDetailInTransport]  WITH CHECK ADD  CONSTRAINT [FK_StaffDetailInTransport_Incharge] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Incharge] ([InchargeId])
GO
ALTER TABLE [dbo].[StaffDetailInTransport] CHECK CONSTRAINT [FK_StaffDetailInTransport_Incharge]
GO
