using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class Packager{

    static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();

    [MenuItem("TestGame/Build iPhone Resource", false, 100)]
    public static void BuildiPhoneResource()
    {
        BuildTarget target;
#if UNITY_5
        target = BuildTarget.iOS;
#else
        target = BuildTarget.iPhone;
#endif
        BuildAssetResource(target);
    }

    [MenuItem("TestGame/Build Android Resource", false, 101)]
    public static void BuildAndroidResource()
    {
        BuildAssetResource(BuildTarget.Android);
    }

    [MenuItem("TestGame/Build Windows Resource", false, 102)]
    public static void BuildWindowsResource()
    {
        BuildAssetResource(BuildTarget.StandaloneWindows);
    }

    /// <summary>
    /// 生成绑定素材
    /// </summary>
    public static void BuildAssetResource(BuildTarget target)
    {
        
        string streamPath = Application.streamingAssetsPath;
        if (Directory.Exists(streamPath))
        {
            Directory.Delete(streamPath, true);
        }
        Directory.CreateDirectory(streamPath);
        AssetDatabase.Refresh();

        maps.Clear();
        //资源打包

        string[] files = {
                            "Assets/Res/Blade_girl/Character.prefab",
                            "Assets/Res/Skeleton/Monster.prefab"
                         };

        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = "res.unity3d";
        build.assetNames = files;
        maps.Add(build);

        //string resPath = "Assets/" + "StreamingAssets";
        BuildAssetBundleOptions options =
                                          BuildAssetBundleOptions.DeterministicAssetBundle |
                                          BuildAssetBundleOptions.UncompressedAssetBundle;
        BuildPipeline.BuildAssetBundles(streamPath, maps.ToArray(), options, target);
        AssetDatabase.Refresh();
    }
}
