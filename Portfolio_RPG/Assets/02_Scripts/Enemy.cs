using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        None,
        Patrol,
        Attack,
    }

    public EnemyState currentState;
    GameObject target;

    public float lookRange;
    public float moveSpeed;
    Vector3? destination;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.None;
        lookRange = 5f;
        moveSpeed = 10f;
    }

    // 상태 전환 조건 체크
    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.None)
        {
            currentState = EnemyState.Patrol;
        }
        else if (currentState == EnemyState.Patrol)
        {
            if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up, lookRange, transform.forward, out RaycastHit hit, 0.1f))
            {
                Debug.Log($"Get Hit: {hit.collider.name}");
                if (hit.collider.gameObject.GetComponent<CharacterMoveInput>())
                {
                    target = hit.collider.gameObject;
                    currentState = EnemyState.Attack;
                    destination = null;

                    return;
                }
            }

            if (destination == null || (destination.Value - transform.position).magnitude < 0.1f)
            {
                destination = new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f));
            }

            Vector3 direction = (destination.Value - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else if (currentState == EnemyState.Attack)
        {
            if (target == null || (target.transform.position - transform.position).magnitude > 4f)
            {
                currentState = EnemyState.Patrol;

                return;
            }

            Vector3 direction = target.transform.transform.position - transform.position;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}