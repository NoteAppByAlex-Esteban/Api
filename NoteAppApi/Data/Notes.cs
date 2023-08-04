namespace NoteAppApi.Data;

internal class Notes
{


    /// <summary>
    /// Crear nueva nota
    /// </summary>
    /// <param name="note">Modelo de la nota</param>
    public static async Task<int> Create(NoteModel note)
    {

        try
        {
            // Obtener conexion
            var context = Conexion.GetConexion();

            // Agregar nota
            await context.DataBase.Notes.AddAsync(note);

            // Guardar cambios
            context.DataBase.SaveChanges();

            // Retorna el ID
            return note.Id;

        }
        catch
        {
            // Manejo del error

        }

        // ID de error
        return 0;
    }



    /// <summary>
    /// Obtiene una nota
    /// </summary>
    /// <param name="id">ID de la nota</param>
    public static async Task<NoteModel> GetOne(int id)
    {
        try
        {

            // Obtener conexion
            var context = Conexion.GetConexion();

            // Obtener
            var noteData = await (from N in context.DataBase.Notes
                                  where N.Id == id
                                  where N.State == States.Actived
                                  select N).FirstOrDefaultAsync();

            // Evaluar si existe
            if (noteData == null)
                return new()
                {
                    Id = 0
                };

            return noteData;

        }
        catch { }

        return new()
        {
            Id = 0
        };
    }



    /// <summary>
    /// Obtiene las notas de un usuario
    /// </summary>
    /// <param name="id">ID del usuario</param>
    public static async Task<List<NoteModel>> GetAll(int id)
    {
        try
        {

            // Obtener conexion
            var context = Conexion.GetConexion();

            // Obtener
            var notas = await (from N in context.DataBase.Notes
                                  where N.UserId == id && N.State == States.Actived
                                  select N).ToListAsync();

            // Evaluar si existe
            if (notas == null)
                return new();

            return notas;

        }
        catch { }

        return new();
    }



    /// <summary>
    /// Actualiza la informacion de una nota
    /// </summary>
    /// <param name="newData">Nueva informacion</param>
    public static async Task<bool> Update(NoteModel newData)
    {
        try
        {

            // Obtener conexion
            var context = Conexion.GetConexion();

            // Obtener la nota
            var note = await (from N in context.DataBase.Notes
                              where N.Id == newData.Id
                              where N.State == States.Actived
                              select N).FirstOrDefaultAsync();

            // Evaluar si existe
            if (note == null)
                return false;

            // Actualizar datos
            note.Photo = newData.Photo;
            note.Description = newData.Description;
            note.Category = newData.Category;
            note.Title = newData.Title;

            // Guardar Cambios
            context.DataBase.SaveChanges();

            return true;
        }
        catch { }

        return false;
    }



    /// <summary>
    /// Actualiza el estado de una nota
    /// </summary>
    /// <param name="id">ID de la nota</param>
    /// <param name="newState">Nuevo estado</param>
    public static async Task<bool> UpdateState(int id, States newState)
    {
        try
        {

            // Obtener conexion
            var context = Conexion.GetConexion();

            // Obtener 
            var note = await (from U in context.DataBase.Notes
                              where U.Id == id
                              select U).FirstOrDefaultAsync();

            // Evaluar si existe
            if (note == null)
                return false;

            // Actualizar estado
            note.State = newState;

            // Guardar Cambios
            context.DataBase.SaveChanges();

            return true;
        }
        catch { }

        return false;
    }


}
