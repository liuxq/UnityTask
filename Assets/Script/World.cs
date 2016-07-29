using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    GameObject m_Character;
    public Terrain m_terrain;

    Random m_rand = new Random();

    IEnumerator LoadAssets()
    {
        // Start a download of the given URL
	    string PathURL =
#if UNITY_ANDROID
		"jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
		Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
	"file://" + Application.dataPath + "/StreamingAssets/";
#else
        string.Empty;
#endif
        WWW www = WWW.LoadFromCacheOrDownload(PathURL + "/res.unity3d", 5);

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
   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CharacterLoaded(GameObject character)
    {
        //m_Character = Instantiate(character, new Vector3(0, m_terrain.SampleHeight(Vector3.zero), 0), Quaternion.identity) as GameObject;
    }

    void MonsterLoaded(GameObject monster)
    {

        //GameObject imonster = Instantiate(monster, new Vector3(0, m_terrain.SampleHeight(Vector3.zero), 4), Quaternion.identity) as GameObject;
        //imonster.GetComponent<AI>().player = m_Character;
    }
}
