namespace ExpenseTrackerApi.Queries;

public static class IncomeCategoryQuery
{
    #region Get Income Category List Query

    public static string GetIncomeCategoryListQuery()
    {
        return @"SELECT [IncomeCategoryId]
      ,[IncomeCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Income_Category] WHERE IsActive = @IsActive ORDER BY IncomeCategoryId DESC";
    }

    #endregion

    #region Create Income Category Query

    public static string CreateIncomeCategoryQuery()
    {
        return @"INSERT INTO Rest_Income_Category (IncomeCategoryName, IsActive)
VALUES (@IncomeCategoryName, @IsActive)";
    }

    #endregion

    #region Check Create Income Category Duplicate Query

    public static string CheckCreateIncomeCategoryDuplicateQuery()
    {
        return @"SELECT [IncomeCategoryId]
      ,[IncomeCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Income_Category] WHERE IncomeCategoryName = @IncomeCategoryName AND IsActive = @IsActive";
    }

    #endregion

    #region Check Income Category Duplicate Query

    public static string CheckIncomeCategoryDuplicateQuery()
    {
        return @"SELECT [IncomeCategoryId]
      ,[IncomeCategoryName]
      ,[IsActive]
            FROM[dbo].[Rest_Income_Category] WHERE IncomeCategoryName = @IncomeCategoryName AND
          IsActive = @IsActive AND IncomeCategoryId != @IncomeCategoryId";
    }

    #endregion

    #region Update Income Category Query

    public static string UpdateIncomeCategoryQuery()
    {
        return @"UPDATE Rest_Income_Category SET IncomeCategoryName = @IncomeCategoryName 
WHERE IncomeCategoryId = @IncomeCategoryId";
    }

    #endregion

    #region Check Income Category Exists Query

    public static string CheckIncomeCategoryExistsQuery()
    {
        return @"SELECT [IncomeId]
      ,[IncomeCategoryId]
      ,[Amount]
      ,[IsActive]
  FROM [dbo].[Rest_Income] WHERE IncomeCategoryId = @IncomeCategoryId";
    }
    #endregion

    #region Delete Income Category Query

    public static string DeleteIncomeCategoryQuery()
    {
        return @"UPDATE Rest_Income_Category SET IsActive = @IsActive WHERE IncomeCategoryId = @IncomeCategoryId";
    }

    #endregion
}