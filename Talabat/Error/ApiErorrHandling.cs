using Microsoft.AspNetCore.Http;

namespace Talabat.Error
{
    public class ApiErorrHandling
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public ApiErorrHandling(int StatusCode ,string? ErrorMessage =null)
        {
            this.StatusCode = StatusCode;
            this.ErrorMessage = ErrorMessage ?? GetDefualtMessageForStatusCode(StatusCode);

        }


        private string? GetDefualtMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad Requt ,you have made",
                401=>"Athruzed are you not",
                404=>"Resurses Not Found",
                500=>"theer is server Eroee",
                _=>null,
            };
        }


    }
}
