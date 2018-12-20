using System;
using System.Threading.Tasks;
using Christ.Domain.Core.Commands;

namespace Example.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
    }
}
