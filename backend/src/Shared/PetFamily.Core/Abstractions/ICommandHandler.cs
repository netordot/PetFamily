using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.Core.Abstractions
{
    public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
    {
        public Task<Result<TResponse, ErrorList>> Handle(TCommand command, CancellationToken cancellation);
    }

    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        public Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken cancellation);
    }

}
