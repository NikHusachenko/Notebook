using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using Notebook.Services.ResultService;

namespace Notebook.Services.Flows;

public sealed class UserAccessFlow
{
    private const string CreateCredentialsError = "Registration error.";
    private const string CreateTokenError = "Error while create token.";
    private const string UndoError = "System error.";
    private const string InviteEmailTitle = "You was invited into our system!";
    private const string AcceptInviteButton = "Accept";

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
}