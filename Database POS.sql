USE [master]
GO
/****** Object:  Database [SistemPointOfSale(POS)]    Script Date: 03/04/2025 10:01:46 ******/
CREATE DATABASE [SistemPointOfSale(POS)]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SistemPointOfSale(POS)', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\SistemPointOfSale(POS).mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SistemPointOfSale(POS)_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\SistemPointOfSale(POS)_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SistemPointOfSale(POS)].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET ARITHABORT OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET  MULTI_USER 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET QUERY_STORE = ON
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SistemPointOfSale(POS)]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 03/04/2025 10:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[StockQuantity] [int] NOT NULL,
	[Description] [text] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 03/04/2025 10:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewPrroductWithCategory]    Script Date: 03/04/2025 10:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewPrroductWithCategory]
AS
SELECT        dbo.Categories.CategoryId, dbo.Categories.CategoryName, dbo.Products.ProductId, dbo.Products.ProductName, dbo.Products.Price, dbo.Products.Description, dbo.Products.StockQuantity
FROM            dbo.Categories INNER JOIN
                         dbo.Products ON dbo.Categories.CategoryId = dbo.Products.CategoryId
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 03/04/2025 10:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[ContactNumber] [nchar](14) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Address] [text] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 03/04/2025 10:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[SaleId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[SaleDate] [datetime] NOT NULL,
	[TotalAmount] [decimal](15, 2) NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleItems]    Script Date: 03/04/2025 10:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleItems](
	[SaleItemsId] [int] IDENTITY(1,1) NOT NULL,
	[SaleId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_SaleItemId] PRIMARY KEY CLUSTERED 
(
	[SaleItemsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewSalesAndProductWithCustomer]    Script Date: 03/04/2025 10:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewSalesAndProductWithCustomer]
AS
SELECT        dbo.Sales.SaleDate, dbo.Sales.SaleId, dbo.Sales.TotalAmount, dbo.SaleItems.SaleItemsId, dbo.SaleItems.Quantity, dbo.SaleItems.Price, dbo.Products.ProductName, dbo.Products.Description, dbo.Categories.CategoryId, 
                         dbo.Categories.CategoryName, dbo.Customers.CustomerId AS CustomersId, dbo.Customers.CustomerName AS CustomersName, dbo.Customers.ContactNumber AS ContactNumbers, dbo.Customers.Email, 
                         dbo.Customers.Address, dbo.Products.ProductId AS ProductsId
FROM            dbo.Sales INNER JOIN
                         dbo.SaleItems ON dbo.Sales.SaleId = dbo.SaleItems.SaleId INNER JOIN
                         dbo.Products ON dbo.SaleItems.ProductId = dbo.Products.ProductId INNER JOIN
                         dbo.Categories ON dbo.Products.CategoryId = dbo.Categories.CategoryId INNER JOIN
                         dbo.Customers ON dbo.Sales.CustomerId = dbo.Customers.CustomerId
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 03/04/2025 10:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](50) NOT NULL,
	[ContactNumber] [nchar](14) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Position] [nvarchar](35) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[SaleItems]  WITH CHECK ADD  CONSTRAINT [FK_SaleItems_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[SaleItems] CHECK CONSTRAINT [FK_SaleItems_Products]
GO
ALTER TABLE [dbo].[SaleItems]  WITH CHECK ADD  CONSTRAINT [FK_SaleItems_Sales] FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sales] ([SaleId])
GO
ALTER TABLE [dbo].[SaleItems] CHECK CONSTRAINT [FK_SaleItems_Sales]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Customers]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Categories"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Products"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPrroductWithCategory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPrroductWithCategory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Sales"
            Begin Extent = 
               Top = 93
               Left = 279
               Bottom = 223
               Right = 449
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SaleItems"
            Begin Extent = 
               Top = 2
               Left = 476
               Bottom = 132
               Right = 646
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Products"
            Begin Extent = 
               Top = 94
               Left = 676
               Bottom = 224
               Right = 846
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Categories"
            Begin Extent = 
               Top = 6
               Left = 874
               Bottom = 102
               Right = 1044
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Customers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 213
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1665
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewSalesAndProductWithCustomer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewSalesAndProductWithCustomer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewSalesAndProductWithCustomer'
GO
USE [master]
GO
ALTER DATABASE [SistemPointOfSale(POS)] SET  READ_WRITE 
GO
