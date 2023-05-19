using System;
namespace Project.Network.Interface
{
    public interface IApiClient
    {
        public Task<string> JsonGetDataAsync(string endPoint);
        //other http request method goes here
    }
}

