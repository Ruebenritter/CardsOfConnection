using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public class DontDestroy : MonoBehaviour
{
    void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
    }
}
}
