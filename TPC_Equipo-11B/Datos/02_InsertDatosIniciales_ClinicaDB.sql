USE ClinicaDB;
GO

-----------------------------------
-- ROLES
-----------------------------------
INSERT INTO Roles (Nombre)
VALUES 
('Administrador'),
('Recepcionista'),
('Medico'),
('Paciente');
GO

-----------------------------------
-- GENEROS
-----------------------------------
INSERT INTO Generos (Descripcion)
VALUES
('Masculino'),
('Femenino'),
('Otro');
GO

-----------------------------------
-- OBRAS SOCIALES
-----------------------------------
INSERT INTO ObrasSociales (Nombre)
VALUES
('Particular'),
('OSDE'),
('Swiss Medical'),
('Galeno'),
('Medife'),
('PAMI'),
('IOMA');
GO

-----------------------------------
-- USUARIOS (10 max)
-----------------------------------
INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo)
VALUES
('Admin', 'Sistema', 'admin@clinica.com', '1111111111', 'admin', '123', 1, 1),

('Carla', 'Gomez', 'carla@clinica.com', '1111111112', 'cgomez', '123', 2, 1),
('Martin', 'Lopez', 'martin@clinica.com', '1111111113', 'mlopez', '123', 2, 1),

('Juan', 'Perez', 'jperez@clinica.com', '1111111114', 'jperez', '123', 3, 1),
('Maria', 'Gonzalez', 'mgonzalez@clinica.com', '1111111115', 'mgonzalez', '123', 3, 1),
('Carlos', 'Diaz', 'cdiaz@clinica.com', '1111111116', 'cdiaz', '123', 3, 1),

('Ana', 'Fernandez', 'afernandez@clinica.com', '1111111117', 'afernandez', '123', 3, 1),
('Luis', 'Martinez', 'lmartinez@clinica.com', '1111111118', 'lmartinez', '123', 3, 1),

('Sofia', 'Ruiz', 'sruiz@clinica.com', '1111111119', 'sruiz', '123', 4, 1),
('Diego', 'Sanchez', 'dsanchez@clinica.com', '1111111120', 'dsanchez', '123', 4, 1);
GO

-----------------------------------
-- MEDICOS (depende de Usuarios)
-----------------------------------
INSERT INTO Medicos (IDUsuario, Matricula, Activo)
VALUES
(4, 'MED-1001', 1),
(5, 'MED-1002', 1),
(6, 'MED-1003', 1),
(7, 'MED-1004', 1),
(8, 'MED-1005', 1);
GO

-----------------------------------
-- PACIENTES
-----------------------------------
INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
VALUES
(9,  '30111222', '1990-05-10', 'Av Corrientes 123', 2, 1, 1),
(10, '28999111', '1985-03-21', 'San Martin 456', 3, 2, 1);
GO

-----------------------------------
-- ESPECIALIDADES
-----------------------------------
INSERT INTO Especialidades (Nombre, Descripcion, Activo)
VALUES
('Cardiología', 'Corazón', 1),
('Clínica Médica', 'General', 1),
('Pediatría', 'Niños', 1),
('Traumatología', 'Huesos', 1),
('Neurología', 'Sistema nervioso', 1);
GO

-----------------------------------
-- MEDICO ESPECIALIDAD
-----------------------------------
INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad)
VALUES
(1,1),
(2,2),
(3,3),
(4,4),
(5,5);
GO

-----------------------------------
-- ESTADO TURNO
-----------------------------------
INSERT INTO EstadoTurno (Nombre)
VALUES
('Pendiente'),
('Confirmado'),
('Cancelado'),
('Finalizado');
GO

-----------------------------------
-- TURNOS
-----------------------------------
INSERT INTO Turnos (Codigo, IDPaciente, IDMedico, FechaHora, IDEstadoTurno)
VALUES
('T-001', 1, 1, '2026-06-10 09:00', 1),
('T-002', 2, 2, '2026-06-10 10:00', 2),
('T-003', 1, 3, '2026-06-11 11:00', 1);
GO

-----------------------------------
-- DISPONIBILIDAD
-----------------------------------
INSERT INTO DisponibilidadMedico (IDMedico, DiaSemana, HoraInicio, HoraFin)
VALUES
(1,1,'08:00','12:00'),
(2,2,'08:00','12:00'),
(3,3,'08:00','12:00'),
(4,4,'08:00','12:00'),
(5,5,'08:00','12:00');
GO

-----------------------------------
-- HISTORIA CLINICA (opcional demo)
-----------------------------------
INSERT INTO HistoriaClinica (IDPaciente, IDMedico, IDTurno, Diagnostico, Tratamiento, Observaciones)
VALUES
(1,1,1,'Control general','Reposo','Sin complicaciones');
GO


