using System;

namespace LANChat
{
    /// <summary>
    /// Static class for playing wave streams asynchronously from memory using native methods.
    /// This overcomes the inherent problems with garbage collection of .Net SoundPlayer class.
    /// </summary>
    internal static class SoundPlayer
    {
        private static System.Runtime.InteropServices.GCHandle? gcHandle = null;
        
        private static byte[] bytesToPlay = null;
        private static byte[] BytesToPlay
        {
            get { return bytesToPlay; }
            set { FreeHandle(); bytesToPlay = value; }
        }

        public static void PlaySound(System.IO.Stream stream)
        {
            PlaySound(stream, SoundFlags.SND_MEMORY | SoundFlags.SND_ASYNC);
        }

        public static void PlaySound(System.IO.Stream stream, SoundFlags flags)
        {
            LoadStream(stream);
            flags |= (SoundFlags.SND_MEMORY | SoundFlags.SND_ASYNC);

            if (BytesToPlay != null) {
                //  The byte array is pinned so that garbage collector cannot move it.
                gcHandle = System.Runtime.InteropServices.GCHandle.Alloc(BytesToPlay,
                    System.Runtime.InteropServices.GCHandleType.Pinned);
                Win32.PlaySound(gcHandle.Value.AddrOfPinnedObject(), UIntPtr.Zero, (uint)flags);
            }
            else
                Win32.PlaySound((byte[])null, UIntPtr.Zero, (uint)flags);
        }

        private static void LoadStream(System.IO.Stream stream)
        {
            if (stream != null) {
                byte[] bytesToPlay = new byte[stream.Length];
                stream.Read(bytesToPlay, 0, (int)stream.Length);
                BytesToPlay = bytesToPlay;
            }
            else
                BytesToPlay = null;
        }

        private static void FreeHandle()
        {
            if (gcHandle != null) {
                Win32.PlaySound((byte[])null, UIntPtr.Zero, 0);
                gcHandle.Value.Free();
                gcHandle = null;
            }
        }
    }

    [Flags]
    internal enum SoundFlags : int
    {
        SND_SYNC = 0x0000,          //  Play synchronously (default)
        SND_ASYNC = 0x0001,         //  Play asynchronously
        SND_NODEFAULT = 0x0002,     //  Silence (!default) if sound not found
        SND_MEMORY = 0x0004,        //  pszSound points to a memory file
        SND_LOOP = 0x0008,          //  Loop the sound until next sndPlaySound
        SND_NOSTOP = 0x0010,        //  Don't stop any currently playing sound
        SND_NOWAIT = 0x00002000,    //  Don't wait if the driver is busy
        SND_ALIAS = 0x00010000,     //  Name is a registry alias
        SND_ALIAS_ID = 0x00110000,  //  Alias is a predefined id
        SND_FILENAME = 0x00020000,  //  Name is file name
    }
}
