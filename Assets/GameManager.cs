using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float difficulty = 1;

    #region Singleton

    public static GameManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getDifficulty(){
        return difficulty;
    }
}
