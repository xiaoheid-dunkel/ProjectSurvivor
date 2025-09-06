using System;
using System.Collections;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
    public partial class TestMaxPowerUpCount : ViewController
    {
        private int mPowerUpCount = 0;

        IEnumerator Start()
        {
            var powerUpManager = FindObjectOfType<PowerUpManager>();

            powerUpManager.GetAllExp.Instantiate()
                .Position(gameObject.Position())
                .Show();

            for (int i = 0; i < 1000; i++)
            {
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));
                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                    Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                Global.GeneratePowerUp(gameObject, false);
                mPowerUpCount++;
                yield return new WaitForEndOfFrame();
            }
        }


        private void OnGUI()
        {
            var cached = GUI.matrix;
            IMGUIHelper.SetDesignResolution(960, 540);
            GUILayout.Space(10);
            GUILayout.Label(mPowerUpCount.ToString());


            GUI.matrix = cached;
        }
    }
}