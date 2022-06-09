using UnityEngine;

namespace BAA
{
    // Example MonoBehaviour that oscillates a transform position between two points.
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _startPos = new Vector3(-10, 0f, 0f);

        [SerializeField]
        private Vector3 _endPos = new Vector3(10f, 0f, 0f);

        [SerializeField]
        private float _moveSpeed = .2f;

        public Vector3 StartPos
        {
            get => _startPos;
            set => _startPos = value;
        }

        public Vector3 EndPos
        {
            get => _endPos;
            set => _endPos = value;
        }

        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        private void Update()
        {
            SnapToPath(Time.time);
        }

        public void SnapToPath(float time)
        {
            transform.position = Vector3.Lerp(_startPos, _endPos, (Mathf.Sin(time * _moveSpeed) + 1) * .5f);
        }
    }
}
