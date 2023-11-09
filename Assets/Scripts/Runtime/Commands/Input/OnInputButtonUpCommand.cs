using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Input
{
    public class OnInputButtonUpCommand
    {
        private bool _isTouching;

        public OnInputButtonUpCommand(bool isTouching)
        {
            _isTouching = isTouching;
        }

        public void Execute()
        {
            _isTouching = false;
            InputSignals.Instance.onInputReleased?.Invoke();
            Debug.LogWarning("Executec ----> OnInputReleased");
        }
    }
}