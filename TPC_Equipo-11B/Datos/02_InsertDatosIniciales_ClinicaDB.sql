USE ClinicaDB;
GO


INSERT INTO Roles (Nombre) VALUES ('Administrador'),('Recepcionista'),('Medico'),('Paciente');
GO

INSERT INTO Generos (Descripcion) VALUES ('Masculino'),('Femenino'),('Otro');
GO

INSERT INTO EstadoTurno (Nombre) VALUES ('Pendiente'),('Confirmado'),('Cancelado'),('Finalizado'), ('No asistió'), ('Reprogramado');
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


SET NOCOUNT ON;
GO

------------------------------------------------------------
-- USUARIOS
------------------------------------------------------------

INSERT INTO Usuarios
    (Nombre, Apellido, Email, Telefono, Username, PasswordHash, ImagenUrl, IDRol)
SELECT v.Nombre, v.Apellido, v.Email, v.Telefono, v.Username, v.PasswordHash, v.ImagenUrl, r.IDRol
FROM (VALUES
    ('Admin','Sistema','admin@clinica.com','1100000001','admin','1234','https://randomuser.me/api/portraits/men/1.jpg','Administrador'),
   
   ('Carla','Gomez','cgomez@clinica.com','1100000002','cgomez','1234','https://randomuser.me/api/portraits/women/12.jpg','Recepcionista'),
    ('Martin','Lopez','mlopez@clinica.com','1100000003','mlopez','1234','https://randomuser.me/api/portraits/men/13.jpg','Recepcionista'),
    ('Natalia','Paz','npaz@clinica.com','1100000004','npaz','1234','https://randomuser.me/api/portraits/women/14.jpg','Recepcionista'),

    ('Juan','Perez','jperez.med@clinica.com','1100000101','jperez.med','1234','https://randomuser.me/api/portraits/men/21.jpg','Medico'),
    ('Maria','Gonzalez','mgonzalez.med@clinica.com','1100000102','mgonzalez.med','1234','https://randomuser.me/api/portraits/women/22.jpg','Medico'),
    ('Carlos','Diaz','cdiaz.med@clinica.com','1100000103','cdiaz.med','1234','https://randomuser.me/api/portraits/men/23.jpg','Medico'),
    ('Ana','Fernandez','afernandez.med@clinica.com','1100000104','afernandez.med','1234','https://randomuser.me/api/portraits/women/24.jpg','Medico'),
    ('Luis','Martinez','lmartinez.med@clinica.com','1100000105','lmartinez.med','1234','https://randomuser.me/api/portraits/men/25.jpg','Medico'),
    ('Lucia','Romero','lromero.med@clinica.com','1100000106','lromero.med','1234','https://randomuser.me/api/portraits/women/26.jpg','Medico'),
    ('Ricardo','Alvarez','ralvarez.med@clinica.com','1100000107','ralvarez.med','1234','https://randomuser.me/api/portraits/men/27.jpg','Medico'),
    ('Paula','Mendez','pmendez.med@clinica.com','1100000108','pmendez.med','1234','https://randomuser.me/api/portraits/women/28.jpg','Medico'),
    ('Santiago','Molina','smolina.med@clinica.com','1100000109','smolina.med','1234','https://randomuser.me/api/portraits/men/29.jpg','Medico'),
    ('Valeria','Suarez','vsuarez.med@clinica.com','1100000110','vsuarez.med','1234','https://randomuser.me/api/portraits/women/30.jpg','Medico'),
    ('Federico','Costa','fcosta.med@clinica.com','1100000111','fcosta.med','1234','https://randomuser.me/api/portraits/men/31.jpg','Medico'),
    ('Elena','Rivas','erivas.med@clinica.com','1100000112','erivas.med','1234','https://randomuser.me/api/portraits/women/32.jpg','Medico'),
    ('Hector','Salas','hsalas.med@clinica.com','1100000113','hsalas.med','1234','https://randomuser.me/api/portraits/men/33.jpg','Medico'),
    ('Marina','Vega','mvega.med@clinica.com','1100000114','mvega.med','1234','https://randomuser.me/api/portraits/women/34.jpg','Medico'),
    ('Andres','Ibarra','aibarra.med@clinica.com','1100000115','aibarra.med','1234','https://randomuser.me/api/portraits/men/35.jpg','Medico'),

    ('Sofia','Ruiz','sruiz@pacientes.com','1100000201','sruiz','1234','https://randomuser.me/api/portraits/women/41.jpg','Paciente'),
    ('Diego','Sanchez','dsanchez@pacientes.com','1100000202','dsanchez','1234','https://randomuser.me/api/portraits/men/42.jpg','Paciente'),
    ('Valentina','Torres','vtorres@pacientes.com','1100000203','vtorres','1234','https://randomuser.me/api/portraits/women/43.jpg','Paciente'),
    ('Mateo','Herrera','mherrera@pacientes.com','1100000204','mherrera','1234','https://randomuser.me/api/portraits/men/44.jpg','Paciente'),
    ('Camila','Castro','ccastro@pacientes.com','1100000205','ccastro','1234','https://randomuser.me/api/portraits/women/45.jpg','Paciente'),
    ('Nicolas','Vega','nvega@pacientes.com','1100000206','nvega','1234','https://randomuser.me/api/portraits/men/46.jpg','Paciente'),
    ('Julieta','Silva','jsilva@pacientes.com','1100000207','jsilva','1234','https://randomuser.me/api/portraits/women/47.jpg','Paciente'),
    ('Federico','Acosta','facosta@pacientes.com','1100000208','facosta','1234','https://randomuser.me/api/portraits/men/48.jpg','Paciente'),
    ('Paula','Benitez','pbenitez@pacientes.com','1100000209','pbenitez','1234','https://randomuser.me/api/portraits/women/49.jpg','Paciente'),
    ('Agustin','Suarez','asuarez@pacientes.com','1100000210','asuarez','1234','https://randomuser.me/api/portraits/men/50.jpg','Paciente'),
    ('Florencia','Mendez','fmendez@pacientes.com','1100000211','fmendez','1234','https://randomuser.me/api/portraits/women/51.jpg','Paciente'),
    ('Bruno','Ortiz','bortiz@pacientes.com','1100000212','bortiz','1234','https://randomuser.me/api/portraits/men/52.jpg','Paciente'),
    ('Lara','Rios','lrios@pacientes.com','1100000213','lrios','1234','https://randomuser.me/api/portraits/women/53.jpg','Paciente'),
    ('Tomas','Nunez','tnunez@pacientes.com','1100000214','tnunez','1234','https://randomuser.me/api/portraits/men/54.jpg','Paciente'),
    ('Milagros','Campos','mcampos@pacientes.com','1100000215','mcampos','1234','https://randomuser.me/api/portraits/women/55.jpg','Paciente'),
    ('Rafael','Moreno','rmoreno@pacientes.com','1100000216','rmoreno','1234','https://randomuser.me/api/portraits/men/56.jpg','Paciente'),
    ('Belen','Sosa','bsosa@pacientes.com','1100000217','bsosa','1234','https://randomuser.me/api/portraits/women/57.jpg','Paciente'),
    ('Ivan','Luna','iluna@pacientes.com','1100000218','iluna','1234','https://randomuser.me/api/portraits/men/58.jpg','Paciente'),
    ('Micaela','Ponce','mponce@pacientes.com','1100000219','mponce','1234','https://randomuser.me/api/portraits/women/59.jpg','Paciente'),
    ('Ezequiel','Navarro','enavarro@pacientes.com','1100000220','enavarro','1234','https://randomuser.me/api/portraits/men/60.jpg','Paciente'),
    ('Renata','Arias','rarias@pacientes.com','1100000221','rarias','1234','https://randomuser.me/api/portraits/women/61.jpg','Paciente'),
    ('Gaston','Peralta','gperalta@pacientes.com','1100000222','gperalta','1234','https://randomuser.me/api/portraits/men/62.jpg','Paciente'),
    ('Noelia','Farias','nfarias@pacientes.com','1100000223','nfarias','1234','https://randomuser.me/api/portraits/women/63.jpg','Paciente'),
    ('Sebastian','Cabrera','scabrera@pacientes.com','1100000224','scabrera','1234','https://randomuser.me/api/portraits/men/64.jpg','Paciente'),
    ('Abril','Medina','amedina@pacientes.com','1100000225','amedina','1234','https://randomuser.me/api/portraits/women/65.jpg','Paciente'),
    ('Lucas','Correa','lcorrea@pacientes.com','1100000226','lcorrea','1234','https://randomuser.me/api/portraits/men/66.jpg','Paciente'),
    ('Rocio','Paredes','rparedes@pacientes.com','1100000227','rparedes','1234','https://randomuser.me/api/portraits/women/67.jpg','Paciente'),
    ('Emiliano','Bravo','ebravo@pacientes.com','1100000228','ebravo','1234','https://randomuser.me/api/portraits/men/68.jpg','Paciente'),
    ('Aldana','Serrano','aserrano@pacientes.com','1100000229','aserrano','1234','https://randomuser.me/api/portraits/women/69.jpg','Paciente'),
    ('Joaquin','Mansilla','jmansilla@pacientes.com','1100000230','jmansilla','1234','https://randomuser.me/api/portraits/men/70.jpg','Paciente')
) AS v(Nombre, Apellido, Email, Telefono, Username, PasswordHash, ImagenUrl, Rol)
JOIN Roles r ON r.Nombre = v.Rol
WHERE NOT EXISTS (SELECT 1 FROM Usuarios u WHERE u.Username = v.Username);
GO

------------------------------------------------------------
-- MEDICOS
------------------------------------------------------------

INSERT INTO Medicos (IDUsuario, IDEspecialidad, Matricula)
SELECT u.IDUsuario, e.IDEspecialidad, v.Matricula
FROM (VALUES
    ('jperez.med','Cardiologia','MED-1001'),
    ('mgonzalez.med','Dermatologia','MED-1002'),
    ('cdiaz.med','Oftalmologia','MED-1003'),
    ('afernandez.med','Clinica Medica','MED-1004'),
    ('lmartinez.med','Otorrinolaringologia','MED-1005'),
    ('lromero.med','Pediatria','MED-1006'),
    ('ralvarez.med','Traumatologia','MED-1007'),
    ('pmendez.med','Ginecologia','MED-1008'),
    ('smolina.med','Urologia','MED-1009'),
    ('vsuarez.med','Endocrinologia','MED-1010'),
    ('fcosta.med','Neurologia','MED-1011'),
    ('erivas.med','Neumonologia','MED-1012'),
    ('hsalas.med','Reumatologia','MED-1013'),
    ('mvega.med','Psiquiatria','MED-1014'),
    ('aibarra.med','Nutricion','MED-1015')
) AS v(Username, Especialidad, Matricula)
JOIN Usuarios u ON u.Username = v.Username
JOIN Especialidades e ON e.Nombre = v.Especialidad
WHERE NOT EXISTS (SELECT 1 FROM Medicos m WHERE m.Matricula = v.Matricula);
GO

------------------------------------------------------------
-- PACIENTES
------------------------------------------------------------

INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero)
SELECT u.IDUsuario, v.DNI, v.FechaNacimiento, v.Direccion, os.IDObraSocial, g.IDGenero
FROM (VALUES
    ('sruiz','30000000','1990-01-10','Av. Rivadavia 1200','OSDE','Basico','Femenino'),
    ('dsanchez','30011111','1988-02-11','Calle Mitre 101','Swiss Medical','Intermedio','Masculino'),
    ('vtorres','30022222','1992-03-12','Av. Corrientes 2345','Galeno','Premium','Femenino'),
    ('mherrera','30033333','1985-04-13','Calle Belgrano 345','Medife','Basico','Masculino'),
    ('ccastro','30044444','1994-05-14','Av. Santa Fe 4567','PAMI','Intermedio','Femenino'),
    ('nvega','30055555','1981-06-15','Calle San Martin 567','IOMA','Premium','Masculino'),
    ('jsilva','30066666','1996-07-16','Av. Las Heras 678','Particular','Sin Cobertura','Femenino'),
    ('facosta','30077777','1979-08-17','Calle Moreno 789','OSDE','Premium','Masculino'),
    ('pbenitez','30088888','1998-09-18','Av. Maipu 890','Swiss Medical','Basico','Femenino'),
    ('asuarez','30099999','1987-10-19','Calle Sarmiento 901','Galeno','Intermedio','Masculino'),
    ('fmendez','30111110','1991-11-20','Av. Cordoba 1111','Medife','Premium','Femenino'),
    ('bortiz','30122221','1975-12-21','Calle Alsina 1212','PAMI','Basico','Masculino'),
    ('lrios','30133332','1993-01-22','Av. Callao 1313','IOMA','Intermedio','Femenino'),
    ('tnunez','30144443','1984-02-23','Calle Peru 1414','OSDE','Intermedio','Masculino'),
    ('mcampos','30155554','2000-03-24','Av. Colon 1515','Swiss Medical','Premium','Femenino'),
    ('rmoreno','30166665','1972-04-25','Calle Chile 1616','Galeno','Basico','Masculino'),
    ('bsosa','30177776','1997-05-26','Av. Libertador 1717','Medife','Intermedio','Femenino'),
    ('iluna','30188887','1989-06-27','Calle Uruguay 1818','PAMI','Premium','Masculino'),
    ('mponce','30199998','1995-07-28','Av. Independencia 1919','IOMA','Basico','Femenino'),
    ('enavarro','30211109','1982-08-29','Calle Jujuy 2020','Particular','Sin Cobertura','Masculino'),
    ('rarias','30222210','2001-09-30','Av. San Juan 2121','OSDE','Basico','Femenino'),
    ('gperalta','30233321','1978-10-01','Calle Mexico 2222','Swiss Medical','Intermedio','Masculino'),
    ('nfarias','30244432','1986-11-02','Av. Entre Rios 2323','Galeno','Premium','Femenino'),
    ('scabrera','30255543','1999-12-03','Calle Brasil 2424','Medife','Basico','Masculino'),
    ('amedina','30266654','1990-01-04','Av. Pueyrredon 2525','PAMI','Intermedio','Femenino'),
    ('lcorrea','30277765','1974-02-05','Calle Ecuador 2626','IOMA','Premium','Masculino'),
    ('rparedes','30288876','1992-03-06','Av. Congreso 2727','OSDE','Premium','Femenino'),
    ('ebravo','30299987','1983-04-07','Calle Venezuela 2828','Swiss Medical','Basico','Masculino'),
    ('aserrano','30311198','1996-05-08','Av. Boedo 2929','Galeno','Intermedio','Femenino'),
    ('jmansilla','30322209','1980-06-09','Calle Defensa 3030','Medife','Premium','Masculino')
) AS v(Username, DNI, FechaNacimiento, Direccion, ObraSocial, TipoPlan, Genero)
JOIN Usuarios u ON u.Username = v.Username
JOIN ObrasSociales os ON os.Nombre = v.ObraSocial AND os.TipoPlan = v.TipoPlan
JOIN Generos g ON g.Descripcion = v.Genero
WHERE NOT EXISTS (SELECT 1 FROM Pacientes p WHERE p.DNI = v.DNI);
GO

------------------------------------------------------------
-- MEDICO - OBRA SOCIAL
------------------------------------------------------------

INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial)
SELECT m.IDMedico, os.IDObraSocial
FROM (VALUES
    ('MED-1001','OSDE','Basico'),('MED-1001','OSDE','Intermedio'),('MED-1001','OSDE','Premium'),('MED-1001','Particular','Sin Cobertura'),
    ('MED-1002','Swiss Medical','Basico'),('MED-1002','Swiss Medical','Intermedio'),('MED-1002','Swiss Medical','Premium'),('MED-1002','Particular','Sin Cobertura'),
    ('MED-1003','Galeno','Basico'),('MED-1003','Galeno','Intermedio'),('MED-1003','Galeno','Premium'),('MED-1003','Particular','Sin Cobertura'),
    ('MED-1004','Medife','Basico'),('MED-1004','Medife','Intermedio'),('MED-1004','Medife','Premium'),('MED-1004','Particular','Sin Cobertura'),
    ('MED-1005','PAMI','Basico'),('MED-1005','PAMI','Intermedio'),('MED-1005','PAMI','Premium'),('MED-1005','Particular','Sin Cobertura'),
    ('MED-1006','IOMA','Basico'),('MED-1006','IOMA','Intermedio'),('MED-1006','IOMA','Premium'),('MED-1006','Particular','Sin Cobertura'),
    ('MED-1007','OSDE','Basico'),('MED-1007','Swiss Medical','Basico'),('MED-1007','Galeno','Basico'),('MED-1007','Particular','Sin Cobertura'),
    ('MED-1008','OSDE','Intermedio'),('MED-1008','Swiss Medical','Intermedio'),('MED-1008','Medife','Intermedio'),('MED-1008','Particular','Sin Cobertura'),
    ('MED-1009','Galeno','Premium'),('MED-1009','Medife','Premium'),('MED-1009','IOMA','Premium'),('MED-1009','Particular','Sin Cobertura'),
    ('MED-1010','PAMI','Basico'),('MED-1010','IOMA','Basico'),('MED-1010','OSDE','Basico'),('MED-1010','Particular','Sin Cobertura'),
    ('MED-1011','Swiss Medical','Premium'),('MED-1011','Galeno','Premium'),('MED-1011','OSDE','Premium'),('MED-1011','Particular','Sin Cobertura'),
    ('MED-1012','PAMI','Intermedio'),('MED-1012','IOMA','Intermedio'),('MED-1012','Medife','Intermedio'),('MED-1012','Particular','Sin Cobertura'),
    ('MED-1013','OSDE','Premium'),('MED-1013','Swiss Medical','Premium'),('MED-1013','IOMA','Premium'),('MED-1013','Particular','Sin Cobertura'),
    ('MED-1014','Galeno','Intermedio'),('MED-1014','Medife','Intermedio'),('MED-1014','Swiss Medical','Intermedio'),('MED-1014','Particular','Sin Cobertura'),
    ('MED-1015','OSDE','Basico'),('MED-1015','PAMI','Basico'),('MED-1015','IOMA','Basico'),('MED-1015','Particular','Sin Cobertura')
) AS v(Matricula, ObraSocial, TipoPlan)
JOIN Medicos m ON m.Matricula = v.Matricula
JOIN ObrasSociales os ON os.Nombre = v.ObraSocial AND os.TipoPlan = v.TipoPlan
WHERE NOT EXISTS (
    SELECT 1
    FROM MedicoObraSocial mos
    WHERE mos.IDMedico = m.IDMedico AND mos.IDObraSocial = os.IDObraSocial
);
GO

------------------------------------------------------------
-- DISPONIBILIDAD MEDICA
------------------------------------------------------------

INSERT INTO DisponibilidadMedico (IDMedico, DiaSemana, HoraInicio, HoraFin)
SELECT m.IDMedico, v.DiaSemana, CAST(v.HoraInicio AS TIME), CAST(v.HoraFin AS TIME)
FROM (VALUES
    ('MED-1001',1,'08:00','12:00'),('MED-1001',3,'08:00','12:00'),('MED-1001',5,'14:00','18:00'),
    ('MED-1002',2,'09:00','13:00'),('MED-1002',4,'09:00','13:00'),('MED-1002',6,'08:00','12:00'),
    ('MED-1003',1,'14:00','18:00'),('MED-1003',3,'14:00','18:00'),('MED-1003',5,'08:00','12:00'),
    ('MED-1004',2,'08:00','12:00'),('MED-1004',4,'14:00','18:00'),('MED-1004',6,'09:00','13:00'),
    ('MED-1005',1,'09:00','13:00'),('MED-1005',3,'09:00','13:00'),('MED-1005',5,'14:00','18:00'),
    ('MED-1006',2,'14:00','18:00'),('MED-1006',4,'08:00','12:00'),('MED-1006',6,'08:00','12:00'),
    ('MED-1007',1,'08:00','12:00'),('MED-1007',3,'14:00','18:00'),('MED-1007',5,'08:00','12:00'),
    ('MED-1008',2,'09:00','13:00'),('MED-1008',4,'09:00','13:00'),('MED-1008',5,'14:00','18:00'),
    ('MED-1009',1,'14:00','18:00'),('MED-1009',3,'08:00','12:00'),('MED-1009',6,'09:00','13:00'),
    ('MED-1010',2,'08:00','12:00'),('MED-1010',4,'14:00','18:00'),('MED-1010',6,'08:00','12:00'),
    ('MED-1011',1,'09:00','13:00'),('MED-1011',3,'14:00','18:00'),('MED-1011',5,'09:00','13:00'),
    ('MED-1012',2,'14:00','18:00'),('MED-1012',4,'08:00','12:00'),('MED-1012',6,'14:00','18:00'),
    ('MED-1013',1,'08:00','12:00'),('MED-1013',3,'08:00','12:00'),('MED-1013',5,'14:00','18:00'),
    ('MED-1014',2,'09:00','13:00'),('MED-1014',4,'09:00','13:00'),('MED-1014',6,'08:00','12:00'),
    ('MED-1015',1,'14:00','18:00'),('MED-1015',3,'08:00','12:00'),('MED-1015',5,'09:00','13:00')
) AS v(Matricula, DiaSemana, HoraInicio, HoraFin)
JOIN Medicos m ON m.Matricula = v.Matricula
WHERE NOT EXISTS (
    SELECT 1
    FROM DisponibilidadMedico d
    WHERE d.IDMedico = m.IDMedico
      AND d.DiaSemana = v.DiaSemana
      AND d.HoraInicio = CAST(v.HoraInicio AS TIME)
      AND d.HoraFin = CAST(v.HoraFin AS TIME)
);
GO

------------------------------------------------------------
-- AUSENCIAS MEDICAS
------------------------------------------------------------

INSERT INTO AusenciasMedico (IDMedico, Fecha, Motivo)
SELECT m.IDMedico, DATEADD(DAY, v.DiasDesdeHoy, CAST(GETDATE() AS DATE)), v.Motivo
FROM (VALUES
    ('MED-1001',45,'Congreso medico'),
    ('MED-1002',46,'Capacitacion'),
    ('MED-1003',47,'Licencia programada'),
    ('MED-1004',48,'Jornada academica'),
    ('MED-1005',49,'Capacitacion'),
    ('MED-1006',50,'Licencia programada'),
    ('MED-1007',51,'Congreso medico'),
    ('MED-1008',52,'Capacitacion'),
    ('MED-1009',53,'Jornada academica'),
    ('MED-1010',54,'Licencia programada')
) AS v(Matricula, DiasDesdeHoy, Motivo)
JOIN Medicos m ON m.Matricula = v.Matricula
WHERE NOT EXISTS (
    SELECT 1
    FROM AusenciasMedico a
    WHERE a.IDMedico = m.IDMedico
      AND a.Fecha = DATEADD(DAY, v.DiasDesdeHoy, CAST(GETDATE() AS DATE))
);
GO

------------------------------------------------------------
-- TURNOS
-- Reglas aplicadas:
-- 1. Finalizado y Cancelado se usan en fechas pasadas.
-- 2. Pendiente, Confirmado y Reprogramado se usan en fechas futuras.
-- 3. Ningun turno futuro queda Finalizado.
-- 4. Ningun turno pasado queda Pendiente o Confirmado.
-- 5. La especialidad del turno sale de la especialidad real del medico.
------------------------------------------------------------

INSERT INTO Turnos (Codigo, IDPaciente, IDMedico, FechaHora, IDEstadoTurno, IDEspecialidad)
SELECT
    v.Codigo,
    p.IDPaciente,
    m.IDMedico,
    DATEADD(MINUTE, v.Minutos, CAST(DATEADD(DAY, v.DiasDesdeHoy, CAST(GETDATE() AS DATE)) AS DATETIME)),
    et.IDEstadoTurno,
    m.IDEspecialidad
FROM (VALUES
    ('T-PAST-001','sruiz','MED-1001',-60,540,'Finalizado'),
    ('T-PAST-002','dsanchez','MED-1002',-59,600,'Finalizado'),
    ('T-PAST-003','vtorres','MED-1003',-58,660,'Cancelado'),
    ('T-PAST-004','mherrera','MED-1004',-57,840,'Finalizado'),
    ('T-PAST-005','ccastro','MED-1005',-56,900,'Cancelado'),
    ('T-PAST-006','nvega','MED-1006',-55,960,'Finalizado'),
    ('T-PAST-007','jsilva','MED-1007',-54,540,'Cancelado'),
    ('T-PAST-008','facosta','MED-1008',-53,600,'Finalizado'),
    ('T-PAST-009','pbenitez','MED-1009',-52,660,'Finalizado'),
    ('T-PAST-010','asuarez','MED-1010',-51,840,'Cancelado'),
    ('T-PAST-011','fmendez','MED-1011',-50,900,'Finalizado'),
    ('T-PAST-012','bortiz','MED-1012',-49,960,'Cancelado'),
    ('T-PAST-013','lrios','MED-1013',-48,540,'Finalizado'),
    ('T-PAST-014','tnunez','MED-1014',-47,600,'Cancelado'),
    ('T-PAST-015','mcampos','MED-1015',-46,660,'Finalizado'),
    ('T-PAST-016','rmoreno','MED-1001',-45,840,'Finalizado'),
    ('T-PAST-017','bsosa','MED-1002',-44,900,'Cancelado'),
    ('T-PAST-018','iluna','MED-1003',-43,960,'Finalizado'),
    ('T-PAST-019','mponce','MED-1004',-42,540,'Cancelado'),
    ('T-PAST-020','enavarro','MED-1005',-41,600,'Finalizado'),
    ('T-PAST-021','rarias','MED-1006',-40,660,'Finalizado'),
    ('T-PAST-022','gperalta','MED-1007',-39,840,'Cancelado'),
    ('T-PAST-023','nfarias','MED-1008',-38,900,'Finalizado'),
    ('T-PAST-024','scabrera','MED-1009',-37,960,'Finalizado'),
    ('T-PAST-025','amedina','MED-1010',-36,540,'Cancelado'),
    ('T-PAST-026','lcorrea','MED-1011',-35,600,'Finalizado'),
    ('T-PAST-027','rparedes','MED-1012',-34,660,'Cancelado'),
    ('T-PAST-028','ebravo','MED-1013',-33,840,'Finalizado'),
    ('T-PAST-029','aserrano','MED-1014',-32,900,'Finalizado'),
    ('T-PAST-030','jmansilla','MED-1015',-31,960,'Cancelado'),

    ('T-FUT-001','sruiz','MED-1004',7,540,'Pendiente'),
    ('T-FUT-002','dsanchez','MED-1005',8,600,'Confirmado'),
    ('T-FUT-003','vtorres','MED-1006',9,660,'Reprogramado'),
    ('T-FUT-004','mherrera','MED-1007',10,840,'Confirmado'),
    ('T-FUT-005','ccastro','MED-1008',11,900,'Pendiente'),
    ('T-FUT-006','nvega','MED-1009',12,960,'Confirmado'),
    ('T-FUT-007','jsilva','MED-1010',13,540,'Pendiente'),
    ('T-FUT-008','facosta','MED-1011',14,600,'Confirmado'),
    ('T-FUT-009','pbenitez','MED-1012',15,660,'Reprogramado'),
    ('T-FUT-010','asuarez','MED-1013',16,840,'Confirmado'),
    ('T-FUT-011','fmendez','MED-1014',17,900,'Pendiente'),
    ('T-FUT-012','bortiz','MED-1015',18,960,'Confirmado'),
    ('T-FUT-013','lrios','MED-1001',19,540,'Pendiente'),
    ('T-FUT-014','tnunez','MED-1002',20,600,'Confirmado'),
    ('T-FUT-015','mcampos','MED-1003',21,660,'Cancelado'),
    ('T-FUT-016','rmoreno','MED-1004',22,840,'Confirmado'),
    ('T-FUT-017','bsosa','MED-1005',23,900,'Pendiente'),
    ('T-FUT-018','iluna','MED-1006',24,960,'Confirmado'),
    ('T-FUT-019','mponce','MED-1007',25,540,'Reprogramado'),
    ('T-FUT-020','enavarro','MED-1008',26,600,'Confirmado'),
    ('T-FUT-021','rarias','MED-1009',27,660,'Pendiente'),
    ('T-FUT-022','gperalta','MED-1010',28,840,'Confirmado'),
    ('T-FUT-023','nfarias','MED-1011',29,900,'Cancelado'),
    ('T-FUT-024','scabrera','MED-1012',30,960,'Confirmado'),
    ('T-FUT-025','amedina','MED-1013',31,540,'Pendiente'),
    ('T-FUT-026','lcorrea','MED-1014',32,600,'Confirmado'),
    ('T-FUT-027','rparedes','MED-1015',33,660,'Reprogramado'),
    ('T-FUT-028','ebravo','MED-1001',34,840,'Confirmado'),
    ('T-FUT-029','aserrano','MED-1002',35,900,'Pendiente'),
    ('T-FUT-030','jmansilla','MED-1003',36,960,'Confirmado')
) AS v(Codigo, PacienteUsername, Matricula, DiasDesdeHoy, Minutos, EstadoTurno)
JOIN Usuarios up ON up.Username = v.PacienteUsername
JOIN Pacientes p ON p.IDUsuario = up.IDUsuario
JOIN Medicos m ON m.Matricula = v.Matricula
JOIN EstadoTurno et ON et.Nombre = v.EstadoTurno
WHERE NOT EXISTS (SELECT 1 FROM Turnos t WHERE t.Codigo = v.Codigo);
GO

------------------------------------------------------------
-- HISTORIA CLINICA
-- Se crean registros solo para turnos finalizados.
------------------------------------------------------------

INSERT INTO HistoriaClinica (IDPaciente, IDMedico, IDTurno, Diagnostico, Tratamiento, Observaciones)
SELECT t.IDPaciente, t.IDMedico, t.IDTurno, v.Diagnostico, v.Tratamiento, v.Observaciones
FROM (VALUES
    ('T-PAST-001','Hipertension arterial controlada','Continuar Losartan 50 mg diarios.','Control cardiologico en 60 dias.'),
    ('T-PAST-002','Dermatitis atopica','Crema con corticoide topico durante 7 dias.','Se recomienda hidratacion diaria de la piel.'),
    ('T-PAST-004','Gripe estacional','Reposo, hidratacion y antitermicos.','Sin signos de alarma.'),
    ('T-PAST-006','Control pediatrico anual','Sin tratamiento farmacologico.','Crecimiento y desarrollo acordes a edad.'),
    ('T-PAST-008','Control ginecologico preventivo','Solicitud de estudios de rutina.','Control anual indicado.'),
    ('T-PAST-009','Infeccion urinaria baja','Nitrofurantoina durante 5 dias.','Reconsulta si persisten sintomas.'),
    ('T-PAST-011','Cefalea tensional','Paracetamol segun necesidad y pautas de descanso.','Sin signos neurologicos de alarma.'),
    ('T-PAST-013','Dolor articular inespecifico','Antiinflamatorio por 5 dias y actividad fisica moderada.','Se solicita laboratorio de control.'),
    ('T-PAST-015','Sobrepeso','Plan alimentario hipocalorico y seguimiento mensual.','Se indica registro de comidas.'),
    ('T-PAST-016','Dislipidemia','Dieta hipolipidica y actividad fisica.','Solicitado laboratorio de control.'),
    ('T-PAST-018','Conjuntivitis alergica','Colirio antihistaminico durante 7 dias.','Evitar automedicacion con corticoides.'),
    ('T-PAST-020','Sinusitis aguda','Amoxicilina con acido clavulanico por 7 dias.','Control si persiste fiebre.'),
    ('T-PAST-021','Broncoespasmo leve','Salbutamol segun indicacion y control.','Se explican pautas de alarma.'),
    ('T-PAST-023','Dolor pelviano inespecifico','Analgesia y ecografia de control.','Seguimiento en consultorio.'),
    ('T-PAST-024','Litiasis renal pequena','Hidratacion, analgesia y control urologico.','Se solicita ecografia renal.'),
    ('T-PAST-026','Migrana sin aura','Tratamiento analgesico y control de desencadenantes.','Control neurologico en 30 dias.'),
    ('T-PAST-028','Lumbalgia mecanica','Reposo relativo, calor local y kinesiologia.','Sin deficit neurologico.'),
    ('T-PAST-029','Ansiedad leve','Psicoeducacion y seguimiento.','Se propone control en 30 dias.')
) AS v(CodigoTurno, Diagnostico, Tratamiento, Observaciones)
JOIN Turnos t ON t.Codigo = v.CodigoTurno
JOIN EstadoTurno et ON et.IDEstadoTurno = t.IDEstadoTurno AND et.Nombre = 'Finalizado'
WHERE NOT EXISTS (
    SELECT 1
    FROM HistoriaClinica hc
    WHERE hc.IDTurno = t.IDTurno
);
GO

SET NOCOUNT OFF;
GO
