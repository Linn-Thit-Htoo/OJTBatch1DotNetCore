﻿namespace ExpenseTrackerApi.Queries;

public static class IncomeQuery
{
    #region Get Income List Query

    public static string GetIncomeListQuery()
    {
        return @"SELECT Rest_Income.IncomeId, Rest_Income.CreateDate, Rest_Users.UserName, Rest_Income_Category.IncomeCategoryName,
Rest_Income.Amount, Rest_Income.IsActive
FROM Rest_Income
INNER JOIN Rest_Users ON Rest_Income.UserId = Rest_Users.UserId
INNER JOIN Rest_Income_Category ON Rest_Income.IncomeCategoryId = Rest_Income_Category.IncomeCategoryId
WHERE Rest_Income.IsActive = @IsActive
ORDER BY IncomeId DESC";
        ;
    }

    #endregion

    #region Get Income List By UserId Query

    public static string GetIncomeListByUserIdQuery()
    {
        return @"SELECT Rest_Income.IncomeId, Rest_Income.CreateDate, Rest_Users.UserName, Rest_Income_Category.IncomeCategoryName,
Rest_Income.Amount, Rest_Income.IsActive
FROM Rest_Income
INNER JOIN Rest_Users ON Rest_Income.UserId = Rest_Users.UserId
INNER JOIN Rest_Income_Category ON Rest_Income.IncomeCategoryId = Rest_Income_Category.IncomeCategoryId
WHERE Rest_Income.IsActive = @IsActive AND Rest_Income.UserId = @UserId
ORDER BY IncomeId DESC";
    }

    #endregion

    #region Create Income Query

    public static string CreateIncomeQuery()
    {
        return @"INSERT INTO Rest_Income (IncomeCategoryId, UserId, Amount, CreateDate, IsActive)
VALUES (@IncomeCategoryId, @UserId, @Amount, @CreateDate, @IsActive)";
    }

    #endregion

    #region Update Income Query

    public static string UpdateIncomeQuery()
    {
        return @"UPDATE Rest_Income SET IncomeCategoryId = @IncomeCategoryId,
Amount = @Amount WHERE IncomeId = @IncomeId AND UserId = @UserId";
    }

    #endregion

    #region Delete Income Query

    public static string DeleteIncomeQuery()
    {
        return @"UPDATE Rest_Income SET IsActive = @IsActive WHERE IncomeId = @IncomeId";
    }

    #endregion
}
