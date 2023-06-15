USE [master]
GO
/****** Object:  Database [Privat_Hospital]    Script Date: 15.06.2023 12:47:49 ******/
CREATE DATABASE [Privat_Hospital]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Privat_Hospital', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Privat_Hospital.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Privat_Hospital_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Privat_Hospital_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Privat_Hospital] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Privat_Hospital].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Privat_Hospital] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Privat_Hospital] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Privat_Hospital] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Privat_Hospital] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Privat_Hospital] SET ARITHABORT OFF 
GO
ALTER DATABASE [Privat_Hospital] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Privat_Hospital] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Privat_Hospital] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Privat_Hospital] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Privat_Hospital] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Privat_Hospital] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Privat_Hospital] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Privat_Hospital] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Privat_Hospital] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Privat_Hospital] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Privat_Hospital] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Privat_Hospital] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Privat_Hospital] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Privat_Hospital] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Privat_Hospital] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Privat_Hospital] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Privat_Hospital] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Privat_Hospital] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Privat_Hospital] SET  MULTI_USER 
GO
ALTER DATABASE [Privat_Hospital] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Privat_Hospital] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Privat_Hospital] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Privat_Hospital] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Privat_Hospital] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Privat_Hospital] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Privat_Hospital] SET QUERY_STORE = ON
GO
ALTER DATABASE [Privat_Hospital] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Privat_Hospital]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 15.06.2023 12:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id_Admin] [int] IDENTITY(1,1) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Middle_Name] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[Id_Admin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 15.06.2023 12:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[Id_Appointmaent] [int] IDENTITY(1,1) NOT NULL,
	[Id_Doctor] [int] NOT NULL,
	[Id_Patient] [int] NOT NULL,
	[DataTime_Appointment] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Appointments_1] PRIMARY KEY CLUSTERED 
(
	[Id_Appointmaent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authorization_history]    Script Date: 15.06.2023 12:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authorization_history](
	[Id_Auth] [bigint] IDENTITY(1,1) NOT NULL,
	[Id_Admin] [int] NOT NULL,
	[Date_Auth] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Authorization_history] PRIMARY KEY CLUSTERED 
(
	[Id_Auth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Conclusions]    Script Date: 15.06.2023 12:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conclusions](
	[Id_Counclusion] [int] IDENTITY(1,1) NOT NULL,
	[Id_Patient] [int] NOT NULL,
	[Id_Doctor] [int] NULL,
	[Complaint] [nvarchar](max) NULL,
	[Life_History] [nvarchar](max) NULL,
	[Objective_Status] [nvarchar](max) NULL,
	[Diagnosis] [nvarchar](max) NULL,
	[Treatment] [nvarchar](max) NULL,
	[DataTime_Conclusion] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Conclusions_1] PRIMARY KEY CLUSTERED 
(
	[Id_Counclusion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 15.06.2023 12:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id_Doctor] [int] IDENTITY(1,1) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Middle_Name] [nvarchar](50) NOT NULL,
	[Specialty] [int] NOT NULL,
	[Shift] [int] NOT NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[Id_Doctor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 15.06.2023 12:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id_Patient] [int] IDENTITY(1,1) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Middle_Name] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](17) NOT NULL,
	[Bithday] [date] NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[Id_Patient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Types_of_services]    Script Date: 15.06.2023 12:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Types_of_services](
	[Id_Type] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Cost] [int] NOT NULL,
 CONSTRAINT [PK_Types_of_services] PRIMARY KEY CLUSTERED 
(
	[Id_Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Work_shift]    Script Date: 15.06.2023 12:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Work_shift](
	[Id_Shift] [int] NOT NULL,
	[Start_Work_Day] [time](7) NOT NULL,
	[End_Work_Day] [time](7) NOT NULL,
	[Number] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Work_Shift] PRIMARY KEY CLUSTERED 
(
	[Id_Shift] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admins] ON 

INSERT [dbo].[Admins] ([Id_Admin], [Last_Name], [First_Name], [Middle_Name], [Login], [Password]) VALUES (1, N'Завгородний', N'Владислав', N'Дмитриевич', N'VlaZav', N'1111')
INSERT [dbo].[Admins] ([Id_Admin], [Last_Name], [First_Name], [Middle_Name], [Login], [Password]) VALUES (19, N'Курсовой', N'Админ', N'Дмитриевич', N'testLogin', N'1111')
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1028, 1, 4, CAST(N'2023-06-01T17:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1029, 2, 1, CAST(N'2023-05-31T02:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1030, 5, 5, CAST(N'2023-06-04T11:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1031, 3, 10, CAST(N'2023-06-03T18:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1032, 17, 10, CAST(N'2023-06-21T16:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1033, 15, 8, CAST(N'2023-06-09T03:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1034, 4, 7, CAST(N'2023-06-02T15:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1035, 17, 6, CAST(N'2023-06-13T10:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1036, 9, 3, CAST(N'2023-06-01T11:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1038, 18, 11, CAST(N'2023-06-02T19:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1039, 8, 9, CAST(N'2023-06-03T13:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1040, 7, 6, CAST(N'2023-05-31T17:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1041, 6, 4, CAST(N'2023-05-31T22:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1042, 1, 5, CAST(N'2023-06-10T19:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1043, 1, 1, CAST(N'2023-06-10T15:00:00' AS SmallDateTime))
INSERT [dbo].[Appointments] ([Id_Appointmaent], [Id_Doctor], [Id_Patient], [DataTime_Appointment]) VALUES (1044, 20, 13, CAST(N'2023-06-15T19:00:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[Appointments] OFF
GO
SET IDENTITY_INSERT [dbo].[Authorization_history] ON 

INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10359, 1, CAST(N'2023-06-12T10:51:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10360, 1, CAST(N'2023-06-12T10:55:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10361, 1, CAST(N'2023-06-12T10:55:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10362, 1, CAST(N'2023-06-12T10:57:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10363, 1, CAST(N'2023-06-13T09:36:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10364, 1, CAST(N'2023-06-13T11:37:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10365, 1, CAST(N'2023-06-13T11:40:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10366, 19, CAST(N'2023-06-14T00:30:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10367, 1, CAST(N'2023-06-14T00:32:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10368, 1, CAST(N'2023-06-14T12:19:00' AS SmallDateTime))
INSERT [dbo].[Authorization_history] ([Id_Auth], [Id_Admin], [Date_Auth]) VALUES (10369, 19, CAST(N'2023-06-14T13:05:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[Authorization_history] OFF
GO
SET IDENTITY_INSERT [dbo].[Conclusions] ON 

INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (1, 1, 1, N'Повышение АД, ускоренное сердцебиение, отдышка', N'Не курит, операций и трвам нет. В контакте с инфецированными за последние 14 дней не был', N'В лёгких везикулярное дыхание. Тоны сердца ясные, ритмичные. АД - 160/90. Отёков нет', N'Гипертоническая болезнь 2 стадии, 2 степени, риск 3', N'Таблеточек, да побольше', CAST(N'2023-05-08T12:00:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (6, 3, 4, N'Нервозность', N'Перегрузка на работе', N'Тревожность и раздражительность', N'Невроз', N'Психотерапия и релаксационные техники', CAST(N'2023-04-27T15:00:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (7, 4, 9, N'Боль в спине', N'Физическая активность', N'Болевой синдром в поясничной области', N'Остеохондроз', N'Физиотерапия и растяжка', CAST(N'2023-04-21T11:30:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (8, 5, 18, N'Зуд кожи', N'Аллергическая реакция', N'Покраснение и сыпь на коже', N'Аллергия', N'Антигистаминные препараты и местное лечение', CAST(N'2023-01-30T09:30:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (10, 6, 8, N'Бессонница', N'Стресс и беспокойство', N'Нарушение сна и бодрствования', N'Бессонница', N'Психотерапия и снотворные препараты', CAST(N'2023-02-25T12:40:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (12, 7, 15, N'Боль в зубах', N'Пренебрежение гигиеной', N'Разрушение зуба', N'Кариес', N'Удаление нерва и пломбирование зуба', CAST(N'2023-05-30T02:30:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (13, 8, 5, N'Боли в правом подреберье', N'Наличие желтушного оттенка кожи', N'Увеличение печени', N'Холецистит', N'Холецистэктомия', CAST(N'2023-01-15T10:30:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (16, 9, 6, N'Покраснение и зуд кожи', N'Контакт с новым моющим средством', N'Покраснение и отечность кожи', N'Контактный дерматит', N'Избегать контакта с аллергеном, противовоспалительные мази', CAST(N'2023-02-05T11:30:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (17, 10, 7, N'Покраснение глаз', N'Ощущение песка в глазах', N'Конъюнктивит', N'Аллергический конъюнктивит', N'Противогистаминные капли и смазки', CAST(N'2023-03-10T14:15:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (19, 11, 17, N'Головная боль', N'Повышенное напряжение, стресс', N'Напряжение мышц шеи и головы', N'Напряженная головная боль', N'Релаксация, физиотерапия', CAST(N'2023-04-20T11:45:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (20, 12, 2, N'Кровотечение из прямой кишки', N'Ощущение дискомфорта, запоры', N'Кровотечение, трещины', N'Внутренние геморроиды', N'Регулярное стулосблагающее средство', CAST(N'2023-05-15T01:30:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (21, 7, 18, N'Усталость, повышенная сонливость', N'Недостаток сна, сниженная физическая активность', N'Общая слабость', N'Хроническая усталость', N'Регулярный сон, физические упражнения', CAST(N'2023-06-20T14:00:00' AS SmallDateTime))
INSERT [dbo].[Conclusions] ([Id_Counclusion], [Id_Patient], [Id_Doctor], [Complaint], [Life_History], [Objective_Status], [Diagnosis], [Treatment], [DataTime_Conclusion]) VALUES (22, 1, 4, N'Чувство тревоги, депрессия', N'Потеря интереса к жизни, беспокойство', N'Пониженное настроение, тревожность', N'Депрессивное и тревожное расстройство', N'Психотерапия, поддержка, возможно препараты', CAST(N'2023-07-10T16:30:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[Conclusions] OFF
GO
SET IDENTITY_INSERT [dbo].[Doctors] ON 

INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (1, N'Завгородняя', N'Елена', N'Николаевна', 3, 2)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (2, N'Борисов', N'Андрей', N'Александрович', 12, 4)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (3, N'Давыдова', N'Ксения', N'Антоновна', 7, 2)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (4, N'Копылов', N'Максим', N'Егорович', 4, 2)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (5, N'Станкевич', N'Полина', N'Евгеньевна', 2, 1)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (6, N'Шевченко', N'Леська', N'Сергеевна', 8, 4)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (7, N'Завгородний', N'Денис', N'Дмитриевич', 5, 2)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (8, N'Айболит', N'Арнольд', N'Шварцнигер', 4, 1)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (9, N'Филимонова', N'Полина', N'Артёмовна', 9, 1)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (15, N'Гренков', N'Илья', N'Макарович', 6, 4)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (17, N'Нерничев', N'Максим', N'Георгиевич', 11, 3)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (18, N'Крипаров', N'Александр', N'Максимович', 1, 3)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (19, N'Захарова', N'Анна', N'Александрова', 1, 4)
INSERT [dbo].[Doctors] ([Id_Doctor], [Last_Name], [First_Name], [Middle_Name], [Specialty], [Shift]) VALUES (20, N'Скляр', N'Андрей', N'Александрович', 14, 2)
SET IDENTITY_INSERT [dbo].[Doctors] OFF
GO
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (1, N'Демченко', N'Антон', N'Сергеевич', N'+7(903)-456-88-97', CAST(N'2004-01-01' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (3, N'Егорова', N'Евгения', N'Андреевна', N'+7(212)-100-00-00', CAST(N'1900-02-01' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (4, N'Завгородний', N'Денис', N'Дмитриевич', N'+7(847)-465-14-65', CAST(N'2010-05-27' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (5, N'Шевченко', N'Леська', N'Сергеевна', N'+7(961)-538-22-13', CAST(N'2010-12-28' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (6, N'Войнов', N'Иван', N'Петрович', N'+7(815)-234-12-44', CAST(N'2023-05-08' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (7, N'Ивочкин', N'Дмитрий', N'Алексеевич', N'+7(800)-555-35-35', CAST(N'2010-02-25' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (8, N'Ковров', N'Коралл', N'Матвеевич', N'+7(919)-265-24-19', CAST(N'2007-02-21' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (9, N'Зубенко', N'Михаил', N'Петрович', N'+7(954)-753-11-75', CAST(N'1999-06-01' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (10, N'Кочеров', N'Иван', N'Романович', N'+7(992)-315-45-64', CAST(N'2004-10-15' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (11, N'Иванченко', N'Илья', N'Ильич', N'+7(854)-735-15-64', CAST(N'1980-07-11' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (12, N'Измалов', N'Кирилл', N'Иванов', N'+7(745)-315-87-55', CAST(N'1984-06-21' AS Date))
INSERT [dbo].[Patients] ([Id_Patient], [Last_Name], [First_Name], [Middle_Name], [Phone], [Bithday]) VALUES (13, N'Як', N'Дмитрий', N'Евгеньевич', N'+7(875)-615-45-77', CAST(N'2023-06-08' AS Date))
SET IDENTITY_INSERT [dbo].[Patients] OFF
GO
SET IDENTITY_INSERT [dbo].[Types_of_services] ON 

INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (1, N'Терапевт', 1250)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (2, N'Хирург', 1000)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (3, N'Кардиолог', 1200)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (4, N'Психотерапевт', 1000)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (5, N'Окулист', 500)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (6, N'Стоматолог', 6000)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (7, N'Педиатр', 850)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (8, N'Косметолог', 3000)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (9, N'Травмотолог', 1500)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (11, N'Неврология', 1300)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (12, N'Проктолог', 1000)
INSERT [dbo].[Types_of_services] ([Id_Type], [Name], [Cost]) VALUES (14, N'Уролог', 7000)
SET IDENTITY_INSERT [dbo].[Types_of_services] OFF
GO
INSERT [dbo].[Work_shift] ([Id_Shift], [Start_Work_Day], [End_Work_Day], [Number]) VALUES (1, CAST(N'08:00:00' AS Time), CAST(N'14:00:00' AS Time), N'1')
INSERT [dbo].[Work_shift] ([Id_Shift], [Start_Work_Day], [End_Work_Day], [Number]) VALUES (2, CAST(N'14:00:00' AS Time), CAST(N'20:00:00' AS Time), N'2         ')
INSERT [dbo].[Work_shift] ([Id_Shift], [Start_Work_Day], [End_Work_Day], [Number]) VALUES (3, CAST(N'08:00:00' AS Time), CAST(N'20:00:00' AS Time), N'Полный день')
INSERT [dbo].[Work_shift] ([Id_Shift], [Start_Work_Day], [End_Work_Day], [Number]) VALUES (4, CAST(N'20:00:00' AS Time), CAST(N'08:00:00' AS Time), N'Ночная')
GO
ALTER TABLE [dbo].[Conclusions] ADD  CONSTRAINT [DF_Conclusions_Id_Doctor]  DEFAULT ((0)) FOR [Id_Doctor]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Doctors1] FOREIGN KEY([Id_Doctor])
REFERENCES [dbo].[Doctors] ([Id_Doctor])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Doctors1]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Patients1] FOREIGN KEY([Id_Patient])
REFERENCES [dbo].[Patients] ([Id_Patient])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Patients1]
GO
ALTER TABLE [dbo].[Authorization_history]  WITH CHECK ADD  CONSTRAINT [FK_Authorization_history_Admins] FOREIGN KEY([Id_Admin])
REFERENCES [dbo].[Admins] ([Id_Admin])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Authorization_history] CHECK CONSTRAINT [FK_Authorization_history_Admins]
GO
ALTER TABLE [dbo].[Conclusions]  WITH CHECK ADD  CONSTRAINT [FK_Conclusions_Doctors1] FOREIGN KEY([Id_Doctor])
REFERENCES [dbo].[Doctors] ([Id_Doctor])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Conclusions] CHECK CONSTRAINT [FK_Conclusions_Doctors1]
GO
ALTER TABLE [dbo].[Conclusions]  WITH CHECK ADD  CONSTRAINT [FK_Conclusions_Patients1] FOREIGN KEY([Id_Patient])
REFERENCES [dbo].[Patients] ([Id_Patient])
GO
ALTER TABLE [dbo].[Conclusions] CHECK CONSTRAINT [FK_Conclusions_Patients1]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Types_of_services] FOREIGN KEY([Specialty])
REFERENCES [dbo].[Types_of_services] ([Id_Type])
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_Types_of_services]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Work_Shift] FOREIGN KEY([Shift])
REFERENCES [dbo].[Work_shift] ([Id_Shift])
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_Work_Shift]
GO
ALTER TABLE [dbo].[Admins]  WITH CHECK ADD  CONSTRAINT [CK_Admins_FName] CHECK  ((len([First_Name])>=(2)))
GO
ALTER TABLE [dbo].[Admins] CHECK CONSTRAINT [CK_Admins_FName]
GO
ALTER TABLE [dbo].[Admins]  WITH CHECK ADD  CONSTRAINT [CK_Admins_LName] CHECK  ((len([Last_Name])>=(2)))
GO
ALTER TABLE [dbo].[Admins] CHECK CONSTRAINT [CK_Admins_LName]
GO
ALTER TABLE [dbo].[Admins]  WITH CHECK ADD  CONSTRAINT [CK_Admins_Login] CHECK  ((len([Login])>=(3)))
GO
ALTER TABLE [dbo].[Admins] CHECK CONSTRAINT [CK_Admins_Login]
GO
ALTER TABLE [dbo].[Admins]  WITH CHECK ADD  CONSTRAINT [CK_Admins_Pass] CHECK  ((len([Password])>=(4)))
GO
ALTER TABLE [dbo].[Admins] CHECK CONSTRAINT [CK_Admins_Pass]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [CK_Doctors_FName] CHECK  ((len([First_Name])>=(2)))
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [CK_Doctors_FName]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [CK_Doctors_LName] CHECK  ((len([Last_Name])>=(2)))
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [CK_Doctors_LName]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [CK_Doctors_MName] CHECK  ((len([Middle_Name])>=(4)))
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [CK_Doctors_MName]
GO
USE [master]
GO
ALTER DATABASE [Privat_Hospital] SET  READ_WRITE 
GO
