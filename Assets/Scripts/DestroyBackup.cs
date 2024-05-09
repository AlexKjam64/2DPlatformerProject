using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBackup : MonoBehaviour
{
    //A temp solution to make sure everything related to dog is gone when player deals damage

    public GameObject Dog;
    public GameObject ThisObject;

    void Update()
    {
        if (Dog == null)
        {
            Destroy(ThisObject);
        }
    }

}
