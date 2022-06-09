using UnityEngine;

namespace BAA
{
    // Example MonoBehaviour that oscillates a transform position between two points.
    public class Platform : MonoBehaviour
    {
        // TODO: I dont like making fields public only so tooling would be easier...
        public Vector3 StartPos = new Vector3(-10.0F, 0.0F, 0.0F);
        public Vector3 EndPos = new Vector3(10.0F, 0.0F, 0.0F);
        public float MoveSpeed = 0.2F;

        private void Update()
        {
            SnapToPath(Time.time);
        }

        public void SnapToPath(float time)
        {
            transform.position = Vector3.Lerp(StartPos, EndPos, (Mathf.Sin(time * MoveSpeed) + 1) * .5f);
        }
    }
}
