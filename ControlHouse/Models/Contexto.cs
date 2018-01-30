namespace ControlHouse.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class Contexto : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'Contexto' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'ControlHouse.Models.Contexto' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'Contexto'  en el archivo de configuración de la aplicación.
        public Contexto()
            : base("name=Contexto")
        {
        }

        public DbSet<CuentaModel> Cuenta { get; set; }
        public DbSet<MovimientosModel> Movimiento { get; set; }
        public DbSet<CategoriaModel> Categoria { get; set; }
        public DbSet<SubCategoriaModel> SubCategoria { get; set; }
        public DbSet<TransaccionModel> Transaccion { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            #region "Cuenta"

            modelBuilder.Entity<CuentaModel>()
                        .Property(c => c.Nombre)
                        .IsRequired();

            #endregion

            #region "Movimiento"
            modelBuilder.Entity<MovimientosModel>()
                .HasRequired(c => c.Cuenta)
                .WithMany(c => c.Movimientos)
                .HasForeignKey(c => c.CuentaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovimientosModel>()
                .HasRequired(c => c.Categoria)
                .WithMany(c => c.Movimientos)
                .HasForeignKey(c => c.CategoriaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovimientosModel>()
                .HasRequired(c => c.SubCategoria)
                .WithMany(c => c.Movimientos)
                .HasForeignKey(c => c.SubCategoriaId)
                .WillCascadeOnDelete(false);

            #endregion

            #region "Categoria"
            #endregion

            #region "SubCategoria"

            modelBuilder.Entity<SubCategoriaModel>()
                .HasRequired(c => c.Categoria)
                .WithMany(c => c.SubCategorias)
                .HasForeignKey(c => c.CategoriaId)
                .WillCascadeOnDelete(false);
            #endregion

            #region "Transacciones"

            modelBuilder.Entity<TransaccionModel>()
                .HasRequired(c => c.CuentaOrigen)
                .WithMany(c => c.TransaccionesCuentaOrigen)
                .HasForeignKey(c => c.CuentaIdOrigen);

            modelBuilder.Entity<TransaccionModel>()
                .HasRequired(c => c.CuentaDestino)
                .WithMany(c => c.TransaccionesCuentaDestino)
                .HasForeignKey(c => c.CuentaIdDestino);

            #endregion 
        }

 
        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}