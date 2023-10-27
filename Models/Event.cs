using TerrariaApi.Server;

namespace TShockTutorials.Models
{
    public abstract class Event // <--- emphasis on the abstract class
    {
        public abstract void Enable(TerrariaPlugin plugin);
        public abstract void Disable(TerrariaPlugin plugin);
        public void EventMethod() { }
    }
}
