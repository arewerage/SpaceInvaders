using System;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace _Project._Codebase.Core.SceneLoader
{
    public interface ISceneLoader
    {
        UniTask<SceneInstance> LoadAsync(string name, IProgress<float> progress = null);
    }
}