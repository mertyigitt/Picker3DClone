using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Input
{
    public class OnInputButtonDownCommand
    {
        private bool _isTouching;
        private bool _isFirstTimeTouchTaken;
        private Vector2? _mousePosition;
        

        public OnInputButtonDownCommand(bool isTouching, bool isFirstTimeTouchTaken, Vector2? mousePosition)
        {
            _isTouching = isTouching;
            _isFirstTimeTouchTaken = isFirstTimeTouchTaken;
            _mousePosition = mousePosition;
        }

        public void Execute()
        {
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            Debug.LogWarning("Executec ----> OnInputTaken");
            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                Debug.LogWarning("Executec ----> OnFirstTimeTouchTaken");
            }

            _mousePosition = UnityEngine.Input.mousePosition;
        }
    }
}