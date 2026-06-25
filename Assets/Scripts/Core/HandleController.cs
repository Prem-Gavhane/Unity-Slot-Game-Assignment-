

using UnityEngine;

public class HandleController : MonoBehaviour
{    public GameObject Handel_Up;
public GameObject Handel_Down;


    void Awake()
    {
         Handel_Up.gameObject.SetActive(true);
        Handel_Down.gameObject.SetActive(false);
    }
 public void PlayHandle()
    {
        Handel_Up.gameObject.SetActive(false);
        Handel_Down.gameObject.SetActive(true);
    }

 public void StopHandle()
    {
         Handel_Up.gameObject.SetActive(true);
        Handel_Down.gameObject.SetActive(false);
    }
}