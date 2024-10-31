using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers     _Instance;

    DataManager         _data = new DataManager();
    InputManager        _input = new InputManager();
    PoolManager         _pool = new PoolManager();
    ResourceManager     _resource = new ResourceManager();
    SceneManagerEx      _scene = new SceneManagerEx();
    UIManager           _ui = new UIManager();
    SoundManager        _sound = new SoundManager();

    static Managers Instance { get { Initialize(); return _Instance; } }

    public static DataManager data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound;  } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource;  } }
    public static UIManager UI { get { return Instance._ui; } }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Initialize()
    {
        if(_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            _Instance = go.GetComponent<Managers>();

            _Instance._data.Init();
            _Instance._pool.Init();
            _Instance._sound.Init();
        }
        
    }

    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
