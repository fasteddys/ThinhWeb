using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command.PostCommands
{
    public class DeletePostCommand : IRequest<bool>
    {
    }
}
