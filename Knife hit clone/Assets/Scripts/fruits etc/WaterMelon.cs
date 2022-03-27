using Circle;
using Knife;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fruits_etc
{
    public class WaterMelon : MonoBehaviour
    {
        [SerializeField] private Transform fallingObjects;
        [SerializeField] private Vector3 angularVelocityRandom;
        [SerializeField] private Vector3 velocityRandom;
        private Transform _transform;
        
        public void CutWaterMelon()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rigidbodyOfPart in rigidbodies)
            {
                rigidbodyOfPart.useGravity = true;
                rigidbodyOfPart.isKinematic = false;
                AddRandomForcesWhenInCircle(rigidbodyOfPart);
            }
            _transform.parent = CircleController.Current.transform;
            SoundManager.SoundManager.Current.PlayWaterMelonSound();
            ParticlesManager.Current.CreateJuiceParticles(_transform.position);
            Destroy(gameObject, 2f);
            Destroy(this);
        }
        private void AddRandomForcesWhenInCircle(Rigidbody rigidbodyOfPart)
        {
            rigidbodyOfPart.angularVelocity =  angularVelocityRandom * Random.Range(-1, 1);
            rigidbodyOfPart.velocity = Random.Range(-1f, 1f) * velocityRandom;
            rigidbodyOfPart.useGravity = true;
            rigidbodyOfPart.isKinematic = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Knife") && CircleController.Current.currentCircleInfo.isReadyForInteracting)
            {
                SaveManager.Current.currentMelons++;
                CutWaterMelon();
            }
        }

        private void Start()
        {
            _transform = transform;
        }
    }
}
