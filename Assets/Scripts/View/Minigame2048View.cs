using DG.Tweening;
using Game.Service;
using TMPro;
using UnityEngine;

namespace Game.View
{
    public class Minigame2048View : View,
        IShowHide,
        ITrigger
    {
        [SerializeField] private TextMeshProUGUI _textName;

        private Sequence seqTextName;

        public override void Awake()
        {
            base.Awake();
            _textName.alpha = 0.0f;
        }

        public override void Hide()
        {
            base.Hide();
        }

        public override void Show()
        {
            base.Show();
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
    }
}
