USE [master]
GO
/****** Object:  Database [Timesheet]    Script Date: 2020.01.30. 20:58:02 ******/
CREATE DATABASE [Timesheet]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Timesheet', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Timesheet.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Timesheet_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Timesheet_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Timesheet] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Timesheet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Timesheet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Timesheet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Timesheet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Timesheet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Timesheet] SET ARITHABORT OFF 
GO
ALTER DATABASE [Timesheet] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Timesheet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Timesheet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Timesheet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Timesheet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Timesheet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Timesheet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Timesheet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Timesheet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Timesheet] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Timesheet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Timesheet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Timesheet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Timesheet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Timesheet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Timesheet] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Timesheet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Timesheet] SET RECOVERY FULL 
GO
ALTER DATABASE [Timesheet] SET  MULTI_USER 
GO
ALTER DATABASE [Timesheet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Timesheet] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Timesheet] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Timesheet] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Timesheet] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Timesheet', N'ON'
GO
ALTER DATABASE [Timesheet] SET QUERY_STORE = OFF
GO
USE [Timesheet]
GO
/****** Object:  Table [dbo].[Activities]    Script Date: 2020.01.30. 20:58:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserActivities]    Script Date: 2020.01.30. 20:58:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActivities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[Comment] [ntext] NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserActivities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2020.01.30. 20:58:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nchar](120) NOT NULL,
	[FullName] [nvarchar](120) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Activities] ON 

INSERT [dbo].[Activities] ([Id], [Title], [Description]) VALUES (1, N'Meeting', N'Egy kis összeröffenés. Kóla és pogácsa lesz.')
INSERT [dbo].[Activities] ([Id], [Title], [Description]) VALUES (2, N'QS Implementation ', N'Hard work with my computer.')
SET IDENTITY_INSERT [dbo].[Activities] OFF
SET IDENTITY_INSERT [dbo].[UserActivities] ON 

INSERT [dbo].[UserActivities] ([Id], [UserId], [ActivityId], [Duration], [Comment], [Date]) VALUES (1, 1, 2, 60, NULL, CAST(N'2020-01-30T20:51:49.8333333' AS DateTime2))
INSERT [dbo].[UserActivities] ([Id], [UserId], [ActivityId], [Duration], [Comment], [Date]) VALUES (2, 2, 1, 44040, N'Too much meeting', CAST(N'2020-01-30T20:53:17.7441815' AS DateTime2))
SET IDENTITY_INSERT [dbo].[UserActivities] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [FullName]) VALUES (1, N'Admin', N'pw                                                                                                                      ', N'Admin János')
INSERT [dbo].[Users] ([Id], [Username], [Password], [FullName]) VALUES (2, N'peters', N'sdasd                                                                                                                   ', N'Peter Smith')
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Index [IX_ActivityId]    Script Date: 2020.01.30. 20:58:03 ******/
CREATE NONCLUSTERED INDEX [IX_ActivityId] ON [dbo].[UserActivities]
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserActivities] ADD  CONSTRAINT [DF_UserActivities_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[UserActivities]  WITH CHECK ADD  CONSTRAINT [FK_UserActivities_Activities] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activities] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserActivities] CHECK CONSTRAINT [FK_UserActivities_Activities]
GO
ALTER TABLE [dbo].[UserActivities]  WITH CHECK ADD  CONSTRAINT [FK_UserActivities_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserActivities] CHECK CONSTRAINT [FK_UserActivities_Users]
GO
USE [master]
GO
ALTER DATABASE [Timesheet] SET  READ_WRITE 
GO
