using UnityEngine;

[CreateAssetMenu(menuName = "CharBasicSO", fileName ="CharBasicSO", order = 0)]


public class CharacterBasicSO : ScriptableObject
{
    [Header("캐릭터 공용 스텟")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpPower;
}

