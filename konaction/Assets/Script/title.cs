using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title : MonoBehaviour
{
    private bool firstPush = false;

    //スターボタン押したら呼ばれる
    public void PressStart()
    {
        Debug.Log("PressStart");

        if (!firstPush)
        {
            Debug.Log("Go Next Scene");
            //次のシーンへ行く

            //
            firstPush = true;
        }
    }

}