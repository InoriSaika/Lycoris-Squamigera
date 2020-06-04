using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Habilities : MonoBehaviour
{

    [SerializeField] float walkSpeed;
    [SerializeField] float jumpForce;

    Animator animator;
    CryptoAPITransform transform;

    public enum skeletonStates {  idle, walk, jumping, falling, transf, transfreverse, liana, death };
    // 0,     1,     2,       3,      4,          5,         6,     7

    const string STATE_IDLE_NAME = "Static";
    enum Directions {  left, right, max_dir };

    // Start is called before the first frame update
    void Start()
    {
        
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
