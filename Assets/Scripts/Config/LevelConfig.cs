using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    //Greate Asset Menu: Adds option to Unity's Assets/Create menu
    [CreateAssetMenu]
    //Lecel Config class: Inherits from ScriptableObject, used to tore level data
    public class LevelConfig : ScriptableObject
    {
        //Serialized field: List of enemy wave groups
        [SerializeField]
        public List<EnemyWaveGroup> EnemyWaveGroups = new List<EnemyWaveGroup>();
    }
    //Enemy wave group class
    [Serializable]
    public class EnemyWaveGroup
    {
        //Group name
        public string Name;
        //Text Area atribute: Group descriotion
        [TextArea] public string Description = string.Empty;
        //List of waves
        [SerializeField]
        public List<EnemyWave> Waves = new List<EnemyWave>();
    }
    
    [Serializable]
    public class EnemyWave
    {
        public string Name;
        public bool Active = true;
        public float GenerateDuration = 1;
        public GameObject EnemyPrefab;
        public int Seconds = 10;
        public float HPScale = 1.0f;
        public float SpeedScale = 1.0f;
    }
}
