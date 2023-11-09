using System.Collections.Generic;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Runtime.Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Image> stageImages = new List<Image>();
        [SerializeField] private List<TextMeshProUGUI> levelTexts = new List<TextMeshProUGUI>();
        
        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onSetLevelValue += OnSetLevelValue;
            UISignals.Instance.onSetStageColor += OnSetStageColor;
        }

        [Button("SetStageColor")]
        private void OnSetStageColor(byte stageValue)
        {
            stageImages[stageValue].DOColor(new Color(0.996f, 0.419f, 0.078f), 0.5f);
        }

        private void OnSetLevelValue(byte levelValue)
        {
            var additionalValue = ++levelValue;
            levelTexts[0].text = additionalValue.ToString();
            additionalValue++;
            levelTexts[1].text = additionalValue.ToString();
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.onSetLevelValue -= OnSetLevelValue;
            UISignals.Instance.onSetStageColor -= OnSetStageColor;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}