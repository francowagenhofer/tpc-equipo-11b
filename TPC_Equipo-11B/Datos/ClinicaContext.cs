using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dominio;
using System.Security.Cryptography.X509Certificates;

namespace Datos {
    public class ClinicaContext : DbContext {
        public ClinicaContext() : base("name=ClinicaDB"){ } // nombre de la base de datos, se puede configurar en App.config
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<MedicoEspecialidad> MedicoEspecialidades { get; set; }
        public DbSet<DisponibilidadMedico> DisponibilidadesMedico { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<EstadoTurno> EstadosTurno { get; set; }
        public DbSet<HistoriaClinica> HistoriasClinicas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MedicoEspecialidad>()
                .HasKey(me => new { me.MedicoId, me.EspecialidadId });

            modelBuilder.Entity<HistoriaClinica>()
                .HasRequired(h => h.Medico)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<HistoriaClinica>()
                .HasOptional(h => h.Turno)
                .WithMany()
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
