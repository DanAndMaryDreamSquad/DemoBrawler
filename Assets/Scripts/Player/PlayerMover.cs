﻿using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {
	
	public float speed = 6.0F;
	public float comboSpeed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float runAngle;
	public Animator animator;	
	private GameObject camera;
	private GameObject lastCameraTransform;
	private float animationSpeed = 1.0f;
	private Vector3 desiredDirection = Vector3.zero;
    private Vector3 desiredDirectionWithSpeed = Vector3.zero;
    private CharacterController controller;
	private AudioSource jumpAudio;
    private bool isMoving = false;
    private bool fullStop = true;
    private Quaternion lastDesiredFacingRotation = Quaternion.identity;
    private Quaternion defaultLeanRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
	private Vector3 preJumpDesiredDirection = Vector3.zero;
	private float yMovement = 0;
	private PlayerFighting playerFighting;

    void Start () {
		camera = GameObject.FindGameObjectWithTag("CameraObject");
		lastCameraTransform = camera;
        //animator = GetComponent<Animator> ();
        controller = GetComponent<CharacterController> ();
		lastDesiredFacingRotation = Quaternion.Euler (0, 45, 0);
		playerFighting = GetComponent<PlayerFighting>();
    }

	void Update () {
		if (!playerFighting.IsComboing) {
            FindDirection ();
		    Falling ();
            Move ();
	    } else {
			StudderStep();
		}
    }

    void FindDirection () {
        desiredDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        if (desiredDirection.sqrMagnitude > 1) {
            desiredDirection = desiredDirection.normalized;
        }
        if (desiredDirection.sqrMagnitude == 0) {
            fullStop = true;
		}
		if (!isMoving && desiredDirection.sqrMagnitude <= 0.05f) {
			desiredDirection = Vector3.zero;
		}

		desiredDirection = lastCameraTransform.transform.TransformDirection (desiredDirection);
		desiredDirection = Quaternion.AngleAxis(-camera.transform.eulerAngles.x, camera.transform.right) * desiredDirection;
        desiredDirection.y = 0.0f;
		animationSpeed = desiredDirection.sqrMagnitude;

        if ((fullStop && desiredDirection.sqrMagnitude > 0.0f) || (!isMoving && desiredDirection.sqrMagnitude > 0.05f) || (isMoving && desiredDirection.sqrMagnitude > 0.05f)) {
            Quaternion desiredFacingRotation = Quaternion.LookRotation (desiredDirection, Vector3.up);
            Quaternion desiredLeanRotation = Quaternion.Euler (new Vector3 (runAngle * desiredDirection.magnitude, 0, 0));
            this.transform.rotation = Quaternion.Slerp (this.transform.rotation, desiredFacingRotation * desiredLeanRotation, Time.deltaTime * 10);
            lastDesiredFacingRotation = desiredFacingRotation;
            isMoving = true;
        } else {
            this.transform.rotation = Quaternion.Lerp (this.transform.rotation, lastDesiredFacingRotation * defaultLeanRotation, Time.deltaTime * 10);
            fullStop = false;
            isMoving = false;
        }

        //desiredDirection *= speed;
    }

	void Falling () {
		if (controller.isGrounded) {
			yMovement = 0;
		}
		yMovement -= gravity * Time.deltaTime;
	}    

	void Move () {
		desiredDirection.y = yMovement;
        controller.Move (desiredDirection * Time.deltaTime * speed);
        animator.SetBool ("IsWalking", isMoving);
		if (isMoving) {
		    animator.speed = Mathf.Max(0.5f, animationSpeed);
		} else {			
			animator.speed = 1.0f;
		}
    }

	void StudderStep() {
		desiredDirection.y = 0;
		if (isMoving) {
			this.transform.rotation = Quaternion.LookRotation (desiredDirection, Vector3.up);
			//Debug.Log("rt " + this.transform.rotation.eulerAngles);
			//Debug.Log("dd " + desiredDirection);
		} else {
			desiredDirection = this.transform.forward;
		}
		desiredDirection = desiredDirection.normalized;
		controller.Move (desiredDirection * Time.deltaTime * comboSpeed);
		animator.speed = 1.0f;
	}
}
