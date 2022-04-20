using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    #region //�C���X�y�N�^�[�Őݒ肷��
    [Header("���x")]public float speed;
    [Header("�d��")] public float gravity;
    [Header("�W�����v���鍂��")] public float jumpHeight;
    [Header("�W�����v��������")] public float jumpLimitedTime;
    [Header("�W�����v���x")] public float jumpSpeed;
    [Header("�ڒn����")] public GroundCheck ground;
    [Header("�����Ԃ�������")] public GroundCheck head;
    [Header("�_�b�V�������\��")] public AnimationCurve dashCurve;
    [Header("�W�����v�����d��")] public AnimationCurve jumpCurve;
    #endregion

    #region//�v���C�x�[�g�ϐ�
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private bool isGround = false;
    private bool isHead = false;
    private bool isJump = false;
    private bool isRun = false;
    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;
    private float dashTime = 0.0f;
    private float beforKey = 0.0f;
    #endregion

    void Start()
    {
        //�R���|�[�l���g�̃C���X�^���X�����܂���
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

 
    void FixedUpdate()
    {
        //�ڒn����𓾂�
        isGround = ground.IsGround();
        isHead = head.IsGround();

        //�e���W���̑��x�����߂�
        float xSpeed = GetXSpeed();
        float ySpeed = GetYSpeed();

        //�A�j���[�V������K�p
        SetAnimation();

        //�ړ����x��ݒ�
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }

    /// <summary>
    /// Y�����ŕK�v�Ȍv�Z�����A���x��Ԃ�
    /// </summary>
    /// <returns>Y���̑���</returns>
    private float GetYSpeed()
    {
        //�L�[���͂��ꂽ��s������
        float verticalkey = Input.GetAxis("Vertical");
        float ySpeed = -gravity;

        if (isGround)
        {
            if (verticalkey > 0.7)//0.7�ȏ�  
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y; //�W�����v�����������L�^
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //������L�[�������Ă��邩
            bool pushUpKey = verticalkey > 0.7;
            //���݂̍�������ׂ鍂����艺��
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //�W�����v���Ԃ������Ȃ肷���Ă��Ȃ���
            bool canTime = jumpLimitedTime > jumpTime;

            if (pushUpKey && canHeight && canTime && !isHead)//0.7�ȏ�
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }
        //�A�j���[�V�����J�[�u�𑬓x�ɓK�p
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        return ySpeed;
    }

    /// <summary>
    /// X�����ŕK�v�Ȍv�Z�����A���x��Ԃ�
    /// </summary>
    /// <returns>X���̑���</returns>
    private float GetXSpeed()
    {
        //�L�[���͂��ꂽ��s������
        float horizontalKey = Input.GetAxis("Horizontal");

        float xSpeed = 0.0f;

        if (horizontalKey > 0.9)//�����̗͂��������Ă���
        {
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = -speed;
        }
        else
        {
            isRun = false;
            dashTime = 0.0f;
            xSpeed = 0.0f;
        }

        //�O��̓��͂���_�b�V���̔��]�𔻒f���đ��x��ς���
        if (horizontalKey > 0 && beforKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforKey > 0)
        {
            dashTime = 0.0f;
        }
        beforKey = horizontalKey;

        //�A�j���[�V�����J�[�u�𑬓x�ɓK�p
        xSpeed *= dashCurve.Evaluate(dashTime);

        return xSpeed;
    }
    /// <summary>
    /// �A�j���[�V������ݒ肷��
    /// </summary>
    private void SetAnimation()
    {
        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
    }
}
       
