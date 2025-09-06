using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
    public partial class EnemyGenerator : ViewController
    {
        //Serialized fied: Level configuraion data
        [SerializeField] public LevelConfig Config;
        //Current generation timr
        private float mCurrentGenerateSeconds = 0;
        //Current ava timer
        private float mCurrentWaveSeconds = 0;
        //Bindable property: Counter for current enemy count
        public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);

        //Enemy wave queue
        private Queue<EnemyWave> mEnemyWavesQueue = new Queue<EnemyWave>();
        //Number of wabes generated
        public int WaveCount = 0;
        //Total number of waes
        private int mTotalCount = 0;
        //Whether this is the last wave
        public bool LastWave => WaveCount == mTotalCount;
        //Called when the object is first enabled
        public EnemyWave CurrentWave => mCurrentWave;

        private void Start()
        {
            ////Iterate through all waves in the group
            foreach (var group in Config.EnemyWaveGroups)
            {
                //Iterate through all waves inj the group
                foreach (var wave in group.Waves)
                {
                    //Add wave to queue

                    mEnemyWavesQueue.Enqueue(wave);
                    //Increase total wave count
                    mTotalCount++;
                }
            }
        }
        //Current wave being processed
        private EnemyWave mCurrentWave = null;

        private void Update()
        {
            //Check if there s currently an active wave
            if (mCurrentWave == null)
            {
                //If there are waves remaining in the queue
                if (mEnemyWavesQueue.Count > 0)
                {
                    //Increase wave count
                    WaveCount++;
                    //Get next wae from queue
                    mCurrentWave = mEnemyWavesQueue.Dequeue();
                    //Reset generation timer
                    mCurrentGenerateSeconds = 0;
                    //Reset wave timer
                    mCurrentWaveSeconds = 0;
                }
            }
            //If there is an active wave
            if (mCurrentWave != null)
            {
                //Update generation timer
                mCurrentGenerateSeconds += Time.deltaTime;
                //Update wave timer
                mCurrentWaveSeconds += Time.deltaTime;
                //Check if generation interval has been reached
                if (mCurrentGenerateSeconds >= mCurrentWave.GenerateDuration)
                {
                    //Reset generation timer
                    mCurrentGenerateSeconds = 0;
                    //Get payer reference
                    var player = Player.Default;
                    //If player exists
                    if (player)
                    {
                        //Randomly choose to generate on X or Y axis boundary
                        var xOry = RandomUtility.Choose(-1, 1);
                        //Initialize position vector
                        var pos = Vector2.zero;
                        //Determine generation position based on chosen axis
                        if (xOry == -1)//
                        {
                            //Random X coordinate within screen bounds
                            pos.x = RandomUtility.Choose(CameraController.LBTrans.position.x,
                                CameraController.RTTrans.position.x);
                            //Random Y coordinate within screen bounds
                            pos.y = Random.Range(CameraController.LBTrans.position.y,
                                CameraController.RTTrans.position.y);
                        }
                        else
                        {
                            // Random X coordinate within screen bounds
                            pos.x = Random.Range(CameraController.LBTrans.position.x,
                                CameraController.RTTrans.position.x);
                            // Randomly choose bottom or top boundary
                            pos.y = RandomUtility.Choose(CameraController.LBTrans.position.y,
                                CameraController.RTTrans.position.y);
                        }
                        
                        //Instantiate enemy prefab and set properties
                        mCurrentWave.EnemyPrefab.Instantiate()
                            .Position(pos)//Set position
                            .Self(self =>//Get self reference
                            {
                                //Get enemy component
                                var enemy = self.GetComponent<IEnemy>();
                                //set speed scale
                                enemy.SetSpeedScale(mCurrentWave.SpeedScale);
                                //set HP scale
                                enemy.SetHPScale(mCurrentWave.HPScale);
                            })
                            .Show();//Show enemy
                    }
                }
                //Checke if wave tie has ended
                if (mCurrentWaveSeconds >= mCurrentWave.Seconds)
                {
                    //Clear current wave
                    mCurrentWave = null;
                }
            }
        }
    }
}