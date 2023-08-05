using NoteAppApi.Services;

namespace NoteAppApi.Controllers;

[Route("notes")]
public class NoteController : Controller
{


    /// <summary>
    /// Crear nueva nota
    /// </summary>
    /// <param name="modelo">Modelo de la nota</param>
    /// <param name="token">Token de acceso</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NoteModel modelo, [FromHeader] string token)
    {

        var (isValid, userID) = Jwt.Validate(token);

        if (!isValid)
            return StatusCode(401, "Invalid Token");


        modelo.UserId = userID;

        // Crea la nota
        var response = await Data.Notes.Create(modelo);

        // Respuesta erronea
        if (response <= 0)
            return StatusCode(400);

        // Todo correcto
        return StatusCode(201, new
        {
            message = "Creado con exito",
            id = response
        });

    }



    /// <summary>
    /// Obtener todas las notas
    /// </summary>
    /// <param name="token">Token</param>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromHeader] string token)
    {

        var (isValid, userID) = Jwt.Validate(token);

        if (!isValid)
        {
            return StatusCode(401, "Invalid Token");
        }

        var notas = await Data.Notes.GetAll(userID);


        return StatusCode(200, new
        {
            message = "Success",
            Models = notas
        });

    }



}