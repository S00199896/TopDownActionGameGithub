using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlSwitcher : MonoBehaviour
{

    bool canEnterVehicle;
    bool isInVehicle;

    GameObject currentVehicle;
    PlayerOnFootMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerOnFootMovement>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnterTrigger") && !isInVehicle)
        {
            canEnterVehicle = true;
            currentVehicle = collision.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnterTrigger") && isInVehicle)
        {
            canEnterVehicle = false;
            currentVehicle = null; //null means there is nothing stored in this variable, trying to access it will result in an error
        }
    }

    private void Update()
    {
        if(canEnterVehicle && !isInVehicle)
        {
            //enter car
            if (Input.GetKeyDown(KeyCode.E)) //GetKeyDown only activates once when the button is pressed once
                enterCar();
        }
        else if (isInVehicle)
        {
            //exit car
            if (Input.GetKeyDown(KeyCode.E))
                exitCar();
        }
    }

    void enterCar()
    {
        //update player
        playerMovement.enabled = false;
        playerMovement.gameObject.GetComponent<Collider2D>().enabled = false; //turns off collider of player
        playerMovement.gameObject.GetComponent<SpriteRenderer>().enabled = false; //turns off sprite of player
        playerMovement.gameObject.GetComponent<Rigidbody2D>().isKinematic = true; //turns off rigidbody of player

        playerMovement.gameObject.transform.position = currentVehicle.transform.position;
        playerMovement.gameObject.transform.parent = currentVehicle.transform;

        //update vehicle
        currentVehicle.GetComponent<PlayerVehicleMovement>().enabled = true;
        isInVehicle = true;
    }

    void exitCar()
    {
        //Update Player
        playerMovement.enabled = true;
        playerMovement.gameObject.GetComponent<Collider2D>().enabled = true;
        playerMovement.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        playerMovement.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        playerMovement.gameObject.transform.parent = null;

        //Update Vehicle
        currentVehicle.GetComponent<PlayerVehicleMovement>().enabled = false;
        isInVehicle = false;
    }
}
