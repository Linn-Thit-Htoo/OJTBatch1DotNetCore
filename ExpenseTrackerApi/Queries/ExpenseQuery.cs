namespace ExpenseTrackerApi.Queries;

public static class ExpenseQuery
{
    #region Get Expense List Query

    public static string GetExpenseListQuery()
    {
        return @"SELECT ExpenseId, Rest_Expense_Category.ExpenseCategoryName, Rest_Users.UserName, Amount, Rest_Expense.IsActive,
Rest_Expense.CreateDate
FROM Rest_Expense
INNER JOIN Rest_Expense_Category ON Rest_Expense.ExpenseCategoryId = Rest_Expense_Category.ExpenseCategoryId
INNER JOIN Rest_Users ON Rest_Expense.UserId = Rest_Users.UserId
WHERE Rest_Expense.IsActive = @IsActive
ORDER BY ExpenseId DESC";
    }

    #endregion

    #region Get Expense Lis tByUserId Query

    #endregion
    public static string GetExpenseListByUserIdQuery()
    {
        return @"SELECT ExpenseId, Rest_Expense_Category.ExpenseCategoryName, Rest_Users.UserName, Amount, Rest_Expense.IsActive,
Rest_Expense.CreateDate
FROM Rest_Expense
INNER JOIN Rest_Expense_Category ON Rest_Expense.ExpenseCategoryId = Rest_Expense_Category.ExpenseCategoryId
INNER JOIN Rest_Users ON Rest_Expense.UserId = Rest_Users.UserId
WHERE Rest_Expense.IsActive = @IsActive AND Rest_Expense.UserId = @UserId
ORDER BY ExpenseId DESC";
    }

    public static string CreateExpenseQuery()
    {
        return @"INSERT INTO Rest_Expense (ExpenseCategoryId, UserId, Amount, CreateDate, IsActive)
VALUES (@ExpenseCategoryId, @UserId, @Amount, @CreateDate, @IsActive)";
    }

    public static string UpdateExpenseQuery()
    {
        return @"UPDATE Rest_Expense SET ExpenseCategoryId = @ExpenseCategoryId, 
Amount = @Amount WHERE ExpenseId = @ExpenseId AND UserId = @UserId";
    }

    public static string DeleteExpenseQuery()
    {
        return @"UPDATE Rest_Expense SET IsActive = @IsActive WHERE ExpenseId = @ExpenseId";
    }
}