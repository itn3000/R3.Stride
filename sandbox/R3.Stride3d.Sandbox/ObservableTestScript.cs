using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace R3.Stride3d.Sandbox
{
    public class ObservableTestScript : StartupScript
    {
        // Declared public member fields and properties will show in the game studio

        public override void Start()
        {
            Stride3dProvider.SetDefaultObservableSystem(Game);
            Observable.EveryUpdate(Stride3dProvider.DefaultFrameProvider)
                .ThrottleLastFrame(60)
                .Subscribe(x =>
                {
                    Log.Info($"everyupdate - sampleframe(10): {Stride3dProvider.DefaultFrameProvider.GetFrameCount()}, {Game.UpdateTime.Elapsed}");
                });
            // Initialization of the script.
        }
    }
}
