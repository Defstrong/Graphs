
namespace Graph.Model
{
    class Peaks<T>
    {
        public T Data { get; set; }
        public bool Visited { get; set; }
        public static int Count { get; set; }
        public int Index { get; set; }
        public Peaks(T data)
        {
            Data = data;
        }
    }
}
