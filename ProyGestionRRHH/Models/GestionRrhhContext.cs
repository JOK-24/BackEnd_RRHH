using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyGestionRRHH.Models;

public partial class GestionRrhhContext : DbContext
{
    public GestionRrhhContext()
    {
    }

    public GestionRrhhContext(DbContextOptions<GestionRrhhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<HistorialPermiso> HistorialPermisos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //    => optionsBuilder.UseSqlServer("server=localhost;database=Gestion_RRHH;integrated security=true;TrustServerCertificate=false;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__departam__64F37A1620B47996");

            entity.ToTable("departamentos");

            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.Codigo)
                .HasMaxLength(100)
                .HasColumnName("codigo");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__empleado__88B5139448B72316");

            entity.ToTable("empleados");

            entity.HasIndex(e => e.Correo, "UQ__empleado__2A586E0B326E74FA").IsUnique();

            entity.HasIndex(e => e.IdUsuario, "UQ__empleado__4E3E04AC3FFE5512").IsUnique();

            entity.HasIndex(e => e.Dni, "UQ__empleado__D87608A736A117A5").IsUnique();

            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .HasColumnName("dni");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.EstadoEmpleado)
                .HasMaxLength(20)
                .HasColumnName("estado_empleado");
            entity.Property(e => e.FechaContratacion).HasColumnName("fecha_contratacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.IdPuesto).HasColumnName("id_puesto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salario");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__empleados__id_de__4CA06362");

            entity.HasOne(d => d.IdPuestoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdPuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__empleados__id_pu__4BAC3F29");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__empleados__id_us__4AB81AF0");
        });

        modelBuilder.Entity<HistorialPermiso>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__historia__76E6C502BA27AB67");

            entity.ToTable("historial_permisos");

            entity.Property(e => e.IdHistorial).HasColumnName("id_historial");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaSolicitud).HasColumnName("fecha_solicitud");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.Motivo)
                .HasMaxLength(500)
                .HasColumnName("motivo");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.HistorialPermisos)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__historial__id_em__5441852A");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.HistorialPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__historial__id_pe__5535A963");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__permisos__228F224FE042F514");

            entity.ToTable("permisos");

            entity.HasIndex(e => e.Nombre, "UQ__permisos__72AFBCC64DAE82AB").IsUnique();

            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.IdPuesto).HasName("PK__puestos__123AAB990E1C515C");

            entity.ToTable("puestos");

            entity.Property(e => e.IdPuesto).HasColumnName("id_puesto");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.SalarioBase)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salario_base");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Puestos)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__puestos__id_depa__3E52440B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F01C6786B");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombre, "UQ__roles__72AFBCC697E7D337").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F60578DA1");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.NombreUsuario, "UQ__usuarios__D4D22D74447A478D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(32)
                .HasColumnName("nombre_usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuarios__id_rol__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
