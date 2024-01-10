using Stride.Engine;

namespace R3;

public class Stride3dFrameProviderDispatcher : SyncScript
{
    Stride3dFrameProvider? frameProvider = null;
    public override void Start()
    {
        frameProvider = new Stride3dFrameProvider(this.Game);
    }
    public override void Update()
    {
        frameProvider?.Run(Game.UpdateTime.Elapsed.TotalSeconds);
    }
}
