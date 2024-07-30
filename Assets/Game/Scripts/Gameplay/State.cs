using System;

namespace Game.Scripts.Gameplay
{
    public abstract class State : IDisposable
    {
        public abstract void Initialize();
        public abstract void Dispose();
    }
}