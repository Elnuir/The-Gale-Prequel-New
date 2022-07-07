using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class HexagonTransition : MonoBehaviour
{
    public string NextSceneName;

    public Camera FG_Camera;
    public Camera BG_Camera;
    public LayerMask FrontgroundCullingMask;
    public LayerMask BackgroundCullingMask;

    [Header("Materials (FG should be transparent)")]
    public Material FG1_Material;
    public Material BG1_Material;

    public Material FG2_Material;
    public Material BG2_Material;

    [Space]
    public UnityEvent GoodToGo;


    private void Awake()
    {

    }

    public void StartTransition()
    {
        DontDestroyOnLoad(this.gameObject);
        FG1_Material.mainTexture = new RenderTexture(FG_Camera.pixelWidth, FG_Camera.pixelHeight, 32);
        BG1_Material.mainTexture = new RenderTexture(FG_Camera.pixelWidth, FG_Camera.pixelHeight, 24);
        FG2_Material.mainTexture = new RenderTexture(FG_Camera.pixelWidth, FG_Camera.pixelHeight, 32);
        BG2_Material.mainTexture = new RenderTexture(FG_Camera.pixelWidth, FG_Camera.pixelHeight, 24);

        SetupCameras();
        StartCoroutine(LoadAsyncScene());
    }

    private void SetupCameras()
    {
        FG_Camera.CopyFrom(Camera.main);
        FG_Camera.cullingMask = FrontgroundCullingMask;
        FG_Camera.backgroundColor = new Color32(0, 0, 0, 0);

        BG_Camera.CopyFrom(Camera.main);
        BG_Camera.cullingMask = BackgroundCullingMask;
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextSceneName);
        asyncLoad.allowSceneActivation = false;
        yield return asyncLoad.isDone;
        StartCoroutine(SceneLoaded(asyncLoad));
    }

    IEnumerator SceneLoaded(AsyncOperation sync)
    {
        yield return WriteBgAndFg(FG1_Material, BG1_Material);

        sync.allowSceneActivation = true;
        yield return SceneManager.GetActiveScene().name == NextSceneName && SceneManager.GetActiveScene().isLoaded;
        yield return new WaitForSecondsRealtime(0.4f);
        yield return new WaitForEndOfFrame();

        SetupCameras();

        yield return WriteBgAndFg(FG2_Material, BG2_Material);
        GoodToGo.Invoke();
    }

    IEnumerator WriteBgAndFg(Material fgTarget, Material bgTarget)
    {
        yield return new WaitForEndOfFrame();
        WriteScreenshotTo(FG_Camera, fgTarget);
        WriteScreenshotTo(BG_Camera, bgTarget);
    }


    public void WriteScreenshotTo(Camera cam, Material target)
    {
        cam.targetTexture = (RenderTexture)target.mainTexture;
        cam.Render();
    }
}