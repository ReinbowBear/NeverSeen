using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuKeyboard : MonoBehaviour
{
    public static MenuKeyboard instance;
    public GameInput menuInput;

    [SerializeField] private float navigateTime;
    [Space]
    [SerializeField] private Panel basePanel;
    [SerializeField] private Panel pausePanel;
    [HideInInspector] public List<Panel> panels = new List<Panel>();
    private Panel currentPanel;
    private Coroutine myCoroutine;

    void Awake()
    {
        instance = this;
        menuInput = new GameInput();

        CheckPanel();
    }


    private void NavigateInput(InputAction.CallbackContext context)
    {
        float input = menuInput.Menu.Navigate.ReadValue<float>();

        int direction = (input == 1) ? -1 : 1; // смотрим направление ввода
        int newButtonIndex = (currentPanel.chosenButton + direction + currentPanel.buttons.Count) % currentPanel.buttons.Count; // если вышли за край, возращаемся с другой стороны

        ChoseNewButton(newButtonIndex);
    }


    public void ChoseNewButton(int index)
    {
        FalseСurrentButton();

        currentPanel.chosenButton = index;

        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }

        myCoroutine = StartCoroutine(MoveToButton(currentPanel.buttons[index].transform));
        currentPanel.buttons[index].Triggered(0.9f);
    }

    private void FalseСurrentButton()
    {
        MyButton button = currentPanel.buttons[currentPanel.chosenButton];
        button.Triggered(0.5f);
    }


    private IEnumerator MoveToButton(Transform target)
    {
        GameObject moveTarget = currentPanel.navigateObject;
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


    private void InvokeButton(InputAction.CallbackContext context)
    {
        currentPanel.buttons[currentPanel.chosenButton].Invoke();
    }

    private void ExitPanel(InputAction.CallbackContext context)
    {
        if (panels.Count != 0)
        {
            panels[panels.Count-1].ClosePanel(); // вызывает так же CheckPanel
        }
        else if (pausePanel != null)
        {
            pausePanel.OpenPanel();
        }
    }


    public void CheckPanel()
    {
        if (panels.Count != 0)
        {
            currentPanel = panels[panels.Count-1];
        }
        else if (basePanel != null)
        {
            currentPanel = basePanel;
        }
        else
        {
            currentPanel = null;
        }
    }


    void OnEnable()
    {
        menuInput.Enable();

        menuInput.Menu.Submit.started += InvokeButton;
        menuInput.Menu.Navigate.started += NavigateInput;
        menuInput.Menu.Esc.started += ExitPanel;
    }

    void OnDisable()
    {
        menuInput.Menu.Submit.started -= InvokeButton;
        menuInput.Menu.Navigate.started -= NavigateInput;
        menuInput.Menu.Esc.started -= ExitPanel;

        menuInput.Disable();
    }
}
