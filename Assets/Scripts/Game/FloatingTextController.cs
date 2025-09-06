using System;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ProjectSurvivor
{
    public partial class FloatingTextController : ViewController
    {
        void Start()
        {
            //Hide floating text on initialzation
            FloatingText.Hide();
        }
        //Play floating text effect
        public static void Play(Vector2 position, string text,bool critical = false)
        {
            //Instantiate floating text effect
            mDefault.FloatingText.InstantiateWithParent(mDefault.transform)
                //set position
                .Position(position.x, position.y)
                //Get sef reference or configuration
                .Self(f =>
                {
                    //Save initial Y position
                    var positionY = position.y;
                    //Find text child object
                    var textTrans = f.transform.Find("Text");
                    //Getx text component
                    var textComp = textTrans.GetComponent<Text>();
                    //Set text content
                    textComp.text = text;
                    //Check if it's critical text
                    if (critical)
                    {
                        //Set text color to red
                        textComp.color = Color.red;
                    }

                    //Create sequence animation
                    ActionKit.Sequence()
                    //First animation: Move upo and scale
                        .Lerp(0, 0.5f, 0.5f, (p) =>
                        {
                            //Move text upward
                            f.PositionY(positionY + p * 0.25f);
                            //X axis scalng (elastic ffect)
                            textComp.LocalScaleX(Mathf.Clamp01(p * 8));
                            //Y axis scaling (elastic effect)
                            textComp.LocalScaleY(Mathf.Clamp01(p * 8));
                        })
                        //Delay for a period
                        .Delay(0.5f)
                        //Second animation: Fade out and destroy
                        .Lerp(1.0f, 0, 0.3f, (p) => { textComp.ColorAlpha(p); },
                            () => { textTrans.DestroyGameObjGracefully(); })
                        .Start(textComp);
                }).Show();
        }
        //Default instance of floating text controller
        private static FloatingTextController mDefault;

        private void Awake()
        {
            mDefault = this;
        }

        private void OnDestroy()
        {
            mDefault = null;
        }
    }
}