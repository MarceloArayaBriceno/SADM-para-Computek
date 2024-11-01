
DELETE FROM Facturas;
DBCC CHECKIDENT ('Facturas', RESEED, 0);

DELETE FROM DetallesFactura;
DBCC CHECKIDENT ('DetallesFactura', RESEED, 0);

INSERT INTO Facturas (NombreCliente, DireccionCliente, Fecha, Total) VALUES 
('Carlos Fernández', 'Avenida Central 1245, San José', '2024-10-21', 95000),
('Ana Vargas', 'Calle 32, Alajuela', '2024-10-22', 185000),
('Luis Morales', 'Barrio Los Yoses, San Pedro', '2024-10-23', 27500),
('María Rojas', 'Avenida 2, Cartago', '2024-10-24', 68000),
('Eduardo Jiménez', 'Calle Principal, Heredia', '2024-10-25', 125000);

INSERT INTO DetallesFactura (FacturaID, Descripcion, Cantidad, PrecioUnitario) VALUES 
(1, 'Teclado Mecánico Logitech G Pro', 1, 75000),
(1, 'Mouse Inalámbrico Logitech M170', 1, 20000);

INSERT INTO DetallesFactura (FacturaID, Descripcion, Cantidad, PrecioUnitario) VALUES 
(2, 'Monitor Samsung 24" LED', 1, 90000),
(2, 'Cámara Web Logitech C920 HD', 1, 45000),
(2, 'Audífonos Corsair HS35', 1, 50000);

INSERT INTO DetallesFactura (FacturaID, Descripcion, Cantidad, PrecioUnitario) VALUES 
(3, 'Mouse Pad Razer Gigantus V2', 1, 12500),
(3, 'Limpiador de Pantalla 100ml', 1, 15000);

INSERT INTO DetallesFactura (FacturaID, Descripcion, Cantidad, PrecioUnitario) VALUES 
(4, 'Memoria USB Kingston 64GB', 2, 17000),
(4, 'Cargador Portátil Anker 10000mAh', 1, 34000);

INSERT INTO DetallesFactura (FacturaID, Descripcion, Cantidad, PrecioUnitario) VALUES 
(5, 'SSD Kingston A400 240GB', 1, 45000),
(5, 'Memoria RAM Corsair Vengeance 8GB', 1, 40000),
(5, 'Mouse Gamer Logitech G502', 1, 40000);
