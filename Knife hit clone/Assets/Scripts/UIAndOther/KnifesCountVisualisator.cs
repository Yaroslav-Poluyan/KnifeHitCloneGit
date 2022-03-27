using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIAndOther
{
    public class KnifesCountVisualisator : MonoBehaviour
    {
        public static KnifesCountVisualisator Current;
        [SerializeField] private GameObject parentOfImages;
        [SerializeField] private List<Image> _imagesRenderers = new List<Image>();
        private Transform _transform;
        private Color disabledColor = new Color(1, 1, 1, 0);
        public void SetCurrentNeededCount(int count)
        {
            foreach (var image in _imagesRenderers)
            {
                image.color = Color.white;
            }
            for (int i = 0; i < _imagesRenderers.Count - count; i++)
            {
                _imagesRenderers[i].color = disabledColor;
            }
        }
        private void Start()
        {
            _imagesRenderers.AddRange(parentOfImages.GetComponentsInChildren<Image>());
        }

        private void Awake()
        {
            _transform = transform;
            Current = this;
        }
    }
}