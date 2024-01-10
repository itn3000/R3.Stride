using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Games;
using Stride.Core.Diagnostics;
using System.Threading;

namespace R3.Stride3d
{
    public static class Stride3dProvider
    {
        static readonly Logger _logger = GlobalLogger.GetLogger(nameof(Stride3dProvider));
        static void DefaultUnobservableException(Exception exception)
        {
            _logger.Error(exception.ToString());
        }
        public static Stride3dFrameProvider? DefaultFrameProvider;
        public static Stride3dTimeProvider? DefaultTimeProvider;
        static IGame? gameObject;
        public static void SetDefaultObservableSystem(IGame game, Action<Exception>? unobservableAction = null)
        {
            if(game != null && (gameObject == null || game != gameObject))
            {
                while (true)
                {
                    IGame? old = gameObject;
                    var retval = Interlocked.CompareExchange(ref gameObject, game, old);
                    if (old == retval)
                    {
                        break;
                    }
                }
                if (unobservableAction != null)
                {
                    ObservableSystem.RegisterUnhandledExceptionHandler(unobservableAction);
                }
                else
                {
                    ObservableSystem.RegisterUnhandledExceptionHandler(DefaultUnobservableException);
                }
                DefaultFrameProvider = new Stride3dFrameProvider(game);
                DefaultTimeProvider = new Stride3dTimeProvider(game);
                ObservableSystem.DefaultFrameProvider = DefaultFrameProvider;
                ObservableSystem.DefaultTimeProvider = DefaultTimeProvider;
            }
        }
    }
}
