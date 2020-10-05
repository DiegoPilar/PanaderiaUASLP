create database Panaderia;

use Panaderia;

create table Proveedor (
	
	idProveedor BIGINT IDENTITY (1,1) not null,
	Nombre VARCHAR(20) not null,
	RFC VARCHAR (13) not null,
	Direccion VARCHAR(20) not null,
	Email VARCHAR(30) not null,

	CONSTRAINT PKPROVEEDOR PRIMARY KEY(idProveedor)
);

ALTER TABLE dbo.Proveedor
ADD CONSTRAINT UQ_Correoprovedor UNIQUE (Email);
ALTER TABLE dbo.Proveedor
ADD CONSTRAINT UQ_RFCprovedor UNIQUE (RFC);

create table Material(
	
		
	idMaterial BIGINT IDENTITY (1,1) not null,
	Nombre VARCHAR(50) not null,
	Precio REAL not null,
	Existencia INT,


	CONSTRAINT PKMATERIAL PRIMARY KEY(idMaterial)

);
ALTER TABLE dbo.Material
ADD CONSTRAINT UQ_material UNIQUE (Nombre);
create table Cliente(
	
	idCliente BIGINT IDENTITY (1,1) not null,
	Nombre VARCHAR(50) not null,
	Telefono VARCHAR (13) not null,
	Email VARCHAR(30) not null,
	Visitas INT not null



	CONSTRAINT PKCLIENTE PRIMARY KEY(idCliente)

);

create table Venta(
	
	idVenta BIGINT IDENTITY (1,1) not null,

	MontoTotal REAL not null,
	DescuentoVisita REAL not null,
	FechaVenta DATE not null,


	CONSTRAINT PKVENTA PRIMARY KEY(idVenta)
);

create table Producto(

	idProducto BIGINT IDENTITY (1,1) not null,
	Nombre VARCHAR(50) not null,
	Precio REAL not null,
	Existencia INT not null,

	CONSTRAINT PKPRODUCTO PRIMARY KEY(idProducto)
);
create table Compra (

	idCompra BIGINT IDENTITY (1,1) not null,
	Fecha DATE not null,
	idProveedor BIGINT not null,
	Total REAL not null,

	CONSTRAINT PKCOMPRA PRIMARY KEY(idCompra),
	CONSTRAINT FKPROVEEDOR FOREIGN KEY (idProveedor) REFERENCES Proveedor(idProveedor)
);

create table DetalleCompra(

	idMaterial BIGINT not null,
	idCompra BIGINT not null,
	Cantidad INT not null,
	SubTotal REAL not null,

	CONSTRAINT FKMATERIAL FOREIGN KEY (idMaterial) REFERENCES Material (idMaterial),

	CONSTRAINT FKCOMPRA FOREIGN KEY (idCompra) REFERENCES Compra (idCompra)

);

create table Produccion(

	idProduccion BIGINT IDENTITY (1,1) not null,
	idProducto BIGINT not null,
	FechaProduccion DATE not null,
	TotalProduccion INT not null,

	CONSTRAINT PKPRODUCCION PRIMARY KEY(idProduccion),
	CONSTRAINT FKPRODUCTO FOREIGN KEY (idProducto) REFERENCES Producto (idProducto)
);

create table MaterialProduccion(

	idMaterial BIGINT not null,
	idProduccion BIGINT not null,
	CantidadConsumida INT not null,

	CONSTRAINT FKMATERIAL_A FOREIGN KEY (idMaterial) REFERENCES Material (idMaterial),
	CONSTRAINT FKPRODUCCION FOREIGN KEY (idProduccion) REFERENCES Produccion (idProduccion)
);

create table DetalleVenta(

	
	idProducto BIGINT not null,
	idVenta BIGINT not null,
	Unidades INT not null,
	SubTotal REAL not null,

	CONSTRAINT FKPRODUCTO_O FOREIGN KEY (idProducto) REFERENCES Producto (idProducto),

	CONSTRAINT FKVENTA FOREIGN KEY (idVenta) REFERENCES Venta (idVenta)
);

CREATE TRIGGER dbo.sumaproductos ON dbo.Produccion FOR INSERT, DELETE,UPDATE AS
			DECLARE @idProducto AS BIGINT 
			DECLARE @TotalProduccion AS INT
BEGIN
	IF EXISTS (SELECT * FROM inserted)	
	BEGIN
		SELECT @idProducto = idProducto, @TotalProduccion = TotalProduccion FROM inserted
		UPDATE dbo.Producto SET Existencia = (SELECT Existencia FROM dbo.Producto WHERE idProducto=@idProducto)+@TotalProduccion WHERE idProducto=@idProducto
	END
	ELSE
	BEGIN
		SELECT @idProducto = idProducto, @TotalProduccion = TotalProduccion FROM deleted
		UPDATE dbo.Producto SET Existencia = (SELECT Existencia FROM dbo.Producto WHERE idProducto=@idProducto)-@TotalProduccion WHERE idProducto=@idProducto
	END
END;


CREATE TRIGGER dbo.sumamaterial ON dbo.DetalleCompra FOR INSERT, DELETE,UPDATE AS
			DECLARE @idMaterial AS BIGINT 
			DECLARE @Cantidad AS INT
BEGIN
	IF EXISTS (SELECT * FROM inserted)	
	BEGIN
		SELECT @idMaterial = idMaterial, @Cantidad = Cantidad FROM inserted
		UPDATE dbo.Material SET Existencia = (SELECT Existencia FROM dbo.Material WHERE idMaterial=@idMaterial)+@Cantidad WHERE idMaterial=@idMaterial
	END
	ELSE
	BEGIN
		SELECT @idMaterial = idMaterial, @Cantidad = Cantidad FROM deleted
		UPDATE dbo.Material SET Existencia = (SELECT Existencia FROM dbo.Material WHERE idMaterial=@idMaterial)-@Cantidad WHERE idMaterial=@idMaterial
	END
END;
INSERT INTO dbo.Producto (Nombre,Precio,Existencia) VALUES ('Oreja',3,0);
INSERT INTO dbo.Produccion (idProducto,FechaProduccion,TotalProduccion) VALUES ('1','2020-10-01',3)
select * from dbo.Producto
select * from dbo.Produccion
delete dbo.Produccion where idProduccion=2
INSERT INTO dbo.Material (Nombre,Precio,Existencia) VALUES ('Harina',10,0)
INSERT INTO dbo.Proveedor (Nombre,RFC,Direccion,Email) VALUES ('bimbo','bjbkjbkjbjbb','slp','jlnj@knlknk');
select * from dbo.Proveedor 
INSERT INTO dbo.Compra (Fecha,idProveedor,Total) VALUES ('2020-10-01',1,0)
select * from dbo.Compra
INSERT INTO dbo.DetalleCompra(idMaterial,idCompra,Cantidad,SubTotal) VALUES (1,1,20,100)
select * from dbo.DetalleCompra
SELECT * FROM dbo.Material
DELETE dbo.DetalleCompra WHERE idCompra=1