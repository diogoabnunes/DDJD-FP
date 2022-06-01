using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    #region Singleton

    public static InteractionManager instance;

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

    public void manageInteraction(Interaction interaction){
        interaction.execute();
    }
}
