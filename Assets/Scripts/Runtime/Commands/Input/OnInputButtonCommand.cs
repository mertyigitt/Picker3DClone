using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Runtime.Signals;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Commands.Input
{
    public class OnInputButtonCommand
    {
        private bool _isTouching;
        private float _currentVelocity;
        private Vector2? _mousePosition;
        private InputData _data;
        private float3 _moveVector;

        public OnInputButtonCommand( bool isTouching, Vector2? mousePosition, InputData data, float3 moveVector, float currentVelocity)
        {
            _isTouching = isTouching;
            _mousePosition = mousePosition;
            _data = data;
            _moveVector = moveVector;
            _currentVelocity = currentVelocity;
        }


        public void Execute()
        {
            if (_isTouching)
            {
                if (_mousePosition != null)
                {
                    Vector2 mouseDeltaPos = (Vector2)UnityEngine.Input.mousePosition - _mousePosition.Value;
                    if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                    {
                        _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }
                    else if(mouseDeltaPos.x < _data.HorizontalInputSpeed)
                    {
                        _moveVector.x = -_data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }
                    else
                    {
                        _moveVector.x = Mathf.SmoothDamp(-_moveVector.x,0f,ref _currentVelocity,_data.ClampSpeed);
                    }

                    _mousePosition = UnityEngine.Input.mousePosition;
                        
                    InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                    {
                        HorizontalValue = _moveVector.x,
                        ClampValues = _data.ClampValues
                    });
                }
            }
        }
    }
}