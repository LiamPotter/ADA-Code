using UnityEngine;
using System.Collections;

public class ThirdPersonPlayerAnimation : MonoBehaviour
{
public float runSpeedScale = 1.0f;
public float walkSpeedScale = 1.0f;

private PlatformCharacterController platformCharController;
private PhysicsCharacterMotor phsxMotor;

void Start ()
{
	// By default loop all animations
	GetComponent<Animation>().wrapMode = WrapMode.Loop;

	GetComponent<Animation>()["run"].layer = -1;
	GetComponent<Animation>()["walk"].layer = -1;
	GetComponent<Animation>()["idle"].layer = -2;
	GetComponent<Animation>().SyncLayer(-1);



	// The jump animation is clamped and overrides all others
	GetComponent<Animation>()["jump"].layer = 10;
	GetComponent<Animation>()["jump"].wrapMode = WrapMode.ClampForever;

	GetComponent<Animation>()["fall"].layer = 10;	
	GetComponent<Animation>()["fall"].wrapMode = WrapMode.Loop;

	GetComponent<Animation>()["land"].layer = 10;	
	GetComponent<Animation>()["land"].wrapMode = WrapMode.Once;




	// We are in full control here - don't let any other animations play when we start
	GetComponent<Animation>().Stop();
	GetComponent<Animation>().Play("idle");


    platformCharController = GetComponent<PlatformCharacterController>();
    phsxMotor = GetComponent<PhysicsCharacterMotor>();
    
}



void Update ()
{

    // float currentSpeed = rigidbody.velocity.magnitude;

        

    if (phsxMotor.grounded)
    {
        GetComponent<Animation>().Blend("fall", 0.0f, 0.3f);
        GetComponent<Animation>().Blend("jump", 0.0f, 0.3f);
        
        //Fade in Run
        if (GetComponent<Rigidbody>().velocity.magnitude > phsxMotor.maxForwardSpeed -1)
        {
            GetComponent<Animation>().CrossFade("run");

            //fade out walk
            GetComponent<Animation>().Blend("walk", 0.0f, 0.3f);
        }

        // Fade in walk
        else if (GetComponent<Rigidbody>().velocity.magnitude > (phsxMotor.maxForwardSpeed * platformCharController.walkMultiplier - 1))
        {

            GetComponent<Animation>().CrossFade("walk");
            
            // We fade out jumpland realy quick otherwise we get sliding feet
            GetComponent<Animation>().Blend("run", 0.0f, 0.3f);
            
        }


        // Fade out walk and run so just idle remains
        else
        {
            GetComponent<Animation>().Blend("walk", 0.0f, 0.3f);
            GetComponent<Animation>().Blend("run", 0.0f, 0.3f);


        }

        GetComponent<Animation>()["run"].normalizedSpeed = runSpeedScale;
        GetComponent<Animation>()["walk"].normalizedSpeed = walkSpeedScale;

    }



    if (phsxMotor.jumping)
    {


        if (!phsxMotor.jumpingReachedApex && phsxMotor.jumping)
        {
            GetComponent<Animation>().CrossFade("jump", 0.2f);
            GetComponent<Animation>().Blend("fall", 0.0f, 0.3f);
        }

        else
        {
            GetComponent<Animation>().Blend("jump", 0.0f, 0.3f);
            GetComponent<Animation>().CrossFade("fall", 0.2f);

        }
       


        GetComponent<Animation>().Blend("walk", 0.0f, 0.1f);
        GetComponent<Animation>().Blend("run", 0.0f, 0.1f);

    }




   
    
}

public void DidLand () {
	GetComponent<Animation>().Play("land");
}


}//end class