using Microsoft.AspNetCore.SignalR;

namespace NoteAppApi.Hubs;

public class NoteRealTime : Hub
{


    /// <summary>
    /// Unir una nota a RealTime
    /// </summary>
    /// <param name="idNota">ID de la nota</param>
    public async Task Join(int idNota)
    {
        // Grupo de la cuenta
        await Groups.AddToGroupAsync(Context.ConnectionId, idNota.ToString());

    }



    /// <summary>
    /// Sacar una nota de RealTime
    /// </summary>
    /// <param name="idNota">ID de la nota</param>
    public async Task Left(int idNota)
    {
        // Grupo de la cuenta
        await Groups.AddToGroupAsync(Context.ConnectionId, idNota.ToString());
    }



    /// <summary>
    /// Actualiza en tiempo real la nota
    /// </summary>
    /// <param name="newContent">Nuevo contenido</param>
    /// <param name="nota">ID de la nota</param>
    public async Task Update(string newContent, int nota)
    {
        // Clientes ignorados
        string[] exepts = new string[1] { Context.ConnectionId };

        // Envia el cambio
        await Clients.GroupExcept(nota.ToString(), exepts).SendAsync("refresh", newContent);

    }


}
