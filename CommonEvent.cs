using RubyMarshal.Types;

namespace RpgMakerVXAceEventSearcher
{
    internal sealed class CommonEvent(int id, string name)
    {
        public Base? Data { get; set; }
        public string Name { get; set; } = name;
        public int ID { get; set; } = id;
    }
}
