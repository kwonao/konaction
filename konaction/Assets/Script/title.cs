using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title : MonoBehaviour
{
    private bool firstPush = false;

    //�X�^�[�{�^����������Ă΂��
    public void PressStart()
    {
        Debug.Log("PressStart");

        if (!firstPush)
        {
            Debug.Log("Go Next Scene");
            //���̃V�[���֍s��

            //
            firstPush = true;
        }
    }

}