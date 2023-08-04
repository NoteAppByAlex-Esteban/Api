using Microsoft.Extensions.Options;

namespace NoteAppApi;

public class Conexion
{


    public static string Connection { get; set; }  


    public Data.Context DataBase { get; set; }



    public Conexion()
    {
        DbContextOptionsBuilder<Data.Context> options = new ();
        options.UseSqlServer(Connection);

        DataBase = new(options.Options);
    }

  




    public static void SetString(string str)
    {
        Connection = str;
    }


    public static Conexion GetConexion()
    {
        return new();
    }


}
