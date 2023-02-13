using Graph.Model;
using System.Collections;

namespace Graph.Services
{
    sealed class Graph<T> : IEnumerable<T>
    {
        List<Rids<T>> rids = new List<Rids<T>>();
        List<Peaks<T>> peaks = new List<Peaks<T>>();
        public int Count { get; private set; }

        private Peaks<T> FindPeak(int idxFindPeak)
        {
            foreach(var ii in peaks)
            {
                if(ii.Index.Equals(idxFindPeak)) return ii;
            }
            return null;
        }

        private Rids<T> FindRids(Peaks<T> findRids)
        {
            foreach(var ii in rids)
            {
                if(ii.Before.Equals(findRids) || ii.After.Equals(findRids)) return ii;
            }
            return null;
        }
        public void AddPeak(T item)
        {
            var createPeak = new Peaks<T>(item);
            Peaks<T>.Count++;
            createPeak.Index = Peaks<T>.Count;
            Count++;
            peaks.Add(createPeak);
        }

        public void AddRids(int idxBefore, int IdxAfter, int price = 1, bool directed = false)
        {
            var createRids = new Rids<T>(FindPeak(idxBefore), FindPeak(IdxAfter), price, directed);

            rids.Add(createRids);
        }
        public int FindPeakForMatrix(int idxPeak)
        {
            foreach(var ii in rids)
            {
                if (ii.Before.Index == idxPeak)
                    return ii.Before.Index;
                else if(ii.After.Index == idxPeak)
                    return ii.After.Index;
            }
            return -1;
        }
        public bool[,] GetConnectMatrix()
        {
            
            bool[,] matrixGraph = new bool[peaks.Count(), peaks.Count()];
            foreach(var ii in rids)
            {
                var row = ii.Before.Index;
                var column = ii.After.Index;
                matrixGraph[row-1, column-1] = true;
            }

            return matrixGraph;
        }

        public bool DeletePeaks(int idxPeakForDelete)
        {
            if (peaks.Count != 0)
            {
                var peakForDelete = FindPeak(idxPeakForDelete);
                var ridsForDisconnect = FindRids(peakForDelete);
                idxPeakForDelete = -1;
                ridsForDisconnect = null;
                return true;
            }
            return false;
        }
        public List<Peaks<T>> GetListPeaks(int idxPeak)
        {
            var resultList = new List<Peaks<T>>();

            foreach(var ii in rids)
            {
                if(ii.Before.Index == idxPeak)
                    resultList.Add(ii.After);
            }
            return resultList;
        }

        public bool GetRoad(int idxStart, int idxFinish)
        {
            var listPeaks = new List<int>();
            listPeaks.Add(idxStart);

            for(int i = 0; i < listPeaks.Count; i++)
            {
                var vertex = listPeaks[i];
                foreach(var ii in GetListPeaks(vertex))
                {
                    if(!ii.Visited)
                    {
                        ii.Visited = true;
                        listPeaks.Add(ii.Index);
                    }
                }
            }
            return listPeaks.Contains(idxFinish);

        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var ii in peaks)
                yield return ii.Data;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
