using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField]
    public GameObject arrow;
    [SerializeField]
    public Transform shootPoint;
    // delay shoot
    [SerializeField]
    public float delayTime;
    bool canShoot = true;
    Timer timer;

    [SerializeField]
    float maxSpeed = 15f;
    public float speed;
    public float rangeMagnitude = 10;
    //
    Rigidbody2D rb;
    LineRenderer lr;



    // Support Aim animation and shoot

    Vector2 mousePosA;
    Vector2 mousePosB;
    Vector2 direction;

    void Start()
    {
        // Setup delay
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = delayTime;

        // Setup Line draw
        rb = shootPoint.GetComponent<Rigidbody2D>();
        lr = shootPoint.GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        // 2 diem tru nhau ra vector
        //direction = mousePos - bowPos;
        //direction.Normalize();
        // vector chi huong cung
        //transform.right = direction;
        GameObject arro = GameObject.FindWithTag("Arrow");
        if (arro == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                mousePosB = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = mousePosA - mousePosB;

                transform.right = direction; // spin bow


                //Debug.Log(speed);
                Vector2[] trajectory = PlotV2(rb, (Vector2)shootPoint.transform.position, direction, direction.magnitude, 50);                

                lr.positionCount = trajectory.Length;
                Vector3[] positions = new Vector3[trajectory.Length];
                for (int i = 0; i < trajectory.Length; i++)
                {
                    positions[i] = trajectory[i];
                }
                lr.SetPositions(positions);

            }
            if (Input.GetMouseButtonUp(0))
            {
                //ResetLineRender(lr);
                shoot(direction);
            }
        }
        
        if (timer.Finished)
        {
            canShoot = true;
        }
    }



    #region Public Method

    public void shoot(Vector2 direc)
    {
        AudioManager.Play(AudioName.bowShoot);
        GameObject newArrow = Instantiate(arrow, (Vector2)shootPoint.transform.position, Quaternion.identity);

        //newArrow.GetComponent<Rigidbody2D>().AddForce(direction * lauchForce, ForceMode2D.Impulse) ;
        //newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * lauchForce;
        newArrow.GetComponent<Arrow>().direction = direc;
        newArrow.GetComponent<Arrow>().speed = direc.magnitude;
        newArrow.GetComponent<Arrow>().maxSpeed = this.maxSpeed;
        newArrow.GetComponent<Arrow>().rangeMagnitude = this.rangeMagnitude;
        
        canShoot = false;
        // Chay time delay
        timer.Run();
    }

    public float CalSpeed(float mag)
    {
        if (mag > rangeMagnitude)
        {
            return maxSpeed;
        }
        else
        {
            return mag * (float)maxSpeed / (float)rangeMagnitude;
        }
    }

    
    public Vector2[] Plot(Rigidbody2D rb, Vector2 pos, Vector2 velocity, int step){
        Vector2[] result = new Vector2[step];

        float timeStep = Time.fixedDeltaTime * Physics2D.velocityIterations;
        Vector2 gravityAccel = new Vector2(0,-10) * timeStep * timeStep;

        float drag = 1f - timeStep * rb.drag;

        Vector2 moveStep = velocity * timeStep;

        for(int i = 0; i < step; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            result[i] = pos;
        }
        return result;
    }

    public Vector2[] PlotV2(Rigidbody2D rb, Vector2 pos, Vector2 direc, float speed, int step)
    {
        
        speed = CalSpeed(speed);
        

        float angle = Mathf.Atan2(direction.y, direction.x);
        // Chieu len 2 truc 
        float vy = speed * Mathf.Sin(angle);
        float vx = speed * Mathf.Cos(angle);

       
        Vector2[] result = new Vector2[step];

        float timeStep = Time.fixedDeltaTime ;
        
        //float drag = 1f - timeStep * rb.drag;

        Vector2 moveStep = pos;

        for (int i = 0; i < step; i++)
        {
            moveStep.x = vx * timeStep;
            vy = vy - 10 * timeStep;
            moveStep.y = vy * timeStep;

            //moveStep *= drag;
            pos += moveStep;
            result[i] = pos;
        }
        return result;
    }

    public void ResetLineRender(LineRenderer lr)
    {
        lr.positionCount = 0;
    }
    #endregion
}
