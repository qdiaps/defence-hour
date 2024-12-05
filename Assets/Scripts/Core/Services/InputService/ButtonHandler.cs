using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Core.Services.InputService
{
    public class ButtonHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public bool IsClick { get; private set; }

        public event Action OnClick;

        public void OnPointerUp(PointerEventData eventData) =>
            IsClick = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick?.Invoke();
            IsClick = true;
        }
    }
}
