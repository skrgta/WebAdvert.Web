using AdvertApi.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAdvert.SearchWorker
{
    internal static class MappingHelper
    {
        public static AdvertType Map(AdvertConfirmedMessage advertConfirmedMessage)
        {
            return new AdvertType()
            {
                Id = advertConfirmedMessage.Id,
                Title = advertConfirmedMessage.Title,
                CreateionDateTime = DateTime.UtcNow
            };
        }
    }
}
