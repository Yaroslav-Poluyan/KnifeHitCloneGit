using Circle;
using UIAndOther;
using UnityEngine;

namespace Knife
{
    public class KnifeSpawner : MonoBehaviour
    {
        public static KnifeSpawner Current;
        public Transform knifeThrowPoint;
        public KnifeScript currentKnife;
        public GameObject knifePrefab;
        [SerializeField] private Transform spawnFirstPoint;
        [SerializeField] private float timeDelay;
        private float _timeSinceStart = 100f;

        public void SpawnKnife()
        {
            _timeSinceStart = 0f;
            var knife = Instantiate(knifePrefab, spawnFirstPoint);
            knife.name = Random.Range(0, 100).ToString();
            currentKnife = knife.GetComponent<KnifeScript>();
        }

        private void MovingAtSpawn()
        {
            if(currentKnife == null) return;
            var position = spawnFirstPoint.position;
            var direction = knifeThrowPoint.position - position;
            var t = _timeSinceStart / timeDelay;
            currentKnife._rigidbody.MovePosition(position + direction * t);
            _timeSinceStart += Time.deltaTime;

        }
        private void Update()
        {
            if (_timeSinceStart <= timeDelay)
            {
                MovingAtSpawn();
            }
#if UNITY_EDITOR

            if (Input.GetKeyDown(KeyCode.Space) && currentKnife != null && !LevelManager.Current.isAnimationsOfLogDestructionPlaying &&
                CircleController.Current.currentCircleInfo.isReadyForInteracting && !CanvasesManager.Current.IsAnyWindowOpen())
            {
                _timeSinceStart = 100;
                currentKnife.LaunchKnife();
                currentKnife = null;
            }
#endif
            if (Input.touchCount > 0 && currentKnife != null && !LevelManager.Current.isAnimationsOfLogDestructionPlaying &&
                CircleController.Current.currentCircleInfo.isReadyForInteracting && !CanvasesManager.Current.IsAnyWindowOpen())
            {
                _timeSinceStart = 100;
                currentKnife.LaunchKnife();
                currentKnife = null;
            }
        }
        
        private void Awake()
        {
            Current = this;
        }
    }
}