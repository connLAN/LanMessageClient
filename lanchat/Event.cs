using System;
using System.Threading;

namespace LANChat
{
    public class Event : IDisposable
    {
        private const uint INFINITE = 0xFFFFFFFF;
        private const string EVENT_NAME = "Global\\lanmsngr-69e3739c-b43e-4c53-af94-f298853d8a44";
        private const string EVENT_TERM_NAME = "Global\\lanmsngr-term-69e3739c-b43e-4c53-af94-f298853d8a44";
        private const uint SYNCHRONIZE = 0x00100000;
        private const uint EVENT_MODIFY_STATE = 0x0002;

        private bool bDisposed = false;
        private IntPtr hEvent = IntPtr.Zero;		//  Handle to event
        private bool bExists = false;
        private MainForm form;

        public delegate void EventSignalHandler();

        public Event()
        {
            InitEvent(GlobalEvents.Instance);
        }

        public Event(GlobalEvents eventType)
        {
            InitEvent(eventType);
        }

        ~Event()
        {
            Dispose(false);
        }

        //  After creation, call this to determine if this is the first instance.
        public bool Exists()
        {
            return bExists;
        }

        //  An instance calls this when it detects that it is
        //  the second instance.  Then it exits.
        public void SignalEvent()
        {
            if (hEvent != IntPtr.Zero)
                Win32.SetEvent(hEvent);
        }

        //  Set a reference to the form so that its event can be raised.
        public void SetObject(MainForm obj)
        {
            form = obj;
        }

        //  Creates an event with the given name. Does not create
        //  if an event with the same name exists.
        private void InitEvent(GlobalEvents eventType)
        {
            string eventName;
            switch (eventType) {
                case GlobalEvents.Terminate:
                    eventName = EVENT_TERM_NAME;
                    break;
                default:
                    eventName = EVENT_NAME;
                    break;
            }

            hEvent = Win32.OpenEvent(EVENT_MODIFY_STATE | SYNCHRONIZE, false, eventName);
            int lastError = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
            if (hEvent == IntPtr.Zero) {
                Win32.SECURITY_ATTRIBUTES securityAttributes = Win32.CreateSecurityAttributes();
                hEvent = Win32.CreateEvent(ref securityAttributes, true, false, eventName);
                lastError = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                if (hEvent != IntPtr.Zero) {
                    Thread thread = new Thread(new ParameterizedThreadStart(WaitForSignal));
                    thread.Start(eventType);
                }
            }
            else {
                bExists = true;
            }
        }

        //  Thread method will wait on the event, which will signal
        //  if another instance tries to start.
        private void WaitForSignal(object eventType)
        {
            while (true) {
                uint result = Win32.WaitForSingleObject(hEvent, INFINITE);

                if (result == 0) {
                    Win32.ResetEvent(hEvent);
                    GlobalEvents _eventType = (GlobalEvents)eventType;
                    switch (_eventType) {
                        case GlobalEvents.Terminate:
                            form.Invoke(form.TermEventSignalled, null);
                            break;
                        default:
                            form.Invoke(form.EventSignalled, null);
                            break;
                    }
                }
                else {
                    // Do not risk a busy loop, let the thread die.
                    break;
                }
            }
        }

        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (!this.bDisposed) {
                if (disposing) {
                    // dispose managed resources
                    if (form != null) {
                        form.Dispose();
                        form = null;
                    }
                }
                // dispose unmanaged resources
                if (hEvent != IntPtr.Zero)
                    Win32.CloseHandle(hEvent);
                hEvent = IntPtr.Zero;

                bDisposed = true;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
