using System;
using Circle;
using UIAndOther;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Knife
{
    public class KnifeScript : MonoBehaviour
    {
        public bool isLaunched;
        public Rigidbody _rigidbody;
        [SerializeField] private BoxCollider bladeCollider;
        [SerializeField] private BoxCollider guardCollider;
        [SerializeField] private Vector3 angularVelocityRandom;
        [SerializeField] private Vector3 velocityRandom;
        [SerializeField] float timeForFlight = 0.3f;
        [SerializeField] private Transform endOfBladePosition;
        private Transform _transform;
        private Vector3 _startPosition;
        private float _timeSinceStart = 0f;

        public void LaunchKnife()
        {
            if (_transform != null)
            {
                CheckForKnifeValidPos();
                _startPosition = _transform.position;
                isLaunched = true;
            }
        }

        public void AddRandomForcesWhenInCircle()
        {
            _rigidbody.angularVelocity =  angularVelocityRandom * Random.Range(-1, 1);
            _rigidbody.velocity = (_transform.position - CircleController.Current.centerOfCircle).normalized * Random.Range(3f, 7f);
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            var cols = GetComponentsInChildren<Collider>();
            foreach (var col in cols)
            {
                col.enabled = false;
            }
            Destroy(gameObject, 2f);
            Destroy(this);
        }

        private void AddRandomForces()
        {
            _rigidbody.angularVelocity =  angularVelocityRandom * Random.Range(-1, 1);
            _rigidbody.velocity = velocityRandom * Random.Range(-1, 1);
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            Destroy(gameObject, 2f);
            Destroy(this);
        }


        private void MoveKnife()
        {
            var direction = CircleController.Current.GetTargetPosition() - _startPosition;
            var t = _timeSinceStart / timeForFlight;
            _rigidbody.MovePosition(_startPosition + direction * t);
        }

        private void Update()
        {
            if (isLaunched)
            {
                _timeSinceStart += Time.deltaTime;
                if (_timeSinceStart > timeForFlight)
                {
                    StopKnife();
                    KnifeSpawner.Current.SpawnKnife();
                }
                else MoveKnife();
            }
        }

        private void StopKnife()
        {
            _transform.parent = CircleController.Current.currentCircleInfo.transform;
            _transform.position = CircleController.Current.GetTargetPosition();
            _rigidbody.isKinematic = true;
            CircleController.Current.currentCircleInfo.KnifeStuckAnimations();
            var colliders = GetComponentsInChildren<Collider>();
            foreach (var col in colliders)
            {
                col.isTrigger = true;
            }
            bladeCollider.enabled = false;
            isLaunched = false;
            LevelManager.Current.NewKnifeStuck();
            SoundManager.SoundManager.Current.PlayKnifeStuck();
            ParticlesManager.Current.CreateKnifeStuckParticles(endOfBladePosition.position, _transform);
            SkyBoxManager.Current.MakeBackgroundPulse();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Knife"))
            {
                if(other == null) Debug.Log("other is null!");
                if(ParticlesManager.Current == null) Debug.Log("particleManager is null!");
                //Debug.Log(other.name + " " + gameObject.name + " " + "intercection");
                Intercection();
                var closestPoint = other.ClosestPoint(_transform.position);
                ParticlesManager.Current.CreateKnifesCollisionParticles(closestPoint);
            }
        }

        private void Intercection()
        {
            if(LevelManager.Current.isAnimationsOfLogDestructionPlaying) return;
            CircleController.Current.PlayLooseAnimation();
            if (KnifeSpawner.Current.currentKnife != null)
            {
                KnifeSpawner.Current.currentKnife.AddRandomForces();
                Destroy(KnifeSpawner.Current.currentKnife.gameObject, 3);
                KnifeSpawner.Current.currentKnife = null;
            }
            LevelManager.Current.LooseGame();
            VibrationManager.Current.Vibrate();
            SoundManager.SoundManager.Current.PlayIntercectionSound();
            AddRandomForces();
        }
        private void CheckForKnifeValidPos()
        {
            if (_transform.position != KnifeSpawner.Current.knifeThrowPoint.position)
            {
                _transform.position = KnifeSpawner.Current.knifeThrowPoint.position;
                _transform.rotation = KnifeSpawner.Current.knifePrefab.transform.rotation;
            }
        }
        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}
