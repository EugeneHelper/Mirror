using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    #region Veriables
    [SerializeField]
    private Renderer displayColorRenderer = null;
    [SerializeField]
    private TMP_Text displayNameText=null;

    [SyncVar(hook = nameof(ChangeNameOfPlayer))]
    [SerializeField]
    private string displayName = "Missing Name";

    [SyncVar(hook = nameof(ChangeColorOfPlayer))]
    [SerializeField]
    private Color color = Color.blue;
    #endregion

    #region Server
    [Server]
    public void SetDisplayName(string NewDisplayName)
    {
        displayName = NewDisplayName; 
    }
    
    [Server]
    public void SetColorPlayer(Color newCol)
    {
        color = newCol;
        displayColorRenderer.material.color = newCol;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        RpcLogNewName(newDisplayName);

        SetDisplayName(newDisplayName);
    }


    #endregion

    #region Client        //  ф-ии мен€ют значени€ на клиенте, когда они мен€ютс€ на сервере(ффункции синхронизации)
    public void ChangeColorOfPlayer(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.color = newColor;
    }
    
    public void ChangeNameOfPlayer(string oldTxt, string newTxt)
    {
        displayNameText.text = newTxt; 
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }


    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }
    #endregion

}