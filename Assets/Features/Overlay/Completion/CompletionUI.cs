using System;
using Cysharp.Threading.Tasks;
using Features.Loop;
using Global.UI;
using Internal;
using UnityEngine;
using VContainer;

namespace Features.Completion
{
    [DisallowMultipleComponent]
    public class CompletionUI : MonoBehaviour, ICompletionUI, ISceneService
    {
        [SerializeField] private GameObject _win;
        [SerializeField] private GameObject _lose;
        [SerializeField] private DesignButton _restartButton;
        [SerializeField] private DesignButton _nextButton;

        private IOverlayBackground _background;
        private IPause _pause;

        public IUIConstraints Constraints { get; } = UIConstraints.Game;

        [Inject]
        private void Construct(IOverlayBackground background, IPause pause)
        {
            _pause = pause;
            _background = background;
        }

        public void Create(IScopeBuilder builder)
        {
            builder.RegisterComponent(this)
                .As<ICompletionUI>();

            gameObject.SetActive(false);
        }

        public async UniTask Process(IUIStateHandle handle, GameResult result)
        {
            _pause.Pause();
            handle.AttachGameObject(gameObject);
            _background.Show(handle);

            _win.SetActive(false);
            _lose.SetActive(false);

            switch (result)
            {
                case GameResult.Win:
                    _win.SetActive(true);
                    await _nextButton.WaitClick(handle);
                    break;
                case GameResult.Lose:
                    _lose.SetActive(true);
                    await _restartButton.WaitClick(handle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }

            _pause.Continue();
        }
    }
}