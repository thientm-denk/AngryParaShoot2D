using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [Header("Score Settings")]
    [Tooltip("Set this target score")]
    public int score = 0;


    Rigidbody2D rd;
    public PointAddedEvent pointAddedEvent;

    // Start is called before the first frame update
    void Start()
    {
        pointAddedEvent = new PointAddedEvent();
        EventManager.AddPointsAddedInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            AudioManager.Play(AudioName.hitTarget);
            pointAddedEvent.Invoke(score);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
    */
    
    public void AddPointsAddedListener(UnityAction<int> listener)
    {
        pointAddedEvent.AddListener(listener);
    }
}
