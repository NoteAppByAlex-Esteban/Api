namespace NoteAppApi.Data;

internal class User
{


    /// <summary>
    /// Crear nuevo usuario
    /// </summary>
    /// <param name="user">Modelo del usuario</param>
    public static async Task<int> Create(UserModel user)
    {

        try
        {
            // Obtener conexion
            var context = Conexion.GetConexion();

            // Agregar usuario
            await context.DataBase.Users.AddAsync(user);

            // Guardar cambios
            context.DataBase.SaveChanges();

            // Retorna el ID del usuario
            return user.ID;

        }
        catch (Exception ex)
        {
            // Manejo del error

        }

        // ID de error
        return 0;
    }



    /// <summary>
    /// Obtiene un usuario
    /// </summary>
    /// <param name="user">Usuario de una cuenta</param>
    public static async Task<UserModel> GetOne(string user)
    {
        try
        {

            // Obtener conexion
            var context = Conexion.GetConexion();

            // Obtener el usuario
            var userData = await (from U in context.DataBase.Users
                                  where U.User == user
                                  select U).FirstOrDefaultAsync();

            // Evaluar si existe
            if (userData == null)
                return new()
                {
                    ID = 0
                };

            return userData;

        }
        catch { }

        return new()
        {
            ID = 0
        };
    }



    /// <summary>
    /// Actualiza la contraseña de un usuario
    /// </summary>
    /// <param name="id">ID del usuario</param>
    /// <param name="newPassword">Nueva contraseña</param>
    public static async Task<bool> UpdatePassword(int id, string newPassword)
    {
        try
        {

            // Obtener conexion
            var context = Conexion.GetConexion();

            // Obtener el usuario
            var user = await (from U in context.DataBase.Users
                              where U.ID == id
                              where U.State == States.Actived
                              select U).FirstOrDefaultAsync();

            // Evaluar si existe
            if (user == null)
                return false;

            // Actualizar contraseña
            user.Password = newPassword;

            // Guardar Cambios
            context.DataBase.SaveChanges();

            return true;
        }
        catch { }

        return false;
    }



    /// <summary>
    /// Actualiza la informacion de un usuario
    /// </summary>
    /// <param name="newData">Nueva informacion</param>
    public static async Task<bool> Update(UserModel newData)
    {
        try
        {

            // Obtener conexion
            var context = Conexion.GetConexion();

            // Obtener el usuario
            var user = await (from U in context.DataBase.Users
                              where U.ID == newData.ID
                              where U.State == States.Actived
                              select U).FirstOrDefaultAsync();

            // Evaluar si existe
            if (user == null)
                return false;

            // Actualizar datos
            user.User = newData.User;
            user.Photo = newData.Photo;
            user.Name = newData.Name;

            // Guardar Cambios
            context.DataBase.SaveChanges();

            return true;
        }
        catch { }

        return false;
    }



    /// <summary>
    /// Actualiza el estado de un usuario
    /// </summary>
    /// <param name="id">ID del usuario</param>
    /// <param name="newState">Nuevo estado</param>
    public static async Task<bool> UpdateState(int id, States newState)
    {
        try
        {

            // Obtener conexion
            var context = Conexion.GetConexion();

            // Obtener el usuario
            var user = await (from U in context.DataBase.Users
                              where U.ID == id
                              select U).FirstOrDefaultAsync();

            // Evaluar si existe
            if (user == null)
                return false;

            // Actualizar estado
            user.State = newState;

            // Guardar Cambios
            context.DataBase.SaveChanges();

            return true;
        }
        catch { }

        return false;
    }



}
