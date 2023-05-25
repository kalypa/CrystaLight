using Redcode.Pools;
using UnityEngine;

public class Note : MonoBehaviour, IPoolObject
{
    public enum NoteType
    { 
        Normal,
        Refraction,
        Penetration,
        Long,
        Invisible,
    }
    public NoteType type;
    double timeInstantiated;
    public double endTime;
    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;
    public GameObject tail;
    public Material[] lightmaterial;
    int longNoteSpawnCount = 0;
    public bool isHolding = false;
    public bool isMissed = true;
    private readonly float lanePos = -3f;
    public GameObject[] noteLinks1;
    public GameObject[] noteLinks2;
    public GameObject[] noteLinks3;
    public GameObject[] noteLinks4;
    void Start() => Init();

    void Update() => MoveNote();

    void MoveNote()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));
        float t2 = (float)(timeSinceInstantiated / (SongManager.Instance.longNoteTime * 2));
        if (type == NoteType.Long) NoteDown(t, t2);
        else NoteDown(t, t);
    }

    void SetLineRendererPos(Vector3 startPos, Vector3 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
    private void MoveTail(float progress)
    {
        Vector3 tailPosition = Vector3.Lerp(Vector3.forward * SongManager.Instance.noteDespawnZ, Vector3.forward * (SongManager.Instance.noteDespawnZ - SongManager.Instance.noteSpawnZ), progress);
        tail.transform.localPosition = tailPosition;
    }
    void NoteDown(float t, float t2)
    {
        if (t2 > 1)
        {
            var lane = this.GetComponentInParent<Lane>();
            PoolManager.Instance.TakeToPool(lane.laneNum, this);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.forward * SongManager.Instance.noteSpawnZ, Vector3.forward * SongManager.Instance.noteDespawnZ, t);
            spriteRenderer.material = UpdateSprite(t, lightmaterial);
            spriteRenderer.enabled = true;
        }
    }
    Material UpdateSprite(float t, Material[] mat) => type switch
    {
        NoteType.Normal => mat[0],
        NoteType.Refraction => mat[1],
        NoteType.Penetration => mat[2],
        NoteType.Invisible => mat[3],
        NoteType.Long => LongTypeHandling(t, mat),
        _ => mat[4]
    };
    public int ParticleIndex() => type switch
    {
        NoteType.Normal => 1,
        NoteType.Refraction => 2,
        NoteType.Penetration => 3,
        NoteType.Invisible => 0,
        NoteType.Long => 0,
        _ => throw new System.NotImplementedException()
    };
    Material LongTypeHandling(float t, Material[]  mat)
    {
        lineRenderer.enabled = true;
        SpawnLongNote(t);
        return mat[4];
    }
    void SpawnLongNote(float t)
    {
        tail.SetActive(true);
        if (SongManager.GetAudioSourceTime() >= endTime - SongManager.Instance.noteTime)
        {
            if (longNoteSpawnCount == 0)
            {
                tail.SetActive(true);
                tail.transform.position = new Vector3(transform.position.x, transform.position.y, SongManager.Instance.noteSpawnZ);
                longNoteSpawnCount++;
            }
            if (!isHolding) SetLineRendererPos(transform.position, new Vector3(transform.position.x, transform.position.y, tail.transform.position.z));
            else SetLineRendererPos(new Vector3(transform.position.x, transform.position.y, lanePos), new Vector3(transform.position.x, transform.position.y, tail.transform.position.z));
        }
        else SetLineRendererPos(transform.position, new Vector3(transform.position.x, transform.position.y, SongManager.Instance.noteSpawnZ));
        if (transform.localPosition.z <= SongManager.Instance.noteDespawnZ && isHolding)
        {
            float tailProgress = Mathf.Clamp01(t - (SongManager.Instance.noteDespawnZ - SongManager.Instance.noteSpawnZ) / (2 * (SongManager.Instance.noteDespawnZ - SongManager.Instance.noteSpawnZ)));
            MoveTail(tailProgress);
        }
    }

    void Init()
    {
        NoteLinkInit();
        timeInstantiated = SongManager.GetAudioSourceTime();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        spriteRenderer.color = Color.white;
        this.transform.position = Vector3.zero;
        spriteRenderer.enabled = false;
        tail.SetActive(false);
        isHolding = false;
        isMissed = true;
    }
    public void NoteLinkInit()
    {
        for(int i = 0; i < noteLinks1.Length; i++) if (noteLinks1[i] != null) noteLinks1[i].SetActive(false);
        for(int i = 0; i < noteLinks2.Length; i++) if (noteLinks2[i] != null) noteLinks2[i].SetActive(false);
        for(int i = 0; i < noteLinks3.Length; i++) if (noteLinks3[i] != null) noteLinks3[i].SetActive(false);
        for(int i = 0; i < noteLinks4.Length; i++) if (noteLinks4[i] != null) noteLinks4[i].SetActive(false);
    }
    public void OnCreatedInPool() { }

    public void OnGettingFromPool() => Init();
}