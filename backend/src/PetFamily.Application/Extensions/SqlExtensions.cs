using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Extensions
{
    public static class SqlExtensions
    {
        public static void ApplySorting(
            this StringBuilder stringBuilder,
            DynamicParameters parameters,
            string SortOrder,
            string SortCriterium)
        {
            parameters.Add("@SortBy", SortCriterium);
            parameters.Add("@SortOrder", SortOrder);

            stringBuilder.Append(" ORDER BY @SortBy @SortOrder");
        }

        public static void ApplyPagination(
            this StringBuilder stringBuilder,
            DynamicParameters parameters,
            int page,
            int pageSize)
        {
            parameters.Add("@Page", (page - 1) * pageSize);
            parameters.Add("@PageSize", pageSize);

            stringBuilder.Append(" LIMIT @PageSize OFFSET @Page");
        }
    }
}
