﻿using CSharpFunctionalExtensions;

namespace PetFamily.Core.Abstractions
{
    public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
    {
        public Task<TResponse> Handle(TQuery query, CancellationToken cancellation);
    }

}