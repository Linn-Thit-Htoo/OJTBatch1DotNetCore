namespace ExpenseTrackerApi.Queries
{
    public static class BalanceQuery
    {
        public static string CreateBalanceQuery()
        {
            return @"INSERT INTO Rest_Balance (UserId, Amount, CreateDate)
            VALUES (@UserId, @Amount, @CreateDate)";
        }

        public static string UpdateBalanceQuery()
        {
            return @"UPDATE Rest_Balance SET Amount = @Amount, UpdateDate = @UpdateDate
WHERE UserId = @UserId";
        }
    }
}