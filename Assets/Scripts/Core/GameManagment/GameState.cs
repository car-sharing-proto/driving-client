using System;

namespace Core.GameManagment
{
    public class GameState 
    {
        private bool _isPause;

        public Action OnPause { get; private set; }
        public Action OnUnpause { get; private set; }

        public bool IsPause => _isPause;
        public bool IsUnpause => !_isPause;

        public GameState()
        {
            _isPause = false;
        }

        public void Pause()
        {
            _isPause = true;

            OnPause?.Invoke();
        }

        public void Unpause()
        {
            _isPause = false;

            OnUnpause?.Invoke();
        }

        public void SwitchPauseState()
        {
            _isPause = !_isPause;
        }
    }
}
