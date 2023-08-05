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
        model.ID = 0;
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



    /// <summary>
    /// Login
    /// </summary>
    /// <param name="user">Usuario</param>
    /// <param name="password">Contraseña</param>
    [HttpGet]
    public async Task<IActionResult> Login([FromQuery] string user, [FromQuery] string password)
    {

        // Valida los datos
        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
            return StatusCode(400, "Invalid Params");

        // Obtiene un usuario
        var userData = await Data.User.GetOne(user);

        // Evalua la respuesta
        if (userData == null || userData.ID <= 0)
            return StatusCode(404, "Not found User");

        // Evaluacion de la contraseña
        if (userData.Password != password)
            return StatusCode(401, "Invalid password");
        
        // Generacion del JWT
        var token = new
        {
            User = userData,
            Jwt = $"//{userData.ID}//"
        };

        // Respuesta satisfactoria
        return StatusCode(200, token);

    }
   

}