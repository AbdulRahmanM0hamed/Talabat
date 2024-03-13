namespace Talabat.Error
{
    public class ApiValidtionErorrResponse:ApiErorrHandling
    {
        public IEnumerable<String>Erorrs { get; set; }
        public ApiValidtionErorrResponse():base(400)
        {
            Erorrs = new List<String>();
        }
    }
}
