using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class RopePhysics : MonoBehaviour
{
    [SerializeField]
    [Range(1, 1000)]
    public int ropeLength = 1;
    [SerializeField]
    public GameObject ropeNode;
   
}


#if UNITY_EDITOR
[CustomEditor(typeof(RopePhysics))]
[System.Serializable]
class RopeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RopePhysics script = (RopePhysics)target;
        Vector3 dist = new Vector3(0, -0.23f, 0);

        int nodeCount = (int)(-1 * script.ropeLength / dist.y);

        if (GUILayout.Button("ADD"))
        {
            Reset(ref script);
            for (int i = 0; i < nodeCount; i++)
            {
                GameObject newRope = Instantiate(script.ropeNode);
                newRope.transform.parent = script.transform;
                newRope.name = script.transform.childCount.ToString();
                newRope.transform.position = script.transform.position + dist * (i+1);
                if (i == 0)
                {
                    DestroyImmediate(newRope.GetComponent<CharacterJoint>());
                    newRope.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    Debug.Log("girdi");
                }
                else
                    newRope.GetComponent<CharacterJoint>().connectedBody = script.transform.Find((script.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
                Debug.Log("name: " + newRope.name + " parent: " + newRope.transform.parent.name + " obj: " + script.gameObject.name);
            }
            
        }
        if (GUILayout.Button("RESET"))
        {
            Reset(ref script);
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("ropeLength"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ropeNode"));

        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
    void Reset(ref RopePhysics script)
    {
        Debug.Log("count" + script.transform.childCount);
        int tmp = script.transform.childCount;

        for (int i = 0; i < tmp; i++)
        {

            Debug.Log("name: " + script.transform.GetChild(0).gameObject.name);
            DestroyImmediate(script.transform.GetChild(0).gameObject);
            Debug.Log("index: " + i);
        }
    }
}
#endif

