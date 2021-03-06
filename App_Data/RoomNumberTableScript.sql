USE [Akal]
GO
/****** Object:  Table [dbo].[RoomNumbers]    Script Date: 10/21/2016 10:17:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomNumbers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BuildingID] [int] NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[BuildingFloor] [int] NOT NULL,
	[NumOfBed] [int] NULL,
 CONSTRAINT [PK_RoomNumbers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[RoomNumbers] ON 

INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (1, 1, N'101-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (2, 1, N'102-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (3, 1, N'103-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (4, 1, N'104-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (5, 1, N'105-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (6, 1, N'106-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (7, 1, N'107-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (8, 1, N'108-B', 0, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (9, 1, N'109-B', 0, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (10, 1, N'110-B', 0, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (11, 1, N'111-B', 0, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (12, 1, N'112-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (13, 1, N'113-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (14, 1, N'114-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (15, 1, N'115-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (16, 1, N'116-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (17, 1, N'117-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (18, 1, N'118-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (19, 1, N'119-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (20, 1, N'120-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (21, 1, N'121-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (22, 1, N'122-B', 0, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (23, 1, N'123-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (24, 1, N'124-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (25, 1, N'125-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (26, 1, N'126-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (27, 1, N'127-B', 0, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (28, 1, N'101-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (29, 1, N'102-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (30, 1, N'103-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (31, 1, N'104-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (32, 1, N'105-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (33, 1, N'106-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (34, 1, N'107-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (35, 1, N'108-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (36, 1, N'109-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (37, 1, N'110-VIP', 1, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (38, 1, N'111-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (39, 1, N'112-VIP', 1, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (40, 1, N'113-VIP', 1, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (41, 1, N'114-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (42, 1, N'115-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (43, 1, N'116-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (44, 1, N'117-VIP', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (45, 1, N'201-A-B', 2, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (46, 1, N'202-B', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (47, 1, N'203-VIP', 2, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (48, 1, N'204-VIP', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (49, 1, N'205-VIP', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (50, 1, N'206-VIP', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (51, 1, N'207-VIP', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (52, 1, N'208-VIP', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (53, 1, N'209-VIP', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (54, 1, N'210-VIP', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (55, 1, N'211-VIP', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (56, 1, N'212-A-B', 2, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (57, 1, N'213-A-B', 2, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (58, 1, N'301-VIP', 3, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (59, 1, N'302-VIP', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (60, 1, N'303-VIP', 3, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (61, 1, N'304-VIP', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (62, 1, N'305-VIP', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (63, 1, N'306-VIP', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (64, 1, N'307-VIP', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (65, 1, N'308-VIP', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (66, 1, N'309-VIP', 3, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (67, 1, N'310-VIP', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (68, 1, N'311-A-B', 3, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (69, 1, N'312-A-B', 3, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (70, 1, N'401-A', 4, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (71, 1, N'402-VIP', 4, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (72, 1, N'403-VIP', 4, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (73, 1, N'404-VIP', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (74, 1, N'405-VIP', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (75, 1, N'406-VIP', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (76, 1, N'407-VIP', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (77, 1, N'408-VIP', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (78, 1, N'409-VIP', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (79, 1, N'410-VIP', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (80, 1, N'411', 4, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (81, 1, N'412-A-B', 4, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (82, 1, N'413-A-B', 4, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (83, 1, N'501-A-B', 5, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (84, 1, N'502-B', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (85, 1, N'503-VIP', 5, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (86, 1, N'504-VIP', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (87, 1, N'505-VIP', 5, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (88, 1, N'506-VIP', 5, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (89, 1, N'507-VIP', 5, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (90, 1, N'508-VIP', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (91, 1, N'509-VIP', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (92, 1, N'510-VIP', 5, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (93, 1, N'511-VIP', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (94, 1, N'512-A-B', 5, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (95, 1, N'513-A-B', 5, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (96, 2, N'1', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (97, 2, N'2', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (98, 2, N'3', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (99, 2, N'4', 1, 2)
GO
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (100, 2, N'5', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (101, 2, N'6', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (102, 2, N'7', 1, 3)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (103, 2, N'8', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (104, 2, N'9', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (105, 2, N'10', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (106, 2, N'11', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (107, 2, N'12', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (108, 2, N'13', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (109, 2, N'14', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (110, 2, N'15', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (111, 2, N'16', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (112, 2, N'17', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (113, 2, N'18', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (114, 2, N'19', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (115, 2, N'20', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (116, 2, N'21', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (117, 2, N'22', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (118, 2, N'23', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (119, 2, N'24', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (120, 2, N'25', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (121, 2, N'26', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (122, 2, N'27', 2, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (123, 2, N'28', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (124, 2, N'29', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (125, 2, N'30', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (126, 2, N'31', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (127, 2, N'32', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (128, 2, N'33', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (129, 2, N'34', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (130, 2, N'35', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (131, 2, N'36', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (132, 2, N'37', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (133, 2, N'38', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (134, 2, N'39', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (135, 2, N'40', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (136, 2, N'41', 3, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (137, 2, N'42', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (138, 2, N'43', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (139, 2, N'44', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (140, 2, N'45', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (141, 2, N'46', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (142, 2, N'47', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (143, 2, N'48', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (144, 2, N'49', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (145, 2, N'50', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (146, 2, N'51', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (147, 2, N'52', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (148, 2, N'53', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (149, 2, N'54', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (150, 2, N'55', 4, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (151, 2, N'56', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (152, 2, N'57', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (153, 2, N'58', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (154, 2, N'59', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (155, 2, N'60', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (156, 2, N'61', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (157, 2, N'62', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (158, 2, N'63', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (159, 2, N'64', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (160, 2, N'65', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (161, 2, N'66', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (162, 2, N'67', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (163, 2, N'68', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (164, 2, N'69', 5, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (165, 2, N'70', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (166, 2, N'71', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (167, 2, N'72', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (168, 2, N'73', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (169, 2, N'74', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (170, 2, N'75', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (171, 2, N'76', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (172, 2, N'77', 6, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (173, 2, N'78', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (174, 2, N'79', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (175, 2, N'80', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (176, 2, N'81', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (177, 2, N'82', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (178, 2, N'83', 6, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (179, 2, N'84', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (180, 2, N'85', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (181, 2, N'86', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (182, 2, N'87', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (183, 2, N'88', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (184, 2, N'89', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (185, 2, N'90', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (186, 2, N'91', 7, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (187, 2, N'92', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (188, 2, N'93', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (189, 2, N'94', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (190, 2, N'95', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (191, 2, N'96', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (192, 2, N'97', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (193, 2, N'98', 7, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (194, 2, N'99', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (195, 2, N'100', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (196, 2, N'101', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (197, 2, N'102', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (198, 2, N'103', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (199, 2, N'104', 8, 2)
GO
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (200, 2, N'105', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (201, 2, N'106', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (202, 2, N'107', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (203, 2, N'108', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (204, 2, N'109', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (205, 2, N'110', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (206, 2, N'111', 8, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (207, 1, N'202-A', 2, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (208, 1, N'502-A', 5, 1)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (209, 3, N'N-1', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (210, 3, N'N-2', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (211, 3, N'N-3', 1, 6)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (212, 3, N'N-4', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (213, 3, N'N-5', 1, 6)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (214, 3, N'N-6', 1, 6)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (215, 3, N'N-7', 1, 6)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (216, 3, N'N-8', 1, 2)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (217, 3, N'N-9', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (218, 3, N'N-10', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (219, 3, N'N-11', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (220, 3, N'N-12', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (221, 3, N'N-13', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (222, 3, N'N-14', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (223, 3, N'N-15', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (224, 3, N'N-16', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (225, 3, N'N-17', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (226, 3, N'N-18', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (227, 3, N'N-19', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (228, 3, N'N-20', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (229, 3, N'N-21', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (230, 3, N'N-22', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (231, 3, N'N-23', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (232, 3, N'N-24', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (233, 3, N'N-25', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (234, 3, N'N-26', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (235, 3, N'N-27', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (236, 3, N'N-28', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (237, 3, N'N-29', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (238, 3, N'N-30', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (239, 3, N'N-31', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (240, 3, N'N-32', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (241, 3, N'N-33', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (242, 3, N'N-34', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (243, 3, N'N-35', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (244, 3, N'N-36', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (245, 3, N'N-37', 1, 6)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (246, 3, N'N-38', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (247, 3, N'N-39', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (248, 3, N'N-40', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (249, 3, N'N-41', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (250, 3, N'N-42', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (251, 3, N'N-43', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (252, 3, N'N-44', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (253, 3, N'N-45', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (254, 3, N'N-46', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (255, 3, N'N-47', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (256, 3, N'N-48', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (257, 3, N'N-49', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (258, 3, N'N-50', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (259, 3, N'N-51', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (260, 3, N'N-52', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (261, 3, N'N-53', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (262, 3, N'N-54', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (263, 3, N'N-55', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (264, 3, N'N-56', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (265, 3, N'N-57', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (266, 3, N'N-58', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (267, 3, N'N-59', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (268, 3, N'N-60', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (269, 3, N'N-61', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (270, 3, N'N-62', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (271, 3, N'N-63', 1, 4)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (272, 3, N'HALL-3', 1, 14)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (273, 3, N'HALL-4', 1, 19)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (274, 3, N'HALL-5', 1, 27)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (275, 3, N'HALL-6', 1, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (276, 3, N'HALL-7', 1, NULL)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (277, 3, N'HALL-8', 1, 14)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (278, 3, N'HALL-9', 1, 13)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (279, 3, N'HALL-10', 1, 43)
INSERT [dbo].[RoomNumbers] ([ID], [BuildingID], [Number], [BuildingFloor], [NumOfBed]) VALUES (280, 3, N'MESS HALL', 1, 52)
SET IDENTITY_INSERT [dbo].[RoomNumbers] OFF
ALTER TABLE [dbo].[RoomNumbers]  WITH CHECK ADD  CONSTRAINT [FK_RoomNumbers_BuildingName] FOREIGN KEY([BuildingID])
REFERENCES [dbo].[BuildingName] ([ID])
GO
ALTER TABLE [dbo].[RoomNumbers] CHECK CONSTRAINT [FK_RoomNumbers_BuildingName]
GO
