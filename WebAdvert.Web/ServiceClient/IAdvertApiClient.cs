﻿using AdvertApi.Models;

namespace WebAdvert.Web.ServiceClient
{
    public interface IAdvertApiClient
    {
        Task<AdvertResponse> CreateAsync(CreateAdvertModel model);
        Task<bool> ConfirmAsync(ConfirmAdvertRequest model);

        Task<List<Advertisement>> GetAllAsync();
    }
}
