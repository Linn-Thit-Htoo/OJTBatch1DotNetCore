﻿namespace ExpenseTrackerApi.Queries;

public static class UserQuery
{
    public static string GetRegisterQuery()
    {
        return @"INSERT INTO Rest_Users (UserName, Email, Password, UserRole, DOB, Gender, IsActive)
VALUES (@UserName, @Email, @Password, @UserRole, @DOB, @Gender, @IsActive);
SELECT SCOPE_IDENTITY();";
    }

    public static string GetLoginQuery()
    {
        return @"SELECT [UserId]
      ,[UserName]
      ,[Email]
      ,[UserRole]
      ,[DOB]
      ,[Gender]
      ,[IsActive]
  FROM [dbo].[Rest_Users] WHERE Email = @Email AND Password = @Password AND IsActive = @IsActive";
    }

    public static string GetDuplicateEmailQuery()
    {
        return @"SELECT [UserId]
      ,[UserName]
      ,[Email]
      ,[UserRole]
      ,[DOB]
      ,[Gender]
      ,[IsActive]
  FROM [dbo].[Rest_Users] WHERE Email = @Email AND IsActive = @IsActive";
    }

    public static string CheckUserEixstsQuery()
    {
        return @"SELECT [UserId]
      ,[UserName]
      ,[Email]
      ,[UserRole]
      ,[DOB]
      ,[Gender]
      ,[IsActive]
  FROM [dbo].[Rest_Users] WHERE UserId = @UserId AND IsActive = @IsActive";
    }
}
