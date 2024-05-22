namespace ExpenseTrackerApi.Queries;

public static class ExpenseCategoryQuery
{
    #region Get Expens eCategory List Query

    public static string GetExpenseCategoryListQuery()
    {
        return @"SELECT [ExpenseCategoryId]
      ,[ExpenseCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Expense_Category] WHERE IsActive = @IsActive ORDER BY ExpenseCategoryId DESC";
    }

    #endregion

    #region Check Create Expense Category Duplicate Query

    public static string CheckCreateExpenseCategoryDuplicateQuery()
    {
        return @"SELECT [ExpenseCategoryId]
      ,[ExpenseCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Expense_Category] WHERE ExpenseCategoryName = @ExpenseCategoryName AND IsActive = @IsActive";
    }

    #endregion

    #region Create Expens eCategory Query

    public static string CreateExpenseCategoryQuery()
    {
        return @"INSERT INTO Rest_Expense_Category (ExpenseCategoryName, IsActive) VALUES (@ExpenseCategoryName, @IsActive)";
    }

    #endregion

    #region Check Update Expense Category Duplicate Query

    public static string CheckUpdateExpenseCategoryDuplicateQuery()
    {
        return @"SELECT [ExpenseCategoryId]
      ,[ExpenseCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Expense_Category] WHERE ExpenseCategoryName = @ExpenseCategoryName AND
IsActive = @IsActive AND
ExpenseCategoryId != @ExpenseCategoryId";
    }

    #endregion

    #region UpdateExpenseCategoryQuery

    public static string UpdateExpenseCategoryQuery()
    {
        return @"UPDATE Rest_Expense_Category SET ExpenseCategoryName = @ExpenseCategoryName WHERE
ExpenseCategoryId = @ExpenseCategoryId";
    }

    #endregion

    #region Check Expense Category Query

    public static string CheckExpenseCategoryQuery()
    {
        return @"SELECT [ExpenseId]
      ,[ExpenseCategoryId]
      ,[Amount]
      ,[IsActive]
  FROM [dbo].[Rest_Expense] WHERE ExpenseCategoryId = @ExpenseCategoryId";
    }

    #endregion

    #region Delete Expense Category Query

    public static string DeleteExpenseCategoryQuery()
    {
        return @"UPDATE Rest_Expense_Category SET IsActive = @IsActive WHERE ExpenseCategoryId = @ExpenseCategoryId";
    }

    #endregion
}