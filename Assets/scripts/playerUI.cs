using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform healthBarFill;

    [SerializeField]
    GameObject PauseMenu;

    [SerializeField]
    GameObject scoreboard;

    private player _player;

    public void setPlayer(player playerLocal)
    {
        _player = playerLocal;
    }

    // Start is called before the first frame update
    void Start()
    {
        pausemenu.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        setHealthAmount(_player.getHealthPct());
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
    }

    void setHealthAmount(float amount)
    {
        healthBarFill.localScale = new Vector3(1f, amount, 1f);
    }

    void togglePauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        pausemenu.isOn = PauseMenu.activeSelf;
    }
}
