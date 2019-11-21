using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CharacterStats : MonoBehaviour
{

    public Stat Maxhp;
    public Stat sp;
    public Stat attack;
    public Stat armor;
    //공격 리치
    public Stat range;
    public Stat attackSpeed;

    public Image HealthUI;

    float currentHP;

    private PhotonView photonView;

    public System.Action onPlayerDie;

    private void OnEnable()
    {
        if(HealthUI != null)
            HealthUI.fillAmount = 1;
        currentHP = Maxhp.GetValue();
    }

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void TakeDamageRPC(float _damage)
    {
        photonView.RPC("TakeDamage", RpcTarget.AllBuffered, _damage);
    }

    [PunRPC]
    public void TakeDamage(float _damage)
    {
        //맞는 애니메이션 실행
        _damage -= armor.GetValue();
        _damage = Mathf.Clamp(_damage, 0, _damage);
        currentHP -= _damage;
        HealthUI.fillAmount = currentHP / Maxhp.GetValue();
        Debug.Log(gameObject.name + "이" + _damage +"의 데미지를 받았습니다.");
        if(currentHP <= 0)
        {
            Die();
        }

        

    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + "이 죽었습니다!");
        photonView.TransferOwnership(PhotonNetwork.MasterClient);
        if (photonView.Owner == PhotonNetwork.MasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
            Debug.Log("마스터클라이언트만 파괴할 수 있습니다!");
        onPlayerDie?.Invoke();
    }
}
