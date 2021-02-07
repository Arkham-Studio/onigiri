namespace Arkham.Onigiri.Utils
{
    public interface IEventListener
    {
        void OnEventRaised();
    }
    public interface IEventListener<T>
    {
        void OnEventRaised(T o);
    }
}