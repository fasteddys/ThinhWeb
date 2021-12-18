using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command.Post
{
    public class DeletePostCommand : IRequest<bool>
    {
    }
}
