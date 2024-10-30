using UnityEngine;

[CreateAssetMenu(menuName = "CharBasicSO", fileName ="CharBasicSO", order = 0)]


public class CharacterBasicSO : ScriptableObject
{
    [Header("ĳ���� ���� ����")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpPower;
}

