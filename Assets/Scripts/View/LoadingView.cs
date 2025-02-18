using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public class LoadingView : View
    {
        [SerializeField] protected Image _imgLoading;

        public float TimeLoading = 0.15f; 
        public float TimeLoadingFadeIn = 0.15f;
        public float TimeLoadingFadeOut = 0.15f;
        public bool IsLoading;

        private Sequence _seqLoading;

        public override void Awake()
        {
            base.Awake();
        }

        public void AnimateLoading(ref Sequence sequence)
        {
            IsLoading = true;
            if (sequence == null)
            {
                sequence = DOTween.Sequence();
                sequence.SetAutoKill(false);

                sequence.AppendCallback(() => base.Show());
                sequence.Append(_imgLoading.DOFade(1f, TimeLoadingFadeIn));
                sequence.AppendInterval(TimeLoading);
                sequence.Append(_imgLoading.DOFade(0f, TimeLoadingFadeOut));
                sequence.AppendCallback(() => base.Hide());
                sequence.AppendCallback(() => IsLoading = false);
            }
            else
            {
                sequence.Restart();
            }

        }

        public void LoadNow()
        {
            AnimateLoading(ref _seqLoading);
        }
    }
}
