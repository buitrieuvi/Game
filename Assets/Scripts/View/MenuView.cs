using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public class MenuView : View
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private VerticalLayoutGroup _vlg;

        private List<ButtonOptionMenu> _btns = new List<ButtonOptionMenu>();

        private Sequence seqContent;

        public override void Awake()
        {
            base.Awake();

            foreach (RectTransform btn in _content.transform)
            {
                ButtonOptionMenu button = btn.GetComponent<ButtonOptionMenu>();
                _btns.Add(button);
            }
            //_vlg.enabled = false;
        }



        public override void Show()
        {
            base.Show();
            AnimateContent(ref seqContent);
        }

        public override void Hide()
        {
            base.Hide();
        }

        public void AnimateContent(ref Sequence sequence)
        {
            if (sequence == null)
            {
                float time = 0.35f;

                sequence = DOTween.Sequence();
                sequence.SetAutoKill(false);

                foreach (ButtonBase child in _btns)
                {
                    sequence.Insert(time, child.Cvg.DOFade(1f, 0.35f));
                    sequence.Insert(time, child.Rect.DOAnchorPosX(270f, 0.2f));

                    time += 0.035f;
                }
            }
            else
            {
                sequence.Restart();
            }
        }

        public void AnimateContent() 
        {
            AnimateContent(ref seqContent);
        }

    }
}
