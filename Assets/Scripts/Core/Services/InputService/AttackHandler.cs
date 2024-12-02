using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Core.Services.InputService
{
    public class AttackHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public bool IsAttack { get; private set; }

        public event Action OnAttack;

        public void OnPointerUp(PointerEventData eventData) =>
            IsAttack = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnAttack?.Invoke();
            IsAttack = true;
        }
    }
}
