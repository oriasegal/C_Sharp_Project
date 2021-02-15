using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DO;
using System.IO;
using System.Globalization;
using System.Xml.Linq;

namespace DS
{
    public static class DataSource
    {
        public static List<DO.Station> Stations = new List<DO.Station>(); //all stations in the system
        public static List<DO.Bus> Buses = new List<DO.Bus>(); //all buses in the system
        public static List<DO.BusLine> BusLines = new List<DO.BusLine>(); //all bus lines in the system
        public static List<DO.LineStation> LineStations = new List<DO.LineStation>(); //all line-stations in the system
        public static List<DO.DrivingBus> DrivingBuses = new List<DO.DrivingBus>(); //all driving buses in the system
        public static List<DO.LeavingLine> LeavingLines = new List<DO.LeavingLine>(); //all leaving-line in the system
        public static List<DO.ConsecutiveStations> ConsecutiveStations = new List<DO.ConsecutiveStations>(); //all consecutive-stations in the system
        public static List<DO.User> Users = new List<DO.User>(); //all users in the system
        public static List<DO.UserDrive> UserDrives = new List<DO.UserDrive>(); //all user-drives in the system
        public static List<string> LicenseNumbers = new List<string>();

        static DataSource()
        {
            //InitializeStations();
            //InitializeBuses();
            InitializeBusLines();
            //InitializeLineStations();
           // InitializeConsecutiveStations();

           InitializeLeavingLines();
            //InitializeUsers();


            #region save2List
           saveListToXml(LeavingLines, "LeavingLines.xml");
            //saveListToXml(Stations, "Stations.xml");
            //saveListToXml(Buses, "Buses.xml");
            saveListToXml(BusLines, "BusLines.xml");
            //saveListToXml(LineStations, "LineStations.xml");
            //saveListToXml(ConsecutiveStations, "ConsecutiveStations.xml");
            //saveListToXml(Users, "Users.xml");

            #endregion

        }

        static Random rand = new Random(DateTime.Now.Millisecond);

        #region InitializeStations
        public static void InitializeStations()
        {
            Stations.Add(new DO.Station
            {
                StationNumber = 1,
                StationName = "בי''ס בר לב/בן יהודה",
                Latitude = 32.183921,
                Longitude = 34.917806,
                Address = "בן יהודה 76 כפר סבא"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 2,
                StationName = "הרצל/צומת בילו",
                Latitude = 31.870034,
                Longitude = 34.819541,
                Address = "הרצל קרית עקרון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 3,
                StationName = "הנחשול/הדייגים",
                Latitude = 31.984553,
                Longitude = 34.782828,
                Address = "הנחשול 30 ראשון לציון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 4,
                StationName = "פריד/ששת הימים",
                Latitude = 31.88855,
                Longitude = 34.790904,
                Address = "משה פריד 9 רחובות"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 5,
                StationName = "דרך ארץ/אודם",
                Latitude = 32.467293,
                Longitude = 35.039674,
                Address = "שדרות דרך ארץ 17 חריש"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 6,
                StationName = "מנחם דידנר/נחום שריג",
                Latitude = 31.280502,
                Longitude = 34.804679,
                Address = "מנחם דידנר 20 באר שבע"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 7,
                StationName = "אליעזר קפלן/דובנוב",
                Latitude = 32.07356,
                Longitude = 34.783258,
                Address = "אליעזר קפלן 11 תל אביב יפו"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 8,
                StationName = "הבנים/אלי כהן",
                Latitude = 31.862305,
                Longitude = 34.821857,
                Address = "בנים 4 קרית עקרון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 9,
                StationName = "אהבת אדם/האושר",
                Latitude = 32.312963,
                Longitude = 34.948685,
                Address = "אהבת אדם 68 כפר יונה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 10,
                StationName = "שדרות יצחק שמיר/שונית",
                Latitude = 32.763055,
                Longitude = 34.964147,
                Address = "שדרות יצחק שמיר 9 טירת כרמל"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 11,
                StationName = "עצמאות/יצחק שדה",
                Latitude = 31.933518,
                Longitude = 34.802311,
                Address = "עצמאות 35 נס ציונה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 12,
                StationName = "אהבת ישראל ו",
                Latitude = 31.877572,
                Longitude = 35.238152,
                Address = "אהבת ישראל כוכב יעקב"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 13,
                StationName = "טננבאום/צוקרמן",
                Latitude = 32.035488,
                Longitude = 34.894918,
                Address = "טננבאום 13 יהוד"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 14,
                StationName = "שבזי/ויצמן",
                Latitude = 31.865348,
                Longitude = 34.827102,
                Address = "שבזי 31 קרית עקרון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 15,
                StationName = "חיים בר לב/שדרות יצחק רבין",
                Latitude = 31.977409,
                Longitude = 34.763896,
                Address = "חיים ברלב ראשון לציון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 16,
                StationName = "יער אודם/אביטל",
                Latitude = 32.63711,
                Longitude = 35.085955,
                Address = "יער אודם 4 יקנעם עילית"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 17,
                StationName = "הולצמן/המדע",
                Latitude = 31.914255,
                Longitude = 34.807944,
                Address = "חיים הולצמן 2 רחובות"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 18,
                StationName = "עוזי חיטמן/אהוד מנור",
                Latitude = 32.280987,
                Longitude = 34.839573,
                Address = "עוזי חיטמן 15 נתניה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 19,
                StationName = "הרצל/גולני",
                Latitude = 31.856115,
                Longitude = 34.825249,
                Address = "הרצל 4 קרית עקרון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 20,
                StationName = "יהודה לאו פיקרד/הממציא",
                Latitude = 31.725326,
                Longitude = 35.222672,
                Address = "יהודה לאו פיקרד 11 ירושלים"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 21,
                StationName = "בית הלוחם/שמואל ברקאי",
                Latitude = 32.123097,
                Longitude = 34.808383,
                Address = " שמואל ברקאי 33 תל אביב יפו"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 22,
                StationName = "דרך מנחם בגין/הרב בן ציון אבא שאול",
                Latitude = 31.531656,
                Longitude = 34.601081,
                Address = "דרך אריאל שרון שדרות"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 23,
                StationName = "שדרות ירושלים/יובל",
                Latitude = 31.420781,
                Longitude = 34.57596,
                Address = "שדרות ירושלים 191 נתיבות"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 24,
                StationName = "איתמר/יואב",
                Latitude = 32.08949,
                Longitude = 34.80698,
                Address = "איתמר 14 רמת גן"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 25,
                StationName = "סוקולוב/גולדה מאיר",
                Latitude = 32.167578,
                Longitude = 34.901093,
                Address = "סוקולוב 53 הוד השרון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 26,
                StationName = "דרור/שלומציון המלכה",
                Latitude = 32.468281,
                Longitude = 34.966974,
                Address = "דרור 1 פרדס חנה כרכור"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 27,
                StationName = "רשות שדות התעופה/העליה",
                Latitude = 31.990876,
                Longitude = 34.8976,
                Address = "שדרות העליה נמל תעופה בן גוריון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 28,
                StationName = "אחדות העבודה/הפועל הצעיר",
                Latitude = 32.076684,
                Longitude = 34.804474,
                Address = "אחדות העבודה 18 גבעתיים"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 29,
                StationName = "דרך יצחק רבין/שדרות מעוף הציפור",
                Latitude = 32.56823,
                Longitude = 34.962049,
                Address = "רך יצחק רבין זכרון יעקב"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 30,
                StationName = "שלמה בן יוסף/זאב ז'בוטינסקי",
                Latitude = 32.926909,
                Longitude = 35.070445,
                Address = "שלמה בן יוסף 10 עכו"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 31,
                StationName = "הראשונים/כביש 5700",
                Latitude = 32.352953,
                Longitude = 34.899465,
                Address = "המגדל 13 כפר חיים"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 32,
                StationName = "החיד''א/בן איש חי",
                Latitude = 31.309127,
                Longitude = 34.621971,
                Address = "החיד''א אופקים"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 33,
                StationName = "הנחושת/המלאכה",
                Latitude = 32.016287,
                Longitude = 34.799374,
                Address = "הנחושת 4 חולון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 34,
                StationName = "קרן היסוד/ההסתדרות",
                Latitude = 32.187796,
                Longitude = 34.872476,
                Address = " קרן היסוד 38 רעננה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 35,
                StationName = "גורודסקי/יחיאל פלדי",
                Latitude = 31.898463,
                Longitude = 34.823461,
                Address = "יהודה גורודיסקי 35 רחובות"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 36,
                StationName = "דרך מנחם בגין/יעקב חזן",
                Latitude = 32.076535,
                Longitude = 34.904907,
                Address = "דרך מנחם בגין 30 פתח תקווה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 37,
                StationName = "דרך הפארק/הרב נריה",
                Latitude = 32.299994,
                Longitude = 34.878765,
                Address = "דרך הפארק 20 נתניה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 38,
                StationName = "דרך התבלינים/דודאים",
                Latitude = 29.537577,
                Longitude = 34.93285,
                Address = "דרך התבלינים 60 אילת"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 39,
                StationName = "דרך בן צבי/יהודה הימית",
                Latitude = 32.049799,
                Longitude = 34.76056,
                Address = "דרך בן צבי 12 תל אביב יפו"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 40,
                StationName = "השייטים/הדוגית",
                Latitude = 31.866905,
                Longitude = 34.734985,
                Address = "השייטים יבנה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 41,
                StationName = "מועדון פיס לגיל הזהב",
                Latitude = 32.969281,
                Longitude = 35.540372,
                Address = "דרך היקב ראש פינה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 42,
                StationName = "מנחם בגין/יצחק רבין",
                Latitude = 31.799224,
                Longitude = 34.782985,
                Address = "שדרות מנחם בגין 4 גדרה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 43,
                StationName = "ספיר/שלמה וחיה אנגל",
                Latitude = 32.198678,
                Longitude = 34.889921,
                Address = "ספיר 26 כפר סבא"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 44,
                StationName = "יהודה הנשיא/דוד רזיאל",
                Latitude = 32.155948,
                Longitude = 34.847418,
                Address = "יהודה הנשיא 28 הרצליה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 45,
                StationName = "נווה שאנן/בית אל",
                Latitude = 32.102458,
                Longitude = 35.202218,
                Address = "נווה שאנן 40 אריאל"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 46,
                StationName = "הרב שלום שבזי/נלי זקס",
                Latitude = 32.084701,
                Longitude = 34.964125,
                Address = "הרב שלום שבזי 179 ראש העין"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 47,
                StationName = "התדהר/שדרות שאול עמור",
                Latitude = 32.679749,
                Longitude = 35.243731,
                Address = "התדהר מגדל העמק"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 48,
                StationName = "אגן האיילות/היעל",
                Latitude = 31.865643,
                Longitude = 35.151746,
                Address = "האיילות 59 גבעת זאב"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 49,
                StationName = "הרב מיימון/יפה נוף",
                Latitude = 33.069153,
                Longitude = 35.142351,
                Address = "הרב מיימון 19 שלומי"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 50,
                StationName = "דרך החרושת/שדרות ישראל פולק",
                Latitude = 31.597756,
                Longitude = 34.776492,
                Address = "דרך החרושת קרית גת"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 51,
                StationName = "הרימון/התאנה",
                Latitude = 32.497692,
                Longitude = 34.923921,
                Address = "הרימון 25 אור עקיבא"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 52,
                StationName = "'קוסטה ריקה ב",
                Latitude = 31.751921,
                Longitude = 35.171162,
                Address = "קוסטה ריקה ירושלים"
            });


            Stations.Add(new DO.Station
            {
                StationNumber = 53,
                StationName = "רוקח/רקפת",
                Latitude = 32.095871,
                Longitude = 34.81454,
                Address = "רוקח 116 רמת גן"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 54,
                StationName = "בי''ס ניסויי יחד מודיעין",
                Latitude = 31.914535,
                Longitude = 35.000777,
                Address = "עמק בית שאן מודיעין מכבים רעות"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 55,
                StationName = "בי''ס נאות מרגלית לבנים",
                Latitude = 32.097895,
                Longitude = 34.888538,
                Address = "דורני שלום פתח תקווה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 56,
                StationName = "הרב בר אילן/הרב שפירא",
                Latitude = 32.089315,
                Longitude = 34.840526,
                Address = "הרב בר אילן 13 בני ברק"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 57,
                StationName = "ששת הימים/דרך יותם",
                Latitude = 29.553806,
                Longitude = 34.939021,
                Address = "שדרות ששת הימים 221 אילת"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 58,
                StationName = "מחלף חמד",
                Latitude = 31.801002,
                Longitude = 35.12454,
                Address = "יציאה למחלף חמד מטה יהודה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 59,
                StationName = "דרב אלחמרה",
                Latitude = 33.030096,
                Longitude = 35.258881,
                Address = "דרב אלחמרה מעיליא"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 60,
                StationName = "יואש דובנוב/שדרות יצחק רבין",
                Latitude = 32.615951,
                Longitude = 35.299784,
                Address = "שדרות יואש דובנוב עפולה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 61,
                StationName = "הרב שלמה גורן/מגשימים",
                Latitude = 32.086138,
                Longitude = 34.853782,
                Address = "הרב גורן גבעת שמואל"
            });


            Stations.Add(new DO.Station
            {
                StationNumber = 62,
                StationName = "בן איש חי",
                Latitude = 31.748063,
                Longitude = 35.001105,
                Address = "בן איש חי 41 בית שמש"
            });


            Stations.Add(new DO.Station
            {
                StationNumber = 63,
                StationName = "שדרות מח''ל/בית אלפא",
                Latitude = 32.83655,
                Longitude = 35.061097,
                Address = "שדרות מח''ל 21 חיפה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 64,
                StationName = "בית ספר החינוך/שערי תשובה",
                Latitude = 31.924119,
                Longitude = 31.924119,
                Address = "שערי תשובה מודיעין עילית"
            });


            Stations.Add(new DO.Station
            {
                StationNumber = 65,
                StationName = "דקר/יהונתן נתניהו",
                Latitude = 32.843112,
                Longitude = 35.084758,
                Address = "דקר 15 קרית מוצקין"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 66,
                StationName = "רש''י/סאן דייגו",
                Latitude = 31.729559,
                Longitude = 34.748849,
                Address = "שדרות רש''י 9 קרית מלאכי"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 67,
                StationName = "אבן עזרא/שלום שבזי",
                Latitude = 31.073322,
                Longitude = 35.042939,
                Address = "אבן עזרא 4 דימונה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 68,
                StationName = "בן גוריון/נאות אשקלון",
                Latitude = 31.66131,
                Longitude = 34.576455,
                Address = "שדרות דוד בן גוריון אשקלון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 69,
                StationName = "שדרות הצנחנים/גלזמן",
                Latitude = 31.225258,
                Longitude = 34.779296,
                Address = "שדרות הצנחנים 16 באר שבע"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 70,
                StationName = "קיבוץ גלויות/שבי ציון",
                Latitude = 31.805885,
                Longitude = 34.651855,
                Address = "קיבוץ גלויות 4 אשדוד"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 71,
                StationName = "רוזנבלום / רבניצקי",
                Latitude = 31.235022,
                Longitude = 34.766439,
                Address = "הרצל רוזנבלום באר שבע"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 72,
                StationName = "אפרים לוזון/קדושי סלוניקי",
                Latitude = 31.266408,
                Longitude = 34.757928,
                Address = "אפרים לוזון 63 באר שבע"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 73,
                StationName = "רבי יהודה הנשיא/רבי שמעון בן שטח",
                Latitude = 32.052943,
                Longitude = 34.964555,
                Address = "רבי יהודה הנשיא 106 אלעד"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 74,
                StationName = "אמפי בת ים/שדרות העצמאות",
                Latitude = 32.022678,
                Longitude = 34.740397,
                Address = "שדרות העצמאות 3 בת ים"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 75,
                StationName = "ההסתדרות/גולדה מאיר",
                Latitude = 31.966975,
                Longitude = 34.783226,
                Address = "ההסתדרות 8 ראשון לציון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 76,
                StationName = "חיים ארלוזורוב/הרצפלד",
                Latitude = 32.084529,
                Longitude = 34.873652,
                Address = "חיים ארלוזורוב 28 פתח תקווה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 77,
                StationName = "בן גוריון / שלמה המלך",
                Latitude = 32.053875,
                Longitude = 34.852848,
                Address = "שדרות בן גוריון קרית אונו"
            });


            Stations.Add(new DO.Station
            {
                StationNumber = 78,
                StationName = "פת/ברל לוקר",
                Latitude = 31.751267,
                Longitude = 35.202518,
                Address = "יעקב פת ירושלים"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 79,
                StationName = "נצח ירושלים/חגי",
                Latitude = 31.662327,
                Longitude = 35.154902,
                Address = "נצח ירושלים 53 אפרת"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 80,
                StationName = "שדרות השופטים/שדרות מלכי ישראל",
                Latitude = 31.61213,
                Longitude = 34.756871,
                Address = "שדרות השופטים 65 קרית גת"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 81,
                StationName = "עמנואל זיסמן/אהרון כהן",
                Latitude = 31.720627,
                Longitude = 35.230856,
                Address = "עמנואל זיסמן 13 ירושלים"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 82,
                StationName = "טובה ושמשון דרנגר/שחם",
                Latitude = 32.084205,
                Longitude = 34.857968,
                Address = "טובה ושמשון דרנגר פתח תקווה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 83,
                StationName = "פארק ארץ ישראל/משה רחמילביץ",
                Latitude = 31.82234,
                Longitude = 35.241031,
                Address = "משה רחמילביץ' 20 ירושלים"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 84,
                StationName = "נחליאלי/סייפן",
                Latitude = 31.711259,
                Longitude = 35.102147,
                Address = "נחליאלי 10 צור הדסה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 85,
                StationName = "אסף שמחוני/זמנהוף",
                Latitude = 32.074808,
                Longitude = 34.892332,
                Address = "אסף שמחוני 11 פתח תקווה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 86,
                StationName = "שדרות החוצבים",
                Latitude = 31.789812,
                Longitude = 35.141176,
                Address = "רשדרות החוצבים 12 מבשרת ציון"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 87,
                StationName = "דרך שלמה/שלבים",
                Latitude = 32.054799,
                Longitude = 34.765859,
                Address = "דרך שלמה 42 תל אביב יפו"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 88,
                StationName = "נס לגויים/היינריך היינה",
                Latitude = 32.044009,
                Longitude = 34.763015,
                Address = "נס לגויים 24 תל אביב יפו"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 89,
                StationName = "הסיבים/שוהם",
                Latitude = 32.091206,
                Longitude = 34.857191,
                Address = "הסיבים 5 פתח תקווה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 90,
                StationName = "הרב אלישיב/אמרי סופר",
                Latitude = 31.707479,
                Longitude = 35.11277,
                Address = "הרב אלישיב 3 ביתר עילית"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 91,
                StationName = "יעקב אלעזר/קאנטרי רמות",
                Latitude = 31.814188,
                Longitude = 35.209765,
                Address = "יעקב אלעזר 23 ירושלים"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 92,
                StationName = "השיטה/הרדוף הנחלים",
                Latitude = 32.474536,
                Longitude = 34.942117,
                Address = "השיטה 10 קיסריה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 93,
                StationName = "מרפאה",
                Latitude = 32.277649,
                Longitude = 34.911151,
                Address = "שדרות יצחק בן צבי 44 קדימה צורן"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 94,
                StationName = "משה דיין/רעם",
                Latitude = 32.174042,
                Longitude = 34.91968,
                Address = "משה דיין 6 כפר סבא"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 95,
                StationName = "הדקל/הכלנית",
                Latitude = 32.248448,
                Longitude = 34.915964,
                Address = "הדקל 18 תל מונד"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 96,
                StationName = "ארלוזרוב/ז'בוטינסקי",
                Latitude = 31.944554,
                Longitude = 34.888744,
                Address = "חיים ארלוזורוב 23 לוד"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 97,
                StationName = "בי''ס ברנר",
                Latitude = 32.083066,
                Longitude = 34.897829,
                Address = "עין גנים 73 פתח תקווה"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 98,
                StationName = "שדרות עמק איילון/חרמון",
                Latitude = 31.996882,
                Longitude = 34.946147,
                Address = "שדרות עמק איילון 33 שוהם"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 99,
                StationName = "יהושע בן נון/הרש''ש",
                Latitude = 32.103353,
                Longitude = 34.949185,
                Address = "יהושע בן נון 37 ראש העין"
            });

            Stations.Add(new DO.Station
            {
                StationNumber = 100,
                StationName = "בן פורת/ראשונים",
                Latitude = 32.028349,
                Longitude = 34.850753,
                Address = "שדרות מרדכי בן פורת 49 אור יהודה"
            });
        }

        #endregion

        #region InitializeBuses

        public static void InitializeBuses()
        {
            Buses.Add(new DO.Bus
            {
                LicenseNumber = "12354688",
                StartDate = DateTime.Today.AddYears(-1),
                Mileage = 150,
                Fuel = 1100,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "25478965",
                StartDate = DateTime.Today.AddYears(-1),
                Mileage = 160,
                Fuel = 1000,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "25487659",
                StartDate = DateTime.Today.AddYears(-1),
                Mileage = 25,
                Fuel = 260,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "24875965",
                StartDate = DateTime.Today.AddYears(-3),
                Mileage = 358,
                Fuel = 56,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            }); ;

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "7854629",
                StartDate = DateTime.Today.AddYears(-4),
                Mileage = 57,
                Fuel = 68,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "1245876",
                StartDate = DateTime.Today.AddYears(-5),
                Mileage = 682,
                Fuel = 320,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "4587125",
                StartDate = DateTime.Today.AddYears(-5),
                Mileage = 526,
                Fuel = 345,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "1298563",
                StartDate = DateTime.Today.AddYears(-6),
                Mileage = 23,
                Fuel = 12,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "1478596",
                StartDate = DateTime.Today.AddYears(-8),
                Mileage = 289,
                Fuel = 752,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "14785236",
                StartDate = DateTime.Today.AddYears(-9),
                Mileage = 854,
                Fuel = 987,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "4586215",
                StartDate = DateTime.Today.AddYears(-15),
                Mileage = 98,
                Fuel = 45,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "4578963",
                StartDate = DateTime.Today.AddYears(-9),
                Mileage = 57,
                Fuel = 75,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "9874563",
                StartDate = DateTime.Today.AddYears(-8),
                Mileage = 25,
                Fuel = 69,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "1236987",
                StartDate = DateTime.Today.AddYears(-23),
                Mileage = 98,
                Fuel = 89,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "7412589",
                StartDate = DateTime.Today.AddYears(-95),
                Mileage = 230,
                Fuel = 320,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "9874100",
                StartDate = DateTime.Today.AddYears(-25),
                Mileage = 201,
                Fuel = 101,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "1235795",
                StartDate = DateTime.Today.AddYears(-30),
                Mileage = 59,
                Fuel = 95,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "7852143",
                StartDate = DateTime.Today.AddYears(-53),
                Mileage = 65,
                Fuel = 52,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "7854123",
                StartDate = DateTime.Today.AddYears(-45),
                Mileage = 1231,
                Fuel = 1524,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "1234567",
                StartDate = DateTime.Today.AddYears(-78),
                Mileage = 4521,
                Fuel = 3254,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });

            Buses.Add(new DO.Bus
            {
                LicenseNumber = "9512357",
                StartDate = DateTime.Today.AddYears(-75),
                Mileage = 5478,
                Fuel = 9874,
                Treatment = DateTime.Today,
                Status = STATUS.READY,
            });
        }

        #endregion

        #region InitializeBusLines

        public static AREA AreaGenerator()
        {
            int num = rand.Next(1, 6);
            if (num == 1)
                return AREA.CENTER;
            if (num == 2)
                return AREA.GENERAL;
            if (num == 3)
                return AREA.JERUSALEM;
            if (num == 4)
                return AREA.NORTH;
            if (num == 5)
                return AREA.SOUTH;
            return AREA.NULL;
        }

        public static void InitializeBusLines()
        {
            for (int i = 1, j = 1; i <= 15; i++, j += 10)
            {
                BusLines.Add(new DO.BusLine
                {
                    BusLineIndex = Config.BusLineIndex,
                    LineNumber = i,
                    Area = AreaGenerator(),
                    FirstStation = j,
                    LastStation = j + 9,
                });
            }
        }

        #endregion

        #region InitializeLineStations

        public static void InitializeLineStations()
        {
            for (int i = 1, j = 1; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 1).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }

            for (int i = 1, j = 11; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 2).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }

            for (int i = 1, j = 21; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 3).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }

            for (int i = 1, j = 31; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 4).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }

            for (int i = 1, j = 41; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 5).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }

            for (int i = 1, j = 51; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 6).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }

            for (int i = 1, j = 61; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 7).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }

            for (int i = 1, j = 71; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 8).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }

            for (int i = 1, j = 81; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 9).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }

            for (int i = 1, j = 91; i <= 10; i++, j++)
            {
                LineStations.Add(new DO.LineStation
                {
                    BusLineIndex = BusLines.Find(item => item.LineNumber == 10).BusLineIndex,
                    StationNumber = j,
                    NumberOnLine = i
                });
            }
        }

        #endregion

        #region InitializeConsecutiveStations

        public static void InitializeConsecutiveStations()
        {
            int StationOne = 1;
            int StationTwo = 2;

            for (int i = 1; i < 10; i++) //1-10
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 1,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //hour, minutes, seconds
                });

                StationOne++;
                StationTwo++;
            }

            StationOne++; //11
            StationTwo++; //12

            for (int i = 1; i < 10; i++) //11-20
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 2,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //in minutes
                });

                StationOne++;
                StationTwo++;
            }

            StationOne++; //21
            StationTwo++; //22

            for (int i = 1; i < 10; i++) //21-30
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 3,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //in minutes
                });

                StationOne++;
                StationTwo++;
            }

            StationOne++; //31
            StationTwo++; //32

            for (int i = 1; i < 10; i++) //31-40
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 4,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //in minutes
                });

                StationOne++;
                StationTwo++;
            }

            StationOne++; //41
            StationTwo++; //42

            for (int i = 1; i < 10; i++) //41-50
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 5,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //in minutes
                });

                StationOne++;
                StationTwo++;
            }

            StationOne++; //51
            StationTwo++; //52

            for (int i = 1; i < 10; i++) //51-60
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 6,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //in minutes
                });

                StationOne++;
                StationTwo++;
            }

            StationOne++; //61
            StationTwo++; //62

            for (int i = 1; i < 10; i++) //61-70
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 7,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //in minutes
                });

                StationOne++;
                StationTwo++;
            }

            StationOne++; //71
            StationTwo++; //72

            for (int i = 1; i < 10; i++) //71-80
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 8,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //in minutes
                });

                StationOne++;
                StationTwo++;
            }

            StationOne++; //81
            StationTwo++; //82

            for (int i = 1; i < 10; i++) //81-90
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 9,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //in minutes
                });

                StationOne++;
                StationTwo++;
            }

            StationOne++; //91
            StationTwo++; //92

            for (int i = 1; i < 10; i++) //91-100
            {
                ConsecutiveStations.Add(new DO.ConsecutiveStations
                {
                    BusLineIndex = 10,
                    ConsecutiveStationsIndex = Config.ConsecutiveStationsIndex,
                    FirstStation = StationOne,
                    SecondStation = StationTwo,
                    Distance = rand.Next(10, 10000), //in meters
                    AvgTravelTime = new TimeSpan(rand.Next(2, 7), 0, 0) //in minutes
                });

                StationOne++;
                StationTwo++;
            }
        }

        #endregion

        #region InitializeLeavingLines

        public static void InitializeLeavingLines()
        {
            int y = 6;
            for (int i = 0; i <=15; i++, y++) //creats 5 leaving schedule for each line with random values
            {
                
                foreach (var line in BusLines) //goes throw all the lines in the system
                {
                    LeavingLines.Add(new LeavingLine
                    {

                        LeavingLineIndex = Config.LeavingLineIndex,
                        BusLineIndex = line.BusLineIndex,
                        Start = new TimeSpan(rand.Next(6, 12), 0, 0)
                    }) ;
                }
            }
        }


        #endregion

        #region InitializeUsers

        public static void InitializeUsers()
        {
            Users.Add(new DO.User
            {
                UserName = "Oria123",
                Password = "Oria123",
                Permission = true
            });

            Users.Add(new DO.User
            {
                UserName = "Hilla123",
                Password = "Hilla123",
                Permission = true
            });

            Users.Add(new DO.User
            {
                UserName = "Yair123",
                Password = "Yair123",
                Permission = true
            });

        }
        #endregion

        #region serializer

        public static void saveListToXml(List<Station> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
            fs.Close();
        }

        public static void saveListToXml(List<Bus> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
            fs.Close();
        }

        public static void saveListToXml(List<BusLine> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
            fs.Close();
        }

        public static void saveListToXml(List<LineStation> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
            fs.Close();
        }

        //public static void saveListToXml(List<ConsecutiveStations> list, string path)
        //{
        //    XmlSerializer x = new XmlSerializer(list.GetType());
        //    FileStream fs = new FileStream(path, FileMode.Create);
        //    x.Serialize(fs, list);
        //    fs.Close();
        //}

        public static void saveListToXml(List<LeavingLine> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
            fs.Close();
        }

        public static void saveListToXml(List<User> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
            fs.Close();
        }

        public static void saveListToXml(List<UserDrive> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
            fs.Close();
        }

        public static void saveListToXml(List<DrivingBus> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
            fs.Close();
        }



        #endregion

    }
}