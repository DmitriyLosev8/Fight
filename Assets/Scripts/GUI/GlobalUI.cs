using System;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUI : MonoBehaviour
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _cancel;
    [SerializeField] private Button _changeWeapon;
    [SerializeField] private Button _takeFullHP;
    [SerializeField] private CharacterStateSlider _playerPrepareSlider;
    [SerializeField] private CharacterStateSlider _playerAttackSlider;
    [SerializeField] private CharacterStateSlider _enemyPrepareSlider;
    [SerializeField] private CharacterStateSlider _enemyAttackSlider;
    [SerializeField] private Slider _playerHealth;
    [SerializeField] private Slider _playerArmor;
    [SerializeField] private Slider _enemyHealth;
    [SerializeField] private ImageRotator _changeWeaponDelayImage;
    [SerializeField] private ImageRotator _spawnDelayImage;

    public event Action StartButtonClicked;
    public event Action CancelButtonClicked;
    public event Action ChangeWeaponButtonClicked;
    public event Action TakeFullHPButtonClicked;
     
    public CharacterStateSlider PlayerPrepareSlider => _playerPrepareSlider;

    public CharacterStateSlider PlayerAttackSlider => _playerAttackSlider;

    public CharacterStateSlider EnemyPrepareSlider => _enemyPrepareSlider;

    public CharacterStateSlider EnemyAttackSlider => _enemyAttackSlider;

    private void Start()
    {
        _start.onClick.AddListener(OnStartButtonClicked);
        _cancel.onClick.AddListener(OnCancelButtonClicked);
        _changeWeapon.onClick.AddListener(OnChangeWeaponClicked);
        _takeFullHP.onClick.AddListener(OnTakeFullHPButtonClicked);
    }

    private void OnDisable()
    {
        _start.onClick.RemoveListener(OnStartButtonClicked);
        _cancel.onClick.RemoveListener(OnCancelButtonClicked);
        _changeWeapon.onClick.RemoveListener(OnChangeWeaponClicked);
        _takeFullHP.onClick.RemoveListener(OnTakeFullHPButtonClicked);
    }

    public void TurnChangeWeaponDelayImage(bool isWeaponChanging)
    {
        _changeWeaponDelayImage.gameObject.SetActive(isWeaponChanging);
    }

    public void TurnSpawnDelayImage(bool isSpawning)
    {
        _spawnDelayImage.gameObject.SetActive(isSpawning);
    }

    public void TurnTakeFullHPButton(bool isNotInFight)
    {
        _takeFullHP.gameObject.SetActive(isNotInFight);
    }
    public void TurnCancelButton(bool isNotInFight)
    {
        _cancel.gameObject.SetActive(isNotInFight);
    }

    public void SetPlayerArmorValueSlider(float value)
    {
        _playerArmor.maxValue = value;
        _playerArmor.value = value;
    }

    public void SetMaxPlayerHealthSlider(float maxValue)
    {
        _playerHealth.maxValue = maxValue;
        _playerHealth.value = maxValue;
    }

    public void SetMaxEnemyHealthSlider(float maxValue)
    {
        _enemyHealth.maxValue = maxValue;
        _enemyHealth.value = maxValue;
        _enemyHealth.gameObject.SetActive(true);
    }

    public void StopShowEnemyHealth()
    {
        _enemyHealth.gameObject.SetActive(false);
    }

    public void ShowPlayerHealth(float currentValue)
    {
        _playerHealth.value = currentValue;
    }

    public void ShowEnemyHealth(float currentValue)
    {
        _enemyHealth.value = currentValue;    
    }

    public void EnableStarBatton()
    {
        _start.gameObject.SetActive(true);
    }

    private void OnStartButtonClicked()
    {
        StartButtonClicked?.Invoke();
        _start.gameObject.SetActive(false);
    }

    private void OnCancelButtonClicked()
    {
        CancelButtonClicked?.Invoke();
    }

    private void OnChangeWeaponClicked()
    {
        ChangeWeaponButtonClicked?.Invoke();
    }

    private void OnTakeFullHPButtonClicked()
    {
        TakeFullHPButtonClicked?.Invoke();
    }
}
