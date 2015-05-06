using UnityEngine;
using System.Collections;

public enum NoteType {
    EIGHTH,
    QUARTER,
    HALF,
    WHOLE,
    DOTTED_EIGHTH,
    DOTTED_QUARTER,
    DOTTED_HALF,
    DOTTED_WHOLE
};

public class Note : MonoBehaviour {
    public NoteType noteType = NoteType.QUARTER;
    public float displacement = 1.0f; // how far apart the next note is
    public bool isRest = false;

    // How long user needs to hold the note. Normalized so whole note = 1.
    // Duration of 0 indicates single press (quarter, eighth notes, etc.)
    public float duration {
        get {
            float value = 0.0f;
            switch (noteType) {
                case NoteType.HALF:
                    value = 0.5f;
                    break;
                case NoteType.WHOLE:
                    value = 1.0f;
                    break;
                case NoteType.DOTTED_HALF:
                    value = 0.75f;
                    break;
                case NoteType.DOTTED_WHOLE:
                    value = 1.5f;
                    break;
            }
            return value;
        }
    }

    // How long note actually lasts. Normalized so whole note = 1.
    public float beatValue {
        get {
            switch (noteType) {
                case NoteType.EIGHTH:
                    return 0.125f;
                case NoteType.QUARTER:
                    return 0.25f;
                case NoteType.HALF:
                    return 0.5f;
                case NoteType.WHOLE:
                    return 1.0f;
                case NoteType.DOTTED_EIGHTH:
                    return 0.1875f;
                case NoteType.DOTTED_QUARTER:
                    return 0.375f;
                case NoteType.DOTTED_HALF:
                    return 0.75f;
                case NoteType.DOTTED_WHOLE:
                    return 1.5f;
            }
            return 0.0f;
        }
    }

    public bool played = false; // when note has been played
    int row = 0; // which row on the staff the note lines up with
    bool falling = false;

    // Animation data
    Animator anim;
    int hitHash = Animator.StringToHash("hit");
    int releaseHash = Animator.StringToHash("release");
    int dieHash = Animator.StringToHash("die");


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (falling && gameObject.activeInHierarchy) {
            transform.Rotate(8f * Vector3.back * Time.deltaTime);
            if (!GetComponent<SpriteRenderer>().isVisible) {
                gameObject.SetActive(false);
            }
        }
    }

    // TODO: Show a flashy animation on the beat
    // For now, we just bounce it up a few pixels
    public void AnimateBeat(bool on) {
        Vector3 pos = transform.position;
        pos.y += on ? 0.1f : -0.1f;
        transform.position = pos;

        if (isRest) {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    // Show a flashy animation when the note is hit
    public void AnimateHit(Score score) {
        if (isRest) {
            if (score.value > 0) {
                // TODO: what to do on rest?
            } else {
                // didn't hit rest, good
            }
        } else {
            // not a rest
            if (score.value > 0) {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                anim.SetTrigger(hitHash);
                anim.speed = Song.currentSong.beatTime;
                score.ShowText(transform.position, Color.black);
            } else {
                // TODO: replace with falling animation
                anim.SetTrigger(dieHash);
                anim.speed = Song.currentSong.beatTime;
                score.ShowText(transform.position, Color.red);
                gameObject.AddComponent<Rigidbody2D>(); // add gravity
                falling = true;
            }
        }
        played = true;
    }

    // Show a flashy animation when the note is released
    public void AnimateRelease(Score score) {
        Debug.Log("working");
        if (score.value > 0) {
            anim.SetTrigger(releaseHash);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
            score.ShowText(transform.position, Color.blue);
        } else {
            // TODO: show animation for miss
            anim.SetTrigger(dieHash);
            score.ShowText(transform.position, Color.red);
        }
    }
}
