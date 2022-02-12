/// <summary>
/// Class of Garage user interface.
/// </summary>
using System;
using System.Collections.Generic;

public class GarageUI
{
    private Garage m_Garage;

    // Constructor
    public GarageUI(Garage i_Garage)
    {
        m_Garage = i_Garage;
    }

    // Start the work in the garage.
    public void StartWork()
    {       
        const string k_Quit = "q";
        string userChoice = string.Empty;
        eServiceType serviceType;

        Console.Write("Hello! welcome to Moran and Tair's garage:)\n");

        while (userChoice != k_Quit)
        {
            displayStartMenu();
            userChoice = getValidMenuInput();

            if (userChoice == "q")
            {
                serviceType = eServiceType.Exit;
            }
            else
            {
                serviceType = (eServiceType)Enum.Parse(typeof(eServiceType), userChoice);
            }

            switch (serviceType)
            {
                case eServiceType.AddVehicleToTheGarage:
                    addVehicleToTheGarage();
                    break;

                case eServiceType.ShowLicencePlates:
                    displayLicencePlates();
                    break;

                case eServiceType.ChangeVehicleStatus:                    
                    changeVehicleStatus();
                    break;

                case eServiceType.InflatingTheWheelsToMaximum:
                    inflateWheels();
                    break;

                case eServiceType.VehicleRefueling:
                    vehicleFueling();
                    break;

                case eServiceType.ChargingElectricVehicle:
                    chargeElectricVehicle();
                    break;

                case eServiceType.PrintVehicleDetails:
                    displayVehicleDetails();
                    break;

                case eServiceType.Exit:
                    Console.WriteLine("We were happy to give you service. Good bye!");
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }

    // Function in switch case 
    private void addVehicleToTheGarage()
    {
        eVehicleType vehicleType;
        string vehicleTypeStr = string.Empty;

        // For enum
        const int k_MinChoice = 0;
        const int k_MaxChoice = 4;

        // get licence plate from user
        string licensePlate = getLicencePlate();

        if (!vehicleExistsInGarage(licensePlate))
        {
            displayVehicleMenu();
            vehicleTypeStr = getValidEnumValue(k_MinChoice, k_MaxChoice);
            vehicleType = (eVehicleType)Enum.Parse(typeof(eVehicleType), vehicleTypeStr);

            switch (vehicleType)
            {
                case eVehicleType.BasicMotorcycle:
                    VehicleCard basicMotorcycleCard = createABasicMotorcycle(licensePlate, vehicleType);
                    m_Garage.AddNewVehicle(basicMotorcycleCard);
                    break;

                case eVehicleType.ElectricMotorcycle:
                    VehicleCard electricMotorcycleCard = createAnElectricMotorcycle(licensePlate, vehicleType);
                    m_Garage.AddNewVehicle(electricMotorcycleCard);
                    break;

                case eVehicleType.BasicCar:
                    VehicleCard basicCarCard = createABasicCar(licensePlate, vehicleType);
                    m_Garage.AddNewVehicle(basicCarCard);
                    break;

                case eVehicleType.ElectricCar:
                    VehicleCard electricCarCard = createAnElectricCar(licensePlate, vehicleType);
                    m_Garage.AddNewVehicle(electricCarCard);
                    break;

                case eVehicleType.Truck:
                    VehicleCard truckCard = createATruck(licensePlate, vehicleType);
                    m_Garage.AddNewVehicle(truckCard);
                    break;

                default:
                    Console.WriteLine("Bad input.");
                    break;
            }
        }
    }

    private void displayLicencePlates()
    {
        // For enum
        const int k_MinChoice = 0;
        const int k_MaxChoice = 3;

        Console.WriteLine("Display licence plates by vehicle status:\n0 ---> In repair.\n1 ---> Fixed. \n2 ---> Paid.\n3 ---> All repair statuses.");
        string vehicleStatusStr = getValidEnumValue(k_MinChoice, k_MaxChoice);
        eVehicleStatus vehicleStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), vehicleStatusStr);

        List<string> licensePlateList = m_Garage.LicencePlates(vehicleStatus);

        if (licensePlateList.Count != 0)
        {
            Console.WriteLine("Licence plates:");
            foreach (string licencePlateStr in licensePlateList)
            {
                Console.WriteLine(string.Format("{0}.", licencePlateStr));
            }
        }
        else
        {
            if(m_Garage.GarageCards.Count == 0)
            {
                Console.WriteLine("There is no vehicles int the garage.");
            }
            else
            {
                Console.WriteLine(string.Format("There is no vehicles with status: {0} in the garage.", vehicleStatus));
            }
        }
    }

    private void changeVehicleStatus()
    {
        // get licence plate from user
        string licensePlate = getLicencePlate();
        bool vehicleIsFound = false;

        // For enum
        const int k_MinChoice = 0;
        const int k_MaxChoice = 2;

        Console.WriteLine("Change vehicle status to:\n0 ---> In repair\n1 ---> Fixed \n2 ---> Paid.");
        string vehicleStatusStr = getValidEnumValue(k_MinChoice, k_MaxChoice);
        eVehicleStatus vehicleStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), vehicleStatusStr);

        m_Garage.ChangeVehicleStatus(licensePlate, vehicleStatus, ref vehicleIsFound);

        if (vehicleIsFound)
        {
            Console.WriteLine(string.Format("Vehicle {0} in status: {1}.", licensePlate, vehicleStatus));
        }
        else
        {
            Console.WriteLine(string.Format("Sorry, There is no vehicle with licence plate: {0} in the garage.", licensePlate));
        }
    }

    private void inflateWheels()
    {
        Console.WriteLine("Inflate wheels:");
        string licensePlate = getLicencePlate();
        bool vehicleIsFound = false;

        try
        {
            m_Garage.InflateWheels(licensePlate, ref vehicleIsFound);

            if (vehicleIsFound)
            {
                Console.WriteLine(string.Format("Inflate of the vehicle wheels: {0} is complete!", licensePlate));
            }
            else
            {
                Console.WriteLine(string.Format("Sorry, There is no vehicle with licence plate: {0} in the garage.", licensePlate));
            }
        }
        catch (ValueOutOfRangeException i_ValueOutOfRangeException) 
        {
            Console.WriteLine(i_ValueOutOfRangeException.Message);
            Console.WriteLine("You passed the maximum air pressure of your wheels.");
        }
    }

    private void vehicleFueling()
    {
        ////This method fuels a gas-powered vehicle. First check that there is a vehicle with such a license number, 
        ////And then checks if the engine type is appropriate        
        bool isSuitableEngine = false;
        bool vehicleIsFound = false;

        Console.WriteLine("Vehicle fueling:");
        string licensePlate = getLicencePlate();

        // For enum
        int k_MinChoice = 0;
        int k_MaxChoice = 3;

        Console.WriteLine("Please enter type of fuel:\n0---> Soler\n1---> Octan95\n2---> Octan96\n3---> Octan98.");
        string fuelTypeStr = getValidEnumValue(k_MinChoice, k_MaxChoice);
        eFuelType fuelType = (eFuelType)Enum.Parse(typeof(eFuelType), fuelTypeStr);

        Console.WriteLine("Please enter quantity of liter to add:");
        float quantityOfLiterToAdd = getValidFloatNumber();

        try
        {
            m_Garage.VehicleFueling(licensePlate, fuelType, quantityOfLiterToAdd, ref vehicleIsFound, ref isSuitableEngine);

            if (vehicleIsFound)
            {
                if (isSuitableEngine)
                {
                    Console.WriteLine(string.Format("Fueling of vehicle {0} was completed!", licensePlate));
                }
                else
                {
                    Console.WriteLine(string.Format("Sorry, {0} is an electric vehicle.It cannot be refueled.", licensePlate));
                }
            }
            else
            {
                Console.WriteLine(string.Format("Sorry, There is no vehicle with licence plate: {0} in the garage.", licensePlate));
            }
        }
        catch (ValueOutOfRangeException i_ValueOutOfRangeException)
        {
            Console.WriteLine(i_ValueOutOfRangeException.Message);
            Console.WriteLine("You passed the maximum fuel amount of your vehicle tank.");
        }
        catch (ArgumentException i_ArgumentException)
        {
            Console.WriteLine(i_ArgumentException.Message);
        }
    }

    private void chargeElectricVehicle()
    {
        string licensePlate = getLicencePlate();
        bool vehicleIsFound = false;
        bool isSuitableEngine = false;

        Console.WriteLine("Please enter minutes to charge:");        
        float minutesToCharge = getValidFloatNumber();

        try
        {
            m_Garage.ChargeElectricVehicle(licensePlate, minutesToCharge, ref vehicleIsFound, ref isSuitableEngine);

            if (vehicleIsFound)
            {
                if (isSuitableEngine)
                {
                    Console.WriteLine(string.Format("Charging of vehicle {0} was completed!", licensePlate));
                }
                else
                {
                    Console.WriteLine(string.Format("Sorry, {0} is not an electric vehicle.It cannot be charged.", licensePlate));
                }
            }
            else
            {
                Console.WriteLine(string.Format("Sorry, There is no vehicle with licence plate: {0} in the garage.", licensePlate));
            }
        }
        catch (ValueOutOfRangeException i_ValueOutOfRangeException)
        {
            Console.WriteLine(i_ValueOutOfRangeException.Message);
            Console.WriteLine("Your vehicle battry is fully charged.");
        }
    }

    private void displayVehicleDetails()
    {
        bool vehicleIsFound = false;
        string licensePlate = getLicencePlate();
        VehicleCard vehicleCard = m_Garage.VehicleDetails(licensePlate, ref vehicleIsFound);

        if (vehicleIsFound)
        {
            Console.WriteLine("\nVehicle card:");
            Console.WriteLine(vehicleCard.ToString());
        }
        else
        {
            Console.WriteLine(string.Format("Sorry, There is no vehicle with licence plate: {0} in the garage.", licensePlate));
        }
    }

    // Help functions.
    private void displayVehicleMenu()
    {
        Console.WriteLine("What car do you want to put in the garage?");
        Console.WriteLine("0 ---> Basic Motorcycle.");
        Console.WriteLine("1 ---> Electric Motorcycle.");
        Console.WriteLine("2 ---> Basic Car.");
        Console.WriteLine("3 ---> Electric Car.");
        Console.WriteLine("4 ---> Truck.");
    }

    private bool vehicleExistsInGarage(string i_LicensePlate)
    {
        bool vehicleIsFound = false;

        foreach (VehicleCard vehicleCard in m_Garage.GarageCards)
        {
            if (vehicleCard.Vehicle.LicensePlate == i_LicensePlate)
            {
                Console.WriteLine(string.Format("Vehicle with licence plate: {0} already exist in the garage.", i_LicensePlate));
                vehicleIsFound = true;
                break;
            }
        }

        return vehicleIsFound;
    }

    // Start menue
    private void displayStartMenu()
    {
        Console.WriteLine("\nPlease select the service you want:");
        Console.WriteLine("0 ---> Add Vehicle To The Garage.");
        Console.WriteLine("1 ---> Show Licence Plates.");
        Console.WriteLine("2 ---> Change vehicle status.");
        Console.WriteLine("3 ---> Inflating the wheels to maximum.");
        Console.WriteLine("4 ---> Vehicle refueling.");
        Console.WriteLine("5 ---> Charging electric vehicle.");
        Console.WriteLine("6 ---> Display Vehicle Details.");
        Console.WriteLine("q ---> To exit.");
    }

    private string getValidMenuInput()
    {
        bool isValidInput = false;
        string userInput = string.Empty;

        while (!isValidInput)
        {
            try
            {
                userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
                {
                    throw new ArgumentException("Invalid empty input.Try again.");
                }

                for (int i = 0; i < userInput.Length; i++)
                {
                    //// If there is a letter and it is not q.
                    if (char.IsLetter(userInput[i]) && userInput[i] != 'q')
                    {
                        throw new FormatException("Invalid letter: " + userInput[i] + ".");
                    }

                    //// If it is not a digit
                    if (!char.IsLetterOrDigit(userInput[i]))
                    {
                        throw new FormatException("Invalid character: " + userInput[i] + ".");
                    }

                    if (userInput[i] != '0' && userInput[i] != '1' && userInput[i] != '2' && userInput[i] != '3' && userInput[i] != '4' && userInput[i] != '5' && userInput[i] != '6' && userInput[i] != 'q')
                    {
                        throw new ValueOutOfRangeException(0, 6);
                    }
                }

                isValidInput = true;
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine(i_FormatException.Message);
            }
            catch(ValueOutOfRangeException i_ValueOutOfRangeException)
            {
               Console.WriteLine(i_ValueOutOfRangeException.Message);
            }                                   
    }

        return userInput;
    }

    // Licence plate
    private string getLicencePlate()
    {
        Console.WriteLine("Please type license plate of the vehicle: ");
        string licensePlate = getValidNumberString();

        return licensePlate;
    }

    // Get valid values
    private string getValidNumberString()
    {
        // Return just a number.
        string numberStr = string.Empty;
        bool isValidInput = false;

        while (!isValidInput)
        {
            try
            {
                numberStr = Console.ReadLine();

                if (string.IsNullOrEmpty(numberStr) || string.IsNullOrWhiteSpace(numberStr))
                {
                    throw new ArgumentException("Invalid empty input.Try again.");
                }

                for (int i = 0; i < numberStr.Length; i++)
                {
                    if (!char.IsDigit(numberStr[i]))
                    {
                        throw new FormatException(string.Format("Invalid char: {0}. Enter only a digit.", numberStr[i]));
                    }
                }

                isValidInput = true;
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine(i_FormatException.Message);
            }
        }
        
        return numberStr;
    }

    private float getValidFloatNumber()
    {
        // Return just a float number.
        string floatNumberStr = string.Empty;
        bool isValidInput = false;
        bool isValidFloat = false;
        float floatNumber = 0;

        while (!isValidInput)
        {
            try
            {
                floatNumberStr = Console.ReadLine();
                isValidFloat = float.TryParse(floatNumberStr, out floatNumber);

                if (string.IsNullOrEmpty(floatNumberStr) || string.IsNullOrWhiteSpace(floatNumberStr))
                {
                    throw new ArgumentException("Invalid empty input.Try again.");
                }

                if (!isValidFloat)
                {
                    throw new FormatException(string.Format("Invalid string: {0}. Enter only a float number.", floatNumberStr));
                }
                
                isValidInput = true;
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine(i_FormatException.Message);
            }
        }

        floatNumber = float.Parse(floatNumberStr);

        return floatNumber;
    }

    private string getValidLetterString()
    {        
        string validString = string.Empty;
        bool isValidInput = false;

        while (!isValidInput)
        {
            try
            {
                validString = Console.ReadLine();

                if (string.IsNullOrEmpty(validString) || string.IsNullOrWhiteSpace(validString))
                {
                    throw new ArgumentException("Invalid empty input.Try again.");
                }

                for (int i = 0; i < validString.Length; i++)
                {
                    if (!char.IsLetter(validString[i]))
                    {
                        throw new FormatException(string.Format("Invalid input. Please enter only letters.", validString[i]));
                    }
                }

                isValidInput = true;
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine(i_FormatException.Message);
            }
        }

        return validString;
    }

    private float getValidRangeNumberValue(float i_MaxValue)
    {
        bool isValidInput = false;
        float currentValue = 0;

        while (!isValidInput)
        {
            try
            {
                currentValue = getValidFloatNumber();
                if (currentValue > i_MaxValue)
                {
                    throw new ValueOutOfRangeException(0, i_MaxValue);
                }

                isValidInput = true;
            }
            catch (ValueOutOfRangeException i_ValueOutOfRangeException)
            {
                Console.WriteLine(i_ValueOutOfRangeException.Message);
            }
        }

        return currentValue;
    }

    private string getValidEnumValue(int i_MinValue, int i_MaxValue)
    {
        bool isValidInput = false;
        string inputForEnumStr = string.Empty;
        float valueForEnum = 0;

        while (!isValidInput)
        {
            try
            {
                inputForEnumStr = getValidNumberString();
                valueForEnum = float.Parse(inputForEnumStr);
                if (valueForEnum < i_MinValue || valueForEnum > i_MaxValue)
                {
                    throw new ValueOutOfRangeException(i_MinValue, i_MaxValue);
                }

                isValidInput = true;
            }
            catch (ValueOutOfRangeException i_ValueOutOfRangeException)
            {
                Console.WriteLine(i_ValueOutOfRangeException.Message);
            }
        }

        return inputForEnumStr;
    }

    private bool getValidBoolValue()
    {
        bool validBoolValue = false;
        bool isValidValue = false;
        string boolValueStr = string.Empty;

        while (!isValidValue)
        {
            try
            {
                boolValueStr = Console.ReadLine();

                if (string.IsNullOrEmpty(boolValueStr) || string.IsNullOrWhiteSpace(boolValueStr))
                {
                    throw new ArgumentException("Invalid empty input.Try again.");
                }

                if (boolValueStr.Length != 1)
                {
                    throw new FormatException("Invalid input.Try again.");
                }

                for (int i = 0; i < boolValueStr.Length; i++)
                {
                    if (boolValueStr[i] != 'y' && boolValueStr[i] != 'n')
                    {
                        throw new FormatException("Invalid input. Try again.");
                    }
                }

                isValidValue = true;
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine(i_FormatException.Message);
            }
        }

        validBoolValue = (boolValueStr == "y") ? true : false;

        return validBoolValue;
    }

    // Basic vehicle
    private void inputForBasicVehicle(out string o_ModelName, out string o_ManufacturernName)
    {
        o_ModelName = string.Empty;
        o_ManufacturernName = string.Empty;        

        Console.WriteLine("Please enter a model name:");
        o_ModelName = getValidLetterString();        

        Console.WriteLine("Please enter manufacturern name of the wheels:");
        o_ManufacturernName = getValidLetterString();       
    }

    // Motorcycle
    private void inputForMotorcycle(out float o_CurrentAirPressure, out eLicenseType o_LicenseType, out int engineVolume)
    {
        // Wheels 
        float o_MaximumAirPressure = 28;

        // For enum
        const int k_MinChoice = 0;
        const int k_MaxChoice = 3;

        Console.WriteLine("Please enter current air pressure:");
        o_CurrentAirPressure = getValidRangeNumberValue(o_MaximumAirPressure);

        Console.WriteLine("Please enter type of license:\n0---> A.\n1---> A1.\n2---> A2.\n3---> B.");
        string licenseTypeStr = getValidEnumValue(k_MinChoice, k_MaxChoice);
        o_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), licenseTypeStr);

        Console.WriteLine("Type an engine volume of your motorcycle:");
        string engineVolumeStr = getValidNumberString();
        engineVolume = int.Parse(engineVolumeStr);
    }

    private void inputForBasicMotorcycle(out BasicEngine o_EngineType)
    {
        // Fuel Engine    
        eFuelType fuelType = eFuelType.Octan98;
        float maximumFuelCapacity = 5.5f;

        // Fuel Engine
        Console.WriteLine("Please enter current amount of fuel:");
        float currentAmountOfFuel = getValidRangeNumberValue(maximumFuelCapacity);
        float energyLeft = 1 - (currentAmountOfFuel / maximumFuelCapacity);

        // Create fuelEngine
        o_EngineType = new FuelEngine(fuelType, currentAmountOfFuel, maximumFuelCapacity, energyLeft);
    }
    
    private void inputForElectricMotorcycle(out BasicEngine o_EngineType)
    {
        // Electric Engine    
        float maximumBatteryTime = 1.6f;

        Console.WriteLine("Please enter battery time left:");
        float batteryTimeLeft = getValidRangeNumberValue(maximumBatteryTime);
        float energyLeft = batteryTimeLeft / maximumBatteryTime;

        o_EngineType = new ElectricEngine(batteryTimeLeft, maximumBatteryTime, energyLeft);
    }

    // Car 
    private void inputForCar(out float o_CurrentAirPressure, out eColor o_Color, out eNumberofDoors o_NumberofDoors)
    {                
        float o_MaximumAirPressure = 30;

        // For enum
        const int k_MinChoice = 0;
        const int k_MaxChoice = 3;

        Console.WriteLine("Please enter current air pressure:");
        o_CurrentAirPressure = getValidRangeNumberValue(o_MaximumAirPressure);

        Console.WriteLine("Please enter color type:\n0---> Yellow.\n1---> White.\n2---> Black.\n3---> Blue.");
        string colorNum = getValidEnumValue(k_MinChoice, k_MaxChoice);
        o_Color = (eColor)Enum.Parse(typeof(eColor), colorNum);

        Console.WriteLine("Please enter number of doors:\n0---> Two.\n1---> Three.\n2---> Four.\n3---> Five.");
        string amountOfDoorsNum = getValidEnumValue(k_MinChoice, k_MaxChoice);
        o_NumberofDoors = (eNumberofDoors)Enum.Parse(typeof(eNumberofDoors), amountOfDoorsNum);
    }
    
    private void inputForBasicCar(out BasicEngine o_EngineType)
    {
        // Fuel Engine    
        eFuelType fuelType = eFuelType.Octan95;
        float maximumFuelCapacity = 50f;

        // Fuel Engine
        Console.WriteLine("Please enter current amount of fuel:");
        float currentAmountOfFuel = getValidRangeNumberValue(maximumFuelCapacity);

        float energyLeft = 1 - (currentAmountOfFuel / maximumFuelCapacity);

        // Create fuelEngine
        o_EngineType = new FuelEngine(fuelType, currentAmountOfFuel, maximumFuelCapacity, energyLeft);
    }

    private void inputForElectricCar(out BasicEngine o_EngineType)
    {
        // Electric Engine  
        float maximumBatteryTime = 2.8f;

        Console.WriteLine("Please enter battery time left:");
        float batteryTimeLeft = getValidRangeNumberValue(maximumBatteryTime);
        float energyLeft = batteryTimeLeft / maximumBatteryTime;

        o_EngineType = new ElectricEngine(batteryTimeLeft, maximumBatteryTime, energyLeft);
    }

    // Truck
    private void inputForTruck(out float o_CurrentAirPressure, out BasicEngine o_EngineType, out bool i_HasHazardousMaterials, out float i_CargoVolume)
    {
        float o_MaximumAirPressure = 26;

        // Fuel Engine    
        eFuelType fuelType = eFuelType.Soler;
        float maximumFuelCapacity = 110f;

        Console.WriteLine("Please enter current air pressure:");
        o_CurrentAirPressure = getValidRangeNumberValue(o_MaximumAirPressure);

        Console.WriteLine("Please enter current amount of fuel:");
        float currentAmountOfFuel = getValidRangeNumberValue(maximumFuelCapacity);

        float energyLeft = 1 - (currentAmountOfFuel / maximumFuelCapacity);

        // Create fuel Engine
        o_EngineType = new FuelEngine(fuelType, currentAmountOfFuel, maximumFuelCapacity, energyLeft);

        Console.WriteLine("Does the truck transport hazardous materials? y/n");
        i_HasHazardousMaterials = getValidBoolValue();

        Console.WriteLine("Please enter cargo volume of the truck:");
        i_CargoVolume = getValidFloatNumber();
    }

    // Garage card
    private void inputForVehicleDetails(out string io_OwnerName, out string io_OwnerNumberPhone, out eVehicleStatus i_VehicleStatus)
    {
        i_VehicleStatus = eVehicleStatus.InRepair;
        io_OwnerName = null;
        io_OwnerNumberPhone = null;

        Console.WriteLine("Please enter owner name of the vehicle:");
        io_OwnerName = getValidLetterString();

        Console.WriteLine("Please enter owner phone number of the vehicle:");
        io_OwnerNumberPhone = getValidNumberString();

        Console.WriteLine(string.Format("{0}'s vehicle in repair.", io_OwnerName));
    }

    private VehicleCard createABasicMotorcycle(string i_LicensePlate, eVehicleType i_VehicleType)
    {
        // Vehicle
        string modelName;

        // Wheels 
        string manufacturernName;
        float currentAirPressure;

        // Fuel Engine    
        BasicEngine engine;

        // Motorcycle
        eLicenseType licenseType;
        int engineVolume;

        // Garage card
        string ownerName;
        string ownerPhone;
        eVehicleStatus vehicleStatus;

        // Create basic motorcycle
        Motorcycle basicMotorcycle = VehicleFactory.getVehicle(i_LicensePlate, i_VehicleType) as Motorcycle;

        inputForBasicVehicle(out modelName, out manufacturernName);
        inputForMotorcycle(out currentAirPressure, out licenseType, out engineVolume);
        inputForBasicMotorcycle(out engine);
        inputForVehicleDetails(out ownerName, out ownerPhone, out vehicleStatus);

        // Update class fields
        basicMotorcycle.ModelName = modelName;
        basicMotorcycle.LicenseType = licenseType;
        basicMotorcycle.EngineVolume = engineVolume;
        basicMotorcycle.BasicEngine = engine;

        foreach (Wheel wheel in basicMotorcycle.Wheels)
        {
            wheel.ManufacturernName = manufacturernName;
            wheel.CurrentAirPressure = currentAirPressure;
        }

        VehicleCard vehicleCard = new VehicleCard(basicMotorcycle, ownerName, ownerPhone, vehicleStatus);

        return vehicleCard;
    }

    private VehicleCard createAnElectricMotorcycle(string i_LicensePlate, eVehicleType i_VehicleType)
    {
        // Vehicle
        string modelName;

        // Wheels 
        string manufacturernName;
        float currentAirPressure;

        // Electric Engine    
        BasicEngine engine;

        // Motorcycle
        eLicenseType licenseType;
        int engineVolume;

        // Garage card
        string ownerName;
        string ownerPhone;
        eVehicleStatus vehicleStatus;

        // Create electric motorcycle
        Motorcycle electricMotorcycle = VehicleFactory.getVehicle(i_LicensePlate, i_VehicleType) as Motorcycle;

        inputForBasicVehicle(out modelName, out manufacturernName);
        inputForMotorcycle(out currentAirPressure, out licenseType, out engineVolume);
        inputForElectricMotorcycle(out engine);
        inputForVehicleDetails(out ownerName, out ownerPhone, out vehicleStatus);

        // Update class fields
        electricMotorcycle.ModelName = modelName;
        electricMotorcycle.LicenseType = licenseType;
        electricMotorcycle.EngineVolume = engineVolume;
        electricMotorcycle.BasicEngine = engine;

        foreach (Wheel wheel in electricMotorcycle.Wheels)
        {
            wheel.ManufacturernName = manufacturernName;
            wheel.CurrentAirPressure = currentAirPressure;
        }

        VehicleCard vehicleCard = new VehicleCard(electricMotorcycle, ownerName, ownerPhone, vehicleStatus);

        return vehicleCard;
    }

    private VehicleCard createABasicCar(string i_LicensePlate, eVehicleType i_VehicleType)
    {
        // Vehicle
        string modelName;

        // Wheels 
        string manufacturernName;
        float currentAirPressure;

        // Fuel Engine    
        BasicEngine engine;

        // Car 
        eColor color;
        eNumberofDoors numberofDoors;

        // Garage card
        string ownerName;
        string ownerPhone;
        eVehicleStatus vehicleStatus;

        Car basicCar = VehicleFactory.getVehicle(i_LicensePlate, i_VehicleType) as Car;

        inputForBasicVehicle(out modelName, out manufacturernName);
        inputForCar(out currentAirPressure, out color, out numberofDoors);
        inputForBasicCar(out engine);
        inputForVehicleDetails(out ownerName, out ownerPhone, out vehicleStatus);

        // Update class fields
        basicCar.ModelName = modelName;
        basicCar.Color = color;
        basicCar.NumberOfDoors = numberofDoors;
        basicCar.BasicEngine = engine;

        foreach (Wheel wheel in basicCar.Wheels)
        {
            wheel.ManufacturernName = manufacturernName;
            wheel.CurrentAirPressure = currentAirPressure;
        }

        VehicleCard vehicleCard = new VehicleCard(basicCar, ownerName, ownerPhone, vehicleStatus);

        return vehicleCard;
    } 

    private VehicleCard createAnElectricCar(string i_LicensePlate, eVehicleType i_VehicleType)
    {
        // Vehicle
        string modelName;

        // Wheels 
        string manufacturernName;
        float currentAirPressure;

        // Electric Engine    
        BasicEngine engine;

        // Car 
        eColor color;
        eNumberofDoors numberofDoors;

        // Garage card
        string ownerName;
        string ownerPhone;
        eVehicleStatus vehicleStatus;

        Car electricCar = VehicleFactory.getVehicle(i_LicensePlate, i_VehicleType) as Car;

        inputForBasicVehicle(out modelName, out manufacturernName);
        inputForCar(out currentAirPressure, out color, out numberofDoors);
        inputForElectricCar(out engine);
        inputForVehicleDetails(out ownerName, out ownerPhone, out vehicleStatus);

        // Update class fields
        electricCar.ModelName = modelName;
        electricCar.Color = color;
        electricCar.NumberOfDoors = numberofDoors;
        electricCar.BasicEngine = engine;

        foreach (Wheel wheel in electricCar.Wheels)
        {
            wheel.ManufacturernName = manufacturernName;
            wheel.CurrentAirPressure = currentAirPressure;
        }

        VehicleCard vehicleCard = new VehicleCard(electricCar, ownerName, ownerPhone, vehicleStatus);

        return vehicleCard;
    }

    private VehicleCard createATruck(string i_LicensePlate, eVehicleType i_VehicleType)
    {
        // Vehicle
        string modelName;

        // Wheels 
        string manufacturernName;
        float currentAirPressure;

        // Fuel Engine    
        BasicEngine engine;

        // Truck 
        bool hasHazardousMaterials;
        float cargoVolume;

        // Garage card
        string ownerName;
        string ownerPhone;
        eVehicleStatus vehicleStatus;

        Truck truck = VehicleFactory.getVehicle(i_LicensePlate, i_VehicleType) as Truck;

        inputForBasicVehicle(out modelName, out manufacturernName);
        inputForTruck(out currentAirPressure, out engine, out hasHazardousMaterials, out cargoVolume); 
        inputForVehicleDetails(out ownerName, out ownerPhone, out vehicleStatus);

        // Update class fields
        truck.ModelName = modelName;
        truck.HasHazardousMaterials = hasHazardousMaterials;
        truck.CargoVolume = cargoVolume;
        truck.BasicEngine = engine;

        foreach (Wheel wheel in truck.Wheels)
        {
            wheel.ManufacturernName = manufacturernName;
            wheel.CurrentAirPressure = currentAirPressure;
        }

        VehicleCard vehicleCard = new VehicleCard(truck, ownerName, ownerPhone, vehicleStatus);

        return vehicleCard;
    }
}