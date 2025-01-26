namespace RpgMakerVXAceEventSearcher
{
    internal sealed class SearchResult(
        int pageIndex,
        string eventName,
        string mapName,
        int mapId,
        int eventId,
        Point location
    )
    {
        //公共事件
        public SearchResult(int eventId, string eventName):this(
            pageIndex: 0,
            eventName: eventName,
            mapName: "Common Event",
            mapId: 0,
            eventId: eventId,
            location: new Point())
        {
            IsCommonEvent = true;
        }
        public int PageIndex { get; private set; } = pageIndex;
        public string EventName { get; private set; } = eventName;
        public string MapName { get; private set; } = mapName;
        public int MapID { get; private set; } = mapId;
        public int EventID { get; private set; } = eventId;
        public bool IsCommonEvent { get; private set; } = false;
        public Point Location { get; private set; } = location;
    }
}
