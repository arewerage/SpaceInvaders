﻿using _Project._Codebase.Configs;
using _Project._Codebase.UI.Screens;
using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Services.UI.Screen
{
    public interface IScreenService
    {
        UniTask<IUIScreen> Show(ScreenId screenId);
    }
}