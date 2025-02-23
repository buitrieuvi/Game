using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.View
{
    public class Cell2048View : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        public CanvasGroup Canv;

        Sequence seqGenShow;
        Sequence seqGenHide;

        public void Awake()
        {
            
        }

        public void AnimateGenShow()
        {
            if (seqGenShow == null)
            {
                seqGenShow = DOTween.Sequence();
                seqGenShow.SetAutoKill(false);

                seqGenShow.AppendInterval(1.35f);
                seqGenShow.Join(Canv.transform.DOScale(1f, 0.25f));
                seqGenShow.Join(Canv.DOFade(1f, 0.45f));
            }
            else
            {
                seqGenShow.Restart();
            }
        }

        public void AnimateGenHide()
        {
            if (seqGenHide == null)
            {
                seqGenHide = DOTween.Sequence();
                seqGenHide.SetAutoKill(false);

                seqGenHide.Join(Canv.transform.DOScale(0f, 0.15f));
                seqGenHide.Join(Canv.DOFade(0f, 0.15f));
            }
            else
            {
                seqGenHide.Restart();
            }
        }
    }
}
