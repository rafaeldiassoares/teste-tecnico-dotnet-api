using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dotnet_api.Infra.Filters;

public class AuthFilter : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // TODO: Implementar lógica de autenticação real
        // Por enquanto, apenas simula a verificação de token
        
        var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Token de autenticação é obrigatório" });
            return;
        }

        // Aqui você pode adicionar validação do token JWT
        // Por exemplo, verificar se o token é válido, não expirou, etc.
        
        await Task.CompletedTask;
    }
} 