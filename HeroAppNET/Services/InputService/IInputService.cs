using System.Windows.Input;

namespace HeroAppNET.Services.InputService
{
    public interface IInputService
    {
        IEnumerable<Key> PressedKeys { get; }

        bool IsKeyPressed(Key key);
    }
}