namespace Airport.RouteCalculator.Infrastructure.Data
{
    public static class QueriesExtensions
    {
        public static string PagedRoutes => 
                @"SELECT *
                  FROM Routes
                  ORDER BY CreatedAt
                  OFFSET (@page -1 ) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
    }
}
