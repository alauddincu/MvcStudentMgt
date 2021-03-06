USE [dbase]
GO
/****** Object:  Table [dbo].[CourseDetail]    Script Date: 16-Oct-15 7:08:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CourseDetail](
	[courseId] [varchar](50) NOT NULL,
	[courseName] [varchar](50) NULL,
	[courseMarks] [int] NULL,
 CONSTRAINT [PK_CourseDetail] PRIMARY KEY CLUSTERED 
(
	[courseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentDetail]    Script Date: 16-Oct-15 7:08:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentDetail](
	[id] [varchar](50) NOT NULL,
	[name] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[section] [varchar](50) NULL,
	[marks] [int] NULL,
 CONSTRAINT [PK_StudentDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAcc]    Script Date: 16-Oct-15 7:08:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAcc](
	[userMail] [varchar](50) NOT NULL,
	[userPass] [varchar](50) NOT NULL,
	[userType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserAcc] PRIMARY KEY CLUSTERED 
(
	[userMail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
