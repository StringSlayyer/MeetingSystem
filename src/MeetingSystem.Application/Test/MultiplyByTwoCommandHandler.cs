using MeetingSystem.Application.Abstractions.Messaging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Test
{
    public class MultiplyByTwoCommandHandler : ICommandHandler<MultiplyByTwoCommand, int>
    {
        public async Task<Result<int>> Handle(MultiplyByTwoCommand command, CancellationToken cancellationToken)
        {
            var result = command.number * 2;
            return result;
        }
    }
}
