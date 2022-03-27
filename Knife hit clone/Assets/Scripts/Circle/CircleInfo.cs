using System;
using fruits_etc;
using UnityEngine;

namespace Circle
{
    public class CircleInfo : MonoBehaviour
    {
        public bool isReadyForInteracting = false;
        public GameObject fullCircleModel;
        public GameObject type0Destruction;
        public GameObject type1Destruction;
        public GameObject type2Destruction;
        [SerializeField] private Color onStuckColor;
        private Vector3 _defaultSize;
        private Vector3 _targetSize;
        private Color _defaulColor;
        private Color _targetColor;
        private Transform _transform;
        private MeshRenderer _meshRenderer;
        private bool _isStartScaling;

        public void KnifeStuckAnimations()
        {
            _targetColor = onStuckColor;
            _targetSize = _defaultSize * 1.1f;
        }

        private void Update()
        {
            if (_isStartScaling)
            {
                if (_transform.localScale != Vector3.one)
                    _transform.localScale =
                        Vector3.MoveTowards(_transform.localScale, Vector3.one, 5f * Time.deltaTime);
                else if (_transform.localScale == Vector3.one)
                {
                    _isStartScaling = false;
                    isReadyForInteracting = true;
                }
            }
            else
            {
                _transform.localScale = Vector3.MoveTowards(_transform.localScale, _targetSize, 2f * Time.deltaTime);
                if (_transform.localScale == _targetSize) _targetSize = _defaultSize;
            }

            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _targetColor, 2f);
            if (_meshRenderer.material.color == _targetColor) _targetColor = _defaulColor;
        }

        private void Start()
        {
            _meshRenderer = fullCircleModel.GetComponent<MeshRenderer>();
            _targetColor = _defaulColor = _meshRenderer.material.color;
        }

        private void Awake()
        {
            _transform = transform;
            _targetSize = _defaultSize = _transform.localScale;
            _transform.localScale = Vector3.zero;
            _isStartScaling = true;
            isReadyForInteracting = false;
        }
    }
}
