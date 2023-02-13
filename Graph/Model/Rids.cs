
namespace Graph.Model
{
    sealed class Rids<T>
    {
        public Peaks<T> Before { get; set; }
        public Peaks<T> After { get; set; }
        public bool Directed { get; set; }
        public int Price { get; set; }
        public Rids(Peaks<T> before, Peaks<T> after,int price = 1, bool directed = false)
        {
            Before = before;
            After = after;
            Price = price;
            Directed = directed;
        }
    }
}
