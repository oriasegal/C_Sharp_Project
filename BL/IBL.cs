using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BLAPI
{
    public interface IBL
    {
        #region Station CRUD
        bool AddStation(int StationNumber, string StationName, double Latitude, double Longitude, string Address); //CREATE
        BO.Station ReadStation(int StationNumber); //READ 
        bool UpdateStation(int StationNumber, int NewStationNumber, string StationName, double Latitude, double Longitude, string Address); //UPDATE
        bool DeleteStation(int StationNumber); //DELETE

        List<BO.Station> GetStations();

        List<BO.BusLine> GetStationPassingLines(BO.Station station);

        #endregion

        #region Bus CRUD
        bool AddBus(string LicenseNumber, string StartDate, double Mileage, double Fuel); //CREATE (gets statingDate as string!!!!)
        BO.Bus ReadBus(string LicenseNumber); //READ 
        bool UpdateBus(string LicenseNumber, string NewLicenseNumber, double Mileage, double Fuel);
        bool DeleteBus(string LicenseNumber); //DELETE

        void BusTreatment(BO.Bus bus); //treatment due to mileage over 20,000 km
        void BusRefuel(BO.Bus bus);

        //List<BO.Bus> GetBusses();
        IEnumerable<BO.Bus> GetBusses();

        #endregion

        #region User CRUD
        bool AddUser(string UserName, string Password, bool Permission); //CREATE
        BO.User ReadUser(string UserName, string Password); //READ 
        bool UpdateUser(string UserName, string Password, bool Permission); //UPDATE
        bool DeleteUser(string UserName); //DELETE

        List<BO.User> GetUsers();

        #endregion

        #region BusLine CRUD
        bool AddBusLine(int LineNumber, string Area, int FirstStation, int LastStation);
        BO.BusLine ReadBusLine(int lineNumber);
        bool UpdateBusLine(int LineIndex, int NewLineNumber, string Area, int StationNumber, string Action);
        bool UpdateStationOnLine(int LineIndex, int StationNumber, int Placement, int DistancePrevious, int DistanceNext, int TravelTimePrevious, int TravelTimeNext);
        bool AddStationToLine(BO.BusLine BusLine, BO.Station Station);
        bool DeleteStationFromLine(BO.BusLine BusLine, BO.Station Station);
        bool DeleteBusLine(int BusLineIndex);
        string GetArea(BO.BusLine bus);
        string GetStationInfo(int LineIndex, int StationNumber, string Info);

        List<BO.BusLine> GetBusLines();
        List<int> GetStationNumbers(BO.BusLine busline);
        List<int> GetStationsOnLineByNumber(int BusLineIndex);
        bool OnLine(int LineIndex, int StationNumber);
        List<BO.Station> GetStationsInArea(string Area);
        string GetFinalStationName(int BusLineIndex);
        List<BO.LineTiming> GetAllLinesToShow(BO.Station ChoosenStation, TimeSpan CurrentTime);
        
        #endregion
    }
}