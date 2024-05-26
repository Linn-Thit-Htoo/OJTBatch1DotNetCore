namespace ExpenseTrackerApi.Queries;

public static class IncomeCategoryQuery
{
    #region GetIncomeCategoryListQuery

    public static string GetIncomeCategoryListQuery()
    {
        return @"SELECT [IncomeCategoryId]
      ,[IncomeCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Income_Category] WHERE IsActive = @IsActive ORDER BY IncomeCategoryId DESC";
    }

    #endregion

    #region CreateIncomeCategoryQuery

    public static string CreateIncomeCategoryQuery()
    {
        return @"INSERT INTO Rest_Income_Category (IncomeCategoryName, IsActive)
VALUES (@IncomeCategoryName, @IsActive)";
    }

    #endregion

    #region CheckCreateIncomeCategoryDuplicateQuery

    public static string CheckCreateIncomeCategoryDuplicateQuery()
    {
        return @"SELECT [IncomeCategoryId]
      ,[IncomeCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Income_Category] WHERE IncomeCategoryName = @IncomeCategoryName AND IsActive = @IsActive";
    }

    #endregion

    #region CheckIncomeCategoryDuplicateQuery

    public static string CheckIncomeCategoryDuplicateQuery()
    {
        return @"SELECT [IncomeCategoryId]
      ,[IncomeCategoryName]
      ,[IsActive]
            FROM[dbo].[Rest_Income_Category] WHERE IncomeCategoryName = @IncomeCategoryName AND
          IsActive = @IsActive AND IncomeCategoryId != @IncomeCategoryId";
    }

    #endregion

    public static string UpdateIncomeCategoryQuery()
    {
        return @"UPDATE Rest_Income_Category SET IncomeCategoryName = @IncomeCategoryName 
WHERE IncomeCategoryId = @IncomeCategoryId";
    }

    public static string CheckIncomeCategoryExistsQuery()
    {
        return @"SELECT [IncomeId]
      ,[IncomeCategoryId]
      ,[Amount]
      ,[IsActive]
  FROM [dbo].[Rest_Income] WHERE IncomeCategoryId = @IncomeCategoryId";
    }

    public static string DeleteIncomeCategoryQuery()
    {
        return @"UPDATE Rest_Income_Category SET IsActive = @IsActive WHERE IncomeCategoryId = @IncomeCategoryId";
    }
}