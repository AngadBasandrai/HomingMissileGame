using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Button pvc;
    public Button pvce;
    public Button pvcn;
    public Button pvch;
    public GameObject pvcs;
    public Button pvp;

    // Start is called before the first frame update
    void Start()
    {
        pvc.onClick.AddListener(() => pvcs.SetActive(!pvcs.activeSelf));
        pvce.onClick.AddListener(() => SceneManager.LoadScene("PVC Easy"));
        pvcn.onClick.AddListener(() => SceneManager.LoadScene("PVC Normal"));
        pvch.onClick.AddListener(() => SceneManager.LoadScene("PVC Hard"));
        pvp.onClick.AddListener(() => SceneManager.LoadScene("PVP"));
    }
}
