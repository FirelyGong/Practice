using System;

namespace Practice
{
    public abstract class DisposableObject:IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                DoDispose();
            }
        }

        protected abstract void DoDispose();
    }
}
