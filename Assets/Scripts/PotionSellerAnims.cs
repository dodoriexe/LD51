using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSellerAnims : MonoBehaviour
{
        public void Show()
        {
            transform.LeanMoveLocalX(663, .5f).setEaseOutBounce();
        }

        public void Hide()
        {
            transform.LeanMoveLocalX(1293, .5f).setEaseOutBounce();
        }
}
