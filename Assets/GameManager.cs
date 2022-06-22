using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float difficulty = 1;
    public int killedEnemies = 0;

    public int bossCount = 50;

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

    public void addEnemyKilled(){
        killedEnemies += 1;
        if(killedEnemies > bossCount){
            Debug.Log("BOSS");
        }
    }
}
