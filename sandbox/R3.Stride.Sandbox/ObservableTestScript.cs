using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace R3.Stride.Sandbox
{
    public class ObservableTestScript : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        StrideFrameProvider frameProvider;
        public override void Start()
        {
            frameProvider = new StrideFrameProvider(Game);
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    Log.Info($"interval: {Game.UpdateTime.Total}");
                });
            Observable.EveryUpdate()
                .ThrottleLastFrame(60)
                .Subscribe(x =>
                {
                    Log.Info($"everyupdate - sampleframe(10): {frameProvider.GetFrameCount()}, {Game.UpdateTime.Elapsed}");
                });
            Observable.EveryUpdate(frameProvider)
                .ThrottleLastFrame(180, frameProvider)
                .Subscribe(x =>
                {
                    Log.Info($"everyupdate - sampleframe(180), local frame provider: {frameProvider.GetFrameCount()}, {Game.UpdateTime.Elapsed}");
                });
            // Initialization of the script.
        }

        public override void Update()
        {
            frameProvider.Run(Game.UpdateTime.Elapsed.TotalSeconds);
        }
    }
}
