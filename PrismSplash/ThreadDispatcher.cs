using System;
using System.Windows.Threading;

namespace PrismSplash
{
    public delegate void DoEvents(object arg);

    public class ThreadDispatcher
    {
        #region Fields

        private static ThreadDispatcher dispatcher;
        private readonly Dispatcher uiDispatcher;

        #endregion

        #region Private Constructor

        private ThreadDispatcher()
        {
            uiDispatcher = Dispatcher.CurrentDispatcher;
        }

        #endregion

        #region Singleton

        public static ThreadDispatcher Instance
        {
            get
            {
                if(dispatcher == null)
                    InitializeDispatcher();

                return dispatcher;
            }
        }

        private static void InitializeDispatcher()
        {
            dispatcher = new ThreadDispatcher();
        }

        #endregion

        #region Invoke Methods

        public virtual void BeginInvoke(DispatcherPriority priority, Delegate method)
        {
            if (!uiDispatcher.HasShutdownStarted)
                uiDispatcher.BeginInvoke(priority, method);
        }

        public virtual void BeginInvoke(DispatcherPriority priority, Delegate method, object arg)
        {
            if (!uiDispatcher.HasShutdownStarted)
                uiDispatcher.BeginInvoke(priority, method, arg);
        }

        public virtual void BeginInvoke(DispatcherPriority priority, Delegate method, object arg, params object[] param)
        {
            if (!uiDispatcher.HasShutdownStarted)
                uiDispatcher.BeginInvoke(priority, method, arg, param);
        }

        public void BeginInvoke(DispatcherPriority priority, Func<object, object> action, object arg)
        {
            if (!uiDispatcher.HasShutdownStarted)
            {
                uiDispatcher.BeginInvoke(priority, action, arg);
            }
        }

        public virtual void Invoke(DispatcherPriority priority, Delegate method, object arg, params object[] param)
        {
            if (!uiDispatcher.HasShutdownStarted)
                uiDispatcher.Invoke(priority, method, arg, param);
        }

        public virtual void Invoke(DispatcherPriority priority, Delegate method, object arg)
        {
            if (!uiDispatcher.HasShutdownStarted)
            {
                if (arg == null)
                {
                    uiDispatcher.Invoke(priority, method);
                }
                else
                {
                    uiDispatcher.Invoke(priority, method, arg);
                }
            }
        }

        public virtual void Invoke(DispatcherPriority priority, Delegate method)
        {
            Invoke(priority, method, null);
        }

        public virtual void Invoke(DispatcherPriority priority, Action<object> action, object arg)
        {
            if(!uiDispatcher.HasShutdownStarted)
            {
                if(arg == null)
                {
                    uiDispatcher.Invoke(priority, action);
                }
                else
                {
                    uiDispatcher.Invoke(priority, action, arg);
                }
            }
        }

        public virtual void Invoke(DispatcherPriority priority, Action<object> action)
        {
            Invoke(priority, action, null);
        }

        #endregion

        #region DoEvents

        public void DoEvents()
        {
            var frame = new DispatcherFrame();
            BeginInvoke(DispatcherPriority.Loaded, new DoEvents(arg => 
                                                                    {
                                                                        var f = arg as DispatcherFrame;
                                                                        if (f != null)
                                                                            f.Continue = false;
                                                                    }), frame);
            Dispatcher.PushFrame(frame);
        }

        #endregion
    }
}
