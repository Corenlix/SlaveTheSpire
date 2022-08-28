using UnityEngine;

namespace Infrastructure.Assets
{
    public interface IAssetProvider
    {
        T Instantiate<T>(string path, Vector3 position) where T : MonoBehaviour;
        GameObject Instantiate(string path, Vector3 position);
        T Instantiate<T>(string path) where T : MonoBehaviour;
        GameObject Instantiate(string path);
        T Load<T>(string path) where T : Object;
    }
}