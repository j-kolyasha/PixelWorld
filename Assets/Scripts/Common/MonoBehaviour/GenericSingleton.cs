
namespace Common.MonoBehaviour
{
    public abstract class GenericSingleton<T> : CashedMonoBehaviour where T : GenericSingleton<T>
    {
        private static T _instance;

        public static T Instance => _instance;

        protected override void InheritAwake()
        {
            if (_instance == null)
                _instance = this as T;
            else
                Destroy(this);

            DontDestroyOnLoad(this);
        }
    }
}