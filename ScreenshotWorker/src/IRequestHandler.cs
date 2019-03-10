using System.Threading.Tasks;
using MassTransit;
using Screenshot.MQ.Messages;

namespace Screenshot.Worker
{
    interface IRequestHandler
    {
        Task HandleRequest(DownloadScreenshots message, ConsumeContext context);
    }
}
