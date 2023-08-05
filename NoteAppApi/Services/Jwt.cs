using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NoteAppApi.Services;


public class Jwt
{


    /// <summary>
    /// Genera un JSON Web Token
    /// </summary>
    /// <param name="user">Modelo de usuario</param>
    internal static string Generate(UserModel user)
    {

        // Clave del JWT
        var clave = Configuration.GetConfiguration("jwt:key");

        // Configuración
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));

        // Credenciales
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        // Reclamaciones
        var claims = new[]
        {
            new Claim(ClaimTypes.PrimarySid, user.ID.ToString())
        };

        // Expiración del token
        var expiración = DateTime.Now.AddHours(5);

        // Token
        var token = new JwtSecurityToken(null, null, claims, null, expiración, credentials);

        // Genera el token
        return new JwtSecurityTokenHandler().WriteToken(token);
    }



    /// <summary>
    /// Valida un JSON Web token
    /// </summary>
    /// <param name="token">Token a validar</param>
    internal static (bool isValid, int userID) Validate(string token)
    {
        try
        {

            // Clave del JWT
            var clave = Configuration.GetConfiguration("jwt:key");

            // Configurar la clave secreta
            var key = Encoding.ASCII.GetBytes(clave);

            // Validar el token
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
            };

            try
            {

                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;


                // 
                _ = int.TryParse(jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.PrimarySid)?.Value, out int id);


                // Devuelve una respuesta exitosa
                return (true, id);
            }
            catch (SecurityTokenException)
            {

            }


        }
        catch { }

        return (false, 0);




    }


}