USE [master]
GO
/****** Object:  Database [ApartmentsDB]    Script Date: 15.01.2022 20:19:55 ******/
CREATE DATABASE [ApartmentsDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ApartmentsDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ApartmentsDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ApartmentsDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ApartmentsDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ApartmentsDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ApartmentsDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ApartmentsDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ApartmentsDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ApartmentsDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ApartmentsDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ApartmentsDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ApartmentsDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ApartmentsDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ApartmentsDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ApartmentsDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ApartmentsDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ApartmentsDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ApartmentsDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ApartmentsDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ApartmentsDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ApartmentsDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ApartmentsDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ApartmentsDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ApartmentsDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ApartmentsDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ApartmentsDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ApartmentsDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ApartmentsDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ApartmentsDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ApartmentsDB] SET  MULTI_USER 
GO
ALTER DATABASE [ApartmentsDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ApartmentsDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ApartmentsDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ApartmentsDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ApartmentsDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ApartmentsDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ApartmentsDB', N'ON'
GO
ALTER DATABASE [ApartmentsDB] SET QUERY_STORE = OFF
GO
USE [ApartmentsDB]
GO
/****** Object:  Schema [HangFire]    Script Date: 15.01.2022 20:19:55 ******/
CREATE SCHEMA [HangFire]
GO
/****** Object:  Table [dbo].[Bills]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bills](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[homeId] [int] NOT NULL,
 CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ElectricBill]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElectricBill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[billsId] [int] NOT NULL,
	[isPaid] [bit] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[billDate] [datetime] NOT NULL,
	[paymentDate] [datetime] NULL,
 CONSTRAINT [PK_electricBill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GasBill]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GasBill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[billsId] [int] NOT NULL,
	[isPaid] [bit] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[billDate] [datetime] NOT NULL,
	[paymentDate] [datetime] NULL,
 CONSTRAINT [PK_gasBill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeBill]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeBill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[billsId] [int] NOT NULL,
	[isPaid] [bit] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[billDate] [datetime] NOT NULL,
	[paymentDate] [datetime] NULL,
 CONSTRAINT [PK_homeBill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Homes]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Homes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ownerId] [int] NULL,
	[isOwned] [bit] NOT NULL,
	[isActive] [bit] NOT NULL,
	[blockName] [varchar](50) NOT NULL,
	[homeType] [varchar](20) NOT NULL,
	[floorNumber] [smallint] NOT NULL,
	[doorNumber] [smallint] NOT NULL,
	[insertDate] [datetime] NOT NULL,
	[updateDate] [datetime] NULL,
	[duesPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Homes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[senderId] [int] NOT NULL,
	[receiverId] [int] NOT NULL,
	[isSenderReaded] [bit] NOT NULL,
	[isReceiverReaded] [bit] NOT NULL,
	[messageTitle] [varchar](50) NOT NULL,
	[senderMessage] [varchar](1000) NOT NULL,
	[receiverMessage] [varchar](1000) NULL,
	[insertDate] [datetime] NOT NULL,
	[updateDate] [datetime] NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tcNo] [varchar](11) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[surName] [varchar](50) NOT NULL,
	[displayName] [varchar](125) NOT NULL,
	[email] [varchar](75) NOT NULL,
	[phoneNumber] [varchar](50) NOT NULL,
	[password] [varchar](450) NOT NULL,
	[carPlate] [varchar](50) NULL,
	[insertDate] [datetime] NOT NULL,
	[updateDate] [datetime] NULL,
	[isDeleted] [bit] NOT NULL,
	[isAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WaterBill]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterBill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[billsId] [int] NOT NULL,
	[isPaid] [bit] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[billDate] [datetime] NOT NULL,
	[paymentDate] [datetime] NULL,
 CONSTRAINT [PK_waterBill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[AggregatedCounter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Counter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
	[ExpireAt] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CX_HangFire_Counter]    Script Date: 15.01.2022 20:19:55 ******/
CREATE CLUSTERED INDEX [CX_HangFire_Counter] ON [HangFire].[Counter]
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Hash](
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Job]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Job](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StateId] [bigint] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobParameter](
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobQueue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Queue] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[List]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[List](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Server]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](200) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Set]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Set](
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[State]    Script Date: 15.01.2022 20:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[State](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bills] ON 

INSERT [dbo].[Bills] ([id], [homeId]) VALUES (17, 2)
INSERT [dbo].[Bills] ([id], [homeId]) VALUES (19, 2)
INSERT [dbo].[Bills] ([id], [homeId]) VALUES (20, 23)
INSERT [dbo].[Bills] ([id], [homeId]) VALUES (21, 24)
INSERT [dbo].[Bills] ([id], [homeId]) VALUES (22, 26)
INSERT [dbo].[Bills] ([id], [homeId]) VALUES (23, 2)
SET IDENTITY_INSERT [dbo].[Bills] OFF
GO
SET IDENTITY_INSERT [dbo].[ElectricBill] ON 

INSERT [dbo].[ElectricBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (7, 17, 1, CAST(170.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:29:42.000' AS DateTime), CAST(N'2022-01-15T20:00:07.933' AS DateTime))
INSERT [dbo].[ElectricBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (9, 19, 0, CAST(100.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:03.000' AS DateTime), NULL)
INSERT [dbo].[ElectricBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (10, 20, 0, CAST(100.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:03.000' AS DateTime), NULL)
INSERT [dbo].[ElectricBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (11, 21, 0, CAST(100.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:03.000' AS DateTime), NULL)
INSERT [dbo].[ElectricBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (12, 22, 0, CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-01-31T13:38:03.000' AS DateTime), CAST(N'2022-01-31T13:38:03.000' AS DateTime))
INSERT [dbo].[ElectricBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (13, 23, 0, CAST(150.00 AS Decimal(18, 2)), CAST(N'2022-01-15T19:31:27.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ElectricBill] OFF
GO
SET IDENTITY_INSERT [dbo].[GasBill] ON 

INSERT [dbo].[GasBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (5, 17, 1, CAST(185.60 AS Decimal(18, 2)), CAST(N'2022-01-14T16:29:56.000' AS DateTime), CAST(N'2022-01-15T20:00:18.870' AS DateTime))
INSERT [dbo].[GasBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (7, 19, 0, CAST(350.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:17.000' AS DateTime), NULL)
INSERT [dbo].[GasBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (8, 20, 0, CAST(300.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:17.000' AS DateTime), NULL)
INSERT [dbo].[GasBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (9, 21, 0, CAST(300.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:17.000' AS DateTime), NULL)
INSERT [dbo].[GasBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (10, 22, 0, CAST(300.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:17.000' AS DateTime), NULL)
INSERT [dbo].[GasBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (11, 23, 0, CAST(260.00 AS Decimal(18, 2)), CAST(N'2022-01-18T19:31:32.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[GasBill] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeBill] ON 

INSERT [dbo].[HomeBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (5, 17, 1, CAST(350.00 AS Decimal(18, 2)), CAST(N'2022-01-14T19:30:04.740' AS DateTime), CAST(N'2022-01-15T18:23:56.017' AS DateTime))
INSERT [dbo].[HomeBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (6, 19, 1, CAST(350.00 AS Decimal(18, 2)), CAST(N'2022-01-14T19:38:22.420' AS DateTime), CAST(N'2022-01-15T20:00:00.473' AS DateTime))
INSERT [dbo].[HomeBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (7, 20, 0, CAST(470.00 AS Decimal(18, 2)), CAST(N'2022-01-14T19:38:22.560' AS DateTime), NULL)
INSERT [dbo].[HomeBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (8, 21, 0, CAST(380.00 AS Decimal(18, 2)), CAST(N'2022-01-14T19:38:22.570' AS DateTime), NULL)
INSERT [dbo].[HomeBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (9, 22, 0, CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-01-14T19:38:22.577' AS DateTime), CAST(N'2022-01-14T19:38:22.577' AS DateTime))
INSERT [dbo].[HomeBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (10, 23, 0, CAST(350.00 AS Decimal(18, 2)), CAST(N'2022-01-14T22:31:34.673' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[HomeBill] OFF
GO
SET IDENTITY_INSERT [dbo].[Homes] ON 

INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (2, 6, 1, 1, N'A Blok', N'3+1', 1, 102, CAST(N'2022-01-07T15:22:27.153' AS DateTime), CAST(N'2022-01-12T16:06:48.313' AS DateTime), CAST(350.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (3, NULL, 0, 1, N'B Blok', N'5+2', 1, 175, CAST(N'2022-01-07T15:23:45.390' AS DateTime), CAST(N'2022-01-14T17:33:24.993' AS DateTime), CAST(250.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (8, NULL, 0, 1, N'C Blok', N'4+1', 12, 1206, CAST(N'2022-01-07T20:13:25.280' AS DateTime), CAST(N'2022-01-12T15:49:21.177' AS DateTime), CAST(290.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (10, NULL, 0, 1, N'A Blok', N'1+0 Stüdyo', 1, 12, CAST(N'2022-01-07T20:17:19.753' AS DateTime), CAST(N'2022-01-09T17:38:21.823' AS DateTime), CAST(490.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (11, NULL, 0, 1, N'F Blok', N'2+1', 4, 401, CAST(N'2022-01-07T22:56:27.933' AS DateTime), CAST(N'2022-01-10T03:22:00.940' AS DateTime), CAST(550.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (12, NULL, 0, 1, N'F Blok', N'2+1', 4, 401, CAST(N'2022-01-07T22:56:38.327' AS DateTime), NULL, CAST(650.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (13, NULL, 0, 1, N'Bee', N'2+2', 10, 1001, CAST(N'2022-01-07T23:02:09.703' AS DateTime), NULL, CAST(400.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (14, NULL, 0, 1, N'Ander', N'1+3', 2, 204, CAST(N'2022-01-07T23:04:19.857' AS DateTime), NULL, CAST(560.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (15, NULL, 0, 1, N'Butter', N'1+0 Stüdyo', 4, 420, CAST(N'2022-01-07T23:06:27.350' AS DateTime), NULL, CAST(105.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (16, NULL, 0, 1, N'G Blok', N'7+2 Triplex', 28, 2800, CAST(N'2022-01-07T23:14:41.703' AS DateTime), CAST(N'2022-01-08T23:33:15.140' AS DateTime), CAST(540.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (17, NULL, 0, 1, N'F Blok', N'4+1', 21, 2100, CAST(N'2022-01-07T23:16:42.857' AS DateTime), NULL, CAST(295.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (18, NULL, 0, 1, N'F Blok', N'1+0 Stüdyo', 1, 111, CAST(N'2022-01-08T02:34:17.897' AS DateTime), NULL, CAST(400.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (19, NULL, 0, 1, N'Franklin', N'3+1', 18, 1800, CAST(N'2022-01-08T02:36:34.877' AS DateTime), NULL, CAST(100.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (20, NULL, 0, 1, N'Charlie', N'5+2', 9, 978, CAST(N'2022-01-08T19:25:55.713' AS DateTime), NULL, CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (21, NULL, 0, 1, N'Butter', N'3+1', 4, 414, CAST(N'2022-01-10T03:17:42.513' AS DateTime), NULL, CAST(259.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (23, NULL, 0, 1, N'A Blok', N'3+1', 12, 1202, CAST(N'2022-01-11T23:42:16.537' AS DateTime), CAST(N'2022-01-14T19:39:37.587' AS DateTime), CAST(470.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (24, NULL, 1, 1, N'C Blok', N'3+1', 1, 199, CAST(N'2022-01-12T00:22:10.553' AS DateTime), NULL, CAST(380.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (25, NULL, 0, 1, N'Charlie', N'3+1', 2, 208, CAST(N'2022-01-12T00:51:34.200' AS DateTime), CAST(N'2022-01-12T15:49:36.910' AS DateTime), CAST(620.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (26, 7, 1, 1, N'Franklin', N'7+2 Triplex', 17, 1799, CAST(N'2022-01-13T17:20:03.920' AS DateTime), NULL, CAST(240.00 AS Decimal(18, 2)))
INSERT [dbo].[Homes] ([id], [ownerId], [isOwned], [isActive], [blockName], [homeType], [floorNumber], [doorNumber], [insertDate], [updateDate], [duesPrice]) VALUES (27, NULL, 0, 1, N'A Blok', N'2+1', 3, 350, CAST(N'2022-01-14T17:30:07.160' AS DateTime), NULL, CAST(250.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Homes] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([id], [senderId], [receiverId], [isSenderReaded], [isReceiverReaded], [messageTitle], [senderMessage], [receiverMessage], [insertDate], [updateDate]) VALUES (1, 2, 6, 1, 1, N'Başlık', N'İçerik', N'bu niye boş mesaj yönetici', CAST(N'2022-01-12T22:42:23.003' AS DateTime), CAST(N'2022-01-13T16:14:12.197' AS DateTime))
INSERT [dbo].[Messages] ([id], [senderId], [receiverId], [isSenderReaded], [isReceiverReaded], [messageTitle], [senderMessage], [receiverMessage], [insertDate], [updateDate]) VALUES (2, 2, 7, 1, 1, N'2. başlık', N'Ronaldoya sevgilerle...', N'Thanks Broo SIUUUUUU', CAST(N'2022-01-12T22:42:41.730' AS DateTime), CAST(N'2022-01-13T16:17:16.703' AS DateTime))
INSERT [dbo].[Messages] ([id], [senderId], [receiverId], [isSenderReaded], [isReceiverReaded], [messageTitle], [senderMessage], [receiverMessage], [insertDate], [updateDate]) VALUES (3, 6, 2, 1, 1, N'Faturalar niye bu kadar pahalı', N'Lan eve para yetmiyor yönetici!!! Aya 3 lirayla başlıyorum faturalardan sonra elimde para kalmıyor!!!!. Yeter artık bu fiyatlar !!!', N'Az ye', CAST(N'2022-01-12T22:43:16.827' AS DateTime), CAST(N'2022-01-13T16:02:18.223' AS DateTime))
INSERT [dbo].[Messages] ([id], [senderId], [receiverId], [isSenderReaded], [isReceiverReaded], [messageTitle], [senderMessage], [receiverMessage], [insertDate], [updateDate]) VALUES (4, 7, 2, 1, 1, N'Hi From Portugal', N'Hi Manager. Thanks for your offer. But i cant come to your team.I am the king and i thinking my career. Have a good day :)', N'Thanks for your response :))', CAST(N'2022-01-12T22:45:53.950' AS DateTime), CAST(N'2022-01-13T16:13:39.523' AS DateTime))
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [tcNo], [name], [surName], [displayName], [email], [phoneNumber], [password], [carPlate], [insertDate], [updateDate], [isDeleted], [isAdmin]) VALUES (2, N'11111111111', N'Talha', N'Ekrem', N'Talha Ekrem', N'talha.ekrem.99@gmail.com', N'05314378788', N'$2a$11$v6iNgkTdqmSFtszIYc19euB8IjUtBy.e6tsIW.b5EtpvpMDyEYduS', NULL, CAST(N'2022-01-10T22:29:30.443' AS DateTime), CAST(N'2022-01-15T20:05:25.453' AS DateTime), 0, 1)
INSERT [dbo].[Users] ([id], [tcNo], [name], [surName], [displayName], [email], [phoneNumber], [password], [carPlate], [insertDate], [updateDate], [isDeleted], [isAdmin]) VALUES (6, N'12345612345', N'Batuhan', N'Ekrem', N'Batuhan Ekrem', N'batuhanekrem9@gmail.com', N'05432567677', N'$2a$11$oKGE4BX.iJ0lBIjD7da99eX/Rjwj6erHDV/r3alGBdCzjgZ6Tk.IC', NULL, CAST(N'2022-01-11T19:55:51.083' AS DateTime), CAST(N'2022-01-15T20:05:02.370' AS DateTime), 0, 0)
INSERT [dbo].[Users] ([id], [tcNo], [name], [surName], [displayName], [email], [phoneNumber], [password], [carPlate], [insertDate], [updateDate], [isDeleted], [isAdmin]) VALUES (7, N'12345678900', N'Cristiano', N'Ronaldo', N'Cristiano Ronaldo', N'talhaekrem54@gmail.com', N'05432101234', N'$2a$11$ZjmfPdrHXocnXTE1P.siBulG2O3pVIazXqrejMBMh5J81odCvuFri', NULL, CAST(N'2022-01-12T16:55:03.710' AS DateTime), NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[WaterBill] ON 

INSERT [dbo].[WaterBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (7, 17, 1, CAST(260.00 AS Decimal(18, 2)), CAST(N'2022-01-19T16:29:46.000' AS DateTime), CAST(N'2022-01-15T20:00:14.867' AS DateTime))
INSERT [dbo].[WaterBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (9, 19, 0, CAST(200.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:12.000' AS DateTime), NULL)
INSERT [dbo].[WaterBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (10, 20, 0, CAST(200.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:12.000' AS DateTime), NULL)
INSERT [dbo].[WaterBill] ([id], [billsId], [isPaid], [price], [billDate], [paymentDate]) VALUES (11, 21, 0, CAST(200.00 AS Decimal(18, 2)), CAST(N'2022-01-15T16:38:12.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[WaterBill] OFF
GO
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded', 1, NULL)
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2022-01-12', 1, CAST(N'2022-02-12T13:55:06.417' AS DateTime))
GO
INSERT [HangFire].[Schema] ([Version]) VALUES (7)
GO
INSERT [HangFire].[Server] ([Id], [Data], [LastHeartbeat]) VALUES (N'talhaekrem:2012:044155b2-ad76-48d8-a6b7-01eff469f7bf', N'{"WorkerCount":20,"Queues":["default"],"StartedAt":"2022-01-15T16:55:55.6005928Z"}', CAST(N'2022-01-15T17:06:27.267' AS DateTime))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Email]    Script Date: 15.01.2022 20:19:55 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UK_Email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_tcNo]    Script Date: 15.01.2022 20:19:55 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UK_tcNo] UNIQUE NONCLUSTERED 
(
	[tcNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_AggregatedCounter_ExpireAt]    Script Date: 15.01.2022 20:19:55 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_AggregatedCounter_ExpireAt] ON [HangFire].[AggregatedCounter]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Hash_ExpireAt]    Script Date: 15.01.2022 20:19:55 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Hash_ExpireAt] ON [HangFire].[Hash]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Job_ExpireAt]    Script Date: 15.01.2022 20:19:55 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_ExpireAt] ON [HangFire].[Job]
(
	[ExpireAt] ASC
)
INCLUDE([StateName]) 
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Job_StateName]    Script Date: 15.01.2022 20:19:55 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_StateName] ON [HangFire].[Job]
(
	[StateName] ASC
)
WHERE ([StateName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_List_ExpireAt]    Script Date: 15.01.2022 20:19:55 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_List_ExpireAt] ON [HangFire].[List]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Server_LastHeartbeat]    Script Date: 15.01.2022 20:19:55 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Server_LastHeartbeat] ON [HangFire].[Server]
(
	[LastHeartbeat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Set_ExpireAt]    Script Date: 15.01.2022 20:19:55 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_ExpireAt] ON [HangFire].[Set]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Set_Score]    Script Date: 15.01.2022 20:19:55 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_Score] ON [HangFire].[Set]
(
	[Key] ASC,
	[Score] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ElectricBill] ADD  CONSTRAINT [DF_electricBill_isPaid]  DEFAULT ((0)) FOR [isPaid]
GO
ALTER TABLE [dbo].[ElectricBill] ADD  CONSTRAINT [DF_electricBill_billDate]  DEFAULT (getdate()) FOR [billDate]
GO
ALTER TABLE [dbo].[GasBill] ADD  CONSTRAINT [DF_gasBill_isPaid]  DEFAULT ((0)) FOR [isPaid]
GO
ALTER TABLE [dbo].[GasBill] ADD  CONSTRAINT [DF_gasBill_billDate]  DEFAULT (getdate()) FOR [billDate]
GO
ALTER TABLE [dbo].[HomeBill] ADD  CONSTRAINT [DF_homeBill_billsId]  DEFAULT ((0)) FOR [billsId]
GO
ALTER TABLE [dbo].[HomeBill] ADD  CONSTRAINT [DF_homeBill_isPaid]  DEFAULT ((0)) FOR [isPaid]
GO
ALTER TABLE [dbo].[HomeBill] ADD  CONSTRAINT [DF_homeBill_billDate]  DEFAULT (getdate()) FOR [billDate]
GO
ALTER TABLE [dbo].[Homes] ADD  CONSTRAINT [DF_Homes_isOwned]  DEFAULT ((0)) FOR [isOwned]
GO
ALTER TABLE [dbo].[Homes] ADD  CONSTRAINT [DF_Homes_isActive]  DEFAULT ((0)) FOR [isActive]
GO
ALTER TABLE [dbo].[Homes] ADD  CONSTRAINT [DF_Homes_insertDate]  DEFAULT (getdate()) FOR [insertDate]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Table_1_isReaded]  DEFAULT ((0)) FOR [isSenderReaded]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_isReceiverReaded]  DEFAULT ((0)) FOR [isReceiverReaded]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_insertDate]  DEFAULT (getdate()) FOR [insertDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_insertDate]  DEFAULT (getdate()) FOR [insertDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_isAdmin]  DEFAULT ((0)) FOR [isAdmin]
GO
ALTER TABLE [dbo].[WaterBill] ADD  CONSTRAINT [DF_waterBill_isPaid]  DEFAULT ((0)) FOR [isPaid]
GO
ALTER TABLE [dbo].[WaterBill] ADD  CONSTRAINT [DF_waterBill_billDate]  DEFAULT (getdate()) FOR [billDate]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_Bills_Homes] FOREIGN KEY([homeId])
REFERENCES [dbo].[Homes] ([id])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_Bills_Homes]
GO
ALTER TABLE [dbo].[ElectricBill]  WITH CHECK ADD  CONSTRAINT [FK_ElectricBill_Bills] FOREIGN KEY([billsId])
REFERENCES [dbo].[Bills] ([id])
GO
ALTER TABLE [dbo].[ElectricBill] CHECK CONSTRAINT [FK_ElectricBill_Bills]
GO
ALTER TABLE [dbo].[GasBill]  WITH CHECK ADD  CONSTRAINT [FK_GasBill_Bills] FOREIGN KEY([billsId])
REFERENCES [dbo].[Bills] ([id])
GO
ALTER TABLE [dbo].[GasBill] CHECK CONSTRAINT [FK_GasBill_Bills]
GO
ALTER TABLE [dbo].[HomeBill]  WITH CHECK ADD  CONSTRAINT [FK_HomeBill_Bills] FOREIGN KEY([billsId])
REFERENCES [dbo].[Bills] ([id])
GO
ALTER TABLE [dbo].[HomeBill] CHECK CONSTRAINT [FK_HomeBill_Bills]
GO
ALTER TABLE [dbo].[Homes]  WITH CHECK ADD  CONSTRAINT [FK_Homes_Users] FOREIGN KEY([ownerId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Homes] CHECK CONSTRAINT [FK_Homes_Users]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users] FOREIGN KEY([senderId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users1] FOREIGN KEY([receiverId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users1]
GO
ALTER TABLE [dbo].[WaterBill]  WITH CHECK ADD  CONSTRAINT [FK_WaterBill_Bills] FOREIGN KEY([billsId])
REFERENCES [dbo].[Bills] ([id])
GO
ALTER TABLE [dbo].[WaterBill] CHECK CONSTRAINT [FK_WaterBill_Bills]
GO
ALTER TABLE [HangFire].[JobParameter]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[JobParameter] CHECK CONSTRAINT [FK_HangFire_JobParameter_Job]
GO
ALTER TABLE [HangFire].[State]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_State_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[State] CHECK CONSTRAINT [FK_HangFire_State_Job]
GO
USE [master]
GO
ALTER DATABASE [ApartmentsDB] SET  READ_WRITE 
GO
