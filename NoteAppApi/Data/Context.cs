namespace NoteAppApi.Data;

public class Context : DbContext
{

    /// <summary>
    /// Tabla de usuarios
    /// </summary>
    public DbSet<UserModel> Users { get; set; }



    /// <summary>
    /// Tabla de notas
    /// </summary>
    public DbSet<NoteModel> Notes { get; set; }




    public Context(DbContextOptions<Context> options) : base(options) 
    {
    }




    /// <summary>
    /// Configuracion de los nombres de la BD
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Nombre de las tabalas
        modelBuilder.Entity<UserModel>().ToTable("USERS");
        modelBuilder.Entity<NoteModel>().ToTable("NOTES");

        // Configuracion de Users
        modelBuilder.Entity<UserModel>().HasKey(u => u.User);

        // Indices de Users
        modelBuilder.Entity<UserModel>().HasIndex(u => u.Id);

        // Indices de notas
        modelBuilder.Entity<NoteModel>().HasIndex(u => u.Id);
        modelBuilder.Entity<NoteModel>().HasIndex(u => u.UserId);

        // Metodo base
        base.OnModelCreating(modelBuilder);
    }



}