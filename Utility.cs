using RubyMarshal.Types;
using System.Xml.Linq;

namespace RpgMakerVXAceEventSearcher
{
    internal static class Utility
    {
        public static IEnumerable<SearchCommandResult> SearchFromCommands(List<MapInfo> mapList,Func<Command,bool> func)
        {
            foreach (var map in mapList)
            {
                foreach (var ev in map.Map.AsObject()["@events"].AsHash())
                {
                    int pageIndex = 0;
                    foreach (var page in ev.Value.AsObject()["@pages"].AsArray())
                    {
                        foreach (var _cmd in page.AsObject()["@list"].AsArray())
                        {
                            var cmd = _cmd.AsObject();
                            var code = cmd["@code"].AsFixnum().ToInt32();
                            var parameters = cmd["@parameters"].AsArray();
                            var command = new Command(code, [.. parameters]);
                            if (func(command))
                            {
                                yield return new SearchCommandResult(
                                    pageIndex: pageIndex,
                                    eventName: ev.Value.AsObject()["@name"].AsInstanceVariable().Base.AsString().Value, 
                                    mapName: map.Name,
                                    mapId: map.ID,
                                    eventId: ev.Value.AsObject()["@id"].AsFixnum().ToInt32(),
                                    location: new Point(
                                        x: ev.Value.AsObject()["@x"].AsFixnum().ToInt32(),
                                        y: ev.Value.AsObject()["@y"].AsFixnum().ToInt32()
                                    )
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
