using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace SMM
{
    public static class Config
    {
        #region Variables

        public static List<Key> settings = new List<Key>();
        public static string destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager", "config.xml");

        #endregion

        #region Config Variables

        public static void AddVariable(string name, string value)
        {
            Key k = new Key();
            k.name = name;
            k.value = value;

            try
            {
                foreach (Key key in settings)
                    if (key.name == k.name)
                        settings.Remove(key);
            }
            catch { }

            settings.Add(k);
        }

        public static void AddVariable(string name, object value)
        {
            AddVariable(name, value.ToString());
        }

        public static void AddVariable(string name, int value)
        {
            AddVariable(name, value.ToString());
        }

        public static void AddVariable(string name, float value)
        {
            AddVariable(name, value.ToString("R").Replace(',', '.'));
        }

        public static object ReadVariable(string name)
        {
            foreach (Key k in settings)
                if (k.name == name)
                    return k.value;

            throw new KeyNotFoundException();
        }

        public static object TryReadVariable(string name)
        {
            try
            {
                return ReadVariable(name);
            }
            catch { }

            return null;
        }

        public static object ReadString(string name)
        {
            return (string)ReadVariable(name);
        }

        public static object TryReadString(string name)
        {
            return (string)TryReadVariable(name);
        }

        public static float ReadFloat(string name)
        {
            return float.Parse((string)ReadVariable(name), CultureInfo.InvariantCulture.NumberFormat);
        }

        public static float TryReadFloat(string name)
        {
            return float.Parse((string)TryReadVariable(name), CultureInfo.InvariantCulture.NumberFormat);
        }

        public static int ReadInt(string name)
        {
            return Convert.ToInt32((string)ReadVariable(name));
        }

        public static int TryReadInt(string name)
        {
            return Convert.ToInt32((string)TryReadVariable(name));
        }

        #endregion

        #region IO

        public static void Save()
        {
            TextWriter w = new StreamWriter(destination);
            XmlSerializer s = new XmlSerializer(typeof(Key[]));

            s.Serialize(w, settings.ToArray());
            w.Close();
            w.Dispose();
        }

        public static void Load()
        {
            try
            {
                settings.Clear();

                TextReader w = new StreamReader(destination);
                XmlSerializer s = new XmlSerializer(typeof(Key[]));

                foreach (Key k in (Key[])s.Deserialize(w))
                    settings.Add(k);

                w.Close();
                w.Dispose();
            }
            catch { }
        }

        #endregion

        public class KeyNotFoundException : Exception
        {
            public override string Message => "Key with matching name not found";
        }
    }

    public class Key
    {
        public string name;
        public string value;
    }
}
