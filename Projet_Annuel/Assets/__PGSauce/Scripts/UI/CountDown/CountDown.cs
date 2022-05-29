using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using PGSauce.AudioManagement;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.Events;

namespace PGSauce.UI
{
    public class CountDown : PGMonoBehaviour
    {
        [Serializable]
        public struct NumberData
        {
            public string text;
            public UIElementData uiElementData;
            public List<PgSfxBase> uiSounds;
        }

        [SerializeField] private List<NumberData> numbers;
        [SerializeField] private TMP_Text countDownText;
        [SerializeField] private UIElement uiElement;
        [SerializeField] private UnityEvent onCountDownFinish;

        private Sequence _showCountDownTween;

        private void Awake()
        {
            uiElement.Initialize();
            uiElement.ChangeVisibility(false, true);
        }
        
        public void ChangeVisibility(bool visible, bool noSounds = false)
        {
            _showCountDownTween?.Kill();
            if (!visible)
            {
                uiElement.ChangeVisibility(false, noSounds);
            }
            else
            {
                _showCountDownTween = DOTween.Sequence();
                var i = 0;
                foreach (var number in numbers)
                {
                    _showCountDownTween
                        .AppendCallback(() =>
                        {
                            uiElement.SetUIData(number.uiElementData);
                            uiElement.ChangeVisibility(true, noSounds);
                            countDownText.text = number.text;
                            if (PgAudioManager.Instance && number.uiSounds.Count >= 1)
                            {
                                foreach (var uiSound in number.uiSounds)
                                {
                                    PgAudioManager.Sfx.Play(uiSound);
                                }
                            }
                        });
                        
                        var showTime = uiElement.GetAllShowingTime(number.uiElementData);

                        _showCountDownTween
                            .AppendInterval(showTime);

                        if (i == numbers.Count - 1)
                        {
                            _showCountDownTween.AppendCallback(() => onCountDownFinish.Invoke());
                        }
                        
                        _showCountDownTween
                            .AppendCallback(() =>
                            {
                                uiElement.ChangeVisibility(false, noSounds);
                            });
                        var hideTime = uiElement.GetAllHidingTime(number.uiElementData);
                        _showCountDownTween.AppendInterval(hideTime);

                        i++;
                }
            }
        }

        [Button, DisableInEditorMode]
        public void ShowCountDown()
        {
            ChangeVisibility(true);
        }
    }
}
