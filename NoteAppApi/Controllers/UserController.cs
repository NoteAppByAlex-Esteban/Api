namespace NoteAppApi.Controllers;


[Route("user")]
public class UserController : Controller
{


    /// <summary>
    /// Crear nuevo usuario
    /// </summary>
    /// <param name="model">Modelo del usuario</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserModel model)
    {

        // Validacion de campos
        if (string.IsNullOrWhiteSpace(model.User) || string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Name))
        {
            return StatusCode(400, new
            {
                Id = 0,
                Message = "Parametros invalidos"
            });
        }

        // Sobrescribe los datos
        //model.Id = 0;
        model.State = States.Actived;

        // Respuesta de la BD
        var response = await Data.User.Create(model);

        // Validación
        if (response <= 0)
        {
            return StatusCode(400, new
            {
                Id = 0,
                Message = "No se creo"
            });
        }

        // Correcto
        return StatusCode(201, new
        {
            Id = response,
            Message = "Ok"
        });

    }



   




}
