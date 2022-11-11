using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject rabbit;
    [SerializeField]
    private GameObject fish;
    [SerializeField]
    private GameObject rat;

    Vector2 rdPos;
    void Start()
    {

        spawnATarget();


    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("Target") == null)
        {
            spawnATarget();
        };
    }


    #region Public Method
    public Vector3 getRandomPos()
    {
        Vector3 location = new Vector3(Random.Range(0, ScreenUtils.ScreenRight), ScreenUtils.ScreenTop, -Camera.main.transform.position.z);
        return location;
    }
    public void spawnATarget()
    {
        var rdPos = getRandomPos();
        var obj = getRandomObjTarger();
        Instantiate<GameObject>(obj, rdPos, Quaternion.identity);
    }
    public GameObject getRandomObjTarger()
    {
        int rd = Random.Range(0, 3);
        switch (rd)
        {
            case 0:
                {
                    return rabbit;
                    
                }
            case 1:
                {
                    return fish;
                }
            case 2:
                {
                    return rat;
                }
            default:
                {
                    return null;
                }
        }
    }

    #endregion
}
