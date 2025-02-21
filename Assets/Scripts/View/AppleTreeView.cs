using DG.Tweening;
using Game.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{

    public class AppleTreeView : View,
        IShowHide,
        ITrigger
    {
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private RectTransform _main;
        [SerializeField] private Button _btnGet;
        public Button BtnGet  => _btnGet;

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


        public override void TriggerEnter()
        {
            AnimateTextName(ref seqTextName, _textName);
        }

        public override void TriggerExit()
        {
            seqTextName.Pause();
            _textName.DOFade(0f, 1.5f);
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
