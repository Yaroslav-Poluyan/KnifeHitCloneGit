using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkyBoxManager : MonoBehaviour
{
        public static SkyBoxManager Current;
        [SerializeField] private List<Color> colorsUp = new List<Color>();
        [SerializeField] private List<Color> colorsDown = new List<Color>();
        [SerializeField] private List<Texture> noiseImages = new List<Texture>();
        [SerializeField] private float changingSpeed;
        [SerializeField] private Vector4 pulseRate = new Vector4(.7f, .7f, .7f, 1f);
        private int _colorUpID;
        private int _colorDownID;
        private int _noiseID;
        private int _targetUpColor;
        private int _targetBottomColor;
        private bool _isChangingColor;

        public void MakeBackgroundPulse()
        {
                var upGlobalColor = Shader.GetGlobalColor(_colorUpID);
                var downGlobalColor = Shader.GetGlobalColor(_colorDownID);
                upGlobalColor *= pulseRate;
                downGlobalColor *= pulseRate;
                Shader.SetGlobalColor(_colorUpID, upGlobalColor);
                Shader.SetGlobalColor(_colorDownID, downGlobalColor);
        }
        public void SetNewBackGroundColors()
        {
                _targetUpColor = Random.Range(0, colorsUp.Count);
                _targetBottomColor = Random.Range(0, colorsDown.Count);
                Shader.SetGlobalTexture(_noiseID, noiseImages[Random.Range(0, noiseImages.Count)]);
                _isChangingColor = true;
        }

        private void Update()
        {
                if (!_isChangingColor) return;
                var upGlobalColor = Shader.GetGlobalColor(_colorUpID);
                var downGlobalColor = Shader.GetGlobalColor(_colorDownID);
                var deltaUp = Color.Lerp(upGlobalColor, colorsUp[_targetUpColor],
                        changingSpeed * Time.deltaTime);
                var deltaDown = Color.Lerp(downGlobalColor, colorsDown[_targetBottomColor],
                        changingSpeed * Time.deltaTime);
                Shader.SetGlobalColor(_colorUpID, deltaUp);
                Shader.SetGlobalColor(_colorDownID, deltaDown);
        }

        private void Awake()
        {
                Current = this;
                _colorUpID = Shader.PropertyToID("_ColorUp");
                _colorDownID = Shader.PropertyToID("_ColorDown");
                _noiseID = Shader.PropertyToID("_NoiseTex");
                SetNewBackGroundColors();
        }
}