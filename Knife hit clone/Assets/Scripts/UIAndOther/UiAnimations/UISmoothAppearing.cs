using UnityEngine;

namespace UIAndOther.UiAnimations
{
    public class UISmoothAppearing : MonoBehaviour
    {
        [SerializeField] private Transform targetPointForDissapear;
        [SerializeField] private float appearingSpeed = 2f;
        private Vector3 _targetSize = new Vector3(-1, -1, -1);
        private Vector3 _defaultSize;
        private Vector3 _defaultPosition;
        private Transform _transform;
        private void OnEnable()
        {
            _targetSize = _defaultSize;
            _transform.position = targetPointForDissapear.position;
            _transform.localScale = Vector3.zero;
        }

        private void OnDisable()
        {
            _transform.position = targetPointForDissapear.position;
            _transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            if (_targetSize != new Vector3(-1, -1, -1))
            {
                _transform.localScale =
                    Vector3.Lerp(_transform.localScale, _defaultSize, appearingSpeed * Time.deltaTime);
                _transform.position =
                    Vector3.Lerp(_transform.position, _defaultPosition, appearingSpeed * Time.deltaTime);
                if (_transform.position == _defaultPosition)
                {
                   _targetSize = new Vector3(-1, -1, -1);
                }
            }
        }

        private void Awake()
        {
            _transform = transform;
            _defaultSize = _transform.localScale;
            _defaultPosition = _transform.position;
            _targetSize = new Vector3(-1, -1, -1);
        }
    }
}