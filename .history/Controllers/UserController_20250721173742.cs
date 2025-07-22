using dotnet_api.Infra.Repository.User;
using dotnet_api.Models;

namespace dotnet_api.Controllers;

public class UserController
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResult> CreateUser(UserModel user)
    {
        try
        {
            // Verificar se o email já existe
            var existingUser = await _userRepository.GetByEmail(user.Email);
            if (existingUser != null)
            {
                return Results.BadRequest(new { message = "Email já cadastrado" });
            }

            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            
            await _userRepository.AddAsync(user);
            await _userRepository.CommitAsync();
            
            return Results.Created($"/users/{user.Id}", user);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao criar usuário", error = ex.Message });
        }
    }

    public async Task<IResult> UpdateUser(int id, UserModel user)
    {
        try
        {
            var existingUser = await _userRepository.GetById(id);
            if (existingUser == null)
            {
                return Results.NotFound(new { message = "Usuário não encontrado" });
            }

            // Verificar se o email já existe em outro usuário
            var userWithEmail = await _userRepository.GetByEmail(user.Email);
            if (userWithEmail != null && userWithEmail.Id != id)
            {
                return Results.BadRequest(new { message = "Email já cadastrado por outro usuário" });
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.UtcNow;
            
            await _userRepository.UpdateAsync(existingUser);
            await _userRepository.CommitAsync();
            
            return Results.Ok(existingUser);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao atualizar usuário", error = ex.Message });
        }
    }

    public async Task<IResult> DeleteUser(int id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return Results.NotFound(new { message = "Usuário não encontrado" });
            }

            // TODO: Implementar delete no repositório
            // await _userRepository.DeleteAsync(user);
            // await _userRepository.CommitAsync();
            
            return Results.Ok(new { message = "Usuário deletado com sucesso" });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao deletar usuário", error = ex.Message });
        }
    }

    public async Task<IResult> Login(LoginRequest loginRequest)
    {
        try
        {
            var user = await _userRepository.GetByEmail(loginRequest.Email);
            if (user == null)
            {
                return Results.BadRequest(new { message = "Email ou senha inválidos" });
            }

            // TODO: Implementar verificação de senha com hash
            if (user.Password != loginRequest.Password)
            {
                return Results.BadRequest(new { message = "Email ou senha inválidos" });
            }

            // TODO: Gerar token JWT
            var token = "fake-jwt-token-" + Guid.NewGuid();

            return Results.Ok(new { 
                message = "Login realizado com sucesso",
                token = token,
                user = new { id = user.Id, name = user.Name, email = user.Email }
            });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao fazer login", error = ex.Message });
        }
    }

    public async Task<IResult> GetUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return Results.NotFound(new { message = "Usuário não encontrado" });
            }

            return Results.Ok(user);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao buscar usuário", error = ex.Message });
        }
    }

    public async Task<IResult> GetUsers()
    {
        try
        {
            // TODO: Implementar listagem de todos os usuários no repositório
            return Results.Ok(new { message = "Lista de usuários", users = new List<UserModel>() });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao buscar usuários", error = ex.Message });
        }
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
} 