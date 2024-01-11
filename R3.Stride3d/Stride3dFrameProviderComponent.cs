using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core;
using Stride.Engine;
using Stride.Games;
using System.Runtime.CompilerServices;

namespace R3.Stride3d
{
    [ComponentCategory("R3")]
    public class Stride3dFrameProviderComponent : SyncScript
    {
        Stride3dFrameProvider? frameProvider;
        StrongBox<double> Delta = new StrongBox<double>();

        public override void Start()
        {
            Stride3dProvider.SetDefaultObservableSystem(Game);
        }
        public override void Update()
        {
            if(Stride3dProvider.DefaultFrameProvider != null)
            {
                Stride3dProvider.DefaultFrameProvider.Delta.Value = Game.UpdateTime.Elapsed.TotalSeconds;
                Stride3dProvider.DefaultFrameProvider.Run(Game.UpdateTime.Elapsed.TotalSeconds);
            }
        }
    }
}
