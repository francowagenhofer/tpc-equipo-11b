namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisponibilidadMedicoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicoId = c.Int(nullable: false),
                        DiaSemana = c.Int(nullable: false),
                        HoraInicio = c.Time(nullable: false, precision: 7),
                        HoraFin = c.Time(nullable: false, precision: 7),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicos", t => t.MedicoId, cascadeDelete: true)
                .Index(t => t.MedicoId);
            
            CreateTable(
                "dbo.Medicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        Matricula = c.String(),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.MedicoEspecialidads",
                c => new
                    {
                        MedicoId = c.Int(nullable: false),
                        EspecialidadId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MedicoId, t.EspecialidadId })
                .ForeignKey("dbo.Especialidads", t => t.EspecialidadId, cascadeDelete: true)
                .ForeignKey("dbo.Medicos", t => t.MedicoId, cascadeDelete: true)
                .Index(t => t.MedicoId)
                .Index(t => t.EspecialidadId);
            
            CreateTable(
                "dbo.Especialidads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Turnoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        PacienteId = c.Int(nullable: false),
                        MedicoId = c.Int(nullable: false),
                        EspecialidadId = c.Int(nullable: false),
                        FechaHora = c.DateTime(nullable: false),
                        EstadoTurnoId = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        ObservacionesPaciente = c.String(),
                        ObservacionesMedico = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Especialidads", t => t.EspecialidadId, cascadeDelete: true)
                .ForeignKey("dbo.EstadoTurnoes", t => t.EstadoTurnoId, cascadeDelete: true)
                .ForeignKey("dbo.Medicos", t => t.MedicoId, cascadeDelete: true)
                .ForeignKey("dbo.Pacientes", t => t.PacienteId, cascadeDelete: true)
                .Index(t => t.PacienteId)
                .Index(t => t.MedicoId)
                .Index(t => t.EspecialidadId)
                .Index(t => t.EstadoTurnoId);
            
            CreateTable(
                "dbo.EstadoTurnoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        DNI = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Email = c.String(),
                        Telefono = c.String(),
                        Direccion = c.String(),
                        ObraSocial = c.String(),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HistoriaClinicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PacienteId = c.Int(nullable: false),
                        MedicoId = c.Int(nullable: false),
                        TurnoId = c.Int(),
                        Fecha = c.DateTime(nullable: false),
                        Diagnostico = c.String(),
                        Tratamiento = c.String(),
                        Observaciones = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicos", t => t.MedicoId)
                .ForeignKey("dbo.Pacientes", t => t.PacienteId, cascadeDelete: true)
                .ForeignKey("dbo.Turnoes", t => t.TurnoId)
                .Index(t => t.PacienteId)
                .Index(t => t.MedicoId)
                .Index(t => t.TurnoId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Email = c.String(),
                        Telefono = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        RolId = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rols", t => t.RolId, cascadeDelete: true)
                .Index(t => t.RolId);
            
            CreateTable(
                "dbo.Rols",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medicos", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "RolId", "dbo.Rols");
            DropForeignKey("dbo.Turnoes", "PacienteId", "dbo.Pacientes");
            DropForeignKey("dbo.HistoriaClinicas", "TurnoId", "dbo.Turnoes");
            DropForeignKey("dbo.HistoriaClinicas", "PacienteId", "dbo.Pacientes");
            DropForeignKey("dbo.HistoriaClinicas", "MedicoId", "dbo.Medicos");
            DropForeignKey("dbo.Turnoes", "MedicoId", "dbo.Medicos");
            DropForeignKey("dbo.Turnoes", "EstadoTurnoId", "dbo.EstadoTurnoes");
            DropForeignKey("dbo.Turnoes", "EspecialidadId", "dbo.Especialidads");
            DropForeignKey("dbo.MedicoEspecialidads", "MedicoId", "dbo.Medicos");
            DropForeignKey("dbo.MedicoEspecialidads", "EspecialidadId", "dbo.Especialidads");
            DropForeignKey("dbo.DisponibilidadMedicoes", "MedicoId", "dbo.Medicos");
            DropIndex("dbo.Usuarios", new[] { "RolId" });
            DropIndex("dbo.HistoriaClinicas", new[] { "TurnoId" });
            DropIndex("dbo.HistoriaClinicas", new[] { "MedicoId" });
            DropIndex("dbo.HistoriaClinicas", new[] { "PacienteId" });
            DropIndex("dbo.Turnoes", new[] { "EstadoTurnoId" });
            DropIndex("dbo.Turnoes", new[] { "EspecialidadId" });
            DropIndex("dbo.Turnoes", new[] { "MedicoId" });
            DropIndex("dbo.Turnoes", new[] { "PacienteId" });
            DropIndex("dbo.MedicoEspecialidads", new[] { "EspecialidadId" });
            DropIndex("dbo.MedicoEspecialidads", new[] { "MedicoId" });
            DropIndex("dbo.Medicos", new[] { "UsuarioId" });
            DropIndex("dbo.DisponibilidadMedicoes", new[] { "MedicoId" });
            DropTable("dbo.Rols");
            DropTable("dbo.Usuarios");
            DropTable("dbo.HistoriaClinicas");
            DropTable("dbo.Pacientes");
            DropTable("dbo.EstadoTurnoes");
            DropTable("dbo.Turnoes");
            DropTable("dbo.Especialidads");
            DropTable("dbo.MedicoEspecialidads");
            DropTable("dbo.Medicos");
            DropTable("dbo.DisponibilidadMedicoes");
        }
    }
}
