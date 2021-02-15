using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using System.Text;
using System.Threading.Tasks;

using DALAPI;
using BLAPI;
using System.Xml;

namespace BL
{
    class BLImp : IBL
    {    
        IDAL dl = DalFactory.GetDL();

        #region singleton implementaion

        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp() { } // default => private
        public static BLImp Instance { get => instance; }// The public Instance property to use

        #endregion

        static Random rand = new Random(DateTime.Now.Millisecond);

        #region Station Converter

        private DO.Station ConvertToDO(BO.Station station) //converts a station from BO to DO
        {
            return new DO.Station
            {
                StationNumber = station.StationNumber,
                StationName = station.StationName,
                Longitude = station.Longitude,
                Latitude = station.Latitude,
                Address = station.Address
            };
        }

        private BO.Station ConvertToBO(DO.Station station) //converts a station from DO to BO
        {
            return new BO.Station
            {
                StationNumber = station.StationNumber,
                StationName = station.StationName,
                Longitude = station.Longitude,
                Latitude = station.Latitude,
                Address = station.Address,
            };
        }

        #endregion

        #region Station

        public bool IsHebrew(string Word)
        {
            string[] HL = { " ", "\"", "\'", "-", ",", "/", "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י", "כ", "ך", "ל", "מ", "ם", "נ", "ן", "ס", "ע", "פ", "ף", "צ", "ץ", "ק", "ר", "ש", "ת" };
            List<string> HebrewLetters = new List<string>(HL);

            foreach (var letter in Word)
                if (!HebrewLetters.Contains(letter.ToString()) && !char.IsDigit(letter))
                    return false;
            return true;
        }

        public bool AddStation(int StationNumber, string StationName, double Latitude, double Longitude, string Address)
        {
            if (StationNumber > 0 && StationNumber < BO.Consts.MAXDIGITS) //station number is correct
            {
                if (IsHebrew(StationName)) //returns true is the station name that was sent contains only Hebrew letters or digits
                {
                    if (Latitude >= BO.Consts.MIN_LAT && Latitude <= BO.Consts.MAX_LAT && Longitude >= BO.Consts.MIN_LON && Longitude <= BO.Consts.MAX_LON) //latitude and longitude is correct
                    {
                        if (IsHebrew(Address)) //returns true is the station address that was sent contains only Hebrew letters or digits
                        {
                            try
                            {
                                BO.Station stationBO = new BO.Station
                                {
                                    StationNumber = StationNumber,
                                    StationName = StationName,
                                    Latitude = Latitude,
                                    Longitude = Longitude,
                                    Address = Address
                                };

                                return dl.AddStation(ConvertToDO(stationBO)); //adds the converted station to the list in DS by using the AddStation func from IDAL
                                                                              //returns true if the station was added successfully
                            }

                            catch (DO.BusException ex)
                            {
                                throw new BO.BlBusException(ex.Message);
                            }
                        }

                        else
                        {
                            throw new BO.BlBusException("You entered an invalid station address.\nCan't add station.");
                        }
                    }

                    else
                    {
                        throw new BO.BlBusException("You entered an invalid station measurements.\nCan't add station.");
                    }
                }

                else
                {
                    throw new BO.BlBusException("You entered an invalid station name.\nCan't add station.");
                }
            }

            else
            {
                throw new BO.BlBusException("You entered an invalid station number.\nCan't add station.");
            }
        }

        public BO.Station ReadStation(int StationNumber)
        {
            if (StationNumber > 0 && StationNumber < BO.Consts.MAXDIGITS) //station number is correct
            {
                try
                {
                    BO.Station station = new BO.Station(); //creates a new BO station 
                    DO.Station stationDO = dl.ReadStation(StationNumber); //gets the matching DO station by using the ReadStation func from IDAL
                    if (stationDO == null) //if the station couldn't be found in the DS by DAL
                    {
                        throw new BO.BlBusException("Station with the number you entered couldn't be found.\nCan't show station.");
                    }
                    stationDO.Clone(station); //copies the DO station's info into the BO station 
                    return station; //returns the BO station
                }

                catch (DO.BusException ex)
                {
                    throw new BO.BlBusException(ex.Message);
                }
            }

            else
            {
                throw new BO.BlBusException("You entered an invalid station number.\nCan't show station.");
            }
        }

        public bool UpdateStation(int StationNumber, int NewStationNumber, string StationName, double Latitude, double Longitude, string Address)
        {

            if (StationNumber > 0 && StationNumber < BO.Consts.MAXDIGITS) //station number is correct
            {
                BO.Station station = ReadStation(StationNumber);

                if (NewStationNumber < 0) //if station number wasn't changed
                    NewStationNumber = station.StationNumber;

                if (StationName == "0") //if station name wasn't changed
                    StationName = station.StationName;

                if (Latitude <= 0) //if latitude wasn't changed
                    Latitude = station.Latitude;

                if (Longitude <= 0) //if longitude wasn't changed
                    Longitude = station.Longitude;

                if (Address == "0") //if address wasn't changed
                    Address = station.Address;

                if (IsHebrew(StationName)) //returns true is the station name that was sent contains only Hebrew letters or digits
                {
                    if (Latitude >= BO.Consts.MIN_LAT && Latitude <= BO.Consts.MAX_LAT && Longitude >= BO.Consts.MIN_LON && Longitude <= BO.Consts.MAX_LON) //latitude and longitude is correct
                    {
                        if (IsHebrew(Address)) //returns true is the station address that was sent contains only Hebrew letters or digits
                        {
                            try
                            {
                                BO.Station stationBO = new BO.Station
                                {
                                    StationNumber = NewStationNumber,
                                    StationName = StationName,
                                    Latitude = Latitude,
                                    Longitude = Longitude,
                                    Address = Address
                                };

                                return dl.UpdateStation(ConvertToDO(stationBO), StationNumber); //uses the UpdateStation func from IDAL with the converted station (from BO to DO) to update the station and the stations list
                            }

                            catch (DO.BusException ex)
                            {
                                throw new BO.BlBusException(ex.Message);
                            }
                        }

                        else
                        {
                            throw new BO.BlBusException("You entered an invalid station address.\nCan't update station.");
                        }
                    }

                    else
                    {
                        throw new BO.BlBusException("You entered an invalid station measurements.\nCan't update station.");
                    }
                }

                else
                {
                    throw new BO.BlBusException("You entered an invalid station name.\nCan't update station.");
                }
            }

            else
            {
                throw new BO.BlBusException("You entered an invalid station number.\nCan't update station.");
            }
        }

        public bool DeleteStation(int StationNumber)
        {
            
            if (StationNumber > 0 && StationNumber < BO.Consts.MAXDIGITS) //station number is correct
            {
                List<BO.Station> result = new List<BO.Station>();
                foreach (var station in dl.GetStations())
                    result.Add(ConvertToBO(station)); //gets all stations in the system in a BO format

                foreach (var station in result)
                {
                    try
                    {
                        if (station.StationNumber == StationNumber) //searching for the matching station 
                            return dl.DeleteStation(ConvertToDO(station)); //uses the DeleteStation func from IDAL with the converted station (from BO to DO) to delete the station
                    }
                    catch (DO.BusException ex)
                    {
                        throw new BO.BlBusException(ex.Message);
                    }
                }

                //gets here if didn't find the station to be deleted
                throw new BO.BlBusException("Station with the number you entered couldn't be found.\nCan't delete station.");
            }

            throw new BO.BlBusException("You entered an invalid station number.\nCan't delete station.");
          
        }

        public List<BO.Station> GetStations()
        {
          
            List<BO.Station> result = new List<BO.Station>();

            foreach (var station in dl.GetStations()) //for each station in the stations list in DAL (comes from DS)
                result.Add(ConvertToBO(station));     //converts the DO station into a BO station and adds it to the result list

            if (result == null)
                throw new BO.BlBusException("No stations in the system.");
            else
                return result;

        }

        public List<BO.BusLine> GetStationPassingLines(BO.Station station)
        {
            List<BO.BusLine> BusLinesInStation = new List<BO.BusLine>();
            
            foreach(var ls in dl.GetLineStations())
            {
                if (ls.StationNumber == station.StationNumber) //if the pair line-station matches this station - add line number to list of lines that pass this station
                    BusLinesInStation.Add(ConvertToBO(dl.ReadBusLine(ls.BusLineIndex)));
            }
            
            return BusLinesInStation;
        }

        #endregion

        #region Bus Converter

        private DO.Bus ConvertToDO(BO.Bus bus) //converts a bus from BO to DO
        {
            return new DO.Bus
            {
                LicenseNumber = bus.LicenseNumber,
                StartDate = bus.StartDate,
                Mileage = bus.Mileage,
                Fuel = bus.Fuel,
                Treatment = DateTime.Today.AddYears(1), //date of treatment to the current date plus one year
                Status = DO.STATUS.READY
            };
        }

        private BO.Bus ConvertToBO(DO.Bus bus) //converts a bus from DO to BO
        {
            return new BO.Bus
            {
                LicenseNumber = bus.LicenseNumber,
                StartDate = bus.StartDate,
                Mileage = bus.Mileage,
                Fuel = bus.Fuel,
                Treatment = bus.Treatment,
                Status = (BO.STATUS)bus.Status

            };
        }

        #endregion

        #region Bus

        public bool AddBus(string LicenseNumber, string StartDate, double Mileage, double Fuel)
        {
            DateTime sd;
            if (DateTime.TryParse(StartDate, out sd))
            {
                if ((sd.Year > 2018 && LicenseNumber.Length == 8) || (sd.Year <= 2018 && LicenseNumber.Length == 7))
                {

                    if (Mileage < 20000 && Mileage > 0)
                    {

                        if (Fuel < BO.Consts.tank && Fuel > 0)
                        {
                            try
                            {
                                BO.Bus busBO = new BO.Bus
                                {
                                    LicenseNumber = LicenseNumber,
                                    StartDate = sd,
                                    Mileage = Mileage,
                                    Fuel = Fuel,
                                    Treatment = DateTime.Today.AddYears(1),
                                    Status = BO.STATUS.READY

                                };

                                return dl.AddBus(ConvertToDO(busBO)); //adds the converted bus to the list in DS by using the AddBus func from IDAL
                            }

                            catch (DO.BusException ex)
                            {
                                throw new BO.BlBusException(ex.Message);
                            }
                        }

                        else throw new BO.BlBusException("Fuel is invalid.\nCan't add bus."); //fuel inserted is invalid
                    }

                    else throw new BO.BlBusException("Mileage is invalid.\nCan't add bus."); //mileage inserted is invalid
                }

                else throw new BO.BlBusException("License number is invalid.\nCan't add bus."); //license number inserted is invalid                                                                          
            }
            
            else throw new BO.BlBusException("Starting date is invalid.\nCan't add bus."); //starting date inserted is invalid            
        }

        public BO.Bus ReadBus(string LicenseNumber)
        {
            if (LicenseNumber.Length == 7 || LicenseNumber.Length == 8) //license number is correct (only checks length)
            {
                try 
                {
                    BO.Bus bus = new BO.Bus(); //creats a new BO bus
                    DO.Bus busDO = dl.ReadBus(LicenseNumber); //gets the matching DO bus by using the ReadBus func from IDAL
                    if (busDO == null) //if the bus couldn't be found in the DS by DAL
                    {
                        throw new BO.BlBusException("Bus with the license number you entered couldn't be found.\nCan't show bus.");
                    }
                    busDO.Clone(bus); //copies the DO bus's info into the BO bus 
                    return bus; //returns the BO bus
                }

                catch (DO.BusException ex)
                {
                    throw new BO.BlBusException(ex.Message);
                }
            }

            else
            {
                throw new BO.BlBusException("You entered an invalid license number.\nCan't show bus.");
            }
        }

        public bool UpdateBus(string LicenseNumber, string NewLicenseNumber, double Mileage, double Fuel)
        {

            if ((LicenseNumber.Length == 7) || LicenseNumber.Length == 8) //station number is correct
            {
                BO.Bus bus = ReadBus(LicenseNumber);

                if (NewLicenseNumber == "-1") //if station number wasn't changed
                    NewLicenseNumber = bus.LicenseNumber;

                if (Mileage == -1) //if latitude wasn't changed
                    Mileage = bus.Mileage;

                if (Fuel == -1) //if longitude wasn't changed
                    Fuel = bus.Fuel;

                if ((bus.StartDate.Year > 2018 && LicenseNumber.Length == 8) || (bus.StartDate.Year <= 2018 && LicenseNumber.Length == 7))
                {
                    if (Mileage < 20000 && Mileage > 0)
                    {

                        if (Fuel < BO.Consts.tank && Fuel > 0)
                        {
                            try
                            {
                                BO.Bus busBO = new BO.Bus
                                {
                                    LicenseNumber = NewLicenseNumber,
                                    StartDate = bus.StartDate,
                                    Mileage = Mileage,
                                    Fuel = Fuel,
                                    Treatment = bus.Treatment,
                                    Status = BO.STATUS.READY
                                };

                                return dl.UpdateBus(ConvertToDO(busBO), LicenseNumber);
                            }

                            catch (DO.BusException ex)
                            {
                                throw new BO.BlBusException(ex.Message);
                            }
                        }

                        else throw new BO.BlBusException("Fuel is invalid.\nCan't update bus."); //fuel inserted is invalid
                    }

                    else throw new BO.BlBusException("Mileage is invalid.\nCan't update bus."); //mileage inserted is invalid
                }

                else throw new BO.BlBusException("License number is invalid.\nCan't update bus."); //license number inserted is invalid
            }

            else throw new BO.BlBusException("License number is invalid.\nCan't add bus."); //license number inserted is invalid
        }

        public bool DeleteBus(string LicenseNumber)
        {
            if (LicenseNumber.Length == 7 || LicenseNumber.Length == 8) //license number is correct (only checks length)
            {
                try
                {
                    List<BO.Bus> result = new List<BO.Bus>();
                    foreach (var bus in dl.GetBusses())
                        result.Add(ConvertToBO(bus));

                    foreach (var bus in result)
                    {
                        if (bus.LicenseNumber == LicenseNumber)
                            return dl.DeleteBus(ConvertToDO(bus)); //uses the DeleteBus func from IDAL with the converted bus (from BO to DO) to delete the bus
                    }

                }

                catch(DO.BusException ex)
                {
                    throw new BO.BlBusException(ex.Message);
                }

                //gets here if didn't find the bus to be deleted
                throw new BO.BlBusException("Bus with the license number you entered couldn't be found.\nCan't delete bus.");
            }

            else
            {
                throw new BO.BlBusException("You entered an invalid bus license number.\nCan't delete bus.");
            }
        }

        public IEnumerable <BO.Bus> GetBusses()
        {
            List<BO.Bus> result = new List<BO.Bus>();

            try
            {
                //foreach (var bus in dl.GetBusses()) //for each bus in the buses list in DAL (comes from DS)
                //    result.Add(ConvertToBO(bus));   //converts the DO bus into a BO bus and adds it to the result list
               return from bus in dl.GetBusses()
                      orderby bus.LicenseNumber
                      select ConvertToBO(bus);
            }

            catch(DO.BusException ex)
            {
                throw new BO.BlBusException(ex.Message);
            }

            //if (result == null)
            //    throw new BO.BlBusException("No busses in the system.");
            //else
            //    return result;
        }

        public void BusTreatment(BO.Bus bus) //treatment due to mileage over 20,000 km
        {
            bus.Treatment = DateTime.Today.AddYears(1); //updates the date of treatment to the current time
            bus.Mileage = 0; //sets bus's mileage to 0 to start over
        }

        public void BusRefuel(BO.Bus bus)
        {
            bus.Fuel = BO.Consts.tank ; //updates the fuel tank of the bus to the full amount of 1200
        }

        #endregion

        #region User Converter

        private DO.User ConvertToDO(BO.User user) //converts a user from BO to DO
        {
            return new DO.User
            {
                UserName = user.UserName,
                Password = user.Password,
                Permission = user.Permission
            };
        }

        private BO.User ConvertToBO(DO.User user) //converts a user from DO to BO
        {
            return new BO.User
            {
                UserName = user.UserName,
                Password = user.Password,
                Permission = user.Permission
            };
        }

        #endregion

        #region User

        public bool AddUser(string UserName, string Password, bool Permission)
        {
            
            if (UserName.Any(char.IsUpper) && UserName.Any(char.IsLower) && UserName.Length >= 5)
            {
                if (Password.Any(char.IsUpper) && Password.Any(char.IsLower) && Password.Any(char.IsDigit) && Password.Length >= 5)
                {
                    BO.User userBO = new BO.User
                    {
                        UserName = UserName,
                        Password = Password,
                        Permission = Permission
                    };

                    return dl.AddUser(ConvertToDO(userBO)); //adds the converted user to the list in DS by using the AddUser func from IDAL
                                                            //returns true if the user was added successfully
                }

                else
                {
                    throw new BO.BlBusException("Password is invalid.\nCan't add user.");
                }
            }

            else
            {
                throw new BO.BlBusException("UserName is invalid.\nCan't add user.");
            }
        }

        public BO.User ReadUser(string UserName, string Password)
        {
            
            if ((UserName.Any(char.IsUpper) && UserName.Any(char.IsLower) && UserName.Length >= 5) && (Password.Any(char.IsUpper) && Password.Any(char.IsLower) && Password.Any(char.IsDigit) && Password.Length >= 5)) //user name is correct 
            {
                BO.User user = new BO.User(); //creats a new BO user

                try
                {
                    DO.User userDO = dl.ReadUser(UserName, Password); //gets the matching DO user by using the ReadUser func from IDAL
                    if (userDO == null) //if the user couldn't be found in the DS by DAL
                    {
                        throw new BO.BlBusException("User with the UserName you entered couldn't be found.\nCan't show user.");
                    }
                    userDO.Clone(user); //copies the DO user's info into the BO user 
                    return user; //returns the BO user    
                }
                catch (DO.BusException ex)
                {
                    throw new BO.BlBusException(ex.Message);
                }
            }

            else
            {
                throw new BO.BlBusException("You entered an invalid UserName or password.\nCan't show user.");
            }
        }

        public bool UpdateUser(string UserName, string Password, bool Permission)
        {
          
            if (UserName.Any(char.IsUpper) && UserName.Any(char.IsLower) && UserName.Length >= 5) //user name is correct
            {
                if (UserName.Any(char.IsUpper) && UserName.Any(char.IsLower) && UserName.Any(char.IsDigit) && UserName.Length >= 5) //password is correct
                {
                    try
                    {
                        BO.User userBO = new BO.User
                        {
                            UserName = UserName,
                            Password = Password,
                            Permission = Permission
                        };

                        return dl.UpdateUser(ConvertToDO(userBO)); //uses the UpdateUser func from IDAL with the converted user (from BO to DO) to update the user and users list
                    }

                    catch(DO.BusException ex)
                    {
                        throw new BO.BlBusException(ex.Message);
                    }
                }

                else
                {
                    throw new BO.BlBusException("Password is invalid.\nCan't update user.");
                }
            }

            else
            {
                throw new BO.BlBusException("UserName is invalid.\nCan't update user.");
            }
        }

        public bool DeleteUser(string UserName)
        {
          
            if (UserName.Any(char.IsUpper) && UserName.Any(char.IsLower) && UserName.Length >= 5)
            {
                List<BO.User> result = new List<BO.User>();
                try
                {
                    foreach (var user in dl.GetUsers())
                        result.Add(ConvertToBO(user));

                    foreach (var user in result)
                    {
                        if (user.UserName == UserName)
                            return dl.DeleteUser(ConvertToDO(user)); //uses the DeleteUser func from IDAL with the converted user (from BO to DO) to delete the user
                    }
                }

                catch(DO.BusException ex)
                {
                    throw new BO.BlBusException(ex.Message);
                }

                //gets here if didn't find the user to be deleted
                throw new BO.BlBusException("User with the UserName you entered couldn't be found.\nCan't delete user.");

            }

            else
            {
                throw new BO.BlBusException("You entered an invalid UserName.\nCan't delete user.");
            }
        }

        public List<BO.User> GetUsers()
        {
           
            List<BO.User> result = new List<BO.User>();

            try
            {
                foreach (var user in dl.GetUsers()) //for each user in the users list in DAL (comes from DS)
                    result.Add(ConvertToBO(user));   //converts the DO user into a BO user and adds it to the result list
            }

            catch (DO.BusException ex)
            {
                throw new BO.BlBusException(ex.Message);
            }

            if (result == null)
                throw new BO.BlBusException("No users in the system.");
            else
                return result;
        }

        #endregion

        #region BusLine Converter
        private DO.BusLine ConvertToDO(BO.BusLine busLine) //converts a bus line from BO to DO
        {
            return new DO.BusLine
            {
                BusLineIndex = busLine.BusLineIndex,
                LineNumber = busLine.LineNumber,
                FirstStation = busLine.FirstStation,
                LastStation = busLine.LastStation,
                Area = busLine.Area
            };
        }

        private BO.BusLine ConvertToBO(DO.BusLine busLine) //converts a bus line from BO to DO
        {
            return new BO.BusLine
            {
                BusLineIndex = busLine.BusLineIndex,
                LineNumber = busLine.LineNumber,
                FirstStation = busLine.FirstStation,
                LastStation = busLine.LastStation,
                Area = busLine.Area
            };
        }

        #endregion

        #region BusLine
        public bool AddBusLine(int LineNumber, string Area, int FirstStation, int LastStation)
        {
            string randtime = rand.Next(2, 7).ToString();

            string time = "PT" + randtime + "M";
            if (LineNumber > 0)
            {
                if (!(Area == "Select area"))
                {
                    if (FirstStation > 0)
                    {
                        if (LastStation > 0)
                        {
                            if (!(FirstStation == LastStation))
                            {
                                int F = -1, L = -1;
                                List<BO.Station> stations = GetStations();

                                foreach (var s in stations)
                                {
                                    if (s.StationNumber == FirstStation)
                                        F = s.StationNumber;
                                    if (s.StationNumber == LastStation)
                                        L = s.StationNumber;
                                }

                                if (F >= 0)
                                {
                                    if (L >= 0)
                                    {
                                        try
                                        {
                                            BO.BusLine bus = new BO.BusLine
                                            {
                                                BusLineIndex = dl.GetConfig("BusLineIndex"),
                                                LineNumber = LineNumber,
                                                FirstStation = FirstStation,
                                                LastStation = LastStation,
                                                Area = GetArea(Area)
                                            };

                                            DO.LineStation First = new DO.LineStation
                                            {
                                                BusLineIndex = bus.BusLineIndex,
                                                StationNumber = FirstStation,
                                                NumberOnLine = 1
                                            };

                                            DO.LineStation Last = new DO.LineStation
                                            {
                                                BusLineIndex = bus.BusLineIndex,
                                                StationNumber = LastStation,
                                                NumberOnLine = 2
                                            };

                                            DO.ConsecutiveStations consecutiveStations = new DO.ConsecutiveStations
                                            {
                                                BusLineIndex = bus.BusLineIndex,
                                                ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                                                FirstStation = FirstStation,
                                                SecondStation = LastStation,
                                                Distance = rand.Next(10, 10000), //in meters
                                                AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                                            };

                                            return dl.AddBusLine(ConvertToDO(bus)) && dl.AddLineStation(First) && dl.AddLineStation(Last) && dl.AddConsecutiveStations(consecutiveStations);
                                        }

                                        catch (DO.BusException ex)
                                        {
                                            throw new BO.BlBusException(ex.Message);
                                        }

                                    }

                                    else throw new BO.BlBusException("A station you entered doesn't exists in the system.\nCan't add bus line.");
                                }

                                else throw new BO.BlBusException("A station you entered doesn't exists in the system.\nCan't add bus line.");
                            }

                            else throw new BO.BlBusException("First and last station needs to be different.\nCan't add nus line.");
                        }

                        else throw new BO.BlBusException("The number of the last station is invaild.\nCan't add bus line.");
                    }

                    else throw new BO.BlBusException("The number of the first station is invaild.\nCan't add bus line.");
                }

                else throw new BO.BlBusException("You didn't choose an area.\nCan't add bus line.");
            }

            else throw new BO.BlBusException("The line number is invaild.\nCan't add bus line.");
        }

        public BO.BusLine ReadBusLine(int lineNumber)
        {
            try
            {
                BO.BusLine busBO = new BO.BusLine();
                DO.BusLine busDO = dl.ReadBusLine(lineNumber);

                if (busDO == null)
                    throw new BO.BlBusException("This bus line couldn't be found.\nCan't show bus line.");

                busDO.Clone(busBO);
                return busBO;
            }

            catch (DO.BusException ex)
            {
                throw new BO.BlBusException(ex.Message);
            }
        }

        public bool UpdateBusLine(int LineIndex, int NewLineNumber, string Area, int StationNumber, string Action)
        {
           
            if (LineIndex >= 0)
            {
                bool flag;

                BO.BusLine BusLine = ReadBusLine(LineIndex);

                if (NewLineNumber < -1)
                    throw new BO.BlBusException("You entered an invalid line number.\nCan't update bus line.");
                else if (NewLineNumber >= 0) //if line number was changed
                    BusLine.LineNumber = NewLineNumber;

                if (Area != "Select area") //if area was changed
                    BusLine.Area = GetArea(Area);

                try
                {
                    flag = dl.UpdateBusLine(ConvertToDO(BusLine)); //uses the UpdateStation func from IDAL with the converted station (from BO to DO) to update the station and the stations list
                }

                catch (DO.BusException ex)
                {
                    throw new BO.BlBusException(ex.Message);
                }

                if (StationNumber < -1)
                    throw new BO.BlBusException("You entered an invalid station number.\nCan't update bus line");
                else if (StationNumber == -1) //if not asked to update a station
                    return flag;
                else //station exists in the system
                {
                    if (Action == "Select action")
                        throw new BO.BlBusException("You entered a station number but didn't choose and action.\nCan't update bus line");

                    else if (Action == "Add to line") //wants to add the sent station to the line
                    {
                        BO.Station station = ReadStation(StationNumber);
                        flag = AddStationToLine(BusLine, station); //updates the last station of this bus line to be the new station that is added
                    }

                    else if (Action == "Delete from line") //wants to delete the sent station from the line
                    {
                        BO.Station station = ReadStation(StationNumber);
                        flag = DeleteStationFromLine(BusLine, station);
                    }

                    return flag;
                }
            }

            else throw new BO.BlBusException("You entered an invalid line index.\nCan't update bus line.");
        }

        public bool AddStationToLine(BO.BusLine BusLine, BO.Station Station)
        {
            string randtime = rand.Next(2, 7).ToString();
            string time = "PT" + randtime + "M";
            if (!GetStationNumbers(BusLine).Contains(Station.StationNumber))
            {
                try
                {
                    DO.LineStation LineStation = new DO.LineStation
                    {
                        BusLineIndex = BusLine.BusLineIndex,
                        StationNumber = Station.StationNumber,
                        NumberOnLine = GetStationNumbers(BusLine).Count //last by default
                    };

                    DO.ConsecutiveStations ConsecutiveStations = new DO.ConsecutiveStations
                    {
                        BusLineIndex = BusLine.BusLineIndex,
                        ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                        FirstStation = BusLine.LastStation,
                        SecondStation = Station.StationNumber,
                        Distance = rand.Next(10, 10000), //in meters
                        AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                    };

                    BusLine.LastStation = Station.StationNumber;

                    return dl.UpdateBusLine(ConvertToDO(BusLine)) && dl.AddLineStation(LineStation) && dl.AddConsecutiveStations(ConsecutiveStations);
                }

                catch (DO.BusException ex)
                {
                    throw new BO.BlBusException(ex.Message);
                }
            }

            else throw new BO.BlBusException("The station you asked to add is already on the line and can't be added.");
        }

        public bool DeleteStationFromLine(BO.BusLine BusLine, BO.Station Station)
        {
            bool flag;
            string randtime = rand.Next(2, 7).ToString();
            string time = "PT" + randtime + "M";
            if (GetStationNumbers(BusLine).Contains(Station.StationNumber))
            {
                try
                {
                    if (Station.StationNumber == BusLine.FirstStation) //if first station is getting deleted makes the second one first and fixes the index of all the rest of the stations on the line
                    {
                        BusLine.FirstStation = GetStationNumbers(BusLine)[1];
                        foreach(var LS in GetStationsOnLine(BusLine)) 
                        {
                            LS.NumberOnLine--;
                            flag = dl.UpdateLineStation(LS);
                        }
                    }

                    if (Station.StationNumber == BusLine.LastStation) //if last station is getting deleted makes the second to last one last
                        BusLine.LastStation = GetStationNumbers(BusLine)[GetStationNumbers(BusLine).Count - 1];

                
                    //creates the new consecutive-stations 
                    int First = GetConsecutiveStationsOnLine(BusLine.BusLineIndex, Station.StationNumber, 2).FirstStation;
                    int Second = GetConsecutiveStationsOnLine(BusLine.BusLineIndex, Station.StationNumber, 1).SecondStation;
                    DO.ConsecutiveStations NewConsecutiveStations = new DO.ConsecutiveStations
                    {
                        BusLineIndex = BusLine.BusLineIndex,
                        ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                        FirstStation = First,
                        SecondStation = Second,
                        Distance = rand.Next(10, 10000), //in meters
                        AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                    };
                    flag = dl.AddConsecutiveStations(NewConsecutiveStations);

                    //updating the index of all the stations that come after the deleted station on the original line
                    int index = 0;
                    foreach (var LS in GetStationsOnLine(BusLine))
                    {
                        if (LS.StationNumber == Station.StationNumber)
                            index = LS.NumberOnLine;
                    }

                    foreach (var LS in GetStationsOnLine(BusLine))
                    {
                        if (LS.NumberOnLine > index)
                        {
                            LS.NumberOnLine--;
                            flag = dl.UpdateLineStation(LS);
                        }
                    }

                    //deletes the line-station and the consecutive-stations that match
                    return flag && dl.DeleteLineStation(dl.ReadLineStation(BusLine.BusLineIndex, Station.StationNumber)) && 
                        dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(BusLine.BusLineIndex, Station.StationNumber, 1)) && dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(BusLine.BusLineIndex, Station.StationNumber, 2));
                }

                catch (DO.BusException ex)
                {
                    throw new BO.BlBusException(ex.Message);
                }
            }

            else throw new BO.BlBusException("The station you asked to delete isn't on the line and can't be deleted.");
        }

        public DO.ConsecutiveStations GetConsecutiveStationsOnLine(int BusLineIndex, int StationNumber, int number)
        {
            foreach(var cs in dl.GetConsecutiveStations())
            {
                if (cs.BusLineIndex == BusLineIndex && number == 1 && cs.FirstStation == StationNumber)
                    return cs;
                if (cs.BusLineIndex == BusLineIndex && number == 2 && cs.SecondStation == StationNumber)
                    return cs;
            }
            
            throw new BO.BlBusException("No matching consecutive stations on this line BLImp-998");
        }

        public bool DeleteBusLine(int BusLineIndex)
        {
            
            foreach (var bline in dl.GetBusLines())  //Search the bus and delete it
            {
                try
                {
                    if (bline.BusLineIndex == BusLineIndex)
                    {
                        foreach (var station in GetStationNumbers(ConvertToBO(bline))) //deletes all the line-stations of this line
                        {
                            dl.DeleteLineStation(dl.ReadLineStation(bline.BusLineIndex, station));
                        }

                        foreach(var consecutiveStations in dl.GetConsecutiveStations()) //deletes all the consecutive-stations of this line
                        {
                            if (consecutiveStations.BusLineIndex == BusLineIndex)
                                dl.DeleteConsecutiveStations(consecutiveStations);
                        }

                        return dl.DeleteBusLine(bline); //return true if it is deleted
                    }
                }

                catch (DO.BusException ex)
                {
                    throw new BO.BlBusException(ex.Message);
                }
            }

            //gets here if didn't find the bus line to be deleted
            throw new BO.BlBusException("The bus line couldn't be found by it's index.\nCan't delete bus line");
        }

        public DO.AREA GetArea(string area)
        {
            if (area == "GENERAL" || area == "General")
                return DO.AREA.GENERAL;
            if (area == "NORTH" || area == "North")
                return DO.AREA.NORTH;
            if (area == "SOUTH" || area == "South")
                return DO.AREA.SOUTH;
            if (area == "CENTER" || area == "Center")
                return DO.AREA.CENTER;
            if (area == "JERUSALEM" || area == "Jerusalem")
                return DO.AREA.JERUSALEM;
            return DO.AREA.NULL;
        }

        public string GetArea(BO.BusLine bus)
        {
            return bus.Area.ToString();
        }

        public List<BO.BusLine> GetBusLines()
        {
            
            List<BO.BusLine> result = new List<BO.BusLine>();

            try
            {
                foreach (var busline in dl.GetBusLines()) //for each bus line in the bus lines list in DAL (comes from DS)
                    result.Add(ConvertToBO(busline));   //converts the DO bus-line into a BO bus-line and adds it to the result list
            }

            catch(DO.BusException ex)
            {
                throw new BO.BlBusException(ex.Message);
            }

            if (result == null)
                throw new BO.BlBusException("No bus lines in the system.");
            else
                return result;
        }

        public List<int> GetStationNumbers(BO.BusLine busline)
        {
         
            List<int> stations = new List<int>();

            try
            {
                foreach (var linestation in dl.GetLineStations())
                {
                    if (linestation.BusLineIndex == busline.BusLineIndex)
                    {
                        stations.Add(linestation.StationNumber);
                    }
                }
            }

            catch (DO.BusException ex)
            {
                throw new BO.BlBusException(ex.Message);
            }

            return stations;
        }

        public List<DO.LineStation> GetStationsOnLine(BO.BusLine BusLine)
        {
            List<DO.LineStation> a = dl.GetLineStations();
            List<DO.LineStation> b = new List<DO.LineStation>();
            foreach (var s in a)
            {
                if (s.BusLineIndex == BusLine.BusLineIndex)
                    b.Add(s);
            }
            return b.OrderBy(o => o.NumberOnLine).ToList();
        }

        public List<int> GetStationsOnLineByNumber(int LineIndex)
        {
            List<DO.LineStation> a = dl.GetLineStations();
            List<DO.LineStation> b = new List<DO.LineStation>();
            foreach(var s in a)
            {
                if (s.BusLineIndex == LineIndex)
                    b.Add(s);
            }
            List<DO.LineStation> c = b.OrderBy(o => o.NumberOnLine).ToList();
            List<int> d = new List<int>();
            foreach(var s in c)
            {
                d.Add(s.StationNumber);
            }
            return d;
        }

        public bool OnLine(int LineIndex, int StationNumber)
        {
            foreach(var station in GetStationsOnLine(ReadBusLine(LineIndex)))
            {
                if (station.StationNumber == StationNumber)
                    return true;
            }

            throw new BO.BlBusException("Station isn't on this line. Can't update.");
        }

        public bool UpdateStationOnLine(int LineIndex, int StationNumber, int Placement, int DistancePrevious, int DistanceNext, int TravelTimePrevious, int TravelTimeNext)
        {
            string randtime = rand.Next(2, 7).ToString();
            string time = "PT" + randtime + "M";
            bool flag = true;

            //both line-index and station-number were checked already (if legal, if station is on this line..)

            int NewDistancePrevious, NewDistanceNext, NewTravelTimePrevious, NewTravelTimeNext; 

            try
            {
                if (Placement < -1)
                    throw new BO.BlBusException("You entered an invalid placement value.\nCan't update station.");
                if (Placement >= 0) //if placement was asked to be changed
                {
                    if (Placement > GetStationsOnLine(ReadBusLine(LineIndex)).LastOrDefault().NumberOnLine) //if placement asked for is bigger then the whole line
                    {
                        throw new BO.BlBusException("You asked to insert the station in an index that's too big.\nLine has "
                            + GetStationsOnLine(ReadBusLine(LineIndex)).LastOrDefault().NumberOnLine + " stations.\nCan't update the station.");
                    }

                    DO.LineStation Station = new DO.LineStation();
                    foreach (var station in GetStationsOnLine(ReadBusLine(LineIndex))) //gets the matching line-station
                        if (station.StationNumber == StationNumber)
                            Station = station;

                    //מכל מקום קדימה
                    int First = -1, Second = -1, One = -1, Two = -1;

                    if (Station.NumberOnLine > Placement && Placement != 1)
                    {
                        foreach (var s in GetStationsOnLine(ReadBusLine(LineIndex)))
                        {
                            if (s.NumberOnLine == Placement - 1) //1
                                First = s.StationNumber;

                            if (s.NumberOnLine == Placement) //2
                                Second = s.StationNumber;

                            if (s.NumberOnLine == Station.NumberOnLine - 1) //5
                                One = s.StationNumber;

                            if (s.NumberOnLine == Station.NumberOnLine + 1) //7
                                Two = s.StationNumber;

                            if (s.NumberOnLine >= Placement && s.NumberOnLine < Station.NumberOnLine) //2,3,4,5
                            {
                                s.NumberOnLine++;
                                flag = dl.UpdateLineStation(s);
                            }
                        }

                        Station.NumberOnLine = Placement;
                        flag = dl.UpdateLineStation(Station);

                        flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, First, 1)); //1-2
                                                                                                                //if (One != -1)
                        flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, Station.StationNumber, 2)); //5-6
                                                                                                                                //if(Two != -1)
                        flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, Station.StationNumber, 1)); //6-7

                        //1-6
                        DO.ConsecutiveStations NewConsecutiveStations1 = new DO.ConsecutiveStations
                        {
                            BusLineIndex = LineIndex,
                            ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                            FirstStation = First,
                            SecondStation = Station.StationNumber,
                            Distance = rand.Next(10, 10000), //in meters
                            AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                        };
                        flag = dl.AddConsecutiveStations(NewConsecutiveStations1);

                        //6-2
                        DO.ConsecutiveStations NewConsecutiveStations2 = new DO.ConsecutiveStations
                        {
                            BusLineIndex = LineIndex,
                            ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                            FirstStation = Station.StationNumber,
                            SecondStation = Second,
                            Distance = rand.Next(10, 10000), //in meters
                            AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                        };
                        flag = dl.AddConsecutiveStations(NewConsecutiveStations2);

                        if (Two != -1)
                        {
                            //5-7
                            DO.ConsecutiveStations NewConsecutiveStations3 = new DO.ConsecutiveStations
                            {
                                BusLineIndex = LineIndex,
                                ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                                FirstStation = One,
                                SecondStation = Two,
                                Distance = rand.Next(10, 10000), //in meters
                                AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                            };
                            flag = dl.AddConsecutiveStations(NewConsecutiveStations3);
                        }
                    }

                    //מכל מקום אחורה
                    if (Station.NumberOnLine < Placement && Placement != GetStationsOnLine(ReadBusLine(LineIndex)).LastOrDefault().NumberOnLine)
                    {
                        foreach (var s in GetStationsOnLine(ReadBusLine(LineIndex)))
                        {
                            if (s.NumberOnLine == Placement) //5
                            {
                                First = s.StationNumber;
                            }

                            if (s.NumberOnLine == Placement + 1) //6
                            {
                                Second = s.StationNumber;
                            }

                            if (s.NumberOnLine == Station.NumberOnLine - 1) //1
                            {
                                One = s.StationNumber;
                            }

                            if (s.NumberOnLine == Station.NumberOnLine + 1) //3
                            {
                                Two = s.StationNumber;
                            }

                            if (s.NumberOnLine > Station.NumberOnLine && s.NumberOnLine <= Placement) //3,4,5
                            {
                                s.NumberOnLine--;
                                flag = dl.UpdateLineStation(s);
                            }
                        }

                        Station.NumberOnLine = Placement;
                        flag = dl.UpdateLineStation(Station);

                        flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, First, 1)); //5-6
                        if (One != -1)
                            flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, Station.StationNumber, 2)); //1-2
                        if (Two != -1)
                            flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, Station.StationNumber, 1)); //2-3

                        //5-2
                        DO.ConsecutiveStations NewConsecutiveStations1 = new DO.ConsecutiveStations
                        {
                            BusLineIndex = LineIndex,
                            ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                            FirstStation = First,
                            SecondStation = Station.StationNumber,
                            Distance = rand.Next(10, 10000), //in meters
                            AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                        };
                        flag = dl.AddConsecutiveStations(NewConsecutiveStations1);

                        //2-6
                        DO.ConsecutiveStations NewConsecutiveStations2 = new DO.ConsecutiveStations
                        {
                            BusLineIndex = LineIndex,
                            ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                            FirstStation = Station.StationNumber,
                            SecondStation = Second,
                            Distance = rand.Next(10, 10000), //in meters
                            AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                        };
                        flag = dl.AddConsecutiveStations(NewConsecutiveStations2);

                        if (One != -1)
                        {
                            //1-3
                            DO.ConsecutiveStations NewConsecutiveStations3 = new DO.ConsecutiveStations
                            {
                                BusLineIndex = LineIndex,
                                ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                                FirstStation = One,
                                SecondStation = Two,
                                Distance = rand.Next(10, 10000), //in meters
                                AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                            };
                            flag = dl.AddConsecutiveStations(NewConsecutiveStations3);
                        }
                    }

                    //מכל מקום לראשון
                    if (Placement == 1)
                    {
                        foreach (var s in GetStationsOnLine(ReadBusLine(LineIndex)))
                        {
                            if (s.NumberOnLine == 1) //0
                            {
                                Second = s.StationNumber;
                            }

                            if (s.NumberOnLine == Station.NumberOnLine - 1) //5
                            {
                                One = s.StationNumber;
                            }

                            if (s.NumberOnLine == Station.NumberOnLine + 1) //7
                            {
                                Two = s.StationNumber;
                            }

                            if (s.NumberOnLine < Station.NumberOnLine) //0,1,2,3,4,5
                            {
                                s.NumberOnLine++;
                                flag = dl.UpdateLineStation(s);
                            }
                        }
                        Station.NumberOnLine = Placement;
                        flag = dl.UpdateLineStation(Station);

                        if (One != -1)
                            flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, Station.StationNumber, 2)); //5-6
                        if (Two != -1)
                            flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, Station.StationNumber, 1)); //6-7

                        //6-0
                        DO.ConsecutiveStations NewConsecutiveStations1 = new DO.ConsecutiveStations
                        {
                            BusLineIndex = LineIndex,
                            ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                            FirstStation = Station.StationNumber,
                            SecondStation = Second,
                            Distance = rand.Next(10, 10000), //in meters
                            AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                        };
                        flag = dl.AddConsecutiveStations(NewConsecutiveStations1);

                        if (Two != -1)
                        {
                            //5-7
                            DO.ConsecutiveStations NewConsecutiveStations3 = new DO.ConsecutiveStations
                            {
                                BusLineIndex = LineIndex,
                                ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                                FirstStation = One,
                                SecondStation = Two,
                                Distance = rand.Next(10, 10000), //in meters
                                AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                            };
                            flag = dl.AddConsecutiveStations(NewConsecutiveStations3);
                        }
                    }

                    //מכל מקום לאחרון
                    int count = GetStationsOnLine(ReadBusLine(LineIndex)).LastOrDefault().NumberOnLine; //gets the amount of stations on this line (to know whats last)

                    if (Placement == count)
                    {
                        foreach (var s in GetStationsOnLine(ReadBusLine(LineIndex)))
                        {
                            if (s.NumberOnLine == Station.NumberOnLine - 1) //-1
                            {
                                One = s.StationNumber;
                            }

                            if (s.NumberOnLine == Station.NumberOnLine + 1) //1
                            {
                                Two = s.StationNumber;
                            }

                            if (s.NumberOnLine == count) //6
                            {
                                First = s.StationNumber;
                            }

                            if (s.NumberOnLine > Station.NumberOnLine && s.NumberOnLine <= count) //1,2,3,4,5,6
                            {
                                s.NumberOnLine--;
                                flag = dl.UpdateLineStation(s);
                            }
                        }

                        Station.NumberOnLine = Placement;
                        flag = dl.UpdateLineStation(Station);

                        if (One != -1)
                            flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, Station.StationNumber, 2)); //-1-0
                        if (Two != -1)
                            flag = dl.DeleteConsecutiveStations(GetConsecutiveStationsOnLine(LineIndex, Station.StationNumber, 1)); //0-1

                        //6-0
                        DO.ConsecutiveStations NewConsecutiveStations1 = new DO.ConsecutiveStations
                        {
                            BusLineIndex = LineIndex,
                            ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                            FirstStation = First,
                            SecondStation = Station.StationNumber,
                            Distance = rand.Next(10, 10000), //in meters
                            AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                        };
                        flag = dl.AddConsecutiveStations(NewConsecutiveStations1);

                        if (One != -1)
                        {
                            //-1-1
                            DO.ConsecutiveStations NewConsecutiveStations3 = new DO.ConsecutiveStations
                            {
                                BusLineIndex = LineIndex,
                                ConsecutiveStationsIndex = dl.GetConfig("ConsecutiveStationsIndex"),
                                FirstStation = One,
                                SecondStation = Two,
                                Distance = rand.Next(10, 10000), //in meters
                                AvgTravelTime = XmlConvert.ToTimeSpan(time) //in minutes
                            };
                            flag = dl.AddConsecutiveStations(NewConsecutiveStations3);
                        }
                    }

                    List<BO.BusLine> B = new List<BO.BusLine>(GetBusLines());
                    foreach (var b in B)
                    {
                        if (b.BusLineIndex == LineIndex)
                        {
                            b.FirstStation = GetStationsOnLineByNumber(LineIndex).FirstOrDefault();
                            b.LastStation = GetStationsOnLineByNumber(LineIndex).LastOrDefault();
                            dl.UpdateBusLine(ConvertToDO(b));
                        }
                    }

                }

                if (DistancePrevious > 0) //if distance from previous station was asked to be changed
                {
                    DO.ConsecutiveStations CS = GetConsecutiveStationsOnLine(LineIndex, StationNumber, 2);
                    CS.Distance = DistancePrevious;
                    flag = dl.UpdateConsecutiveStations(CS);
                }

                if (DistancePrevious < 0)
                    throw new BO.BlBusException("You entered an invalid distatnce from previous value.\nCan't update station.");

                if (DistanceNext > 0) //if distance from next station was asked to be changed
                {
                    DO.ConsecutiveStations CS = GetConsecutiveStationsOnLine(LineIndex, StationNumber, 1);
                    CS.Distance = DistanceNext;
                    flag = dl.UpdateConsecutiveStations(CS);
                }

                if (DistanceNext < 0)
                    throw new BO.BlBusException("You entered an invalid distatnce from next value.\nCan't update station.");

                if (TravelTimePrevious > 0) //if travel time from previous station was asked to be changed
                {
                    string prTime = "PT" + TravelTimePrevious + "M";
                    DO.ConsecutiveStations CS = GetConsecutiveStationsOnLine(LineIndex, StationNumber, 2);
                    CS.AvgTravelTime = XmlConvert.ToTimeSpan(prTime);
                    flag = dl.UpdateConsecutiveStations(CS);
                }

                if (TravelTimePrevious < 0)
                    throw new BO.BlBusException("You entered an invalid travel time from previous value.\nCan't update station.");

                if (TravelTimeNext > 0) //if travel time from next station was asked to be changed
                {
                    string neTime = "PT" + TravelTimeNext + "M";
                    DO.ConsecutiveStations CS = GetConsecutiveStationsOnLine(LineIndex, StationNumber, 1);
                    CS.AvgTravelTime = XmlConvert.ToTimeSpan(neTime);
                    flag = dl.UpdateConsecutiveStations(CS);
                }

                if (TravelTimeNext < 0)
                    throw new BO.BlBusException("You entered an invalid travel time from next value.\nCan't update station.");

                return flag;
            }

            catch (DO.BusException ex)
            {
                throw new BO.BlBusException(ex.Message);
            }
        }

        public string GetStationInfo(int LineIndex, int StationNumber, string Info)
        {
            if (Info == "Placement")
                return dl.ReadLineStation(LineIndex, StationNumber).NumberOnLine.ToString();

            if (Info == "PreviousStation")
            {
                if (dl.ReadLineStation(LineIndex, StationNumber).NumberOnLine == 1)
                    return "--";
                return GetConsecutiveStationsOnLine(LineIndex, StationNumber, 2).FirstStation.ToString();
            }

            if (Info == "DistPreviousStation")
            {
                if (dl.ReadLineStation(LineIndex, StationNumber).NumberOnLine == 1)
                    return "--";
                return GetConsecutiveStationsOnLine(LineIndex, StationNumber, 2).Distance.ToString();
            }

            if (Info == "TravelPreviousStation")
            {
                if (dl.ReadLineStation(LineIndex, StationNumber).NumberOnLine == 1)
                    return "--";
                return GetConsecutiveStationsOnLine(LineIndex, StationNumber, 2).AvgTravelTime.ToString();
            }

            if (Info == "NextStation")
            {
                if (StationNumber == GetStationsOnLine(ReadBusLine(LineIndex)).LastOrDefault().StationNumber)
                    return "--";
                return GetConsecutiveStationsOnLine(LineIndex, StationNumber, 1).SecondStation.ToString();
            }

            if (Info == "DistNextStation")
            {
                if (StationNumber == GetStationsOnLine(ReadBusLine(LineIndex)).LastOrDefault().StationNumber)
                    return "--";
                return GetConsecutiveStationsOnLine(LineIndex, StationNumber, 1).Distance.ToString();
            }

            if (Info == "TravelNextStation")
            {
                if (StationNumber == GetStationsOnLine(ReadBusLine(LineIndex)).LastOrDefault().StationNumber)
                    return "--";
                return GetConsecutiveStationsOnLine(LineIndex, StationNumber, 1).AvgTravelTime.ToString();
            }

            return "";
        }

        public List<BO.Station> GetStationsInArea(string Area)
        {
            List<BO.BusLine> BusLinesInArea = new List<BO.BusLine>();
            foreach(var bl in GetBusLines())
            {
                if (bl.Area == GetArea(Area)) //if the line is in the wanted area
                    BusLinesInArea.Add(bl);
            }

            List<DO.LineStation> StationsOnLineInArea = new List<DO.LineStation>();
            foreach (var bl in BusLinesInArea)
            {
                foreach(var s in GetStationsOnLine(bl))
                {
                    StationsOnLineInArea.Add(s); //a list of all the line-stations in the area (from all the lines in the area)
                }
            }

            List<BO.Station> StationsInArea = new List<BO.Station>();
            foreach(var s in StationsOnLineInArea)
            {
                StationsInArea.Add(ReadStation(s.StationNumber)); //a list of all the stations in this area
            }

            return StationsInArea;
        }

        public string GetFinalStationName(int BusLineIndex)
        {
            BO.Station FinalStation = ReadStation(GetStationsOnLine(ReadBusLine(BusLineIndex)).LastOrDefault().StationNumber);
            return FinalStation.StationName;
        }

        public TimeSpan GetExpectedArrivel(BO.Station ChoosenStation, BO.BusLine Line, List<DO.LineStation> StationsOnLineByOrder)
        {
            List<DO.ConsecutiveStations> AllCSInSystem = dl.GetConsecutiveStations(); //all consecutive-stations pairs in the system
            List<DO.ConsecutiveStations> CSOfLine = new List<DO.ConsecutiveStations>(); //all consecutive-stations pairs on the sent line

            foreach(var cs in AllCSInSystem)
            {
                if (cs.BusLineIndex == Line.BusLineIndex)
                    CSOfLine.Add(cs); //if consecutive-stations pair is on the sent line - adds it to the list of all consecutive-stations pairs on the sent line
            }

            int ExpectedArrivelTime = 0, Placement = 0;

            //finds the choosen station's placement on the line (to know what stations come before so we can add the travel time to get the total expected)
            foreach(var station in StationsOnLineByOrder)
            {
                if (station.StationNumber == ChoosenStation.StationNumber)
                    Placement = station.NumberOnLine;
            }

            //calculates the sum of avg travel time between all station on ine that come before the choosen station (aka the expected arrivel time in miniutes) 
            for(int i = 0; i < Placement; i++) //try Placement - 1 maybe...
            {
                foreach(var cs in CSOfLine)
                {
                    if (cs.FirstStation == StationsOnLineByOrder[i].StationNumber)
                        ExpectedArrivelTime += cs.AvgTravelTime.Minutes; //in minutes
                }
            }

            //changing the sum from int (min) to TimeSpan
            return TimeSpan.FromMinutes((double)ExpectedArrivelTime);
        }

        public List<BO.LineTiming> GetAllLinesToShow(BO.Station ChoosenStation, TimeSpan CurrentTime)
        {
            List<BO.BusLine> LinesPassingChoosenStation = GetStationPassingLines(ChoosenStation); //all bus-lines that pass the choosen station
            List<DO.LeavingLine> GeneralSchedule = dl.GetLeavingLines(); //all leaving-lines in the system, general lines schedule
            List<DO.LeavingLine> ChoosenStationSchedule = new List<DO.LeavingLine>(); //lines schedule for the choosen station
            List<BO.LineTiming> AllLinesToShow = new List<BO.LineTiming>(); //the returend list of all the lines that will be showen on the dinamic panel

            foreach (var Gline in GeneralSchedule)
            {
                foreach(var Sline in LinesPassingChoosenStation)
                {
                    if (Sline.BusLineIndex == Gline.BusLineIndex)
                        ChoosenStationSchedule.Add(Gline); //if leaving-line is of a bus-line that's passing the choosen station - gets added to the choosen station's schedule
                }
            }

            foreach(var line in ChoosenStationSchedule)
            {
                if(line.Start >= CurrentTime)
                {
                    BO.LineTiming LT = new BO.LineTiming
                    {
                        LineTimingIndex = dl.GetConfig("LineTimingIndex"),
                        BusLineIndex = line.BusLineIndex,
                        LineNumber = ReadBusLine(line.BusLineIndex).LineNumber,
                        StatringTime = line.Start,
                        FinalStationName = GetFinalStationName(line.BusLineIndex),
                        ExpectedArrivel = GetExpectedArrivel(ChoosenStation, ReadBusLine(line.BusLineIndex), GetStationsOnLine(ReadBusLine(line.BusLineIndex))) + (line.Start - CurrentTime)
                    };

                    AllLinesToShow.Add(LT);
                }
            }

            return AllLinesToShow;
        }

        #endregion
    }
}
