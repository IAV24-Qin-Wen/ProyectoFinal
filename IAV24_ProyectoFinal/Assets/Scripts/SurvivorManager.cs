using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurvivorManager : MonoBehaviour
{
    [SerializeField]
    private int nSurvivors;
    [SerializeField]
    private GameObject survivorPrefab;
    [SerializeField]
    private GameObject survivorsParent;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject survivorIDTextPrefab; 
    [SerializeField]
    private GameObject survivorStateTextPrefab;
    [SerializeField]
    private Vector2 IDPos;
    [SerializeField]
    private Vector2 statePos;

    public Dictionary<GameObject, TextMeshProUGUI> survivors;

    [SerializeField]
    private GameObject levelGo;
    private GameEnding level;

    int minSurvive;
    int survived;

    // Start is called before the first frame update
    void Start()
    {
        level=levelGo.GetComponent<GameEnding>();
        survivors =new Dictionary<GameObject, TextMeshProUGUI> ();
        if (getNSurvivors() > 2) minSurvive = 2;
        else minSurvive = 1;

        GameObject spawnPoints = GameObject.FindGameObjectWithTag("SpawnPoint");
        bool[] used = new bool[spawnPoints.transform.childCount];

        if (spawnPoints.transform.childCount < nSurvivors) return;

        for (int i = 0; i < nSurvivors; i++)
        {
            bool spawned = false;
            while (!spawned)
            {
                //aux = UnityEngine.Random.Range(0, spawnPoints.transform.childCount);

                //if (!used[aux])
                //{
                //    used[aux] = spawned = true;
                //    Instantiate(survivorPrefab, spawnPoints.transform.GetChild(aux).transform.position, 
                //        spawnPoints.transform.GetChild(aux).transform.rotation, survivors.transform);
                //}

                if (!used[i])
                {
                    used[i] = spawned = true;
                    GameObject s=Instantiate(survivorPrefab, spawnPoints.transform.GetChild(i).transform.position,
                        spawnPoints.transform.GetChild(i).transform.rotation, survivorsParent.transform);
                    GameObject ID = s.transform.GetChild(0).gameObject;
                    TMP_Text t= ID.GetComponent<TMP_Text>();
                    
                    t.text = (i+1).ToString();

                    GameObject idText = Instantiate(survivorIDTextPrefab, new Vector3(0, 0, 0),
                    survivorIDTextPrefab.transform.rotation, canvas.transform);
                    RectTransform m_RectTransform = idText.GetComponent<RectTransform>();
                    m_RectTransform.anchoredPosition = IDPos + new Vector2(0, -i * 35.0f);
                    idText.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();

                    GameObject stateText=Instantiate(survivorStateTextPrefab, new Vector3(0, 0, 0),
                    survivorStateTextPrefab.transform.rotation, canvas.transform);
                    m_RectTransform = stateText.GetComponent<RectTransform>();
                    m_RectTransform.anchoredPosition = statePos + new Vector2(0, -i * 35.0f);

                    SharedGameObject aux=new SharedGameObject();
                    aux.Value=stateText;
                    s.GetComponent<BehaviorTree>().SetVariable("stateText", aux);
                    
              
                    survivors.Add(s,stateText.GetComponent<TextMeshProUGUI>());
                }
            }

        }
    }
    public void OnSurvivorArrive(GameObject s)
    {
        if (s == null) return;
        TextMeshProUGUI t;
        survivors.TryGetValue(s, out t);

        t.text = "Arrived";
        t.color = Color.green;
        survivors.Remove(s);
        Destroy(s);
        ++survived;
    }
    public void OnSurvivorDie(GameObject s)
    {
        if (s == null) return;
        TextMeshProUGUI t;
        survivors.TryGetValue(s, out t);
        t.text = "Dead";
        t.color = Color.red;  
        survivors.Remove(s);
        Destroy(s);

        if (survivors.Count == 0 && survived<minSurvive) level.OnKillerWin();
    }
    public int getNSurvivors() { return nSurvivors; }
}
