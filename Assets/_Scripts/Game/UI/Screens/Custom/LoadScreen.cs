using System.Collections.Generic;
using _Scripts.Common.Logger;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Game.UI.Screens.Custom
{
    public class LoadScreen : BaseScreen
    {
        private List<string> lol = new();
        private Dictionary<string, int> lol2 = new();
        
        public override UniTask Show(ScreenOpenHideMode mode)
        {
            lol.Add("lox");
            lol.Add("xyi");
            lol.Add("zalypa");

            lol2.Add("lox", 512);
            lol2.Add("zalypa", 77);
            lol2.Add("xyi", 5333);
            
            DebugExtensions.LogDetailed(this, "LoadScreen");
            DebugExtensions.LogDetailed(lol, "LoadScreen");
            DebugExtensions.LogDetailed(lol2, "LoadScreen");
            return base.Show(mode);
        }

        public override UniTask Hide(ScreenOpenHideMode mode)
        {
            return base.Hide(mode);
        }

        public override UniTask Destroy()
        {
            return base.Destroy();
        }
    }
}