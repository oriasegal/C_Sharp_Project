using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DO;

namespace DALAPI
{
    public static class ConfigXml
    {
        static string dir = @"xml\";
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.Open);
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

        static int busLineIndex = LoadListFromXMLSerializer<BusLine>(@"BusLines.xml").LastOrDefault().BusLineIndex;
        public static int BusLineIndex => ++busLineIndex;

        static int consecutiveStationsIndex = LoadListFromXMLSerializer<ConsecutiveStations>(@"ConsecutiveStations.xml").LastOrDefault().ConsecutiveStationsIndex;
        public static int ConsecutiveStationsIndex => ++consecutiveStationsIndex;

        static int leavingLineIndex = LoadListFromXMLSerializer<LeavingLine>(@"LeavingLines.xml").LastOrDefault().LeavingLineIndex;
        public static int LeavingLineIndex => ++leavingLineIndex;

        static int drivingBusIndex = 0;
        public static int DrivingBusIndex => ++drivingBusIndex;

        static int lineTimingIndex = 0;
        public static int LineTimingIndex => ++lineTimingIndex;





        static int driveIndex = 0; //UserDrive
        public static int DriveIndex => ++driveIndex;

        static int lineInUse = 0; //DrivingBus
        public static int LineInUse => ++lineInUse;
    }
}


