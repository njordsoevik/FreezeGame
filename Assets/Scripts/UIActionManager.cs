using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIActionManager : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    public void ResetScene() {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Reset");
    }
}
