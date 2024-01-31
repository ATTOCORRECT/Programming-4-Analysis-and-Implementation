using NodeCanvas.Tasks.Conditions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionManager : Singleton<VisionManager>
{
    // Start is called before the first frame update
    public List<GameObject> AgentsBeingSeen = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AgentsBeingSeen.Clear();
    }

    public bool getAgentSeesMe(GameObject me)
    {
        return AgentsBeingSeen.Contains(me);
    }
}
