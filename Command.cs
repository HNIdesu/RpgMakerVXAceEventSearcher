using RubyMarshal.Types;

namespace RpgMakerVXAceEventSearcher
{
    internal class Command(int code, List<Base> parameters)
    {
        public int Code { get; private set; } = code;
        public List<Base> Parameters { get; private set; } = parameters;
        public Base? GetParameter(int index)
        {
            if (index < Parameters.Count)
                return Parameters[index];
            else return null;
        }
    }
}
