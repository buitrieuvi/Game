using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game
{
    public class play : MonoBehaviour
    {
        TextMeshProUGUI textMeshProUGUI;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void EE() 
        {
            textMeshProUGUI.DOFade(1f, 1f);
        }
    }
}
