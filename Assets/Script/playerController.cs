using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
    
    public class playerController : MonoBehaviour
    {
    

        public float playerSpeed;
        public float collisonOffset = 0.05f;
        public ContactFilter2D movementFilter; 
        public Vector2 movementInput;

        [SerializeField]
        private InputActionReference attack;

        
        Rigidbody2D rb;
        List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
        
        Animator animator;
        SpriteRenderer spriteRenderer;
        

        bool canMove = true;
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
            if(canMove){

                //rb.AddForce(movementInput * playerSpeed * Time.deltaTime);
                if(movementInput != Vector2.zero){
                    bool success = TryMove(movementInput);

                    if(!success){
                        success = TryMove(new Vector2(movementInput.x, 0));
                   
                    }
                    if(!success){
                        success = TryMove(new Vector2(movementInput.y, 0));
                    }

                    animator.SetBool("isMoving", success);
                 } else{
                    animator.SetBool("isMoving", false);
                }

                if(movementInput.x < 0){
                    spriteRenderer.flipX = true;
                    weaponParent.attackDirection = WeaponParent.AttackDirection.left;
                
                } else if(movementInput.x > 0){
                    spriteRenderer.flipX = false;
                    weaponParent.attackDirection = WeaponParent.AttackDirection.right;
                }

             
            }
           
                
        }

        private bool TryMove(Vector2 direction){
            if(direction != Vector2.zero)
            {
                int count = rb.Cast(
                        direction,
                        movementFilter,
                        castCollisions,
                        playerSpeed * Time.fixedDeltaTime + collisonOffset
                    );

                    if(count == 0){
                        rb.MovePosition(rb.position + direction * playerSpeed * Time.fixedDeltaTime);
                        return true;
                    } else {
                        return false;
                    } 
            }
                else
                    {
                        return false;
                    }
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

        public void LockMovement(){
            canMove = false;
        }

        public void UnlockMovement(){
            canMove = true;
        }
    }