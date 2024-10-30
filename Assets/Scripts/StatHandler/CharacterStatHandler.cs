using System.Collections;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterBasicSO CharbasicSO;

    public float OriginMoveSpeed { get; private set; }
    public float CurMoveSpeed { get; private set; }

    public float OriginJumpPower { get; private set; }
    public float CurJumpPower { get; private set; }

    void Awake()
    {
        Init();
    }

    void Init()
    {
        OriginMoveSpeed = CharbasicSO.moveSpeed;
        CurMoveSpeed = OriginMoveSpeed;
        OriginJumpPower = CharbasicSO.jumpPower;
        CurJumpPower = OriginJumpPower;
    }

    //스텟변화가 있을경우
    //다른 분류의 스텟이 동시에 변화되거나 동일 분류의 스텟이 2중으로 변화될때 대응은?
    //효과 지속중 재사용시 지속시간이 되돌아가야함
    /*
    void StatChange(float amount, float duration)
    {
        CurMoveSpeed += amount;
        Invoke("GoBackOrigin", duration);
    }
    void GoBackOrigin()
    {
        CurMoveSpeed = OriginMoveSpeed;
    }
    */
}

