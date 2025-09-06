using System;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Player : ViewController
    {
        // Player movement speed, adjustable in the Inspector panel
        public float MovementSpeed = 5;
        //Static instance for global access to the player object (Singleton pattern)
        public static Player Default;
        //Private field for storing reference to the walking sound effect audio player
        private AudioPlayer mWalkSfx;
        //Awake method: Called when the object si initialized, ealier than Start method
        private void Awake()
        {
            //Set static instance to current object for easy access by other scripts
            Default = this;
        }
        //OnDestory method: Called when the object is destoryed
        private void OnDestroy()
        {
            //Clean up static instance reference to prevent dangling references
            Default = null;
        }
        //Start method: Called when the object is first enabled, used for initialization
        void Start()
        {
            //Register HurtBox trigger enter event for handling damage detection
            HurtBox.OnTriggerEnter2DEvent(collider2D =>
            {
                //Get HitHurtBox component from collider
                var hitBox = collider2D.GetComponent<HitHurtBox>();
                if (hitBox)
                {
                    //Check if damage source is an enemy
                    if (hitBox.Owner.CompareTag("Enemy"))
                    {
                        //Decrease player health
                        Global.HP.Value--;
                        //Check if health is less than or equal to 0(Player death)
                        if (Global.HP.Value <= 0)
                        {//Play death sound effect
                            AudioKit.PlaySound("Die");
                            //Safely destory player game object
                            this.DestroyGameObjGracefully();
                            //OPen game over panel
                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
                        else
                        {
                            //Play hurt sound effect
                            AudioKit.PlaySound("Hurt");
                        }
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//Automatically unregister
                                                             //event when gameobject is destoryed
            //Local function: Update health UI display
            void UpdateHP()
            {
                //Calculate health fill ratio and set UI
                HPValue.fillAmount = Global.HP.Value / (float)Global.MaxHP.Value;
            }
            //Register health change event, called on initialization and value change
            Global.HP.RegisterWithInitValue(hp => { UpdateHP(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);//Automatically unregister
                                                               //when object is destoryed
            //Register max health chane event
            Global.MaxHP.RegisterWithInitValue(maxHp => { UpdateHP(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        //private field: Record player's current facing direction (true = right, false = left)
        private bool mFaceRight = true;
        //Uodate methodL Called every frame, used to handle player input and update state
        private void Update()
        {
            //get horizontal input value(-1,0,1)
            var horizontal = Input.GetAxisRaw("Horizontal"); // 1
            //Get vertical input value(-1,0,1)
            var vertical = Input.GetAxisRaw("Vertical"); // 1
            //Calculate target velocity vector(normalized and mutiplied by speed and speed rate)
            var targetVelocity = new Vector2(horizontal, vertical).normalized *
                                 (MovementSpeed * Global.MovementSpeedRate.Value);
            //Check if player is stationary(on input)
            if (horizontal == 0 && vertical == 0)
            {
                //Play corresponding idle animation based on facing direction
                if (mFaceRight)
                {
                    Sprite.Play("PlayerIdleRight");//Right-facing idle animation
                }
                else
                {
                    Sprite.Play("PlayerIdleLeft");//Left-facing animation
                }
                //If walking soud effect is playing, stop it
                if (mWalkSfx != null)
                {
                    mWalkSfx.Stop();
                    mWalkSfx = null;//Clear reference
                }
            }
            else//Player is moving
            {
                //If walking sound effect is not playing, start it
                if (mWalkSfx == null)
                {
                    
                    mWalkSfx = AudioKit.PlaySound(Sfx.WALK, true);
                }
                //Update character facing direction based on horizontal input
                if (horizontal > 0)
                {
                    mFaceRight = true;
                } 
                else if (horizontal < 0)
                {
                    mFaceRight = false;
                }
                
                if (mFaceRight)
                {
                    Sprite.Play("PlayerWalkRight");
                }
                else
                {
                    Sprite.Play("PlayerWalkLeft");
                }
            }
            //Use linear interpolation to smoothly update rigidbody's linear velocity
            //Mathf.Exp(-Time.deltaTimne * 5)is used to calculate smoothling factor for more natural movement
            SelfRigidbody2D.linearVelocity =
                Vector2.Lerp(SelfRigidbody2D.linearVelocity, targetVelocity, 1 - Mathf.Exp(-Time.deltaTime * 5));
        }
    }
}