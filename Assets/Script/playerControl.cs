using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
    
    public class playerControl: MonoBehaviour
    {
        // bool IsMoving{
        //     set{
        //         isMoving = value;
        //         animator.SetBool ("isMoving", isMoving);
        //     }
        // }

        public float moveSpeed = 6f;
        public float maxSpeed = 8f;
        public float idleFriction = 0.9f;

        public ContactFilter2D movementFilter; 
        Vector2 movementInput = Vector2.zero;

        [SerializeField]
        private InputActionReference attack;

        
        Rigidbody2D rb;
        List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
        
        Animator animator;
        SpriteRenderer spriteRenderer;

        //bool isMoving = false;
        

        //bool canMove = true;
        private WeaponParent weaponParent;

        private void OnEnable() {
            attack.action.performed += PerformAttack;
        }

        private void OnDisable() {
            attack.action.performed -= PerformAttack;
        }

    public void PerformAttack(InputAction.CallbackContext obj)
    {
        weaponParent.Attack();
    }

    // Start is called before the first frame update
    void Start() {

            
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            weaponParent = GetComponentInChildren<WeaponParent>();
            
        }
    
        // Update is called once per frame
       
        void FixedUpdate() 
        {
            if(movementInput != Vector2.zero){

                //rb.AddForce(movementInput * playerSpeed * Time.deltaTime);
                rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);

                if(movementInput.x < 0){
                    spriteRenderer.flipX = true;
                    weaponParent.attackDirection = WeaponParent.AttackDirection.left;
                
                } else if(movementInput.x > 0){
                    spriteRenderer.flipX = false;
                    weaponParent.attackDirection = WeaponParent.AttackDirection.right;
                }

                //IsMoving = true;

             
            } else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
                //IsMoving = false;
            }
                UpdateAnimatorPrameters();
           
                
        }

        public void OnMove(InputValue movementValue){
            movementInput = movementValue.Get<Vector2>();

            //if(movementInput != Vector2.zero){
                //animator.SetFloat("XInput", movementInput.x);
               // animator.SetFloat("YInput", movementInput.y);
            //}
        }
        
        void OnFire(){
            animator.SetTrigger("swordAttack");
        }

        void UpdateAnimatorPrameters()
        {
            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
        }

        // public void LockMovement(){
        //     canMove = false;
        // }

        // public void UnlockMovement(){
        //     canMove = true;
        // }
    }
