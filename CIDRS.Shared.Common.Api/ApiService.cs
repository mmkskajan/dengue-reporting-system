using CIDRS.Shared.Common.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Shared.Common.Api
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _httpContextAccessor;
        public ApiService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ResponseResult<TResult>> GetAsync<TResult>(string baseUrl, string url,bool useContexttoken = true, string accessToken = "")
        {

            using var client = await GetHttpClientAsync(baseUrl,useContexttoken,accessToken);
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    return GetSuccessResponse(apiResponse.DeserializeObject<TResult>());
                }
                catch (Exception ex)
                {
                    return GetErrorResponse<TResult>(new string[] { ex.Message });
                }
            }
            else
            {
                return GetErrorResponse<TResult>(new string[] { string.Format("Error with Status Code : {0}", response.StatusCode.ToString()) });
            }

        }

        public async Task<ResponseResult<TResult>> PostAsync<TResult>(string baseUrl, string url, string jsonData, bool useContexttoken = true, string accessToken = "")
        {
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using var client = await GetHttpClientAsync(baseUrl,useContexttoken,accessToken);
            var response = await client.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    return GetSuccessResponse(apiResponse.DeserializeObject<TResult>());
                }
                catch (Exception ex)
                {
                    return GetErrorResponse<TResult>(new string[] { ex.Message });
                }
            }
            else
            {
                return GetErrorResponse<TResult>(new string[] { string.Format("Error with Status Code : {0}", response.StatusCode.ToString()) });
            }

        }

        public async Task<ResponseResult<TResult>> PostAsync<TResult, TRequestData>(string baseUrl, string url, TRequestData requestData, bool useContexttoken = true, string accessToken = "")
        {
            var jsonData = JsonConvert.SerializeObject(requestData);
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using var client = await GetHttpClientAsync(baseUrl,useContexttoken,accessToken);
            var response = await client.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    return GetSuccessResponse(apiResponse.DeserializeObject<TResult>());
                }
                catch (Exception ex)
                {
                    return GetErrorResponse<TResult>(new string[] { ex.Message });
                }
            }
            else
            {
                return GetErrorResponse<TResult>(new string[] { string.Format("Error with Status Code : {0}", response.StatusCode.ToString()) });
            }

        }


        #region private methods
        private async Task<HttpClient> GetHttpClientAsync(string apibaseUrl,bool useContexttoken = true, string accessToken = "")
        {
            var token = useContexttoken ? await _httpContextAccessor.HttpContext.GetTokenAsync("access_token"): accessToken;
            int timeOutInMinutes = 5;
            var baseUrl = apibaseUrl;
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(timeOutInMinutes);
            client.BaseAddress = new Uri(baseUrl);
            if (!string.IsNullOrWhiteSpace(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        private ResponseResult<TResult> GetErrorResponse<TResult>(string[] errorMessages)
        {
            return new ResponseResult<TResult>() { Errors = errorMessages, Result = default(TResult), Succeeded = false };
        }


        private ResponseResult<TResult> GetSuccessResponse<TResult>(TResult result)
        {
            return new ResponseResult<TResult>() { Errors = null, Result = result, Succeeded = true };
        }
        #endregion


    }
}
