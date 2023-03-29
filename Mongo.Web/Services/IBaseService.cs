using Mongo.Web.Models;

namespace Mongo.Web.Services
{
    public interface IBaseService: IDisposable
    {
        ResponseDto responseModel { get; set; }
        Task<T> SendAync<T>(ApiRequest apiRequest);
    }
}
