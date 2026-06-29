USE ClinicaDB;
GO

BEGIN TRANSACTION;
BEGIN TRY

    -- =========================================================================
    -- 0. CREAR TABLA INTERMEDIA SI NO EXISTE
    -- =========================================================================
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MedicoObraSocial]') AND type in (N'U'))
    BEGIN
        CREATE TABLE MedicoObraSocial (
            IDMedico INT NOT NULL,
            IDObraSocial INT NOT NULL,
            CONSTRAINT PK_MedicoObraSocial PRIMARY KEY (IDMedico, IDObraSocial),
            CONSTRAINT FK_MedicoObraSocial_Medicos FOREIGN KEY (IDMedico) REFERENCES Medicos(IDMedico),
            CONSTRAINT FK_MedicoObraSocial_ObrasSociales FOREIGN KEY (IDObraSocial) REFERENCES ObrasSociales(IDObraSocial)
        );
        PRINT 'Tabla MedicoObraSocial creada con éxito.';
    END

    -- =========================================================================
    -- 1. INSERTAR ESPECIALIDADES (Solo si no existen - Búsqueda tolerante a tildes)
    -- =========================================================================
    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Oftalmolog%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Oftalmología', 'Diagnóstico y tratamiento de enfermedades oculares y de la visión.', 1);

    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Odontolog%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Odontología', 'Cuidado y tratamiento de la salud dental y bucodental.', 1);

    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Traumatolog%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Traumatología', 'Tratamiento de lesiones del aparato locomotor.', 1);

    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Ginecolog%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Ginecología', 'Salud y cuidado del sistema reproductor femenino y obstetricia.', 1);

    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Dermatolog%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Dermatología', 'Prevención y diagnóstico de enfermedades de la piel, cabello y uñas.', 1);

    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Psiquiatr%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Psiquiatría', 'Evaluación y tratamiento de trastornos de la salud mental.', 1);

    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Otorrinolaringolog%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Otorrinolaringología', 'Enfermedades del oído, la nariz y la garganta.', 1);

    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Gastroenterolog%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Gastroenterología', 'Tratamiento de afecciones del estómago y sistema digestivo.', 1);

    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Neurolog%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Neurología', 'Diagnóstico y tratamiento de trastornos del sistema nervioso.', 1);

    IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre LIKE 'Endocrinolog%')
        INSERT INTO Especialidades (Nombre, Descripcion, Activo) VALUES ('Endocrinología', 'Tratamiento de enfermedades de las hormonas y metabolismo.', 1);

    -- =========================================================================
    -- 2. INSERTAR NUEVAS OBRAS SOCIALES (Solo si no existen)
    -- =========================================================================
    IF NOT EXISTS (SELECT 1 FROM ObrasSociales WHERE Nombre LIKE 'PAMI%')
        INSERT INTO ObrasSociales (Nombre, Activo) VALUES ('PAMI', 1);

    IF NOT EXISTS (SELECT 1 FROM ObrasSociales WHERE Nombre LIKE 'OSECAC%')
        INSERT INTO ObrasSociales (Nombre, Activo) VALUES ('OSECAC', 1);

    IF NOT EXISTS (SELECT 1 FROM ObrasSociales WHERE Nombre LIKE 'IOMA%')
        INSERT INTO ObrasSociales (Nombre, Activo) VALUES ('IOMA', 1);

    IF NOT EXISTS (SELECT 1 FROM ObrasSociales WHERE Nombre LIKE 'Medif%')
        INSERT INTO ObrasSociales (Nombre, Activo) VALUES ('Medifé', 1);

    IF NOT EXISTS (SELECT 1 FROM ObrasSociales WHERE Nombre LIKE 'Sancor%')
        INSERT INTO ObrasSociales (Nombre, Activo) VALUES ('Sancor Salud', 1);

    -- =========================================================================
    -- 3. INSERTAR 10 MEDICOS (Solo si el Username no existe)
    -- =========================================================================
    DECLARE @IdUsr INT;
    DECLARE @IdMed INT;
    DECLARE @RolMedico INT = (SELECT TOP 1 IDRol FROM Roles WHERE Nombre LIKE 'Med%');

    -- Médico 1: Oftalmología
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.jgonzalez')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Juan', 'González', 'juan.gonzalez@clinica.com', '1155551234', 'doc.jgonzalez', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90001', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Oftalmolog%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSDE%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Swiss%'));
    END

    -- Médico 2: Odontología
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.mrodriguez')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('María', 'Rodríguez', 'maria.rodriguez@clinica.com', '1155555678', 'doc.mrodriguez', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90002', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Odontolog%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Galeno%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSECAC%'));
    END

    -- Médico 3: Traumatología
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.cgomez')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Carlos', 'Gómez', 'carlos.gomez@clinica.com', '1155559012', 'doc.cgomez', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90003', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Traumatolog%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSDE%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Swiss%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Galeno%'));
    END

    -- Médico 4: Ginecología
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.amartinez')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Ana', 'Martínez', 'ana.martinez@clinica.com', '1155553456', 'doc.amartinez', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90004', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Ginecolog%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Swiss%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSECAC%'));
    END

    -- Médico 5: Dermatología
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.lfernandez')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Luis', 'Fernández', 'luis.fernandez@clinica.com', '1155557890', 'doc.lfernandez', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90005', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Dermatolog%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSDE%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Galeno%'));
    END

    -- Médico 6: Psiquiatría
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.llopez')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Laura', 'López', 'laura.lopez@clinica.com', '1155552345', 'doc.llopez', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90006', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Psiquiatr%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'PAMI%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSECAC%'));
    END

    -- Médico 7: Otorrinolaringología
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.jdiaz')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Jorge', 'Díaz', 'jorge.diaz@clinica.com', '1155556789', 'doc.jdiaz', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90007', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Otorrinolaringolog%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSDE%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Swiss%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Galeno%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'PAMI%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSECAC%'));
    END

    -- Médico 8: Gastroenterología
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.sperez')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Sofía', 'Pérez', 'sofia.perez@clinica.com', '1155550123', 'doc.sperez', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90008', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Gastroenterolog%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Swiss%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Galeno%'));
    END

    -- Médico 9: Neurología
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.mromero')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Miguel', 'Romero', 'miguel.romero@clinica.com', '1155554567', 'doc.mromero', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90009', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Neurolog%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSDE%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Swiss%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'PAMI%'));
    END

    -- Médico 10: Endocrinología
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'doc.etorres')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Elena', 'Torres', 'elena.torres@clinica.com', '1155558901', 'doc.etorres', 'med1234', @RolMedico, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@IdUsr, 'MN-90010', 1);
        SET @IdMed = SCOPE_IDENTITY();
        INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@IdMed, (SELECT TOP 1 IDEspecialidad FROM Especialidades WHERE Nombre LIKE 'Endocrinolog%'));
        INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES 
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Galeno%')),
        (@IdMed, (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSECAC%'));
    END

    -- =========================================================================
    -- 4. INSERTAR 10 PACIENTES (Solo si el Username no existe)
    -- =========================================================================
    DECLARE @RolPaciente INT = (SELECT TOP 1 IDRol FROM Roles WHERE Nombre LIKE 'Pac%');
    DECLARE @GenMasculino INT = COALESCE((SELECT TOP 1 IDGenero FROM Generos WHERE Descripcion LIKE '%Masc%'), (SELECT TOP 1 IDGenero FROM Generos));
    DECLARE @GenFemenino INT = COALESCE((SELECT TOP 1 IDGenero FROM Generos WHERE Descripcion LIKE '%Fem%'), (SELECT TOP 1 IDGenero FROM Generos));

    -- Paciente 1: OSDE, Masculino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.lsanchez')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Lucas', 'Sánchez', 'lucas.sanchez@gmail.com', '1166661111', 'pat.lsanchez', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100201', '1990-05-15', 'Av. Rivadavia 1234, CABA', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSDE%'), @GenMasculino, 1);
    END

    -- Paciente 2: Swiss Medical, Femenino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.csilva')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Camila', 'Silva', 'camila.silva@gmail.com', '1166662222', 'pat.csilva', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100202', '1994-08-22', 'Calle Corrientes 567, Rosario', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Swiss%'), @GenFemenino, 1);
    END

    -- Paciente 3: Galeno, Masculino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.malvarez')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Mateo', 'Álvarez', 'mateo.alvarez@gmail.com', '1166663333', 'pat.malvarez', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100203', '1988-12-05', 'Av. Santa Fe 3456, CABA', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Galeno%'), @GenMasculino, 1);
    END

    -- Paciente 4: PAMI, Femenino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.mrossi')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Martina', 'Rossi', 'martina.rossi@gmail.com', '1166664444', 'pat.mrossi', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100204', '1952-03-30', 'San Martín 981, Córdoba', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'PAMI%'), @GenFemenino, 1);
    END

    -- Paciente 5: OSECAC, Masculino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.jcastro')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Joaquín', 'Castro', 'joaquin.castro@gmail.com', '1166665555', 'pat.jcastro', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100205', '1996-07-12', 'Belgrano 432, Mendoza', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSECAC%'), @GenMasculino, 1);
    END

    -- Paciente 6: OSDE, Femenino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.vherrera')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Valentina', 'Herrera', 'valentina.herrera@gmail.com', '1166666666', 'pat.vherrera', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100206', '1991-01-25', 'Av. Colón 1230, Córdoba', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSDE%'), @GenFemenino, 1);
    END

    -- Paciente 7: Swiss Medical, Masculino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.bortega')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Bruno', 'Ortega', 'bruno.ortega@gmail.com', '1166667777', 'pat.bortega', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100207', '1985-10-18', 'Pueyrredón 789, CABA', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Swiss%'), @GenMasculino, 1);
    END

    -- Paciente 8: Galeno, Femenino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.dmedina')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Delfina', 'Medina', 'delfina.medina@gmail.com', '1166668888', 'pat.dmedina', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100208', '1993-11-02', 'Maipú 150, Tucumán', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'Galeno%'), @GenFemenino, 1);
    END

    -- Paciente 9: OSDE, Masculino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.tmolina')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Tomás', 'Molina', 'tomas.molina@gmail.com', '1166669999', 'pat.tmolina', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100209', '1987-04-14', 'Av. de Mayo 880, CABA', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSDE%'), @GenMasculino, 1);
    END

    -- Paciente 10: OSECAC, Femenino
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = 'pat.zromero')
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo, FechaAlta)
        VALUES ('Zoe', 'Romero', 'zoe.romero@gmail.com', '1166660000', 'pat.zromero', 'pac1234', @RolPaciente, 1, GETDATE());
        SET @IdUsr = SCOPE_IDENTITY();
        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
        VALUES (@IdUsr, '95100210', '1999-09-09', 'Ituzaingó 1420, La Plata', (SELECT TOP 1 IDObraSocial FROM ObrasSociales WHERE Nombre LIKE 'OSECAC%'), @GenFemenino, 1);
    END

    COMMIT TRANSACTION;
    PRINT '¡Scripts ejecutados y registros insertados con éxito!';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'Ocurrió un error en la ejecución. Transacción revertida.';
    THROW;
END CATCH;
