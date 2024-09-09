using Cysharp.Threading.Tasks;
using Features.Loop;
using Global.Audio;
using Global.Publisher;
using Global.Saves;
using Global.UI;
using Internal;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Features
{
    [DisallowMultipleComponent]
    public class SettingsUI : MonoBehaviour, ISettingsUI, IUIStateAsyncEnterHandler, ISceneService, IScopeSetupAsync
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private DesignButton _closeButton;

        private IOverlayBackground _background;
        private IPause _pause;
        private IDataStorage _dataStorage;
        private IGlobalVolume _volume;

        public IUIConstraints Constraints { get; } = UIConstraints.Game;

        [Inject]
        private void Construct(
            IOverlayBackground background,
            IPause pause,
            IDataStorage dataStorage,
            IGlobalVolume volume)
        {
            _volume = volume;
            _dataStorage = dataStorage;
            _pause = pause;
            _background = background;
        }

        public void Create(IScopeBuilder builder)
        {
            builder.RegisterComponent(this)
                .As<ISettingsUI>()
                .As<IScopeSetupAsync>();

            gameObject.SetActive(false);
        }

        public async UniTask OnSetupAsync(IReadOnlyLifetime lifetime)
        {
            var save = await _dataStorage.GetEntry<VolumeSave>();
            _slider.value = save.SoundVolume;
            _volume.SetVolume(0f, save.SoundVolume);

            _slider.onValueChanged.AddListener(value =>
            {
                save.SoundVolume = value;
                _dataStorage.Save(save);
                _volume.SetVolume(0f, value);
            });
        }

        public async UniTask OnEntered(IUIStateHandle handle)
        {
            _pause.Pause();
            handle.AttachGameObject(gameObject);
            _background.Show(handle);

            await _closeButton.WaitClick(handle);

            _pause.Continue();
        }
    }
}