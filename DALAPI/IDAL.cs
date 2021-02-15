using DO;
using System.Collections.Generic;

namespace DALAPI
{
    public interface IDAL //CRUD - רק הצהרות על הפונק
        //לכל מחלקה יש CRUD משלה
    {
        int GetConfig(string Config);

        #region Bus CRUD

        bool AddBus(DO.Bus bus); //CREATE
        DO.Bus ReadBus(string LicenseNumber); //READ 
        bool UpdateBus(DO.Bus bus, string licenseN); //UPDATE
        bool DeleteBus(DO.Bus bus); //DELETE

        List<DO.Bus> GetBusses();
        //List<string> GetLicenseNumbers();

        #endregion

        #region Station CRUD 
        bool AddStation(DO.Station station); //CREATE
        DO.Station ReadStation(int StationNumber); //READ 
        bool UpdateStation(DO.Station station, int OldStationNumber); //UPDATE
        bool DeleteStation(DO.Station station); //DELETE

        List<DO.Station> GetStations();

        #endregion

        #region BusLine CRUD 
        bool AddBusLine(DO.BusLine busLine); //CREATE
        DO.BusLine ReadBusLine(int busLineIndex); //READ 
        bool UpdateBusLine(DO.BusLine busLine); //UPDATE
        bool DeleteBusLine(DO.BusLine busLine); //DELETE

        List<DO.BusLine> GetBusLines();

        #endregion

        #region LineStation CRUD

        bool AddLineStation(DO.LineStation lineStation); //CREATE
        DO.LineStation ReadLineStation(int busLineIndex, int stationNumber); //READ 
        bool UpdateLineStation(DO.LineStation lineStation); //UPDATE
        bool DeleteLineStation(DO.LineStation lineStation); //DELETE

        List<DO.LineStation> GetLineStations();

        #endregion

        #region DrivingBus CRUD 
        bool AddDrivingBus(DO.DrivingBus drivingBus); //CREATE
        DO.DrivingBus ReadDrivingBus(int drivingBusIndex); //READ 
        bool UpdateDrivingBus(DO.DrivingBus drivingBus); //UPDATE
        bool DeleteDrivingBus(DO.DrivingBus drivingBus); //DELETE

        List<DO.DrivingBus> GetDrivingBuses();

        #endregion

        #region LeavingLine CRUD

        bool AddLeavingLine(DO.LeavingLine leavingLine); //CREATE
        DO.LeavingLine ReadLeavingLine(int BusLineIndex); //READ 
        bool UpdateLeavingLine(DO.LeavingLine leavingLine); //UPDATE
        bool DeleteLeavingLine(DO.LeavingLine leavingLine); //DELETE

        List<DO.LeavingLine> GetLeavingLines();

        #endregion

        #region ConsecutiveStations CRUD

        bool AddConsecutiveStations(DO.ConsecutiveStations consecutiveStations); //CREATE
        DO.ConsecutiveStations ReadConsecutiveStations(int ConsecutiveStationsIndex); //READ 
        bool UpdateConsecutiveStations(DO.ConsecutiveStations consecutiveStations); //UPDATE
        bool DeleteConsecutiveStations(DO.ConsecutiveStations consecutiveStations); //DELETE

        List<DO.ConsecutiveStations> GetConsecutiveStations();

        #endregion

        #region User CRUD

        bool AddUser(DO.User user); //CREATE
        DO.User ReadUser(string UserName, string Password); //READ 
        bool UpdateUser(DO.User user); //UPDATE
        bool DeleteUser(DO.User user); //DELETE

        List<DO.User> GetUsers();

        #endregion

        #region UserDrive CRUD

        bool AddUserDrive(DO.UserDrive userDrive); //CREATE
        DO.UserDrive ReadUserDrive(int DriveIndex); //READ 
        bool UpdateUserDrive(DO.UserDrive userDrive); //UPDATE
        bool DeleteUserDrive(DO.UserDrive userDrive); //DELETE

        List<DO.UserDrive> GetUserDrives();

        #endregion
    }
}
