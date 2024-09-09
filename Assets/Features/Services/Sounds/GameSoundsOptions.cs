using Internal;
using UnityEngine;

namespace Features.Services.Sounds
{
    public class GameSoundsOptions : EnvAsset
    {
        [SerializeField] private AudioClip _collision;
        [SerializeField] private AudioClip _pickup;
        
        public AudioClip Collision => _collision;
        public AudioClip Pickup => _pickup;
    }
}