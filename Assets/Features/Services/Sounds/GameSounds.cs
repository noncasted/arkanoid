using System;
using Global.Audio;
using Global.Systems;
using Internal;

namespace Features.Services.Sounds
{
    public class GameSounds : IScopeSetup
    {
        public GameSounds(IGlobalAudioPlayer audioPlayer, GameSoundsOptions options)
        {
            _audioPlayer = audioPlayer;
            _options = options;
        }
        
        private readonly IGlobalAudioPlayer _audioPlayer;
        private readonly GameSoundsOptions _options;

        public void OnSetup(IReadOnlyLifetime lifetime)
        {
            Msg.Listen<GameSoundType>(lifetime, OnRequest);    
        }

        private void OnRequest(GameSoundType type)
        {
            switch(type)
            {
                case GameSoundType.Collision:
                    _audioPlayer.PlaySound(_options.Collision);
                    break;
                case GameSoundType.Pickup:
                    _audioPlayer.PlaySound(_options.Pickup);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}