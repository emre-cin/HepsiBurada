USE [master]
GO
/****** Object:  Database [HepsiBurada]    Script Date: 25.12.2021 23:50:02 ******/
CREATE DATABASE [HepsiBurada]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HepsiBurada', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\HepsiBurada.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HepsiBurada_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\HepsiBurada_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [HepsiBurada] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HepsiBurada].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HepsiBurada] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HepsiBurada] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HepsiBurada] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HepsiBurada] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HepsiBurada] SET ARITHABORT OFF 
GO
ALTER DATABASE [HepsiBurada] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HepsiBurada] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HepsiBurada] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HepsiBurada] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HepsiBurada] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HepsiBurada] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HepsiBurada] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HepsiBurada] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HepsiBurada] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HepsiBurada] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HepsiBurada] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HepsiBurada] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HepsiBurada] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HepsiBurada] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HepsiBurada] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HepsiBurada] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HepsiBurada] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HepsiBurada] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HepsiBurada] SET  MULTI_USER 
GO
ALTER DATABASE [HepsiBurada] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HepsiBurada] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HepsiBurada] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HepsiBurada] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HepsiBurada] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HepsiBurada] SET QUERY_STORE = OFF
GO
USE [HepsiBurada]
GO
/****** Object:  Table [dbo].[Campaign]    Script Date: 25.12.2021 23:50:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campaign](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignName] [varchar](max) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [decimal](18, 4) NOT NULL,
	[DiscountPercent] [decimal](18, 4) NOT NULL,
	[TargetSalesCount] [int] NOT NULL,
	[Limit] [decimal](18, 4) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 25.12.2021 23:50:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CampaignId] [int] NULL,
	[Quantity] [int] NOT NULL,
	[Discount] [decimal](18, 4) NOT NULL,
	[UnitPrice] [decimal](18, 4) NOT NULL,
	[TotalPrice] [decimal](18, 4) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 25.12.2021 23:50:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [varchar](max) NOT NULL,
	[UnitPrice] [decimal](18, 4) NOT NULL,
	[UnitsInStock] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Campaign]  WITH CHECK ADD  CONSTRAINT [FK_Campaign_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Campaign] CHECK CONSTRAINT [FK_Campaign_Product]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Campaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaign] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Campaign]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Product]
GO
USE [master]
GO
ALTER DATABASE [HepsiBurada] SET  READ_WRITE 
GO
