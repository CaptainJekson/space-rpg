using System;
using System.Threading.Tasks;
using Code.Modules.StarSystemsModule.Configs;
using Code.Modules.StarSystemsModule.Enums;
using Plugins.Injection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Modules.StarSystemsModule.Processors
{
    public class StarSystemLoader
    {
        [Inject] private StarSystemsConfig _starSystemsConfig;

        public async void Load(StarSystemType starSystemType, Action onComplete = null)
        {
            var starSystemData = _starSystemsConfig.GetStarSystemByType(starSystemType);
            var loadOperation = SceneManager.LoadSceneAsync(starSystemData.Scene.name, LoadSceneMode.Additive);
            
            await WaitOperation(loadOperation, onComplete);
        }

        public async void UnLoad(StarSystemType starSystemType, Action onComplete = null)
        {
            var starSystemData = _starSystemsConfig.GetStarSystemByType(starSystemType);
            var loadOperation = SceneManager.UnloadSceneAsync(starSystemData.Scene.name);

            await WaitOperation(loadOperation, onComplete);
        }

        private async Task WaitOperation(AsyncOperation loadOperation, Action onComplete)
        {
            while (!loadOperation.isDone)
            {
                await Task.Yield();
            }
            onComplete?.Invoke();
        }
    }
}