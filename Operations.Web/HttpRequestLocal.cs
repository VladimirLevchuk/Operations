namespace Operations.Web
{
    public class HttpRequestLocal<T>
        where T: class
    {
        public HttpRequestLocal(string key)
        {
            Key = key;
        }

        protected string Key { get; }

        public virtual T Value
        {
            get => (T) HttpContextAccessor.Current.Items[Key];
            set => HttpContextAccessor.Current.Items[Key] = value;
        }

        public virtual bool HasContext => HttpContextAccessor.CurrentOrNull != null;

        public T GetValueOrDefault(T @default = default(T))
        {
            return (HasContext ? Value : null) ?? @default;
        }
    }
}