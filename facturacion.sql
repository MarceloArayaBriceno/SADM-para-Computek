CREATE TABLE Facturas (
    FacturaID INT PRIMARY KEY IDENTITY,
    NombreCliente NVARCHAR(50),
    DireccionCliente NVARCHAR(100),
    Fecha DATE,
    Total DECIMAL(10, 2)
);

CREATE TABLE DetallesFactura (
    DetalleID INT PRIMARY KEY IDENTITY,
    FacturaID INT FOREIGN KEY REFERENCES Facturas(FacturaID) ON DELETE CASCADE,
    Descripcion NVARCHAR(100),
    Cantidad INT,
    PrecioUnitario DECIMAL(10, 2),
    Subtotal AS (Cantidad * PrecioUnitario)
);

