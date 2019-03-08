using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Screenshot.Messages;

namespace Screenshot.Service
{
    public class ScreenshotSavedConsumer : IConsumer<ScreenshotSaved>
    {
        private Context _dbContext;
        public ScreenshotSavedConsumer(Context dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ScreenshotSaved> context)
        {
            Console.WriteLine("Consumed! " + context.Message.Guid);
            var request = _dbContext.ScreenshotRequests.Single(e => e.Guid == context.Message.Guid);

            request.File = context.Message.File;
            await _dbContext.SaveChangesAsync();
        }

    }

}