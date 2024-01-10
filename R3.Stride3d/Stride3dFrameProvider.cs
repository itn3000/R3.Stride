using System;
using System.Runtime.CompilerServices;
using R3.Collections;
using Stride.Games;

namespace R3;

public class Stride3dFrameProvider : FrameProvider
{
    FreeListCore<IFrameRunnerWorkItem> list;
    readonly object gate = new object();

    internal StrongBox<double> Delta = default!; // set from Node before running process.

    public Stride3dFrameProvider(IGame game)
    {
        this.list = new FreeListCore<IFrameRunnerWorkItem>(gate);
        _Game = game;
    }

    readonly IGame _Game;

    public override long GetFrameCount()
    {
        if(_Game != null)
        {
            return _Game.UpdateTime.FrameCount;
        }
        else
        {
            return 0;
        }
    }

    public override void Register(IFrameRunnerWorkItem callback)
    {
        list.Add(callback, out _);
    }

    internal void Run(double _)
    {
        long frameCount = GetFrameCount();

        var span = list.AsSpan();
        for (int i = 0; i < span.Length; i++)
        {
            ref readonly var item = ref span[i];
            if (item != null)
            {
                try
                {
                    if (!item.MoveNext(frameCount))
                    {
                        list.Remove(i);
                    }
                }
                catch (Exception ex)
                {
                    list.Remove(i);
                    try
                    {
                        ObservableSystem.GetUnhandledExceptionHandler().Invoke(ex);
                    }
                    catch { }
                }
            }
        }
    }
}
