namespace Notebook.EntityFramework.Configuration;

public static class ConfigurationConstrains
{
    public const int LOGIN_MAX_LENGHT = 31;
    public const int PASSWORD_MAX_LENGTH = 31;
    public const int EMAIL_MAX_LENGTH = 63;
    public const int FIRST_NAME_MAX_LENGTH = 31;
    public const int LAST_NAME_MAX_LENGTH = 31;
    public const int NOTE_CONTENT_MAX_LENGTH = 511;
    public const int COMMENT_CONTENT_MAX_LENGTH = 127;
}

public static class DatabaseTableNames
{
    public const string CREDENTIALS_TABLE_NAME = "Credentials";
    public const string USERS_TABLE_NAME = "Users";
    public const string NOTES_TABLE_NAME = "Notes";
    public const string USER_COMMENTS_TABLE_NAME = "Comments";
    public const string USER_LIKES_TABLE_NAME = "User Likes";
}