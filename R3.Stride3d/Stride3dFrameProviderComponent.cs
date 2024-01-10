using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core;
using Stride.Engine;
using Stride.Games;

namespace R3.Stride3d
{
    public class Stride3dFrameProviderComponent : SyncScript
    {
        Stride3dFrameProvider? frameProvider;

        public override void Start()
        {
            Stride3dProvider.SetDefaultObservableSystem(Game);
        }
        public override void Update()
        {
            Stride3dProvider.DefaultFrameProvider?.Run(Game.UpdateTime.Elapsed.TotalSeconds);
        }
    }
}
