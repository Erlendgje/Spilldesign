using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTree : MonoBehaviour {

    public string thisScene;
    public List<string> adjecentScenes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(thisScene));

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (!adjecentScenes.Contains(SceneManager.GetSceneAt(i).name) && SceneManager.GetSceneAt(i).name != thisScene)
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                }
            }
            foreach (string s in adjecentScenes)
            {
                if (!SceneManager.GetSceneByName(s).isLoaded)
                {
                    SceneManager.LoadScene(s, LoadSceneMode.Additive);
                }
            }
        }
    }
}
