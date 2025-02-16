using DG.Tweening;
using Game.Service;
using TMPro;
using UnityEngine;

namespace Game.View
{
    public class AppleTreeView : View,
        IShowHide,
        ITrigger
    {
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private RectTransform _main;
        CanvasGroup _canvasGroup;

        private Sequence seqTextName;
        private Sequence seqMain;

        public override void Awake()
        {
            base.Awake();
            _textName.alpha = 0.0f;

            _canvasGroup = _main.GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0.0f;
            _main.localPosition += new Vector3(-2000f, 0, 0);
        }

        public override void Show()
        {
            base.Show();
            AnimateMain(ref seqMain);
        }

        public override void Hide()
        {
            base.Hide();
        }


        public virtual void TriggerEnter()
        {
            AnimateTextName(ref seqTextName);
        }

        public virtual void TriggerExit()
        {
            seqTextName.Pause();
            _textName.DOFade(0f, 1.5f);
        }

        public void AnimateTextName(ref Sequence sequence) 
        {

            if (sequence == null)
            {
                sequence = DOTween.Sequence();
                sequence.SetAutoKill(false);
                sequence.SetLoops(-1, LoopType.Yoyo);

                sequence.Append(_textName.DOFade(0.9f, 1.5f));
            }
            else 
            {
                sequence.Restart();
            }
        }
        public void AnimateMain(ref Sequence sequence)
        {
            if (sequence == null)
            {
                sequence = DOTween.Sequence();
                sequence.SetAutoKill(false);

                sequence.Append(_main.DOAnchorPosX(0f, 0.15f));
                sequence.Join(_canvasGroup.DOFade(1f, 0.15f));
            }
            else
            {
                sequence.Restart();
            }
        }
    }
}
