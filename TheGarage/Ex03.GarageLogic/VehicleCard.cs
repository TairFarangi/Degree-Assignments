/// <summary>
/// Class that represents the card of the vehicle details in garage.
/// </summary>
public class VehicleCard
{
        // Declare of variables
        private BasicVehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;

        // Constructor
        public VehicleCard(BasicVehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber, eVehicleStatus i_VehicleStatus)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatus = i_VehicleStatus;           
        }
        
        // Properties
        public BasicVehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }
        
        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }
        
        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }            
        }

        // Method
        public override string ToString()
        {
            return string.Format("{0}.\nOwner name: {1}.\nOwner phone number: {2}.\nVehicle status: {3}.", m_Vehicle.ToString(), m_OwnerName, m_OwnerPhoneNumber, m_VehicleStatus);
        }
}