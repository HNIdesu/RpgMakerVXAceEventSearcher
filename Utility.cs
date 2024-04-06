using RubyMarshal.Types;

namespace RpgMakerVXAceEventSearcher
{
    internal static class Utility
    {
        
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public class SearchCommandResult(int pageIndex, string evName, string mapName,int mapId,int eventId)
        {
            public int PageIndex { get; private set; } = pageIndex;
            public string EventName { get; private set; } = evName;
            public string MapName { get; private set; } = mapName;
            public int MapID { get; private set; } = mapId;
            public int EventID { get; private set; } = eventId;
        }
        public class Command(int code, List<Base> parameters)
        {
            public int Code { get; private set; } = code;
            public List<Base> Parameters { get; private set; } = parameters;
        }
        public static IEnumerable<SearchCommandResult> SearchCommands(List<MapInfo> mapList,Func<Command,bool> func)
        {
            foreach (var map in mapList)
            {
                foreach (var ev in map.Map.AsObject()["@events"].AsHash())
                {
                    int pageIndex = 0;
                    foreach (var page in ev.Value.AsObject()["@pages"].AsArray())
                    {
                        bool evFlag = false;
                        foreach (var _cmd in page.AsObject()["@list"].AsArray())
                        {
                            var cmd = _cmd.AsObject();
                            var code = cmd["@code"].AsFixnum().ToInt32();
                            var parameters = cmd["@parameters"].AsArray();
                            var command = new Command(code, [.. parameters]);
                            if (func(command))
                            {
                                yield return new SearchCommandResult(
                                    pageIndex,
                                    ev.Value.AsObject()["@name"].AsInstanceVariable().Name.ToString(), 
                                    map.Name,
                                    map.ID,
                                    ev.Value.AsObject()["@id"].AsFixnum().ToInt32()
                                    ); 
                                break;
                            }
                        }
                        pageIndex++;
                    }
                }
            }
        }

    }
}
