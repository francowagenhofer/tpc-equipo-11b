/*
=========================================
 ClinicaDB
 Script de creación de base de datos
 Proyecto Final - Programación III
=========================================
*/

CREATE DATABASE ClinicaDB;
GO

USE ClinicaDB;
GO

----------------------------------------------
--                 ROLES                
----------------------------------------------

CREATE TABLE Roles (
    IDRol INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(50) NOT NULL UNIQUE, 

	Activo BIT NOT NULL DEFAULT 1

);
GO

----------------------------------------------
--                 USUARIOS                         
----------------------------------------------

CREATE TABLE Usuarios (
    IDUsuario INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(100) NOT NULL,

    Apellido VARCHAR(100) NOT NULL,

    Email VARCHAR(150) NOT NULL UNIQUE,

    Telefono VARCHAR(30),

    Username VARCHAR(50) NOT NULL UNIQUE,

    PasswordHash VARCHAR(255) NOT NULL,
	
	ImagenUrl VARCHAR(300) NULL, 

	FechaAlta DATETIME NOT NULL DEFAULT GETDATE(),
    
	IDRol INT NOT NULL,

    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Usuarios_Roles
        FOREIGN KEY (IDRol)
        REFERENCES Roles(IDRol)
);
GO

----------------------------------------------
--                GENEROS                        
----------------------------------------------

CREATE TABLE Generos (
    IDGenero INT PRIMARY KEY IDENTITY(1,1),

    Descripcion VARCHAR(50) NOT NULL UNIQUE,

    Activo BIT NOT NULL DEFAULT 1
);
GO

----------------------------------------------
--          OBRASSOCIALES                    
----------------------------------------------
CREATE TABLE ObrasSociales (
    IDObraSocial INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(100) NOT NULL UNIQUE,

    Activo BIT NOT NULL DEFAULT 1
);
GO

----------------------------------------------
--                PACIENTES                        
----------------------------------------------

CREATE TABLE Pacientes (
    IDPaciente INT PRIMARY KEY IDENTITY(1,1),

    IDUsuario INT NOT NULL UNIQUE,

    DNI VARCHAR(20) NOT NULL UNIQUE,

    FechaNacimiento DATE NOT NULL,

    Direccion VARCHAR(200) NULL,

    IDObraSocial INT NULL,

    IDGenero INT NULL,

    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Pacientes_Usuarios
        FOREIGN KEY (IDUsuario)
        REFERENCES Usuarios(IDUsuario),

    CONSTRAINT FK_Pacientes_ObrasSociales
        FOREIGN KEY (IDObraSocial)
        REFERENCES ObrasSociales(IDObraSocial),

    CONSTRAINT FK_Pacientes_Generos
        FOREIGN KEY (IDGenero)
        REFERENCES Generos(IDGenero)
);
GO

----------------------------------------------
--               MEDICOS                         
----------------------------------------------

CREATE TABLE Medicos (
    IDMedico INT PRIMARY KEY IDENTITY(1,1),

    IDUsuario INT NOT NULL UNIQUE,

    Matricula VARCHAR(50) NOT NULL UNIQUE,

    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Medicos_Usuarios
        FOREIGN KEY (IDUsuario)
        REFERENCES Usuarios(IDUsuario)
);
GO

----------------------------------------------
--              ESPECIALIDADES                      
----------------------------------------------

CREATE TABLE Especialidades (
    IDEspecialidad INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(100) NOT NULL UNIQUE,

    Descripcion VARCHAR(300),

    Activo BIT NOT NULL DEFAULT 1
);
GO

----------------------------------------------
--             MEDICO_ESPECIALIDAD                    
----------------------------------------------

CREATE TABLE MedicoEspecialidad (
    IDMedicoEspecialidad INT PRIMARY KEY IDENTITY(1,1),

    IDMedico INT NOT NULL,

    IDEspecialidad INT NOT NULL,

    CONSTRAINT UQ_MedicoEspecialidad
        UNIQUE (IDMedico, IDEspecialidad),

    CONSTRAINT FK_MedicoEspecialidad_Medicos
        FOREIGN KEY (IDMedico)
        REFERENCES Medicos(IDMedico),

    CONSTRAINT FK_MedicoEspecialidad_Especialidades
        FOREIGN KEY (IDEspecialidad)
        REFERENCES Especialidades(IDEspecialidad)
);
GO

----------------------------------------------
--           DISPONIBILIDAD MEDICO                   
----------------------------------------------

CREATE TABLE DisponibilidadMedico (
    IDDisponibilidad INT PRIMARY KEY IDENTITY(1,1),

    IDMedico INT NOT NULL,

    DiaSemana INT NOT NULL
        CHECK (DiaSemana BETWEEN 1 AND 7),

    HoraInicio TIME NOT NULL,

    HoraFin TIME NOT NULL,

    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT CHK_Disponibilidad_Horarios
        CHECK (HoraInicio < HoraFin),

    CONSTRAINT FK_DisponibilidadMedico_Medicos
        FOREIGN KEY (IDMedico)
        REFERENCES Medicos(IDMedico)
);
GO

----------------------------------------------
--               ESTADO TURNO                         
----------------------------------------------

CREATE TABLE EstadoTurno (
    IDEstadoTurno INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(50) NOT NULL UNIQUE,

	Activo BIT NOT NULL DEFAULT 1,
);
GO

----------------------------------------------
--                 TURNOS                         
----------------------------------------------

CREATE TABLE Turnos (
    IDTurno INT PRIMARY KEY IDENTITY(1,1),
	
    Codigo VARCHAR(20) NOT NULL UNIQUE,

    IDPaciente INT NOT NULL,

    IDMedico INT NOT NULL,

    FechaHora DATETIME NOT NULL,

    IDEstadoTurno INT NOT NULL,

    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),

    FechaModificacion DATETIME NULL,

	-- Evita que un médico tenga dos turnos en la misma fecha y hora
    CONSTRAINT UQ_Turno_Medico_FechaHora
        UNIQUE (IDMedico, FechaHora), 

	-- Evita que un paciente tenga dos turnos en la misma fecha y hora
    CONSTRAINT UQ_Turno_Paciente_FechaHora
        UNIQUE (IDPaciente, FechaHora),

    CONSTRAINT FK_Turnos_Pacientes
        FOREIGN KEY (IDPaciente)
        REFERENCES Pacientes(IDPaciente),

    CONSTRAINT FK_Turnos_Medicos
        FOREIGN KEY (IDMedico)
        REFERENCES Medicos(IDMedico),

    CONSTRAINT FK_Turnos_EstadoTurno
        FOREIGN KEY (IDEstadoTurno)
        REFERENCES EstadoTurno(IDEstadoTurno)
);
GO

----------------------------------------------
--           HISTORIA CLINICA                      
----------------------------------------------

CREATE TABLE HistoriaClinica (
    IDHistoriaClinica INT PRIMARY KEY IDENTITY(1,1),

    IDPaciente INT NOT NULL,

    IDMedico INT NOT NULL,

    IDTurno INT NULL,

    Fecha DATETIME NOT NULL DEFAULT GETDATE(),

    Diagnostico NVARCHAR(500) NOT NULL,

    Tratamiento NVARCHAR(500),

    Observaciones NVARCHAR(500),

    CONSTRAINT FK_HistoriaClinica_Pacientes
        FOREIGN KEY (IDPaciente)
        REFERENCES Pacientes(IDPaciente),

    CONSTRAINT FK_HistoriaClinica_Medicos
        FOREIGN KEY (IDMedico)
        REFERENCES Medicos(IDMedico),

    CONSTRAINT FK_HistoriaClinica_Turnos
        FOREIGN KEY (IDTurno)
        REFERENCES Turnos(IDTurno)
);
GO

