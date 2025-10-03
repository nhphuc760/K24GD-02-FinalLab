using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneUI : MonoBehaviour
{
    [SerializeField] Button playBTN;
    private void Start()
    {
        playBTN.onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });
    }

}
