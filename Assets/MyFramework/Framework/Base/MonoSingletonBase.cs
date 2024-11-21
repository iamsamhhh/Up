
namespace MyFramework
{
    public class MonoSingletonBase<T> : MonoBehaviourSimplify where T : MonoBehaviourSimplify
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            _instance = this as T;
        }

    }
}
