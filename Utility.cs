using Arche.RpgMaker2MV.VXACE;
using System.Text.Json.Nodes;

namespace RpgMakerVXAceEventSearcher
{
    internal static class Utility
    {
        public static JsonObject RvdataToJson(string filename)
        {
            return null;
        }
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }


    }
}
