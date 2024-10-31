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
    State패턴.
    현재 플레이어의 상태를 열거형으로 정리해
    그 각 열거형 형태마다 함수로 캡슐화 해서 한번에 하나의 애니메이션 가동
    한번에 한 상태밖에 줄수 없다는 단점이 있지만 소형 플젝에선 사용하기 용이함.
    단점이 왜 문제가 되냐면, 움직이면서 주문질할때는 moving, skill 두개의 상태가 필요하기 때문.
     */
    void UpdateDie()
    {
        //엥 뭐 없대
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        //float끼리의 뺄샘에서는 항상 오차범위가 있기 때문에 극소값으로 계산
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            //normalize를 하지 않으면 벡터값(방향과 속도 등을 담고있음)에 미묘한 오차가 생김.
            //normalizs로 벡터값을 1로 바꿔서 속도는 일정하게 하고 방향값만 가져와야함.

            //첫번째 변수값의 최댓값을 무조건 두번째와 세번째 값 사이의 값으로 만드는 기능.
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            //transform.LookAt(_destPos);
        }

        //애니메이션
        Animator anim = GetComponent<Animator>();
        //현재 상태 정보 전달
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        //애니메이션
        Animator anim = GetComponent<Animator>();
        //현재 상태 정보 전달
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
