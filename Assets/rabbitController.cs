using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class rabbitController : MonoBehaviour
{

    public enum State { Patrolling, Chasing, Waiting, Charge };
    public State currentState = State.Patrolling;

    [Header("Configuration")]
    [Tooltip("Carpeta de gameObjects")]
    public Transform[] Waypoints;
    public Transform player;
    public float speed = 2f;
    public float detectionRange = 5f;
    public float ChargeRange = 3f;
    public float waitTime = 2f;

    private Transform playerChargePosition;
    private int currentWatpointIndex = 0;
    private Animator anim;
    private SpriteRenderer sprite;
    void patrol()
    {
        Transform target = Waypoints[currentWatpointIndex];
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(target.position.x, -1.25f), speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.1) StartCoroutine(WaitWaypoint());
        RotarSprite(target);
    }

    IEnumerator WaitWaypoint()
    {
        if (currentState != State.Patrolling) yield return null;
        currentState = State.Waiting;
        yield return new WaitForSeconds(waitTime);
        currentWatpointIndex = UnityEngine.Random.Range(0, Waypoints.Length);
        currentState = State.Patrolling;
    }

    void chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(player.position.x, -1.25f), speed * Time.deltaTime * 1.2f);
        RotarSprite(player);
    }
    void charge()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(player.position.x, -1.25f), speed * Time.deltaTime * 1.3f);
        StartCoroutine(WaitWaypoint());
        RotarSprite(player);
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    private void RotarSprite(Transform objetivo)
    {
        if (transform.position.x < objetivo.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {

            if (currentState != State.Charge)
            {
                StopAllCoroutines();
                currentState = State.Chasing;
            }
            if (distanceToPlayer < ChargeRange)
            {
                if (currentState == State.Chasing) playerChargePosition = player;
                currentState = State.Charge;
            }
        }
        else if (currentState == State.Chasing && distanceToPlayer >= detectionRange)
        {
            currentWatpointIndex = UnityEngine.Random.Range(0, Waypoints.Length);
            currentState = State.Patrolling;
        }

        switch (currentState)
        {
            case State.Chasing:
                chase();
                anim.SetBool("IsRunning" , true);
                break;
            case State.Patrolling:
                patrol();
                anim.SetBool("IsRunning", true);
                break;
            case State.Waiting:
                anim.SetBool("IsRunning", false);
                break;
            case State.Charge:
                charge();
                anim.SetBool("IsRunning", true);
                break;

        }
    }
}
