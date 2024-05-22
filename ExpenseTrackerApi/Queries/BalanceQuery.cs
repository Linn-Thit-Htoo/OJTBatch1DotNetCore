namespace ExpenseTrackerApi.Queries;

public static class BalanceQuery
{
    #region Create Balance Query

    public static string CreateBalanceQuery()
    {
        return @"INSERT INTO Rest_Balance (UserId, Amount, CreateDate)
            VALUES (@UserId, @Amount, @CreateDate)";
    }

    #endregion

    #region MyRegion

    #endregion
    public static string UpdateBalanceQuery()
    {
        return @"UPDATE Rest_Balance SET Amount = @Amount, UpdateDate = @UpdateDate
WHERE UserId = @UserId";
    }
}