
CREATE DATABASE GestorClientes;
GO

USE GestorClientes;
GO

CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Cedula NVARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(150) NOT NULL
);

CREATE TABLE Telefonos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Numero NVARCHAR(20) NOT NULL,
    ClienteId INT NOT NULL,

    CONSTRAINT FK_Telefonos_Clientes
    FOREIGN KEY (ClienteId)
    REFERENCES Clientes(Id)
    ON DELETE CASCADE
);
