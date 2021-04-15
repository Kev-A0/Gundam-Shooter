using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GundamSpecial : MonoBehaviour
{
    public int specialAvaliable;
    public int numOfSpecial;

    public Image[] special;
    public Sprite fullSpecial;
    public Sprite emptySpecial;

    private void Update()
    {
        if (specialAvaliable > numOfSpecial)
        {
            specialAvaliable = numOfSpecial;
        }

        for (int i = 0; i < special.Length; i++)
        {
            if (i < specialAvaliable)
            {
                special[i].sprite = fullSpecial;
            }
            else
            {
                special[i].sprite = emptySpecial;
            }

            // filling themselves with special picture
            if (i < numOfSpecial)
            {
                special[i].enabled = true;
            }
            else
            {
                special[i].enabled = false;
            }
        }
    }
}
