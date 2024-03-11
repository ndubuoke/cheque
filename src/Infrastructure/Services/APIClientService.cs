using ChequeMicroservice.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using ChequeMicroservice.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Azure;
using Serilog;

namespace ChequeMicroservice.Infrastructure.Services
{
    public class ApiClientService : IApiClientService
    {
        private readonly IRestClient _client;

        public ApiClientService(IRestClient client)
        {
            _client = client;
        }

       
        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
        public async Task<T> Get<T>(string apiUrl, string apiKey, bool isFormData = false)
        {
            try
            {
                RestRequest restRequest = new RestRequest(apiUrl);
                if (!string.IsNullOrEmpty(apiKey))
                {
                    restRequest.AddHeader("Accept", "application/json");
                    restRequest.AddHeader("Authorization", "Bearer " + apiKey);
                }
                T response = await _client.GetAsync<T>(restRequest);
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Exception occurred: {Message}", ex.InnerException?.Message);
                throw;
            }
        }


        public async Task<string> Get(string apiUrl, string apiKey, object requestObject, bool isFormData = false)
        {
            try
            {
                RestRequest restRequest = new RestRequest(apiUrl, Method.Get);
                if (!string.IsNullOrEmpty(apiKey))
                {
                    restRequest.AddHeader("Accept", "application/json");
                    restRequest.AddHeader("Authorization", "Bearer " + apiKey);
                }
                RestResponse restResponse = await _client.ExecuteAsync(restRequest);
                string responseContent = restResponse.Content;
                return responseContent;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Exception occurred: {Message}", ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<string> Post(string apiUrl, string apiKey, object requestObject, bool isFormData = false)
        {
            try
            {
                RestClient client = new RestClient(apiUrl);
                RestRequest restRequest = new RestRequest(apiUrl, Method.Post);
                if (!string.IsNullOrEmpty(apiKey))
                {
                    restRequest.AddHeader("Accept", "application/json");
                    restRequest.AddHeader("Authorization", "Bearer " + apiKey);
                }
                restRequest.AddJsonBody(requestObject);
                RestResponse restResponse = await client.ExecuteAsync(restRequest);
                string responseContent = restResponse.Content;
                return responseContent;
            }
            catch (Exception ex)
            {
                Log.Error( ex.Message, "Exception occurred: {Message}", ex.InnerException?.Message);
                throw;
            }
        }


        public async Task<T> Post<T>(ApiRequestDto request)
        {
            try
            {
                RestRequest restRequest = new RestRequest(request.ApiUrl, Method.Post) { Timeout = 300000 };
                restRequest.AddHeader("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(request.ApiKey))
                {
                    restRequest.AddHeader("Accept", "application/json");
                    if (request.ApiKey.Contains("Bearer"))
                    {
                        restRequest.AddHeader("Authorization", request.ApiKey);
                    }
                    else
                    {
                        restRequest.AddHeader("Authorization", "Bearer " + request.ApiKey);
                    }
                }
                restRequest.AddJsonBody(request.requestObject);
                RestResponse<T> restResponse = await _client.ExecuteAsync<T>(restRequest);
                if (restResponse.Data == null)
                {
                    Log.Error($"An error occured. Error code: {restResponse.StatusCode}", JsonConvert.SerializeObject(restResponse.Content));
                }
                return restResponse.Data;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Exception occurred: {Message}", ex.InnerException?.Message);
                throw;
            }
        }
    
    }
}
