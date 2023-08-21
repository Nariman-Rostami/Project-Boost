using UnityEngine;
using UnityEngine.UI;

public class RocketFuel : MonoBehaviour
{
    [SerializeField]public float startingFuel = 100f;
    [SerializeField]public float fuelBurnRate = 0.05f;
    public Slider fuelBar;
    public float currentFuel;
    bool fuelDisabled;
    private void Start()
    {
        StartingActions();
    }
    private void Update()
    {
        RespondToDebugKeys();
        FuelReduction();
    }
        void StartingActions()
    {
        currentFuel = startingFuel;
        fuelBar.maxValue = startingFuel;
        fuelBar.value = startingFuel;
    }

    private void FuelReduction()
    {
        if (fuelDisabled) { return; }
        if (IsPressingSpace() && IsHavingFuel())
        {
            currentFuel -= fuelBurnRate * Time.deltaTime;
            fuelBar.value = currentFuel;
        }
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            fuelDisabled = !fuelDisabled;
        }
    }
    static bool IsPressingSpace() {return Input.GetKey(KeyCode.Space);}
    bool IsHavingFuel() {return currentFuel > 0f;}
}
