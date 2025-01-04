using UnityEngine;

namespace Infrastructure {

    public class SingletonCrossScene<T> : MonoBehaviour where T : MonoBehaviour {

        private static T _instance;
        public static T Instance => _instance;

        protected virtual void Awake() {
            var component = GetComponent<T>();
            if (_instance == null) {
                _instance = GetComponent<T>();
                DontDestroyOnLoad(gameObject);
                Init();
                return;
            }
            if (_instance != component) {
                Destroy(gameObject);
            }
        }

        protected virtual void Init() { }
    }
}
