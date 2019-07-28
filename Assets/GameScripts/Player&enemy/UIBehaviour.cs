using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{

    [System.Serializable]
    public class UIElement
    {
        public string tag;
        public float FullValue;
        public Image UIBar;

    }


    public List<UIElement> Elements;
    public GameObject PlayerUI;
    public Dictionary<string, UIElement> UIDictionary;

    private PhotonView PV;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            UIDictionary = new Dictionary<string, UIElement>();

            foreach (UIElement ele in Elements)
            {

                UIDictionary.Add(ele.tag, ele);

            }
        }
        else
            Destroy(PlayerUI);
    }

    public void OnChange(float CurrentValue, string tag)
    {
        if (PV.IsMine)
        {
            if (!UIDictionary.ContainsKey(tag))
            {
                Debug.Log("The Tag " + tag + " Does Not Exist!");
                return;
            }
            UIElement Bar = UIDictionary[tag];
            Bar.UIBar.fillAmount = ((float)CurrentValue / Bar.FullValue);
        }
    }
}
