using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{


    public float maxSpeed = 15f;
    public float speed;
    public float rangeMagnitude = 10;
    float vy = 0f;
    float vx = 0f;

  
    public Vector2 direction;
    // Lay vi tri
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
       
        
        if(speed > rangeMagnitude)
        {
            speed = maxSpeed;
        }
        else
        {
            speed = speed * maxSpeed / rangeMagnitude;
        }

        
        //direction.Normalize();
        // tinh goc nem
        float angle = Mathf.Atan2(direction.y, direction.x) ;
        // Chieu len 2 truc 
        vy = speed * Mathf.Sin(angle);
        vx = speed * Mathf.Cos(angle);

        pos = transform.position;
        //Debug.Log(direction);
       
    }

    // Update is called once per frame
    void Update()
    {
        // khong can xoay nua
        //// Get angle
        float angle = Mathf.Atan2(vy, vx) * Mathf.Rad2Deg;
        //// Rotate around Z
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float timeStep = Time.deltaTime;
        // chuyen dong ngang khong thay doi x = v0 * t;
        pos.x += timeStep * vx;

        //van toc va pos y theo truc y bien doi theo thoi gian/vy = vo - gt / y = vy * t
        vy = vy - 10 * timeStep;
        pos.y += (timeStep * vy);

        float lenghRay = (pos - transform.position).magnitude;
        // Object sap dung
        
        
        RaycastHit2D hit = Physics2D.Raycast(pos, transform.TransformDirection(Vector2.right), lenghRay);
        Debug.DrawRay(pos, transform.TransformDirection(Vector2.right) * lenghRay,  Color.white);



        if (hit.collider != null)
        {
            Debug.Log("hitted: " + hit.collider.name);
            handleHitTarget(hit);
        }
        else
        {
            transform.position = pos;
        }
        

        


        //var x = Physics2D.OverlapArea(pos, pos);
        ////Check xem pos co cham vao object nao nay ko truoc khi gan
        //if (x == null)
        //{
        //    transform.position = pos;
        //}
        //else
        //{
        //    //Debug.Log(x.ToString());
        //    handleHitTarget(x);
        //}


    }
    private void handleHitTarget(RaycastHit2D hit)
    {
        var collObj = hit.collider.gameObject;
        if (collObj.tag == "Target")
        {
            collObj.GetComponent<Target>().pointAddedEvent.Invoke(collObj.GetComponent<Target>().score);
            Destroy(collObj);
            AudioManager.Play(AudioName.hitTarget);
        }
        Destroy(gameObject);
    }




    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        
    //    Destroy(gameObject);
    //}

    //private void OnBecameInvisible()
    //{
        
    //    Destroy(gameObject);
    //}
}
