using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Screenshot.MQ.Messages;
using Screenshot.Service.Entity;

namespace Screenshot.Service.MQ.Consumer
{
    public class ScreenshotSavedConsumer : IConsumer<ScreenshotSaved>
    {
        private ScreenshotContext _dbContext;
        public ScreenshotSavedConsumer(ScreenshotContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ScreenshotSaved> context)
        {
            // Find entry for screenshot which was saved
            var screenshot = _dbContext.Screenshots.Single(e => e.RequestGuid == context.Message.Guid && e.Url == context.Message.Url);
            screenshot.Path = context.Message.Filename;
            screenshot.Status = "Success";

            await _dbContext.SaveChangesAsync();
        }
    }
}