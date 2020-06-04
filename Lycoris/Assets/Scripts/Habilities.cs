using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class Habilities : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private GameObject graphic;

    Animator animator;
    Transform transform;

    public enum skeletonStates {  idle, walk, jumping, falling, transf, transfreverse, liana, death, skel_max_states };
                                // 0,     1,     2,       3,      4,          5,         6,     7

    const string STATE_IDLE_NAME = "Static";
    enum Directions {  left, right, max_dir };
    Directions currentDirection = Directions.left;
    public skeletonStates currentAnimationState = skeletonStates.idle;

    bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();

        Screen.orientation = ScreenOrientation.Landscape;
    }


    void changeState(skeletonStates state)
    {
        if (currentAnimationState != state)
        {
            animator.SetInteger("DestinationState", (int)state);
            currentAnimationState = state;
            print("cambio de estados activo");
        }
    }


    bool isPlaying(skeletonStates state)
    {
        return state ==
    }




    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("e"))
        {
            print("E key was pressed");
        }
    }
}
