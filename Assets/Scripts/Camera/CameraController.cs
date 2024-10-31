using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;
    [SerializeField]
    GameObject _player;
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Insert();
    }

    public void Init()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Insert()
    {
        if(_mode == Define.CameraMode.QuarterView)
        {
            RaycastHit hit;
            if(Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
