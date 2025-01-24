namespace RpgMakerVXAceEventSearcher
{
    internal class SearchCommandResult(
        int pageIndex,
        string eventName,
        string mapName,
        int mapId,
        int eventId,
        Point location
    )
    {
        public int PageIndex { get; private set; } = pageIndex;
        public string EventName { get; private set; } = eventName;
        public string MapName { get; private set; } = mapName;
        public int MapID { get; private set; } = mapId;
        public int EventID { get; private set; } = eventId;
        public Point Location { get; private set; } = location;
    }
}
