namespace EndlessRunnerECS.Infrastructure.Pools
{
    public interface IObjectPool<T>
    {
        T Get();
        void Release(T obj);
    }
}
