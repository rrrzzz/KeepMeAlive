using System.Collections;
using TMPro;
using UnityEngine;

public class InputRegister : MonoBehaviour
{
    public GameObject intro;
    public float introLength;
    public float intervalInSecs;
    
    private Animator _anim;
    private float _startTime;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("dead"))
        {
            var tmpText = intro.GetComponent<TMP_Text>();
            tmpText.text = tmpText.text.Replace("User", System.Environment.UserName);
            _anim = intro.GetComponent<Animator>();
            PlayerPrefs.SetString("dead", "true");
            PlayerPrefs.Save();
            intro.SetActive(true);
            _startTime = Time.realtimeSinceStartup;
            StartCoroutine(DisableText());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            _startTime = Time.realtimeSinceStartup;
        }

        var currentInterval = Time.realtimeSinceStartup - _startTime;
        if (currentInterval > intervalInSecs)
        {
            Application.Quit();
        }
        
        _anim.speed = Mathf.Lerp(1, 0, currentInterval / intervalInSecs);
    }

    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(introLength);
        var text = intro.GetComponent<TMP_Text>();
        text.text = ".";
        text.fontSize = 50;
        text.alignment = TextAlignmentOptions.Center;
        _anim.enabled = true;
    }
}