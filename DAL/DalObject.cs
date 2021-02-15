using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using DALAPI;
using DO;
using DS;


namespace DAL
{
    public sealed class DalObject : IDAL
    {
        #region Singleton Implementaion

        static readonly DalObject instance = new DalObject();
        static DalObject() { } // static ctor to ensure instance init is done just before first usage
        DalObject() { } // default => private
        public static DalObject Instance { get => instance; } // The public Instance property to use

        #endregion

        #region IDAL implementations

        #region GetConfig

        public int GetConfig(string Config)
        {
            if (Config == "BusLineIndex")
                return DS.Config.BusLineIndex;

            if (Config == "DrivingBusIndex")
                return DS.Config.DrivingBusIndex;

            if (Config == "DriveIndex")
                return DS.Config.DriveIndex;

            if (Config == "LineInUse")
                return DS.Config.LineInUse;

            if (Config == "ConsecutiveStationsIndex")
                return DS.Config.ConsecutiveStationsIndex;

            if (Config == "LeavingLineIndex")
                return DS.Config.LeavingLineIndex;

            if (Config == "LineTimingIndex")
                return DS.Config.LineTimingIndex;

            return 1;
        }

        #endregion

        #region Bus
        public bool AddBus(DO.Bus bus)
        {
            if (DataSource.Buses.Exists(item => item.LicenseNumber == bus.LicenseNumber)) //looks for a bus in the Buses list with the same license number as the sent bus
            {
                throw new BusException("License number exists already.\nCan't add bus.");
            }

            DataSource.Buses.Add(bus.Clone()); //if this is a new bus- adds a copy of it to the Buses list
            return true;
        }

        public DO.Bus ReadBus(string LicenseNumber)
        {
            DO.Bus result;
            result = DS.DataSource.Buses.FirstOrDefault(item => item.LicenseNumber == LicenseNumber); //returns the first element that matches the conditions 
            if (result != null)
            {
                return result.Clone();
            }
            return null;
        }

        public bool UpdateBus(DO.Bus bus, string licenseOld)
        {
            if (DataSource.Buses.Exists(item => item.LicenseNumber == bus.LicenseNumber) && !(licenseOld == bus.LicenseNumber)) //if a bus with this number exists already
                throw new BusException("A bus with this license number exists already.\nCan't update bus.");

            else
            {
                if (DataSource.Buses.Exists(item => item.LicenseNumber == licenseOld)) //if couldn't find the station in the system - can't delete it
                {
                    DataSource.Buses.RemoveAll(item => item.LicenseNumber == licenseOld); //using RemoveAll and not just Remove because of the condition
                    DataSource.Buses.Add(bus.Clone());
                    return true;
                }

                throw new BusException("You're trying to update a bus that doesn't exists.");
            }
        }

        public bool DeleteBus(DO.Bus bus)
        {
            if (!DS.DataSource.Buses.Exists(item => item.LicenseNumber == bus.LicenseNumber)) //if couldn't find the bus in the system - can't delete it
            {
                throw new BusException("Bus doesn't exists.\nCan't delete bus.");
            }

            DS.DataSource.Buses.RemoveAll(item => item.LicenseNumber == bus.LicenseNumber); //using RemoveAll and not just Remove because of the condition
            return true;
        }

        public List<DO.Bus> GetBusses()
        {
            return (from bus in DS.DataSource.Buses
                    orderby bus.LicenseNumber
                    select bus.Clone()).ToList(); ;
        }

        //public List<string> GetLicenseNumbers()
        //{
        //    //List<string> result = new List<string>();
        //    //foreach (var lisence in DS.DataSource.LicenseNumbers)
        //    //{
        //    //    result.Add(lisence);
        //    //}
        //    //return result;

        //    var l = from lisence in DS.DataSource.LicenseNumbers
        //            select lisence;
        //    return l.ToList();


        //}

        #endregion

        #region Station
        public bool AddStation(DO.Station station)
        {
            if (DataSource.Stations.Exists(item => item.StationNumber == station.StationNumber)) //looks for a station in the Stations list with the same station number as the sent station
            {
                throw new BusException("The station you're trying to add exists already.");
            }

            DataSource.Stations.Add(station.Clone()); //if this is a new station- adds a copy of it to the Stations list
            return true;
        }

        public DO.Station ReadStation(int StationNumber)
        {
            DO.Station stat;
            stat = DS.DataSource.Stations.FirstOrDefault(item => item.StationNumber == StationNumber);
            if (stat != null)
                return stat.Clone();
            throw new BusException("Couldn't find station.");
        }

        public bool UpdateStation(DO.Station station, int OldStationNumber)
        {
            if (DataSource.Stations.Exists(item => item.StationNumber == station.StationNumber) && !(OldStationNumber == station.StationNumber)) //if a station with this number exists already
                throw new BusException("A station with this number exists already.\nCan't update station.");

            else
            {
                if (DataSource.Stations.Exists(item => item.StationNumber == OldStationNumber)) //if couldn't find the station in the system - can't delete it
                {
                    DataSource.Stations.RemoveAll(item => item.StationNumber == OldStationNumber); //using RemoveAll and not just Remove because of the condition
                    DataSource.Stations.Add(station.Clone());

                    foreach (var ls in DataSource.LineStations)
                    {
                        if (ls.StationNumber == OldStationNumber)
                            ls.StationNumber = station.StationNumber;
                    }

                    return true;
                }

                throw new BusException("You're trying to update a station that doesn't exists.");
            }
        }

        public bool DeleteStation(DO.Station station)
        {
            if (DataSource.Stations.Exists(item => item.StationNumber == station.StationNumber)) //if couldn't find the station in the system - can't delete it
            {
                DataSource.Stations.RemoveAll(item => item.StationNumber == station.StationNumber); //using RemoveAll and not just Remove because of the condition

                List<DO.LineStation> LineStations = GetLineStations();
                foreach (var ls in LineStations)
                {
                    if (ls.StationNumber == station.StationNumber)
                        DataSource.LineStations.Remove(ls);
                }

                return true;
            }

            throw new BusException("You're trying to delete a station that doesn't exists.");
        }

        public List<DO.Station> GetStations()
        {
            return (List<Station>)(from station in DataSource.Stations
                                   select station.Clone()).ToList();

        }

        #endregion

        #region BusLine
        public bool AddBusLine(DO.BusLine busLine)
        {
            foreach (var bl in DataSource.BusLines)
            {
                if (bl.LineNumber == busLine.LineNumber)
                {
                    if (bl.FirstStation == busLine.LastStation)
                    {
                        if (bl.LastStation == busLine.FirstStation)
                        {
                            DataSource.BusLines.Add(busLine.Clone()); //if this is a new bus line- adds a copy of it to the BusLines list
                            return true;
                        }

                        else throw new DO.BusException("A bus line with this number exists already and\nseems like your new bus line isn't it's reversed line.");

                    }

                    else throw new DO.BusException("A bus line with this number exists already and\nseems like your new bus line isn't it's reversed line.");
                }
            }

            //gets here if didn't find any matching line with the same line number
            DataSource.BusLines.Add(busLine.Clone()); //if this is a new bus line- adds a copy of it to the BusLines list
            return true;
        }

        public DO.BusLine ReadBusLine(int busLineIndex)
        {
            BusLine result;
            result = DS.DataSource.BusLines.FirstOrDefault(item => item.BusLineIndex == busLineIndex); //returns the first element that matches the conditions 
            if (result != null)
            {
                return result.Clone();
            }
            throw new BusException("Can't find bus line in the system.");
        }

        public bool UpdateBusLine(DO.BusLine busLine)
        {
            if (!DataSource.BusLines.Exists(item => item.BusLineIndex == busLine.BusLineIndex)) //if couldn't find the bus line in the system - can't delete it
            {
                throw new BusException("Bus line doesn't exists.\nCan't update bus line.");
            }

            DataSource.BusLines.RemoveAll(item => item.BusLineIndex == busLine.BusLineIndex); //using RemoveAll and not just Remove because of the condition
            DataSource.BusLines.Add(busLine.Clone());
            return true;
        }

        public bool DeleteBusLine(DO.BusLine busLine)
        {
            if (!DataSource.BusLines.Exists(item => item.BusLineIndex == busLine.BusLineIndex)) //if couldn't find the bus line in the system - can't delete it
            {
                throw new BusException("Bus line doesn't exists.\nCan't delete bus line.");
            }

            DataSource.BusLines.RemoveAll(item => item.BusLineIndex == busLine.BusLineIndex); //using RemoveAll and not just Remove because of the condition
            return true;
        }

        public List<DO.BusLine> GetBusLines()
        {
            List<DO.BusLine> result = new List<DO.BusLine>();
            foreach (var busLine in DS.DataSource.BusLines)
            {
                result.Add(busLine.Clone());
            }
            return result;
        }

        #endregion

        #region LineStation
        public bool AddLineStation(DO.LineStation lineStation)
        {
            if (DataSource.LineStations.Exists(item => (item.BusLineIndex == lineStation.BusLineIndex) && (item.StationNumber == lineStation.StationNumber))) //looks for a line-station in the LineStations list with the same index and stationNumber as the sent bus
            {
                throw new BusException("A station with this number is already on this line.\nCan't add line station.");
            }

            DataSource.LineStations.Add(lineStation.Clone()); //if this is a new line-station- adds a copy of it to the Buses list
            return true;
        }

        public DO.LineStation ReadLineStation(int busLineIndex, int stationNumber)
        {
            DO.LineStation result;
            result = DS.DataSource.LineStations.FirstOrDefault(item => (item.BusLineIndex == busLineIndex) && (item.StationNumber == stationNumber)); //returns the first element that matches the conditions 
            if (result != null)
            {
                return result.Clone();
            }

            throw new BusException("Can't find line-station.");
        }

        public bool UpdateLineStation(DO.LineStation lineStation)
        {
            if (DataSource.LineStations.Exists(item => (item.BusLineIndex == lineStation.BusLineIndex) && (item.StationNumber == lineStation.StationNumber))) //if couldn't find the line-station in the system - can't delete it
            {
                DataSource.LineStations.RemoveAll(item => (item.BusLineIndex == lineStation.BusLineIndex) && (item.StationNumber == lineStation.StationNumber)); //using RemoveAll and not just Remove because of the condition
                DataSource.LineStations.Add(lineStation.Clone());
                return true;
            }

            throw new BusException("Line-station doesn't exists.\nCan't update line station.");
        }

        public bool DeleteLineStation(DO.LineStation lineStation)
        {
            if (!DS.DataSource.LineStations.Exists(item => (item.BusLineIndex == lineStation.BusLineIndex) && (item.StationNumber == lineStation.StationNumber))) //if couldn't find the line-station in the system - can't delete it
            {
                throw new BusException("Line-station doesn't exists.\nCan't delete line station.");
            }

            DS.DataSource.LineStations.RemoveAll(item => (item.BusLineIndex == lineStation.BusLineIndex) && (item.StationNumber == lineStation.StationNumber)); //using RemoveAll and not just Remove because of the condition
            return true;
        }

        public List<DO.LineStation> GetLineStations()
        {
            List<DO.LineStation> result = new List<DO.LineStation>();
            foreach (var lineStation in DS.DataSource.LineStations)
            {
                result.Add(lineStation.Clone());
            }
            return result;
        }

        #endregion

        #region DrivingBus
        public bool AddDrivingBus(DO.DrivingBus drivingbus)
        {
            if (DataSource.DrivingBuses.Exists(item => item.DrivingBusIndex == drivingbus.DrivingBusIndex)) //looks for a Driving-bus in the DrivingBuses list with the same DrivingBusIndex as the sent bus
            {
                throw new BusException("Driving-bus exists already.\nCan't add driving-bus.");
            }

            DataSource.DrivingBuses.Add(drivingbus.Clone()); //if this is a new driving-bus- adds a copy of it to the DrivingBuses list
            return true;
        }

        public DO.DrivingBus ReadDrivingBus(int DrivingBusIndex)
        {
            DO.DrivingBus result;
            result = DS.DataSource.DrivingBuses.FirstOrDefault(item => item.DrivingBusIndex == DrivingBusIndex); //returns the first element that matches the conditions 
            if (result != null)
            {
                return result.Clone();
            }

            throw new BusException("Can't find driving-bus.");
        }

        public bool UpdateDrivingBus(DO.DrivingBus drivingbus)
        {
            if (!DataSource.DrivingBuses.Exists(item => item.DrivingBusIndex == drivingbus.DrivingBusIndex)) //if couldn't find the driving-bus in the system - can't delete it
            {
                throw new BusException("Driving-bus doesn't exists.\nCan't update driving-bus.");
            }

            DataSource.DrivingBuses.RemoveAll(item => item.DrivingBusIndex == drivingbus.DrivingBusIndex); //using RemoveAll and not just Remove because of the condition
            DataSource.DrivingBuses.Add(drivingbus.Clone());
            return true;
        }

        public bool DeleteDrivingBus(DO.DrivingBus drivingbus)
        {
            if (!DS.DataSource.DrivingBuses.Exists(item => item.DrivingBusIndex == drivingbus.DrivingBusIndex)) //if couldn't find the driving-bus in the system - can't delete it
            {
                throw new BusException("Driving-bus doesn't exists.\nCan't delete driving-bus.");
            }

            DS.DataSource.DrivingBuses.RemoveAll(item => item.DrivingBusIndex == drivingbus.DrivingBusIndex); //using RemoveAll and not just Remove because of the condition
            return true;
        }

        public List<DO.DrivingBus> GetDrivingBuses()
        {
            List<DO.DrivingBus> result = new List<DO.DrivingBus>();
            foreach (var drivingbus in DS.DataSource.DrivingBuses)
            {
                result.Add(drivingbus.Clone());
            }
            return result;
        }

        #endregion

        #region LeavingLine
        public bool AddLeavingLine(DO.LeavingLine leavingLine)
        {
            if (DataSource.LeavingLines.Exists(item => item.BusLineIndex == leavingLine.BusLineIndex)) //looks for a leaving-line in the Buses list with the same BusLineIndex as the sent bus
            {
                throw new BusException("Leaving-line exists already.\nCan't add leaving-line.");
            }

            DataSource.LeavingLines.Add(leavingLine.Clone()); //if this is a new leaving-line- adds a copy of it to the Buses list
            return true;
        }

        public DO.LeavingLine ReadLeavingLine(int BusLineIndex)
        {
            DO.LeavingLine result;
            result = DS.DataSource.LeavingLines.FirstOrDefault(item => item.BusLineIndex == BusLineIndex); //returns the first element that matches the conditions 
            if (result != null)
            {
                return result.Clone();
            }

            throw new BusException("Can't find leaving-line.");
        }

        public bool UpdateLeavingLine(DO.LeavingLine leavingLine)
        {
            if (!DataSource.LeavingLines.Exists(item => item.BusLineIndex == leavingLine.BusLineIndex)) //if couldn't find the leaving-line in the system - can't delete it
            {
                throw new BusException("Leaving-line doesn't exists.\nCan't update leaving-line.");
            }

            DataSource.LeavingLines.RemoveAll(item => item.BusLineIndex == leavingLine.BusLineIndex); //using RemoveAll and not just Remove because of the condition
            DataSource.LeavingLines.Add(leavingLine.Clone());
            return true;
        }

        public bool DeleteLeavingLine(DO.LeavingLine leavingLine)
        {
            if (!DS.DataSource.LeavingLines.Exists(item => item.BusLineIndex == leavingLine.BusLineIndex)) //if couldn't find the leaving-line in the system - can't delete it
            {
                throw new BusException("Leaving-line doesn't exists.\nCan't delete leaving-line.");
            }

            DS.DataSource.LeavingLines.RemoveAll(item => item.BusLineIndex == leavingLine.BusLineIndex); //using RemoveAll and not just Remove because of the condition
            return true;
        }

        public List<DO.LeavingLine> GetLeavingLines()
        {
            List<DO.LeavingLine> result = new List<DO.LeavingLine>();
            foreach (var leavingLine in DS.DataSource.LeavingLines)
            {
                result.Add(leavingLine.Clone());
            }
            return result;
        }

        #endregion

        #region ConsecutiveStations
        public bool AddConsecutiveStations(DO.ConsecutiveStations consecutiveStations)
        {
            if (DataSource.ConsecutiveStations.Exists(item => (item.ConsecutiveStationsIndex == consecutiveStations.ConsecutiveStationsIndex))) //looks for a pair of consecutive stations in the ConsecutiveStations list with the same index as the sent Consecutive-stations
            {
                throw new BusException("Consecutive-stations exists already.\nCan't add consecutive-stations.");
            }

            DataSource.ConsecutiveStations.Add(consecutiveStations.Clone()); //if this is a new pair of consecutive stations- adds a copy of it to the Buses list
            return true;
        }

        public DO.ConsecutiveStations ReadConsecutiveStations(int ConsecutiveStationsIndex)
        {
            DO.ConsecutiveStations result;
            result = DS.DataSource.ConsecutiveStations.FirstOrDefault(item => (item.ConsecutiveStationsIndex == ConsecutiveStationsIndex)); //returns the first element that matches the conditions 
            if (result != null)
            {
                return result.Clone();
            }

            throw new BusException("Can't find consecutive stations.");
        }

        public bool UpdateConsecutiveStations(DO.ConsecutiveStations consecutiveStations)
        {
            if (!DataSource.ConsecutiveStations.Exists(item => (item.ConsecutiveStationsIndex == consecutiveStations.ConsecutiveStationsIndex))) //if couldn't find the consecutive stations in the system - can't delete it
            {
                throw new BusException("Consecutive-stations doesn't exists.\nCan't update consecutive-stations.");
            }

            DataSource.ConsecutiveStations.RemoveAll(item => (item.ConsecutiveStationsIndex == consecutiveStations.ConsecutiveStationsIndex)); //using RemoveAll and not just Remove because of the condition
            DataSource.ConsecutiveStations.Add(consecutiveStations.Clone());
            return true;
        }

        public bool DeleteConsecutiveStations(DO.ConsecutiveStations consecutiveStations)
        {
            if (!DS.DataSource.ConsecutiveStations.Exists(item => (item.ConsecutiveStationsIndex == consecutiveStations.ConsecutiveStationsIndex))) //if couldn't find the consecutive-stations in the system - can't delete it
            {
                throw new BusException("Consecutive-stations doesn't exists.\nCan't delete consecutive-stations.");
            }

            DS.DataSource.ConsecutiveStations.RemoveAll(item => (item.ConsecutiveStationsIndex == consecutiveStations.ConsecutiveStationsIndex)); //using RemoveAll and not just Remove because of the condition
            return true;
        }

        public List<DO.ConsecutiveStations> GetConsecutiveStations()
        {
            List<DO.ConsecutiveStations> result = new List<DO.ConsecutiveStations>();
            foreach (var consecutiveStations in DS.DataSource.ConsecutiveStations)
            {
                result.Add(consecutiveStations.Clone());
            }
            return result;
        }

        #endregion

        #region User
        public bool AddUser(DO.User user)
        {
            if (DataSource.Users.Exists(item => item.UserName == user.UserName && item.Password == user.Password && item.Permission == user.Permission)) //looks for a user in the Users list with the same userName as the sent user
            {
                throw new BusException("User exists already.\nCan't add user.");
            }

            DataSource.Users.Add(user.Clone()); //if this is a new user- adds a copy of it to the Users list
            return true;
        }

        public DO.User ReadUser(string UserName, string Password)
        {
            DO.User result;
            result = DS.DataSource.Users.FirstOrDefault(item => item.UserName == UserName && item.Password == Password); //returns the first element that matches the conditions 

            if (result != null)
            {
                return result.Clone();
            }

            throw new BusException("Can't find user in the system.");
        }

        public bool UpdateUser(DO.User user)
        {
            if (!DataSource.Users.Exists(item => item.UserName == user.UserName)) //if couldn't find the user in the system - can't delete it
            {
                throw new BusException("User doesn't exists.\nCan't update user.");
            }

            DataSource.Users.RemoveAll(item => item.UserName == user.UserName); //using RemoveAll and not just Remove because of the condition
            DataSource.Users.Add(user.Clone());
            return true;
        }

        public bool DeleteUser(DO.User user)
        {
            if (!DS.DataSource.Users.Exists(item => item.UserName == user.UserName)) //if couldn't find the user in the system - can't delete it
            {
                throw new BusException("User doesn't exists.\nCan't delete user.");
            }

            DS.DataSource.Users.RemoveAll(item => item.UserName == user.UserName); //using RemoveAll and not just Remove because of the condition
            return true;
        }

        public List<DO.User> GetUsers()
        {
            List<DO.User> result = new List<DO.User>();
            foreach (var user in DS.DataSource.Users)
            {
                result.Add(user.Clone());
            }
            return result;
        }

        #endregion

        #region UserDrive
        public bool AddUserDrive(DO.UserDrive userDrive)
        {
            if (DataSource.UserDrives.Exists(item => item.DriveIndex == userDrive.DriveIndex)) //looks for a bus in the UserDrives list with the same DriveIndex as the sent bus
            {
                throw new BusException("User-drive exists already.\nCan't add user-drive.");
            }

            DataSource.UserDrives.Add(userDrive.Clone()); //if this is a new User-drive- adds a copy of it to the UserDrives list
            return true;
        }

        public DO.UserDrive ReadUserDrive(int DriveIndex)
        {
            DO.UserDrive result;
            result = DS.DataSource.UserDrives.FirstOrDefault(item => item.DriveIndex == DriveIndex); //returns the first element that matches the conditions 
            if (result != null)
            {
                return result.Clone();
            }

            throw new BusException("Can't find user-drive.");
        }

        public bool UpdateUserDrive(DO.UserDrive userDrive)
        {
            if (!DataSource.UserDrives.Exists(item => item.DriveIndex == userDrive.DriveIndex)) //if couldn't find the user-drive in the system - can't delete it
            {
                throw new BusException("User-drive doesn't exists.\nCan't update user-drive.");
            }

            DataSource.UserDrives.RemoveAll(item => item.DriveIndex == userDrive.DriveIndex); //using RemoveAll and not just Remove because of the condition
            DataSource.UserDrives.Add(userDrive.Clone());
            return true;
        }

        public bool DeleteUserDrive(DO.UserDrive userDrive)
        {
            if (!DS.DataSource.UserDrives.Exists(item => item.DriveIndex == userDrive.DriveIndex)) //if couldn't find the user-drives in the system - can't delete it
            {
                throw new BusException("User-drives doesn't exists.\nCan't delete user-drive.");
            }

            DS.DataSource.UserDrives.RemoveAll(item => item.DriveIndex == userDrive.DriveIndex); //using RemoveAll and not just Remove because of the condition
            return true;
        }

        public List<DO.UserDrive> GetUserDrives()
        {
            List<DO.UserDrive> result = new List<DO.UserDrive>();
            foreach (var userDrive in DS.DataSource.UserDrives)
            {
                result.Add(userDrive.Clone());
            }
            return result;
        }

        #endregion

        #endregion 
    }
}