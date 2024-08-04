using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using Notebook.Services.HashServices;
using Notebook.Services.ResultService;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Services.Flows;

public sealed class UserAccessFlow
{
    private const string CreateCredentialsError = "Registration error.";
    private const string UpdateCredentialsError = "Error while update credentials.";
    private const string CreateTokenError = "Error while create token.";
    private const string CreateUserError = "Error while create user.";
    private const string UndoError = "System error.";
    private const string InviteEmailTitle = "You was invited into our system!";
    private const string AcceptInviteButton = "Accept";
    private const string InvalidCredentialsError = "Invalid credentials.";

    private const string SESSION_KEY_NAME = "Id";

    public async Task<Result> InviteNewUser(
        string email,
        IDbContextTransaction transaction,
        Func<Result<string>> tokenGenerator,
        Func<CredentialsEntity, Task> createCredentials,
        Func<TokenEntity, Task> createToken,
        Func<string, string> urlBuilder,
        Func<string, string, Task<string>> emailTemplate,
        Func<string, string, string, Task<Result>> sendEmail)
    {
        using (transaction)
        {
            Result<CredentialsEntity> credentialsResult = await CreateCredentials(email, createCredentials);
            if (credentialsResult.ErrorMessages.Any())
            {
                await transaction.RollbackAsync();
                return Result.Error(credentialsResult.ErrorMessages);
            }

            Result<TokenEntity> tokenResult = await CreateToken(tokenGenerator().Value, credentialsResult.Value.Id, createToken);
            if (tokenResult.ErrorMessages.Any())
            {
                await transaction.RollbackAsync();
                return Result.Error(tokenResult.ErrorMessages);
            }

            string inviteLink = urlBuilder(tokenResult.Value.Token);
            string htmlContent = await emailTemplate(inviteLink, AcceptInviteButton);

            Result sendEmailResult = await sendEmail(email, InviteEmailTitle, htmlContent);
            if (sendEmailResult.ErrorMessages.Any())
            {
                await transaction.RollbackAsync();
                return sendEmailResult;
            }


            await transaction.CommitAsync();
        }

        return Result.Success();
    }
    
    public async Task<Result> RegistrationComplete(
        string token,
        string firstName,
        string lastName,
        string login,
        string password,
        IDbContextTransaction transaction,
        Func<string, Task<Result<CredentialsEntity>>> validateToken,
        Func<UserEntity, Task> createUser,
        Func<CredentialsEntity, Task> updateCredentials,
        Action<string, string> authenticate)
    {
        
        using (transaction)
        {
            Result<CredentialsEntity> validationResult = await validateToken(token);
            if (validationResult.ErrorMessages.Any())
            {
                await transaction.RollbackAsync();
                return Result.Error(validationResult.ErrorMessages);
            }

            Result<UserEntity> createUserResult = await CreateUser(firstName, lastName, validationResult.Value.Id, createUser);
            if (createUserResult.ErrorMessages.Any())
            {
                await transaction.RollbackAsync();
                return Result.Error(createUserResult.ErrorMessages);
            }

            Result updateResult = await UpdateCredentials(validationResult.Value, login, password, updateCredentials);
            if (updateResult.ErrorMessages.Any())
            {
                await transaction.RollbackAsync();
                return updateResult;
            }

            authenticate(SESSION_KEY_NAME, validationResult.Value.Id.ToString());
            await transaction.CommitAsync();
        }

        return Result.Success();
    }

    public async Task<Result> Authentication(
        string login,
        string password,
        Func<string, Task<CredentialsEntity?>> getCredentials,
        Action<string, string> authenticate)
    {
        CredentialsEntity? dbRecord = await getCredentials(login);
        if (dbRecord is null)
        {
            return Result.Error(InvalidCredentialsError);
        }

        bool isValidCredentials = ValidateCredentials(dbRecord!, login, password);
        if (!isValidCredentials)
        {
            return Result.Error(InvalidCredentialsError);
        }

        authenticate(SESSION_KEY_NAME, dbRecord!.Id.ToString());
        return Result.Success();
    }

    private async Task<Result<CredentialsEntity>> CreateCredentials(string email, Func<CredentialsEntity, Task> createCredentials)
    {
        CredentialsEntity dbRecord = new() { Email = email };
        try
        {
            await createCredentials(dbRecord);
        }
        catch
        {
            return Result<CredentialsEntity>.Error(CreateCredentialsError);
        }
        return Result<CredentialsEntity>.Success(dbRecord);
    }

    private async Task<Result<TokenEntity>> CreateToken(string token, Guid credentialsId, Func<TokenEntity, Task> createToken)
    {
        TokenEntity dbRecord = new TokenEntity()
        {
            CredentialsId = credentialsId,
            Token = token
        };

        try
        {
            await createToken(dbRecord);
        }
        catch
        {
            return Result<TokenEntity>.Error(CreateTokenError);
        }
        return Result<TokenEntity>.Success(dbRecord);
    }

    private async Task<Result<UserEntity>> CreateUser(string fistName, 
        string lastName, 
        Guid credentialsId, 
        Func<UserEntity, Task> createUser)
    {
        UserEntity dbRecord = new UserEntity()
        {
            CredentialsId = credentialsId,
            FirstName = fistName,
            LastName = lastName
        };

        try
        {
            await createUser(dbRecord);
        }
        catch
        {
            return Result<UserEntity>.Error(CreateUserError);
        }
        return Result<UserEntity>.Success(dbRecord);
    }

    private async Task<Result> UpdateCredentials(CredentialsEntity credentials, 
        string login, 
        string password, 
        Func<CredentialsEntity, Task> updateCredentials)
    {
        (string hash, byte[] salt) = Hasher.Hash(password);

        credentials.Login = login;
        credentials.HashedPassword = hash;
        credentials.Salt = salt;

        try
        {
            await updateCredentials(credentials);
        }
        catch
        {
            return Result.Error(CreateCredentialsError);
        }
        return Result.Success();
    }

    private bool ValidateCredentials(CredentialsEntity credentials, string inputLogin, string inputPassword) =>
        credentials.Login == inputLogin &&
        Hasher.Verify(credentials.HashedPassword,
            inputPassword,
            credentials.Salt);
}