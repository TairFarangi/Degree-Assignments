/// <summary>
/// Class that represents the engine for gas-powered vehicles.
/// </summary>
using System;

public class FuelEngine : BasicEngine
{
    // Declare of variables
    private readonly float r_MaximumFuelCapacity;
    private eFuelType m_FuelType;
    private float m_CurrentFuelAmount;

    // Constructor
    public FuelEngine(eFuelType i_FuelType, float i_CurrentFuelAmount, float i_MaximumFuelCapacity, float i_EnergyLeft) : base(i_EnergyLeft)
    {
        r_MaximumFuelCapacity = i_MaximumFuelCapacity;
        m_FuelType = i_FuelType;
        m_CurrentFuelAmount = i_CurrentFuelAmount;
    }

    // Properties
    public eFuelType FuelType
    {
        get
        {
            return m_FuelType;
        }
    }

    public float CurrentFuelAmount
    {
        get
        {
            return m_CurrentFuelAmount;
        }
    }

    // Methods
    public void AddFuel(float i_AmountOfLiterToAdd, eFuelType i_FuelType)
    {
        if (m_CurrentFuelAmount + i_AmountOfLiterToAdd > r_MaximumFuelCapacity)
        {
            throw new ValueOutOfRangeException(0, r_MaximumFuelCapacity);
        }

        if (i_FuelType != m_FuelType)
        {
            throw new ArgumentException("Sorry. You have selected the wrong type of fuel.");
        }

        m_CurrentFuelAmount += i_AmountOfLiterToAdd;
        EnergyLeftPercent = m_CurrentFuelAmount / r_MaximumFuelCapacity;
    }

    public override string ToString()
    {
        return string.Format("Fuel type: {0}.\nCurrent fuel amount: {1}.\nMaximum fuel capacity: {2}.\n{3}", m_FuelType, m_CurrentFuelAmount, r_MaximumFuelCapacity, base.ToString());
    }
}