using Runtime.Enums;
using Runtime.Extentions;
using Runtime.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onInputTaken = delegate { };
        public UnityAction onEnableInput = delegate { };
        public UnityAction onDisableInput = delegate { };
        public UnityAction onInputReleased = delegate { };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
        public UnityAction<InputState> onInputStateChanged = delegate { };
    }
}