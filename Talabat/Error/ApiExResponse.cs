namespace Talabat.Error
{
    public class ApiExResponse:ApiErorrHandling
    {
        public string Detaiels { get; set; }
        public ApiExResponse(int StatusCode, string? ErrorMessage = null,string? Detaiels =null):base(StatusCode, ErrorMessage)
        {
            this.Detaiels = Detaiels;
        }


    }
}
