using CIDRS.Shared.Common.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Shared.Common.Api
{
    public interface IApiService
    {
        Task<ResponseResult<TResult>> GetAsync<TResult>(string baseUrl, string url, bool useContexttoken = true, string accessToken = "");
        Task<ResponseResult<TResult>> PostAsync<TResult>(string baseUrl, string url, string jsonData, bool useContexttoken = true, string accessToken = "");
        Task<ResponseResult<TResult>> PostAsync<TResult, TRequestData>(string baseUrl, string url, TRequestData requestData, bool useContexttoken = true, string accessToken = "");
    }
}
