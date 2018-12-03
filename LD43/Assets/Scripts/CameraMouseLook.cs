using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseLook : MonoBehaviour {

    public bool cursorLocked;

    public float sensitivity;
    public float smoothing;
    public int minY;
    public int maxY;
    

    Vector2 mouseLook;
    //Vector2 smoothV;
    GameObject character;


	void Start ()
    {
        character = this.transform.parent.gameObject;
        SetCursorState();
    }
	
	void Update ()
    {


		Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")); //mus-input

        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            ////smoothing rörelsen mellan två punkter, istället för att snapa mellan två punkter (märkte ingen större skillnad med eller utan)
        //smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f / smoothing);
        //smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f / smoothing);
        //mouseLook += smoothV;

        mouseLook += mouseDelta;

        mouseLook.y = Mathf.Clamp(mouseLook.y, minY, maxY);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right); //kameran roterar runt x-axeln
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up); //karaktär och kamera rotera runt y-axel
    }

    void SetCursorState()
    {
        if (cursorLocked == true)
            Cursor.lockState = CursorLockMode.Locked;
    }
}
