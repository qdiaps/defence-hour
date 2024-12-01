using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Core.Services.InputService
{
    public class SwipeHandler : MonoBehaviour, IDragHandler
    {
        public event Action<int> OnHorizontalSwipe;
        public event Action<int> OnVerticalSwipe;

        public void OnDrag(PointerEventData eventData)
        {
            float deltaX = eventData.delta.x;
            float deltaY = eventData.delta.y;
            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                if (OnHorizontalSwipe == null)
                    return;
                OnHorizontalSwipe.Invoke(deltaX > 0 ? -1 : 1);
            }
            else
            {
                if (OnVerticalSwipe == null)
                    return;
                OnVerticalSwipe.Invoke(deltaY > 0 ? -1 : 1);
            }
        }
    }
}
