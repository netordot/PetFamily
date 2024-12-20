﻿using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Extensions
{
    public static class QuerriesExtensions
    {
        public static async Task<PagedList<T>> ToPagedList<T>(
            this IQueryable<T> source, int page, int size, CancellationToken cancellation)
        {
            var totalCount = await source.CountAsync(cancellation);

            var items = await source.Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellation);

            var result = new PagedList<T>
            {
                Items = items,
                TotalPages = totalCount,
                Page = page,
                PageSize = size
            };

            return result;

        }

        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;   
        }
    }
}
