using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core;
using Stride.Engine;
using Stride.Games;
using System.Runtime.CompilerServices;

namespace R3.Stride
{
    [ComponentCategory("R3")]
    [Display("R3 Frame Dispatcher")]
    public class R3FrameDispatcherComponent : SyncScript
    {
        StrideFrameProvider? frameProvider;
        StrongBox<double> Delta = new StrongBox<double>();

        public override void Start()
        {
            StrideProvider.SetDefaultObservableSystem(Game);
        }
        public override void Update()
        {
            if(StrideProvider.DefaultFrameProvider != null)
            {
                StrideProvider.DefaultFrameProvider.Delta.Value = Game.UpdateTime.Elapsed.TotalSeconds;
                StrideProvider.DefaultFrameProvider.Run(Game.UpdateTime.Elapsed.TotalSeconds);
            }
        }
    }
}
