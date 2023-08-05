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



    /// <summary>
    /// Obtiene una nota especifica
    /// </summary>
    /// <param name="token">Token</param>
    [HttpGet("/one")]
    public async Task<IActionResult> GetOne([FromHeader] int id, [FromHeader] string token)
    {

        var (isValid, userID) = Jwt.Validate(token);

        if (!isValid)
        {
            return StatusCode(401, "Invalid Token");
        }

        var nota = await Data.Notes.GetOne(id);

        if (nota.UserId != userID)
            return StatusCode(401, new
            {
                message = "Esta nota no te pertenece"
            });


        return StatusCode(200, new
        {
            message = "Success",
            Note = nota
        });

    }



    /// <summary>
    /// Actualizar una nota
    /// </summary>
    /// <param name="token">Token</param>
    [HttpPatch]
    public async Task<IActionResult> Update([FromHeader] string token, [FromBody] NoteModel model)
    {

        var (isValid, _) = Jwt.Validate(token);

        if (!isValid)
        {
            return StatusCode(401, "Invalid Token");
        }

        var nota = await Data.Notes.Update(model);

        if (nota)
            return StatusCode(400, new
            {
                message = "This note can't be updated or doesn't exist"
            });


        return StatusCode(200, new
        {
            message = "Success"
        });

    }



}