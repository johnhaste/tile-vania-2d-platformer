using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    int currentSceneIndex;
    [SerializeField] Sprite openedDor;
    SpriteRenderer mySpriteRenderer;

    void Start(){
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col){
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(col.tag == "Player"){
            mySpriteRenderer.sprite = openedDor;
            StartCoroutine(LoadNextLevel());
       }
    }

    IEnumerator LoadNextLevel(){
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
