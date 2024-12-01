using System;
using Code.Modules.StarSystemsModule.Enums;
using UnityEditor;
using UnityEngine;

namespace Code.Modules.StarSystemsModule.Configs
{
    [CreateAssetMenu(menuName = "StarSystems/StarSystemsConfig", fileName = "StarSystemsConfig")]
    public class StarSystemsConfig : ScriptableObject
    {
        [SerializeField] private StarSystemData[] _starSystemsData;
        
        public StarSystemData GetStarSystemByType(StarSystemType model)
        {
            foreach (var starSystemData in _starSystemsData)
            {
                if (starSystemData.Model != model)
                {
                    continue;
                }
                
                return starSystemData;
            }

            return null;
        }
    }
    
    [Serializable]
    public class StarSystemData
    {
        public StarSystemType Model;
        public SceneAsset Scene;
    }
}