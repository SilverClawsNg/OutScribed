using Microsoft.AspNetCore.Components;

namespace OutScribed.Client.Disposable
{
    public abstract class DisposableComponent : ComponentBase, IDisposable
    {
        protected CancellationTokenSource? CTS;

        protected CancellationToken CancelToken => (CTS ??= new()).Token;

        protected void Cancel() => DisposeCTS();

        public virtual void Dispose() => DisposeCTS();

        public virtual void DisposeCTS()
        {
            if (CTS != null)
            {
                CTS.Cancel();
                CTS.Dispose();
                CTS = null;
            }
        }
    }

}
