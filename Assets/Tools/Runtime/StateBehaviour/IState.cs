using System;

namespace Tools.Runtime.StateBehaviour
{
    public interface IState : IDisposable
    {
        public void Initialize();
    }
}