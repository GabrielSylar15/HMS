USE [master]
GO
/****** Object:  Database [PRN211_HMS]    Script Date: 7/24/2022 10:43:25 PM ******/
CREATE DATABASE [PRN211_HMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PRN211_HMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\PRN211_HMS.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PRN211_HMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\PRN211_HMS_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PRN211_HMS] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PRN211_HMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PRN211_HMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PRN211_HMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PRN211_HMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PRN211_HMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PRN211_HMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [PRN211_HMS] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PRN211_HMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PRN211_HMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PRN211_HMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PRN211_HMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PRN211_HMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PRN211_HMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PRN211_HMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PRN211_HMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PRN211_HMS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PRN211_HMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PRN211_HMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PRN211_HMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PRN211_HMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PRN211_HMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PRN211_HMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PRN211_HMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PRN211_HMS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PRN211_HMS] SET  MULTI_USER 
GO
ALTER DATABASE [PRN211_HMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PRN211_HMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PRN211_HMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PRN211_HMS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PRN211_HMS] SET DELAYED_DURABILITY = DISABLED 
GO
USE [PRN211_HMS]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[DoB] [datetime] NOT NULL,
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nchar](10) NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Facilities]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facilities](
	[FacilityId] [int] IDENTITY(1,1) NOT NULL,
	[FacilityName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Facilities] PRIMARY KEY CLUSTERED 
(
	[FacilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Facility_Room]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facility_Room](
	[FacilityId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
 CONSTRAINT [PK_Facility_Room] PRIMARY KEY CLUSTERED 
(
	[FacilityId] ASC,
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackId] [int] NOT NULL,
	[Note] [nvarchar](500) NOT NULL,
	[Rated] [int] NOT NULL,
	[FeedbackDate] [datetime] NOT NULL,
	[ReservationId] [int] NOT NULL,
	[ImageName] [varchar](50) NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[AccountId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[Price] [float] NOT NULL,
	[EndDate] [date] NOT NULL,
	[ReservationId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reservation_Service]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation_Service](
	[ReservationId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[BookedDate] [datetime] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Reservation_Service] PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC,
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Room]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [nvarchar](50) NOT NULL,
	[SizeId] [int] NOT NULL,
	[RoomDescription] [ntext] NULL,
	[RoomImage] [nvarchar](100) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Service]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[ỊmageName] [varchar](50) NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SizeRoom]    Script Date: 7/24/2022 10:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SizeRoom](
	[SizeId] [int] IDENTITY(1,1) NOT NULL,
	[Size] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_SizeRoom] PRIMARY KEY CLUSTERED 
(
	[SizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([UserName], [Password], [Address], [DoB], [AccountId], [Phone], [DisplayName], [RoleId]) VALUES (N'vinhnthe151215@fpt.edu.vn', N'123', N'HaiPhong', CAST(N'2001-05-15 00:00:00.000' AS DateTime), 5, N'0989902069', N'Nguyễn Thế Vinh', 1)
INSERT [dbo].[Account] ([UserName], [Password], [Address], [DoB], [AccountId], [Phone], [DisplayName], [RoleId]) VALUES (N'thanhnthe153122@fpt.edu.vn', N'123', N'NgheAn', CAST(N'2001-01-02 00:00:00.000' AS DateTime), 11, N'1234456   ', N'Nguyễn Tuấn Thành', 2)
INSERT [dbo].[Account] ([UserName], [Password], [Address], [DoB], [AccountId], [Phone], [DisplayName], [RoleId]) VALUES (N'thuanndhe151215@fpt.edu.vn', N'123', N'HoaBinh', CAST(N'2003-01-02 00:00:00.000' AS DateTime), 12, N'123456    ', N'Trần Đức Thuận', 1)
INSERT [dbo].[Account] ([UserName], [Password], [Address], [DoB], [AccountId], [Phone], [DisplayName], [RoleId]) VALUES (N'thainmhe151215@fpt.edu.vn', N'123', N'HaiPhong', CAST(N'2003-01-02 00:00:00.000' AS DateTime), 14, N'123456    ', N'Nguyễn Minh Thái', 2)
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[Facilities] ON 

INSERT [dbo].[Facilities] ([FacilityId], [FacilityName]) VALUES (1, N'HighWifi')
INSERT [dbo].[Facilities] ([FacilityId], [FacilityName]) VALUES (2, N'Love chair')
INSERT [dbo].[Facilities] ([FacilityId], [FacilityName]) VALUES (3, N'Smart TV')
INSERT [dbo].[Facilities] ([FacilityId], [FacilityName]) VALUES (4, N'AC')
INSERT [dbo].[Facilities] ([FacilityId], [FacilityName]) VALUES (5, N'Parking')
INSERT [dbo].[Facilities] ([FacilityId], [FacilityName]) VALUES (6, N'Pool')
SET IDENTITY_INSERT [dbo].[Facilities] OFF
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (1, 1)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (1, 2)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (1, 3)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (1, 6)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (2, 1)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (2, 2)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (2, 6)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (3, 1)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (3, 2)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (3, 6)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (4, 1)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (4, 3)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (5, 1)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (5, 3)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (5, 8)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (6, 2)
INSERT [dbo].[Facility_Room] ([FacilityId], [RoomId]) VALUES (6, 8)
INSERT [dbo].[Feedback] ([FeedbackId], [Note], [Rated], [FeedbackDate], [ReservationId], [ImageName]) VALUES (1, N'Good Room', 5, CAST(N'2022-07-25 00:00:00.000' AS DateTime), 5, NULL)
SET IDENTITY_INSERT [dbo].[Reservation] ON 

INSERT [dbo].[Reservation] ([AccountId], [RoomId], [StartDate], [Price], [EndDate], [ReservationId]) VALUES (11, 1, CAST(N'2022-07-23' AS Date), 300, CAST(N'2022-07-24' AS Date), 5)
INSERT [dbo].[Reservation] ([AccountId], [RoomId], [StartDate], [Price], [EndDate], [ReservationId]) VALUES (11, 2, CAST(N'2022-07-24' AS Date), 150, CAST(N'2022-07-24' AS Date), 12)
INSERT [dbo].[Reservation] ([AccountId], [RoomId], [StartDate], [Price], [EndDate], [ReservationId]) VALUES (12, 3, CAST(N'2022-08-19' AS Date), 1200, CAST(N'2022-08-23' AS Date), 13)
SET IDENTITY_INSERT [dbo].[Reservation] OFF
INSERT [dbo].[Reservation_Service] ([ReservationId], [ServiceId], [Price], [BookedDate], [Quantity]) VALUES (5, 1, 1, CAST(N'2022-07-23 00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Reservation_Service] ([ReservationId], [ServiceId], [Price], [BookedDate], [Quantity]) VALUES (5, 2, 2.5, CAST(N'2022-07-23 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Reservation_Service] ([ReservationId], [ServiceId], [Price], [BookedDate], [Quantity]) VALUES (12, 3, 300, CAST(N'2022-08-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Reservation_Service] ([ReservationId], [ServiceId], [Price], [BookedDate], [Quantity]) VALUES (13, 4, 1.5, CAST(N'2022-08-19 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Customer')
SET IDENTITY_INSERT [dbo].[Room] ON 

INSERT [dbo].[Room] ([RoomId], [RoomName], [SizeId], [RoomDescription], [RoomImage]) VALUES (1, N'101', 1, N'Cảm nhận sự thư giãn và thoải mái tối đa với các phòng Superior Twin với phong cách trang trí đương đại độc đáo của Việt Nam. Các phòng đều lớn và được trang bị đầy đủ với 2 giường đơn thoải mái và các tiện nghi trong phòng phong phú là sự lựa chọn hoàn hảo cho việc nghỉ ngơi, thư giãn.', NULL)
INSERT [dbo].[Room] ([RoomId], [RoomName], [SizeId], [RoomDescription], [RoomImage]) VALUES (2, N'102', 2, N'Phòng Deluxe sẽ giúp quý khách quên đi sự mệt mỏi của cuộc sống vội vã ngoài kia.', NULL)
INSERT [dbo].[Room] ([RoomId], [RoomName], [SizeId], [RoomDescription], [RoomImage]) VALUES (3, N'103', 3, N'Với phòng Deluxe King quý khách sẽ được thưởng thức trọn vẹn vẻ đẹp ấn tượng của thành phố ngay trong phòng ngủ của mình. Quý khách sẽ tận hưởng được cảm giác sang trọng, ấm cúng trong lối thiết kế phòng độc đáo với 1 giường King và các thiết bị nội thất hiện đại.', NULL)
INSERT [dbo].[Room] ([RoomId], [RoomName], [SizeId], [RoomDescription], [RoomImage]) VALUES (6, N'104', 3, N'Phòng căn hộ sang trọng và rộng rãi có 2 tầng trong đó có 3 phòng ngủ giường to.', NULL)
INSERT [dbo].[Room] ([RoomId], [RoomName], [SizeId], [RoomDescription], [RoomImage]) VALUES (8, N'201', 1, N'Phòng Premium Deluxe Triple được thiết kế với diện tích 37 m2 mang đến cho bạn tận hưởng sự thoải mái và rộng rãi. Quý khách sẽ thấy hài lòng với trang thiết bị hiện đại: 3 giường đơn và các thiết bị nội thất hiện đại cùng với sự chu đáo tỉ mỉ và tận tình của nhân viên phục vụ.', NULL)
SET IDENTITY_INSERT [dbo].[Room] OFF
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([ServiceId], [ServiceName], [Price], [ỊmageName]) VALUES (1, N'Cocacola', 1, NULL)
INSERT [dbo].[Service] ([ServiceId], [ServiceName], [Price], [ỊmageName]) VALUES (2, N'Pizza', 2.5, NULL)
INSERT [dbo].[Service] ([ServiceId], [ServiceName], [Price], [ỊmageName]) VALUES (3, N'Massage', 30, NULL)
INSERT [dbo].[Service] ([ServiceId], [ServiceName], [Price], [ỊmageName]) VALUES (4, N'Cake', 1.5, NULL)
SET IDENTITY_INSERT [dbo].[Service] OFF
SET IDENTITY_INSERT [dbo].[SizeRoom] ON 

INSERT [dbo].[SizeRoom] ([SizeId], [Size], [Price]) VALUES (1, 1, 100)
INSERT [dbo].[SizeRoom] ([SizeId], [Size], [Price]) VALUES (2, 2, 150)
INSERT [dbo].[SizeRoom] ([SizeId], [Size], [Price]) VALUES (3, 3, 200)
INSERT [dbo].[SizeRoom] ([SizeId], [Size], [Price]) VALUES (5, 4, 300)
SET IDENTITY_INSERT [dbo].[SizeRoom] OFF
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[Facility_Room]  WITH CHECK ADD  CONSTRAINT [FK_Facility_Room_Facilities] FOREIGN KEY([FacilityId])
REFERENCES [dbo].[Facilities] ([FacilityId])
GO
ALTER TABLE [dbo].[Facility_Room] CHECK CONSTRAINT [FK_Facility_Room_Facilities]
GO
ALTER TABLE [dbo].[Facility_Room]  WITH CHECK ADD  CONSTRAINT [FK_Facility_Room_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Facility_Room] CHECK CONSTRAINT [FK_Facility_Room_Room]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Reservation] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[Reservation] ([ReservationId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Reservation]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Account]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Room]
GO
ALTER TABLE [dbo].[Reservation_Service]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Service_Reservation] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[Reservation] ([ReservationId])
GO
ALTER TABLE [dbo].[Reservation_Service] CHECK CONSTRAINT [FK_Reservation_Service_Reservation]
GO
ALTER TABLE [dbo].[Reservation_Service]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Service_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([ServiceId])
GO
ALTER TABLE [dbo].[Reservation_Service] CHECK CONSTRAINT [FK_Reservation_Service_Service]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_SizeRoom] FOREIGN KEY([SizeId])
REFERENCES [dbo].[SizeRoom] ([SizeId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_SizeRoom]
GO
USE [master]
GO
ALTER DATABASE [PRN211_HMS] SET  READ_WRITE 
GO
