using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Models
{
    public class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; init; }
        public int TotalPages { get; init; }
        public int PageSize { get; init; }
        public int Page { get; init; }
        public bool HasNextPage => TotalPages > Page * PageSize;
        public bool HasPreviousPage => Page > 1;
    }
}
