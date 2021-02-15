using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    class XmlTools
    {
        static string dir = @"xml\";

        static XmlTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        #region SaveLoadWithXElement
        public static bool SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir+ filePath);
                return true;
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(dir+filePath))
                {
                    return XElement.Load( dir+ filePath);
                }
                else
                {
                    XElement rootElem = new XElement(dir+ filePath);
                    
                    rootElem.Save( dir + filePath);
                    //initialize(filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }

        #endregion

        #region SaveLoadWithXMLSerializer
        public static bool SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dir+ filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir+ filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir+ filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }

        #endregion

        #region Get
        internal static List<DO.Station> GetStations()
        {
            List<DO.Station> listStations;
            XmlSerializer x = new XmlSerializer(typeof(List<DO.Station>));
            FileStream fs = new FileStream("xml//Stations.xml", FileMode.Open, FileAccess.Read ,FileShare.ReadWrite);
            listStations = (List<DO.Station>)x.Deserialize(fs);
            return listStations;

        }

        internal static List<DO.Bus> GetBusses()
        {
            List<DO.Bus> buses;
            XmlSerializer x = new XmlSerializer(typeof(List<DO.Bus>));
            FileStream fs = new FileStream("xml//Buses.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            buses = (List<DO.Bus>)x.Deserialize(fs);
            return buses;

        }

        internal static List<DO.BusLine> GetBusLines()
        {
            List<DO.BusLine> busLines;
            XmlSerializer x = new XmlSerializer(typeof(List<DO.BusLine>));
            FileStream fs = new FileStream("xml//BusLines.xml", FileMode.Open ,FileAccess.Read, FileShare.ReadWrite);
            busLines = (List<DO.BusLine>)x.Deserialize(fs);
            return busLines;
        }

        internal static List<DO.DrivingBus> GetDrivingBuses()
        {
            List<DO.DrivingBus> drivingBuses;
            XmlSerializer x = new XmlSerializer(typeof(List<DO.BusLine>));
            FileStream fs = new FileStream("xml//DrivingBuses.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            drivingBuses = (List<DO.DrivingBus>)x.Deserialize(fs);
            return drivingBuses;
        }

        internal static List<DO.LineStation> GetLineStations()
        {
            List<DO.LineStation> lineStations;
            XmlSerializer x = new XmlSerializer(typeof(List<DO.LineStation>));
            FileStream fs = new FileStream("xml//LineStations.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            lineStations = (List<DO.LineStation>)x.Deserialize(fs);
            return lineStations;
        }

        internal static List<DO.User> GetUsers()
        {
            List<DO.User> users;
            XmlSerializer x = new XmlSerializer(typeof(List<DO.User>));
            FileStream fs = new FileStream("xml//Users.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            users = (List<DO.User>)x.Deserialize(fs);
            return users;
        }

        internal static List<DO.UserDrive> GetUserDrives()
        {
            List<DO.UserDrive> userdrives;
            XmlSerializer x = new XmlSerializer(typeof(List<DO.UserDrive>));
            FileStream fs = new FileStream("xml//UserDrives.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            userdrives = (List<DO.UserDrive>)x.Deserialize(fs);
            return userdrives;
        }

        #endregion

    }


}


