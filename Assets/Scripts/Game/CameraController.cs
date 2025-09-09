using System;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
    
    //Partial class declaration, inheriing from ViewController
    public partial class CameraController : ViewController
    {
        //Private field: Target position vector. initialized to zero vector
        private Vector2 mTargetPosition = Vector2.zero;
        //Static private field: Default instance of camera controller
        private static CameraController mDefault = null;
        //Static property: Get the transform componet of the left-bottom boundary
        public static Transform LBTrans => mDefault.LB;
        //Static property: Get the trasform component of the right-top boundary
        public static Transform RTTrans => mDefault.RT;

        

        private void Awake()
        {
            mDefault = this;
        }

        private void OnDestroy()
        {
            mDefault = null;
        }

        private void Start()
        {
            //Set application targetframe rate to 60 frames per second
            Application.targetFrameRate = 60;
        }

        //Current camera position
        private Vector3 mCurrentCameraPos;
        //Wether fraes of shaking
        private bool mShake = false;
        //Shake amplitede
        private int mShakeFrame = 0;
        //Trigger camera shake effect
        private float mShakeA = 2.0f; // 振幅

        public static void Shake()
        {
            //Set shake flag to true
            mDefault.mShake = true;
            //Set shake duration frames
            mDefault.mShakeFrame = 30;
            //Set shake amplitude to 0.2
            mDefault.mShakeA = 0.2f;
        }

        private void Update()
        {
            //Check if player object exists
            if (Player.Default)
            {
                //Set target position to player's current position
                mTargetPosition = Player.Default.transform.position;
                //Calculate X position using exponential smoothing
                mCurrentCameraPos.x = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.x, mTargetPosition.x);
                //Calculate Y position using exponential smoothing
                mCurrentCameraPos.y = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.y, mTargetPosition.y);
                //Keep Z position unchanged
                mCurrentCameraPos.z = transform.position.z;
                //Check if shaking is active
                if (mShake)
                {
                    //Decrease remaining shake frames
                    mShakeFrame--;
                    //Calculate current frame's shake amplitude (decaying over time)
                    var shakeA = Mathf.Lerp(mShakeA, 0.0f, (mShakeFrame / 30.0f));
                    //Se5t camera position (add random offset for shake effect）
                    transform.position = new Vector3(mCurrentCameraPos.x + Random.Range(-shakeA, shakeA),
                        mCurrentCameraPos.y + Random.Range(-shakeA, shakeA), mCurrentCameraPos.z);
                    //Check if shaking has ended
                    if (mShakeFrame <= 0)
                    {
                        //Turn off shake flag
                        mShake = false;
                    }
                }
                else//Not shaking
                {
                    //Update x positin using exponential smoothing
                    transform.PositionX(
                        (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                        .Lerp(transform.position.x, mTargetPosition.x));
                    //Update Y position using exponential smoothing
                    transform.PositionY(
                        (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                        .Lerp(transform.position.y, mTargetPosition.y));
                }
            }
        }
    }
}