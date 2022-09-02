using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent<GameObject> _action;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(_tag))
            {
                _action?.Invoke(col.gameObject);
            }
        }
    }
}