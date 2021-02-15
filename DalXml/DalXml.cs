using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DALAPI;
using DalXml;
using DO;

namespace DAL
{
    public sealed class DalXml : IDAL
    {

        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        DalXml() { } // default => private
        public static DalXml Instance { get => instance; }// The public Instance property to use

        #endregion

        #region DS XML Files

        static string LeavingLinesPath = @"LeavingLines.xml"; //XElement
        string ConsecutiveStationsPath = @"ConsecutiveStations.xml"; //XElement


        string BusPath = @"Buses.xml"; //XMLSerializer
        string BusLinePath = @"BusLines.xml"; //XMLSerializer
        string DrivingBusPath = @"DrivingBus.xml"; //XMLSerializer
        string LineStationPath = @"LineStations.xml"; //XMLSerializer
        string StationsPath = @"Stations.xml"; //XMLSerializer
        string UserPath = @"Users.xml"; //XMLSerializer
        string UserDrivePath = @"UserDrives.xml"; //XMLSerializer

        //XElement LeavingLineRoot;
        //XElement ConsecutiveStationsRoot;
        #endregion

       
        #region GetConfig

        public int GetConfig(string Config)
        {
            if (Config == "BusLineIndex")
                return ConfigXml.BusLineIndex;

            if (Config == "DrivingBusIndex")
                return ConfigXml.DrivingBusIndex;

            if (Config == "DriveIndex")
                return ConfigXml.DriveIndex;

            if (Config == "LineInUse")
                return ConfigXml.LineInUse;

            if (Config == "ConsecutiveStationsIndex")
                return ConfigXml.ConsecutiveStationsIndex;

            if (Config == "LeavingLineIndex")
                return ConfigXml.LeavingLineIndex;

            if (Config == "LineTimingIndex")
                return ConfigXml.LineTimingIndex;

            return 1;
        }

        #endregion

        #region Bus
        public bool AddBus(DO.Bus bus)
        {
            List<Bus> ListBuses = XmlTools.LoadListFromXMLSerializer<Bus>(BusPath);
           
            if (ListBuses.FirstOrDefault(b => b.LicenseNumber ==bus.LicenseNumber) != null)
                throw new DO.BusException("License number exists already.\nCan't add bus.");
        
            ListBuses.Add(bus);
            return XmlTools.SaveListToXMLSerializer(ListBuses, BusPath);
           
        }

        public bool DeleteBus(Bus bus)
        {
            List<Bus> ListBuses = XmlTools.LoadListFromXMLSerializer<Bus>(BusPath);
            Bus b = ListBuses.Find(BUS => BUS.LicenseNumber == bus.LicenseNumber);
            if (b!= null)
            {
                ListBuses.Remove(b);
                return XmlTools.SaveListToXMLSerializer(ListBuses, BusPath);
            }
            else
                throw new DO.BusException("Missing License number. \nC can't delete this bus. ");


        }

        public Bus ReadBus(string LicenseNumber)
        {
            List<Bus> ListBuses = XmlTools.LoadListFromXMLSerializer<Bus>(BusPath);
            Bus b = ListBuses.Find(BUS => BUS.LicenseNumber == LicenseNumber);
            if (b != null)
                return b;
            else 
                throw new DO.BusException("License number doesn't exist.\nCan't return this bus.");

        }

        public bool UpdateBus(Bus bus, string licenseN)
        {
            List<Bus> ListBuses = XmlTools.LoadListFromXMLSerializer<Bus>(BusPath);
            Bus b = ListBuses.Find(BUS => BUS.LicenseNumber == licenseN);
            if (b != null)
            {
                ListBuses.Remove(b);
                ListBuses.Add(bus);
                return XmlTools.SaveListToXMLSerializer(ListBuses, BusPath);
            }
            else 
                throw new DO.BusException("License number doesn't exist.\nCan't update this bus.");

        }

        public List<Bus> GetBusses()
        {
            return (from bus in XmlTools.GetBusses()
                    orderby bus.LicenseNumber
                    select bus.Clone()).ToList();
           
        }

  
        #endregion

        #region BusLine

        public bool AddBusLine(BusLine busLine)
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(BusLinePath);
           
            if (ListBusLines.FirstOrDefault(b => b.LineNumber == busLine.LineNumber) != null)
                throw new DO.BusException("Bus line index exists already.\nCan't add bus line.");
           
            ListBusLines.Add(busLine.Clone());
            return XmlTools.SaveListToXMLSerializer(ListBusLines, BusLinePath);

        }

        public bool DeleteBusLine(BusLine busLine)
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(BusLinePath);

            BusLine b = ListBusLines.Find(BUS => BUS.BusLineIndex == busLine.BusLineIndex);

            if (b != null)
            {
                ListBusLines.Remove(b);
                return XmlTools.SaveListToXMLSerializer(ListBusLines, BusLinePath);
            }
            else
                throw new DO.BusException("Missing bus line index. \nC can't delete this bus. ");
        }

        public List<BusLine> GetBusLines()
        {
            return (from busLine in XmlTools.GetBusLines()
                    orderby busLine.LineNumber
                    select busLine.Clone()).ToList();
        }

        public BusLine ReadBusLine(int busLineIndex)
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(BusLinePath);

            BusLine b = ListBusLines.Find(BUS => BUS.BusLineIndex == busLineIndex);
            if (b != null)
                return b;
            else
                throw new DO.BusException("Bus Line Index doesn't exist.\nCan't return this bus line.");
        }

        public bool UpdateBusLine(BusLine busLine)
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(BusLinePath);

            BusLine b = ListBusLines.Find(BUS => BUS.BusLineIndex == busLine.BusLineIndex);
            if (b != null)
            {
                ListBusLines.Remove(b);
                ListBusLines.Add(busLine.Clone());
                return XmlTools.SaveListToXMLSerializer(ListBusLines, BusLinePath);
            }
            else
                throw new DO.BusException("Bus Line Index doesn't exist.\nCan't return this bus line.");

        }


        #endregion

        #region ConsecutiveStations

        public bool AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            XElement ConsecutiveStationsRoot = XmlTools.LoadListFromXMLElement(ConsecutiveStationsPath);
           
            XElement coStat =(from p in ConsecutiveStationsRoot.Elements()
                             where int.Parse(p.Element("ConsecutiveStationsIndex").Value) == consecutiveStations.ConsecutiveStationsIndex
                             select p).FirstOrDefault();
           
            if (coStat != null)
                throw new DO.BadIndexException(consecutiveStations.ConsecutiveStationsIndex, "Duplicate consecutive Stations index");
            string time = "PT" + consecutiveStations.AvgTravelTime.Minutes + "M";
            XElement constationsElem = new XElement("ConsecutiveStations",
                                        new XElement("BusLineIndex", consecutiveStations.BusLineIndex.ToString()),
                                        new XElement("ConsecutiveStationsIndex", consecutiveStations.ConsecutiveStationsIndex.ToString()),
                                        new XElement("FirstStation", consecutiveStations.FirstStation.ToString()),
                                        new XElement("SecondStation", consecutiveStations.SecondStation.ToString()),
                                        new XElement("Distance", consecutiveStations.Distance.ToString()),
                                        new XElement("AvgTravelTime", time));


            ConsecutiveStationsRoot.Add(constationsElem);
            return XmlTools.SaveListToXMLElement(ConsecutiveStationsRoot, ConsecutiveStationsPath);

        }

        public bool DeleteConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            XElement ConsecutiveStationsRoot = XmlTools.LoadListFromXMLElement(ConsecutiveStationsPath);
            
            XElement coStat = (from s in ConsecutiveStationsRoot.Elements()
                               where int.Parse(s.Element("ConsecutiveStationsIndex").Value) == consecutiveStations.ConsecutiveStationsIndex
                               select s).FirstOrDefault();
            if (coStat != null)
            {
                coStat.Remove();
                return XmlTools.SaveListToXMLElement(ConsecutiveStationsRoot, ConsecutiveStationsPath);
            }

            else
                throw new DO.BadIndexException(consecutiveStations.ConsecutiveStationsIndex, $"bad Consecutive Stations Index: {consecutiveStations.ConsecutiveStationsIndex}");

        }

        public List<ConsecutiveStations> GetConsecutiveStations()
        {
            XElement CoStationsRoot = XmlTools.LoadListFromXMLElement(ConsecutiveStationsPath);
            List<ConsecutiveStations> stations;
            //try
            //{
                stations = (from p in CoStationsRoot.Elements()
                            select new ConsecutiveStations()
                            {
                                BusLineIndex = Int32.Parse(p.Element("BusLineIndex").Value),
                                ConsecutiveStationsIndex = Int32.Parse(p.Element("ConsecutiveStationsIndex").Value),
                                FirstStation = Int32.Parse(p.Element("FirstStation").Value),
                                SecondStation = Int32.Parse(p.Element("SecondStation").Value),
                                Distance = Int32.Parse(p.Element("Distance").Value),
                                AvgTravelTime = XmlConvert.ToTimeSpan(p.Element("AvgTravelTime").Value)
                            }).ToList();
                        
            //}
            //catch
            //{
            //    stations = null;
            //}
            return stations;

        }

        public ConsecutiveStations ReadConsecutiveStations(int ConsecutiveStationsIndex)
        {
            XElement CoStationsRoot = XmlTools.LoadListFromXMLElement(ConsecutiveStationsPath);
            ConsecutiveStations coStat = (from s in CoStationsRoot.Elements()
                                          where int.Parse(s.Element("ConsecutiveStationsIndex").Value) == ConsecutiveStationsIndex
                                          select new ConsecutiveStations()
                                          {
                                              BusLineIndex = Int32.Parse(s.Element("BusLineIndex").Value),
                                              ConsecutiveStationsIndex = Int32.Parse(s.Element("ConsecutiveStationsIndex").Value),
                                              FirstStation = Int32.Parse(s.Element("FirstStation").Value),
                                              SecondStation = Int32.Parse(s.Element("SecondStation").Value),
                                              Distance = Int32.Parse(s.Element("Distance").Value),
                                              AvgTravelTime = XmlConvert.ToTimeSpan(s.Element("AvgTravelTime").Value)
                                          }
                                         ).FirstOrDefault();

            if (coStat == null)
                throw new DO.BadIndexException(ConsecutiveStationsIndex, $"bad Consecutive Stations Index: {ConsecutiveStationsIndex}");

            return coStat;
        }

        public bool UpdateConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            string time = consecutiveStations.AvgTravelTime.ToString();
            string updatedTime = "PT" + consecutiveStations.AvgTravelTime.Minutes + "M";
            XElement ConsecutiveStationsRoot = XmlTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement coStat = (from s in ConsecutiveStationsRoot.Elements()
                               where int.Parse(s.Element("ConsecutiveStationsIndex").Value) == consecutiveStations.ConsecutiveStationsIndex
                               select s).FirstOrDefault();

            if (coStat != null)
            {
                coStat.Element("ConsecutiveStationsIndex").Value = consecutiveStations.ConsecutiveStationsIndex.ToString();
                coStat.Element("FirstStation").Value = consecutiveStations.FirstStation.ToString();
                coStat.Element("SecondStation").Value = consecutiveStations.SecondStation.ToString();
                coStat.Element("Distance").Value = consecutiveStations.Distance.ToString();
                coStat.Element("AvgTravelTime").Value = updatedTime;

                return XmlTools.SaveListToXMLElement(ConsecutiveStationsRoot, ConsecutiveStationsPath);

            }
            else 
                throw new DO.BadIndexException(consecutiveStations.ConsecutiveStationsIndex, $"bad Consecutive Stations Index: {consecutiveStations.ConsecutiveStationsIndex}");

        }

        #endregion

        #region DrivingBus

        public bool AddDrivingBus(DrivingBus drivingBus)
        {
            List<DrivingBus> ListDrivingBus = XmlTools.LoadListFromXMLSerializer<DrivingBus>(DrivingBusPath);

            if (ListDrivingBus.FirstOrDefault(b => b.DrivingBusIndex == drivingBus.DrivingBusIndex) != null)
                throw new DO.BusException("Driving Bus index exists already.\nCan't add driving bus.");

            ListDrivingBus.Add(drivingBus);

            return XmlTools.SaveListToXMLSerializer(ListDrivingBus, DrivingBusPath);
        }

        public bool DeleteDrivingBus(DrivingBus drivingBus)
        {
            List<DrivingBus> ListDrivingBus = XmlTools.LoadListFromXMLSerializer<DrivingBus>(DrivingBusPath);

            DrivingBus b = ListDrivingBus.Find(BUS => BUS.DrivingBusIndex == drivingBus.DrivingBusIndex);

            if (b != null)
            {
                ListDrivingBus.Remove(b);
                return XmlTools.SaveListToXMLSerializer(ListDrivingBus, DrivingBusPath);
            }
            else
                throw new DO.BusException("Missing driving bus index.\nC can't delete this bus drive.");
        }

        public List<DrivingBus> GetDrivingBuses()
        {
            return (from drivingBus in XmlTools.GetDrivingBuses()
                    orderby drivingBus.DrivingBusIndex
                    select drivingBus.Clone()).ToList();
        }

        public DrivingBus ReadDrivingBus(int drivingBusIndex)
        {
            List<DrivingBus> ListDrivingBus = XmlTools.LoadListFromXMLSerializer<DrivingBus>(DrivingBusPath);

            DrivingBus b = ListDrivingBus.Find(BUS => BUS.DrivingBusIndex == drivingBusIndex);
            if (b != null)
                return b;
            else
                throw new DO.BusException("Driving bus index doesn't exist.\nCan't return this drive bus.");
        }

        public bool UpdateDrivingBus(DrivingBus drivingBus)
        {
            List<DrivingBus> ListDrivingBus = XmlTools.LoadListFromXMLSerializer<DrivingBus>(DrivingBusPath);

            DrivingBus b = ListDrivingBus.Find(BUS => BUS.DrivingBusIndex == drivingBus.DrivingBusIndex);
            if (b != null)
            {
                ListDrivingBus.Remove(b);
                ListDrivingBus.Add(drivingBus.Clone());
                return XmlTools.SaveListToXMLSerializer(ListDrivingBus, DrivingBusPath);
            }
            else
                throw new DO.BusException("Driving bus index doesn't exist.\nCan't return this driving bus.");
        }

        #endregion

        #region LeavingLine

        public bool AddLeavingLine(LeavingLine leavingLine)
        {
            XElement LeavingLineRoot = XmlTools.LoadListFromXMLElement(LeavingLinesPath);

            XElement line = (from p in LeavingLineRoot.Elements()
                             where int.Parse(p.Element("BusLineIndex").Value) == leavingLine.BusLineIndex
                             select p).FirstOrDefault();

            if (line != null)
                throw new DO.BadIndexException(leavingLine.BusLineIndex, "Duplicate index-bus of leaving line");
            string time = "PT" + leavingLine.Start.Hours + "H" + leavingLine.Start.Minutes + "M" + leavingLine.Start.Seconds + "H";

            XElement lineElement = new XElement("LeavingLineIndex", new XElement("LeavingLineIndex", leavingLine.BusLineIndex),
                                  new XElement("BusLineIndex", leavingLine.BusLineIndex.ToString()),
                                  new XElement("Start", time));

            LeavingLineRoot.Add(lineElement);
            return XmlTools.SaveListToXMLElement(lineElement, LeavingLinesPath);

        }

        public bool DeleteLeavingLine(LeavingLine leavingLine)
        {
            XElement LeavingLineRoot = XmlTools.LoadListFromXMLElement(LeavingLinesPath);

            XElement line = (from p in LeavingLineRoot.Elements()
                            where int.Parse(p.Element("BusLineIndex").Value) == leavingLine.BusLineIndex
                            select p).FirstOrDefault();

        if (line != null)
            {
                line.Remove(); //<==>   Remove line 

                return (XmlTools.SaveListToXMLElement(LeavingLineRoot, LeavingLinesPath));
            }
            else
                throw new DO.BadIndexException(leavingLine.BusLineIndex, $"bad index of leaving line: {leavingLine.BusLineIndex}");
    }

        public List<LeavingLine> GetLeavingLines()
        {
            XElement LeavingLineRoot = XmlTools.LoadListFromXMLElement(LeavingLinesPath);

            return (from p in LeavingLineRoot.Elements()
                    select new LeavingLine()
                    {
                        LeavingLineIndex = Int32.Parse(p.Element("LeavingLineIndex").Value),
                        BusLineIndex = Int32.Parse(p.Element("BusLineIndex").Value),
                        Start= XmlConvert.ToTimeSpan(p.Element("Start").Value)
                    }

                   ).ToList();
        }

        public LeavingLine ReadLeavingLine(int BusLineIndex)
        {
            XElement LeavingLineRoot = XmlTools.LoadListFromXMLElement(LeavingLinesPath);

            LeavingLine line = (from p in LeavingLineRoot.Elements()
                                where int.Parse(p.Element("BusLineIndex").Value) == BusLineIndex
                                select new LeavingLine()
                                {
                                    LeavingLineIndex = Int32.Parse(p.Element("LeavingLineIndex").Value),
                                    BusLineIndex = Int32.Parse(p.Element("BusLineIndex").Value),
                                    Start = XmlConvert.ToTimeSpan(p.Element("Start").Value)
                                }
                             ).FirstOrDefault();

            if (line == null)
            {
                throw new DO.BadIndexException(BusLineIndex, $"bad index of leaving line: {BusLineIndex}");

            }
            return line;
        }

        public bool UpdateLeavingLine(LeavingLine leavingLine)
        {
            XElement LeavingLineRoot = XmlTools.LoadListFromXMLElement(LeavingLinesPath);

            XElement line = (from p in LeavingLineRoot.Elements()
                             where int.Parse(p.Element("BusLineIndex").Value) == leavingLine.BusLineIndex
                             select p).FirstOrDefault();
            if (line != null)
            {
                line.Element("LeavingLineIndex").Value = leavingLine.LeavingLineIndex.ToString();
                line.Element("BusLineIndex").Value = leavingLine.BusLineIndex.ToString();
                line.Element("Start").Value = leavingLine.Start.ToString();
                return XmlTools.SaveListToXMLElement(LeavingLineRoot, LeavingLinesPath);
            }
            else
                throw new DO.BadIndexException(leavingLine.BusLineIndex, $"bad index of leaving line: {leavingLine.BusLineIndex}");
        }

        #endregion

        #region LineStation

        public bool AddLineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStations = XmlTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
          
            LineStation line = ListLineStations.Find(item => item.BusLineIndex == lineStation.BusLineIndex && item.StationNumber == lineStation.StationNumber);
            if (line!= null)
                throw new DO.BusException("A station with this number is already on this line.\nCan't add line station.");

            ListLineStations.Add(lineStation.Clone());
            return XmlTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
        }

        public bool DeleteLineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStations = XmlTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            LineStation l = (ListLineStations.FirstOrDefault(b => (b.BusLineIndex == lineStation.BusLineIndex) ^ (b.StationNumber == lineStation.StationNumber)));
            if (l != null)
            {
                ListLineStations.Remove(l);
                return XmlTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
            }
            else
                throw new DO.BusException("Missing line station number. \nC can't delete this line station. ");
        }

        public List<LineStation> GetLineStations()
        {
            return (from lineStation in XmlTools.GetLineStations()
                    orderby lineStation.StationNumber
                    select lineStation.Clone()).ToList();
        }

        public LineStation ReadLineStation(int busLineIndex, int stationNumber)
        {
            List<LineStation> ListLineStations = XmlTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            LineStation l = ListLineStations.FirstOrDefault(b => (b.BusLineIndex == busLineIndex) && (b.StationNumber == stationNumber));
            if (l != null)
                return l.Clone();
            else
                throw new BusException("Can't find line-station.");

        }

        public bool UpdateLineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStations = XmlTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            LineStation l = ListLineStations.FirstOrDefault(b => (b.BusLineIndex == lineStation.BusLineIndex) && (b.StationNumber == lineStation.StationNumber));
            if (l != null)
            {
                ListLineStations.Remove(l);
                ListLineStations.Add(lineStation);
                return XmlTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
            }
            else
                throw new DO.BusException("Missing line station number. \nC can't update this line station. ");
        }

        #endregion

        #region Station

        public bool AddStation(Station station)
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(StationsPath);

            if (ListStations.FirstOrDefault(s => s.StationNumber == station.StationNumber) != null)
                throw new DO.BusException("Station number exists already.\nCan't add bus.");
           
           
            ListStations.Add(station.Clone());
            return XmlTools.SaveListToXMLSerializer(ListStations, StationsPath);

        }

        public bool DeleteStation(Station station)
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(StationsPath);

            Station s = ListStations.Find(stat => stat.StationNumber == station.StationNumber);

            if (s != null)
            {
                ListStations.Remove(s);
                return XmlTools.SaveListToXMLSerializer(ListStations, StationsPath);
            }
            else
                throw new DO.BusException("Missing station number. \nC can't delete this station. ");
        }

        public List<Station> GetStations()
        {
            return (from station in XmlTools.GetStations()
                    orderby station.StationNumber
                    select station.Clone()).ToList();
        }

        public Station ReadStation(int StationNumber)
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(StationsPath);

            Station s = ListStations.Find(stat => stat.StationNumber == StationNumber);
          
            if (s != null)
                return s.Clone();
            else
                throw new DO.BusException("Couldn't find station.");
        }

        public bool UpdateStation(Station station, int OldStationNumber)
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(StationsPath);
            List<LineStation> ListLineStation = XmlTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);

            if (ListStations.Exists(item => item.StationNumber == station.StationNumber) && !(OldStationNumber == station.StationNumber)) //if a station with this number exists already
                throw new BusException("A station with this number exists already.\nCan't update station.");

            else
            {
                if (ListStations.Exists(item => item.StationNumber == OldStationNumber)) //if couldn't find the station in the system - can't delete it
                {
                    ListStations.RemoveAll(item => item.StationNumber == OldStationNumber); //using RemoveAll and not just Remove because of the condition
                    ListStations.Add(station.Clone());

                    foreach (var ls in ListLineStation)
                    {
                        if (ls.StationNumber == OldStationNumber)
                            ls.StationNumber = station.StationNumber;
                    }

                    return XmlTools.SaveListToXMLSerializer(ListStations, StationsPath) && XmlTools.SaveListToXMLSerializer(ListLineStation, LineStationPath); 
                }

                throw new BusException("You're trying to update a station that doesn't exists.");
            }

        }
        #endregion

        #region User

        public bool AddUser(User user)
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(UserPath);

            if (ListUsers.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password && u.Permission == user.Permission) != null)
                throw new DO.BusException("user exists already.\nCan't add user.");

            ListUsers.Add(user.Clone());
            return XmlTools.SaveListToXMLSerializer(ListUsers, UserPath);

        }

        public bool DeleteUser(User user)
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(UserPath);
            User u = ListUsers.Find(us => us.UserName == user.UserName && us.Password == user.Password && us.Permission == user.Permission);

            if (u != null)
            {
                ListUsers.Remove(u);
                return XmlTools.SaveListToXMLSerializer(ListUsers, UserPath);
            }
            else
                throw new DO.BusException("Missing user. \nC can't delete this user. ");

        }

        public List<User> GetUsers()
        {
            return (from user in XmlTools.GetUsers()
                    orderby user.UserName
                    select user.Clone()).ToList();
        }

        public User ReadUser(string UserName, string Password)
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(UserPath);
            
            User u = ListUsers.Find(us => us.UserName == UserName && us.Password == Password);

            if (u != null)
                return u.Clone();
            else
                throw new DO.BusException("Missing user. \n can't return this user. ");

        }

        public bool UpdateUser(User user)
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(UserPath);

            User u = ListUsers.Find(us => us.UserName == user.UserName);

            if (u != null)
            {
                ListUsers.Remove(u);
                ListUsers.Add(user.Clone());
                return XmlTools.SaveListToXMLSerializer(ListUsers, UserPath);
            }
            else
                throw new DO.BusException("User doesn't exist.\nCan't update this user.");
        }

        #endregion

        #region UserDrive

        public bool AddUserDrive(UserDrive userDrive)
        {
            List<UserDrive> ListUsersDrive = XmlTools.LoadListFromXMLSerializer<UserDrive>(UserDrivePath);

            if (ListUsersDrive.FirstOrDefault(u => u.DriveIndex == userDrive.DriveIndex) != null)
                throw new DO.BusException("user drive exists already.\nCan't add user drive.");

            ListUsersDrive.Add(userDrive.Clone());
            return XmlTools.SaveListToXMLSerializer(ListUsersDrive, UserDrivePath);
        }

        public bool DeleteUserDrive(UserDrive userDrive)
        {
            List<UserDrive> ListUsersDrive = XmlTools.LoadListFromXMLSerializer<UserDrive>(UserDrivePath);

            UserDrive u = ListUsersDrive.Find(us => us.DriveIndex == userDrive.DriveIndex);

            if (u != null)
            {
                ListUsersDrive.Remove(u);
                return XmlTools.SaveListToXMLSerializer(ListUsersDrive, UserDrivePath);
            }
            else
                throw new DO.BusException("Missing drive index. \nC can't delete this user drive. ");

        }

        public List<UserDrive> GetUserDrives()
        {
            return (from userDrive in XmlTools.GetUserDrives()
                    orderby userDrive.DriveIndex
                    select userDrive.Clone()).ToList();
        }

        public UserDrive ReadUserDrive(int DriveIndex)
        {
            List<UserDrive> ListUsersDrive = XmlTools.LoadListFromXMLSerializer<UserDrive>(UserDrivePath);

            UserDrive u = ListUsersDrive.Find(us => us.DriveIndex == DriveIndex);

            if (u != null)
                return u.Clone();
            else
                throw new DO.BusException("Missing user drive. \nC can't return this user drive. ");
        }

        public bool UpdateUserDrive(UserDrive userDrive)
        {
            List<UserDrive> ListUsersDrive = XmlTools.LoadListFromXMLSerializer<UserDrive>(UserDrivePath);

            UserDrive u = ListUsersDrive.Find(us => us.DriveIndex == userDrive.DriveIndex);

            if (u != null)
            {
                ListUsersDrive.Remove(u);
                ListUsersDrive.Add(userDrive.Clone());
                return XmlTools.SaveListToXMLSerializer(ListUsersDrive, UserDrivePath);
            }
            else
                throw new DO.BusException("User drive doesn't exist.\nCan't update this user drive.");
        }

        #endregion


    }
}
