namespace Talabat.Helper
{
    public class Pagenation<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }


        public Pagenation(int PageIndex, int PageSize,int count ,IReadOnlyList<T> data)
        {
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
            this.Count = count; 
            this.Data= data;



        }
    }
}
