using UnityEngine;

namespace Features.GamePlay.Levels.Platforms
{
    [DisallowMultipleComponent]
    public class RotatingBorder : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private void FixedUpdate()
        {
            transform.Rotate(Vector3.forward, _speed * Time.fixedDeltaTime);
        }
    }
}