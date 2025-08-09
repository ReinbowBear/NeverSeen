using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private GameObject navigateObject;
    [SerializeField] private float navigateTime;
    [Space]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [Space]
    public List<MyButton> buttons;
    
    [HideInInspector] public int currentButton;
    private Coroutine myCoroutine;

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        Debug.Log("у нас тут скрипт звуков реворкнут был (почти) надо разобратся");
        //Sound.Instance.PlaySFX(openSound);
        MenuManager.instance.panels.Add(this);
        MenuManager.instance.CheckPanel();
        MenuManager.instance.ChoseNewButton(0);
    }

    public void ClosePanel()
    {
        //Sound.instance.Play(closeSound);
        MenuManager.instance.panels.Remove(this);
        MenuManager.instance.CheckPanel();

        gameObject.SetActive(false);
    }


    public void ChoseNewButton(int newButton)
    {
        if (newButton == currentButton)
        {
            return;
        }

        buttons[newButton].Trigger(0.9f, true);
        buttons[currentButton].Trigger(0.5f, false);
        currentButton = newButton;

        if (myCoroutine != null) StopCoroutine(myCoroutine);

        StartCoroutine(MoveToButton(buttons[newButton].transform));
    }

    private IEnumerator MoveToButton(Transform target)
    {
        GameObject moveTarget = navigateObject;
        Vector3 startPos = moveTarget.transform.position;
        Vector3 endPos = new Vector3 (0, target.position.y, 0);

        float timeElapsed = 0f;
        while (timeElapsed < navigateTime)
        {
            moveTarget.transform.position = Vector3.Lerp(startPos, endPos, timeElapsed / navigateTime);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        moveTarget.transform.position = endPos;
        myCoroutine = null;
    }
}
