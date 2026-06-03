Use ClinicaDB
go

INSERT INTO Roles (Nombre)
VALUES
('Administrador'),
('Recepcionista'),
('Medico');
GO

INSERT INTO EstadoTurno (Nombre)
VALUES
('Pendiente'),
('Confirmado'),
('Cancelado'),
('Reprogramado'),
('No Asistio'),
('Finalizado');
GO

INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo)
VALUES
-- Administrador
('Administrador', 'AdminClinica', 'Administrador@clinica.com', '1122334455', 'admin', '123', 1, 1),

-- Recepcionistas
('Carla', 'Gomez', 'carla@clinica.com', '1145678910', 'cgomez', '123', 2, 1),
('Martin', 'Lopez', 'martin@clinica.com', '1156789123', 'mlopez', '123', 2, 1),
('Julieta', 'Mendez', 'jmendez@clinica.com', '1111111111', 'jmendez', '123', 2, 0),

-- Médicos (10 usuarios base)
('Juan', 'Perez', 'jperez@clinica.com', '1133344455', 'jperez', '123', 3, 1),
('Maria', 'Gonzalez', 'mgonzalez@clinica.com', '1166677788', 'mgonzalez', '123', 3, 1),
('Carlos', 'Diaz', 'cdiaz@clinica.com', '1177788899', 'cdiaz', '123', 3, 1),
('Ana', 'Fernandez', 'afernandez@clinica.com', '1188899900', 'afernandez', '123', 3, 1),
('Luis', 'Martinez', 'lmartinez@clinica.com', '1199900011', 'lmartinez', '123', 3, 1),
('Sofia', 'Ruiz', 'sruiz@clinica.com', '1122200033', 'sruiz', '123', 3, 1),
('Diego', 'Sanchez', 'dsanchez@clinica.com', '1133300044', 'dsanchez', '123', 3, 1),
('Valeria', 'Romero', 'vromero@clinica.com', '1144400055', 'vromero', '123', 3, 1),
('Pablo', 'Acosta', 'pacosta@clinica.com', '1155500066', 'pacosta', '123', 3, 1),
('Lucia', 'Herrera', 'lherrera@clinica.com', '1166600077', 'lherrera', '123', 3, 1),
('Ricardo', 'Suarez', 'rsuarez@clinica.com', '1111111112', 'rsuarez', '123', 3, 0),
('Gabriel','Torres','gtorres@clinica.com','1111111121','gtorres','123',3,1),
('Natalia','Silva','nsilva@clinica.com','1111111122','nsilva','123',3,1),
('Roberto','Molina','rmolina@clinica.com','1111111123','rmolina','123',3,0),
('Paula','Castro','pcastro@clinica.com','1111111124','pcastro','123',3,1),
('Javier','Benitez','jbenitez@clinica.com','1111111125','jbenitez','123',3,0);
GO

INSERT INTO Medicos (IDUsuario, Matricula, Activo)
VALUES
(4, 'MED-1001', 1),
(5, 'MED-1002', 1),
(6, 'MED-1003', 1),
(7, 'MED-1004', 1),
(8, 'MED-1005', 1),
(9, 'MED-1006', 1),
(10, 'MED-1007', 1),
(11, 'MED-1008', 1),
(12, 'MED-1009', 1),
(13, 'MED-1010', 1),
(14,'MED-1011',0),
(15,'MED-1012',1),
(16,'MED-1013',0),
(17,'MED-1014',1),
(18,'MED-1015',0);
GO


INSERT INTO Pacientes (Nombre, Apellido, DNI, FechaNacimiento, Email, Telefono, Direccion, ObraSocial, Activo)
VALUES
('Juan', 'García', '30111222', '1990-05-10', 'juan@gmail.com', '1122334455', 'Av. Corrientes 123', 'OSDE', 1),
('María', 'Rodriguez', '28999111', '1985-03-21', 'maria@gmail.com', '1133445566', 'San Martín 456', 'Swiss Medical', 1),
('Carlos', 'Lopez', '32123456', '1992-08-15', 'carlos@gmail.com', '1144556677', 'Belgrano 789', 'PAMI', 1),
('Lucia', 'Fernandez', '30199887', '1995-01-12', 'lucia@gmail.com', '1155667788', 'Cabildo 321', 'OSDE', 1),
('Diego', 'Martinez', '27888999', '1980-11-05', 'diego@gmail.com', '1166778899', 'La Plata 654', 'Galeno', 1),
('Sofia', 'Perez', '30987654', '1993-07-22', 'sofia@gmail.com', '1177889900', 'Quilmes 111', 'OSDE', 1),
('Pedro', 'Gomez', '29888777', '1988-12-30', 'pedro@gmail.com', '1188990011', 'Lomas 222', 'Swiss Medical', 1),
('Ana', 'Diaz', '31555111', '1991-09-14', 'ana@gmail.com', '1199001122', 'Avellaneda 333', 'PAMI', 1),
('Martin', 'Sanchez', '32777111', '1996-04-18', 'martin@gmail.com', '1122112233', 'Morón 444', 'OSDE', 1),
('Valeria', 'Romero', '30123498', '1987-06-25', 'valeria@gmail.com', '1133223344', 'San Isidro 555', 'Galeno', 1),
('Pablo', 'Acosta', '29555123', '1983-02-10', 'pablo@gmail.com', '1144334455', 'Lanús 666', 'OSDE', 1),
('Laura', 'Herrera', '30222444', '1994-10-05', 'laura@gmail.com', '1155445566', 'Tigre 777', 'Swiss Medical', 1),
('Fernando', 'Alvarez', '28777111', '1982-03-17', 'fernando@gmail.com', '1166556677', 'CABA 888', 'PAMI', 1),
('Camila', 'Rojas', '31999111', '1997-01-29', 'camila@gmail.com', '1177667788', 'San Justo 999', 'OSDE', 1),
('Nicolas', 'Vargas', '30000999', '1990-12-12', 'nicolas@gmail.com', '1188778899', 'Flores 101', 'Galeno', 1),
('Sebastian','Nuñez','31222333','1989-04-11','sebastian@gmail.com','1111111111','CABA 100','OSDE',1),
('Patricia','Morales','29888766','1979-02-20','patricia@gmail.com','1111111112','CABA 101','Galeno',1),
('Emiliano','Cabrera','32555444','1998-07-07','emiliano@gmail.com','1111111113','CABA 102','PAMI',0),
('Micaela','Vega','33444555','1995-09-18','micaela@gmail.com','1111111114','CABA 103','Swiss Medical',1),
('Hector','Paz','27777123','1975-01-15','hector@gmail.com','1111111115','CABA 104','OSDE',0);
GO 

INSERT INTO Especialidades (Nombre, Descripcion, Activo)
VALUES
('Cardiología', 'Corazón y sistema cardiovascular', 1),
('Clínica Médica', 'Medicina general', 1),
('Pediatría', 'Atención infantil', 1),
('Traumatología', 'Huesos y lesiones', 1),
('Neurología', 'Sistema nervioso', 1),
('Dermatología', 'Piel y afecciones cutáneas', 1),
('Oftalmología', 'Vista y ojos', 1);

INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad)
VALUES
(1,1),
(2,2),
(3,3),
(4,4),
(7,7),
(8,1),
(9,2),
(10,3),
(5,5),
(6,6),
(11,4),
(12,5),
(13,6),
(14,7),
(15,1);

INSERT INTO Turnos (Codigo, IDPaciente, IDMedico, FechaHora, IDEstadoTurno)
VALUES
('T-001', 1, 1, '2026-06-01 09:00', 1), 
('T-002', 2, 2, '2026-06-01 10:00', 2),
('T-003', 3, 3, '2026-06-01 11:00', 1),
('T-004', 4, 4, '2026-06-01 12:00', 3), 
('T-005', 5, 5, '2026-06-01 13:00', 1),
('T-006', 6, 6, '2026-06-01 14:00', 2),
('T-007', 7, 7, '2026-06-01 15:00', 1),
('T-008', 8, 8, '2026-06-01 16:00', 4), 
('T-009', 9, 9, '2026-06-01 17:00', 1),
('T-010',10,10, '2026-06-01 18:00', 2),
('T-011', 11, 1, '2026-06-02 09:00', 5), 
('T-012', 12, 2, '2026-06-02 10:00', 6);
GO


INSERT INTO DisponibilidadMedico
(IDMedico, DiaSemana, HoraInicio, HoraFin)
VALUES
(1,1,'08:00','12:00'),
(1,3,'08:00','12:00'),

(2,2,'09:00','13:00'),
(2,4,'09:00','13:00'),

(3,1,'14:00','18:00'),
(3,5,'14:00','18:00'),

(4,2,'08:00','12:00'),
(5,3,'08:00','12:00'),
(6,4,'08:00','12:00'),
(7,5,'08:00','12:00'),
(8,1,'15:00','19:00'),
(9,2,'15:00','19:00'),
(10,3,'15:00','19:00'),
(11,1,'08:00','12:00'),
(12,2,'08:00','12:00'),
(13,3,'08:00','12:00'),
(14,4,'14:00','18:00'),
(15,5,'14:00','18:00');


