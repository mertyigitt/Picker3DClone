using System.Collections.Generic;
using Runtime.Commands.Input;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private InputData _data;
        private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching;

        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _mousePosition;
        private InputState _state;

        private OnInputButtonUpCommand _onInputButtonUpCommand;
        private OnInputButtonDownCommand _onInputButtonDownCommand;
        private OnInputButtonCommand _onInputButtonCommand;
        

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetInputData();
            //Init();
        }

        /*private void Init()
        {
            _onInputButtonUpCommand = new OnInputButtonUpCommand(_isTouching);
            _onInputButtonDownCommand = new OnInputButtonDownCommand(_isTouching, _isFirstTimeTouchTaken, _mousePosition);
            _onInputButtonCommand = new OnInputButtonCommand(_isTouching, _mousePosition, _data, _moveVector, _currentVelocity);
        }*/


        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/UnityObjects/CD_Input").Data;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += ODisableInput;
            //InputSignals.Instance.onInputStateChanged += OnInputStateChanged;
        }

        private void ODisableInput()
        {
            _isAvailableForTouch = false;
        }

        private void OnEnableInput()
        {
            _isAvailableForTouch = true;
        }

        private void OnReset()
        {
            _isAvailableForTouch = false;
            //_isFirstTimeTouchTaken = false;
            _isTouching = false;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset  -= OnReset;
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= ODisableInput;
            //InputSignals.Instance.onInputStateChanged -= OnInputStateChanged;
        }

        /*private void OnInputStateChanged(InputState inputState)
        {
            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _state = InputState.ButtonUp;
            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _state = InputState.ButtonDown;
            }

            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                _state = InputState.Button;
            }
        }*/

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void Update()
        {
            if (!_isAvailableForTouch) return;

            /*switch (_state)
            {
                case InputState.Button:
                    _onInputButtonCommand.Execute();
                    break;
                case InputState.ButtonDown:
                    _onInputButtonDownCommand.Execute();
                    break;
                case InputState.ButtonUp:
                    _onInputButtonUpCommand.Execute();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                
            }*/

            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
                Debug.LogWarning("Executec ----> OnInputReleased");
            }
            

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
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

                _mousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                if (_isTouching)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
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

                        _mousePosition = Input.mousePosition;
                        
                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                        {
                            HorizontalValue = _moveVector.x,
                            ClampValues = _data.ClampValues
                        });
                    }
                }
            }
        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}