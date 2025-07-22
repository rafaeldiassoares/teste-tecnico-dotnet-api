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
                return Results.NotFound(new { message = "Usuário não existe" });
            }

            var userEmailExosts = await _userRepository.GetByEmail(user.Email);
            if (userEmailExosts != null && userEmailExosts.Id != id)
            {
                return Results.BadRequest(new { message = "Email já está sendo utilizado por outro usuário" });
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
                return Results.NotFound(new { message = "Usuário não existe" });
            }

            await _userRepository.DeleteAsync(id);
            await _userRepository.CommitAsync();   
            
            return Results.Ok(new { message = "Usuário deletado com sucesso" });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao deletar usuário", error = ex.Message });
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

            // O correto é criar um DTO no repository que não deixe retornar a senha
            user.Password = "";
            
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
            var users = await _userRepository.GetAll();
            return Results.Ok(new { message = "Lista de usuários", data = users });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao buscar usuários", error = ex.Message });
        }
    }
}