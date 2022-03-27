using System.Collections.Generic;
using fruits_etc;
using Knife;
using UnityEngine;

namespace Circle
{
    public class CircleController : MonoBehaviour
    {
        public static CircleController Current;
        public Vector3 centerOfCircle;
        public float circleRadius;
        public CircleInfo currentCircleInfo;
        [SerializeField] private Vector3 angularVelocityRandom;
        [SerializeField] private Vector3 velocityRandom;
        [SerializeField] private List<GameObject> circlePrefabs = new List<GameObject>();
        private float _rotationSpeedEtalon = 0f;
        private float _currentRotationSpeed = 0f;
        private float _rotationTime = 0f;
        private float _rotationCounter = 0f;

        public void SpawnNewCircle()
        {
            SoundManager.SoundManager.Current.PlayWoodAppearing();
            StopRotating();
            DeletePreviousCircle();
            var idx = Random.Range(0, 3);
            currentCircleInfo = Instantiate(circlePrefabs[idx], transform).GetComponent<CircleInfo>();
        }

        public void StopRotating()
        {
            _currentRotationSpeed = 0f;
            _rotationSpeedEtalon = 0f;
            _rotationCounter = 0f;
        }
        public void DeletePreviousCircle()
        {
            if (currentCircleInfo != null) Destroy(currentCircleInfo.gameObject);
        }
        public void PlayWinAnimations()
        {
            if (!currentCircleInfo.fullCircleModel.activeSelf) return;
            var knifes = GetComponentsInChildren<KnifeScript>();
            foreach (var knife in knifes)
            {
                knife.AddRandomForcesWhenInCircle();
            }

            var melons = GetComponentsInChildren<WaterMelon>();
            foreach (var melon in melons)
            {
                melon.CutWaterMelon();
            }

            CircleBreakAnimation();
        }

        public void PlayLooseAnimation()
        {
            StopRotating();
        }

        /// <summary>
        /// return lower pos of circle
        /// </summary>
        public Vector3 GetTargetPosition()
        {
            return centerOfCircle + Vector3.down * circleRadius;
        }

        public void GenerateNewSpinParams()
        {
            _rotationSpeedEtalon = Random.Range(80, 280) * (1 + 0.01f * LevelManager.Current.currentLvL);
            _rotationTime = Random.Range(2 * 360 / _rotationSpeedEtalon, 5 * 360 / _rotationSpeedEtalon);
        }

        private void CircleBreakAnimation()
        {
            SoundManager.SoundManager.Current.PlayWoodDestruction();
            StopRotating();
            currentCircleInfo.fullCircleModel.SetActive(false);
            var type = Random.Range(0, 3);
            GameObject typeParent = null;
            if (type == 0) typeParent = currentCircleInfo.type0Destruction;
            else if (type == 1) typeParent = currentCircleInfo.type1Destruction;
            else if (type == 2) typeParent = currentCircleInfo.type2Destruction;
            if (typeParent == null) return;
            typeParent.SetActive(true);
            var rbs = typeParent.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rbs)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                AddRandomForces(rb);
            }
        }

        private void AddRandomForces(Rigidbody rigidbodyOfPart)
        {
            rigidbodyOfPart.angularVelocity = angularVelocityRandom * Random.Range(-1, 1);
            rigidbodyOfPart.velocity = (rigidbodyOfPart.transform.position - centerOfCircle).normalized +
                                       velocityRandom * Random.Range(-2, 2);
            rigidbodyOfPart.useGravity = true;
            rigidbodyOfPart.isKinematic = false;
        }

        private void Rotate()
        {
            if (_rotationCounter >= _rotationTime)
            {
                _rotationSpeedEtalon *= -1f;
                _rotationCounter = 0f;
            }

            _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, _rotationSpeedEtalon, .02f);
            currentCircleInfo.transform.rotation *= Quaternion.AngleAxis(_currentRotationSpeed * Time.deltaTime, Vector3.forward);
            _rotationCounter += Time.deltaTime;
        }

        private void Update()
        {
            Rotate();
        }

        private void Awake()
        {
            Current = this;
        }
    }
}
