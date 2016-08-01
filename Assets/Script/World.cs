using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    GameObject m_Character;
    public Terrain m_terrain;
    private List<GameObject> m_monsters = new List<GameObject>();

    public UIGrid selectMonsterGrid;
    private Object selectMonsterItem;

    public string AppContentPath()
    {
        string path = string.Empty;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                path = "jar:file://" + Application.dataPath + "!/assets/";
                break;
            case RuntimePlatform.IPhonePlayer:
                path = Application.dataPath + "/Raw/";
                break;
            default:
                path = Application.dataPath + "/" + "StreamingAssets" + "/";
                break;
        }
        return path;
    }

    IEnumerator LoadAssets()
    {
        // Start a download of the given URL
        string PathURL = AppContentPath();

        WWW www = WWW.LoadFromCacheOrDownload(PathURL + "/res.unity3d", 0);

        // Wait for download to complete
        yield return www;

        // Load and retrieve the AssetBundle
        AssetBundle bundle = www.assetBundle;

        // Load the object asynchronously
        AssetBundleRequest request = bundle.LoadAssetAsync("Character", typeof(GameObject));

        // Wait for completion
        yield return request;

        // Get the reference to the loaded object
        CharacterLoaded(request.asset as GameObject);
        // Load the object asynchronously
        AssetBundleRequest request2 = bundle.LoadAssetAsync("Monster", typeof(GameObject));

        // Wait for completion
        yield return request2;

        // Get the reference to the loaded object
        MonsterLoaded(request2.asset as GameObject);


        // Unload the AssetBundles compressed contents to conserve memory
        bundle.Unload(false);
    }
	// Use this for initialization
	void Start () {
        StartCoroutine("LoadAssets");
        selectMonsterItem = Resources.Load("item");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CharacterLoaded(GameObject character)
    {
        m_Character = Instantiate(character, new Vector3(0, m_terrain.SampleHeight(Vector3.zero), 0), Quaternion.identity) as GameObject;
    }

    void MonsterLoaded(GameObject monster)
    {
        for (int i = 0; i < 30; i++)
        {
            int x = Random.Range(-30, 30);
            int z = Random.Range(-30, 30);
            GameObject imonster = Instantiate(monster, new Vector3(x, m_terrain.SampleHeight(new Vector3(x,0,z))+0.1f, z), Quaternion.identity) as GameObject;
            imonster.name = "Monster" + i;
            imonster.GetComponent<AI>().player = m_Character;
            imonster.GetComponent<AI>().index = i;
            imonster.transform.FindChild("PanelHead/Label_name").GetComponent<UILabel>().text = "Monster" + i;
            m_monsters.Add(imonster);
            GameObject o = Instantiate(selectMonsterItem, Vector3.zero, Quaternion.identity) as GameObject;
            o.name = "" + i;
            o.transform.FindChild("name").GetComponent<UILabel>().text = "Monster" + i;
            o.GetComponent<UI_SelectItem>().player = m_Character.transform;

            selectMonsterGrid.AddChild(o.transform);

            o.transform.localPosition = Vector3.zero;
            o.transform.localScale = Vector3.one;
        }
        //列表添加后用于刷新listView
        selectMonsterGrid.repositionNow = true;
    }
    
}
