using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickerAnims : MonoBehaviour
{
    public void Show()
    {
        transform.LeanMoveLocalY(490f, .5f).setEaseOutBounce();
    }

    public void Hide()
    {
        transform.LeanMoveLocalY(915, .5f).setEaseOutBounce();
    }
}
