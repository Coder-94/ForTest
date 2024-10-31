using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerr : MonoBehaviour
{

    float       _speed = 10.0f;
    Vector3     _destPos;

    // Start is called before the first frame update
    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;

    /*
    State����.
    ���� �÷��̾��� ���¸� ���������� ������
    �� �� ������ ���¸��� �Լ��� ĸ��ȭ �ؼ� �ѹ��� �ϳ��� �ִϸ��̼� ����
    �ѹ��� �� ���¹ۿ� �ټ� ���ٴ� ������ ������ ���� �������� ����ϱ� ������.
    ������ �� ������ �ǳĸ�, �����̸鼭 �ֹ����Ҷ��� moving, skill �ΰ��� ���°� �ʿ��ϱ� ����.
     */
    void UpdateDie()
    {
        //�� �� ����
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        //float������ ���������� �׻� ���������� �ֱ� ������ �ؼҰ����� ���
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            //normalize�� ���� ������ ���Ͱ�(����� �ӵ� ���� �������)�� �̹��� ������ ����.
            //normalizs�� ���Ͱ��� 1�� �ٲ㼭 �ӵ��� �����ϰ� �ϰ� ���Ⱚ�� �����;���.

            //ù��° �������� �ִ��� ������ �ι�°�� ����° �� ������ ������ ����� ���.
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            //transform.LookAt(_destPos);
        }

        //�ִϸ��̼�
        Animator anim = GetComponent<Animator>();
        //���� ���� ���� ����
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        //�ִϸ��̼�
        Animator anim = GetComponent<Animator>();
        //���� ���� ���� ����
        anim.SetFloat("speed", 0);
    }

    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;

        }
    }

   void OnMouseClicked(Define.MouseEvent evt)
   {
        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _destPos.y = transform.position.y;
            _state = PlayerState.Moving;
        } 
    }
}
