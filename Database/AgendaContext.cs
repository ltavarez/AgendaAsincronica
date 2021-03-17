namespace Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AgendaContext : DbContext
    {
        public AgendaContext()
            : base("name=AgendaContext")
        {
        }

        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<TipoContacto> TipoContacto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoContacto>()
                .HasMany(e => e.Persona)
                .WithOptional(e => e.TipoContacto)
                .HasForeignKey(e => e.IdTipoContacto);
        }
    }
}
