﻿using ChequeMicroservice.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.Common.Interfaces
{
    public interface IAPIClientService
    {
        Task<string> Get(string apiUrl, string apiKey, object requestObject, bool isFormData = false);
        Task<T> Get<T>(string apiUrl, string apiKey, bool isFormData = false);
        Task<string> Post(string apiUrl, string apiKey, object requestObject, bool isFormData = false);
        Task<T> Post<T>(ApiRequestDto request);
    }
}
