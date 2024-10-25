/*DROP TABLE IF EXISTS [dbo].[aprobaciones];
DROP TABLE IF EXISTS [dbo].[account];
DROP TABLE IF EXISTS [dbo].[authorizations];
DROP TABLE IF EXISTS [dbo].[notifications];
DROP TABLE IF EXISTS [dbo].[equipos];
DROP TABLE IF EXISTS [dbo].[HistorialEquipos];
DROP TABLE IF EXISTS [dbo].[prueba];
DROP TABLE IF EXISTS [dbo].[Usuarios];
*/

-- Crear la tabla "account"
CREATE TABLE [dbo].[account](
    [account_id] INT IDENTITY(1,1) NOT NULL,
    [user_id] INT NULL,
    [account_type] VARCHAR(50) NULL,
    [balance] DECIMAL(10, 2) NULL DEFAULT ((0.00)),
    [created_at] DATETIME NULL DEFAULT (GETDATE()),
    PRIMARY KEY CLUSTERED ([account_id] ASC)
) ON [PRIMARY];
GO

-- Crear la tabla "authorizations"
CREATE TABLE [dbo].[authorizations](
    [id] INT IDENTITY(1,1) NOT NULL,
    [userId] INT NOT NULL,
    [pages] NVARCHAR(MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

-- Crear la tabla "notifications"
CREATE TABLE [dbo].[notifications](
    [notification_id] INT IDENTITY(1,1) NOT NULL,
    [user_id] INT NULL,
    [message] TEXT NULL,
    [is_read] BIT NULL DEFAULT ((0)),
    [created_at] DATETIME NULL DEFAULT (GETDATE()),
    PRIMARY KEY CLUSTERED ([notification_id] ASC)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

-- Crear la tabla "user"
CREATE TABLE [dbo].[user](
    [user_id] INT IDENTITY(1,1) NOT NULL,
    [username] VARCHAR(50) NOT NULL,
    [password] VARCHAR(255) NOT NULL,
    [email] VARCHAR(100) NOT NULL,
    [created_at] DATETIME NULL DEFAULT (GETDATE()),
    [description] NCHAR(10) NULL,
    [isAuthorized] BIT NULL,
    PRIMARY KEY CLUSTERED ([user_id] ASC)
) ON [PRIMARY];
GO

SELECT * FROM [dbo].[user];
GO

CREATE TABLE [dbo].[equipos] (
    [equipo_id] INT IDENTITY(1,1) NOT NULL,
    [marca] VARCHAR(100) NOT NULL,
    [modelo] VARCHAR(100) NOT NULL,
    [nombre_cliente] VARCHAR(150) NOT NULL,
    [motivo_ingreso] TEXT NOT NULL,
    [garantia_con_local] BIT NOT NULL,
    [contraseña_equipo] VARCHAR(100) NULL,
    [descripcion] TEXT NULL,
    [fecha_ingreso] DATETIME NOT NULL DEFAULT GETDATE(),
    [usuario_id] INT NULL
) ON [PRIMARY];
GO


SELECT * FROM [dbo].[equipos];
GO

-- Crear la tabla "aprobaciones"
CREATE TABLE [dbo].[aprobaciones](
    [criterio] VARCHAR(255) NOT NULL,
    [cumple] BIT NOT NULL
) ON [PRIMARY];
GO

SELECT * FROM [dbo].[aprobaciones];
GO

-- Configurar claves foráneas
ALTER TABLE [dbo].[account] ADD CONSTRAINT [FK_account_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id]);
GO

ALTER TABLE [dbo].[authorizations] ADD CONSTRAINT [FK_authorizations_userId] FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([user_id]);
GO

ALTER TABLE [dbo].[notifications] ADD CONSTRAINT [FK_notifications_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id]);
GO

-- Agregar índices únicos a la tabla "user"
ALTER TABLE [dbo].[user] ADD CONSTRAINT [UQ_user_email] UNIQUE NONCLUSTERED ([email] ASC);
GO

ALTER TABLE [dbo].[user] ADD CONSTRAINT [UQ_user_username] UNIQUE NONCLUSTERED ([username] ASC);
GO

-- Insertar datos de ejemplo en la tabla "user"
SET IDENTITY_INSERT [dbo].[user] ON;
INSERT INTO [dbo].[user] ([user_id], [username], [password], [email], [created_at], [description], [isAuthorized]) VALUES 
(1, N'Marcelo', N'123', N'marcelo@gmail.com', GETDATE(), NULL, NULL),
(2, N'Melanie', N'123', N'melanie@gmail.com', GETDATE(), NULL, NULL),
(3, N'Weyner', N'123', N'weyner@gmail.com', GETDATE(), NULL, NULL);
SET IDENTITY_INSERT [dbo].[user] OFF;
GO


-- Insertar datos de ejemplo en la tabla "account"
SET IDENTITY_INSERT [dbo].[account] ON;
INSERT INTO [dbo].[account] ([account_id], [user_id], [account_type], [balance], [created_at]) VALUES (1, 1, N'admin', CAST(0.00 AS Decimal(10, 2)), GETDATE());
SET IDENTITY_INSERT [dbo].[account] OFF;
GO

-- Insertar datos de ejemplo en la tabla "authorizations"
SET IDENTITY_INSERT [dbo].[authorizations] ON;
INSERT INTO [dbo].[authorizations] ([id], [userId], [pages]) VALUES (1, 1, N'home;users');
SET IDENTITY_INSERT [dbo].[authorizations] OFF;
GO

-- Insertar datos de ejemplo en la tabla "notifications"
SET IDENTITY_INSERT [dbo].[notifications] ON;
INSERT INTO [dbo].[notifications] ([notification_id], [user_id], [message], [is_read], [created_at]) VALUES 
(1, 1, N'Hello this is a new message', 0, GETDATE()),
(2, 1, N'wertty', 0, GETDATE()),
(3, 1, N'notification 2', 0, GETDATE()),
(4, 1, N'notification 3', 0, GETDATE()),
(5, 1, N'notification 4', 0, GETDATE()),
(6, 1, N'notification 5', 0, GETDATE());
SET IDENTITY_INSERT [dbo].[notifications] OFF;
GO

-- Insertar datos de ejemplo en la tabla "equipos" con los campos adicionales y sin "estado"
INSERT INTO [dbo].[equipos] ([marca], [modelo], [nombre_cliente], [motivo_ingreso], [garantia_con_local], [contraseña_equipo], [descripcion], [fecha_ingreso], [usuario_id]) VALUES 
('HP', 'notebook Pavilion Gaming', 'Sebastian', 'Pantalla no enciende', 1, '123','Laptop de uso gamer', GETDATE(), 1),
('Dell', 'Inspiron 3520', 'Mariana', 'Teclado no funciona', 1, '123','Laptop para entretenimiento', GETDATE(), 1);
GO

-- Insertar criterios de aprobación para el equipo de ejemplo en la tabla "aprobaciones"
INSERT INTO [dbo].[aprobaciones] ([criterio], [cumple]) VALUES 
('El equipo no presenta daños visibles graves.', 0),
('La marca del equipo está en la lista de marcas aprobadas.', 0),
('El equipo no presenta rastros de humedad o exposición a calor excesivo.', 0),
('El procesador es de una generación compatible (10ª generación de Intel o posterior).', 0),
('El equipo no ha sido declarado obsoleto por el fabricante.', 0),
('El equipo no presenta daños visibles graves.', 0),
('La marca del equipo está en la lista de marcas aprobadas.', 0),
('El equipo no presenta rastros de humedad o exposición a calor excesivo.', 0),
('El procesador es de una generación compatible (10ª generación de Intel o posterior).', 0),
('El equipo no ha sido declarado obsoleto por el fabricante.', 0);
GO

ALTER TABLE [dbo].[aprobaciones]
ADD [AprobacionId] INT IDENTITY(1,1) PRIMARY KEY;

ALTER TABLE [dbo].[HistorialEquipos]
ADD CONSTRAINT PK_HistorialEquipos PRIMARY KEY ([HistorialId]);

ALTER TABLE [dbo].[equipos]
ADD CONSTRAINT PK_Equipos PRIMARY KEY ([equipo_id]);


CREATE TABLE [dbo].[HistorialEquipos] (
    [HistorialId] INT IDENTITY(1,1) NOT NULL,
    [EquipoId] INT NOT NULL,
    [DescripcionCambio] NVARCHAR(MAX) NOT NULL,
    [FechaCambio] DATETIME NOT NULL DEFAULT GETDATE()
);

SELECT * FROM [dbo].[HistorialEquipos];
GO

CREATE TABLE [dbo].[PRUEBAS] (
    [DATOS] INT IDENTITY(1,1) NOT NULL
);