using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationChanger : MonoBehaviour
{
    Animator animator;
    int horizontalVelocityId;
    Vector3 prePosition;

    [SerializeField] private float velocity;

    private void Start()
    {
        animator = GetComponent<Animator>();
        horizontalVelocityId = Animator.StringToHash("horizontalVelocity");
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 deltaPosition = currentPosition - prePosition;
        prePosition = currentPosition;

        deltaPosition.y = 0f;
        velocity = deltaPosition.magnitude / Time.deltaTime;

        animator.SetFloat(horizontalVelocityId, velocity);
    }
}
