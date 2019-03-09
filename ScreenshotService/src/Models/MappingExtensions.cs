using System;
using System.Linq;
using Screenshot.Service.Entity;

namespace Screenshot.Service.Models
{
    // We don't want to leak database details, so here's the mapping logic from db entities
    static class MappingExtensions
    {
        public static ScreenshotModel ToModel(this ScreenshotEntity dbEntity)
        {
            return new ScreenshotModel {
                Path = dbEntity.Path,
                Url = dbEntity.Url,
                Status = dbEntity.Status
            };

        }
        public static RequestModel ToModel(this RequestEntity dbEntity)
        {
            return new RequestModel {
                Guid = dbEntity.Guid,
                DateTime = dbEntity.DateTime,
                Screenshots = dbEntity.Screenshots?.Select(ToModel).ToList()
            };
        }

    }

}