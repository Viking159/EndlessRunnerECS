namespace EndlessRunnerECS.Infrastructure.Pools
{
    public interface IPool<T>
    {
        T Get();
        void Release(T obj);
    }
}
