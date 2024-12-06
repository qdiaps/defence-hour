using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace Core.UI.HUD.Movement
{
    public class AttackCooldown : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void StartAnim(float time, Action action)
        {
            _image.DOFillAmount(1, time)
                .From(0)
                .SetEase(Ease.Linear)
                .OnComplete(() => action.Invoke());
        }
    }
}
