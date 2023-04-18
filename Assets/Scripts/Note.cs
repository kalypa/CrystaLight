using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Note : MonoBehaviour
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
    public GameObject tailPrefab;
    public Material[] lightmaterial;
    int longNoteSpawnCount = 0;
    GameObject tail;
    public bool isHolding = false;
    private float lanePos = -3f;
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));
        float t2 = (float)(timeSinceInstantiated / (SongManager.Instance.longNoteTime * 2));
        if (type == NoteType.Long)
        {
            NoteDown(t, t2);
        }
        else
        {
            NoteDown(t, t);
        }
    }
    void SetLineRendererPos(Vector3 startPos, Vector3 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    void NoteDown(float t, float t2)
    {
        if (t2 > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.forward * SongManager.Instance.noteSpawnY, Vector3.forward * SongManager.Instance.noteDespawnY, t);
            switch (type)
            {
                case NoteType.Normal:
                    spriteRenderer.enabled = true;
                    break;
                case NoteType.Refraction:
                    spriteRenderer.enabled = true;
                    spriteRenderer.color = Color.green;
                    break;
                case NoteType.Penetration:
                    spriteRenderer.enabled = true;
                    spriteRenderer.color = Color.red;
                    break;
                case NoteType.Invisible:
                    spriteRenderer.enabled = true;
                    spriteRenderer.color = Color.blue;
                    break;
                case NoteType.Long:
                    spriteRenderer.enabled = true;
                    lineRenderer.enabled = true;

                    if (SongManager.GetAudioSourceTime() >= endTime - SongManager.Instance.noteTime)
                    {
                        if (longNoteSpawnCount == 0)
                        {
                            tail = Instantiate(tailPrefab, new Vector3(this.transform.position.x, this.transform.position.y, SongManager.Instance.noteSpawnY), Quaternion.identity, transform);
                            longNoteSpawnCount++;
                        }
                        if (!isHolding)
                        {
                            SetLineRendererPos(transform.position, new Vector3(transform.position.x, transform.position.y, tail.transform.position.z));
                        }
                        else
                        {
                            SetLineRendererPos(new Vector3(transform.position.x, transform.position.y, lanePos), new Vector3(transform.position.x, transform.position.y, tail.transform.position.z));
                        }
                    }
                    else
                    {
                        SetLineRendererPos(transform.position, new Vector3(transform.position.x, transform.position.y, SongManager.Instance.noteSpawnY));
                    }
                    break;
            }
        }
    }
}