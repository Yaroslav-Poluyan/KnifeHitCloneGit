using System;
using UnityEngine;

namespace UIAndOther.UiAnimations
{
    public class ButtonAnimation : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        private Vector3 _targetSize = new Vector3(-1, -1, -1);
        private readonly Vector3 _defaultSize = Vector3.one;

        private void OnEnable()
        {
            _targetSize = _defaultSize;
        }

        private void OnDisable()
        {
           transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            if (_targetSize != Vector3.zero)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, _targetSize, speed * Time.deltaTime);
                if(transform.localScale == _targetSize) _targetSize = Vector3.zero;
            }
        }

        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }
    }
}
