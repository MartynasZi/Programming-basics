using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRodBehaviour : MonoBehaviour
{
    //This script handles the control rods when they are in the world.
    //
    //That includes launching etc.
    //But also destroying itself when picked up etc.
    //More comments will follow.
    

    //Internal Variables
    private bool hasCollided;
    private Rigidbody rb;
    public int ControlRodID;


    #region Init
    private void Start()
    {

    }
    #endregion

    #region Update
    private void Update()
    {
        //Everything non-physics related goes here!
    }
    private void FixedUpdate()
    {
        //Everything physics related goes here!
        TurnControlRodForward();
    }
    #endregion

    #region Unity Internal
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Reactor"))
            return; // skip this collision

        hasCollided = true;
    }
    #endregion

    #region Methods
    public void LaunchControlRod(float minForce, float maxForce)
    {
        rb = GetComponent<Rigidbody>();

        Vector3 randomDirection = Random.insideUnitSphere;                  //Random vector.
        Vector3 controlRodDirection = Vector3.up;                           //Upwards vector.
        float controlRodForce = Random.Range(minForce, maxForce);           //Random force. Dictated through method.

        randomDirection.y = randomDirection.y * 0.7f;                       //Reduce the random vector's y direction.
        controlRodDirection += randomDirection;                             //Add the two vectors: Will always go up and in random direction.

        Vector3.Normalize(controlRodDirection);                             //Normalize the vector: Makes it more consistent for adding forces & physics.
                                                                            //Normalize = Scaled the Vector length (magnitude) to 1.<

        rb.AddForce(controlRodDirection * controlRodForce, ForceMode.Impulse); //Add the force to the object.
    }
    private void TurnControlRodForward()
    {
        if (!hasCollided && rb.linearVelocity.sqrMagnitude > 0.01f) //If there was no collision, and the rod is still moving.        
        {
            //Match the rotaion of the rod to the direction it moves towards. Otherwise it looks weird.
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized); 
        }            
    }
    #endregion

}
