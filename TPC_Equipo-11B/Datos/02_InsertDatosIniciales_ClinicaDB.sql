USE ClinicaDB;
GO

-- Archivo de datos demo ampliado para ClinicaDB

INSERT INTO Roles (Nombre) VALUES ('Administrador'),('Recepcionista'),('Medico'),('Paciente');
GO

INSERT INTO Generos (Descripcion) VALUES ('Masculino'),('Femenino'),('Otro');
GO

INSERT INTO ObrasSociales (Nombre,TipoPlan) VALUES
('OSDE','Basico'),
('OSDE','Intermedio'),
('OSDE','Premium'),
('Swiss Medical','Basico'),
('Swiss Medical','Intermedio'),
('Swiss Medical','Premium'),
('Galeno','Basico'),
('Galeno','Intermedio'),
('Galeno','Premium'),
('Medife','Basico'),
('Medife','Intermedio'),
('Medife','Premium'),
('PAMI','Basico'),
('PAMI','Intermedio'),
('PAMI','Premium'),
('IOMA','Basico'),
('IOMA','Intermedio'),
('IOMA','Premium'),
('Particular','Sin Cobertura');
GO

INSERT INTO Usuarios (Nombre,Apellido,Email,Telefono,Username,PasswordHash,ImagenUrl,IDRol) VALUES
('Admin','Sistema','admin@clinica.com','1100000001','admin','1234',NULL,1),
('Carla','Gomez','cgomez@clinica.com','1100000002','cgomez','1234',NULL,2),
('Martin','Lopez','mlopez@clinica.com','1100000003','mlopez','1234',NULL,2),
('Juan','Perez','jperez@clinica.com','1100000004','jperez','1234',NULL,3),
('Maria','Gonzalez','mgonzalez@clinica.com','1100000005','mgonzalez','1234',NULL,3),
('Carlos','Diaz','cdiaz@clinica.com','1100000006','cdiaz','1234',NULL,3),
('Ana','Fernandez','afernandez@clinica.com','1100000007','afernandez','1234',NULL,3),
('Luis','Martinez','lmartinez@clinica.com','1100000008','lmartinez','1234',NULL,3),
('Lucia','Romero','lromero@clinica.com','1100000009','lromero','1234',NULL,3),
('Sofia','Ruiz','sruiz@clinica.com','1100000010','sruiz','1234',NULL,4),
('Diego','Sanchez','dsanchez@clinica.com','1100000011','dsanchez','1234',NULL,4),
('Valentina','Torres','vtorres@clinica.com','1100000012','vtorres','1234',NULL,4),
('Mateo','Herrera','mherrera@clinica.com','1100000013','mherrera','1234',NULL,4),
('Camila','Castro','ccastro@clinica.com','1100000014','ccastro','1234',NULL,4),
('Nicolas','Vega','nvega@clinica.com','1100000015','nvega','1234',NULL,4),
('Julieta','Silva','jsilva@clinica.com','1100000016','jsilva','1234',NULL,4),
('Federico','Acosta','facosta@clinica.com','1100000017','facosta','1234',NULL,4),
('Paula','Benitez','pbenitez@clinica.com','1100000018','pbenitez','1234',NULL,4),
('Agustin','Suarez','asuarez@clinica.com','1100000019','asuarez','1234',NULL,4),
('Florencia','Mendez','fmendez@clinica.com','1100000020','fmendez','1234',NULL,4),
('Bruno','Ortiz','bortiz@clinica.com','1100000021','bortiz','1234',NULL,4),
('Lara','Rios','lrios@clinica.com','1100000022','lrios','1234',NULL,4),
('Tomas','Nuñez','tnuñez@clinica.com','1100000023','tnuñez','1234',NULL,4);
GO

INSERT INTO Medicos (IDUsuario,Matricula) VALUES
(4,'MED-1001'),
(5,'MED-1002'),
(6,'MED-1003'),
(7,'MED-1004'),
(8,'MED-1005'),
(9,'MED-1006');
GO

INSERT INTO Pacientes (IDUsuario,DNI,FechaNacimiento,Direccion,IDObraSocial,IDGenero) VALUES
(10,'30000000','1990-01-10','Calle 100',1,1),
(11,'30011111','1991-02-11','Calle 101',2,2),
(12,'30022222','1992-03-12','Calle 102',3,1),
(13,'30033333','1993-04-13','Calle 103',4,2),
(14,'30044444','1994-05-14','Calle 104',5,1),
(15,'30055555','1995-06-15','Calle 105',6,2),
(16,'30066666','1996-07-16','Calle 106',7,1),
(17,'30077777','1997-08-17','Calle 107',8,2),
(18,'30088888','1998-09-18','Calle 108',9,1),
(19,'30099999','1999-01-10','Calle 109',10,2),
(20,'30111110','1990-02-11','Calle 110',11,1),
(21,'30122221','1991-03-12','Calle 111',12,2),
(22,'30133332','1992-04-13','Calle 112',13,1),
(23,'30144443','1993-05-14','Calle 113',14,2);
GO

INSERT INTO Especialidades (Nombre,Descripcion) VALUES
('Cardiologia','Cardiologia'),
('Clinica Medica','Clinica Medica'),
('Pediatria','Pediatria'),
('Traumatologia','Traumatologia'),
('Neurologia','Neurologia'),
('Dermatologia','Dermatologia'),
('Oftalmologia','Oftalmologia'),
('Otorrinolaringologia','Otorrinolaringologia'),
('Ginecologia','Ginecologia'),
('Urologia','Urologia'),
('Endocrinologia','Endocrinologia'),
('Neumonologia','Neumonologia'),
('Reumatologia','Reumatologia'),
('Psiquiatria','Psiquiatria'),
('Nutricion','Nutricion');
GO

INSERT INTO MedicoEspecialidad (IDMedico,IDEspecialidad) VALUES (1,1),(2,2),(3,3),(4,4),(5,5),(6,6);
GO

INSERT INTO MedicoObraSocial (IDMedico,IDObraSocial) VALUES
(1,1),
(1,2),
(1,3),
(1,4),
(1,5),
(2,4),
(2,5),
(2,6),
(2,7),
(2,8),
(3,7),
(3,8),
(3,9),
(3,10),
(3,11),
(4,10),
(4,11),
(4,12),
(4,13),
(4,14),
(5,13),
(5,14),
(5,15),
(5,16),
(5,17),
(6,16),
(6,17),
(6,18),
(6,19),
(6,1);
GO

INSERT INTO DisponibilidadMedico (IDMedico,DiaSemana,HoraInicio,HoraFin) VALUES
(1,1,'08:00','12:00'),
(1,2,'14:00','18:00'),
(1,3,'08:00','12:00'),
(1,5,'14:00','18:00'),
(2,1,'08:00','12:00'),
(2,2,'14:00','18:00'),
(2,3,'08:00','12:00'),
(2,5,'14:00','18:00'),
(3,1,'08:00','12:00'),
(3,2,'14:00','18:00'),
(3,3,'08:00','12:00'),
(3,5,'14:00','18:00'),
(4,1,'08:00','12:00'),
(4,2,'14:00','18:00'),
(4,3,'08:00','12:00'),
(4,5,'14:00','18:00'),
(5,1,'08:00','12:00'),
(5,2,'14:00','18:00'),
(5,3,'08:00','12:00'),
(5,5,'14:00','18:00'),
(6,1,'08:00','12:00'),
(6,2,'14:00','18:00'),
(6,3,'08:00','12:00'),
(6,5,'14:00','18:00');
GO

INSERT INTO AusenciasMedico (IDMedico,Fecha,Motivo) VALUES
(1,'2026-08-01','Capacitacion'),
(2,'2026-08-02','Capacitacion'),
(3,'2026-08-03','Capacitacion'),
(4,'2026-08-04','Capacitacion'),
(5,'2026-08-05','Capacitacion'),
(6,'2026-08-06','Capacitacion'),
(1,'2026-08-07','Capacitacion'),
(2,'2026-08-08','Capacitacion'),
(3,'2026-08-09','Capacitacion'),
(4,'2026-08-10','Capacitacion'),
(5,'2026-08-11','Capacitacion'),
(6,'2026-08-12','Capacitacion');
GO

INSERT INTO EstadoTurno (Nombre) VALUES ('Pendiente'),('Confirmado'),('Cancelado'),('Finalizado'), ('No asistió'), ('Reprogramado');
GO

INSERT INTO Turnos (Codigo,IDPaciente,IDMedico,FechaHora,IDEstadoTurno,IDEspecialidad) VALUES
('T0415823',1,1,'2026-09-01 09:00',1,1),
('T1937461',2,2,'2026-09-02 15:00',2,2),
('T5829134',3,3,'2026-09-03 09:00',3,3),
('T0746358',4,4,'2026-09-04 15:00',4,4),
('T3164827',5,5,'2026-09-05 09:00',1,5),
('T8519472',6,6,'2026-09-06 15:00',2,6),
('T1275638',7,1,'2026-09-07 09:00',3,1),
('T6642915',8,2,'2026-09-08 15:00',4,2),
('T2458716',9,3,'2026-09-09 09:00',1,3),
('T9183425',10,4,'2026-09-10 15:00',2,4),
('T5361749',11,5,'2026-09-11 09:00',3,5),
('T1028457',12,6,'2026-09-12 15:00',4,6),
('T7834196',13,1,'2026-09-13 09:00',1,1),
('T2956481',14,2,'2026-09-14 15:00',2,2),
('T4671935',1,3,'2026-09-15 09:00',3,3),
('T8592714',2,4,'2026-09-16 15:00',4,4),
('T1386247',3,5,'2026-09-17 09:00',1,5),
('T6049812',4,6,'2026-09-18 15:00',2,6),
('T3718259',5,1,'2026-09-19 09:00',3,1),
('T9471368',6,2,'2026-09-20 15:00',4,2),
('T2865743',7,3,'2026-09-01 09:00',1,3),
('T7159284',8,4,'2026-09-02 15:00',2,4),
('T4528631',9,5,'2026-09-03 09:00',3,5),
('T8231475',10,6,'2026-09-04 15:00',4,6),
('T1647582',11,1,'2026-09-05 09:00',1,1),
('T5984217',12,2,'2026-09-06 15:00',2,2),
('T2769138',13,3,'2026-09-07 09:00',3,3),
('T9045176',14,4,'2026-09-08 15:00',4,4),
('T4312865',1,5,'2026-09-09 09:00',1,5),
('T6871542',2,6,'2026-09-10 15:00',2,6),
('T1587436',3,1,'2026-09-11 09:00',3,1),
('T7924681',4,2,'2026-09-12 15:00',4,2),
('T3458197',5,3,'2026-09-13 09:00',1,3),
('T6182754',6,4,'2026-09-14 15:00',2,4),
('T2396418',7,5,'2026-09-15 09:00',3,5),
('T8753246',8,6,'2026-09-16 15:00',4,6),
('T4819573',9,1,'2026-09-17 09:00',1,1),
('T1263845',10,2,'2026-09-18 15:00',2,2),
('T7546189',11,3,'2026-09-19 09:00',3,3),
('T3927154',12,4,'2026-09-20 15:00',4,4);
GO
INSERT INTO HistoriaClinica (IDPaciente,IDMedico,IDTurno,Diagnostico,Tratamiento,Observaciones) VALUES
(1,1,1,'Diagnostico 1','Tratamiento 1','Observacion 1'),
(2,2,2,'Diagnostico 2','Tratamiento 2','Observacion 2'),
(3,3,3,'Diagnostico 3','Tratamiento 3','Observacion 3'),
(4,4,4,'Diagnostico 4','Tratamiento 4','Observacion 4'),
(5,5,5,'Diagnostico 5','Tratamiento 5','Observacion 5'),
(6,6,6,'Diagnostico 6','Tratamiento 6','Observacion 6'),
(7,1,7,'Diagnostico 7','Tratamiento 7','Observacion 7'),
(8,2,8,'Diagnostico 8','Tratamiento 8','Observacion 8'),
(9,3,9,'Diagnostico 9','Tratamiento 9','Observacion 9'),
(10,4,10,'Diagnostico 10','Tratamiento 10','Observacion 10'),
(11,5,11,'Diagnostico 11','Tratamiento 11','Observacion 11'),
(12,6,12,'Diagnostico 12','Tratamiento 12','Observacion 12'),
(13,1,13,'Diagnostico 13','Tratamiento 13','Observacion 13'),
(14,2,14,'Diagnostico 14','Tratamiento 14','Observacion 14'),
(1,3,15,'Diagnostico 15','Tratamiento 15','Observacion 15'),
(2,4,16,'Diagnostico 16','Tratamiento 16','Observacion 16'),
(3,5,17,'Diagnostico 17','Tratamiento 17','Observacion 17'),
(4,6,18,'Diagnostico 18','Tratamiento 18','Observacion 18'),
(5,1,19,'Diagnostico 19','Tratamiento 19','Observacion 19'),
(6,2,20,'Diagnostico 20','Tratamiento 20','Observacion 20'),
(7,3,NULL,'Diagnostico 21','Tratamiento 21','Observacion 21'),
(8,4,NULL,'Diagnostico 22','Tratamiento 22','Observacion 22'),
(9,5,NULL,'Diagnostico 23','Tratamiento 23','Observacion 23'),
(10,6,NULL,'Diagnostico 24','Tratamiento 24','Observacion 24'),
(11,1,NULL,'Diagnostico 25','Tratamiento 25','Observacion 25');
GO



-- Turnos del día (dashboard)
INSERT INTO Turnos
(Codigo,IDPaciente,IDMedico,FechaHora,IDEstadoTurno,IDEspecialidad)
VALUES
('T-J-001',1,1,CAST(CONVERT(date,GETDATE()) AS datetime)+'09:00:00',2,1),
('T-J-002',2,2,CAST(CONVERT(date,GETDATE()) AS datetime)+'10:00:00',1,2),
('T-J-003',3,3,CAST(CONVERT(date,GETDATE()) AS datetime)+'11:00:00',4,3),
('T-J-004',4,4,CAST(CONVERT(date,GETDATE()) AS datetime)+'14:00:00',2,4),
('T-J-005',5,5,CAST(CONVERT(date,GETDATE()) AS datetime)+'16:00:00',1,5);
GO

INSERT INTO HistoriaClinica
(IDPaciente,IDMedico,IDTurno,Diagnostico,Tratamiento,Observaciones)
VALUES
(3,3,43,'Control general','Sin tratamiento','Paciente estable');
GO