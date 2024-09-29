-- Bảng User
CREATE TABLE [dbo].[User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(255),
    RoleGroupId INT FOREIGN KEY REFERENCES RoleGroup(RoleGroupId)
);

-- Bảng RoleGroup
CREATE TABLE [dbo].[RoleGroup] (
    RoleGroupId INT PRIMARY KEY IDENTITY(1,1),
    RoleGroupName NVARCHAR(255) NOT NULL
);

-- Bảng Role
CREATE TABLE [dbo].[Role] (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX)
);

-- Bảng Permission
CREATE TABLE [dbo].[Permission] (
    PermissionId INT PRIMARY KEY IDENTITY(1,1),
    TableName NVARCHAR(255) NOT NULL,
    RoleGroupId INT FOREIGN KEY REFERENCES RoleGroup(RoleGroupId),
    RoleId INT FOREIGN KEY REFERENCES Role(RoleId),
    CanView BIT NOT NULL,
    CanAdd BIT NOT NULL,
    CanEdit BIT NOT NULL,
    CanDelete BIT NOT NULL
);

-- Bảng Menu
CREATE TABLE [dbo].[Menu] (
    MenuId INT PRIMARY KEY IDENTITY(1,1),
    MenuName NVARCHAR(255) NOT NULL,
    MenuUrl NVARCHAR(255),
    ParentMenuId INT NULL,
    IsVisible BIT NOT NULL
);

-- Bảng RoleMenuAccess
CREATE TABLE [dbo].[RoleMenuAccess] (
    AccessId INT PRIMARY KEY IDENTITY(1,1),
    RoleGroupId INT FOREIGN KEY REFERENCES RoleGroup(RoleGroupId),
    MenuId INT FOREIGN KEY REFERENCES Menu(MenuId),
    CanAccess BIT NOT NULL
);

-- Bảng Project
CREATE TABLE [dbo].[Project] (
    ProjectId INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME,
    Status NVARCHAR(50) NOT NULL,
    CreatedBy INT FOREIGN KEY REFERENCES User(UserId),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng Task
CREATE TABLE [dbo].[Task] (
    TaskId INT PRIMARY KEY IDENTITY(1,1),
    ProjectId INT FOREIGN KEY REFERENCES Project(ProjectId),
    AssignedTo INT FOREIGN KEY REFERENCES User(UserId),
    TaskName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    DueDate DATETIME,
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
);

-- Bảng Request
CREATE TABLE [dbo].[Request] (
    RequestId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES User(UserId),
    ProjectId INT FOREIGN KEY REFERENCES Project(ProjectId),
    RequestType NVARCHAR(100),
    Description NVARCHAR(MAX),
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
);

-- Bảng ProjectUser
CREATE TABLE [dbo].[ProjectUser] (
    ProjectUserId INT PRIMARY KEY IDENTITY(1,1),
    ProjectId INT FOREIGN KEY REFERENCES Project(ProjectId),
    UserId INT FOREIGN KEY REFERENCES User(UserId),
    Role NVARCHAR(50),
    JoinedAt DATETIME DEFAULT GETDATE()
);

-- Bảng Comment
CREATE TABLE [dbo].[Comment] (
    CommentId INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT FOREIGN KEY REFERENCES Task(TaskId),
    UserId INT FOREIGN KEY REFERENCES User(UserId),
    Content NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE RefreshTokens
(
    Token NVARCHAR(255) PRIMARY KEY,
    ExpiryDate DATETIME NOT NULL,
    IsRevoked BIT NOT NULL,
    UserId NVARCHAR(255) NOT NULL
);

